using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using catalogApi.Models;

namespace catalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        public ArticleController()
        {
        }

        // GET api/article
        [HttpGet("")]
        public ActionResult<IEnumerable<Article>> Get()
        {
            var articles = new List<Article>();
            var art1 = new Article{ ArticleName = "art1", ArticleDescription = "ART 1 description"}; 
            articles.Add(art1);
            var art2 = new Article{ ArticleName = "art2", ArticleDescription = "ART 2 description"}; 
            articles.Add(art2);
            var art3 = new Article{ ArticleName = "art3", ArticleDescription = "ART 3 description"};
            articles.Add(art3);
            var art4 = new Article{ ArticleName = "art4", ArticleDescription = "ART 4 description"}; 
            articles.Add(art4);
            return articles;
        }

        // GET api/article/5
        [HttpGet("{id}")]
        public ActionResult<string> GetstringById(int id)
        {
            return null;
        }

        // POST api/article
        [HttpPost("")]
        public void Poststring(string value)
        {
        }

        // PUT api/article/5
        [HttpPut("{id}")]
        public void Putstring(int id, string value)
        {
        }

        // DELETE api/article/5
        [HttpDelete("{id}")]
        public void DeletestringById(int id)
        {
        }
    }
}