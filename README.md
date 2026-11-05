# Computer Store Management System

Phần mềm quản lý cửa hàng máy tính được xây dựng bằng **C# Windows
Forms**, **.NET** và **MySQL**. Dự án phục vụ đồ án môn Lập trình .NET,
được phát triển theo nhóm 3 thành viên.

> **Lưu ý:** Trước khi bắt đầu code, cả nhóm cần thống nhất phiên bản
> .NET mục tiêu, phiên bản MySQL, tên namespace và cấu trúc Solution.
> Không tự ý thay đổi các quy ước này khi chưa trao đổi với nhóm.

------------------------------------------------------------------------

## 1. Mục tiêu dự án

Xây dựng ứng dụng desktop hỗ trợ các nghiệp vụ cơ bản của cửa hàng máy
tính:

-   Đăng nhập và quản lý nhân viên.
-   Quản lý danh mục và sản phẩm.
-   Quản lý khách hàng và nhà cung cấp.
-   Lập phiếu nhập hàng, cập nhật tồn kho.
-   Lập hóa đơn bán hàng, trừ tồn kho.
-   Tra cứu và tìm kiếm dữ liệu.

Phạm vi ban đầu được giữ ở mức vừa phải để nhóm có thể hoàn thành, tích
hợp và kiểm thử đúng hạn.

## 2. Công nghệ sử dụng

  Thành phần            Công nghệ
  --------------------- -------------------------------------
  Ngôn ngữ              C#
  Giao diện             Windows Forms (WinForms)
  Nền tảng              .NET (phiên bản do nhóm thống nhất)
  Cơ sở dữ liệu         MySQL Server 8.x
  Công cụ quản trị DB   MySQL Workbench
  Kết nối DB            ADO.NET, NuGet `MySql.Data`
  IDE                   Visual Studio Community
  Quản lý mã nguồn      Git và GitHub

Tất cả thành viên nên dùng cùng phiên bản Visual Studio/.NET phù hợp với
Solution để tránh lỗi tương thích.

## 3. Phạm vi chức năng và phân công

### Thành viên 1 -- Hệ thống

-   `FrmLogin`: đăng nhập.
-   `FrmMain`: màn hình chính và điều hướng đến các chức năng.
-   `FrmNhanVien`: quản lý nhân viên.
-   Phụ trách điều phối repository, quy ước code, tích hợp và kiểm thử
    tổng thể.

### Thành viên 2 -- Quản lý sản phẩm

-   `FrmSanPham`: thêm, sửa, xóa, tìm kiếm sản phẩm.
-   `FrmDanhMuc`: quản lý danh mục.
-   `FrmNhaCungCap`: quản lý nhà cung cấp.

### Thành viên 3 -- Khách hàng và giao dịch

-   `FrmKhachHang`: quản lý khách hàng.
-   `FrmNhapHang`: lập phiếu nhập và tăng tồn kho khi xác nhận nhập.
-   `FrmBanHang`: lập hóa đơn, thanh toán và giảm tồn kho.

Phân công trên là phân công chính. Các thành viên vẫn cần phối hợp khi
chức năng của mình sử dụng dữ liệu do thành viên khác quản lý.

## 4. Danh mục hàng hóa ban đầu

Dự án sử dụng 6 danh mục để dữ liệu demo gọn và dễ kiểm thử:

  Mã danh mục   Tên danh mục
  ------------- --------------
  DM01          Laptop
  DM02          CPU
  DM03          RAM
  DM04          Ổ cứng
  DM05          Màn hình
  DM06          Phụ kiện

Có thể bổ sung danh mục sau khi cả nhóm thống nhất. Mỗi sản phẩm thuộc
một danh mục. Một danh mục có thể có nhiều sản phẩm.

## 5. Cấu trúc Solution và thư mục

Cấu trúc mục tiêu:

