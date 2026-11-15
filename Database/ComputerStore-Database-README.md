# ComputerStore Database Design

Tài liệu này mô tả thiết kế cơ sở dữ liệu của dự án **Computer Store
Management System**. Đây là tài liệu đi kèm với mô hình MySQL Workbench
`ComputerStore.mwb` và sơ đồ ERD được xuất từ mô hình đó.

> Phạm vi: 10 bảng cốt lõi theo thiết kế đã thống nhất của dự án. Tài
> liệu này không thay thế `README.md` ở thư mục gốc của repository.

## 1. Thông tin chung

  Thuộc tính          Giá trị
  ------------------- ----------------------
  Database            `ComputerStore`
  DBMS                MySQL Server 8.x
  Storage Engine      InnoDB
  Character Set       `utf8mb4`
  Collation           `utf8mb4_unicode_ci`
  Mô hình             Relational Database
  Số bảng nghiệp vụ   10

## 2. Danh sách bảng

    STT Bảng                 Mục đích
  ----- -------------------- -------------------------------------------
      1 `TaiKhoan`           Tài khoản đăng nhập và vai trò người dùng
      2 `NhanVien`           Hồ sơ nhân viên
      3 `KhachHang`          Thông tin khách hàng
      4 `DanhMuc`            Danh mục sản phẩm
      5 `SanPham`            Thông tin sản phẩm và tồn kho
      6 `NhaCungCap`         Thông tin nhà cung cấp
      7 `PhieuNhap`          Thông tin chung của phiếu nhập
      8 `ChiTietPhieuNhap`   Các sản phẩm thuộc phiếu nhập
      9 `HoaDon`             Thông tin chung của hóa đơn bán hàng
     10 `ChiTietHoaDon`      Các sản phẩm thuộc hóa đơn

## 3. Quan hệ giữa các bảng

  -----------------------------------------------------------------------------------------
  Bảng cha          Bảng con             Khóa ngoại                       Quan hệ
  ----------------- -------------------- -------------------------------- -----------------
  `NhanVien`        `TaiKhoan`           `TaiKhoan.MaNhanVien`            Một nhân viên có
                                                                          tối đa một tài
                                                                          khoản

  `DanhMuc`         `SanPham`            `SanPham.MaDanhMuc`              Một danh mục có
                                                                          nhiều sản phẩm

  `NhaCungCap`      `PhieuNhap`          `PhieuNhap.MaNhaCungCap`         Một nhà cung cấp
                                                                          có nhiều phiếu
                                                                          nhập

  `NhanVien`        `PhieuNhap`          `PhieuNhap.MaNhanVien`           Một nhân viên có
                                                                          thể lập nhiều
                                                                          phiếu nhập

  `PhieuNhap`       `ChiTietPhieuNhap`   `ChiTietPhieuNhap.MaPhieuNhap`   Một phiếu nhập có
                                                                          nhiều dòng chi
                                                                          tiết

  `SanPham`         `ChiTietPhieuNhap`   `ChiTietPhieuNhap.MaSanPham`     Một sản phẩm có
                                                                          thể xuất hiện
                                                                          trong nhiều phiếu
                                                                          nhập

  `KhachHang`       `HoaDon`             `HoaDon.MaKhachHang`             Một khách hàng có
                                                                          thể có nhiều hóa
                                                                          đơn; khách vãng
                                                                          lai được phép
                                                                          NULL

  `NhanVien`        `HoaDon`             `HoaDon.MaNhanVien`              Một nhân viên có
                                                                          thể lập nhiều hóa
                                                                          đơn

  `HoaDon`          `ChiTietHoaDon`      `ChiTietHoaDon.MaHoaDon`         Một hóa đơn có
                                                                          nhiều dòng chi
                                                                          tiết

  `SanPham`         `ChiTietHoaDon`      `ChiTietHoaDon.MaSanPham`        Một sản phẩm có
                                                                          thể xuất hiện
                                                                          trong nhiều hóa
                                                                          đơn
  -----------------------------------------------------------------------------------------

### Mô hình Header -- Detail

Hai nghiệp vụ nhập hàng và bán hàng đều sử dụng mô hình Header --
Detail:

-   `PhieuNhap` lưu thông tin chung; `ChiTietPhieuNhap` lưu từng sản
    phẩm, số lượng và đơn giá nhập.
-   `HoaDon` lưu thông tin chung; `ChiTietHoaDon` lưu từng sản phẩm, số
    lượng và đơn giá bán.

Cách tách này tránh lặp lại thông tin chung trên từng dòng sản phẩm.

## 4. Danh mục sản phẩm chuẩn

