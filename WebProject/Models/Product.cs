using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    //商品
    public class Product
    {
        [Key]
        [Display(Name = "商品代碼")]
        [StringLength(8, MinimumLength = 8)]
        public string ProductID { get; set; } = null!;
        [Display(Name = "商品名稱")]
        [StringLength(50, ErrorMessage = "商品名稱最多50字")]
        [Required(ErrorMessage = "必填")]
        public string ProductName { get; set; } = null!;
        [Display(Name = "介紹")]
        [StringLength(500, ErrorMessage = "介紹最多500字")]
        public string Description { get; set; } = null!;
        [Display(Name = "商品圖片")]
        [StringLength(18, ErrorMessage = "商品圖檔最多18字")]
        public string Photo { get; set; } = null!;
        [Display(Name = "日幣訂價")]
        [Required(ErrorMessage = "請輸入日幣訂價")]
        [Range(1, 99999999, ErrorMessage = "請輸入日幣訂價1~99999999")]
        public int CostJP { get; set; } = 0;
        [Display(Name = "進貨匯率")]
        [Required(ErrorMessage = "請輸入進貨匯率")]
        [Range(0.01, 0.99, ErrorMessage = "請輸入匯率0.01~0.99")]
        public float CostExchangeRate { get; set; } = 0f;
        [Display(Name = "販賣匯率")]
        [Required(ErrorMessage = "請輸入販賣匯率")]
        [Range(0.01, 0.99, ErrorMessage = "請輸入匯率0.01~0.99")]
        public float PriceExchangeRage { get; set; } = 0f;
        [Display(Name = "庫存量")]
        [Required(ErrorMessage = "請輸入庫存量")]
        public int Inventory { get; set; } = 0;
        [Display(Name = "已訂購量")]
        [Required(ErrorMessage = "請輸入已訂購量")]
        public int OrderedQuantity { get; set; } = 0;

        [ForeignKey("ProductType")]
        [Display(Name = "類型代碼")]
        public string ProductTypeID { get; set; } = null!;
        public virtual ProductType? ProductType { get; set; }

        [ForeignKey("ProductSpecification")]
        [Display(Name = "規格代碼")]
        public string ProductSpecificationID { get; set; } = null!;
        public virtual ProductSpecification? ProductSpecification { get; set; }

        [ForeignKey("Brand")]
        [Display(Name = "品牌商代碼")]
        public string BrandID { get; set; } = null!;
        public virtual Brand? Brand { get; set; }

        [ForeignKey("Supplier")]
        [Display(Name = "供應商代碼")]
        public string SupplierID { get; set; } = null!;
        public virtual Supplier? Supplier { get; set; }
    }
}
