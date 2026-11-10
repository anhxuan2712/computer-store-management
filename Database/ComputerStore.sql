
-- =====================================================
-- COMPUTER STORE MANAGEMENT SYSTEM
-- Database schema: ComputerStore
-- MySQL Server 8.x
-- =====================================================

CREATE DATABASE IF NOT EXISTS ComputerStore
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE ComputerStore;

-- =====================================================
-- 1. TABLE: NhanVien
-- =====================================================

CREATE TABLE IF NOT EXISTS NhanVien (
    MaNhanVien INT AUTO_INCREMENT PRIMARY KEY,
    HoTen VARCHAR(100) NOT NULL,
    GioiTinh ENUM('Nam', 'Nu', 'Khac') DEFAULT 'Khac',
    NgaySinh DATE,
    SoDienThoai VARCHAR(15) UNIQUE,
    Email VARCHAR(100) UNIQUE,
    DiaChi VARCHAR(255),
    NgayVaoLam DATE,
    TrangThai TINYINT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT chk_nhanvien_trangthai
        CHECK (TrangThai IN (0, 1))
) ENGINE=InnoDB;

-- =====================================================
-- 2. TABLE: TaiKhoan
-- =====================================================

CREATE TABLE IF NOT EXISTS TaiKhoan (
    MaTaiKhoan INT AUTO_INCREMENT PRIMARY KEY,
    MaNhanVien INT NOT NULL UNIQUE,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL,
    VaiTro ENUM('Admin', 'NhanVien') NOT NULL DEFAULT 'NhanVien',
    TrangThai TINYINT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_taikhoan_nhanvien
        FOREIGN KEY (MaNhanVien)
        REFERENCES NhanVien(MaNhanVien)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT chk_taikhoan_trangthai
        CHECK (TrangThai IN (0, 1))
) ENGINE=InnoDB;

-- =====================================================
-- 3. TABLE: KhachHang
-- =====================================================

CREATE TABLE IF NOT EXISTS KhachHang (
    MaKhachHang INT AUTO_INCREMENT PRIMARY KEY,
    HoTen VARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) UNIQUE,
    Email VARCHAR(100),
    DiaChi VARCHAR(255),
    NgayTao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- =====================================================
-- 4. TABLE: DanhMuc
-- Giữ nguyên 6 mã danh mục theo README
-- =====================================================

CREATE TABLE IF NOT EXISTS DanhMuc (
    MaDanhMuc VARCHAR(10) PRIMARY KEY,
    TenDanhMuc VARCHAR(100) NOT NULL UNIQUE,
    MoTa VARCHAR(255)
) ENGINE=InnoDB;

-- =====================================================
-- 5. TABLE: NhaCungCap
-- =====================================================

CREATE TABLE IF NOT EXISTS NhaCungCap (
    MaNhaCungCap INT AUTO_INCREMENT PRIMARY KEY,
    TenNhaCungCap VARCHAR(150) NOT NULL,
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100),
    DiaChi VARCHAR(255),
    TrangThai TINYINT NOT NULL DEFAULT 1,

    CONSTRAINT chk_nhacungcap_trangthai
        CHECK (TrangThai IN (0, 1))
) ENGINE=InnoDB;

-- =====================================================
-- 6. TABLE: SanPham
-- =====================================================

CREATE TABLE IF NOT EXISTS SanPham (
    MaSanPham INT AUTO_INCREMENT PRIMARY KEY,
    TenSanPham VARCHAR(150) NOT NULL,
    MaDanhMuc VARCHAR(10) NOT NULL,
    DonViTinh VARCHAR(30) NOT NULL DEFAULT 'Cai',
    GiaNhap DECIMAL(15,2) NOT NULL DEFAULT 0,
    GiaBan DECIMAL(15,2) NOT NULL DEFAULT 0,
    SoLuongTon INT NOT NULL DEFAULT 0,
    MoTa TEXT,
    TrangThai TINYINT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_sanpham_danhmuc
        FOREIGN KEY (MaDanhMuc)
        REFERENCES DanhMuc(MaDanhMuc)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT chk_sanpham_gianhap
        CHECK (GiaNhap >= 0),

    CONSTRAINT chk_sanpham_giaban
        CHECK (GiaBan >= 0),

    CONSTRAINT chk_sanpham_soluong
        CHECK (SoLuongTon >= 0),

    CONSTRAINT chk_sanpham_trangthai
        CHECK (TrangThai IN (0, 1)),

    INDEX idx_sanpham_ten (TenSanPham),
    INDEX idx_sanpham_danhmuc (MaDanhMuc)
) ENGINE=InnoDB;

