# Computer Store Management System

Phần mềm quản lý cửa hàng máy tính được xây dựng bằng **C# Windows Forms**, **.NET 10** và **MySQL**. Dự án phục vụ đồ án môn Lập trình .NET, được phát triển theo nhóm 3 thành viên.

> **Lưu ý:** Toàn bộ nhóm dùng chung schema đã chốt trong `Database/ComputerStore.mwb` và `Database/ComputerStore.sql`, chạy trên `.NET 10` và mở bằng `ComputerStore.slnx`. Không tự ý thay đổi cấu trúc DB, tên bảng/cột hoặc namespace khi chưa trao đổi với nhóm.

------------------------------------------------------------------------

## 1. Mục tiêu dự án

Xây dựng ứng dụng desktop hỗ trợ các nghiệp vụ cơ bản của cửa hàng máy tính:

- Đăng nhập và quản lý nhân viên (kèm tài khoản & phân quyền).
- Quản lý danh mục và sản phẩm.
- Quản lý khách hàng và nhà cung cấp.
- Lập phiếu nhập hàng, cập nhật tồn kho.
- Lập hóa đơn bán hàng, trừ tồn kho.
- Tra cứu và tìm kiếm dữ liệu.

Phạm vi ban đầu được giữ ở mức vừa phải để nhóm có thể hoàn thành, tích hợp và kiểm thử đúng hạn.

## 2. Công nghệ sử dụng

| Thành phần | Công nghệ |
| :--- | :--- |
| Ngôn ngữ | C# |
| Giao diện | Windows Forms (WinForms) |
| Nền tảng | .NET 10 (`net10.0`, GUI dùng `net10.0-windows`) |
| Cơ sở dữ liệu | MySQL Server 8.x |
| Công cụ quản trị DB | MySQL Workbench (`Database/ComputerStore.mwb`) |
| Kết nối DB | ADO.NET, NuGet `MySql.Data` |
| IDE | Visual Studio Community (hỗ trợ .NET 10 & `.slnx`) |
| Quản lý mã nguồn | Git và GitHub |

Tất cả thành viên nên dùng cùng .NET 10 SDK và phiên bản Visual Studio mở được file `ComputerStore.slnx` để tránh lỗi tương thích.

## 3. Phân công thành viên, phạm vi chức năng và danh sách file

Dự án được chia cho **3 thành viên**, bám sát 10 bảng trong `Database/ComputerStore.mwb`. Mỗi thành viên chịu trách nhiệm xuyên suốt cả 4 tầng (`GUI`, `BLL`, `DAL`, `DTO`) cho phân hệ của mình.

| Thành viên | Họ và tên | Vai trò chính | Nhánh Git |
| :--- | :--- | :--- | :--- |
| **Thành viên 1** | **Nguyễn Anh Xuân** | Trưởng nhóm, Hệ thống, Database & Tích hợp | `feature/system-management` |
| **Thành viên 2** | **Nguyễn Phúc Vượng** | Quản lý Danh mục, Sản phẩm & Nhà cung cấp | `feature/product-management` |
| **Thành viên 3** | **Nguyễn Minh Trí** | Quản lý Khách hàng & Giao dịch Nhập / Bán hàng | `feature/sales-inventory` |

---

### Thành viên 1: Nguyễn Anh Xuân -- Hệ thống, Database & Tích hợp (`feature/system-management`)

**Công việc đảm nhận:**
- Khởi tạo kiến trúc Solution 4 tầng (`GUI`, `BLL`, `DAL`, `DTO`), cấu hình `.NET 10`, file `ComputerStore.slnx` và tầng kết nối MySQL (`DbConnection`, `DatabaseService`).
- Thiết kế, chuẩn hóa và quản lý cơ sở dữ liệu dùng chung (`ComputerStore.mwb`, `ComputerStore.sql`, `SampleData.sql`) và đồng bộ 10 lớp entity trong `ComputerStore.DTO/Entities/`.
- Xây dựng chức năng **Đăng nhập** (`FrmLogin` — đối chiếu SHA-256, kiểm tra `TrangThai = 1`, lấy `VaiTro`), **Màn hình chính** (`FrmMain` — điều hướng các Form nghiệp vụ, phân quyền `Admin`/`NhanVien`, đăng xuất) và **Quản lý Nhân viên & Tài khoản** (`FrmNhanVien` — CRUD, tìm kiếm, cấp/cập nhật/khóa tài khoản).
- Điều phối Git/GitHub, review code, merge các nhánh của thành viên 2 & 3 vào `main`, kiểm thử tích hợp và duy trì tài liệu `README.md`.

