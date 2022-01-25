using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BlogDBContext _context;

        public CategoriesController(IConfiguration configuration, BlogDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [EnableCors("Policy")]
        [HttpGet]
        public JsonResult Get()
        {
            try
            {

            var table = _context.Categories.ToList();
            return new JsonResult(table);

            }catch(Exception e)
            {
                return new JsonResult(e.Message);
            }

        }

        [EnableCors("Policy")]
        [HttpPost]
        public async Task<JsonResult> PostAsync(Category categorieAAjouter)
        {
            Category categorie = new Category();

            categorie.CategoryName = categorieAAjouter.CategoryName;

            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            return new JsonResult("Added successfully");
        }

        [EnableCors("Policy")]
        [HttpPut]
        public async Task<JsonResult> PutAsync(Category categorieAModifier)
        {
            var categorie = await _context.Categories.FindAsync(categorieAModifier.CategoryId); 

            categorie.CategoryName = categorieAModifier.CategoryName;

            _context.Categories.Update(categorie);
            await _context.SaveChangesAsync();

            return new JsonResult("Updated successfully");
        }

        [EnableCors("Policy")]
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
