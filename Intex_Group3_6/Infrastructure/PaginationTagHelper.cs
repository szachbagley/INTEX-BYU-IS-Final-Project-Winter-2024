// Required namespaces for the functionality of MVC, Tag Helpers, and ViewModels
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Intex_Group3_6.Models.ViewModels;

namespace Intex_Group3_6.Infrastructure
{
    // This attribute specifies that the tag helper targets <div> elements with an attribute "page-model"
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        // Constructor that accepts IUrlHelperFactory to create IUrlHelper instances
        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        // ViewContext provides context about the view where the Tag Helper is executed
        [ViewContext] [HtmlAttributeNotBound] public ViewContext? ViewContext { get; set; }
        // PageAction is the action method to be called when a page link is clicked
        public string? PageAction { get; set; }

        // Dictionary to hold additional route values for the page links
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        // Pagination model holding current page, total pages, etc.
        public PaginationInfo PageModel { get; set; }

        // Overridden Process method to generate the HTML for pagination
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                // Create a URL helper using the context provided by the tag helper
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                // Change the output tag from div to nav for semantic HTML
                output.TagName = "nav";
                // Create an unordered list that will hold the page links
                TagBuilder result = new TagBuilder("ul");
                result.AddCssClass("pagination");

                // Local function to create individual page links
                void CreatePageLink(int i, bool isCurrentPage = false, string text = null)
                {
                    TagBuilder listItem = new TagBuilder("li");
                    listItem.AddCssClass("page-item");
                    if (isCurrentPage)
                    {
                        listItem.AddCssClass("active");
                    }
                    TagBuilder tag = new TagBuilder("a");
                    tag.AddCssClass("page-link");
                    PageUrlValues["pageNum"] = i;
                    tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    tag.InnerHtml.AppendHtml(text ?? i.ToString());
                    listItem.InnerHtml.AppendHtml(tag);
                    result.InnerHtml.AppendHtml(listItem);
                }

                // Add the "Back" link if not on the first page
                if (PageModel.CurrentPage > 1)
                {
                    CreatePageLink(PageModel.CurrentPage - 1, false, "Back");
                }

                // Always show the first page link
                CreatePageLink(1, PageModel.CurrentPage == 1);

                // Calculate window of pages around the current page to avoid displaying too many page links
                int pageWindow = 3;
                int startPage = Math.Max(2, PageModel.CurrentPage - pageWindow);
                int endPage = Math.Min(PageModel.TotalPages - 1, PageModel.CurrentPage + pageWindow);

                // Display an ellipsis if there's a gap between the first page and the current window start
                if (startPage > 2)
                {
                    result.InnerHtml.AppendHtml("<li class=\"page-item disabled\"><span class=\"page-link\">...</span></li>");
                }

                // Generate page links within the window
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == PageModel.CurrentPage || (i != 1 && i != PageModel.TotalPages))
                    {
                        CreatePageLink(i, i == PageModel.CurrentPage);
                    }
                }

                // Display an ellipsis if there's a gap between the current window end and the last page
                if (endPage < PageModel.TotalPages - 1)
                {
                    result.InnerHtml.AppendHtml("<li class=\"page-item disabled\"><span class=\"page-link\">...</span></li>");
                }

                // Always show the last page link
                CreatePageLink(PageModel.TotalPages, PageModel.CurrentPage == PageModel.TotalPages);

                // Add the "Next" link if not on the last page
                if (PageModel.CurrentPage != PageModel.TotalPages)
                {
                    CreatePageLink(PageModel.CurrentPage + 1, false, "Next");
                }

                // Replace the inner HTML of the nav element with the constructed list of page links
                output.Content.SetHtmlContent(result);
            }
        }
    }
}
