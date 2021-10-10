using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly BlogDBContext _context;

        public PostsController(IConfiguration configuration, IWebHostEnvironment env, BlogDBContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var table = _context.Posts.ToList();

            return new JsonResult(table);

        }

        [HttpPost]
        public async Task<JsonResult> PostAsync(Post postAAjouter)
        {

            Post post = new Post();

            post.PostName = postAAjouter.PostName;
            post.Category = postAAjouter.Category;
            post.ArticleUploadDate = postAAjouter.ArticleUploadDate;
            post.PostDescription = postAAjouter.PostDescription;
            post.PostYoutubeIframe = postAAjouter.PostYoutubeIframe;/*
            post.PostPicture = postAAjouter.PostPicture;
            post.PostLink = postAAjouter.PostLink;*/

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public async Task<JsonResult> PutAsync(Post postAModifier)
        {
            var post = await _context.Posts.FindAsync(postAModifier.PostId); ;

            post.PostName = postAModifier.PostName;
            post.Category = postAModifier.Category;
            post.ArticleUploadDate = postAModifier.ArticleUploadDate;
            post.PostDescription = postAModifier.PostDescription;
            post.PostYoutubeIframe = postAModifier.PostYoutubeIframe;
           /* post.PostPicture = postAModifier.PostPicture;
            post.PostLink = postAModifier.PostLink;*/

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return new JsonResult("Updated successfully");
        }

        [HttpDelete("{PostId}")]
        public async Task<JsonResult> DeleteAsync(int PostId)
        {
            var postASupprimer = _context.Posts.Find(PostId);

            _context.Posts.Remove(postASupprimer);
            await _context.SaveChangesAsync();

            return new JsonResult("Deleted successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllCategories")]
        [HttpGet]
        public JsonResult GetAllCategories()
        {
            var list = _context.Categories.ToList();

            return new JsonResult(list);
        }
    }
}
