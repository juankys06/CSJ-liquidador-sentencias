#pragma checksum "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b8c4f015c768b8f163367d1679adbeebe58e0fa9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ConsultaTasas), @"mvc.1.0.view", @"/Views/Admin/ConsultaTasas.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/ConsultaTasas.cshtml", typeof(AspNetCore.Views_Admin_ConsultaTasas))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b8c4f015c768b8f163367d1679adbeebe58e0fa9", @"/Views/Admin/ConsultaTasas.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e74feaf03a43be7ba35bb5a7f593299181ba98b8", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_ConsultaTasas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("f_tasas"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/consulta_tasas.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 16, true);
            WriteLiteral("<div class=\"row\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 16, "\"", 96, 3);
            WriteAttributeValue("", 24, "margin-top:", 24, 11, true);
            WriteAttributeValue(" ", 35, "10px;", 36, 6, true);
#line 1 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
WriteAttributeValue(" ", 41, TempData["Deleted"] == null ? "display: none;" : "", 42, 54, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(97, 40, true);
            WriteLiteral(">\r\n    <div class=\"alert alert-success\">");
            EndContext();
            BeginContext(138, 29, false);
#line 2 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
                                Write(Html.Raw(TempData["Deleted"]));

#line default
#line hidden
            EndContext();
            BeginContext(167, 32, true);
            WriteLiteral("</div>\r\n</div>\r\n<div class=\"row\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 199, "\"", 282, 3);
            WriteAttributeValue("", 207, "margin-top:", 207, 11, true);
            WriteAttributeValue(" ", 218, "10px;", 219, 6, true);
#line 4 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
WriteAttributeValue(" ", 224, TempData["NotDeleted"] == null ? "display: none;" : "", 225, 57, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(283, 39, true);
            WriteLiteral(">\r\n    <div class=\"alert alert-danger\">");
            EndContext();
            BeginContext(323, 32, false);
#line 5 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
                               Write(Html.Raw(TempData["NotDeleted"]));

#line default
#line hidden
            EndContext();
            BeginContext(355, 147, true);
            WriteLiteral("</div>\r\n</div>\r\n<div class=\"row\">\r\n    <div class=\"col-md-9\">\r\n        <fieldset>\r\n            <legend>Criterios de Consulta</legend>\r\n            ");
            EndContext();
            BeginContext(502, 1269, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8c4f015c768b8f163367d1679adbeebe58e0fa97090", async() => {
                BeginContext(535, 229, true);
                WriteLiteral("\r\n                <div class=\"col-md-12\">\r\n                    <div class=\"form-group\">\r\n                        <label for=\"tipo\">Tipo de Tasa</label>\r\n                        <select name=\"tipo\" class=\"form-control\" required>\r\n");
                EndContext();
#line 16 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
                             foreach (var tasa in (TiposTasas[])ViewData["Tasas"])
                            {

#line default
#line hidden
                BeginContext(879, 32, true);
                WriteLiteral("                                ");
                EndContext();
                BeginContext(911, 57, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8c4f015c768b8f163367d1679adbeebe58e0fa98115", async() => {
                    BeginContext(937, 11, false);
#line 18 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
                                                    Write(tasa.Nombre);

#line default
#line hidden
                    EndContext();
                    BeginContext(948, 2, true);
                    WriteLiteral(" (");
                    EndContext();
                    BeginContext(951, 7, false);
#line 18 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
                                                                  Write(tasa.ID);

#line default
#line hidden
                    EndContext();
                    BeginContext(958, 1, true);
                    WriteLiteral(")");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 18 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
                                   WriteLiteral(tasa.ID);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(968, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 19 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
                            }

#line default
#line hidden
                BeginContext(1001, 763, true);
                WriteLiteral(@"                        </select>
                    </div>
                </div>
                <div class=""form-group"">
                    <div class=""col-md-6"">
                        <label for=""fromDate"">Desde: </label>
                        <input type=""date"" name=""fromDate"" class=""form-control"" required />
                    </div>
                    <div class=""col-md-6"">
                        <label for=""toDate"">Hasta:</label>
                        <input type=""date"" name=""toDate"" class=""form-control"" />
                    </div>
                </div>
                <div class=""row text-center"">
                    <button type=""submit"" class=""btn btn-custom"">Consultar</button>
                </div>
            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1771, 736, true);
            WriteLiteral(@"
        </fieldset>
    </div>
    <div class=""col-md-12"">
        <fieldset>
            <legend>Resultado de la Consulta</legend>
            <table id=""t_tasas"" class=""table display"" style=""width: 100%;"">
                <thead>
                    <tr>
                        <th>Consecutivo</th>
                        <th>Tipo Tasa</th>
                        <th>Vigente Desde</th>
                        <th>Vigente Hasta</th>
                        <th>Valor Tasa</th>
                        <th>Período</th>
                        <th>Resolución</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </fieldset>
    </div>
</div>

");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2524, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(2530, 72, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8c4f015c768b8f163367d1679adbeebe58e0fa914091", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#line 61 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ConsultaTasas.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2602, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591