``` text
ComputerStore/
├── ComputerStore.sln
│
├── ComputerStore.GUI/
│   ├── Forms/
│   │   ├── FrmLogin.cs
│   │   ├── FrmMain.cs
│   │   ├── FrmNhanVien.cs
│   │   ├── FrmSanPham.cs
│   │   ├── FrmDanhMuc.cs
│   │   ├── FrmNhaCungCap.cs
│   │   ├── FrmKhachHang.cs
│   │   ├── FrmNhapHang.cs
│   │   └── FrmBanHang.cs
│   ├── Helpers/
│   ├── Program.cs
│   └── App.config                 # nếu project dùng .NET Framework
│
├── ComputerStore.BLL/
│   ├── TaiKhoanBLL.cs
│   ├── NhanVienBLL.cs
│   ├── SanPhamBLL.cs
│   ├── DanhMucBLL.cs
│   ├── NhaCungCapBLL.cs
│   ├── KhachHangBLL.cs
│   ├── NhapHangBLL.cs
│   └── BanHangBLL.cs
│
├── ComputerStore.DAL/
│   ├── Database/
│   │   └── DbConnection.cs
│   ├── TaiKhoanDAL.cs
│   ├── NhanVienDAL.cs
│   ├── SanPhamDAL.cs
│   ├── DanhMucDAL.cs
│   ├── NhaCungCapDAL.cs
│   ├── KhachHangDAL.cs
│   ├── NhapHangDAL.cs
│   └── BanHangDAL.cs
│
├── ComputerStore.DTO/
│   ├── TaiKhoanDTO.cs
│   ├── NhanVienDTO.cs
│   ├── SanPhamDTO.cs
│   ├── DanhMucDTO.cs
│   ├── NhaCungCapDTO.cs
│   ├── KhachHangDTO.cs
│   ├── PhieuNhapDTO.cs
│   ├── ChiTietPhieuNhapDTO.cs
│   ├── HoaDonDTO.cs
│   └── ChiTietHoaDonDTO.cs
│
├── Database/
│   ├── ComputerStore.sql
│   └── SampleData.sql
│
├── Documents/
│   ├── ERD.png
│   ├── PhanTichChucNang.docx
│   └── PhanCongCongViec.docx
│
└── README.md
```

Đây là cấu trúc định hướng; không cần tạo tất cả file rỗng ngay lập tức.
Khi tạo project, kiểm tra đúng loại project và phiên bản .NET mà nhóm đã
chốt.

### Ý nghĩa các tầng

-   **GUI (Presentation):** hiển thị Form, nhận thao tác người dùng và
    gọi BLL.
-   **BLL (Business Logic):** kiểm tra dữ liệu và xử lý quy tắc nghiệp
    vụ.
-   **DAL (Data Access):** thực hiện truy vấn MySQL.
-   **DTO (Data Transfer Object):** các lớp đối tượng dùng để truyền dữ
    liệu giữa các tầng.

Quan hệ tham chiếu project đề xuất:

``` text
ComputerStore.GUI  -> ComputerStore.BLL
ComputerStore.GUI  -> ComputerStore.DTO

ComputerStore.BLL  -> ComputerStore.DAL
ComputerStore.BLL  -> ComputerStore.DTO

ComputerStore.DAL  -> ComputerStore.DTO
```

Không để GUI truy vấn SQL trực tiếp. Không đặt mật khẩu database trong
mã nguồn được đẩy lên GitHub.

## 6. Cơ sở dữ liệu dùng chung

Cả nhóm sử dụng **một database MySQL có tên `ComputerStore`** với cùng
một schema đã được thống nhất. Không tự tạo schema khác nhau cho từng
thành viên.

Các bảng dự kiến:

1.  `TaiKhoan`
2.  `NhanVien`
3.  `KhachHang`
4.  `DanhMuc`
5.  `SanPham`
6.  `NhaCungCap`
7.  `PhieuNhap`
8.  `ChiTietPhieuNhap`
9.  `HoaDon`
10. `ChiTietHoaDon`

Quan hệ nghiệp vụ chính:

-   `SanPham` tham chiếu `DanhMuc`.
-   `PhieuNhap` tham chiếu `NhaCungCap` và nhân viên lập phiếu.
-   `ChiTietPhieuNhap` tham chiếu `PhieuNhap` và `SanPham`.
-   `HoaDon` tham chiếu khách hàng và nhân viên bán hàng.
-   `ChiTietHoaDon` tham chiếu `HoaDon` và `SanPham`.
-   `TaiKhoan` liên kết với nhân viên theo thiết kế database được nhóm
    thống nhất.

**Quy tắc quan trọng:** Một người được phân công quản lý file schema
chính `Database/ComputerStore.sql`. Khi cần sửa bảng hoặc cột, phải trao
đổi với cả nhóm, cập nhật file SQL và thông báo trước khi các thành viên
cập nhật code.

### Tạo database

Mở MySQL Workbench và chạy:

``` sql
CREATE DATABASE IF NOT EXISTS ComputerStore
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
```

Sau khi nhóm hoàn thiện schema, import/chạy
`Database/ComputerStore.sql`, sau đó chạy `Database/SampleData.sql` để
tạo dữ liệu mẫu.

Không tự ý chạy script xóa dữ liệu trên database dùng chung. Khi thử
nghiệm thao tác xóa hoặc nhập/bán hàng, nên dùng dữ liệu test đã thống
nhất.

