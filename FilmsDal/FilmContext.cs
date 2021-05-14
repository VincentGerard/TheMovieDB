using LibraryProperties;
using Microsoft.EntityFrameworkCore;

namespace FilmsDal
{
    public class FilmContext : DbContext 
    {
        public FilmContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Film>().HasIndex(film => film.Id).IsUnique();
            modelBuilder.Entity<Genre>().HasIndex(genre => genre.Id).IsUnique();
            modelBuilder.Entity<Actor>().HasIndex(actor => actor.Id).IsUnique();
            modelBuilder.Entity<Comment>().HasIndex(comment => comment.Id).IsUnique();*/
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        
        
    }
}