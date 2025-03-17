using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels
{
    public class VMOrderCarItem
    {
        [Display(Name = "商品圖片")]
        public string? img { get; set; }
        public string? product { get; set; }
        [Display(Name = "商品名稱")]
        public string? name { get; set; }

        [Display(Name ="售價")]
        public int price { get; set; } = 0;

        [Display(Name = "折扣")]
        public float offset { get; set; } = 0f;

        [Display(Name = "數量")]
        public int count { get; set; } = 0;

        [Display(Name = "最大數量")]
        public int MaxCount { get; set; } = 0;


    }
}
