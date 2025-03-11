using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels
{
    public class VMOrderDetail
    {
        public List<VMOrderCarItem>? item { get; set; }

        [Display(Name = "包裝費用")]
        public string isFix { get; set; } = "0";

        [Display(Name = "出貨方式")]
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

        [Display(Name = "處理員工")]
        public string?  HandleMember{ get; set; }

        [Display(Name = "處理日期")]
        public DateTime? HandleDate { get; set; }

        [Display(Name = "發票號碼")]
        public string? InvoiceID { get; set; }

        [Display(Name = "收貨人姓名")]
        public string? OrderName { get; set; }

        [Display(Name = "收貨人電話")]
        public string? OrderPhone { get; set; }
    }
}
