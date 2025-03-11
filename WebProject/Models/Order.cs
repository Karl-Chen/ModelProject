using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "訂單編號")]
        [StringLength(13, MinimumLength = 13)]
        public string OrderNo { get; set; } = null!;

        [Required(ErrorMessage = "請輸入下單日期")]
        [Display(Name = "下單時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //[HiddenInput]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "請輸入出貨日期")]
        [Display(Name = "出貨日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        //[HiddenInput]
        public DateTime? ShippingDate { get; set; }

        [Display(Name = "加值包裝")]
        public bool IsGoodPackage { get; set; } = false;

        [Display(Name = "收貨地址")]
        [StringLength(200, ErrorMessage = "地址最多200字")]
        public string? ShippingAddr { get; set; }

        [Display(Name = "收貨人姓名")]
        [StringLength(200, ErrorMessage = "收貨人姓名最多10字")]
        public string? OrderName { get; set; }

        [Display(Name = "收貨人電話")]
        [StringLength(200, ErrorMessage = "收貨人電話最多20字")]
        public string? OrderPhone { get; set; }

        [Display(Name = "付款方式")]
        [ForeignKey("PayWay")]
        public string PayWayID { get; set; } = null!;
        public virtual PayWay? PayWay { get; set; }
        
        public virtual Invoice? Invoice { get; set; }
        
        public virtual List<HandleOrder>? HandleOrder { get; set; }

        [Display(Name = "訂單狀態")]
        [ForeignKey("Ordertatus")]
        public string OrdertatusID { get; set; } = null!;
        public virtual Ordertatus? Ordertatus { get; set; }

        [Display(Name = "會員編號")]
        [ForeignKey("Member")]
        [StringLength(8, MinimumLength = 8)]
        public string MemberID { get; set; } = null!;
        public virtual Member? Member { get; set; }

        [Display(Name = "出貨方式")]
        [ForeignKey("ShippingWay")]
        public string ShippingWayID { get; set; } = null!;
        public virtual ShippingWay? ShippingWay { get; set; }
    }
}
