using System.ComponentModel.DataAnnotations;

namespace EldenBoost.Core.Models.Article
{
	public class ArticleReadViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;

		public string Content { get; set; } = null!;

		public string ImgUrl { get; set; } = string.Empty;

		[Display(Name = "Published On:")]
		public string PublishDate { get; set; } = null!;

		public string Author { get; set; } = null!;
	}
}
