using BooksEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BooksEditor.DataAccess
{
    public class BooksContext : DbContext
    {
        public BooksContext()
        {
        }

        public BooksContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
