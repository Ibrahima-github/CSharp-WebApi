using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BlogDBContext _context;
        private readonly JwtService _jwtService;

        public UtilisateursController(IConfiguration configuration, BlogDBContext context, JwtService jwtService)
        {
            _configuration = configuration;
            _context = context;
            _jwtService = jwtService;
        }

        [EnableCors("Policy")]
        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                var table = _context.Utilisateurs.ToArray();

                return new JsonResult(table);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }


        }

        [EnableCors("Policy")]
        [HttpPost]
        public async Task<JsonResult> PostAsync(Utilisateur utilisateurAAjouter)
        {
            Utilisateur utilisateur = new Utilisateur();

            utilisateur.UtilisateurUsername = utilisateurAAjouter.UtilisateurUsername;
            utilisateur.UtilisateurEmailAddress = utilisateurAAjouter.UtilisateurEmailAddress;
            utilisateur.UtilisateurPassword = BCrypt.Net.BCrypt.HashPassword(utilisateurAAjouter.UtilisateurPassword);
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
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Utilisateur utilisateur)
        {
            Utilisateur user = new Utilisateur();
            user = _context.Utilisateurs.FirstOrDefault(x => x.UtilisateurEmailAddress == utilisateur.UtilisateurEmailAddress);

            if (user == null) return BadRequest(new { message = "Invalid Credentials " });

            if (!BCrypt.Net.BCrypt.Verify(utilisateur.UtilisateurPassword, user.UtilisateurPassword))
            {
                return BadRequest(new { message = "Invalid Credentials " });
            }

            var jwt = _jwtService.Generate(user.UtilisateurId);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new
            {
                jwt
            });

        }

        [HttpGet]
        [Route("user")]
        public IActionResult User(Utilisateur utilisateur)
        {

            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                var userId = int.Parse(token.Issuer);

                var user = _context.Utilisateurs.Find(userId);

                return Ok("Authenticated");

            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Your are logged out" });
        }


    }
}
