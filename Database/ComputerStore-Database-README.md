# ComputerStore Database Design

Tài liệu này mô tả thiết kế cơ sở dữ liệu của dự án **Computer Store Management System**, được đồng bộ trực tiếp theo mô hình MySQL Workbench `Database/ComputerStore.mwb` và script `Database/ComputerStore.sql`.

> Phạm vi: 10 bảng cốt lõi theo thiết kế đã thống nhất của dự án. Tài liệu này không thay thế `README.md` ở thư mục gốc của repository.

## 1. Thông tin chung

| Thuộc tính | Giá trị |
| :--- | :--- |
| Database | `ComputerStore` |
| DBMS | MySQL Server 8.x |
| Storage Engine | InnoDB |
| Character Set | `utf8mb4` |
| Collation | `utf8mb4_unicode_ci` |
| Mô hình | Relational Database |
| Số bảng nghiệp vụ | 10 |

## 2. Danh sách bảng và ánh xạ sang DTO / Form

| STT | Bảng (`ComputerStore.mwb`) | Lớp DTO (`ComputerStore.DTO.Entities`) | Form thao tác chính | Mục đích |
| :-: | :--- | :--- | :--- | :--- |
| 1 | `NhanVien` | `NhanVienDTO` | `FrmNhanVien` | Hồ sơ nhân viên |
| 2 | `TaiKhoan` | `TaiKhoanDTO` | `FrmLogin`, `FrmNhanVien` | Tài khoản đăng nhập và vai trò người dùng |
| 3 | `KhachHang` | `KhachHangDTO` | `FrmKhachHang` | Thông tin khách hàng |
| 4 | `DanhMuc` | `DanhMucDTO` | `FrmDanhMuc` | Danh mục sản phẩm |
| 5 | `NhaCungCap` | `NhaCungCapDTO` | `FrmNhaCungCap` | Thông tin nhà cung cấp |
| 6 | `SanPham` | `SanPhamDTO` | `FrmSanPham` | Thông tin sản phẩm và tồn kho |
| 7 | `PhieuNhap` | `PhieuNhapDTO` | `FrmNhapHang` | Thông tin chung của phiếu nhập |
| 8 | `ChiTietPhieuNhap` | `ChiTietPhieuNhapDTO` | `FrmNhapHang` | Các sản phẩm thuộc phiếu nhập |
| 9 | `HoaDon` | `HoaDonDTO` | `FrmBanHang` | Thông tin chung của hóa đơn bán hàng |
| 10 | `ChiTietHoaDon` | `ChiTietHoaDonDTO` | `FrmBanHang` | Các sản phẩm thuộc hóa đơn |

> `FrmMain` là màn hình chính điều hướng sang các Form nghiệp vụ và phân quyền theo `TaiKhoan.VaiTro` (`Admin`, `NhanVien`).

## 3. Chi tiết cột của 10 bảng (đồng bộ theo `ComputerStore.mwb`)

### 3.1. `NhanVien`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaNhanVien` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `HoTen` | `VARCHAR(100)` | NO | |
| `GioiTinh` | `ENUM('Nam','Nu','Khac')` | YES | `DEFAULT 'Khac'` |
| `NgaySinh` | `DATE` | YES | `DEFAULT NULL` |
| `SoDienThoai` | `VARCHAR(15)` | YES | `UNIQUE`, `DEFAULT NULL` |
| `Email` | `VARCHAR(100)` | YES | `UNIQUE`, `DEFAULT NULL` |
| `DiaChi` | `VARCHAR(255)` | YES | `DEFAULT NULL` |
| `NgayVaoLam` | `DATE` | YES | `DEFAULT NULL` |
| `TrangThai` | `TINYINT` | NO | `DEFAULT 1`, `CHECK (TrangThai IN (0, 1))` |
| `NgayTao` | `DATETIME` | NO | `DEFAULT CURRENT_TIMESTAMP` |

### 3.2. `TaiKhoan`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaTaiKhoan` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `MaNhanVien` | `INT` | NO | `UNIQUE`, `FK -> NhanVien(MaNhanVien)` |
| `TenDangNhap` | `VARCHAR(50)` | NO | `UNIQUE` |
| `MatKhau` | `VARCHAR(255)` | NO | Lưu password hash |
| `VaiTro` | `ENUM('Admin','NhanVien')` | NO | `DEFAULT 'NhanVien'` |
| `TrangThai` | `TINYINT` | NO | `DEFAULT 1`, `CHECK (TrangThai IN (0, 1))` |
| `NgayTao` | `DATETIME` | NO | `DEFAULT CURRENT_TIMESTAMP` |

