namespace EldenBoost.Core.Models.Article
{
    public class AllArticlesFilteredAndPagedModel
    {
        public int TotalArticleCount { get; set; }

        public IEnumerable<ArticleCardViewModel> Articles { get; set; } = new List<ArticleCardViewModel>();
    }
}
