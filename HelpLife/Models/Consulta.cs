namespace HelpLife.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public string Diagnostico { get; set; }
        public string Prescricao { get; set; }
        public string MedicoId { get; set; }
        public Medico Medico { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
