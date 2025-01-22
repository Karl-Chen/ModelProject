using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    //供應商資料表
    public class Supplier
    {
        [Key]
        [Display(Name = "供應商代碼")]
        [StringLength(2, MinimumLength = 2)]
        public string SupplierID { get; set; } = null!;
        [Display(Name = "供應商名稱")]
        [Required(ErrorMessage = "必填")]
        [StringLength(50, MinimumLength = 1)]
        public string SupplierName { get; set;} = null!;
        [Display(Name = "聯絡人")]
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 1)]
        public string ContactWindow { get; set; } = null!;
        [Display(Name = "聯絡電話")]
        [Required(ErrorMessage = "必填")]
        [StringLength(15, MinimumLength = 1)]
        public string Moblie { get; set; } = null!;
        [Display(Name = "地址")]
        [Required(ErrorMessage = "必填")]
        [StringLength(200, MinimumLength = 1)]
        public string Addr { get; set; } = null!;

        public virtual List<Product>? Product { get; set; }
    }
}
