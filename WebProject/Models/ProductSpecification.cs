using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    //商品規格
    public class ProductSpecification
    {
        [Key]
        [Display(Name = "規格代碼")]
        [StringLength(20, MinimumLength = 1)]
        public string SpecificationID { get; set; } = null!;
        [Display(Name = "規格名稱")]
        [Required(ErrorMessage = "必填")]
        [StringLength(50, MinimumLength = 1)]
        public string SpecificationName { get; set;} = null!;

        public virtual List<Product>? Products { get; set; }
    }
}
