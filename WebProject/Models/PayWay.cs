using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual List<Order>? Orders { get; set; }
    }
}
