using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    //訂單明細
    public class OrderDetail
    {
        [ForeignKey("Order")]
        [Display(Name = "訂單編號")]
        [StringLength(13, MinimumLength = 13)]
        public string OrderNo { get; set; } = null!;

        [ForeignKey("Product")]
        [Display(Name = "產品代碼")]
        [StringLength(8, MinimumLength = 8)]
        public string ProductID { get; set; } = null!;

        [Display(Name = "折扣")]
        [Range(0, 1, ErrorMessage = "請輸入折扣")]
        public float Off { get; set; } = 0f;

        [Display(Name = "訂購數量")]
        [Range(1, 99, ErrorMessage = "請輸入訂單商品數量")]
        public int Vol { get; set; } = 1;

        public virtual Order? Order { get; set; }
        public virtual Product? Products { get; set; }
    }
}
