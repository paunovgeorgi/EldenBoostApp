using EldenBoost.Core.Models.Article.Enums;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.GeneralApplicationConstants;

namespace EldenBoost.Core.Models.Article
{
    public class AllArticlesQueryModel
    {
        [Display(Name = "Search")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Articles By")]
        public ArticleSorting ArticleSorting { get; set; }

        public int CurrentPage { get; set; } = DefaultPage;

        [Display(Name = "Show Articles On Page")]
        public int ArticlesOnPage { get; set; } = EntitiesPerPage;

        public int TotalArticles { get; set; }

        public IEnumerable<ArticleCardViewModel> Articles { get; set; } = new List<ArticleCardViewModel>();
    }
}
