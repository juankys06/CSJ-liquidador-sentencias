using Microsoft.AspNetCore.Razor.TagHelpers;

namespace liquidador_web.Extra
{
    public class EmailTagHelper: TagHelper
    {
        // Can be passed via <email mail-to="..." />. 
        // PascalCase gets translated into kebab-case.
        public string MailTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag
            output.Attributes.SetAttribute("href", "mailto:" + MailTo);
            output.Content.SetContent(MailTo);
        }
    }
}
