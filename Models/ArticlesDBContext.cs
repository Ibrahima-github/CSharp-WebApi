using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ArticlesDBContext: DbContext
    {
        public ArticlesDBContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories{ get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
