using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroWebsite.Models
{
    public class SignInModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
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