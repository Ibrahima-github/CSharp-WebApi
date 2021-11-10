using Microsoft.AspNetCore.Cors;
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

        [EnableCors("Policy")]
        [HttpGet]
        public JsonResult Get()
        {
            var table = _context.Utilisateurs.ToArray();

            return new JsonResult(table);

        }

        [EnableCors("Policy")]
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

        [EnableCors("Policy")]
        [HttpPut]
        public async Task<JsonResult> PutAsync(Utilisateur utilisateurAModifier)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(utilisateurAModifier.UtilisateurId);

            utilisateur.UtilisateurUsername = utilisateurAModifier.UtilisateurUsername;
            utilisateur.UtilisateurEmailAddress = utilisateurAModifier.UtilisateurEmailAddress;
            utilisateur.UtilisateurPassword = utilisateurAModifier.UtilisateurPassword;
            utilisateur.IsAdmin = false;

            _context.Utilisateurs.Update(utilisateur);
            await _context.SaveChangesAsync();

            return new JsonResult("Updated successfully");
        }

        [EnableCors("Policy")]
        [HttpDelete("{UtilisateurId}")]
        public async Task<JsonResult> DeleteAsync(int UtilisateurId)
        {
            var utilisateurASupprimer = _context.Utilisateurs.Find(UtilisateurId);

            _context.Utilisateurs.Remove(utilisateurASupprimer);
            await _context.SaveChangesAsync();

            return new JsonResult("Deleted successfully");
        }
    }
}
