using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebProject.Models
{
    public class MemberTel
    {
        [Key]
        // 因為Model必須要有主鍵，所以弄個流水號當主鍵
        public int Index { get; set; } = 0;

        [Display(Name = "會員電話")]
        [StringLength(15, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string TelNumber { get; set; } = null!;

        [ForeignKey("Member")]
        public string MemberID { get; set; } = null!;

        [JsonIgnore]
        public virtual Member? Member { get; set; } = null!;

    }
}
