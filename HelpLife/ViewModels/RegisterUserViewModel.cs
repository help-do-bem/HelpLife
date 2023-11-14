using System.ComponentModel.DataAnnotations;

namespace HelpLife.ViewModels
{
    public class RegisterUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string NomeUsuario{ get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [MaxLength(16, ErrorMessage = "O Tamanho maximo do campo {0} é {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O Tamanho minimo do campo {0} é {1} caracteres.")]
        public string Senha { get; set; }

        [Display(Name = "Confirmação de Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio.")]
        [MaxLength(16, ErrorMessage = "O Tamanho maximo do campo {0} é {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O Tamanho minimo do campo {0} é {1} caracteres.")]
        [Compare(nameof(Senha), ErrorMessage = "As senha estão diferentes.")]
        public string ConfSenha { get; set; }
    }
}