using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelpLife.Models
{
    [Table("ENDERECO")]
    public class Endereco
    {
        [Key]
        [Column("ID_ENDERECO")]
        public int Id { get; set; }
        [Column("CEP"), Required, MaxLength(8)]
        public string CEP { get; set; }
        [Column("ESTADO"), Required, MaxLength(2)]
        public string Estado { get; set; }
        [Column("CIDADE"), Required, MaxLength(100)]
        public string Cidade { get; set; }
        [Column("LOGRADOURO"), Required, MaxLength(100)]
        public string Logradouro { get; set; }
        [Column("NUMERO"), Required, MaxLength(5)]
        public string Numero { get; set; }

        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
