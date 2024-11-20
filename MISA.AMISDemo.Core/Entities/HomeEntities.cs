using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MISA.AMISDemo.Core.Entities
{
    [Table("home")]
    public class HomeEntities
    {
        [Key]
        public int? home_id { get; set; }
        // Code
        public string? home_code { get; set; }
        // Tên 
        public string? home_name { get; set; }
        // Ngày sinh 
        public string home_birth { get; set; }
        // Giới tính 
        public string? home_gender { get; set; }
        // Địa chỉ email 
        public string? home_email { get; set; }
        // Địa chỉ cư trú 
        public string? home_address { get; set; }
        // Số CMTND 
        public string? home_cmtnd { get; set; }
        // Ngày cấp
        public string home_issueDate { get; set; }
        // Nơi cấp
        public string home_issue { get; set; }
        // Vị trí 
        public int? id_location { get; set; }
        // Phòng ban 
        public int? id_department { get; set; }
        // Điện thoại di động 
        public string? home_cellularPhone { get; set; }
        // Điện thoại cố định 
        public string? home_landlinePhone { get; set; }
        // Tài khoản ngân hàng 
        public string? home_bankAccount { get; set; }
        // Tên ngân hàng 
        public int? id_bank { get; set; }
        // Chi nhánh ngân hàng 
        public int? id_branch { get; set; }
    }
}
