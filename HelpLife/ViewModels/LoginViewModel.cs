using System.ComponentModel.DataAnnotations;

namespace HelpLife.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio.")]
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio.")]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Mater-me conectado")]
        public bool Remember { get; set; }

        public string ReturnUrl { get; set; }
    }
}