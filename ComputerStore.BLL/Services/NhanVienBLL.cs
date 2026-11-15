
using System;
using System.Collections.Generic;
using ComputerStore.DAL.Repositories;
using ComputerStore.DTO.Entities;

namespace ComputerStore.BLL.Services
{
    public class NhanVienBLL
    {
        private readonly NhanVienDAL nhanVienDAL;

        public NhanVienBLL()
        {
            nhanVienDAL = new NhanVienDAL();
        }

        // 1. LAY DANH SACH NHAN VIEN
        public List<NhanVienDTO> GetAll()
        {
            return nhanVienDAL.GetAll();
        }

        // 2. TIM NHAN VIEN THEO MA
        public NhanVienDTO GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "Ma nhan vien khong hop le.");

            return nhanVienDAL.GetById(id);
        }

        // 3. THEM NHAN VIEN
        public bool Insert(NhanVienDTO nhanVien)
        {
            ValidateNhanVien(nhanVien);

            return nhanVienDAL.Insert(nhanVien);
        }

        // 4. CAP NHAT NHAN VIEN
        public bool Update(NhanVienDTO nhanVien)
        {
            if (nhanVien == null)
                throw new ArgumentNullException(
                    nameof(nhanVien));

            if (nhanVien.MaNhanVien <= 0)
                throw new ArgumentException(
                    "Ma nhan vien khong hop le.");

            ValidateNhanVien(nhanVien);

            return nhanVienDAL.Update(nhanVien);
        }

        // 5. XOA NHAN VIEN
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "Ma nhan vien khong hop le.");

            return nhanVienDAL.Delete(id);
        }

        // 6. KIEM TRA DU LIEU NHAN VIEN
        private void ValidateNhanVien(NhanVienDTO nhanVien)
        {
            if (nhanVien == null)
                throw new ArgumentNullException(
                    nameof(nhanVien));

            if (string.IsNullOrWhiteSpace(nhanVien.HoTen))
                throw new ArgumentException(
                    "Ho ten nhan vien khong duoc de trong.");

            if (nhanVien.HoTen.Trim().Length > 100)
                throw new ArgumentException(
                    "Ho ten khong duoc vuot qua 100 ky tu.");

            if (!string.IsNullOrWhiteSpace(nhanVien.SoDienThoai)
                && nhanVien.SoDienThoai.Trim().Length > 15)
            {
                throw new ArgumentException(
                    "So dien thoai khong duoc vuot qua 15 ky tu.");
            }

            if (!string.IsNullOrWhiteSpace(nhanVien.Email)
                && nhanVien.Email.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Email khong duoc vuot qua 100 ky tu.");
            }

            if (nhanVien.TrangThai != 0 &&
                nhanVien.TrangThai != 1)
            {
                throw new ArgumentException(
                    "Trang thai chi duoc nhan 0 hoac 1.");
            }
        }
    }
}
