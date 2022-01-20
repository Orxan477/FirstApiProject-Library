using FirstRestApi_Library_.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstRestApi_Library_.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