| Form | Thư mục / Namespace | Bảng DB thao tác | Nghiệp vụ chính |
| :--- | :--- | :--- | :--- |
| `FrmLogin` | `Forms/Authentication/` (`ComputerStore.GUI.Forms.Authentication`) | `TaiKhoan`, `NhanVien` | Đăng nhập bằng `TenDangNhap`/`MatKhau`, kiểm tra `TrangThai = 1`, lấy `VaiTro` (`Admin`/`NhanVien`) |
| `FrmMain` | `Forms/Main/` (`ComputerStore.GUI.Forms.Main`) | `TaiKhoan` (phiên đăng nhập) | Màn hình chính, điều hướng sang toàn bộ các Form nghiệp vụ, phân quyền theo `VaiTro`, đăng xuất |
| `FrmNhanVien` | `Forms/Employees/` (`ComputerStore.GUI.Forms.Employees`) | `NhanVien`, `TaiKhoan` | CRUD & tìm kiếm nhân viên, kiểm tra trùng SĐT/Email, cấp/sửa/khóa tài khoản liên kết |

**Danh sách file phụ trách:**
- **Solution & Tài liệu:** `ComputerStore.slnx`, `.gitignore`, `README.md`, các file `.csproj` của 4 project.
- **Database:** `Database/ComputerStore.mwb`, `Database/ComputerStore.sql`, `Database/SampleData.sql`, `Database/ComputerStore-Database-README.md`.
- **DTO:** `ComputerStore.DTO/Entities/NhanVienDTO.cs`, `ComputerStore.DTO/Entities/TaiKhoanDTO.cs` (và đồng bộ toàn bộ `ComputerStore.DTO/Entities/`).
- **DAL:** `ComputerStore.DAL/Database/DbConnection.cs`, `ComputerStore.DAL/Repositories/TaiKhoanDAL.cs`, `ComputerStore.DAL/Repositories/NhanVienDAL.cs`.
- **BLL:** `ComputerStore.BLL/Services/DatabaseService.cs`, `ComputerStore.BLL/Services/TaiKhoanBLL.cs`, `ComputerStore.BLL/Services/NhanVienBLL.cs`.
- **GUI:**
  - `ComputerStore.GUI/Program.cs`
  - `ComputerStore.GUI/Forms/Authentication/FrmLogin.cs`, `FrmLogin.Designer.cs`, `FrmLogin.resx`
  - `ComputerStore.GUI/Forms/Main/FrmMain.cs`, `FrmMain.Designer.cs`, `FrmMain.resx`
  - `ComputerStore.GUI/Forms/Employees/FrmNhanVien.cs`, `FrmNhanVien.Designer.cs`, `FrmNhanVien.resx`

---

### Thành viên 2: Nguyễn Phúc Vượng -- Quản lý sản phẩm (`feature/product-management`)

**Công việc đảm nhận:**
- Xây dựng trọn bộ CRUD, tìm kiếm và kiểm tra ràng buộc khóa ngoại trước khi xóa cho 3 phân hệ: **Danh mục**, **Sản phẩm** và **Nhà cung cấp**.
- Xây dựng helper dựng control giao diện dùng chung cho các màn hình quản lý (`ManagementFormControls.cs`).

| Form | Thư mục / Namespace | Bảng DB thao tác | Nghiệp vụ chính |
| :--- | :--- | :--- | :--- |
| `FrmSanPham` | `Forms/Products/` (`ComputerStore.GUI.Forms.Products`) | `SanPham`, `DanhMuc` | CRUD & tìm kiếm sản phẩm (`TenSanPham`, `MaDanhMuc`, `DonViTinh`, `GiaNhap`, `GiaBan`, `SoLuongTon`, `MoTa`, `TrangThai`), chặn xóa nếu đã có giao dịch |
| `FrmDanhMuc` | `Forms/Products/` (`ComputerStore.GUI.Forms.Products`) | `DanhMuc` | Quản lý danh mục (`MaDanhMuc`, `TenDanhMuc`, `MoTa`), chặn xóa nếu đã có sản phẩm |
| `FrmNhaCungCap` | `Forms/Suppliers/` (`ComputerStore.GUI.Forms.Suppliers`) | `NhaCungCap` | Quản lý nhà cung cấp (`TenNhaCungCap`, `SoDienThoai`, `Email`, `DiaChi`, `TrangThai`), chặn xóa nếu đã có phiếu nhập |

