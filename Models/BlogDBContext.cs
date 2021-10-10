using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
