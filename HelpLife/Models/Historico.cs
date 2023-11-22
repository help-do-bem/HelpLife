using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpLife.Models
{
    [Table("HISTORICO")]
    public class Historico
    {
        [Key]
        [Column("ID_HISTORICO")]
        public int Id { get; set; }
        [Column("TEMPERATURA")]
        public long Temperatura { get; set; }
        [Column("OXIGENIO")]
        public long Oxigenio { get; set; }
        [Column("BATIMENTOS")]
        public long Batimentos { get; set; }
        [Column("DT_MEDICAO")]
        public DateTime DataMedicao { get; set; }
        [Column("LATITUDE")]
        public long Latitude { get; set; }
        [Column("LONGITUDE")]
        public long Longitude { get; set; }
        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public IList<Alerta> Alerta { get; set; }
    }
}