**Danh sách file phụ trách:**
- **DTO:** `ComputerStore.DTO/Entities/DanhMucDTO.cs`, `ComputerStore.DTO/Entities/SanPhamDTO.cs`, `ComputerStore.DTO/Entities/NhaCungCapDTO.cs`.
- **DAL:** `ComputerStore.DAL/Repositories/DanhMucDAL.cs`, `ComputerStore.DAL/Repositories/SanPhamDAL.cs`, `ComputerStore.DAL/Repositories/NhaCungCapDAL.cs`.
- **BLL:** `ComputerStore.BLL/Services/DanhMucBLL.cs`, `ComputerStore.BLL/Services/SanPhamBLL.cs`, `ComputerStore.BLL/Services/NhaCungCapBLL.cs`.
- **GUI:**
  - `ComputerStore.GUI/Forms/Products/FrmDanhMuc.cs`, `FrmDanhMuc.resx`
  - `ComputerStore.GUI/Forms/Products/FrmSanPham.cs`, `FrmSanPham.resx`
  - `ComputerStore.GUI/Forms/Suppliers/FrmNhaCungCap.cs`, `FrmNhaCungCap.resx`
  - `ComputerStore.GUI/Helpers/ManagementFormControls.cs`

---

### Thành viên 3: Nguyễn Minh Trí -- Khách hàng và giao dịch (`feature/sales-inventory`)

**Công việc đảm nhận:**
- Xây dựng chức năng **Quản lý Khách hàng** (CRUD & tìm kiếm).
- Xây dựng nghiệp vụ **Nhập hàng** và **Bán hàng** sử dụng MySQL transaction để đảm bảo lưu phiếu/hóa đơn, chi tiết và cập nhật tồn kho (`SanPham.SoLuongTon`) nhất quán, không cho bán vượt tồn kho.
- Xây dựng model hiển thị dòng chi tiết giao dịch (`DongChiTietModel.cs`).

| Form | Thư mục / Namespace | Bảng DB thao tác | Nghiệp vụ chính |
| :--- | :--- | :--- | :--- |
| `FrmKhachHang` | `Forms/Customers/` (`ComputerStore.GUI.Forms.Customers`) | `KhachHang` | CRUD & tìm kiếm khách hàng (`HoTen`, `SoDienThoai`, `Email`, `DiaChi`) |
| `FrmNhapHang` | `Forms/Transactions/` (`ComputerStore.GUI.Forms.Transactions`) | `PhieuNhap`, `ChiTietPhieuNhap`, `SanPham`, `NhaCungCap`, `NhanVien` | Lập phiếu nhập, xác nhận (`ChoXacNhan` → `DaXacNhan`) và tăng `SanPham.SoLuongTon` trong transaction |
| `FrmBanHang` | `Forms/Transactions/` (`ComputerStore.GUI.Forms.Transactions`) | `HoaDon`, `ChiTietHoaDon`, `SanPham`, `KhachHang`, `NhanVien` | Lập hóa đơn (hỗ trợ `MaKhachHang NULL` cho khách lẻ), thanh toán (`ChoThanhToan` → `DaThanhToan`) và trừ `SanPham.SoLuongTon` trong transaction |

**Danh sách file phụ trách:**
- **DTO:** `ComputerStore.DTO/Entities/KhachHangDTO.cs`, `ComputerStore.DTO/Entities/PhieuNhapDTO.cs`, `ComputerStore.DTO/Entities/ChiTietPhieuNhap.cs`, `ComputerStore.DTO/Entities/HoaDonDTO.cs`, `ComputerStore.DTO/Entities/ChiTietHoaDon.cs`, `ComputerStore.DTO/Models/DongChiTietModel.cs`.
- **DAL:** `ComputerStore.DAL/Repositories/KhachHangDAL.cs`, `ComputerStore.DAL/Repositories/NhapHangDAL.cs`, `ComputerStore.DAL/Repositories/BanHangDAL.cs`.
- **BLL:** `ComputerStore.BLL/Services/KhachHangBLL.cs`, `ComputerStore.BLL/Services/NhapHangBLL.cs`, `ComputerStore.BLL/Services/BanHangBLL.cs`.
- **GUI:**
  - `ComputerStore.GUI/Forms/Customers/FrmKhachHang.cs`, `FrmKhachHang.Designer.cs`, `FrmKhachHang.resx`
  - `ComputerStore.GUI/Forms/Transactions/FrmNhapHang.cs`, `FrmNhapHang.Designer.cs`, `FrmNhapHang.resx`
  - `ComputerStore.GUI/Forms/Transactions/FrmBanHang.cs`, `FrmBanHang.Designer.cs`, `FrmBanHang.resx`

