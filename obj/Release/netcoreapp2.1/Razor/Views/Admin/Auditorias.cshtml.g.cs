#pragma checksum "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "673e56404c3730a192afb7b6523f2fcef2f8923f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Auditorias), @"mvc.1.0.view", @"/Views/Admin/Auditorias.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Auditorias.cshtml", typeof(AspNetCore.Views_Admin_Auditorias))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"673e56404c3730a192afb7b6523f2fcef2f8923f", @"/Views/Admin/Auditorias.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e74feaf03a43be7ba35bb5a7f593299181ba98b8", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Auditorias : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<liquidador_web.Extra.Paginador<Auditoria>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Auditorias", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Auditoria", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onclick", new global::Microsoft.AspNetCore.Html.HtmlString("return popupCenter(this)"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::liquidador_web.Extra.EmailTagHelper __liquidador_web_Extra_EmailTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(50, 79, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-12 formulario-auditoria\">\r\n        ");
            EndContext();
            BeginContext(129, 1770, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "673e56404c3730a192afb7b6523f2fcef2f8923f5441", async() => {
                BeginContext(192, 138, true);
                WriteLiteral("\r\n            <div class=\"form-group\">\r\n                <label for=\"desde\">Desde:</label>\r\n                <input type=\"date\" name=\"desde\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 330, "\"", 362, 1);
#line 8 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
WriteAttributeValue("", 338, ViewData["actualDesde"], 338, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(363, 182, true);
                WriteLiteral(" class=\"form-control\" />\r\n            </div>\r\n            <div class=\"form-group\">\r\n                <label for=\"hasta\">Hasta:</label>\r\n                <input type=\"date\" name=\"hasta\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 545, "\"", 577, 1);
#line 12 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
WriteAttributeValue("", 553, ViewData["actualHasta"], 553, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(578, 190, true);
                WriteLiteral(" class=\"form-control\" />\r\n            </div>\r\n            <div class=\"form-group\">\r\n                <label for=\"usuario\">Usuario: </label>\r\n                <input type=\"email\" name=\"usuario\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 768, "\"", 802, 1);
#line 16 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
WriteAttributeValue("", 776, ViewData["actualUsuario"], 776, 26, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(803, 154, true);
                WriteLiteral(" class=\"form-control\" />\r\n            </div>\r\n            <div class=\"form-group\">\r\n                <label for=\"modulo\">Módulo: </label>\r\n                ");
                EndContext();
                BeginContext(958, 830, false);
#line 20 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
           Write(Html.DropDownList("modulo", new SelectListItem[] {
                    new SelectListItem { Text = "", Value = "", Selected = string.IsNullOrEmpty(ViewData["actualModulo"] as string) ? true : false },
                    new SelectListItem { Text = "Liquidaciones", Value = "Liquidaciones", Selected = ViewData["actualModulo"] != null && ViewData["actualModulo"].Equals("Liquidaciones") ? true : false },
                    new SelectListItem { Text = "Usuarios", Value = "Usuarios", Selected = ViewData["actualModulo"] != null && ViewData["actualModulo"].Equals("Usuarios") ? true : false },
                    new SelectListItem { Text = "Tasas", Value = "Tasas", Selected = ViewData["actualModulo"] != null && ViewData["actualModulo"].Equals("Tasas") ? true : false },
                }, new { @class = "form-control" }));

#line default
#line hidden
                EndContext();
                BeginContext(1788, 104, true);
                WriteLiteral("\r\n            </div>\r\n            <button type=\"submit\" class=\"btn btn-custom\">Buscar</button>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1899, 315, true);
            WriteLiteral(@"
    </div>
    <div class=""col-md-12"">
        <table class=""table display"">
            <thead>
                <tr>
                    <th>Fecha</th>
                    <th>Usuario</th>
                    <th>Módulo</th>
                    <th>Evento</th>
                    <th>Descripción</th>
");
            EndContext();
            BeginContext(2292, 66, true);
            WriteLiteral("                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
            EndContext();
#line 44 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                 foreach (Auditoria elemento in Model)
                {

#line default
#line hidden
            BeginContext(2433, 54, true);
            WriteLiteral("                    <tr>\r\n                        <td>");
            EndContext();
            BeginContext(2488, 14, false);
#line 47 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                       Write(elemento.fecha);

#line default
#line hidden
            EndContext();
            BeginContext(2502, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2537, 60, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("email", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "673e56404c3730a192afb7b6523f2fcef2f8923f11951", async() => {
                BeginContext(2573, 16, false);
#line 48 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                                                          Write(elemento.usuario);

#line default
#line hidden
                EndContext();
            }
            );
            __liquidador_web_Extra_EmailTagHelper = CreateTagHelper<global::liquidador_web.Extra.EmailTagHelper>();
            __tagHelperExecutionContext.Add(__liquidador_web_Extra_EmailTagHelper);
            BeginWriteTagHelperAttribute();
#line 48 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                                WriteLiteral(elemento.usuario);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __liquidador_web_Extra_EmailTagHelper.MailTo = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("mail-to", __liquidador_web_Extra_EmailTagHelper.MailTo, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2597, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2633, 15, false);
#line 49 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                       Write(elemento.modulo);

#line default
#line hidden
            EndContext();
            BeginContext(2648, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2684, 15, false);
#line 50 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                       Write(elemento.evento);

#line default
#line hidden
            EndContext();
            BeginContext(2699, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(2734, 104, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "673e56404c3730a192afb7b6523f2fcef2f8923f14691", async() => {
                BeginContext(2823, 11, true);
                WriteLiteral("Ver Detalle");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 51 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                                                        WriteLiteral(elemento.ID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2838, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
            BeginContext(2988, 27, true);
            WriteLiteral("                    </tr>\r\n");
            EndContext();
#line 54 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                }

#line default
#line hidden
            BeginContext(3034, 40, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n");
            EndContext();
#line 57 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
          
            var prevDisabled = !Model.HasPreviousPage ? "none" : "initial";
            var nextDisabled = !Model.HasNextPage ? "none" : "initial";
        

#line default
#line hidden
            BeginContext(3247, 10, true);
            WriteLiteral("\r\n        ");
            EndContext();
            BeginContext(3257, 398, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "673e56404c3730a192afb7b6523f2fcef2f8923f17997", async() => {
                BeginContext(3619, 32, true);
                WriteLiteral("\r\n            Anterior\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pageNumber", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 63 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                      WriteLiteral(Model.PageIndex - 1);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 64 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                 WriteLiteral(ViewData["actualDesde"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["desde"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-desde", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["desde"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 65 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                 WriteLiteral(ViewData["actualHasta"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["hasta"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-hasta", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["hasta"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 66 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                  WriteLiteral(ViewData["actualModulo"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["modulo"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-modulo", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["modulo"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 67 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                   WriteLiteral(ViewData["actualUsuario"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["usuario"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-usuario", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["usuario"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "style", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3595, "display:", 3595, 8, true);
#line 68 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
AddHtmlAttributeValue(" ", 3603, prevDisabled, 3604, 13, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3655, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 71 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
         for (int i = 1; i < Model.TotalPages + 1; i++) {

#line default
#line hidden
            BeginContext(3716, 12, true);
            WriteLiteral("            ");
            EndContext();
            BeginContext(3728, 384, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "673e56404c3730a192afb7b6523f2fcef2f8923f23925", async() => {
                BeginContext(4107, 1, false);
#line 77 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                                                                     Write(i);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pageNumber", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 72 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                                                 WriteLiteral(i);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 73 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                     WriteLiteral(ViewData["actualDesde"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["desde"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-desde", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["desde"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 74 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                     WriteLiteral(ViewData["actualHasta"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["hasta"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-hasta", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["hasta"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 75 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                      WriteLiteral(ViewData["actualModulo"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["modulo"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-modulo", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["modulo"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 76 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                       WriteLiteral(ViewData["actualUsuario"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["usuario"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-usuario", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["usuario"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "style", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4054, "font-weight:", 4054, 12, true);
#line 77 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
AddHtmlAttributeValue(" ", 4066, i == Model.PageIndex ? "bold" : "", 4067, 37, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4112, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 78 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
        }

#line default
#line hidden
            BeginContext(4125, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(4133, 399, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "673e56404c3730a192afb7b6523f2fcef2f8923f29996", async() => {
                BeginContext(4495, 33, true);
                WriteLiteral("\r\n            Siguiente\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pageNumber", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 80 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                      WriteLiteral(Model.PageIndex + 1);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 81 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                 WriteLiteral(ViewData["actualDesde"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["desde"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-desde", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["desde"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 82 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                 WriteLiteral(ViewData["actualHasta"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["hasta"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-hasta", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["hasta"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 83 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                  WriteLiteral(ViewData["actualModulo"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["modulo"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-modulo", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["modulo"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 84 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
                   WriteLiteral(ViewData["actualUsuario"]);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["usuario"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-usuario", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["usuario"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "style", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4471, "display:", 4471, 8, true);
#line 85 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\Auditorias.cshtml"
AddHtmlAttributeValue(" ", 4479, nextDisabled, 4480, 13, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4532, 24, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(4573, 462, true);
                WriteLiteral(@"
    <script>
        function popupCenter(link) {
            var left = (screen.width / 2) - (720 / 2);
            var top = (screen.height / 2) - (500 / 2);

            var w = window.open(link.href, link.target || '_blank', 'menubar=no, toolbar=no, location=no, directories=no, status=no, scrollbars=no, resizable=no, dependent, width=720, height=500, left=' + left + ', top=' + top);
            return w ? false : true;
        }
    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<liquidador_web.Extra.Paginador<Auditoria>> Html { get; private set; }
    }
}
#pragma warning restore 1591