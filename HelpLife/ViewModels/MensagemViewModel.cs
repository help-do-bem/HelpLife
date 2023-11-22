using Newtonsoft.Json;

namespace HelpLife.ViewModels
{
    public enum TipoMensagem
    {
        Informacao,
        Erro
    }

    public class MensagemViewModel
    {
        public TipoMensagem Tipo { get; set; }
        public string Texto { get; set; }
        public MensagemViewModel(string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            Tipo = tipo;
            Texto = mensagem;
        }

        public static string Serialize(string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            var mensagemModel = new MensagemViewModel(mensagem, tipo);
            return JsonConvert.SerializeObject(mensagemModel);
        }

        public static MensagemViewModel Deserializar(string mensagemString)
        {
            return JsonConvert.DeserializeObject<MensagemViewModel>(mensagemString);
        }
    }
}