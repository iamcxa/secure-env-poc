using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class EncryptPostDataViewModel
    {
        [Required(ErrorMessage = "data invalid.")]
        public string data { get; set; }

    }

    public class EncryptPostDataViewModel<Obj>
    {
        [Required(ErrorMessage = "data invalid.")]
        public Obj data { get; set; }
        [Required(ErrorMessage = "data invalid.")]
        public List<string> Scope { get; set; }
    }
}
