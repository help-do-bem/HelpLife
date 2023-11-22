namespace HelpLife.Models
{
    public class Alerta
    {
        public int Id { get; set; }
        public string Decricao { get; set; }
        public DateTime DtCriacao { get; set; }
        public int HistoricoId { get; set; }
        public Historico Historico { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
    }
}