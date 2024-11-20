namespace MISA.AMISDemo.Core.Entities
{
    public class Setting
    {
        // ID duy nhất của du khách.
        public int? setting_id { get; set; }

        // Mã du khách (có thể dùng để phân loại).
        public string? setting_code { get; set; }

        // Tên của du khách.
        public string? setting_name { get; set; }

        // Ngày sinh của du khách.
        public DateTime? setting_birth { get; set; }

        // Giới tính của du khách.
        public string? setting_gender { get; set; }

        // Địa chỉ email của du khách.
        public string? setting_email { get; set; }

        // Địa chỉ cư trú của du khách.
        public string? setting_address { get; set; }
    }
}