### 3.3. `KhachHang`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaKhachHang` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `HoTen` | `VARCHAR(100)` | NO | |
| `SoDienThoai` | `VARCHAR(15)` | YES | `UNIQUE`, `DEFAULT NULL` |
| `Email` | `VARCHAR(100)` | YES | `DEFAULT NULL` |
| `DiaChi` | `VARCHAR(255)` | YES | `DEFAULT NULL` |
| `NgayTao` | `DATETIME` | NO | `DEFAULT CURRENT_TIMESTAMP` |

### 3.4. `DanhMuc`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaDanhMuc` | `VARCHAR(10)` | NO | `PRIMARY KEY` |
| `TenDanhMuc` | `VARCHAR(100)` | NO | `UNIQUE` |
| `MoTa` | `VARCHAR(255)` | YES | `DEFAULT NULL` |

### 3.5. `NhaCungCap`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaNhaCungCap` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `TenNhaCungCap` | `VARCHAR(150)` | NO | |
| `SoDienThoai` | `VARCHAR(15)` | YES | `DEFAULT NULL` |
| `Email` | `VARCHAR(100)` | YES | `DEFAULT NULL` |
| `DiaChi` | `VARCHAR(255)` | YES | `DEFAULT NULL` |
| `TrangThai` | `TINYINT` | NO | `DEFAULT 1`, `CHECK (TrangThai IN (0, 1))` |

### 3.6. `SanPham`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaSanPham` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `TenSanPham` | `VARCHAR(150)` | NO | `INDEX idx_sanpham_ten` |
| `MaDanhMuc` | `VARCHAR(10)` | NO | `FK -> DanhMuc(MaDanhMuc)`, `INDEX idx_sanpham_danhmuc` |
| `DonViTinh` | `VARCHAR(30)` | NO | `DEFAULT 'Cai'` |
| `GiaNhap` | `DECIMAL(15,2)` | NO | `DEFAULT 0`, `CHECK (GiaNhap >= 0)` |
| `GiaBan` | `DECIMAL(15,2)` | NO | `DEFAULT 0`, `CHECK (GiaBan >= 0)` |
| `SoLuongTon` | `INT` | NO | `DEFAULT 0`, `CHECK (SoLuongTon >= 0)` |
| `MoTa` | `TEXT` | YES | `DEFAULT NULL` |
| `TrangThai` | `TINYINT` | NO | `DEFAULT 1`, `CHECK (TrangThai IN (0, 1))` |
| `NgayTao` | `DATETIME` | NO | `DEFAULT CURRENT_TIMESTAMP` |

### 3.7. `PhieuNhap`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaPhieuNhap` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `MaNhaCungCap` | `INT` | NO | `FK -> NhaCungCap(MaNhaCungCap)` |
| `MaNhanVien` | `INT` | NO | `FK -> NhanVien(MaNhanVien)` |
| `NgayNhap` | `DATETIME` | NO | `DEFAULT CURRENT_TIMESTAMP`, `INDEX idx_phieunhap_ngay` |
| `TongTien` | `DECIMAL(15,2)` | NO | `DEFAULT 0`, `CHECK (TongTien >= 0)` |
| `TrangThai` | `ENUM('ChoXacNhan','DaXacNhan','DaHuy')` | NO | `DEFAULT 'ChoXacNhan'` |
| `GhiChu` | `VARCHAR(255)` | YES | `DEFAULT NULL` |

### 3.8. `ChiTietPhieuNhap`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaChiTietPhieuNhap` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `MaPhieuNhap` | `INT` | NO | `FK -> PhieuNhap(MaPhieuNhap)`, `UNIQUE(MaPhieuNhap, MaSanPham)` |
| `MaSanPham` | `INT` | NO | `FK -> SanPham(MaSanPham)`, `UNIQUE(MaPhieuNhap, MaSanPham)` |
| `SoLuong` | `INT` | NO | `CHECK (SoLuong > 0)` |
| `DonGiaNhap` | `DECIMAL(15,2)` | NO | `CHECK (DonGiaNhap >= 0)` |
| `ThanhTien` | `DECIMAL(15,2)` | YES | `GENERATED ALWAYS AS (SoLuong * DonGiaNhap) STORED` |

