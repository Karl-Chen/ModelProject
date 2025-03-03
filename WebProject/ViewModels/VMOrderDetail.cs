using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels
{
    public class VMOrderDetail
    {
        public List<VMOrderCarItem>? item { get; set; }

        [Display(Name = "加值包裝")]
        public string isFix { get; set; } = "0";

        public string sendWay { get; set; } = "1";

        public int Total = 0;

        [Display(Name = "訂單狀態")]
        public string OrderStatusID { get; set; } = null!;

        [Display(Name = "下單時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //[HiddenInput]
        public DateTime OrderDate { get; set; }

        [Display(Name = "出貨日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        //[HiddenInput]
        public DateTime? ShippingDate { get; set; }

        [Display(Name = "收貨地址")]
        public string? ShippingAddr { get; set; }

    }
}