Phân công trên là phân công chính. Các thành viên vẫn phối hợp khi chức năng của mình sử dụng dữ liệu do thành viên khác quản lý.

## 4. Danh mục hàng hóa ban đầu

Dự án sử dụng 6 danh mục để dữ liệu demo gọn và dễ kiểm thử:

| Mã danh mục | Tên danh mục |
| :--- | :--- |
| `DM01` | Laptop |
| `DM02` | CPU |
| `DM03` | RAM |
| `DM04` | Ổ cứng |
| `DM05` | Màn hình |
| `DM06` | Phụ kiện |

Có thể bổ sung danh mục sau khi cả nhóm thống nhất. Mỗi sản phẩm thuộc một danh mục; một danh mục có thể có nhiều sản phẩm.

## 5. Cấu trúc Solution và thư mục

Cấu trúc thực tế đã thống nhất (các file Form/BLL/DAL được bổ sung dần khi từng thành viên hoàn thành chức năng):

```text
computer-store-management/
├── ComputerStore.slnx
│
├── ComputerStore.GUI/
│   ├── Forms/
│   │   ├── Authentication/
│   │   │   ├── FrmLogin.cs
│   │   │   ├── FrmLogin.Designer.cs
│   │   │   └── FrmLogin.resx
│   │   ├── Main/
│   │   │   ├── FrmMain.cs
│   │   │   ├── FrmMain.Designer.cs
│   │   │   └── FrmMain.resx
│   │   ├── Employees/
│   │   │   ├── FrmNhanVien.cs
│   │   │   ├── FrmNhanVien.Designer.cs
│   │   │   └── FrmNhanVien.resx
│   │   ├── Products/
│   │   │   ├── FrmSanPham.cs
│   │   │   ├── FrmSanPham.resx
│   │   │   ├── FrmDanhMuc.cs
│   │   │   └── FrmDanhMuc.resx
│   │   ├── Suppliers/
│   │   │   ├── FrmNhaCungCap.cs
│   │   │   └── FrmNhaCungCap.resx
│   │   ├── Customers/
│   │   │   ├── FrmKhachHang.cs
│   │   │   ├── FrmKhachHang.Designer.cs
│   │   │   └── FrmKhachHang.resx
│   │   └── Transactions/
│   │       ├── FrmNhapHang.cs
│   │       ├── FrmNhapHang.Designer.cs
│   │       ├── FrmNhapHang.resx
│   │       ├── FrmBanHang.cs
│   │       ├── FrmBanHang.Designer.cs
│   │       └── FrmBanHang.resx
│   ├── Helpers/
│   │   └── ManagementFormControls.cs
│   ├── Resources/
│   └── Program.cs
│
├── ComputerStore.BLL/
│   └── Services/
│       ├── DatabaseService.cs       # kiểm tra kết nối DB khi khởi động
│       ├── TaiKhoanBLL.cs
│       ├── NhanVienBLL.cs
│       ├── SanPhamBLL.cs
│       ├── DanhMucBLL.cs
│       ├── NhaCungCapBLL.cs
│       ├── KhachHangBLL.cs
│       ├── NhapHangBLL.cs
│       └── BanHangBLL.cs
│
├── ComputerStore.DAL/
│   ├── Database/
│   │   └── DbConnection.cs
│   └── Repositories/
│       ├── TaiKhoanDAL.cs
│       ├── NhanVienDAL.cs
│       ├── SanPhamDAL.cs
│       ├── DanhMucDAL.cs
│       ├── NhaCungCapDAL.cs
│       ├── KhachHangDAL.cs
│       ├── NhapHangDAL.cs
│       └── BanHangDAL.cs
│
├── ComputerStore.DTO/
│   ├── Entities/                    # mỗi lớp tương ứng đúng 1 bảng trong ComputerStore.mwb
│   │   ├── TaiKhoanDTO.cs
│   │   ├── NhanVienDTO.cs
│   │   ├── SanPhamDTO.cs
│   │   ├── DanhMucDTO.cs
│   │   ├── NhaCungCapDTO.cs
│   │   ├── KhachHangDTO.cs
│   │   ├── PhieuNhapDTO.cs
│   │   ├── ChiTietPhieuNhap.cs      # lớp ChiTietPhieuNhapDTO
│   │   ├── HoaDonDTO.cs
│   │   └── ChiTietHoaDon.cs         # lớp ChiTietHoaDonDTO
│   └── Models/                      # lớp phục vụ hiển thị trên Form, không ứng với bảng DB
│       └── DongChiTietModel.cs
│
├── Database/
│   ├── ComputerStore.sql            # schema chính (đồng bộ với ComputerStore.mwb)
│   ├── SampleData.sql               # dữ liệu mẫu (6 danh mục, 20 sản phẩm, 2 tài khoản mẫu)
│   ├── ComputerStore.mwb            # mô hình MySQL Workbench (ERD) gốc
│   └── ComputerStore-Database-README.md
│
└── README.md
```

