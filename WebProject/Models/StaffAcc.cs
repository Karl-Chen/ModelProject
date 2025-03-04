using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class StaffAcc
    {
        [Key]
        [Display(Name = "員工帳號")]
        [RegularExpression("^[a-zA-Z0-9]{6,20}$", ErrorMessage = "帳號必須是6到20個字母或數字組成")]
        public string Account { get; set; } = null!;

        [Display(Name = "員工密碼")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d]{8,20}$", ErrorMessage = "密碼必須至少包含一個大寫字母、一個小寫字母、一個數字，且長度不少於8個字元，最多20個字元")]
        public string Mima { get; set; } = null!;

        [Display(Name = "員工編號")]
        [ForeignKey("Member")]
        public string StaffID { get; set; } = null!;

        public virtual Staff? Staff { get; set; } = null!;
    }
}
