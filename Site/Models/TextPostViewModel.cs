using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class TextPostViewModel
    {
        [Required(ErrorMessage = "Text required.")]
        public string text { get; set; }
    }
}
