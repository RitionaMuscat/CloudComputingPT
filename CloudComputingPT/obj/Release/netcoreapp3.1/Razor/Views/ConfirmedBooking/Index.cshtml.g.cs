#pragma checksum "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "134b0beb08459dcbab341132b4e2b250012f1a42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ConfirmedBooking_Index), @"mvc.1.0.view", @"/Views/ConfirmedBooking/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\_ViewImports.cshtml"
using CloudComputingPT;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\_ViewImports.cshtml"
using CloudComputingPT.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"134b0beb08459dcbab341132b4e2b250012f1a42", @"/Views/ConfirmedBooking/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c966a26d13e1b01ed84f6b17b836620053f48e2", @"/Views/_ViewImports.cshtml")]
    public class Views_ConfirmedBooking_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CloudComputingPT.Models.BookingDetails>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Passenger", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "134b0beb08459dcbab341132b4e2b250012f1a424268", async() => {
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>Available Services</title>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "134b0beb08459dcbab341132b4e2b250012f1a425340", async() => {
                WriteLiteral(@"
    <table class=""table"">
        <thead>
            <tr>
                <th>
                    Passenger Id
                </th>
                <th>
                    Residing Address
                </th>
                <th>
                    Destination Address
                </th>
                <th>
                    Booking Confirmed
                </th>
                <th>
                    Luxury
                </th>
                <th>
                    Economy
                </th>
                <th>
                    Business
                </th>
                <th>
                    Driver Email
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 42 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 46 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.passengerId));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 49 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.residingAdress));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 52 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.destinationAddress));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 55 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.isBookingConfirmed));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 58 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.luxury));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 61 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.economy));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 64 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.business));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 67 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.DriverDetails));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                </td>\r\n                <td>\r\n");
#nullable restore
#line 70 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
                     if (item.DriverDetails == null)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "134b0beb08459dcbab341132b4e2b250012f1a4210362", async() => {
                    WriteLiteral("Acknowledge Service");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 72 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
                                                                      WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 73 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </td>\r\n            </tr>\r\n");
#nullable restore
#line 76 "C:\Users\ritio\Desktop\Mcast Degree\Programming For the Cloud\AssignmentHome\CloudComputingPT\CloudComputingPT\Views\ConfirmedBooking\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\r\n    </table>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CloudComputingPT.Models.BookingDetails>> Html { get; private set; }
    }
}
#pragma warning restore 1591
