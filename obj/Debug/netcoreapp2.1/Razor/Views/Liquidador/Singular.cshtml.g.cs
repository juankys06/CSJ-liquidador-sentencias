#pragma checksum "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\Singular.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "29303f0b11797df30b56930bd7381f4c5ffe1a93"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Liquidador_Singular), @"mvc.1.0.view", @"/Views/Liquidador/Singular.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Liquidador/Singular.cshtml", typeof(AspNetCore.Views_Liquidador_Singular))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"29303f0b11797df30b56930bd7381f4c5ffe1a93", @"/Views/Liquidador/Singular.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e74feaf03a43be7ba35bb5a7f593299181ba98b8", @"/Views/_ViewImports.cshtml")]
    public class Views_Liquidador_Singular : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Form_SearchProcess", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("liq_singular"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Liquidador", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DescargaExcel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("descargarExcel"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/procesos.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/singular.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "29303f0b11797df30b56930bd7381f4c5ffe1a937106", async() => {
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
            BeginContext(38, 835, true);
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
            <li class=""active""><a data-toggle=""tab"" href=""#tab1"">Ingreso de datos para Liquidación</a></li>
            <li><a data-toggle=""tab"" href=""#tab2"">Saldos Iniciales</a></li>
            <li><a data-toggle=""tab"" href=""#tab3"">Detalle Liquidación</a></li>
            <li><a data-toggle=""tab"" href=""#tab4"">Resumen Liquidación</a></li>
        </ul>

        <div class=""tab-content"">
            <div id=""tab1"" class=""tab-pane fade in active"">
                ");
            EndContext();
            BeginContext(873, 6161, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "29303f0b11797df30b56930bd7381f4c5ffe1a939233", async() => {
                BeginContext(911, 6116, true);
                WriteLiteral(@"
                    <div class=""col-md-4"">
                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <td>Capital</td>
                                    <td><input type=""text"" class=""no-numbar"" value=""0"" name=""capital"" required /></td>
                                </tr>
                                <tr>
                                    <td>Fecha Inicio Obligación</td>
                                    <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_obligacion"" required /></td>
                                </tr>
                                <tr>
                                    <td>Fecha Exigibilidad</td>
                                    <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_exigibilidad"" required /></td>
                                </tr>
                                <tr>
                                    <td>Fecha Liquid");
                WriteLiteral(@"ación</td>
                                    <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_liquidacion"" required /></td>
                                </tr>
                            </tbody>
                        </table>
                        Aplica Sanción 20% Art. 731 CC. cheques <input type=""checkbox"" name=""aplica_sancion"" />
                        <br /><br />
                        <button class=""btn btn-custom"" type=""submit"">Liquidar</button>
                        <button class=""btn btn-custom"" disabled type=""button"" id=""guardar"">Guardar</button>
                        <br />
                        <label>Observaciones:</label><br />
                        <textarea name=""observaciones"" style=""resize:none; overflow:auto;"" rows=""5"" cols=""40""></textarea>
                        <input type=""hidden"" name=""idProceso"" />
                    </div>
                    <div class=""col-md-4"">
                        <div>
                            <label>I");
                WriteLiteral(@"nterés Corriente</label>
                            <br />
                            <input type=""radio"" name=""i_corriente"" value=""variable"" /> Variable
                            <br />
                            <input type=""radio"" name=""i_corriente"" value=""pactado"" /> Pactado
                            <input type=""number"" step="".01"" disabled name=""tasa_pacto_a"" />
                        </div>
                        <div>
                            <label>Interés de Mora</label>
                            <br />
                            <input type=""radio"" name=""i_mora"" value=""variable"" /> Variable
                            <br />
                            <input type=""radio"" name=""i_mora"" value=""pactado"" /> Pactado o Legal
                            <input type=""number"" step="".01"" disabled name=""tasa_pacto_b"" />
                        </div>
                    </div>
                    <div class=""col-md-4"">
                        <ul class=""nav nav-tabs"">
        ");
                WriteLiteral(@"                    <li class=""active""><a data-toggle=""tab"" href=""#t_abonos"">Abonos</a></li>
                            <li><a data-toggle=""tab"" href=""#t_capitales"">Capitales</a></li>
                        </ul>

                        <div class=""tab-content"">
                            <div id=""t_abonos"" class=""tab-pane fade in active"">
                                <table class=""table"">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Fecha Abono</th>
                                            <th>Monto</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><i class=""fa fa-trash delete""></i></td>
                                            <td><input type=""date"" max");
                WriteLiteral(@"=""2100-01-01"" min=""1900-01-01"" name=""f_abono[]"" /></td>
                                            <td><input type=""text"" class=""no-numbar"" name=""abono[]"" /></td>

                                        </tr>
                                    </tbody>
                                </table>
                                <input type=""button"" class=""btn btn-custom"" id=""add_abono"" value=""Otro Abono"" />
                                
                                <button class=""btn btn-custom"" type=""button"" id=""btn_completar_abono"">Completar</button>
                            </div>
                            <div id=""t_capitales"" class=""tab-pane fade"">
                                <table class=""table"">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Fecha Capital</th>
                                            <th>Monto</th>
            ");
                WriteLiteral(@"                            </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><i class=""fa fa-trash delete""></i></td>
                                            <td><input type=""date"" max=""2100-01-01"" min=""1900-01-01"" name=""f_capitales[]"" /></td>
                                            <td><input type=""text"" class=""no-numbar"" name=""capitales[]"" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <input type=""button"" class=""btn btn-custom"" id=""add_capitales"" value=""Otro Capital"" />
                                <button class=""btn btn-custom"" type=""button"" id=""btn_completar_capital"">Completar</button>
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
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7034, 366, true);
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
            BeginContext(7458, 4150, true);
            WriteLiteral(@"                                <th>Fecha Elaboración</th>
                                <th>Creador</th>
                                <th>Guardado</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>

            <div id=""dialog"" title=""Tipo de Interés Variable"">
                <h3>Tasa de Referencia</h3>
                <input type=""radio"" name=""i_corriente_var"" value=""IBC"" form=""liq_singular"" checked /> Interés Bancario Corriente
                <br /><br />
                <input type=""radio"" name=""i_corriente_var"" form=""liq_singular"" value=""MIC"" /> Interés Bancario Corriente MICROCRÉDITO
                <br /><br />
                <input type=""radio"" name=""i_corriente_var"" form=""liq_singular"" value=""DTF"" /> DTF + Puntos
                <input type=""number"" name=""puntos_corriente"" form=""liq_singular"" id=""corriente_dtf"" step="".01"" disabled />
           ");
            WriteLiteral(@"     <br /><br />
                <input type=""radio"" name=""i_corriente_var"" form=""liq_singular"" value=""IPC"" /> IPC + Puntos
                <input type=""number"" name=""puntos_corriente"" form=""liq_singular"" id=""corriente_ipc"" step="".01"" disabled />
            </div>

            <div id=""dialog2"" title=""Tipo de Interés Variable"">
                <h3>Tasa de Referencia</h3>
                <input type=""radio"" name=""i_mora_var"" form=""liq_singular"" value=""IBC"" checked /> Interés Bancario Corriente
                <br /><br />
                <input type=""radio"" name=""i_mora_var"" form=""liq_singular"" value=""MIC"" /> Interés Bancario Corriente MICROCRÉDITO
                <br /><br />
                <input type=""radio"" name=""i_mora_var"" form=""liq_singular"" value=""DTF"" /> DTF + Puntos
                <input type=""number"" name=""puntos_mora"" form=""liq_singular"" id=""mora_dtf"" step="".01"" disabled />
                <br /><br />
                <input type=""radio"" name=""i_mora_var"" form=""liq_singular"" value");
            WriteLiteral(@"=""IPC"" /> IPC + Puntos
                <input type=""number"" name=""puntos_mora"" form=""liq_singular"" id=""mora_ipc"" step="".01"" disabled />
            </div>

            <div id=""tab2"" class=""tab-pane fade"">
                <h4>Saldos Iniciales de Intereses</h4>
                <label>Interés de Plazo</label><input type=""number"" value=""0"" step="".01"" name=""si_plazo"" form=""liq_singular"" />
                <br />
                <label>Interés de Mora</label><input type=""number"" value=""0"" step="".01"" name=""si_mora"" form=""liq_singular"" />
            </div>

            <div id=""tab3"" class=""tab-pane fade"">
                <table class=""table display"">
                    <thead>
                        <tr>
                            <th>Desde (dd/mm/aaaa)</th>
                            <th>Hasta (dd/mm/aaaa)</th>
                            <th>NoDias</th>
                            <th>TasaAnual</th>
                            <th>TasaMáxima</th>
                            <th>IntAplicad");
            WriteLiteral(@"o</th>
                            <th>InterésEfectivo</th>
                            <th>Capital</th>
                            <th>CapitalALiquidar</th>
                            <th>IntPlazoPeriodo</th>
                            <th>SaldoIntPlazo</th>
                            <th>InteresMoraPeríodo</th>
                            <th>SaldoIntMora</th>
                            <th>Abonos</th>
                            <th>SubTotal</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>

            <div id=""tab4"" class=""tab-pane fade"">
                <table class=""table"">
                    <thead>
                        <tr>
                            <th>Asunto</th>
                            <th>Valor</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <p id=""texto"" style=""color:red;");
            WriteLiteral("\"></p>\r\n                <br /><br />\r\n                ");
            EndContext();
            BeginContext(11608, 168, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "29303f0b11797df30b56930bd7381f4c5ffe1a9322359", async() => {
                BeginContext(11712, 60, true);
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
            BeginContext(11776, 26, true);
            WriteLiteral("\r\n                <br />\r\n");
            EndContext();
#line 213 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\Singular.cshtml"
                 if (User.IsInRole("Juez"))
                {

#line default
#line hidden
            BeginContext(11866, 122, true);
            WriteLiteral("                <a role=\"button\" href=\"#\" id=\"descargarPdf\"><span class=\"far fa-file-pdf\"><br />Descargar PDF</span></a>\r\n");
            EndContext();
#line 216 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\Singular.cshtml"
                }

#line default
#line hidden
            BeginContext(12007, 58, true);
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(12082, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(12088, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "29303f0b11797df30b56930bd7381f4c5ffe1a9325337", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
#line 223 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\Singular.cshtml"
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
                BeginContext(12154, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(12160, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "29303f0b11797df30b56930bd7381f4c5ffe1a9327432", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_9.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
#line 224 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Liquidador\Singular.cshtml"
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
                BeginContext(12226, 2, true);
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
