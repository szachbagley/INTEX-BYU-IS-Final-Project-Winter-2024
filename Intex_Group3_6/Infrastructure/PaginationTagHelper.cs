using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Intex_Group3_6.Models.ViewModels;

namespace TheWaterProject.Infrastructure
{
    [HtmlTargetElement("div", Attributes="page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PaginationTagHelper (IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }
        public string? PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix ="page-url-")]
        public Dictionary<string, object> PageUrlValues {  get; set; } = new Dictionary<string, object>();
        public PaginationInfo PageModel { get; set; }

        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; } = String.Empty;
        public string PageClassSelected { get; set; } = String.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");
                
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    PageUrlValues["pageNum"] = i;
                    tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    // Add base class for pagination links
                    tag.AddCssClass("page-link"); // Replace with your desired base class

                    // Add styling for the current page
                    if (i == PageModel.CurrentPage)
                    {
                        tag.AddCssClass("active"); // Replace with your desired class for active page
                    }

                    // Optional: Add disabled class for first and last pages (if applicable)
                    if (i == 1 && !PageModel.HasPreviousPage)
                    {
                        tag.AddCssClass("disabled"); // Replace with your desired class for disabled links
                    }
                    else if (i == PageModel.TotalPages && !PageModel.HasNextPage)
                    {
                        tag.AddCssClass("disabled"); // Replace with your desired class for disabled links
                    }

                    // Your code to customize the content of the link (e.g., text or icons)
                    tag.InnerHtml.Append(i.ToString()); // Or replace with your desired content

                    result.InnerHtml.AppendHtml(tag);
                }


                output.Content.AppendHtml(result.InnerHtml);

            }
        }
    }
}
