using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    //角色
    public class Role
    {
        [Key]
        [Display(Name = "角色編號")]
        [StringLength(1, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string RoleID { get; set; } = null!;

        [Display(Name = "角色")]
        [StringLength(20, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string RoleName { get; set; } = null!;

        public virtual List<Staff>? Staff { get; set; }
    }
}
