using System.ComponentModel.DataAnnotations;

namespace HelpLife.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio.")]
        public string User { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio.")]
        public string Password { get; set; }
    }
}