### Kết nối MySQL

Ví dụ connection string minh họa:

``` text
Server=localhost;Port=3306;Database=ComputerStore;Uid=app_user;Pwd=YOUR_PASSWORD;
```

-   Nếu MySQL chạy trên chính máy thành viên: dùng `localhost`.
-   Nếu cả nhóm kết nối đến một máy chủ MySQL chung trong mạng LAN: thay
    `localhost` bằng IP máy chủ.
-   Tài khoản `app_user` và quyền truy cập phải được người quản trị
    database tạo/cấp.
-   Không commit mật khẩu thật vào Git. Dùng file cấu hình cục bộ không
    đưa lên repository, hoặc cơ chế cấu hình an toàn phù hợp với loại
    project.

Không mở cổng MySQL 3306 trực tiếp ra Internet. Nếu làm việc từ xa, dùng
VPN hoặc môi trường máy chủ được bảo vệ.

## 7. Quy trình nghiệp vụ cần thống nhất

### Nhập hàng

1.  Chọn nhà cung cấp.
2.  Tạo phiếu nhập.
3.  Thêm sản phẩm và số lượng.
4.  Tính tổng tiền.
5.  Xác nhận phiếu nhập.
6.  Lưu phiếu và chi tiết phiếu.
7.  Cập nhật số lượng tồn kho.

Chỉ cập nhật tồn kho khi phiếu nhập được xác nhận thành công.

### Bán hàng

1.  Chọn khách hàng (nếu có).
2.  Chọn sản phẩm và số lượng.
3.  Đưa sản phẩm vào giỏ hàng.
4.  Kiểm tra tồn kho và tính tổng tiền.
5.  Xác nhận thanh toán.
6.  Lưu hóa đơn và chi tiết hóa đơn.
7.  Trừ tồn kho.

Không cho phép bán số lượng lớn hơn tồn kho. Nên dùng transaction trong
MySQL để lưu hóa đơn, chi tiết hóa đơn và cập nhật tồn kho như một giao
dịch thống nhất; nếu có lỗi thì rollback.

## 8. Quy ước Git và GitHub

### Nhánh Git

``` text
main
└── develop
    ├── feature/system-management
    ├── feature/product-management
    └── feature/sales-inventory
```

-   `main`: phiên bản ổn định, dùng để bàn giao/demo.
-   `develop`: nhánh tích hợp chức năng.
-   `feature/...`: nhánh làm việc riêng của từng thành viên.

### Lần đầu lấy project

``` bash
git clone <GITHUB_REPOSITORY_URL>
cd ComputerStore
```

### Tạo nhánh cá nhân

Ví dụ thành viên 2:

``` bash
git checkout develop
git pull origin develop
git checkout -b feature/product-management
```

### Lưu và đẩy code

``` bash
git status
git add .
git commit -m "Add product management form"
git push -u origin feature/product-management
```

Những lần tiếp theo:

``` bash
git add .
git commit -m "Update product validation"
git push
```

### Cập nhật code mới từ nhóm

Trước khi bắt đầu làm việc:

``` bash
git checkout develop
git pull origin develop
git checkout feature/product-management
git merge develop
```

Nếu phát sinh conflict, cần đọc và giải quyết từng file, kiểm tra lại
trong Visual Studio rồi mới commit.

### Quy tắc làm việc nhóm

-   Không push trực tiếp lên `main`.
-   Không tự ý merge khi chưa review.
-   Không cùng sửa một Form hoặc một file cấu hình nếu chưa trao đổi.
-   Commit theo từng chức năng nhỏ, thông điệp commit rõ ràng.
-   Không commit thư mục build như `bin/`, `obj/`, thư mục `.vs/`.
-   Không commit mật khẩu, token hoặc connection string chứa thông tin
    bí mật.
-   Mỗi thành viên phải pull code mới trước khi bắt đầu phiên làm việc.

## 9. Quy trình tích hợp Form

1.  Thành viên hoàn thiện Form và các lớp BLL/DAL/DTO liên quan.
2.  Tự build và kiểm thử chức năng trên máy cá nhân.
3.  Commit và push lên feature branch.
4.  Tạo Pull Request vào `develop`.
5.  Người phụ trách tích hợp review thay đổi.
6.  Merge khi code đã build được và không làm hỏng chức năng hiện có.
7.  Cả nhóm lấy bản `develop` mới và kiểm thử tích hợp.

`FrmMain` là Form điều hướng chính. Các Form chức năng được mở từ các
nút/menu trên `FrmMain`. Nên thống nhất cách mở Form (ví dụ hiển thị
trong Panel nội dung) để giao diện toàn ứng dụng đồng nhất.

