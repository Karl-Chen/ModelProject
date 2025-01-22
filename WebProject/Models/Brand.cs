using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    //品牌廠商
    public class Brand
    {
        [Key]
        [Display(Name = "品牌商代碼")]
        [StringLength(2, MinimumLength = 2)]
        public string BrandID { get; set; } = null!;
        [Display(Name = "供應商名稱")]
        [Required(ErrorMessage = "必填")]
        [StringLength(50, MinimumLength = 1)]
        public string BrandName { get; set; } = null!;
        public virtual List<Product>? Product { get; set; }
    }
}
