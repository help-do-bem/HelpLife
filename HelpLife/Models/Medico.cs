using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpLife.Models
{
    [Table("MEDICO")]
    public class Medico
    {
        [Key]
        [Column("ID_MEDICO")]
        public int Id { get; set; }
        [Column("NOME")]
        public string Nome { get; set; }
        [Column("DATA_NASC")]
        public DateTime DataNasc { get; set; } 
        public string CRM { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public IList<MedicoUsuario> MedicosUsuarios { get; set; }
        public IList<Consulta> Consultas { get; set; }
        public IList<Alerta> Alerta { get; set; }
    }
}
