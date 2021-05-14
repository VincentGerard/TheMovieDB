using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using FilmsDal;
using LibraryProperties;
using Microsoft.EntityFrameworkCore;

namespace ConsoleMovieTxt
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("------------------------");
            //Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location );
            //Console.WriteLine("------------------------");
            //Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            FilmContext _filmContext = null;
            Console.WriteLine("setup");
            DbContextOptionsBuilder<FilmContext> optionsBuilder = new DbContextOptionsBuilder<FilmContext>();
            _filmContext = null;
            
            string databaseFullPath = Properties.getDatabaseFullPath();
            optionsBuilder.UseSqlite("Data Source=" + databaseFullPath + ";Cache=Shared");
            _filmContext = new FilmContext(optionsBuilder.Options);
            _filmContext.Database.EnsureDeleted();
            _filmContext.Database.EnsureCreated();
            
            Console.WriteLine("testInsertAllFilmsFromTxt");
            FilmParser filmParser = new FilmParser(_filmContext);
            
            bool fail = false;
            int i = 0;
            for (i = 0; i < 995; i++)
            {
                Console.WriteLine(i);
                if (filmParser.readAndDecodeLine() == false)
                {
                    fail = true;
                    break;
                }
            }

            Console.WriteLine("Nombre de films insérés : " + i);
        }
    }
}