using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpLife.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int Id { get; set; }
        [Column("NOME")]
        public string Nome { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("DATA_NASC"), DataType(DataType.Date)]
        public DateTime DataNasc { get; set; }
        [Column("TELEFONE")]
        public string Telefone { get; set; }
        [Column("SENHA")]
        public string Senha { get; set; }

        public IList<MedicoUsuario> MedicosUsuarios { get; set; }
        public virtual IList<Endereco> Endereco { get; set; }
        public virtual IList<Historico> Historicos { get; set; }
        public IList<Consulta> Consultas { get; set; }
    }
}