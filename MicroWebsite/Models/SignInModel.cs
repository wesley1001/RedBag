using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroWebsite.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }

    public class SignUpModel : SignInModel
    {
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string LegalPersonName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}