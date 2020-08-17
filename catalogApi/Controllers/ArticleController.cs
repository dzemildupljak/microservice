using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using catalogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        [Authorize(Roles = "admin, manager")]
        public ActionResult<IEnumerable<Article>> Get()
        {
            var articles = new List<Article>();
            var art1 = new Article { Id = 1, ArticleName = "art1", ArticleDescription = "ART 1 description" };
            articles.Add(art1);
            var art2 = new Article { Id = 2, ArticleName = "art2", ArticleDescription = "ART 2 description" };
            articles.Add(art2);
            var art3 = new Article { Id = 3, ArticleName = "art3", ArticleDescription = "ART 3 description" };
            articles.Add(art3);
            var art4 = new Article { Id = 4, ArticleName = "art4", ArticleDescription = "ART 4 description" };
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