-- =====================================================
-- 7. TABLE: PhieuNhap
-- =====================================================

CREATE TABLE IF NOT EXISTS PhieuNhap (
    MaPhieuNhap INT AUTO_INCREMENT PRIMARY KEY,
    MaNhaCungCap INT NOT NULL,
    MaNhanVien INT NOT NULL,
    NgayNhap DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TongTien DECIMAL(15,2) NOT NULL DEFAULT 0,
    TrangThai ENUM('ChoXacNhan', 'DaXacNhan', 'DaHuy')
        NOT NULL DEFAULT 'ChoXacNhan',
    GhiChu VARCHAR(255),

    CONSTRAINT fk_phieunhap_nhacungcap
        FOREIGN KEY (MaNhaCungCap)
        REFERENCES NhaCungCap(MaNhaCungCap)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT fk_phieunhap_nhanvien
        FOREIGN KEY (MaNhanVien)
        REFERENCES NhanVien(MaNhanVien)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT chk_phieunhap_tongtien
        CHECK (TongTien >= 0),

    INDEX idx_phieunhap_ngay (NgayNhap)
) ENGINE=InnoDB;

-- =====================================================
-- 8. TABLE: ChiTietPhieuNhap
-- =====================================================

CREATE TABLE IF NOT EXISTS ChiTietPhieuNhap (
    MaChiTietPhieuNhap INT AUTO_INCREMENT PRIMARY KEY,
    MaPhieuNhap INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGiaNhap DECIMAL(15,2) NOT NULL,
    ThanhTien DECIMAL(15,2)
        GENERATED ALWAYS AS (SoLuong * DonGiaNhap) STORED,

    CONSTRAINT fk_ctphieunhap_phieunhap
        FOREIGN KEY (MaPhieuNhap)
        REFERENCES PhieuNhap(MaPhieuNhap)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT fk_ctphieunhap_sanpham
        FOREIGN KEY (MaSanPham)
        REFERENCES SanPham(MaSanPham)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT uq_ctphieunhap_sanpham
        UNIQUE (MaPhieuNhap, MaSanPham),

    CONSTRAINT chk_ctphieunhap_soluong
        CHECK (SoLuong > 0),

    CONSTRAINT chk_ctphieunhap_dongia
        CHECK (DonGiaNhap >= 0)
) ENGINE=InnoDB;

-- =====================================================
-- 9. TABLE: HoaDon
-- =====================================================

CREATE TABLE IF NOT EXISTS HoaDon (
    MaHoaDon INT AUTO_INCREMENT PRIMARY KEY,
    MaKhachHang INT NULL,
    MaNhanVien INT NOT NULL,
    NgayLap DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TongTien DECIMAL(15,2) NOT NULL DEFAULT 0,
    TrangThai ENUM('ChoThanhToan', 'DaThanhToan', 'DaHuy')
        NOT NULL DEFAULT 'ChoThanhToan',
    GhiChu VARCHAR(255),

    CONSTRAINT fk_hoadon_khachhang
        FOREIGN KEY (MaKhachHang)
        REFERENCES KhachHang(MaKhachHang)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT fk_hoadon_nhanvien
        FOREIGN KEY (MaNhanVien)
        REFERENCES NhanVien(MaNhanVien)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT chk_hoadon_tongtien
        CHECK (TongTien >= 0),

    INDEX idx_hoadon_ngay (NgayLap)
) ENGINE=InnoDB;

-- =====================================================
-- 10. TABLE: ChiTietHoaDon
-- =====================================================

CREATE TABLE IF NOT EXISTS ChiTietHoaDon (
    MaChiTietHoaDon INT AUTO_INCREMENT PRIMARY KEY,
    MaHoaDon INT NOT NULL,
    MaSanPham INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGiaBan DECIMAL(15,2) NOT NULL,
    ThanhTien DECIMAL(15,2)
        GENERATED ALWAYS AS (SoLuong * DonGiaBan) STORED,

    CONSTRAINT fk_cthoadon_hoadon
        FOREIGN KEY (MaHoaDon)
        REFERENCES HoaDon(MaHoaDon)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT fk_cthoadon_sanpham
        FOREIGN KEY (MaSanPham)
        REFERENCES SanPham(MaSanPham)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,

    CONSTRAINT uq_cthoadon_sanpham
        UNIQUE (MaHoaDon, MaSanPham),

    CONSTRAINT chk_cthoadon_soluong
        CHECK (SoLuong > 0),

    CONSTRAINT chk_cthoadon_dongia
        CHECK (DonGiaBan >= 0)
) ENGINE=InnoDB;

-- =====================================================
-- END OF DATABASE SCHEMA
-- =====================================================
