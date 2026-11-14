-- =============================================================
-- ComputerStore - Schema MySQL 8.x (bản đề xuất)
-- LƯU Ý: Đây là bản nháp do Thành viên 3 tạo để chạy được chức năng
-- Khách hàng / Nhập hàng / Bán hàng. Cả nhóm cần review và thống nhất
-- trước khi coi đây là schema chính.
-- =============================================================

CREATE DATABASE IF NOT EXISTS ComputerStore
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE ComputerStore;

CREATE TABLE IF NOT EXISTS NhanVien (
    MaNhanVien   VARCHAR(10)  NOT NULL PRIMARY KEY,
    HoTen        VARCHAR(100) NOT NULL,
    GioiTinh     VARCHAR(5)   NULL,
    NgaySinh     DATE         NULL,
    SoDienThoai  VARCHAR(15)  NULL,
    DiaChi       VARCHAR(255) NULL,
    ChucVu       VARCHAR(50)  NULL
);

CREATE TABLE IF NOT EXISTS TaiKhoan (
    TenDangNhap  VARCHAR(50)  NOT NULL PRIMARY KEY,
    MatKhau      VARCHAR(255) NOT NULL,
    MaNhanVien   VARCHAR(10)  NOT NULL UNIQUE,
    VaiTro       VARCHAR(20)  NOT NULL DEFAULT 'NhanVien',
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE IF NOT EXISTS KhachHang (
    MaKhachHang  VARCHAR(10)  NOT NULL PRIMARY KEY,
    TenKhachHang VARCHAR(100) NOT NULL,
    SoDienThoai  VARCHAR(15)  NOT NULL UNIQUE,
    DiaChi       VARCHAR(255) NULL,
    Email        VARCHAR(100) NULL
);

CREATE TABLE IF NOT EXISTS DanhMuc (
    MaDanhMuc    VARCHAR(10)  NOT NULL PRIMARY KEY,
    TenDanhMuc   VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS SanPham (
    MaSanPham    VARCHAR(10)   NOT NULL PRIMARY KEY,
    TenSanPham   VARCHAR(200)  NOT NULL,
    MaDanhMuc    VARCHAR(10)   NOT NULL,
    ThuongHieu   VARCHAR(100)  NULL,
    GiaNhap      DECIMAL(15,2) NOT NULL DEFAULT 0,
    GiaBan       DECIMAL(15,2) NOT NULL DEFAULT 0,
    SoLuongTon   INT           NOT NULL DEFAULT 0,
    BaoHanh      INT           NULL COMMENT 'Số tháng bảo hành',
    MoTa         TEXT          NULL,
    CONSTRAINT FK_SanPham_DanhMuc FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc),
    CONSTRAINT CK_SanPham_SoLuongTon CHECK (SoLuongTon >= 0)
);

CREATE TABLE IF NOT EXISTS NhaCungCap (
    MaNhaCungCap  VARCHAR(10)  NOT NULL PRIMARY KEY,
    TenNhaCungCap VARCHAR(200) NOT NULL,
    SoDienThoai   VARCHAR(15)  NULL,
    DiaChi        VARCHAR(255) NULL,
    Email         VARCHAR(100) NULL
);

CREATE TABLE IF NOT EXISTS PhieuNhap (
    MaPhieuNhap  VARCHAR(10)   NOT NULL PRIMARY KEY,
    NgayNhap     DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MaNhaCungCap VARCHAR(10)   NOT NULL,
    MaNhanVien   VARCHAR(10)   NOT NULL,
    TongTien     DECIMAL(15,2) NOT NULL DEFAULT 0,
    GhiChu       VARCHAR(255)  NULL,
    CONSTRAINT FK_PhieuNhap_NhaCungCap FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap),
    CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE IF NOT EXISTS ChiTietPhieuNhap (
    MaPhieuNhap  VARCHAR(10)   NOT NULL,
    MaSanPham    VARCHAR(10)   NOT NULL,
    SoLuong      INT           NOT NULL,
    DonGiaNhap   DECIMAL(15,2) NOT NULL,
    ThanhTien    DECIMAL(15,2) NOT NULL,
    PRIMARY KEY (MaPhieuNhap, MaSanPham),
    CONSTRAINT FK_CTPN_PhieuNhap FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap(MaPhieuNhap),
    CONSTRAINT FK_CTPN_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
    CONSTRAINT CK_CTPN_SoLuong CHECK (SoLuong > 0)
);

CREATE TABLE IF NOT EXISTS HoaDon (
    MaHoaDon     VARCHAR(10)   NOT NULL PRIMARY KEY,
    NgayLap      DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MaKhachHang  VARCHAR(10)   NULL COMMENT 'NULL = khách lẻ',
    MaNhanVien   VARCHAR(10)   NOT NULL,
    TongTien     DECIMAL(15,2) NOT NULL DEFAULT 0,
    TienKhachDua DECIMAL(15,2) NOT NULL DEFAULT 0,
    GhiChu       VARCHAR(255)  NULL,
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE IF NOT EXISTS ChiTietHoaDon (
    MaHoaDon     VARCHAR(10)   NOT NULL,
    MaSanPham    VARCHAR(10)   NOT NULL,
    SoLuong      INT           NOT NULL,
    DonGiaBan    DECIMAL(15,2) NOT NULL,
    ThanhTien    DECIMAL(15,2) NOT NULL,
    PRIMARY KEY (MaHoaDon, MaSanPham),
    CONSTRAINT FK_CTHD_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    CONSTRAINT FK_CTHD_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
    CONSTRAINT CK_CTHD_SoLuong CHECK (SoLuong > 0)
);
