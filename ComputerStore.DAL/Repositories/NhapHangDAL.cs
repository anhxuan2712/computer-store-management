using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using ComputerStore.DAL.Database;
using ComputerStore.DTO.Entities;

namespace ComputerStore.DAL.Repositories
{
    public class NhapHangDAL
    {
        // 1. DANH SACH NHA CUNG CAP DANG HOAT DONG
        public DataTable GetNhaCungCap()
        {
            return Query(@"
                SELECT MaNhaCungCap, TenNhaCungCap
                FROM NhaCungCap
                WHERE TrangThai = 1
                ORDER BY TenNhaCungCap;");
        }

        // 2. DANH SACH SAN PHAM DANG KINH DOANH (MA, TEN, GIA NHAP, TON KHO)
        public DataTable GetSanPham()
        {
            return Query(@"
                SELECT
                    MaSanPham,
                    TenSanPham,
                    GiaNhap,
                    SoLuongTon,
                    CONCAT(MaSanPham, ' - ', TenSanPham) AS HienThi
                FROM SanPham
                WHERE TrangThai = 1
                ORDER BY TenSanPham;");
        }

        // 3. DANH SACH PHIEU NHAP
        public DataTable GetDanhSachPhieuNhap()
        {
            return Query(@"
                SELECT
                    pn.MaPhieuNhap,
                    pn.NgayNhap,
                    ncc.TenNhaCungCap,
                    nv.HoTen AS NhanVien,
                    pn.TongTien,
                    pn.TrangThai,
                    pn.GhiChu
                FROM PhieuNhap pn
                JOIN NhaCungCap ncc ON ncc.MaNhaCungCap = pn.MaNhaCungCap
                JOIN NhanVien nv ON nv.MaNhanVien = pn.MaNhanVien
                ORDER BY pn.NgayNhap DESC;");
        }

        // 4. CHI TIET MOT PHIEU NHAP
        public DataTable GetChiTietPhieuNhap(int maPhieuNhap)
        {
            return Query(@"
                SELECT
                    ct.MaSanPham,
                    sp.TenSanPham,
                    ct.SoLuong,
                    ct.DonGiaNhap,
                    ct.ThanhTien
                FROM ChiTietPhieuNhap ct
                JOIN SanPham sp ON sp.MaSanPham = ct.MaSanPham
                WHERE ct.MaPhieuNhap = @MaPhieuNhap;",
                new MySqlParameter("@MaPhieuNhap", maPhieuNhap));
        }

        // 5. LUU PHIEU NHAP DA XAC NHAN
        // Luu phieu, chi tiet va cong ton kho trong cung mot transaction.
        // Loi o bat ky buoc nao se rollback toan bo. Tra ve ma phieu nhap vua tao.
        public int Insert(PhieuNhapDTO phieuNhap, List<ChiTietPhieuNhapDTO> chiTiet)
        {
            using (MySqlConnection connection = DbConnection.GetConnection())
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertPhieu = @"
                            INSERT INTO PhieuNhap
                                (MaNhaCungCap, MaNhanVien, NgayNhap, TongTien, TrangThai, GhiChu)
                            VALUES
                                (@MaNhaCungCap, @MaNhanVien, @NgayNhap, @TongTien, @TrangThai, @GhiChu);";

                        using (MySqlCommand command = new MySqlCommand(insertPhieu, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@MaNhaCungCap", phieuNhap.MaNhaCungCap);
                            command.Parameters.AddWithValue("@MaNhanVien", phieuNhap.MaNhanVien);
                            command.Parameters.AddWithValue("@NgayNhap", phieuNhap.NgayNhap);
                            command.Parameters.AddWithValue("@TongTien", phieuNhap.TongTien);
                            command.Parameters.AddWithValue("@TrangThai", phieuNhap.TrangThai);
                            command.Parameters.AddWithValue("@GhiChu", (object?)phieuNhap.GhiChu ?? DBNull.Value);
                            command.ExecuteNonQuery();

                            phieuNhap.MaPhieuNhap = (int)command.LastInsertedId;
                        }

                        foreach (ChiTietPhieuNhapDTO ct in chiTiet)
                        {
                            ct.MaPhieuNhap = phieuNhap.MaPhieuNhap;

                            // ThanhTien la generated column nen khong insert.
                            string insertChiTiet = @"
                                INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaSanPham, SoLuong, DonGiaNhap)
                                VALUES (@MaPhieuNhap, @MaSanPham, @SoLuong, @DonGiaNhap);";

                            using (MySqlCommand command = new MySqlCommand(insertChiTiet, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@MaPhieuNhap", ct.MaPhieuNhap);
                                command.Parameters.AddWithValue("@MaSanPham", ct.MaSanPham);
                                command.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                                command.Parameters.AddWithValue("@DonGiaNhap", ct.DonGiaNhap);
                                command.ExecuteNonQuery();
                            }

                            // Tang ton kho va cap nhat gia nhap moi nhat cua san pham.
                            string updateTonKho = @"
                                UPDATE SanPham
                                SET SoLuongTon = SoLuongTon + @SoLuong,
                                    GiaNhap = @DonGiaNhap
                                WHERE MaSanPham = @MaSanPham;";

                            using (MySqlCommand command = new MySqlCommand(updateTonKho, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                                command.Parameters.AddWithValue("@DonGiaNhap", ct.DonGiaNhap);
                                command.Parameters.AddWithValue("@MaSanPham", ct.MaSanPham);

                                if (command.ExecuteNonQuery() == 0)
                                {
                                    throw new InvalidOperationException(
                                        $"Không tìm thấy sản phẩm có mã {ct.MaSanPham}.");
                                }
                            }
                        }

                        transaction.Commit();
                        return phieuNhap.MaPhieuNhap;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // 6. HAM CHAY CAU SELECT TRA VE DATATABLE
        private DataTable Query(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = DbConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                command.Parameters.AddRange(parameters);

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}
