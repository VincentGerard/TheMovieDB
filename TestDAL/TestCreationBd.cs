using System;
using System.Linq;
using ConsoleMovieTxt;
using FilmsDal;
using LibraryProperties;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestDAL
{
    public class Tests
    {
        private FilmContext _filmContext = null;
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("setup");
            DbContextOptionsBuilder<FilmContext> optionsBuilder = new DbContextOptionsBuilder<FilmContext>();
            _filmContext = null;
            
            string databaseFullPath = Properties.getDatabaseFullPath();
            optionsBuilder.UseSqlite("Data Source=" + databaseFullPath + ";Cache=Shared");
            _filmContext = new FilmContext(optionsBuilder.Options);
            _filmContext.Database.EnsureDeleted();
            _filmContext.Database.EnsureCreated();
        }

        [Test]
        public void testInsert1Film()
        {
            try
            {
                Genre action = new Genre();
                action.Name = "Action";
                Genre adventure = new Genre();
                adventure.Name = "Adventure";
                Genre thriller = new Genre();
                thriller.Name = "Thriller";

                Actor SeanConnery = new Actor();
                SeanConnery.Name = "Sean";
                SeanConnery.Surname = "Connery";

                Film Goldfinger = new Film();
                Goldfinger.Title = "Goldfinger";
                Goldfinger.ReleaseDate = new DateTime(1964, 9, 20);
                Goldfinger.VoteAverage = 7.7;
                Goldfinger.Runtime = 110;
                Goldfinger.PosterPath = "inconnu";

                Goldfinger.Genres.Add(action);
                Goldfinger.Genres.Add(adventure);
                Goldfinger.Genres.Add(thriller);
                Goldfinger.Actors.Add(SeanConnery);

                _filmContext.Films.Add(Goldfinger);

                _filmContext.SaveChanges();

                AfficheAllBd();
                
                Assert.Pass();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [Test]
        public void testInsert1FilmFromTxt()
        {
            FilmParser filmParser = new FilmParser(this._filmContext);

            if (filmParser.readAndDecodeLine() == true)
            {
                Console.WriteLine("lecture d'un film OK");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Format du fichier :");
                Console.WriteLine(FilmParser.formatFichierTxt);
                Console.WriteLine("Debug : get line ");
                Console.WriteLine(filmParser.DebugGetLine);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------");
                AfficheAllBd();
                Console.WriteLine("----------------------------------------------------------");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("lecture d'un film FAIL");
                Assert.Fail();
            }
        }
    
        [Test]
        public void testInsert10FilmsFromTxt()
        {
            FilmParser filmParser = new FilmParser(this._filmContext);

            bool fail = false;
            for (int i = 0; i < 10; i++)
            {
                if (filmParser.readAndDecodeLine() == false)
                {
                    fail = true;
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("lecture du film " + i + " OK");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Format du fichier :");
                Console.WriteLine(FilmParser.formatFichierTxt);
                Console.WriteLine("Debug : get line ");
                Console.WriteLine(filmParser.DebugGetLine);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------");
                AfficheAllBd();
                Console.WriteLine("----------------------------------------------------------");
            }

            if (!fail)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        
        [Test]
        public void testInsert100FilmsFromTxt()
        {
            FilmParser filmParser = new FilmParser(this._filmContext);

            bool fail = false;
            for (int i = 0; i < 100; i++)
            {
                if (filmParser.readAndDecodeLine() == false)
                {
                    fail = true;
                    break;
                }
            }

            if (!fail)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        
        [Test]
        public void testInsert1000FilmsFromTxt()
        {
            FilmParser filmParser = new FilmParser(this._filmContext);

            bool fail = false;
            int i = 0;
            for (i = 0; i < 1000; i++)
            {
                if (filmParser.readAndDecodeLine() == false)
                {
                    fail = true;
                    break;
                }
            }
            
            Console.WriteLine("Nombre de films insérés : " + i);
            
            if (!fail)
            {
                
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        
        [Test]
        public void testInsert10000FilmsFromTxt()
        {
            Console.WriteLine("testInsertAllFilmsFromTxt");
            FilmParser filmParser = new FilmParser(this._filmContext);
            
            bool fail = false;
            int i = 0;
            for (i = 0; i < 10000; i++)
            {
                Console.WriteLine(i);
                if (filmParser.readAndDecodeLine() == false)
                {
                    fail = true;
                    break;
                }
            }
            
            Console.WriteLine("Nombre de films insérés : " + i);
            
            if (!fail)
            {
                
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        private void AfficheAllBd()
        {
            Console.WriteLine();
            Console.WriteLine("lecture des films a partir de la bd");
            try
            {
                foreach (Film film in _filmContext.Films.ToList())
                {
                    Console.WriteLine(film.ToStringAll());
                    Console.WriteLine("-");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Pas de film ? " + e.StackTrace);
            }
            Console.WriteLine("-----------------------------------------------");
            
            Console.WriteLine("lecture des acteurs a partir de la bd");
            try
            {
                foreach (Actor actor in _filmContext.Actors.ToList())
                {
                    Console.WriteLine(actor.ToStringWithFilms());
                    Console.WriteLine("-");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Pas d'acteur ? " + e.StackTrace);
            }
            Console.WriteLine("-----------------------------------------------");
            
            Console.WriteLine("lecture des genres a partir de la bd");
            try
            {
                foreach (Genre genre in _filmContext.Genres.ToList())
                {
                    Console.WriteLine(genre.ToStringWithFilms());
                    Console.WriteLine("-");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Pas de genre ? " + e.StackTrace);
            }
            Console.WriteLine("-----------------------------------------------");
            
            Console.WriteLine("lecture des comments a partir de la bd");
            try
            {
                foreach (Comment comment in _filmContext.Comments.ToList())
                {
                    Console.WriteLine(comment.ToStringWithFilm());
                    Console.WriteLine("-");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Pas de comment ? " + e.StackTrace);
            }
            Console.WriteLine("-----------------------------------------------");
        }
    }
}