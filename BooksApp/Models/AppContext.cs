using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection")
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                        .HasMany<Author>(b => b.Authors)
                        .WithMany(a => a.Books)
                        .Map(ba =>
                        {
                            ba.MapLeftKey("IdBook");
                            ba.MapRightKey("IdAuthor");
                            ba.ToTable("BookAuthors");
                        });

        }
    }
}
