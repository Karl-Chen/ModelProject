using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    public class Member
    {
        [Key]
        [JsonProperty("MemberID")]
        [Display(Name = "會員代碼")]
        [StringLength(8, MinimumLength = 8)]
        public string MemberID { get; set; } = null!;

        [Display(Name = "會員姓名")]
        [JsonProperty("Name")]
        [StringLength(20, ErrorMessage = "姓名最多20字")]
        [Required(ErrorMessage = "必填")]
        public string Name { get; set; } = null!;

        [Display(Name = "地址")]
        [JsonProperty("Address")]
        [StringLength(200, ErrorMessage = "地址最多200字")]
        public string? Address { get; set; }

        [Display(Name = "Email")]
        [JsonProperty("Email")]
        [StringLength(100, ErrorMessage = "信箱最多100個字")]
        [EmailAddress(ErrorMessage = "E-mail格式錯誤")]
        [Required(ErrorMessage = "必填")]
        public string Email { get; set; } = null!;

        //會員點數
        [JsonProperty("Point")]
        public int Point { get; set; } = 0;

        public virtual List<MemberTel>? MemberTel { get; set; }

        public virtual MemberAcc? MemberAcc { get; set; }
    }
}
