using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Models.Article.Contracts;
using System.ComponentModel.DataAnnotations;

namespace EldenBoost.Core.Models.Article
{
    public class ArticleCardViewModel : IArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        [Display(Name = "Article Type")]
        public ArticleType ArticleType { get; set; }

        public string ImageURL { get; set; } = null!;
    }
}
