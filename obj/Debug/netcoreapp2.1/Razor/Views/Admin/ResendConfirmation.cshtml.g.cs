#pragma checksum "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ResendConfirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c137e8bd5d014cdff23095bd2e6145b0a828710c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ResendConfirmation), @"mvc.1.0.view", @"/Views/Admin/ResendConfirmation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/ResendConfirmation.cshtml", typeof(AspNetCore.Views_Admin_ResendConfirmation))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c137e8bd5d014cdff23095bd2e6145b0a828710c", @"/Views/Admin/ResendConfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e74feaf03a43be7ba35bb5a7f593299181ba98b8", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_ResendConfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 57, true);
            WriteLiteral("<h2>Re-envío de correo electrónico</h2>\r\n<div class=\"row\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 57, "\"", 139, 3);
            WriteAttributeValue("", 65, "margin-top:", 65, 11, true);
            WriteAttributeValue(" ", 76, "10px;", 77, 6, true);
#line 2 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ResendConfirmation.cshtml"
WriteAttributeValue(" ", 82, TempData["Confirmed"] == null ? "display: none;" : "", 83, 56, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(140, 40, true);
            WriteLiteral(">\r\n    <div class=\"alert alert-warning\">");
            EndContext();
            BeginContext(181, 31, false);
#line 3 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ResendConfirmation.cshtml"
                                Write(Html.Raw(TempData["Confirmed"]));

#line default
#line hidden
            EndContext();
            BeginContext(212, 32, true);
            WriteLiteral("</div>\r\n</div>\r\n<div class=\"row\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 244, "\"", 323, 3);
            WriteAttributeValue("", 252, "margin-top:", 252, 11, true);
            WriteAttributeValue(" ", 263, "10px;", 264, 6, true);
#line 5 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ResendConfirmation.cshtml"
WriteAttributeValue(" ", 269, TempData["Sented"] == null ? "display: none;" : "", 270, 53, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(324, 40, true);
            WriteLiteral(">\r\n    <div class=\"alert alert-success\">");
            EndContext();
            BeginContext(365, 28, false);
#line 6 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ResendConfirmation.cshtml"
                                Write(Html.Raw(TempData["Sented"]));

#line default
#line hidden
            EndContext();
            BeginContext(393, 32, true);
            WriteLiteral("</div>\r\n</div>\r\n<div class=\"row\"");
            EndContext();
            BeginWriteAttribute("style", " style=\"", 425, "\"", 503, 3);
            WriteAttributeValue("", 433, "margin-top:", 433, 11, true);
            WriteAttributeValue(" ", 444, "10px;", 445, 6, true);
#line 8 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ResendConfirmation.cshtml"
WriteAttributeValue(" ", 450, TempData["Error"] == null ? "display: none;" : "", 451, 52, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(504, 37, true);
            WriteLiteral(">\r\n    <p class=\"alert alert-danger\">");
            EndContext();
            BeginContext(542, 27, false);
#line 9 "C:\Users\Daniel\Documents\.net core\liquidador\liquidador_web\Views\Admin\ResendConfirmation.cshtml"
                             Write(Html.Raw(TempData["Error"]));

#line default
#line hidden
            EndContext();
            BeginContext(569, 12, true);
            WriteLiteral("</p>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
