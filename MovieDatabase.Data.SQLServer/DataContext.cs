using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data.Domain.Entities;

namespace MovieDatabase.Data.SQLServer
{
    public class DataContext: DbContext
    {
        DbSet<Movie> Movies { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(new Movie()
            {
                Id = 1,
                Rating = 0,
                Seen = false,
                Title = "Shrek"
            });
            modelBuilder.Entity<Movie>().HasData(new Movie()
            {
                Id = 2,
                Rating = 1,
                Seen = true,
                Title = "Inception"
            });
            modelBuilder.Entity<Movie>().HasData(new Movie()
            {
                Id = 3,
                Rating = 2,
                Seen = true,
                Title = "The Green Latern"
            });
            modelBuilder.Entity<Movie>().HasData(new Movie()
            {
                Id = 4,
                Rating = 5,
                Seen = true,
                Title = "The Matrix"
            });
            modelBuilder.Entity<Movie>().HasData(new Movie()
            {
                Id = 5,
                Rating = 5,
                Seen = true,
                Title = "The Muppets"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}