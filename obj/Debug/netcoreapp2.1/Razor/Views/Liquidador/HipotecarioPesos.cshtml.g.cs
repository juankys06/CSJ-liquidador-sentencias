#pragma checksum "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\HipotecarioPesos.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04a1d678603c9c7d32fa01e169da7c2c1af0f601"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Liquidador_HipotecarioPesos), @"mvc.1.0.view", @"/Views/Liquidador/HipotecarioPesos.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Liquidador/HipotecarioPesos.cshtml", typeof(AspNetCore.Views_Liquidador_HipotecarioPesos))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04a1d678603c9c7d32fa01e169da7c2c1af0f601", @"/Views/Liquidador/HipotecarioPesos.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e74feaf03a43be7ba35bb5a7f593299181ba98b8", @"/Views/_ViewImports.cshtml")]
    public class Views_Liquidador_HipotecarioPesos : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Form_SearchProcess", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frm_hipotecario"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Liquidador", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DescargaExcel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("descargarExcel"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/procesos.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/hipotecario.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "04a1d678603c9c7d32fa01e169da7c2c1af0f6017168", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(38, 721, true);
            WriteLiteral(@"
<div class=""alert alert-warning"" id=""AlertFailed"" hidden>
    <strong>Auto Guardado!</strong> No se encontró la llave del proceso.
</div>
<div class=""alert alert-success"" id=""AlertSuccess"" hidden>
    <strong>Auto Guardado!</strong> Los datos se han guardado.
</div>
<div class=""row"">
    <div class=""col-md-12"">
        <ul class=""nav nav-tabs"">
            <li class=""active""><a data-toggle=""tab"" href=""#tab_formulario"">Ingreso de datos para Liquidación</a></li>
            <li><a data-toggle=""tab"" href=""#tab_liquidacion"">Detalle Liquidación</a></li>
            <li><a data-toggle=""tab"" href=""#tab_resumen"">Resumen Liquidación</a></li>
        </ul>
        <input type=""hidden"" readonly name=""r_tipo""");
            EndContext();
            BeginWriteAttribute("value", " value=\'", 759, "\'", 784, 1);
#line 15 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\HipotecarioPesos.cshtml"
WriteAttributeValue("", 767, ViewData["Type"], 767, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(785, 127, true);
            WriteLiteral(" />\r\n        <div class=\"tab-content\">\r\n            <div id=\"tab_formulario\" class=\"tab-pane fade in active\">\r\n                ");
            EndContext();
            BeginContext(912, 6945, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04a1d678603c9c7d32fa01e169da7c2c1af0f6019735", async() => {
                BeginContext(953, 6897, true);
                WriteLiteral(@"
                    <input type=""hidden"" name=""idProceso"" />
                    <h2>Liquidación de intereses de crédito en Pesos</h2>
                    <div class=""col-md-4"">
                        <table class=""table table-condensed"">
                            <tbody>
                                <tr>
                                    <td>Fecha Contrato</td>
                                    <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_contrato"" required /></td>
                                </tr>
                                <tr>
                                    <td>Capital en Pesos</td>
                                    <td><input type=""text"" class=""no-numbar"" name=""capital"" required /></td>
                                </tr>
                                <tr>
                                    <td>Fecha Capital</td>
                                    <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_capital"" required /></td>");
                WriteLiteral(@"
                                </tr>
                                <tr>
                                    <td>Fecha Exigibilidad</td>
                                    <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_exigibilidad"" required /></td>
                                </tr>
                                <tr>
                                    <td>Fecha Liquidación</td>
                                    <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_liquidacion"" required /></td>
                                </tr>
                            </tbody>
                        </table>

                        <fieldset>
                            <legend>Vivienda de Interés Social</legend>
                            <input type=""radio"" name=""vis"" value=""true"" required /> VIS
                            <input type=""radio"" name=""vis"" value=""false"" /> NO VIS
                            <br />
                            <input type=""bu");
                WriteLiteral(@"tton"" class=""btn btn-custom"" value=""Ver Tabla"" id=""btn_ivs"" />
                        </fieldset>
                    </div>
                    <div class=""col-md-4"">
                        <fieldset>
                            <legend>Tasas Pactadas</legend>
                            <table class=""table"">
                                <tbody>
                                    <tr>
                                        <td>Interés Plazo</td>
                                        <td><input type=""number"" min=""0"" max=""99"" step="".01"" name=""i_plazo"" required /></td>
                                        <td> % EA</td>
                                    </tr>
                                    <tr><td></td><td><center>MAX<span id=""maxi_plazo""></span></center></td><td></td></tr>
                                    <tr>
                                        <td>Interés Mora</td>
                                        <td><input type=""number"" min=""0"" max=""99"" step="".01"" name=""i_mo");
                WriteLiteral(@"ra"" required /></td>
                                        <td> % EA</td>
                                    </tr>
                                    <tr><td></td><td><center>MAX<span id=""maxi_mora""></span></center></td><td></td></tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <br /><br />
                        <button class=""btn btn-custom"">Liquidar</button>
                        <button class=""btn btn-custom"" type=""button"" id=""guardar"" disabled>Guardar</button>
                        <br />
                        <label>Observaciones:</label><br />
                        <textarea name=""observaciones"" style=""resize:none; overflow:auto;"" rows=""5"" cols=""40""></textarea>
                        <input type=""hidden"" name=""idProceso"" />
                    </div>
                    <div class=""col-md-4"">
                        <ul class=""nav nav-tabs"">
                            <li class=");
                WriteLiteral(@"""active""><a data-toggle=""tab"" href=""#t_abonos"">Abonos</a></li>
                            <li><a data-toggle=""tab"" href=""#t_capitales"">Capitales</a></li>
                        </ul>
                        <div class=""tab-content"">
                            <div id=""t_abonos"" class=""tab-pane fade in active"">
                                <table class=""table"">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Fecha</th>
                                            <th>Pago</th>
                                            <th>Seguros</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><i class=""fa fa-trash delete""></i></td>
                                            <td><");
                WriteLiteral(@"input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_abono[]"" /></td>
                                            <td><input type=""text"" class=""no-numbar"" name=""pago_abono[]"" /></td>
                                            <td><input type=""text"" class=""no-numbar"" name=""seguro_abono[]"" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div id=""t_capitales"" class=""tab-pane fade"">
                                <table class=""table"">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Fecha Capital</th>
                                            <th>Monto</th>
                                        </tr>
                                    </thead>
                                    <tbody>
    ");
                WriteLiteral(@"                                    <tr>
                                            <td><i class=""fa fa-trash delete""></i></td>
                                            <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_capitales[]"" /></td>
                                            <td><input type=""text"" class=""no-numbar"" name=""capitales[]"" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <input type=""button"" class=""btn btn-custom"" id=""btn_add_capitales"" value=""Completar Capitales"" />
                            </div>
                        </div>
                    </div>
                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
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
            BeginContext(7857, 366, true);
            WriteLiteral(@"

                <div class=""col-md-12"">
                    <h3 style=""text-align: center;"">Liquidaciones Guardadas</h3>
                    <table class=""display"" id=""guardados"">
                        <thead>
                            <tr>
                                <th>Nro. Único</th>
                                <th>Tipo Liquidación</th>
");
            EndContext();
            BeginContext(8281, 2062, true);
            WriteLiteral(@"                                <th>Fecha Elaboración</th>
                                <th>Creador</th>
                                <th>Guardado</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
            <div id=""tab_liquidacion"" class=""tab-pane fade"">
                <table class=""table display"">
                    <thead>
                        <tr>
                            <th>Desde (dd/mm/aaaa)</th>
                            <th>Hasta (dd/mm/aaaa)</th>
                            <th>NoDias</th>
                            <th>Tasa Remuneratoria</th>
                            <th>Tasa Mora</th>
                            <th>Interés Aplicado</th>
                            <th>Interes Diario Aplicado</th>
                            <th>Capital</th>
                            <th>Capital a Liquidar</th>
                           ");
            WriteLiteral(@" <th>Abonos</th>
                            <th>Abono a capital</th>
                            <th>Abono Interes Mora</th>
                            <th>Abono Interes Plazo</th>
                            <th>Int Plazo<br />Período</th>
                            <th>Interes Mora<br />Período $</th>
                            <th>Saldo<br />Int Plazo</th>
                            <th>Saldo Int<br />Mora</th>
                            <th>Sub Total</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
            <div id=""tab_resumen"" class=""tab-pane fade"">
                <table class=""table"">
                    <thead>
                        <tr>
                            <th>Asunto</th>
                            <th>Valor</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <br /><br />
  ");
            WriteLiteral("              ");
            EndContext();
            BeginContext(10343, 168, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04a1d678603c9c7d32fa01e169da7c2c1af0f60121382", async() => {
                BeginContext(10447, 60, true);
                WriteLiteral("<span class=\"fas fa-file-excel\"><br />Descargar Excel</span>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(10511, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 188 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\HipotecarioPesos.cshtml"
                 if (User.IsInRole("Juez"))
                {

#line default
#line hidden
            BeginContext(10577, 126, true);
            WriteLiteral("                    <a role=\"button\" href=\"#\" id=\"descargarPdf\"><span class=\"far fa-file-pdf\"><br />Descargar PDF</span></a>\r\n");
            EndContext();
#line 191 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\HipotecarioPesos.cshtml"
                }

#line default
#line hidden
            BeginContext(10722, 521, true);
            WriteLiteral(@"            </div>

            <div id=""dialog"" title=""Valor Máximo Vivienda de Interés Social"">
                <table class=""display"">
                    <thead>
                        <tr>
                            <th>Desde</th>
                            <th>Hasta</th>
                            <th>ValorMaximo</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>

");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(11260, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(11266, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04a1d678603c9c7d32fa01e169da7c2c1af0f60124814", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
#line 211 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\HipotecarioPesos.cshtml"
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
                BeginContext(11332, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(11338, 69, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04a1d678603c9c7d32fa01e169da7c2c1af0f60126917", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_9.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
#line 212 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\HipotecarioPesos.cshtml"
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
                BeginContext(11407, 2, true);
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