### 3.9. `HoaDon`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaHoaDon` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `MaKhachHang` | `INT` | YES | `DEFAULT NULL`, `FK -> KhachHang(MaKhachHang)` |
| `MaNhanVien` | `INT` | NO | `FK -> NhanVien(MaNhanVien)` |
| `NgayLap` | `DATETIME` | NO | `DEFAULT CURRENT_TIMESTAMP`, `INDEX idx_hoadon_ngay` |
| `TongTien` | `DECIMAL(15,2)` | NO | `DEFAULT 0`, `CHECK (TongTien >= 0)` |
| `TrangThai` | `ENUM('ChoThanhToan','DaThanhToan','DaHuy')` | NO | `DEFAULT 'ChoThanhToan'` |
| `GhiChu` | `VARCHAR(255)` | YES | `DEFAULT NULL` |

### 3.10. `ChiTietHoaDon`

| Cột | Kiểu dữ liệu | NULL | Mặc định / Ràng buộc |
| :--- | :--- | :-: | :--- |
| `MaChiTietHoaDon` | `INT` | NO | `PRIMARY KEY`, `AUTO_INCREMENT` |
| `MaHoaDon` | `INT` | NO | `FK -> HoaDon(MaHoaDon)`, `UNIQUE(MaHoaDon, MaSanPham)` |
| `MaSanPham` | `INT` | NO | `FK -> SanPham(MaSanPham)`, `UNIQUE(MaHoaDon, MaSanPham)` |
| `SoLuong` | `INT` | NO | `CHECK (SoLuong > 0)` |
| `DonGiaBan` | `DECIMAL(15,2)` | NO | `CHECK (DonGiaBan >= 0)` |
| `ThanhTien` | `DECIMAL(15,2)` | YES | `GENERATED ALWAYS AS (SoLuong * DonGiaBan) STORED` |

## 4. Quan hệ giữa các bảng

| Bảng cha | Bảng con | Khóa ngoại | Quan hệ |
| :--- | :--- | :--- | :--- |
| `NhanVien` | `TaiKhoan` | `TaiKhoan.MaNhanVien` | Một nhân viên có tối đa một tài khoản (`UNIQUE`) |
| `DanhMuc` | `SanPham` | `SanPham.MaDanhMuc` | Một danh mục có nhiều sản phẩm |
| `NhaCungCap` | `PhieuNhap` | `PhieuNhap.MaNhaCungCap` | Một nhà cung cấp có nhiều phiếu nhập |
| `NhanVien` | `PhieuNhap` | `PhieuNhap.MaNhanVien` | Một nhân viên có thể lập nhiều phiếu nhập |
| `PhieuNhap` | `ChiTietPhieuNhap` | `ChiTietPhieuNhap.MaPhieuNhap` | Một phiếu nhập có nhiều dòng chi tiết |
| `SanPham` | `ChiTietPhieuNhap` | `ChiTietPhieuNhap.MaSanPham` | Một sản phẩm có thể xuất hiện trong nhiều phiếu nhập |
| `KhachHang` | `HoaDon` | `HoaDon.MaKhachHang` | Một khách hàng có thể có nhiều hóa đơn; khách vãng lai cho phép `NULL` |
| `NhanVien` | `HoaDon` | `HoaDon.MaNhanVien` | Một nhân viên có thể lập nhiều hóa đơn |
| `HoaDon` | `ChiTietHoaDon` | `ChiTietHoaDon.MaHoaDon` | Một hóa đơn có nhiều dòng chi tiết |
| `SanPham` | `ChiTietHoaDon` | `ChiTietHoaDon.MaSanPham` | Một sản phẩm có thể xuất hiện trong nhiều hóa đơn |

### Mô hình Header -- Detail

Hai nghiệp vụ nhập hàng và bán hàng đều sử dụng mô hình Header -- Detail:

- `PhieuNhap` lưu thông tin chung; `ChiTietPhieuNhap` lưu từng sản phẩm, số lượng và đơn giá nhập.
- `HoaDon` lưu thông tin chung; `ChiTietHoaDon` lưu từng sản phẩm, số lượng và đơn giá bán.

## 5. Danh mục sản phẩm chuẩn

Bảng `DanhMuc` sử dụng 6 mã danh mục đã thống nhất:

| Mã | Tên |
| :--- | :--- |
| `DM01` | Laptop |
| `DM02` | CPU |
| `DM03` | RAM |
| `DM04` | Ổ cứng |
| `DM05` | Màn hình |
| `DM06` | Phụ kiện |

