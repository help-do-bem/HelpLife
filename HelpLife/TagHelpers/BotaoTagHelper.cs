using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Helplife.TagHelpers
{
    public class BotaoTagHelper : TagHelper
    {
        public string? Texto { get; set; }
        public string? Tipo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Tipo == "Alerta")
            {
                output.TagName = "button";
                output.Attributes.SetAttribute("class", "btn btn-danger");
                output.Content.SetContent(string.IsNullOrEmpty(Texto) ? "Cadastrar" : Texto);
                return;
            }
            output.TagName = "button";
            output.Attributes.SetAttribute("class", "btn btn-primary");
            output.Content.SetContent(string.IsNullOrEmpty(Texto) ? "Cadastrar" : Texto);
        }
    }
}