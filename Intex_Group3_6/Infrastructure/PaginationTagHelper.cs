using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Intex_Group3_6.Models.ViewModels;

namespace Intex_Group3_6.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext] [HtmlAttributeNotBound] public ViewContext? ViewContext { get; set; }
        public string? PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public PaginationInfo PageModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                output.TagName = "nav";
                TagBuilder result = new TagBuilder("ul");
                result.AddCssClass("pagination");

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

                // "Previous" link
                CreatePageLink(PageModel.CurrentPage - 1, false, "Back");

                // Always show the first page
                CreatePageLink(1, PageModel.CurrentPage == 1);

                int pageWindow = 3;
                int startPage = Math.Max(2, PageModel.CurrentPage - pageWindow);
                int endPage = Math.Min(PageModel.TotalPages - 1, PageModel.CurrentPage + pageWindow);

                if (startPage > 2)
                {
                    result.InnerHtml.AppendHtml("<li class=\"page-item disabled\"><span class=\"page-link\">...</span></li>");
                }

                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == PageModel.CurrentPage || (i != 1 && i != PageModel.TotalPages))
                    {
                        CreatePageLink(i, i == PageModel.CurrentPage);
                    }
                }

                if (endPage < PageModel.TotalPages - 1)
                {
                    result.InnerHtml.AppendHtml("<li class=\"page-item disabled\"><span class=\"page-link\">...</span></li>");
                }

                // Always show the last page
                CreatePageLink(PageModel.TotalPages, PageModel.CurrentPage == PageModel.TotalPages);

                // "Next" link
                CreatePageLink(PageModel.CurrentPage + 1, false, "Next");

                output.Content.SetHtmlContent(result);
            }
        }
    }
}