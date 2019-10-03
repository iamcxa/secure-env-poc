using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Account required.")]
        //[RegularExpression("^[a-zA-Z0-6]+$", ErrorMessage = "Account only allow English characters and numbers.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Password required.")]
        //[RegularExpression("^[a-zA-Z0-6]+$", ErrorMessage = "Password only allow English characters and numbers.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone required.")]
        public string Telephone { get; set; }
    }
}