## 10. Quy ước code

-   Dùng PascalCase cho class, method, property: `SanPhamBLL`,
    `GetAll()`, `MaSanPham`.
-   Tên Form bắt đầu bằng `Frm`: `FrmSanPham`.
-   Tên lớp tầng nghiệp vụ kết thúc bằng `BLL`: `SanPhamBLL`.
-   Tên lớp truy cập dữ liệu kết thúc bằng `DAL`: `SanPhamDAL`.
-   Tên lớp dữ liệu kết thúc bằng `DTO`: `SanPhamDTO`.
-   Tên bảng và tên cột phải theo schema database đã thống nhất.
-   Dùng parameterized query, không nối chuỗi trực tiếp để tạo câu SQL
    từ dữ liệu người dùng.
-   Kiểm tra dữ liệu đầu vào và hiển thị thông báo lỗi dễ hiểu.
-   Đóng/dispose connection, command và reader đúng cách; ưu tiên
    `using`.
-   Không để một Form chứa toàn bộ câu SQL và nghiệp vụ.

## 11. Checklist trước khi hoàn thành

### Môi trường

-   [ ] Cả 3 máy cài Visual Studio và workload .NET Desktop Development.
-   [ ] Cả nhóm thống nhất phiên bản .NET.
-   [ ] MySQL Server và MySQL Workbench hoạt động.
-   [ ] Clone được repository và mở được `ComputerStore.sln`.
-   [ ] Restore NuGet thành công.

### Database

-   [ ] Có file schema `ComputerStore.sql`.
-   [ ] Có dữ liệu mẫu `SampleData.sql`.
-   [ ] Các PK/FK và kiểu dữ liệu đã thống nhất.
-   [ ] Cả nhóm kết nối được database.
-   [ ] Không lưu mật khẩu thật trong Git.

### Chức năng

-   [ ] Đăng nhập đúng/sai.
-   [ ] CRUD nhân viên, sản phẩm, danh mục, nhà cung cấp, khách hàng.
-   [ ] Tìm kiếm dữ liệu.
-   [ ] Nhập hàng cập nhật tồn kho đúng.
-   [ ] Bán hàng trừ tồn kho đúng.
-   [ ] Không cho bán vượt tồn kho.
-   [ ] Dữ liệu hóa đơn và chi tiết hóa đơn nhất quán.

### Tích hợp

-   [ ] Tất cả Form được mở từ `FrmMain`.
-   [ ] Không có lỗi build.
-   [ ] Không có lỗi kết nối database.
-   [ ] Kiểm thử luồng nhập hàng và bán hàng từ đầu đến cuối.
-   [ ] Nhánh `main` chứa phiên bản ổn định.

## 12. Hướng dẫn chạy dự án

1.  Cài Visual Studio Community và workload **.NET desktop
    development**.
2.  Cài MySQL Server và MySQL Workbench.
3.  Clone repository về máy.
4.  Mở `ComputerStore.sln` bằng Visual Studio.
5.  Chạy script tạo database và dữ liệu mẫu theo hướng dẫn ở mục 6.
6.  Cấu hình connection string cục bộ.
7.  Restore NuGet packages.
8.  Chọn `ComputerStore.GUI` làm Startup Project.
9.  Build Solution.
10. Nhấn `F5` để chạy ứng dụng.

Nếu gặp lỗi, kiểm tra lần lượt: MySQL service đã chạy chưa,
IP/port/database/tài khoản có đúng không, NuGet đã restore chưa, và
Startup Project đã chọn đúng chưa.

------------------------------------------------------------------------

## 13. Nguyên tắc phối hợp của nhóm

1.  Thống nhất thiết kế database trước khi code chức năng phụ thuộc vào
    dữ liệu.
2.  Thống nhất tên bảng, tên cột, namespace, tên project và quy ước
    code.
3.  Mỗi thành viên chịu trách nhiệm hoàn thiện cả GUI, BLL, DAL và DTO
    trong phạm vi được giao.
4.  Thành viên phụ trách tích hợp không tự ý thay đổi code của người
    khác khi chưa trao đổi.
5.  Mọi thay đổi ảnh hưởng đến toàn hệ thống phải được thông báo trong
    nhóm.
6.  Thường xuyên tích hợp và kiểm thử, không đợi đến sát ngày nộp mới
    ghép code.

**Mục tiêu cuối cùng:** Cả nhóm cùng phát triển một ứng dụng
`ComputerStore` thống nhất, dùng chung một schema MySQL và có thể clone
repository về máy bất kỳ thành viên nào để build và chạy theo hướng dẫn
trên.