Mỗi Form gồm 3 file nằm cùng thư mục:

| File | Nội dung |
| :--- | :--- |
| `FrmXxx.cs` | Code xử lý sự kiện, gọi BLL |
| `FrmXxx.Designer.cs` | Code giao diện do Visual Studio sinh khi kéo thả control |
| `FrmXxx.resx` | Tài nguyên của Form (ảnh, icon...) |

Quy ước Namespace theo thư mục:

| Thư mục | Namespace |
| :--- | :--- |
| `GUI/Forms/<Module>/` | `ComputerStore.GUI.Forms.<Module>` |
| `BLL/Services/` | `ComputerStore.BLL.Services` |
| `DAL/Repositories/` | `ComputerStore.DAL.Repositories` |
| `DAL/Database/` | `ComputerStore.DAL.Database` |
| `DTO/Entities/` | `ComputerStore.DTO.Entities` |
| `DTO/Models/` | `ComputerStore.DTO.Models` |

Project dùng `.NET 10` (SDK-style) nên không sử dụng `App.config`.

### Ý nghĩa các tầng

- **GUI (Presentation):** hiển thị Form, nhận thao tác người dùng và gọi BLL.
- **BLL (Business Logic):** kiểm tra dữ liệu và xử lý quy tắc nghiệp vụ.
- **DAL (Data Access):** thực hiện truy vấn MySQL.
- **DTO (Data Transfer Object):** các lớp đối tượng truyền dữ liệu giữa các tầng, khớp 1-1 với các cột trong `Database/ComputerStore.mwb`.

Quan hệ tham chiếu project:

```text
ComputerStore.GUI  -> ComputerStore.BLL
ComputerStore.GUI  -> ComputerStore.DTO

ComputerStore.BLL  -> ComputerStore.DAL
ComputerStore.BLL  -> ComputerStore.DTO

ComputerStore.DAL  -> ComputerStore.DTO
```

Không để GUI truy vấn SQL trực tiếp. Không đặt mật khẩu database trong mã nguồn được đẩy lên GitHub.

## 6. Cơ sở dữ liệu dùng chung

Cả nhóm sử dụng **một database MySQL có tên `ComputerStore`** theo mô hình gốc `Database/ComputerStore.mwb` và script `Database/ComputerStore.sql`. Chi tiết đầy đủ từng cột và ràng buộc xem tại `Database/ComputerStore-Database-README.md`.

### Tóm tắt 10 bảng (đồng bộ theo `ComputerStore.mwb`)

