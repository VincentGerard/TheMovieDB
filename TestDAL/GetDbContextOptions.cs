using FilmsDal;
using LibraryProperties;
using Microsoft.EntityFrameworkCore;

namespace TestDAL
{
    public class GetDbContextOptions
    {
        public static DbContextOptions getFilmDbContextOptions()
        {
            DbContextOptionsBuilder<FilmContext> optionsBuilder = new DbContextOptionsBuilder<FilmContext>();

            string databaseFullPath = Properties.getDatabaseFullPath();
            optionsBuilder.UseSqlite("Data Source=" + databaseFullPath + ";Cache=Shared");
            return optionsBuilder.Options;
        }
    }
}