namespace MISA.AMISDemo.Core.Entities
{
    public class Staff
    {
        // ID duy nhất của du khách.
        public int? staff_id { get; set; }

        // Mã du khách (có thể dùng để phân loại).
        public string? staff_code { get; set; }

        // Tên của du khách.
        public string? staff_name { get; set; }

        // Ngày sinh của du khách.
        public DateTime? staff_birth { get; set; }

        // Giới tính của du khách.
        public string? staff_gender { get; set; }

        // Địa chỉ email của du khách.
        public string? staff_email { get; set; }

        // Địa chỉ cư trú của du khách.
        public string? staff_address { get; set; }
    }
}
