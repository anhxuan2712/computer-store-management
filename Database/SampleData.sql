USE ComputerStore;

-- =====================================================
-- 1. DANH MUC (6 danh mục chuẩn)
-- =====================================================
INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc, MoTa)
VALUES
    ('DM01', 'Laptop', 'Máy tính xách tay'),
    ('DM02', 'CPU', 'Bộ vi xử lý'),
    ('DM03', 'RAM', 'Bộ nhớ trong'),
    ('DM04', 'Ổ cứng', 'Thiết bị lưu trữ SSD / HDD'),
    ('DM05', 'Màn hình', 'Màn hình máy tính'),
    ('DM06', 'Phụ kiện', 'Bàn phím, chuột, tai nghe và phụ kiện khác')
ON DUPLICATE KEY UPDATE 
    TenDanhMuc = VALUES(TenDanhMuc), 
    MoTa = VALUES(MoTa);

-- =====================================================
-- 2. NHAN VIEN
-- =====================================================
INSERT INTO NhanVien
    (HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, NgayVaoLam, TrangThai)
VALUES
    ('Nguyen Van An', 'Nam', '2000-05-10', '0912345678', 'an@example.com', 'Hà Nội', '2025-01-01', 1),
    ('Tran Thi Binh', 'Nu', '2001-08-15', '0923456789', 'binh@example.com', 'Hà Nội', '2025-02-01', 1)
ON DUPLICATE KEY UPDATE 
    HoTen = VALUES(HoTen),
    Email = VALUES(Email);

-- =====================================================
-- 3. NHA CUNG CAP
-- =====================================================
INSERT INTO NhaCungCap
    (TenNhaCungCap, SoDienThoai, Email, DiaChi, TrangThai)
VALUES
    ('Công ty TNHH Tin học ABC', '0934567890', 'abc@example.com', 'Hà Nội', 1),
    ('Công ty Phân phối Công nghệ XYZ', '0945678901', 'xyz@example.com', 'Hà Nội', 1),
    ('Công ty Thiết bị Số Á Châu', '0933112233', 'achau@example.com', 'TP. Hồ Chí Minh', 1);

-- =====================================================
-- 4. KHACH HANG
-- =====================================================
INSERT INTO KhachHang
    (HoTen, SoDienThoai, Email, DiaChi)
VALUES
    ('Le Van Cuong', '0956789012', 'cuong@example.com', 'Hà Nội'),
    ('Pham Thi Dung', '0967890123', 'dung@example.com', 'Hà Nội'),
    ('Hoang Van Em', '0978901234', 'em@example.com', 'Đà Nẵng')
ON DUPLICATE KEY UPDATE 
    HoTen = VALUES(HoTen);

-- =====================================================
-- 5. SAN PHAM (20 sản phẩm chia đều cho 6 danh mục)
-- =====================================================
INSERT INTO SanPham
    (TenSanPham, MaDanhMuc, DonViTinh, GiaNhap, GiaBan, SoLuongTon, MoTa, TrangThai)
