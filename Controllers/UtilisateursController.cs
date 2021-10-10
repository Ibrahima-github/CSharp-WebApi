using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BlogDBContext _context;

        public UtilisateursController(IConfiguration configuration, BlogDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var table = _context.Utilisateurs.ToArray();

            return new JsonResult(table);

        }
        [HttpPost]
        public async Task<JsonResult> PostAsync(Utilisateur utilisateurAAjouter)
        {
            Utilisateur utilisateur = new Utilisateur();

            utilisateur.UtilisateurUsername = utilisateurAAjouter.UtilisateurUsername;
            utilisateur.UtilisateurEmailAddress = utilisateurAAjouter.UtilisateurEmailAddress;
            utilisateur.UtilisateurPassword = utilisateurAAjouter.UtilisateurPassword;
            utilisateur.IsAdmin = false;

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return new JsonResult("Added successfully");
        }
    }
}
