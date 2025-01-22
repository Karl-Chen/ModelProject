using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    public class MemberAcc
    {
        [Key]
        [Display(Name = "會員帳號")]
        [RegularExpression("^[a-zA-Z0-9]{6,20}$", ErrorMessage = "帳號必須是6到20個字母或數字組成")]
        public string Account { get; set; } = null!;

        [Display(Name = "會員密碼")]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d]{8,20}$", ErrorMessage = "密碼必須至少包含一個大寫字母、一個小寫字母、一個數字，且長度不少於8個字元，最多20個字元")]
        public string Mima { get; set; } = null!;

        [Display(Name = "會員編號")]
        [ForeignKey("Member")]
        public string MemberID { get; set; } = null!;

        public virtual Member? Member { get; set; } = null!;
    }
}
