using System.Text;

namespace Ecommerce.Helpers
{
    public class MyTool
    {
        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                // 1. Kiểm tra đầu vào
                if (Hinh == null || Hinh.Length == 0)
                {
                    return string.Empty;
                }

                // 2. Tạo tên file mới để tránh trùng lặp
                // Ví dụ: samsung.jpg -> samsung_638321321.jpg
                var fileName = Path.GetFileNameWithoutExtension(Hinh.FileName);
                var extension = Path.GetExtension(Hinh.FileName);
                var newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";

                // 3. Xác định đường dẫn thư mục chứa ảnh
                var rootPath = Directory.GetCurrentDirectory();
                var folderPath = Path.Combine(rootPath, "wwwroot", "Hinh", folder);

                // 4. QUAN TRỌNG: Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // 5. Đường dẫn file đầy đủ
                var fullPath = Path.Combine(folderPath, newFileName);

                // 6. Lưu file (Dùng FileMode.Create để ghi đè nếu lỡ trùng, dù Ticks rất khó trùng)
                using (var myfile = new FileStream(fullPath, FileMode.Create))
                {
                    Hinh.CopyTo(myfile);
                }

                // 7. Trả về tên file MỚI để lưu vào Database
                return newFileName;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi tại đây nếu cần
                Console.WriteLine($"Lỗi Upload Hình: {ex.Message}");
                return string.Empty;
            }
        }

        public static string GenerateRandomKey(int length = 5)
        {
            var pattern = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!";
            var rd = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }
    }
}