| STT | Bảng | Khóa chính | Khóa ngoại | Cột chính & ràng buộc quan trọng |
| :-: | :--- | :--- | :--- | :--- |
| 1 | `NhanVien` | `MaNhanVien` (AI) | — | `HoTen`, `GioiTinh` (`ENUM('Nam','Nu','Khac')`), `NgaySinh?`, `SoDienThoai?` (UQ), `Email?` (UQ), `DiaChi?`, `NgayVaoLam?`, `TrangThai` (0/1), `NgayTao` |
| 2 | `TaiKhoan` | `MaTaiKhoan` (AI) | `MaNhanVien` (UQ → `NhanVien`) | `TenDangNhap` (UQ), `MatKhau` (hash), `VaiTro` (`ENUM('Admin','NhanVien')`), `TrangThai` (0/1), `NgayTao` |
| 3 | `KhachHang` | `MaKhachHang` (AI) | — | `HoTen`, `SoDienThoai?` (UQ), `Email?`, `DiaChi?`, `NgayTao` |
| 4 | `DanhMuc` | `MaDanhMuc` (`VARCHAR(10)`) | — | `TenDanhMuc` (UQ), `MoTa?` |
| 5 | `NhaCungCap` | `MaNhaCungCap` (AI) | — | `TenNhaCungCap`, `SoDienThoai?`, `Email?`, `DiaChi?`, `TrangThai` (0/1) |
| 6 | `SanPham` | `MaSanPham` (AI) | `MaDanhMuc` (→ `DanhMuc`) | `TenSanPham` (IDX), `DonViTinh` (`'Cai'`), `GiaNhap >= 0`, `GiaBan >= 0`, `SoLuongTon >= 0`, `MoTa?`, `TrangThai` (0/1), `NgayTao` |
| 7 | `PhieuNhap` | `MaPhieuNhap` (AI) | `MaNhaCungCap`, `MaNhanVien` | `NgayNhap` (IDX), `TongTien >= 0`, `TrangThai` (`ENUM('ChoXacNhan','DaXacNhan','DaHuy')`), `GhiChu?` |
| 8 | `ChiTietPhieuNhap` | `MaChiTietPhieuNhap` (AI) | `MaPhieuNhap`, `MaSanPham` | `SoLuong > 0`, `DonGiaNhap >= 0`, `ThanhTien` (generated stored), `UQ(MaPhieuNhap, MaSanPham)` |
| 9 | `HoaDon` | `MaHoaDon` (AI) | `MaKhachHang?` (NULL = khách lẻ), `MaNhanVien` | `NgayLap` (IDX), `TongTien >= 0`, `TrangThai` (`ENUM('ChoThanhToan','DaThanhToan','DaHuy')`), `GhiChu?` |
| 10 | `ChiTietHoaDon` | `MaChiTietHoaDon` (AI) | `MaHoaDon`, `MaSanPham` | `SoLuong > 0`, `DonGiaBan >= 0`, `ThanhTien` (generated stored), `UQ(MaHoaDon, MaSanPham)` |

> Ký hiệu `?` biểu thị cột cho phép `NULL` trong MySQL (`ComputerStore.mwb`), `AI` là `INT AUTO_INCREMENT`, `UQ` là `UNIQUE`, `IDX` là `INDEX`. Tất cả khóa ngoại đều dùng `ON UPDATE CASCADE` và `ON DELETE RESTRICT`.

**Quy tắc quan trọng:** Một người phụ trách quản lý `Database/ComputerStore.mwb` và `Database/ComputerStore.sql`. Khi cần sửa bảng hoặc cột, phải trao đổi với cả nhóm, cập nhật đồng thời `.mwb`, `.sql`, tài liệu và các lớp `DTO` rồi thông báo trước khi các thành viên cập nhật code.

### Tạo database

Mở MySQL Workbench và chạy lần lượt:

1. `Database/ComputerStore.sql`: tạo database `ComputerStore` (`utf8mb4` / `utf8mb4_unicode_ci`) và 10 bảng.
2. `Database/SampleData.sql`: chèn dữ liệu mẫu.

Nếu máy đã có database `ComputerStore` theo schema cũ, cần xóa database cũ trên máy cục bộ rồi chạy lại 2 file trên vì script dùng `CREATE TABLE IF NOT EXISTS`.

### Kết nối MySQL

Ứng dụng đọc thông tin đăng nhập MySQL từ **biến môi trường** trong `ComputerStore.DAL/Database/DbConnection.cs`:

| Biến môi trường | Ý nghĩa |
| :--- | :--- |
| `COMPUTERSTORE_DB_USER` | Tên tài khoản MySQL |
| `COMPUTERSTORE_DB_PASSWORD` | Mật khẩu tài khoản MySQL |

Thiết lập một lần bằng PowerShell:

```powershell
setx COMPUTERSTORE_DB_USER "app_user"
setx COMPUTERSTORE_DB_PASSWORD "YOUR_PASSWORD"
```

Sau đó **đóng và mở lại Visual Studio** để nhận biến môi trường mới.

- `Server` (`localhost`), `Port` (`3306`) và `Database` (`ComputerStore`) hiện cấu hình trong `DbConnection.cs`. Nếu kết nối MySQL chung trong mạng LAN, trao đổi với nhóm trước khi thay đổi.
- Không commit mật khẩu thật vào Git. Không mở cổng MySQL `3306` trực tiếp ra Internet.

## 7. Quy trình nghiệp vụ cần thống nhất

### Nhập hàng

