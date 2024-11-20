using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Models.Article
{
    public class ArticleListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string ImgUrl { get; set; } = string.Empty;

        public string PublishDate { get; set; } = null!;

        public string Author { get; set; } = null!;
    }
}
