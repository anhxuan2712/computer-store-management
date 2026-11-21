using System;
using System.Security.Cryptography;
using System.Text;
using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;

namespace ComputerStore.BLL.Services
{
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL taiKhoanDAL;
        private readonly NhanVienDAL nhanVienDAL;

        public TaiKhoanBLL()
        {
            taiKhoanDAL = new TaiKhoanDAL();
            nhanVienDAL = new NhanVienDAL();
        }

        // 1. BAM MAT KHAU BANG SHA-256 (KHOP VOI HAM SHA2(..., 256) TRONG MYSQL)
        public static string HashPassword(string plainPassword)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(plainPassword));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        // 2. DANG NHAP HE THONG
        public bool Login(
            string tenDangNhap,
            string matKhau,
            out TaiKhoanDTO? taiKhoan,
            out NhanVienDTO? nhanVien,
            out string message)
        {
            taiKhoan = null;
            nhanVien = null;

            tenDangNhap = tenDangNhap?.Trim() ?? string.Empty;
            matKhau = matKhau ?? string.Empty;

            if (tenDangNhap.Length == 0)
            {
                message = "Vui lòng nhập tên đăng nhập.";
                return false;
            }

            if (matKhau.Length == 0)
            {
                message = "Vui lòng nhập mật khẩu.";
                return false;
            }

            TaiKhoanDTO? tk = taiKhoanDAL.GetByUsername(tenDangNhap);
            if (tk == null)
            {
                message = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return false;
            }

            string hashedInput = HashPassword(matKhau);
            if (!string.Equals(tk.MatKhau, hashedInput, StringComparison.OrdinalIgnoreCase))
            {
                message = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return false;
            }

            if (tk.TrangThai != 1)
            {
                message = "Tài khoản này đang bị khóa. Vui lòng liên hệ quản trị viên.";
                return false;
            }

            NhanVienDTO? nv = nhanVienDAL.GetById(tk.MaNhanVien);
            if (nv == null || nv.TrangThai != 1)
            {
                message = "Nhân viên của tài khoản này đã ngừng làm việc.";
                return false;
            }

            taiKhoan = tk;
            nhanVien = nv;
            message = "Đăng nhập thành công.";
            return true;
        }

        // 3. LAY TAI KHOAN THEO MA NHAN VIEN
        public TaiKhoanDTO? GetByMaNhanVien(int maNhanVien)
        {
            if (maNhanVien <= 0)
                return null;

            return taiKhoanDAL.GetByMaNhanVien(maNhanVien);
        }

        // 4. CAP MOI HOAC CAP NHAT TAI KHOAN CHO NHAN VIEN
        public bool SaveAccount(
            int maNhanVien,
            string tenDangNhap,
            string matKhau,
            string vaiTro,
            int trangThai,
            out string message)
        {
            if (maNhanVien <= 0)
            {
                message = "Vui lòng chọn nhân viên cần cấp/cập nhật tài khoản.";
                return false;
            }

            tenDangNhap = tenDangNhap?.Trim() ?? string.Empty;
            matKhau = matKhau ?? string.Empty;
            vaiTro = vaiTro?.Trim() ?? "NhanVien";

            if (tenDangNhap.Length < 3 || tenDangNhap.Length > 50)
            {
                message = "Tên đăng nhập phải từ 3 đến 50 ký tự.";
                return false;
            }

            if (tenDangNhap.Contains(' '))
            {
                message = "Tên đăng nhập không được chứa khoảng trắng.";
                return false;
            }

            if (vaiTro != "Admin" && vaiTro != "NhanVien")
            {
                message = "Vai trò chỉ được chọn Admin hoặc NhanVien.";
                return false;
            }

            if (trangThai != 0 && trangThai != 1)
            {
                message = "Trạng thái tài khoản không hợp lệ.";
                return false;
            }

            TaiKhoanDTO? existing = taiKhoanDAL.GetByMaNhanVien(maNhanVien);
            int currentAccountId = existing?.MaTaiKhoan ?? 0;

            if (taiKhoanDAL.ExistsUsername(tenDangNhap, currentAccountId))
            {
                message = "Tên đăng nhập đã được sử dụng bởi nhân viên khác.";
                return false;
            }

            if (existing == null)
            {
                if (matKhau.Length < 6)
                {
                    message = "Mật khẩu tạo mới phải có ít nhất 6 ký tự.";
                    return false;
                }

                var newAccount = new TaiKhoanDTO
                {
                    MaNhanVien = maNhanVien,
                    TenDangNhap = tenDangNhap,
                    MatKhau = HashPassword(matKhau),
                    VaiTro = vaiTro,
                    TrangThai = trangThai
                };

                taiKhoanDAL.Insert(newAccount);
                message = "Đã tạo tài khoản cho nhân viên.";
                return true;
            }

            bool updatePassword = matKhau.Length > 0;
            if (updatePassword && matKhau.Length < 6)
            {
                message = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                return false;
            }

            existing.TenDangNhap = tenDangNhap;
            existing.VaiTro = vaiTro;
            existing.TrangThai = trangThai;
            if (updatePassword)
            {
                existing.MatKhau = HashPassword(matKhau);
            }

            taiKhoanDAL.Update(existing, updatePassword);
            message = "Đã cập nhật tài khoản thành công.";
            return true;
        }
    }
}