1. Chọn nhà cung cấp (`NhaCungCap`).
2. Tạo phiếu nhập (`PhieuNhap`, trạng thái mặc định `ChoXacNhan`).
3. Thêm sản phẩm, số lượng và đơn giá nhập (`ChiTietPhieuNhap`).
4. Tính tổng tiền (`TongTien`).
5. Xác nhận phiếu nhập (`ChoXacNhan` → `DaXacNhan`).
6. Lưu phiếu nhập, chi tiết phiếu nhập và cộng `SanPham.SoLuongTon` trong cùng một MySQL transaction (lỗi thì `ROLLBACK`).

Chỉ cập nhật tồn kho khi phiếu nhập được xác nhận thành công.

### Bán hàng

1. Chọn khách hàng (`KhachHang`, có thể để trống/`NULL` cho khách vãng lai).
2. Chọn sản phẩm và số lượng.
3. Đưa sản phẩm vào giỏ hàng (`ChiTietHoaDon`).
4. Kiểm tra tồn kho và tính tổng tiền (`TongTien`).
5. Xác nhận thanh toán (`ChoThanhToan` → `DaThanhToan`).
6. Lưu hóa đơn, chi tiết hóa đơn và trừ `SanPham.SoLuongTon` trong cùng một MySQL transaction (lỗi thì `ROLLBACK`).

Không cho phép bán số lượng lớn hơn tồn kho.

## 8. Quy ước Git và GitHub

### Nhánh Git

```text
main
└── develop
    ├── feature/system-management
    ├── feature/product-management
    └── feature/sales-inventory
```

- `main`: phiên bản ổn định, dùng để bàn giao/demo.
- `develop`: nhánh tích hợp chức năng.
- `feature/...`: nhánh làm việc riêng của từng thành viên.

### Lần đầu lấy project

```bash
git clone <GITHUB_REPOSITORY_URL>
cd computer-store-management
```

### Tạo nhánh cá nhân

Ví dụ thành viên 2:

```bash
git checkout develop
git pull origin develop
git checkout -b feature/product-management
```

### Lưu và đẩy code

```bash
git status
git add .
git commit -m "Add product management form"
git push -u origin feature/product-management
```

### Cập nhật code mới từ nhóm

Trước khi bắt đầu làm việc:

```bash
git checkout develop
git pull origin develop
git checkout feature/product-management
git merge develop
```

Nếu phát sinh conflict, đọc và giải quyết từng file, kiểm tra build trong Visual Studio rồi mới commit.

### Quy tắc làm việc nhóm

- Không push trực tiếp lên `main`.
- Không tự ý merge khi chưa review.
- Không cùng sửa một Form hoặc một file cấu hình nếu chưa trao đổi.
- Commit theo từng chức năng nhỏ, thông điệp commit rõ ràng.
- Không commit thư mục build (`bin/`, `obj/`, `.vs/`) hoặc bài tập không liên quan.
- Không commit mật khẩu, token hoặc connection string chứa thông tin bí mật.

## 9. Quy trình tích hợp Form

1. Thành viên hoàn thiện Form và các lớp BLL/DAL/DTO liên quan.
2. Tự build và kiểm thử chức năng trên máy cá nhân.
3. Commit và push lên feature branch.
4. Tạo Pull Request vào `develop`.
5. Người phụ trách tích hợp review thay đổi.
6. Merge khi code đã build được và không làm hỏng chức năng hiện có.
7. Cả nhóm lấy bản `develop` mới và kiểm thử tích hợp.

`FrmMain` là Form điều hướng chính. Các Form chức năng được mở từ các nút/menu trên `FrmMain` (ví dụ hiển thị trong Panel nội dung) để giao diện toàn ứng dụng đồng nhất.

## 10. Quy ước code

- Dùng PascalCase cho class, method, property: `SanPhamBLL`, `GetAll()`, `MaSanPham`.
- Tên Form bắt đầu bằng `Frm`: `FrmSanPham`.
- Tên lớp tầng nghiệp vụ kết thúc bằng `BLL`: `SanPhamBLL`.
- Tên lớp truy cập dữ liệu kết thúc bằng `DAL`: `SanPhamDAL`.
- Tên lớp dữ liệu kết thúc bằng `DTO`: `SanPhamDTO`.
- Tên bảng và tên cột phải khớp đúng `Database/ComputerStore.mwb` / `Database/ComputerStore.sql`.
- Dùng parameterized query, không nối chuỗi trực tiếp để tạo câu SQL từ dữ liệu người dùng.
- Kiểm tra dữ liệu đầu vào và hiển thị thông báo lỗi dễ hiểu.
- Đóng/dispose connection, command và reader đúng cách; ưu tiên `using`.
- Không để một Form chứa toàn bộ câu SQL và nghiệp vụ.

