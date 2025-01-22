using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    //商品類型
    public class ProductType
    {
        [Key]
        [Display(Name = "類型代碼")]
        [StringLength(1, MinimumLength = 1)]
        public string TypeID { get; set; } = null!;
        [Display(Name = "類型名稱 ")]
        [Required(ErrorMessage = "必填")]
        [StringLength(10, MinimumLength = 1)]
        public string TypeName { get; set; } = null!;

        public virtual List<Product>? Product { get; set; }
    }
}
