using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    public class Staff
    {
        [Key]
        [Display(Name = "員工編號")]
        [StringLength(6, MinimumLength = 6)]
        public string StaffID { get; set; } = null!;

        [Display(Name = "員工姓名")]
        [StringLength(20, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string Name { get; set; } = null!;

        [Display(Name = "到職日期")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "必填")]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "電話")]
        [StringLength(20, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string Phone { get; set; } = null!;

        

        [Display(Name = "角色")]
        [ForeignKey("Role")]
        public string RoleID { get; set; } = null!;
        public virtual Role? Role { get; set; }
                
        public virtual List<HandleOrder>? HandleOrder { get; set; }
        public virtual StaffAcc? StaffAcc { get; set; }
    }
}
