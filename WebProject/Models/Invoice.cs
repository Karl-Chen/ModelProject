using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebProject.Models
{
    public class Invoice
    {
        [Key]
        [Display(Name = "發票號碼")]
        [StringLength(13, MinimumLength = 13)]
        public string InvoiceNo { get; set; } = null!;

        [ForeignKey("Order")]
        [Display(Name = "訂單編號")]
        [StringLength(13, MinimumLength = 13)]
        public string OrderNo { get; set; } = null!;
        [JsonIgnore]
        public virtual Order? Order { get; set; }
    }
}
