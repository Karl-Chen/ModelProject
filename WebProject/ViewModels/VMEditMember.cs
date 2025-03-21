﻿using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels
{
    public class VMEditMember
    {
        [Key]
        [Display(Name = "會員代碼")]
        [StringLength(8, MinimumLength = 8)]
        public string MemberID { get; set; } = null!;

        [Display(Name = "會員姓名")]
        [StringLength(20, ErrorMessage = "姓名最多20字")]
        [Required(ErrorMessage = "必填")]
        public string Name { get; set; } = null!;

        [Display(Name = "地址")]
        [StringLength(200, ErrorMessage = "地址最多200字")]
        public string? Address { get; set; }

        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "信箱最多100個字")]
        [EmailAddress(ErrorMessage = "E-mail格式錯誤")]
        [Required(ErrorMessage = "必填")]
        public string Email { get; set; } = null!;

        [Display(Name = "會員電話")]
        [StringLength(15, MinimumLength = 1)]
        [Required(ErrorMessage = "必填")]
        public string TelNumber { get; set; } = null!;
    }
}
