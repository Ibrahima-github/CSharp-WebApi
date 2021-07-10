using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly ArticlesDBContext _context;

        public ArticlesController(IConfiguration configuration, IWebHostEnvironment env, ArticlesDBContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            /* DataTable table = new DataTable();
             string sqlDataSource = _configuration.GetConnectionString("ArticleAppCon");
             SqlDataReader myReader;
             string query = "select * from dbo.Article";
             //ArticleId, ArticleName, ArticleSummary, ArticleDescription,ArticlePhotoFileName, ArticleUploadDate, ArticleAffiliateLink, Category

             using (SqlConnection myCon = new SqlConnection(sqlDataSource))
             {
                 myCon.Open();

                 using(SqlCommand myCommand = new SqlCommand(query, myCon))
                 {
                     myReader = myCommand.ExecuteReader();

                     try
                     {
                         table.Load(myReader);
                         myReader.Close();

                         myCon.Close();
                     }
                     catch (Exception e)
                     {
                         Console.WriteLine(e.Message);
                     }

                 }*/
            var table = _context.Articles.ToArray();

            return new JsonResult(table);

        }

        [HttpPost]
        public async Task<JsonResult> PostAsync(Article articleAAjouter)
        {
            Article article = new Article();

            article.ArticleName = articleAAjouter.ArticleName;
            article.ArticleSummary = articleAAjouter.ArticleSummary;
            article.ArticleDescription = articleAAjouter.ArticleDescription;
            article.ArticlePhotoFileName = articleAAjouter.ArticlePhotoFileName;
            article.ArticleUploadDate = articleAAjouter.ArticleUploadDate;
            article.ArticleAffiliateLink = articleAAjouter.ArticleAffiliateLink;
            article.Category = articleAAjouter.Category;

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public async Task<JsonResult> PutAsync(Article articleAModifier)
        {
            var article = await _context.Articles.FindAsync(articleAModifier.ArticleId); ;

            article.ArticleName = articleAModifier.ArticleName;
            article.ArticleSummary = articleAModifier.ArticleSummary;
            article.ArticleDescription = articleAModifier.ArticleDescription;
            article.ArticlePhotoFileName = articleAModifier.ArticlePhotoFileName;
            article.ArticleUploadDate = articleAModifier.ArticleUploadDate;
            article.ArticleAffiliateLink = articleAModifier.ArticleAffiliateLink;
            article.Category = articleAModifier.Category;

            _context.Articles.Update(article);
            await _context.SaveChangesAsync();

            return new JsonResult("Updated successfully");
        }

        [HttpDelete("{ArticleId}")]
        public async Task<JsonResult> DeleteAsync(int ArticleId)
        {
            var articleASupprimer = _context.Articles.Find(ArticleId);

            _context.Articles.Remove(articleASupprimer);
            await _context.SaveChangesAsync(); 

            return new JsonResult("Deleted successfully");
        }
    }
}
