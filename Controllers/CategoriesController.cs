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
    public class CategoriesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ArticlesDBContext _context;

        public CategoriesController(IConfiguration configuration, ArticlesDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var table = _context.Categories.ToArray();

            return new JsonResult(table);

        }

        [HttpPost]
        public async Task<JsonResult> PostAsync(Category categorieAAjouter)
        {
            Category categorie = new Category();

            categorie.CategoryName = categorieAAjouter.CategoryName;

            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public async Task<JsonResult> PutAsync(Category categorieAModifier)
        {
            var categorie = await _context.Categories.FindAsync(categorieAModifier.CategoryId); ;

            categorie.CategoryName = categorieAModifier.CategoryName;

            _context.Categories.Update(categorie);
            await _context.SaveChangesAsync();

            return new JsonResult("Updated successfully");
        }

        [HttpDelete("{CategoryId}")]
        public async Task<JsonResult> DeleteAsync(int CategoryId)
        {
            var categorieASupprimer = _context.Categories.Find(CategoryId);

            _context.Categories.Remove(categorieASupprimer);
            await _context.SaveChangesAsync();

            return new JsonResult("Deleted successfully");
        }
    }
}