Bảng `DanhMuc` sử dụng sáu mã danh mục đã thống nhất:

  Mã       Tên
  -------- ----------
  `DM01`   Laptop
  `DM02`   CPU
  `DM03`   RAM
  `DM04`   Ổ cứng
  `DM05`   Màn hình
  `DM06`   Phụ kiện

Không tự ý thay đổi mã hoặc bổ sung danh mục nếu chưa có sự thống nhất
của nhóm.

## 5. Quy tắc nghiệp vụ liên quan đến tồn kho

### Nhập hàng

1.  Tạo phiếu nhập ở trạng thái `ChoXacNhan`.
2.  Thêm các dòng sản phẩm vào `ChiTietPhieuNhap`.
3.  Khi phiếu được xác nhận, tăng `SanPham.SoLuongTon`.
4.  Cập nhật trạng thái phiếu thành `DaXacNhan`.

Phiếu ở trạng thái chờ xác nhận chưa làm tăng tồn kho.

### Bán hàng

1.  Tạo hóa đơn ở trạng thái `ChoThanhToan`.
2.  Thêm sản phẩm vào `ChiTietHoaDon`.
3.  Kiểm tra tồn kho trước khi hoàn tất thanh toán.
4.  Khi thanh toán thành công, giảm `SanPham.SoLuongTon`.
5.  Cập nhật trạng thái hóa đơn thành `DaThanhToan`.

Không cho phép bán số lượng vượt quá tồn kho.

### Transaction

Việc cập nhật trạng thái chứng từ và số lượng tồn kho phải được thực
hiện trong cùng một database transaction:

-   Tất cả thao tác thành công: `COMMIT`.
-   Có thao tác thất bại: `ROLLBACK`.

Mục tiêu là tránh trường hợp chứng từ đã được xác nhận nhưng tồn kho
chưa cập nhật, hoặc tồn kho đã thay đổi nhưng chứng từ không được lưu
thành công.

## 6. Ràng buộc và lưu ý dữ liệu

-   Primary Key định danh duy nhất mỗi bản ghi.
-   Foreign Key đảm bảo dữ liệu tham chiếu đến bản ghi tồn tại.
-   `UNIQUE` được sử dụng cho các trường cần tránh trùng lặp như tên
    đăng nhập.
-   `CHECK` kiểm soát giá trị hợp lệ như giá không âm, số lượng tồn
    không âm và trạng thái 0/1.
-   `ThanhTien` trong hai bảng chi tiết là generated column, được tính
    từ số lượng nhân đơn giá.
-   `TaiKhoan.MatKhau` phải lưu password hash, không lưu mật khẩu dạng
    văn bản thuần.
-   Các khóa ngoại đang dùng `ON DELETE RESTRICT` để ngăn xóa dữ liệu
    cha khi còn dữ liệu con tham chiếu.
-   `HoaDon.MaKhachHang` cho phép `NULL` để hỗ trợ khách mua trực tiếp
    không đăng ký thông tin.

## 7. File trong thư mục này

Cấu trúc đề xuất:

``` text
Documents/
└── Database/
    ├── README.md
    ├── ComputerStore.mwb
    └── ERD.png
```

  File                  Mục đích
  --------------------- ----------------------------------------------------
  `README.md`           Thuyết minh thiết kế database và quy tắc nghiệp vụ
  `ComputerStore.mwb`   Mô hình MySQL Workbench có thể mở và chỉnh sửa
  `ERD.png`             Ảnh sơ đồ quan hệ để xem nhanh và đưa vào báo cáo

## 8. Cách mở và cập nhật sơ đồ

1.  Mở MySQL Workbench.
2.  Chọn **File → Open Model**.
3.  Mở `ComputerStore.mwb`.
4.  Chỉnh sửa EER Diagram khi schema đã được nhóm thống nhất thay đổi.
5.  Lưu model bằng `Ctrl + S`.
6.  Xuất lại ảnh ERD sau khi bố cục đã hoàn chỉnh.

Khi schema SQL thay đổi, nhóm cần cập nhật model và ảnh ERD để tài liệu
không bị lệch so với database thực tế.

## 9. Quy ước làm việc nhóm

-   Không tự ý đổi tên bảng, cột, kiểu dữ liệu hoặc quan hệ khóa ngoại.
-   Mọi thay đổi schema phải được trao đổi và thống nhất trước khi cập
    nhật.
-   Cập nhật `ComputerStore.sql`, `ComputerStore.mwb`, `ERD.png` và tài
    liệu này khi có thay đổi được duyệt.
-   Các thành viên sử dụng cùng schema và cùng quy tắc nghiệp vụ khi
    triển khai DTO, DAL, BLL và GUI.
