using System.ComponentModel.DataAnnotations;

namespace catalogApi.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }
    }
}