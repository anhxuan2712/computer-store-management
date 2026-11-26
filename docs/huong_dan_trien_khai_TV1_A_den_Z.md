# Giải phẫu Chi tiết từ A → Z phần việc của Thành viên 1 (Nguyễn Anh Xuân)

Tài liệu này viết theo nguyên tắc **không nói chung chung**: mọi khái niệm, từng câu SQL, từng hàm C# đều trả lời đủ **3 câu hỏi**:
1. **Nó là cái gì?** (giải thích bằng ví dụ đời thường + trích code thật trong dự án)
2. **Nó chạy từng bước như thế nào?** (điền dữ liệu thật như `admin` / `123456` để thấy từng biến thay đổi ra sao)
3. **Tại sao phải có nó? Nếu KHÔNG có thì chuyện gì xảy ra?**

---

## PHẦN 1: CƠ SỞ DỮ LIỆU MYSQL (`ComputerStore.sql` & `SampleData.sql`)

### 1.1. Tại sao có `CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci`?
File: [ComputerStore.sql](../Database/ComputerStore.sql#L8-L10)
```sql
CREATE DATABASE IF NOT EXISTS ComputerStore
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
```
- **Là gì:** Bộ mã ký tự để MySQL lưu được đầy đủ tiếng Việt có dấu (và cả emoji nếu cần). `utf8mb4_unicode_ci` còn quy định cách sắp xếp & so sánh chuỗi không phân biệt hoa thường (`ci` = *case-insensitive*, ví dụ tìm `"laptop"` vẫn ra `"Laptop"`).
- **Không có thì sao?** Một số cấu hình MySQL mặc định dùng `latin1`. Khi bạn lưu `"Nguyễn Anh Xuân"` hoặc `"Ổ cứng"`, MySQL sẽ báo lỗi `Incorrect string value` hoặc biến thành `"Nguy?n Anh Xu?n"`.

---

### 1.2. Tại sao `NhanVien` và `TaiKhoan` có quan hệ 1–1 (và không gộp chung 1 bảng)?
File: [ComputerStore.sql](../Database/ComputerStore.sql#L18-L55)
```sql
CREATE TABLE IF NOT EXISTS TaiKhoan (
    MaTaiKhoan INT AUTO_INCREMENT PRIMARY KEY,
    MaNhanVien INT NOT NULL UNIQUE,   -- Khóa ngoại trỏ sang NhanVien + UNIQUE => quan hệ 1-1
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL,
    VaiTro ENUM('Admin', 'NhanVien') NOT NULL DEFAULT 'NhanVien',
    TrangThai TINYINT NOT NULL DEFAULT 1,
    ...
    CONSTRAINT fk_taikhoan_nhanvien
        FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
) ENGINE=InnoDB;
```
- **Vì sao phải là 1–1 (chính xác là 1 – 0..1)?**
  - **1 tài khoản chỉ thuộc đúng 1 nhân viên:** Không cho 2 nhân viên dùng chung 1 tài khoản, vì khi lập hóa đơn/phiếu nhập (`HoaDon.MaNhanVien`, `PhieuNhap.MaNhanVien`) phải truy vết chính xác **ai** là người thao tác.
  - **1 nhân viên chỉ cần tối đa 1 tài khoản:** Nhân viên bán hàng chỉ cần 1 tài khoản để đăng nhập. Nếu làm quan hệ 1–N (1 nhân viên nhiều tài khoản), khi người đó nghỉ việc sẽ phải đi khóa từng tài khoản rất dễ bỏ sót lỗ hổng bảo mật.
  - **Cách MySQL ép thành 1–1:** Chỉ có `FOREIGN KEY (MaNhanVien)` là quan hệ 1–N; gắn thêm **`UNIQUE`** lên chính cột khóa ngoại `MaNhanVien` sẽ cấm 2 dòng `TaiKhoan` trỏ về cùng 1 nhân viên → thành quan hệ **1–1**.
- **Đã 1–1 sao không gộp chung thành 1 bảng `NhanVien` luôn?**
  1. **Không phải nhân viên nào cũng có tài khoản:** Bảo vệ, nhân viên giao hàng, thợ lắp máy cần lưu hồ sơ trong `NhanVien` nhưng không đăng nhập phần mềm. Nếu gộp bảng thì 3 cột `TenDangNhap`, `MatKhau`, `VaiTro` của họ bị bỏ trống (`NULL`).
  2. **Tách biệt bảo mật:** Thông tin hồ sơ (`HoTen`, `SoDienThoai`, `DiaChi`) tách riêng khỏi dữ liệu nhạy cảm (`MatKhau` hash, `VaiTro`).
  3. **Dễ bảo trì:** Code tách rõ `NhanVienBLL/DAL` và `TaiKhoanBLL/DAL` độc lập với nhau.

---

### 1.3. Giải thích `NOT NULL`, `UNIQUE`, `ENUM`, `CHECK` và `ON DELETE RESTRICT`

- **`NOT NULL` là gì?**
  - Nghĩa là **"bắt buộc phải có dữ liệu, KHÔNG được để trống (`NULL`)"**.
  - Ví dụ trong bảng `NhanVien`: `HoTen VARCHAR(100) NOT NULL` bắt buộc nhập; còn `Email VARCHAR(100) UNIQUE` và `NgaySinh DATE` không ghi `NOT NULL` nên được phép để trống (`NULL`).
  - *Phân biệt:* `NULL` là **chưa có dữ liệu**, còn `""` (chuỗi rỗng) vẫn là **có dữ liệu (dài 0 ký tự)**.
  - *Không có `NOT NULL` thì sao?* Có thể lọt nhân viên không tên, tài khoản không mật khẩu, và khi C# gọi `reader.GetString("HoTen")` gặp `NULL` sẽ văng lỗi `SqlNullValueException`.
- **`UNIQUE` (trên `SoDienThoai`, `Email`, `TenDangNhap`, `MaNhanVien`):**
  - *Không có thì sao?* Hai người cùng tạo tài khoản tên `admin` hoặc cùng nhập 1 số điện thoại → lúc đăng nhập hệ thống không biết lấy tài khoản nào, lúc tìm kiếm bị trả nhầm người.
- **`ENUM('Admin', 'NhanVien')` & `CHECK (TrangThai IN (0, 1))`:**
  - *Là gì:* "Hàng rào chốt chặn cuối cùng" ngay trong MySQL. Chỉ chấp nhận đúng các giá trị quy định.
  - *Không có thì sao?* Nếu code C# có bug hoặc ai đó mở MySQL Workbench gõ nhầm `VaiTro = 'QuanLy'` hay `TrangThai = 5`, DB vẫn cho lưu rồi lúc chạy C# so sánh `VaiTro == "Admin"` sẽ sai hết logic phân quyền.
- **`ON UPDATE CASCADE` và `ON DELETE RESTRICT`:**
  - `ON UPDATE CASCADE`: Nếu mã nhân viên bị đổi, các bảng con tự đổi theo.
  - `ON DELETE RESTRICT`: **Cấm xóa** nhân viên nếu mã nhân viên đó đang được nhắc tới ở bảng `TaiKhoan`, `PhieuNhap` hoặc `HoaDon`.
  - *Nếu đổi thành `ON DELETE CASCADE` thì sao?* Cực kỳ nguy hiểm! Ví dụ nhân viên **An** đã bán 200 hóa đơn trong năm rồi nghỉ việc. Nếu dùng `CASCADE`, chỉ cần xóa nhân viên An là **toàn bộ 200 hóa đơn lịch sử tiền bạc tự động bốc hơi theo** → lệch sổ sách doanh thu!

---

### 1.4. Hàm băm mật khẩu `SHA2('123456', 256)` — Giải thích CỰC KỸ!
File: [SampleData.sql](../Database/SampleData.sql#L94-L101)
```sql
INSERT INTO TaiKhoan (MaNhanVien, TenDangNhap, MatKhau, VaiTro, TrangThai)
SELECT MaNhanVien, 'admin', SHA2('123456', 256), 'Admin', 1
FROM NhanVien
WHERE SoDienThoai = '0912345678'
ON DUPLICATE KEY UPDATE ...
```

#### a) SHA-256 là cái gì?
Hãy tưởng tượng **SHA-256 như một chiếc máy xay sinh tố 1 chiều**:
- Bạn bỏ chữ `'123456'` vào máy → nó xay ra **đúng một chuỗi cố định dài 64 ký tự**:
  ```text
  8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92
  ```
- **3 tính chất đặc biệt:**
  1. **Cùng đầu vào → luôn ra 1 kết quả giống hệt nhau:** Dù xay bằng MySQL (`SHA2('123456', 256)`) hay bằng C# (`SHA256.HashData(...)`), cho `'123456'` vào đều ra đúng 64 ký tự trên.
  2. **Đổi chỉ 1 ký tự → kết quả đổi sạch 100%:** `'123457'` ra một chuỗi khác hoàn toàn, không đoán được.
  3. **Chỉ đi 1 chiều (không dịch ngược được):** Từ ly sinh tố `8d969eef...` **không có công thức toán học nào** ép ngược lại thành quả táo `'123456'`.

#### b) Tại sao phải băm? Nếu KHÔNG băm (lưu thẳng `'123456'`) thì sao?
- Nếu lưu thẳng `'123456'` (gọi là lưu *plaintext*):
  1. Bất kỳ ai mở MySQL Workbench (hoặc nhân viên IT quản trị DB) chạy `SELECT * FROM TaiKhoan` là đọc sạch mật khẩu thật của chủ cửa hàng / Admin.
  2. Nếu lỡ bị lộ file backup database, hacker có ngay mật khẩu thật (mà người dùng hay đặt trùng mật khẩu Facebook/ngân hàng!).
  3. Trong môn học / bảo vệ đồ án, lưu mật khẩu dạng chữ thường là **lỗi bảo mật cơ bản nhất**, giảng viên hỏi tới là bị trừ điểm ngay.

#### c) Đã không dịch ngược được thì lúc Đăng nhập máy tính kiểm tra bằng cách nào?
- **Cơ chế đối chiếu cực kỳ đơn giản:**
  1. Khi tạo tài khoản với mật khẩu `'123456'`, hệ thống băm ra `8d969eef...c6c92` rồi lưu vào cột `TaiKhoan.MatKhau`.
  2. Hôm sau người dùng mở `FrmLogin` gõ `'123456'`.
  3. Bên C# ([TaiKhoanBLL.cs](../ComputerStore.BLL/Services/TaiKhoanBLL.cs#L60-L65)) **không giải mã** chuỗi trong DB, mà lấy `'123456'` người dùng vừa gõ đem băm bằng đúng SHA-256 → cũng ra `8d969eef...c6c92`.
  4. So sánh 2 chuỗi đã băm:
     - Giống hệt nhau → **đúng mật khẩu!**
     - Khác nhau (vd gõ `'123457'`) → **sai mật khẩu!**

#### d) Bonus: Tại sao trong `SampleData.sql` lại viết `INSERT ... SELECT MaNhanVien ... WHERE SoDienThoai = '0912345678' ON DUPLICATE KEY UPDATE`?
- Nếu gõ cứng `VALUES (1, 'admin', ...)`: lỡ trước đó bạn từng thêm/xóa thử nhân viên rồi chạy lại script, mã `AUTO_INCREMENT` của Nguyễn Văn An không còn là `1` mà nhảy lên `3` → câu lệnh bị lỗi khóa ngoại ngay!
- Tìm theo SĐT (`WHERE SoDienThoai = '0912345678'`) đảm bảo luôn lấy đúng `MaNhanVien` hiện tại của Nguyễn Văn An.
- `ON DUPLICATE KEY UPDATE` đảm bảo nếu bạn bấm chạy lại file `SampleData.sql` 2–3 lần thì MySQL chỉ cập nhật lại thay vì văng lỗi đỏ `Duplicate entry 'admin'`.

---

## PHẦN 2: KIẾN TRÚC 4 TẦNG, KẾT NỐI MYSQL & DTO

### 2.1. Tại sao tách 4 project `GUI`, `BLL`, `DAL`, `DTO`? Nếu viết chung 1 project thì sao?
Hãy ví cửa hàng máy tính như một **nhà hàng**:
- **`GUI` (Bồi bàn):** Đứng ngoài nhận yêu cầu từ khách (ô `TextBox`, nút bấm `Button`) và bưng món ra (`DataGridView`, `MessageBox`). Bồi bàn **không tự chạy vào kho lấy thịt hay tự nấu**.
- **`BLL` (Quản lý bếp):** Kiểm tra đơn có hợp lệ không (SĐT có đủ 10 số chưa? Nhân viên này có bị khóa không? Tồn kho còn đủ bán không?). Nếu hợp lệ mới ra lệnh cho đầu bếp.
- **`DAL` (Đầu bếp / Thủ kho):** Người duy nhất biết đường vào kho MySQL (`SELECT`, `INSERT`, `UPDATE`, `DELETE`).
- **`DTO` (Cái khay đựng đồ ăn):** Chứa dữ liệu (`NhanVienDTO`, `TaiKhoanDTO`) để truyền từ Bồi bàn → Quản lý → Thủ kho mà không bị rơi rớt.

**Nếu KHÔNG tách 4 tầng (nhét hết SQL vào nút bấm `btnThem_Click` trên Form) thì chuyện gì xảy ra?**
1. Một file Form phình lên cả nghìn dòng, vừa có màu nút, vừa có `if` kiểm tra email, vừa có câu `INSERT INTO...` → cực kỳ khó đọc và sửa lỗi.
2. Trong nhóm 3 người: nếu muốn sửa 1 quy tắc (vd SĐT phải 10 số) mà có 3 Form cùng dùng, phải đi sửa ở 3 nơi.
3. Trong [ComputerStore.GUI.csproj](../ComputerStore.GUI/ComputerStore.GUI.csproj#L11-L14), ta **cố tình chỉ cho `GUI` tham chiếu `BLL` và `DTO`** (KHÔNG tham chiếu `DAL`). Nhờ vậy, nếu thành viên nào cố tình gọi SQL trực tiếp trên Form, Visual Studio sẽ báo lỗi đỏ ngay lập tức!

---

### 2.2. Giải phẫu từng dòng [DbConnection.cs](../ComputerStore.DAL/Database/DbConnection.cs)

```csharp
string? user = Environment.GetEnvironmentVariable("COMPUTERSTORE_DB_USER");
string? password = Environment.GetEnvironmentVariable("COMPUTERSTORE_DB_PASSWORD");
```
1. **Biến môi trường (`Environment.GetEnvironmentVariable`) là gì?**
   - Là các cặp `Tên = Giá trị` lưu riêng trong hệ điều hành Windows của từng máy (bằng lệnh `setx COMPUTERSTORE_DB_USER "root"`).
   - **Tại sao phải có?**
     - Nhóm 3 người dùng chung 1 kho GitHub. Máy bạn đặt mật khẩu MySQL là `123456`, máy Vượng đặt `vuong2026`, máy Trí đặt `root`.
     - **Nếu KHÔNG dùng (gõ cứng mật khẩu trong file `.cs`):** (1) Đẩy lên GitHub là người ngoài thấy luôn mật khẩu máy bạn; (2) Hôm nay bạn push pass của bạn, mai Vượng pull về không chạy được lại sửa thành pass của Vượng rồi push đè lên → cả nhóm đánh nhau vì xung đột code ở `DbConnection.cs`!
2. **Tại sao có dòng `private static string? connectionString;` (dòng 8–13)?**
   - Để lưu nhớ (cache) chuỗi kết nối sau lần đầu lắp ghép. Thay vì mỗi lần truy vấn SQL lại hỏi Windows đọc biến môi trường, chương trình chỉ đọc đúng **1 lần lúc khởi động**, các lần sau lấy dùng ngay.
3. **Tại sao dùng `MySqlConnectionStringBuilder` (dòng 29–37) thay vì nối chuỗi `"Server=localhost;Uid=" + user + ";Pwd=" + password`?**
   - Nếu mật khẩu của ai đó có dấu chấm phẩy `;` (ví dụ `pass;123`), việc nối chuỗi tay sẽ khiến MySQL hiểu nhầm `123` là một tham số cấu hình khác và báo lỗi kết nối. `MySqlConnectionStringBuilder` tự động bọc dấu nháy an toàn cho mọi ký tự đặc biệt.
   - Dòng `CharacterSet = "utf8mb4"` ép kênh truyền giữa C# và MySQL dùng UTF-8 4-byte để không lỗi tiếng Việt.
4. **Tại sao lại có thêm file [DatabaseService.cs](../ComputerStore.BLL/Services/DatabaseService.cs) chỉ để gọi lại `DbConnection.TestConnection`?**
   - Vì `Program.cs` nằm ở `ComputerStore.GUI`, mà `GUI` **không được phép** tham chiếu trực tiếp `DAL`. Nên phải có `DatabaseService` nằm ở `BLL` đứng trung gian chuyển lời gọi từ `Program.cs` → `DatabaseService` → `DbConnection`.

---

### 2.3. Giải phẫu DTO & Dấu hỏi `?` (`Nullable`)
File: [NhanVienDTO.cs](../ComputerStore.DTO/Entities/NhanVienDTO.cs#L6-L18)
```csharp
public class NhanVienDTO
{
    public int MaNhanVien { get; set; }
    public string HoTen { get; set; } = string.Empty;   // Không có ? vì DB là NOT NULL
    public string? GioiTinh { get; set; } = "Khac";
    public DateTime? NgaySinh { get; set; }             // Có ? vì DB cho phép NULL
    public string? SoDienThoai { get; set; }            // Có ? vì DB cho phép NULL
    ...
}
```
- **Tại sao cần DTO?** Nếu không có `NhanVienDTO`, hàm `Insert` sẽ có 8 tham số rời rạc:
  `Insert(string hoTen, string gioiTinh, DateTime? ngaySinh, string sdt, string email, string diaChi, DateTime? ngayVaoLam, int trangThai)`
  Chỉ cần bạn truyền nhầm vị trí `email` và `diaChi` (đều là `string`) là C# không hề báo lỗi nhưng dữ liệu lưu lộn xộn hết! Gói vào 1 đối tượng `NhanVienDTO` có tên thuộc tính rõ ràng thì không bao giờ nhầm.
- **Tại sao `NgaySinh` phải có dấu `?` (`DateTime?`), không có thì sao?**
  - Trong C#, `DateTime` bình thường **không thể mang giá trị rỗng (`null`)** — nếu chưa gán gì nó tự mang giá trị `01/01/0001 00:00:00`.
  - Khi người dùng chưa biết ngày sinh của nhân viên (bỏ tích `DateTimePicker`), nhờ `DateTime?` ta gán được `NgaySinh = null` → DAL chuyển thành `DBNull.Value` lưu `NULL` sạch sẽ xuống MySQL. Không có `?` thì MySQL sẽ lưu năm `0001` hoặc báo lỗi ngày không hợp lệ!

---

## PHẦN 3: GIẢI PHẪU TẦNG DAL (`TaiKhoanDAL.cs` & `NhanVienDAL.cs`)

Mọi hàm trong 2 file này đều có **5 cơ chế kỹ thuật quan trọng**. Nắm chắc 5 cơ chế này là bạn hiểu 100% tầng DAL:

### 3.1. Tại sao lúc nào cũng bọc trong `using (MySqlConnection ...)`?
File: [TaiKhoanDAL.cs](../ComputerStore.DAL/Repositories/TaiKhoanDAL.cs#L25-L41)
```csharp
using (MySqlConnection connection = DbConnection.GetConnection())
{
    connection.Open();
    using (MySqlCommand command = new MySqlCommand(query, connection))
    {
        ...
    }
} // <-- Ra khỏi dấu ngoặc này, C# TỰ ĐỘNG gọi connection.Dispose() (đóng kết nối)!
```
- **Không có `using` thì sao?** MySQL Server có giới hạn số kết nối mở cùng lúc (mặc định 151). Nếu bạn tự gọi `connection.Open()` rồi đến cuối hàm mới gọi `connection.Close()`, giả sử dòng SQL ở giữa bị lỗi và nhảy văng ra ngoài `catch` → dòng `connection.Close()` **không bao giờ được chạy**! Kết nối bị "treo" chiếm chỗ mãi. Cứ lỗi hoặc quên đóng ~150 lần là MySQL sập với lỗi `Too many connections`.
- `using` giống như bản lề cửa tự đóng: **dù chạy xuôi hay văng lỗi giữa chừng**, kết nối và bộ nhớ lệnh đều tự động giải phóng ngay lập tức.

---

### 3.2. Parameterized Query (`@TenDangNhap`) — Tại sao không cộng chuỗi? (Ví dụ hack SQL Injection thật!)
File: [TaiKhoanDAL.cs](../ComputerStore.DAL/Repositories/TaiKhoanDAL.cs#L23-L31)
```csharp
string query = SelectColumns + " WHERE TenDangNhap = @TenDangNhap LIMIT 1;";
command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
```
- **Nếu KHÔNG dùng `@TenDangNhap` mà viết cộng chuỗi kiểu cũ thì hậu quả gì xảy ra?**
  Giả sử bạn viết:
  ```csharp
  string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = '" + txtUser + "' AND MatKhau = '" + txtPass + "'";
  ```
  1. **Bị hack (SQL Injection):** Kẻ xấu không cần biết mật khẩu, chỉ cần gõ vào ô Tên đăng nhập:
     `admin' -- `
     Lúc này câu SQL gửi xuống MySQL trở thành:
     ```sql
     SELECT * FROM TaiKhoan WHERE TenDangNhap = 'admin' -- ' AND MatKhau = 'abc'
     ```
     Trong MySQL, dấu `-- ` nghĩa là **chú thích bỏ qua phần phía sau** → toàn bộ đoạn `AND MatKhau = ...` bị xóa sổ! Kẻ xấu **đăng nhập thẳng vào tài khoản Admin không cần mật khẩu!**
  2. **Lỗi vặt đời thường:** Nếu tên địa chỉ hoặc sản phẩm có dấu nháy đơn `'` (ví dụ `O'Brien` hoặc `Màn hình 27'`), câu SQL nối chuỗi bị thừa 1 dấu `'` và nổ lỗi cú pháp `SQL syntax error`.
- **Khi dùng `@TenDangNhap`:** MySQL nhận khung lệnh riêng và dữ liệu riêng. Chuỗi `admin' -- ` chỉ được đối xử như **văn bản bình thường**, tuyệt đối không bao giờ bị chạy như câu lệnh SQL!

---

### 3.3. Phân biệt 3 lệnh `ExecuteReader`, `ExecuteScalar`, `ExecuteNonQuery`
Trong code của bạn có đủ cả 3 lệnh của ADO.NET — rất dễ bị giảng viên hỏi phân biệt:

| Lệnh | Dùng khi nào? | Kiểu trả về | Ví dụ trong code của bạn |
| :--- | :--- | :--- | :--- |
| **`ExecuteReader()`** | Câu `SELECT` lấy **nhiều cột / nhiều dòng** | `MySqlDataReader` (đọc từng dòng bằng `reader.Read()`) | `GetAll()`, `Search()`, `GetByUsername()` |
| **`ExecuteScalar()`** | Câu `SELECT` chỉ lấy **đúng 1 ô duy nhất** (hàng 1, cột 1) | `object` (ép sang `int` bằng `Convert.ToInt32`) | `SELECT COUNT(*) ...` trong `ExistsUsername`, `ExistsPhone`, `HasRelatedRecords` |
| **`ExecuteNonQuery()`** | Câu **`INSERT`, `UPDATE`, `DELETE`** (không trả bảng) | `int` (số dòng bị tác động; `> 0` là thành công) | `Insert()`, `Update()`, `Delete()` |

---

### 3.4. Năm chi tiết xử lý cực tinh tế trong `TaiKhoanDAL` & `NhanVienDAL`

1. **Tại sao `ExistsPhone(soDienThoai, excludeMaNhanVien = 0)` lại có điều kiện `AND MaNhanVien <> @MaNhanVien`?**
   File: [NhanVienDAL.cs](../ComputerStore.DAL/Repositories/NhanVienDAL.cs#L168-L174)
   ```sql
   SELECT COUNT(*) FROM NhanVien
   WHERE SoDienThoai = @SoDienThoai
     AND MaNhanVien <> @MaNhanVien;
   ```
   - **Tình huống thực tế:** Nhân viên An (`MaNhanVien = 1`, SĐT `0912345678`) đổi địa chỉ nhà từ Hà Nội sang Hải Phòng, SĐT vẫn giữ `0912345678` rồi bấm **Sửa**.
   - **Nếu KHÔNG có `AND MaNhanVien <> @MaNhanVien`:** Câu SQL đếm thấy có 1 dòng mang SĐT `0912345678` (chính là dòng của ông An!) → trả `true` → báo lỗi *"Số điện thoại này đã thuộc về nhân viên khác"* → **vĩnh viễn không bấm Sửa được!**
   - **Có `AND MaNhanVien <> 1`:** MySQL bỏ qua dòng có mã `1`, chỉ kiểm tra xem có nhân viên nào **khác** mang SĐT đó không → trả `0` → sửa ngon lành!
   - Còn lúc **Thêm mới**, `MaNhanVien = 0` (chưa có ai mã 0) → MySQL kiểm tra toàn bộ bảng. (Cơ chế y hệt trong `TaiKhoanDAL.ExistsUsername` và `NhanVienDAL.ExistsEmail`!).

2. **Tại sao `TaiKhoanDAL.Update(taiKhoan, bool updatePassword)` có 2 câu SQL khác nhau?**
   File: [TaiKhoanDAL.cs](../ComputerStore.DAL/Repositories/TaiKhoanDAL.cs#L120-L135)
   - **Tình huống thực tế:** Ông Bình xin nghỉ phép dài ngày, Admin mở `FrmNhanVien` muốn **khóa tài khoản** (`TrangThai = 0`) hoặc nâng quyền lên `Admin` nhưng **không muốn đổi mật khẩu** của ông Bình (ô `txtMatKhau` để trống).
   - **Nếu KHÔNG có cờ `updatePassword`:** Code sẽ băm chuỗi rỗng `""` rồi ghi đè vào `TaiKhoan.MatKhau` → mật khẩu cũ của ông Bình bị phá hỏng!
   - **Có cờ `updatePassword`:** Nếu `txtMatKhau` trống → `updatePassword = false` → chạy câu `UPDATE` **không đụng vào cột `MatKhau`**. Chỉ khi Admin gõ mật khẩu mới vào ô đó thì mới đổi.

3. **Tại sao `NhanVienDAL.Insert` có dòng `nhanVien.MaNhanVien = (int)command.LastInsertedId;`?**
   File: [NhanVienDAL.cs](../ComputerStore.DAL/Repositories/NhanVienDAL.cs#L109-L114)
   - Khi thêm mới nhân viên, `MaNhanVien` trong C# đang là `0` vì MySQL tự sinh số (`AUTO_INCREMENT`). Sau khi `INSERT` xong, thuộc tính `command.LastInsertedId` của `MySql.Data` chứa ngay con số MySQL vừa sinh (vd: `3`). Gán ngược lại cho `nhanVien.MaNhanVien` giúp BLL trả ra câu thông báo đẹp: *"Đã thêm nhân viên mã 3."*

4. **Xử lý `NULL` hai chiều (`IsDBNull` & `DBNull.Value`):**
   File: [NhanVienDAL.cs](../ComputerStore.DAL/Repositories/NhanVienDAL.cs#L269-L301)
   - Trong C#, không có dữ liệu là `null`. Trong ADO.NET / MySQL, không có dữ liệu là `DBNull.Value`.
   - **Lúc đọc từ MySQL:** Nếu cột `Email` của nhân viên trong DB là `NULL` mà bạn gọi thẳng `reader.GetString("Email")` không kiểm tra `reader.IsDBNull(...)` → **chương trình văng lỗi `SqlNullValueException` chết màn hình ngay!**
   - **Lúc ghi xuống MySQL:** `(object?)nhanVien.Email ?? DBNull.Value` nghĩa là: *"Nếu `Email` khác `null` thì lấy chuỗi đó, còn nếu là `null` thì gửi `DBNull.Value` để MySQL lưu thành `NULL`."*

5. **Tại sao có hàm `HasRelatedRecords(int maNhanVien)`?**
   File: [NhanVienDAL.cs](../ComputerStore.DAL/Repositories/NhanVienDAL.cs#L212-L231)
   ```sql
   SELECT
       (SELECT COUNT(*) FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien)
     + (SELECT COUNT(*) FROM PhieuNhap WHERE MaNhanVien = @MaNhanVien)
     + (SELECT COUNT(*) FROM HoaDon WHERE MaNhanVien = @MaNhanVien);
   ```
   - Đếm tổng số dữ liệu liên quan ở cả 3 bảng con chỉ trong **1 lần gửi lệnh SQL**.
   - **Không có hàm này thì sao?** Khi bấm Xóa nhân viên đã có tài khoản/hóa đơn, lệnh `DELETE` đâm thẳng vào `ON DELETE RESTRICT` của MySQL và quăng ra cửa sổ lỗi đỏ tiếng Anh: `Cannot delete or update a parent row: a foreign key constraint fails...` rất thiếu chuyên nghiệp.

---

## PHẦN 4: GIẢI PHẪU TẦNG BLL (`TaiKhoanBLL.cs` & `NhanVienBLL.cs`)

### 4.1. Trace từng dòng 1 lần Đăng nhập thật (`"  admin  "` / `"123456"`)
File: [TaiKhoanBLL.cs](../ComputerStore.BLL/Services/TaiKhoanBLL.cs#L21-L84)

Giả sử người dùng gõ tên đăng nhập `"  admin  "` (lỡ tay gõ thừa dấu cách đầu cuối) và mật khẩu `"123456"`:

1. **Dòng 38–39 — Chuẩn hóa đầu vào:**
   ```csharp
   tenDangNhap = tenDangNhap?.Trim() ?? string.Empty; // -> "admin"
   matKhau = matKhau ?? string.Empty;                 // -> "123456" (KHÔNG Trim!)
   ```
   - *Hỏi hay:* **Tại sao `tenDangNhap` có `.Trim()` còn `matKhau` KHÔNG `.Trim()`?**
     Vì khoảng trắng có thể là 1 ký tự thật sự nằm trong mật khẩu của người dùng; nếu tự ý `.Trim()` sẽ làm sai mật khẩu người ta đặt.
2. **Dòng 41–51 — Chặn rỗng:** Chưa nhập tên hoặc chưa nhập mật khẩu → `return false` ngay, khỏi mất công gọi xuống MySQL.
3. **Dòng 53–58 — Tìm tài khoản trong DB:**
   `TaiKhoanDTO? tk = taiKhoanDAL.GetByUsername("admin");`
   Nếu `tk == null` → báo `"Tên đăng nhập hoặc mật khẩu không đúng."`
   - *Tại sao không báo thẳng `"Tên đăng nhập không tồn tại"`?* Vì trong bảo mật, nếu báo riêng từng lỗi, kẻ tấn công chỉ cần thử danh sách tên là biết chắc tài khoản nào đang có trong hệ thống để tập trung dò mật khẩu.
4. **Dòng 60–65 — Băm SHA-256 và đối chiếu:**
   ```csharp
   string hashedInput = HashPassword("123456");
   // Bước bên trong HashPassword:
   // 1. Encoding.UTF8.GetBytes("123456")      -> mảng 6 byte [49, 50, 51, 52, 53, 54]
   // 2. SHA256.HashData(bytes)                -> mảng 32 byte đã băm
   // 3. Convert.ToHexString(...).ToLowerInvariant()
   //    -> "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"
   ```
   - *Tại sao phải có `.ToLowerInvariant()`?* Vì hàm `Convert.ToHexString()` của .NET trả về chữ **IN HOA** (`8D969EEF...`), còn hàm `SHA2()` của MySQL trả về chữ **thường** (`8d969eef...`). Chuyển về chữ thường + so sánh `StringComparison.OrdinalIgnoreCase` đảm bảo 2 chuỗi luôn khớp 100%.
5. **Dòng 67–71 — Kiểm tra trạng thái Tài khoản:**
   `if (tk.TrangThai != 1)` → báo *"Tài khoản này đang bị khóa..."*.
6. **Dòng 73–78 — Kiểm tra trạng thái Nhân viên (lớp bảo vệ thứ 2!):**
   `NhanVienDTO? nv = nhanVienDAL.GetById(tk.MaNhanVien);`
   `if (nv == null || nv.TrangThai != 1)` → báo *"Nhân viên của tài khoản này đã ngừng làm việc."*
   - *Tại sao cần dòng này?* Thực tế hay có chuyện nhân viên đã nghỉ việc, Admin sửa trạng thái nhân viên thành `"Đã nghỉ việc"` nhưng **quên không khóa tài khoản**. Nhờ kiểm tra kép này, nhân viên đã nghỉ lập tức không đăng nhập được nữa!
7. **Dòng 80–83 — Thành công:**
   Gán `taiKhoan = tk; nhanVien = nv;` qua từ khóa `out` và trả về `true`.

---

### 4.2. Tại sao BLL dùng `bool Method(..., out string message)` thay vì gọi `MessageBox.Show()`?
- Mở file [ComputerStore.BLL.csproj](../ComputerStore.BLL/ComputerStore.BLL.csproj): nó dùng `<TargetFramework>net10.0</TargetFramework>`, **KHÔNG có Windows Forms**, nên trong BLL thậm chí không có lớp `MessageBox`!
- Thiết kế trả về `true/false` kèm `out string message` giúp **tách sạch logic ra khỏi giao diện**:
  - BLL chỉ biết đúng/sai và lý do là gì.
  - GUI nhận kết quả đó để quyết định hiển thị bằng `MessageBox` (hoặc sau này làm web thì hiện toast/JSON error) mà không cần sửa 1 chữ nào ở BLL.

---

### 4.3. Chi tiết thông minh trong `NhanVienBLL.Validate` & `TaiKhoanBLL.SaveAccount`

1. **Đổi chuỗi rỗng `""` thành `null` cho `SoDienThoai` và `Email` ([NhanVienBLL.cs](../ComputerStore.BLL/Services/NhanVienBLL.cs#L143-L144)):**
   ```csharp
   nhanVien.SoDienThoai = string.IsNullOrWhiteSpace(nhanVien.SoDienThoai) ? null : nhanVien.SoDienThoai.Trim();
   nhanVien.Email = string.IsNullOrWhiteSpace(nhanVien.Email) ? null : nhanVien.Email.Trim();
   ```
   - **Nếu KHÔNG có dòng này (lưu thẳng chuỗi rỗng `""` xuống MySQL) thì chuyện gì xảy ra?**
     Cột `Email` và `SoDienThoai` có ràng buộc `UNIQUE`.
     - Trong MySQL, bạn có thể lưu **100 nhân viên có `Email = NULL`** mà **KHÔNG vi phạm `UNIQUE`**.
     - Nhưng nếu nhân viên 1 không nhập email mà bạn lưu `""`, đến nhân viên 2 cũng không nhập email lưu `""` → MySQL thấy 2 chuỗi `""` giống nhau → **báo lỗi `Duplicate entry '' for key 'Email'` ngay!**
2. **Kiểm tra Regex SĐT & Email ([NhanVienBLL.cs](../ComputerStore.BLL/Services/NhanVienBLL.cs#L173-L198)):**
   - `^0\d{9,10}$`:
     - `^0`: bắt buộc bắt đầu bằng số `0`
     - `\d{9,10}`: theo sau đúng 9 hoặc 10 chữ số (tổng 10–11 số chuẩn SĐT Việt Nam)
     - `$`: kết thúc chuỗi (không được thừa chữ cái ở cuối).
   - `^[^@\s]+@[^@\s]+\.[^@\s]+$`: phải có phần tên trước `@`, có dấu `@`, có tên miền, có dấu chấm `.`, và không chứa khoảng trắng.
3. **Hàm `SaveAccount` gộp 2 việc thành 1 nút ([TaiKhoanBLL.cs](../ComputerStore.BLL/Services/TaiKhoanBLL.cs#L96-L187)):**
   - Gọi `existing = taiKhoanDAL.GetByMaNhanVien(maNhanVien)`:
     - Nếu `existing == null` (nhân viên chưa có tài khoản) → chạy nhánh **Tạo mới**: bắt buộc nhập MK ≥ 6 ký tự → băm SHA-256 → `Insert`.
     - Nếu `existing != null` (đã có tài khoản) → chạy nhánh **Cập nhật**: nếu ô MK trống thì giữ MK cũ (`updatePassword = false`); nếu có nhập MK mới thì bắt buộc ≥ 6 ký tự → băm SHA-256 → `Update(..., true)`.
4. **Tư duy "Xóa mềm" (Soft Delete) trong `NhanVienBLL.Delete` ([NhanVienBLL.cs](../ComputerStore.BLL/Services/NhanVienBLL.cs#L107-L111)):**
   - Nếu nhân viên mới tạo nhầm, chưa có tài khoản/hóa đơn nào → cho phép xóa hẳn (`DELETE`).
   - Nếu đã từng lập phiếu nhập/hóa đơn → chặn xóa và hướng dẫn chuyển `TrangThai = 0` (*"Đã nghỉ việc"*). Dữ liệu lịch sử vẫn còn nguyên, còn tài khoản của họ tự động bị chặn đăng nhập!

---

## PHẦN 5: GIẢI PHẪU TẦNG GUI & VÒNG ĐỜI (`Program.cs`, `FrmLogin`, `FrmMain`, `FrmNhanVien`)

### 5.1. Tại sao `Program.cs` lại dùng vòng lặp `while (true)` + `ShowDialog()`?
File: [Program.cs](../ComputerStore.GUI/Program.cs#L12-L40)
```csharp
DatabaseService databaseService = new DatabaseService();
if (!databaseService.TestConnection(out string message))
{
    MessageBox.Show(message, "Lỗi kết nối Database", ...);
    return;
}

while (true)
{
    using var frmLogin = new FrmLogin();
    if (frmLogin.ShowDialog() != DialogResult.OK || frmLogin.LoggedInAccount == null)
        break;

    using var frmMain = new FrmMain(frmLogin.LoggedInAccount, frmLogin.LoggedInEmployee);
    Application.Run(frmMain);

    if (!frmMain.IsLogout)
        break;
}
```
- **Tại sao không làm kiểu cũ `Application.Run(new FrmLogin())` rồi trong `FrmLogin` gọi `this.Hide(); new FrmMain().Show();`?**
  Hầu hết sinh viên mới làm WinForms viết theo kiểu `this.Hide()` và gặp ngay **2 lỗi chết người**:
  1. **App chạy ngầm không tắt được:** Vì `FrmLogin` mới là form chính của `Application.Run`, nó chỉ bị `Hide()` chứ chưa đóng! Khi người dùng bấm nút **X** tắt `FrmMain`, chương trình **vẫn sống ngầm trong Task Manager**, quay lại Visual Studio bấm Build sẽ bị báo lỗi khóa file `.exe`!
  2. **Rò rỉ bộ nhớ khi Đăng xuất:** Mở `FrmLogin` mới từ `FrmMain`, rồi mở `FrmMain` mới từ `FrmLogin` cứ chồng mãi lên nhau không giải phóng RAM.
- **Cách `while (true)` ở trên giải quyết sạch 100%:**
  1. Mở `FrmLogin` dạng hộp thoại (`ShowDialog()`). Người dùng bấm Thoát → `DialogResult != OK` → `break` → tắt app gọn gàng.
  2. Đăng nhập đúng → `FrmLogin` set `DialogResult = OK` và đóng lại; khối `using` giải phóng sạch `FrmLogin` nhưng 2 giá trị `LoggedInAccount` và `LoggedInEmployee` đã được truyền sang `new FrmMain(...)`.
  3. `Application.Run(frmMain)` chạy cửa sổ chính.
  4. Nếu người dùng bấm **Đăng xuất**: `FrmMain` đặt `IsLogout = true` rồi `Close()` → kết thúc `Application.Run` → giải phóng sạch `FrmMain` → thấy `IsLogout == true` nên vòng lặp quay lại bước 1 mở `FrmLogin` mới tinh!
  5. Nếu người dùng bấm nút **X** tắt chương trình: `IsLogout` đang là `false` → `break` → thoát sạch sẽ 100% process!

---

### 5.2. Giải phẫu 3 kỹ thuật quan trọng trong `FrmMain.cs`
File: [FrmMain.cs](../ComputerStore.GUI/Forms/Main/FrmMain.cs)

1. **Phân quyền theo Vai trò:**
   ```csharp
   bool isAdmin = string.Equals(currentAccount.VaiTro, "Admin", StringComparison.OrdinalIgnoreCase);
   btnNhanVien.Visible = isAdmin;
   ```
   - Đăng nhập bằng `admin` (`VaiTro = "Admin"`) → thấy nút **Nhân viên**.
   - Đăng nhập bằng `nhanvien` (`VaiTro = "NhanVien"`) → nút **Nhân viên** biến mất, chỉ thao tác bán hàng / nhập hàng / sản phẩm / khách hàng.

2. **Nhúng Form con vào giữa `FrmMain` (`OpenChildForm`):**
   ```csharp
   CloseActiveForm();                          // 1. Đóng & giải phóng Form cũ tránh tốn RAM
   activeForm = childForm;
   childForm.TopLevel = false;                 // 2. Bắt buộc: biến Form thành control con
   childForm.FormBorderStyle = FormBorderStyle.None; // 3. Bỏ thanh tiêu đề & nút X riêng
   childForm.Dock = DockStyle.Fill;            // 4. Tự giãn vừa khít pnlContent
   pnlContent.Controls.Add(childForm);         // 5. Gắn vào panel nội dung
   childForm.Show();
   ```
   - **Nếu quên `childForm.TopLevel = false` thì sao?** WinForms văng lỗi `ArgumentException: Top-level control cannot be added to a control` ngay lập tức, vì mặc định một `Form` là cửa sổ độc lập của Windows, không thể nhét vào bên trong một `Panel`.
   - **Nếu quên `CloseActiveForm()` thì sao?** Mỗi lần bấm chuyển menu, Form cũ vẫn nằm ẩn dưới Form mới trong `pnlContent` và ngốn RAM dần.

3. **Kết nối dữ liệu phiên đăng nhập sang phần việc của Thành viên 3:**
   ```csharp
   var frm = new FrmBanHang { MaNhanVien = currentAccount.MaNhanVien };
   ```
   - Khi bán hàng hay nhập hàng, bảng `HoaDon` và `PhieuNhap` bắt buộc có cột `MaNhanVien` (ai là người lập hóa đơn?). `FrmMain` truyền sẵn `currentAccount.MaNhanVien` sang để nhân viên không cần phải chọn lại tên mình.

---

### 5.3. Giải phẫu 4 kỹ thuật hay trong `FrmNhanVien.cs`
File: [FrmNhanVien.cs](../ComputerStore.GUI/Forms/Employees/FrmNhanVien.cs)

1. **Khóa `grpTaiKhoan` khi chưa chọn nhân viên (`grpTaiKhoan.Enabled = false`):**
   - Để cấp tài khoản cho nhân viên thì phải có `MaNhanVien` đã tồn tại trong DB (vì khóa ngoại `TaiKhoan.MaNhanVien`). Khi form mới mở hoặc vừa bấm Làm mới (chưa chọn dòng nào trên lưới), tắt `grpTaiKhoan` để tránh bấm nhầm. Khi click vào 1 dòng nhân viên trên `dgvNhanVien`, sự kiện `SelectionChanged` tự động bật `grpTaiKhoan = true` và tải tài khoản của nhân viên đó lên!
2. **Đổi số `0/1` thành chữ `"Đã nghỉ" / "Đang làm"` bằng `CellFormatting`:**
   ```csharp
   if (dgvNhanVien.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value is int tt)
   {
       e.Value = tt == 1 ? "Đang làm" : "Đã nghỉ";
       e.FormattingApplied = true;
   }
   ```
   - Dữ liệu thật trong `NhanVienDTO.TrangThai` vẫn là số nguyên `0`/`1`, nhưng trên màn hình bảng `DataGridView` lại hiện tiếng Việt dễ đọc.
3. **Hàm bọc `Execute(Func<(bool ok, string message)> action)`:**
   - Cả 3 nút **Thêm**, **Sửa**, **Xóa** đều có quy trình giống hệt nhau: *bọc `try/catch` → gọi BLL → nếu thất bại hiện cảnh báo màu vàng → nếu thành công hiện thông báo xanh + tải lại bảng + xóa trắng ô nhập*. Thay vì copy-paste 3 đoạn `try/catch` dài giống nhau, gom vào 1 hàm `Execute(...)` nhận hàm xử lý truyền vào.
4. **`e.SuppressKeyPress = true` khi nhấn Enter trong ô tìm kiếm (`txtTimKiem_KeyDown`):**
   - Trong WinForms, bấm phím Enter ở một `TextBox` bình thường sẽ phát ra tiếng kêu *"tính toong"* khó chịu của Windows. Dòng `e.SuppressKeyPress = true` chặn tiếng kêu đó và kích hoạt luôn nút Tìm kiếm.

---

## PHẦN 6: BÀI TẬP THỰC HÀNH ĐỂ BẠN TỰ LÀM CHỦ 100%

Để biến toàn bộ kiến thức trên từ *"đọc cho biết"* thành *"hiểu nằm lòng, tự code được"*, bạn hãy làm đúng 4 bước này (mất khoảng 1–2 tiếng):

1. **Bước 1 — Tự kéo thả lại 3 Form của bạn (`FrmLogin`, `FrmMain`, `FrmNhanVien`):**
   - Cả 3 Form hiện đã được đặt về Form trắng chuẩn Designer. Bạn mở Visual Studio kéo từng control, đặt đúng `(Name)` theo ghi chú ở đầu file `.cs`, rồi mở comment nối lại sự kiện. Tự tay làm bước này là bạn thuộc ngay toàn bộ luồng GUI!
2. **Bước 2 — Chạy Debug từng dòng bằng phím `F9` / `F10` / `F11`:**
   - Đặt 1 dấu chấm đỏ (Breakpoint - phím `F9`) ở dòng `bool ok = taiKhoanBLL.Login(...)` trong `FrmLogin.cs`.
   - Nhấn `F5` chạy, gõ `admin` / `123456`, bấm Đăng nhập → chương trình dừng ngay dòng đó.
   - Bấm **`F11` (Step Into)** để nhảy từng bước vào `TaiKhoanBLL` → `HashPassword` → `TaiKhoanDAL` → `DbConnection` → `NhanVienDAL` → quay về `FrmLogin` → `Program.cs` → `FrmMain`. Chỉ cần đi 1 vòng debug này, bạn sẽ thấy mọi thứ trong suốt như kính!
3. **Bước 3 — Tự thử viết thêm 1 quy tắc nhỏ (bài test hiểu bài):**
   - Thử mở [TaiKhoanBLL.cs](../ComputerStore.BLL/Services/TaiKhoanBLL.cs) và tự thêm 1 kiểm tra nhỏ (ví dụ: *Tên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới*, hoặc *khi mật khẩu mới nhập vào trùng với mật khẩu cũ thì thông báo*). Nếu bạn tự viết được vào đúng tầng BLL mà không cần sửa DAL hay GUI → bạn đã thực sự làm chủ kiến trúc 4 tầng!
4. **Bước 4 — Luyện trả lời nhanh 5 câu hỏi xoáy của giảng viên:**
   - *"Tại sao mật khẩu `'123456'` trong DB lại dài 64 ký tự? Không băm có sao không?"* → Xem mục **1.4**.
   - *"Không lưu mật khẩu MySQL trong code thì app kết nối kiểu gì?"* → Xem mục **2.2**.
   - *"Chống SQL Injection ở dòng code nào? Nếu cộng chuỗi thì bị hack thế nào?"* → Xem mục **3.2**.
   - *"Tại sao khi Sửa nhân viên giữ nguyên SĐT cũ lại không bị báo trùng?"* → Xem mục **3.4 (ý 1)**.
   - *"Tại sao `Program.cs` lại có `while (true)` mà không dùng `this.Hide()` trên `FrmLogin`?"* → Xem mục **5.1**.
