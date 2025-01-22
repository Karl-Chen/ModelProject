using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    //出貨方式
    public class ShippingWay
    {
        [Key]
        [Display(Name = "出貨方式代碼")]
        [StringLength(1, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string ShippingWayID { get; set; } = null!;
        [StringLength(10, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "出貨方式")]
        public string ShippingWayName { get; set; } = null!;

        public virtual List<Order>? Order { get; set; }
    }
}
