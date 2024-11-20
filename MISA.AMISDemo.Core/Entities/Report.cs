namespace MISA.AMISDemo.Core.Entities
{
    public class Report
    {
        // ID duy nhất của nhân viên
        public int? report_id { get; set; }

        // Mã du khách (có thể dùng để phân loại).
        public string? report_code { get; set; }

        // Tên của nhân viên
        public string? report_name { get; set; }

        // Ngày sinh của nhân viên
        public DateTime? report_birth { get; set; }

        // Giới tính của nhân viên
        public string? report_gender { get; set; }

        // Địa chỉ email của nhân viên
        public string? report_email { get; set; }

        // Lương của nhân viên
        public string? report_salary { get; set; }
    }
}
