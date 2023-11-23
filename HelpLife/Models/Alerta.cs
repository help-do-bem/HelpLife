namespace HelpLife.Models
{
    public class Alerta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DtCriacao { get; set; }
        public int HistoricoId { get; set; }
        public Historico Historico { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
    }
}