VALUES
    -- DM01: Laptop (4 sản phẩm)
    ('Laptop Dell Inspiron 15 3520', 'DM01', 'Cái', 12000000, 14500000, 15, 'Intel Core i5-1235U, RAM 8GB, SSD 512GB, 15.6 inch FHD', 1),
    ('Laptop Asus Vivobook 14 OLED', 'DM01', 'Cái', 14500000, 17200000, 10, 'Intel Core i5-13500H, RAM 16GB, SSD 512GB, Màn OLED 2.8K', 1),
    ('Laptop Lenovo Legion 5 Gaming', 'DM01', 'Cái', 24000000, 27900000, 8, 'Ryzen 7-7735H, RTX 4060 8GB, RAM 16GB, SSD 512GB', 1),
    ('Laptop HP Pavilion 14', 'DM01', 'Cái', 13200000, 15800000, 12, 'Intel Core i5-1335U, RAM 16GB, SSD 512GB, Mỏng nhẹ màu bạc', 1),

    -- DM02: CPU (3 sản phẩm)
    ('CPU Intel Core i5 12400F', 'DM02', 'Cái', 2500000, 3200000, 25, 'Socket LGA1700, 6 nhân 12 luồng, xung nhịp turbo 4.4GHz', 1),
    ('CPU Intel Core i7 13700K', 'DM02', 'Cái', 8200000, 9600000, 10, 'Socket LGA1700, 16 nhân 24 luồng, xung nhịp turbo 5.4GHz', 1),
    ('CPU AMD Ryzen 5 7600', 'DM02', 'Cái', 4600000, 5400000, 18, 'Socket AM5, 6 nhân 12 luồng, xung nhịp turbo 5.1GHz, kèm tản', 1),

    -- DM03: RAM (3 sản phẩm)
    ('RAM Kingston Fury Beast 8GB DDR4 3200MHz', 'DM03', 'Thanh', 420000, 550000, 40, 'DDR4 3200MHz CL16 tản nhiệt nhôm đen', 1),
    ('RAM Kingston Fury Beast 16GB DDR4 3200MHz', 'DM03', 'Thanh', 750000, 980000, 35, 'DDR4 3200MHz CL16 tản nhiệt nhôm đen', 1),
    ('RAM Corsair Vengeance 32GB (2x16GB) DDR5 5600MHz', 'DM03', 'Bộ', 2300000, 2850000, 20, 'DDR5 5600MHz CL36 tối ưu cho Intel XMP 3.0', 1),

    -- DM04: Ổ cứng (3 sản phẩm)
    ('SSD Samsung 980 500GB PCIe NVMe 3.0', 'DM04', 'Cái', 1100000, 1450000, 30, 'Tốc độ đọc 3100MB/s, ghi 2600MB/s M.2 2280', 1),
    ('SSD Samsung 980 Pro 1TB PCIe NVMe 4.0', 'DM04', 'Cái', 2200000, 2690000, 25, 'Tốc độ đọc 7000MB/s, ghi 5000MB/s hiệu năng cao', 1),
    ('HDD Seagate Barracuda 2TB 3.5 inch', 'DM04', 'Cái', 1250000, 1590000, 15, 'Chuẩn SATA 3 6Gb/s, 7200RPM, 256MB Cache', 1),

    -- DM05: Màn hình (3 sản phẩm)
    ('Màn hình Dell UltraSharp U2422H 24 inch IPS', 'DM05', 'Cái', 5200000, 6190000, 12, '23.8 inch FHD IPS 60Hz 100% sRGB, viền siêu mỏng', 1),
    ('Màn hình ASUS TUF Gaming VG249Q3A 24 inch 180Hz', 'DM05', 'Cái', 2850000, 3450000, 16, '23.8 inch FHD Fast IPS 180Hz 1ms MPRT Gaming', 1),
    ('Màn hình LG 27GP850-B 27 inch 2K Nano IPS 165Hz', 'DM05', 'Cái', 7600000, 8900000, 8, '27 inch QHD Nano IPS 165Hz 1ms G-Sync Compatible', 1),

    -- DM06: Phụ kiện (4 sản phẩm)
    ('Chuột Gaming Logitech G102 Lightsync RGB', 'DM06', 'Cái', 260000, 399000, 50, 'Cảm biến 8000 DPI, LED RGB 16.8 triệu màu, màu đen', 1),
    ('Chuột không dây Logitech MX Master 3S', 'DM06', 'Cái', 1850000, 2290000, 15, 'Cảm biến Darkfield 8000 DPI, cuộn MagSpeed, sạc Type-C', 1),
    ('Bàn phím cơ DareU EK87 Tenkeyless D-Switch', 'DM06', 'Cái', 430000, 599000, 30, 'Bàn phím cơ 87 phím, LED Rainbow, Red Switch', 1),
    ('Tai nghe Gaming Kingston HyperX Cloud II Red', 'DM06', 'Cái', 1450000, 1890000, 20, 'Âm thanh vòm 7.1 Virtual Surround, đệm tai mút hoạt tính', 1);


-- =====================================================
-- 6. TAI KHOAN MAU (Mat khau mac dinh: 123456, luu SHA-256)
-- =====================================================
INSERT INTO TaiKhoan (MaNhanVien, TenDangNhap, MatKhau, VaiTro, TrangThai)
SELECT MaNhanVien, 'admin', SHA2('123456', 256), 'Admin', 1
FROM NhanVien
WHERE SoDienThoai = '0912345678'
ON DUPLICATE KEY UPDATE
    MatKhau = VALUES(MatKhau),
    VaiTro = VALUES(VaiTro),
    TrangThai = VALUES(TrangThai);

INSERT INTO TaiKhoan (MaNhanVien, TenDangNhap, MatKhau, VaiTro, TrangThai)
SELECT MaNhanVien, 'nhanvien', SHA2('123456', 256), 'NhanVien', 1
FROM NhanVien
WHERE SoDienThoai = '0923456789'
ON DUPLICATE KEY UPDATE
    MatKhau = VALUES(MatKhau),
    VaiTro = VALUES(VaiTro),
    TrangThai = VALUES(TrangThai);
