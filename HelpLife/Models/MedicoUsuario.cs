using System.ComponentModel.DataAnnotations.Schema;

namespace HelpLife.Models
{
    [Table("Medico_Usuario")]
    public class MedicoUsuario
    {
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set;}
    }
}
