USE ComputerStore;

INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc, MoTa)
VALUES
('DM01', 'Laptop', 'May tinh xach tay'),
('DM02', 'CPU', 'Bo vi xu ly'),
('DM03', 'RAM', 'Bo nho trong'),
('DM04', 'Ổ cứng', 'Thiet bi luu tru'),
('DM05', 'Màn hình', 'Thiet bi hien thi'),
('DM06', 'Phụ kiện', 'Phu kien may tinh');

USE ComputerStore;

-- 1. NhanVien
INSERT INTO NhanVien
    (HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, NgayVaoLam)
VALUES
    ('Nguyen Van An', 'Nam', '2000-05-10',
     '0912345678', 'an@example.com', 'Ha Noi', '2025-01-01'),

    ('Tran Thi Binh', 'Nu', '2001-08-15',
     '0923456789', 'binh@example.com', 'Ha Noi', '2025-02-01');

-- 2. NhaCungCap
INSERT INTO NhaCungCap
    (TenNhaCungCap, SoDienThoai, Email, DiaChi)
VALUES
    ('Cong ty TNHH ABC', '0934567890',
     'abc@example.com', 'Ha Noi'),

    ('Cong ty XYZ', '0945678901',
     'xyz@example.com', 'Ha Noi');

-- 3. KhachHang
INSERT INTO KhachHang
    (HoTen, SoDienThoai, Email, DiaChi)
VALUES
    ('Le Van Cuong', '0956789012',
     'cuong@example.com', 'Ha Noi'),

    ('Pham Thi Dung', '0967890123',
     'dung@example.com', 'Ha Noi');

-- 4. SanPham
INSERT INTO SanPham
    (TenSanPham, MaDanhMuc, DonViTinh,
     GiaNhap, GiaBan, SoLuongTon, MoTa)
VALUES
    ('Laptop Dell Inspiron 15', 'DM01', 'Cai',
     12000000, 14500000, 10, 'Laptop Dell'),

    ('Intel Core i5 12400F', 'DM02', 'Cai',
     2500000, 3200000, 20, 'CPU Intel'),

    ('Kingston Fury 16GB DDR4', 'DM03', 'Thanh',
     700000, 950000, 30, 'RAM DDR4'),

    ('SSD Samsung 1TB', 'DM04', 'Cai',
     1500000, 1900000, 15, 'SSD 1TB'),

    ('Dell UltraSharp 24 inch', 'DM05', 'Cai',
     3500000, 4200000, 8, 'Man hinh Dell'),

    ('Logitech G102', 'DM06', 'Cai',
     250000, 390000, 25, 'Chuot Logitech');