## 11. Checklist trước khi hoàn thành

### Môi trường

- [ ] Cả 3 máy cài Visual Studio và workload .NET Desktop Development.
- [ ] Cả 3 máy cài .NET 10 SDK.
- [ ] MySQL Server và MySQL Workbench hoạt động.
- [ ] Clone được repository và mở được `ComputerStore.slnx`.
- [ ] Thiết lập biến môi trường `COMPUTERSTORE_DB_USER` và `COMPUTERSTORE_DB_PASSWORD`.
- [ ] Restore NuGet thành công.

### Database

- [ ] Có mô hình `Database/ComputerStore.mwb` và schema `Database/ComputerStore.sql` đồng bộ.
- [ ] Có dữ liệu mẫu `Database/SampleData.sql` (kèm tài khoản đăng nhập mẫu).
- [ ] Các PK/FK, kiểu dữ liệu, `DTO` và `Form` khớp schema đã thống nhất.
- [ ] Cả nhóm kết nối được database.
- [ ] Không lưu mật khẩu thật trong Git.

### Chức năng

- [ ] Đăng nhập đúng/sai và phân quyền `Admin`/`NhanVien`.
- [ ] CRUD nhân viên, sản phẩm, danh mục, nhà cung cấp, khách hàng.
- [ ] Tìm kiếm dữ liệu.
- [ ] Nhập hàng cập nhật tồn kho đúng.
- [ ] Bán hàng trừ tồn kho đúng.
- [ ] Không cho bán vượt tồn kho.
- [ ] Dữ liệu hóa đơn và chi tiết hóa đơn nhất quán.

### Tích hợp

- [ ] Tất cả Form được mở từ `FrmMain`.
- [ ] Không có lỗi build.
- [ ] Không có lỗi kết nối database.
- [ ] Kiểm thử luồng nhập hàng và bán hàng từ đầu đến cuối.
- [ ] Nhánh `main` chứa phiên bản ổn định.

## 12. Hướng dẫn chạy dự án

1. Cài Visual Studio Community (bản hỗ trợ `.NET 10`) và workload **.NET desktop development**.
2. Cài MySQL Server và MySQL Workbench.
3. Clone repository về máy.
4. Mở `ComputerStore.slnx` bằng Visual Studio.
5. Chạy `Database/ComputerStore.sql` và `Database/SampleData.sql` trong MySQL Workbench.
6. Đặt biến môi trường `COMPUTERSTORE_DB_USER` và `COMPUTERSTORE_DB_PASSWORD` theo mục 6, rồi mở lại Visual Studio.
7. Restore NuGet packages.
8. Chọn `ComputerStore.GUI` làm Startup Project.
9. Build Solution.
10. Nhấn `F5` để chạy ứng dụng.

### Tài khoản đăng nhập mẫu (trong `Database/SampleData.sql`)

| Tên đăng nhập | Mật khẩu | Vai trò |
| :--- | :--- | :--- |
| `admin` | `123456` | `Admin` (toàn quyền) |
| `nhanvien` | `123456` | `NhanVien` (ẩn quản lý nhân viên) |

Nếu gặp lỗi, kiểm tra lần lượt: MySQL service đã chạy chưa, đã đặt 2 biến môi trường và khởi động lại Visual Studio chưa, tài khoản/database có đúng không, NuGet đã restore chưa, và Startup Project đã chọn đúng `ComputerStore.GUI` chưa.

------------------------------------------------------------------------

## 13. Nguyên tắc phối hợp của nhóm

1. Thống nhất thiết kế database (`ComputerStore.mwb`) trước khi code chức năng phụ thuộc vào dữ liệu.
2. Thống nhất tên bảng, tên cột, namespace, tên project và quy ước code.
3. Mỗi thành viên chịu trách nhiệm hoàn thiện cả GUI, BLL, DAL và DTO trong phạm vi được giao.
4. Thành viên phụ trách tích hợp không tự ý thay đổi code của người khác khi chưa trao đổi.
5. Mọi thay đổi ảnh hưởng đến toàn hệ thống phải được thông báo trong nhóm.
6. Thường xuyên tích hợp và kiểm thử, không đợi đến sát ngày nộp mới ghép code.

**Mục tiêu cuối cùng:** Cả nhóm cùng phát triển một ứng dụng `ComputerStore` thống nhất, dùng chung một schema MySQL (`ComputerStore.mwb`) và có thể clone repository về máy bất kỳ thành viên nào để build và chạy theo hướng dẫn trên.
