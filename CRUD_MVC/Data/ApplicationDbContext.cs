using Microsoft.EntityFrameworkCore;
using CRUD_MVC.Models;


namespace CRUD_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
            
        
        }

        public DbSet<Category> Categories { get; set; }
    }
}