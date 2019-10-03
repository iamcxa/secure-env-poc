using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class UserUpdateViewModel
    {

        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone required.")]
        public string Telephone { get; set; }
    }
}
