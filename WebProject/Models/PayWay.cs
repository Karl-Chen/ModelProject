using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class PayWay
    {
        [Key]
        [Display(Name = "付款方式代碼")]
        [StringLength(1, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string PayWayID { get; set; } = null!;

        [Display(Name = "付款方式代碼")]
        [StringLength(10, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string PayWayName { get; set; } = null!;

        public virtual List<Order>? Orders { get; set; }
    }
}
