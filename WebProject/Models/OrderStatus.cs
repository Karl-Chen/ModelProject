using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebProject.Models
{
    public class Ordertatus
    {
        [Key]
        [Display(Name = "訂單狀態代碼")]
        [StringLength(2, MinimumLength = 2)]
        [Required(ErrorMessage = "必填")]
        public string OrdertatusID { get; set; } = null!;
        [StringLength(10, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        [Display(Name = "訂單狀態")]
        public string OrdertatusName { get; set; } = null!;

        [JsonIgnore]
        public virtual List<Order>? Orders { get; set; }
    }
}
