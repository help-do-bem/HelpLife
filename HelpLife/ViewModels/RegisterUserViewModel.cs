using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpLife.ViewModels
{
    public class RegisterUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime? DataNasc { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [MaxLength(13, ErrorMessage = "O Tamanho maximo do campo {0} é {1} caracteres.")]
        public string CRM { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string NomeUsuario{ get; set; }

        [Display(Name = "Número de Telefone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [StringLength(11, ErrorMessage = "O Campo {0} precisa ter {1} digitos.")]
        public string Telefone { get; set; }

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