Không tự ý thay đổi mã hoặc bổ sung danh mục nếu chưa có sự thống nhất của nhóm.

## 6. Quy tắc nghiệp vụ liên quan đến tồn kho

### Nhập hàng

1. Tạo phiếu nhập ở trạng thái `ChoXacNhan`.
2. Thêm các dòng sản phẩm vào `ChiTietPhieuNhap`.
3. Khi phiếu được xác nhận, tăng `SanPham.SoLuongTon`.
4. Cập nhật trạng thái phiếu thành `DaXacNhan`.

Phiếu ở trạng thái `ChoXacNhan` chưa làm tăng tồn kho.

### Bán hàng

1. Tạo hóa đơn ở trạng thái `ChoThanhToan`.
2. Thêm sản phẩm vào `ChiTietHoaDon`.
3. Kiểm tra tồn kho trước khi hoàn tất thanh toán.
4. Khi thanh toán thành công, giảm `SanPham.SoLuongTon`.
5. Cập nhật trạng thái hóa đơn thành `DaThanhToan`.

Không cho phép bán số lượng vượt quá tồn kho.

### Transaction

Việc cập nhật trạng thái chứng từ, dòng chi tiết và số lượng tồn kho phải được thực hiện trong cùng một database transaction:

- Tất cả thao tác thành công: `COMMIT`.
- Có thao tác thất bại: `ROLLBACK`.

## 7. Ràng buộc và lưu ý dữ liệu

- Primary Key định danh duy nhất mỗi bản ghi.
- Foreign Key đảm bảo dữ liệu tham chiếu đến bản ghi tồn tại (`ON UPDATE CASCADE`, `ON DELETE RESTRICT`).
- `UNIQUE` dùng cho `TaiKhoan.MaNhanVien`, `TaiKhoan.TenDangNhap`, `NhanVien.SoDienThoai`, `NhanVien.Email`, `KhachHang.SoDienThoai`, `DanhMuc.TenDanhMuc`, `(MaPhieuNhap, MaSanPham)` và `(MaHoaDon, MaSanPham)`.
- `CHECK` kiểm soát giá trị hợp lệ (giá không âm, tồn kho không âm, số lượng dòng chi tiết `> 0`, trạng thái `0`/`1`).
- `ThanhTien` trong `ChiTietPhieuNhap` và `ChiTietHoaDon` là generated column (`STORED`), không đưa vào câu `INSERT`/`UPDATE`.
- `TaiKhoan.MatKhau` phải lưu password hash, không lưu mật khẩu dạng văn bản thuần.
- `HoaDon.MaKhachHang` cho phép `NULL` để hỗ trợ khách mua trực tiếp không đăng ký thông tin.

## 8. File trong thư mục `Database/`

```text
Database/
├── ComputerStore.sql
├── SampleData.sql
├── ComputerStore.mwb
└── ComputerStore-Database-README.md
```

| File | Mục đích |
| :--- | :--- |
| `ComputerStore.sql` | Script tạo database và 10 bảng chuẩn |
| `SampleData.sql` | Script chèn dữ liệu mẫu kiểm thử |
| `ComputerStore.mwb` | Mô hình MySQL Workbench (ERD) gốc để mở và chỉnh sửa |
| `ComputerStore-Database-README.md` | Thuyết minh thiết kế database và quy tắc nghiệp vụ |

## 9. Cách mở và cập nhật sơ đồ

1. Mở MySQL Workbench.
2. Chọn **File → Open Model**.
3. Mở `Database/ComputerStore.mwb`.
4. Chỉnh sửa EER Diagram khi schema đã được nhóm thống nhất thay đổi.
5. Lưu model bằng `Ctrl + S` và xuất/cập nhật đồng bộ `Database/ComputerStore.sql`, `ComputerStore.DTO/Entities/` cùng tài liệu này.

## 10. Quy ước làm việc nhóm

- Không tự ý đổi tên bảng, cột, kiểu dữ liệu hoặc quan hệ khóa ngoại.
- Mọi thay đổi schema phải được trao đổi và thống nhất trước khi cập nhật.
- Khi có thay đổi được duyệt, cập nhật đồng thời `ComputerStore.mwb`, `ComputerStore.sql`, `ComputerStore-Database-README.md`, `README.md` và các lớp `DTO` tương ứng.
