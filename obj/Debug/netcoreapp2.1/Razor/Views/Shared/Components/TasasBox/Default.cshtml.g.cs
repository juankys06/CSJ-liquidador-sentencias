#pragma checksum "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17c07ffebfe3fcf3c67c9a014a360b5837f73bc0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_TasasBox_Default), @"mvc.1.0.view", @"/Views/Shared/Components/TasasBox/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/TasasBox/Default.cshtml", typeof(AspNetCore.Views_Shared_Components_TasasBox_Default))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\_ViewImports.cshtml"
using liquidador_web;

#line default
#line hidden
#line 2 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\_ViewImports.cshtml"
using liquidador_web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17c07ffebfe3fcf3c67c9a014a360b5837f73bc0", @"/Views/Shared/Components/TasasBox/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e74feaf03a43be7ba35bb5a7f593299181ba98b8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_TasasBox_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Datasainte>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(25, 175, true);
            WriteLiteral("\r\n<div class=\"cuadro-tasas center-block\">\r\n    <fieldset>\r\n        <legend>Tasas:</legend>\r\n        <table class=\"table table-responsive\">\r\n            \r\n            <tbody>\r\n");
            EndContext();
#line 9 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml"
                 foreach (Datasainte element in @Model)
                {

#line default
#line hidden
            BeginContext(276, 49, true);
            WriteLiteral("                <tr>\r\n                    <td><b>");
            EndContext();
            BeginContext(326, 16, false);
#line 12 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml"
                      Write(element.TipoTasa);

#line default
#line hidden
            EndContext();
            BeginContext(342, 13, true);
            WriteLiteral(": </b><strong");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 355, "\"", 437, 1);
#line 12 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml"
WriteAttributeValue("", 363, element.VigenteHasta < DateTime.Today ? "color: red;" : "color: green;", 363, 74, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(438, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(440, 44, false);
#line 12 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml"
                                                                                                                                        Write(string.Format("{0:0.##}", element.ValorTasa));

#line default
#line hidden
            EndContext();
            BeginContext(484, 9, true);
            WriteLiteral("</strong>");
            EndContext();
            BeginContext(495, 48, false);
#line 12 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml"
                                                                                                                                                                                               Write(element.VigenteHasta < DateTime.Today ? "*" : "");

#line default
#line hidden
            EndContext();
            BeginContext(544, 51, true);
            WriteLiteral("</td>\r\n                    <td class=\"fecha-tasas\">");
            EndContext();
            BeginContext(596, 43, false);
#line 13 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml"
                                       Write(element.VigenteDesde.ToString("dd/MM/yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(639, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 15 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Shared\Components\TasasBox\Default.cshtml"
                }

#line default
#line hidden
            BeginContext(688, 162, true);
            WriteLiteral("            </tbody>\r\n            <tfoot><tr><td>*<strong>Tasa no se encuentra Actualizada</strong></td></tr></tfoot>\r\n        </table>\r\n\r\n    </fieldset>\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Datasainte>> Html { get; private set; }
    }
}
#pragma warning restore 1591