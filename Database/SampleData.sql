-- Dữ liệu mẫu cho ComputerStore (chạy sau ComputerStore.sql)
USE ComputerStore;

INSERT IGNORE INTO NhanVien (MaNhanVien, HoTen, GioiTinh, SoDienThoai, ChucVu) VALUES
('NV001', 'Quản trị viên', 'Nam', '0900000001', 'Quản lý'),
('NV002', 'Nhân viên bán hàng', 'Nữ', '0900000002', 'Bán hàng');

INSERT IGNORE INTO DanhMuc (MaDanhMuc, TenDanhMuc) VALUES
('DM01', 'Laptop'),
('DM02', 'CPU'),
('DM03', 'RAM'),
('DM04', 'Ổ cứng'),
('DM05', 'Màn hình'),
('DM06', 'Phụ kiện');

INSERT IGNORE INTO SanPham (MaSanPham, TenSanPham, MaDanhMuc, ThuongHieu, GiaNhap, GiaBan, SoLuongTon, BaoHanh) VALUES
('SP001', 'Laptop Dell Inspiron 15', 'DM01', 'Dell', 14000000, 16500000, 10, 12),
('SP002', 'CPU Intel Core i5-12400F', 'DM02', 'Intel', 3200000, 3790000, 20, 36),
('SP003', 'RAM Kingston 16GB DDR4 3200', 'DM03', 'Kingston', 900000, 1150000, 30, 36),
('SP004', 'SSD Samsung 980 500GB', 'DM04', 'Samsung', 1100000, 1390000, 25, 60),
('SP005', 'Màn hình LG 24 inch IPS', 'DM05', 'LG', 2500000, 2990000, 8, 24),
('SP006', 'Chuột Logitech G102', 'DM06', 'Logitech', 300000, 420000, 50, 24);

INSERT IGNORE INTO NhaCungCap (MaNhaCungCap, TenNhaCungCap, SoDienThoai, DiaChi) VALUES
('NCC01', 'Công ty TNHH Phân phối Tin học A', '0281111111', 'TP. Hồ Chí Minh'),
('NCC02', 'Công ty CP Thiết bị số B', '0242222222', 'Hà Nội');

INSERT IGNORE INTO KhachHang (MaKhachHang, TenKhachHang, SoDienThoai, DiaChi, Email) VALUES
('KH001', 'Nguyễn Văn An', '0911111111', 'Hà Nội', 'an@example.com'),
('KH002', 'Trần Thị Bình', '0922222222', 'Đà Nẵng', NULL);
