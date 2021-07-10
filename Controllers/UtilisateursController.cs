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
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ArticlesDBContext _context;

        public UtilisateursController(IConfiguration configuration, ArticlesDBContext context)
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

            utilisateur.UtilisateurEmailAddress = utilisateurAAjouter.UtilisateurEmailAddress;

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return new JsonResult("Added successfully");
        }
    }
}
