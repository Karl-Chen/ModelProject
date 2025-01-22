using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class HandleOrder
    {
        [Key]
        [Column(Order =2)]
        [ForeignKey("Order")]
        [Display(Name = "訂單編號")]
        [StringLength(13, MinimumLength = 13)]
        public string OrderNo { get; set; } = null!;
        public virtual Order? Order { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Staff")]
        [Display(Name = "員工編號")]
        [StringLength(6, MinimumLength = 6)]
        public string StaffID { get; set; } = null!;
        public virtual Staff? Staff { get; set; }


        [Display(Name = "處理日期")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "必填")]
        public DateTime HandleTime { get; set; }
    }
}
