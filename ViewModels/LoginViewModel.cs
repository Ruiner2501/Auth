using System;
using System.ComponentModel.DataAnnotations;


namespace Auth.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        

        ///[Display(Name = "Remember me")]
        ///public bool RememberMe { get; set; }
       


        public string ReturnUrl { get; set; }
    }
}
