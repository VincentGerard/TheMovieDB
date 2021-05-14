using System;
using System.Collections.Generic;
using System.Linq;
using FilmsDal;
using NUnit.Framework;

namespace TestDAL
{
    [TestFixture]
    public class FilmManagerTest
    {
        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetFilmsFromTo(int from, int to)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            List<Film> films = filmManager.getFilmsFromTo(from, to).ToList();

            Console.WriteLine("Films de " + from + " a " + to + " :");
            int i = 1;
            foreach (Film film in films)
            {
                Console.WriteLine("Film [" + i + "]" + " : " + film.ToStringAll());
                i++;
            }
            Console.WriteLine("fin de : " + "Films de " + from + " a " + to);
        }
        
        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetActorsFromTo(int from, int to)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            List<Actor> actors = filmManager.getActorsFromTo(from, to).ToList();

            Console.WriteLine("Acteurs de " + from + " a " + to + " :");
            int i = 1;
            foreach (Actor actor in actors)
            {
                Console.WriteLine("Acteurs [" + i + "]" + " : " + actor.ToStringWithFilms());
                i++;
            }
            Console.WriteLine("Acteurs de : " + "Acteurs de " + from + " a " + to);
        }
        
        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetGenresFromTo(int from, int to)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            List<Genre> genres = filmManager.getGenresFromTo(from, to).ToList();

            Console.WriteLine("Genres de " + from + " a " + to + " :");
            int i = 1;
            foreach (Genre genre in genres)
            {
                Console.WriteLine("Genres [" + i + "]" + " : " + genre.ToStringWithFilms());
                i++;
            }
            Console.WriteLine("Genres de : " + "Genres de " + from + " a " + to);
        }
        
        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetCommentsFromTo(int from, int to)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            List<Comment> comments = filmManager.getCommentsFromTo(from, to).ToList();

            Console.WriteLine("Comments de " + from + " a " + to + " :");
            int i = 1;
            foreach (Comment comment in comments)
            {
                Console.WriteLine("Comments [" + i + "]" + " : " + comment.ToStringWithFilm());
                i++;
            }
            Console.WriteLine("Comments de : " + "Comments de " + from + " a " + to);
        }

        [TestCase(10)]//Bob Peterson
        [TestCase(15)]//personne !
        public void testGetActorFromId(int id)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Actor actor = filmManager.getActorFromId(id);
            if(actor != null)
                Console.WriteLine("Actor pour l'id(" + id + ") : " + actor.ToStringWithFilms());
            else
                Console.WriteLine("Pas d'actor trouvé pour l'id : " + id);
        }
        
        [TestCase(11)]//Star Wars
        [TestCase(10)]//rien !
        public void testGetFilmFromId(int id)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Film film = filmManager.getFilmFromId(id);
            if (film != null)
            {
                Console.WriteLine("Film pour l'id(" + id + ") : " + film.ToStringAll());
                Console.WriteLine("comments du film en détails : ");
                foreach (Comment filmComment in film.Comments)
                {
                    Console.WriteLine(filmComment.ToStringWithFilm());
                }
            }
            else
                Console.WriteLine("Pas de Film trouvé pour l'id : " + id);
        }
        
        [TestCase(0,10, "james")]
        [TestCase(10,20, "james")]
        [TestCase(0,25, "james")]
        [TestCase(0,25, "skjdghfdkjshfkjdshfkj")]//aucun acteur n'a ce nom
        [TestCase(0,10, "Anthony James")]//nom + prénom
        [TestCase(0,10, "a b c d")]//plus de 2 mots
        public void testFindListFilmByPartialActorName(int from, int to, string nomActeur)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            IQueryable<Film> testNull = filmManager.FindListFilmByPartialActorName(from, to, nomActeur);
            Console.WriteLine("Liste des films dans lequel un acteur dont on donne un nom partiellement ou entierement : " + nomActeur);
            if (testNull != null)
            {
                List<Film> films = testNull.ToList();
                int i = 1;
                Console.WriteLine("DEBUG nbr films = " + films.Count);
                foreach (Film film in films)
                {
                    Console.WriteLine("["+i+"]\n" + film.ToStringAll());
                    i++;
                }
            }
        }
        
        [TestCase(0,10)]
        [TestCase(10,20)]
        [TestCase(0,25)]
        public void TestGetFavoriteActors(int from, int to)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            List<Actor>actors = filmManager.GetFavoriteActors(from, to).ToList();
            Console.WriteLine("Acteur qui joue dans 2 films ou plus : de " + from + ", to " + to);
            int i = 1;
            foreach (Actor actor in actors)
            {
                Console.WriteLine("[" + i + "]" + actor.ToStringWithFilms());
                i++;
            }
        }

        [Test]
        public void testInsertDeleteComment()
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Comment comment = new Comment()
            {
                Content = "test de content pour le commentaire",
                Date = DateTime.Now,
                Film = filmManager.getFilmFromId(11)/*star wars*/,
                Rate = 19,
                Username = "jojo"
            };
        
            Console.WriteLine("insert comment...");
            filmManager.InsertComment(comment);
            Console.WriteLine("insert comment ok : affichage à partir de la lecture de la bd :");

            Film film = filmManager.getFilmFromId(comment.Film.Id);
            int idComment = 0;
            foreach (Comment filmComment in film.Comments)
            {
                idComment = filmComment.Id;
            }
        
            Console.WriteLine("Film : \n" + film.ToStringAll());
        
            Comment commentFromBd = filmManager.getCommentFromId(idComment);
            Console.WriteLine("Comment : \n" + commentFromBd.ToStringWithFilm());

            Console.WriteLine();
            Console.WriteLine("Delete comment...");
            filmManager.DeleteComment(commentFromBd);
            Console.WriteLine("Apres Delete comment...");
        
            film = filmManager.getFilmFromId(comment.Film.Id);
            Console.WriteLine("Film : \n" + film.ToStringAll());

            commentFromBd = filmManager.getCommentFromId(commentFromBd.Id);
            if(commentFromBd == null) Console.WriteLine("commentaire bien supprimé");
            else Console.WriteLine("commentaire pas supprimé : " + commentFromBd);
            
            Assert.Pass();
        }
    
        [Test]
        public void testGetNumberFilmsFromTo()
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Console.WriteLine("nombre de films dans la bd = " + filmManager.getFilmsFromTo());
        }
    
        [Test]
        public void testGetNumberActorsFromTo()
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Console.WriteLine("nombre d'acteurs dans la bd = " + filmManager.getActorsFromTo());
        }

        [Test]
        public void testGetNumberGenresFromTo()
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Console.WriteLine("nombre de genres dans la bd = " + filmManager.getGenresFromTo());
        }
        
        [Test]
        public void testGetNumberCommentsFromTo()
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Console.WriteLine("nombre de comments dans la bd = " + filmManager.getCommentsFromTo());
        }

        [TestCase("james")]
        [TestCase("skjdghfdkjshfkjdshfkj")]//aucun acteur n'a ce nom
        [TestCase("Anthony James")]//nom + prénom
        [TestCase("a b c d")]//plus de 2 mots
        public void testNumberFindListFilmByPartialActorName(string nomActeur)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Console.WriteLine("nombre de films pour l'acteur dont le nom contient(" + nomActeur + ") = " + filmManager.FindListFilmByPartialActorName(nomActeur));
        }

        [Test]
        public void testGetNumberFavoriteActor()
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            Console.WriteLine("nombre d'acteur qui jouent dans au moins 2 films : " + filmManager.GetFavoriteActors());
        }
        
        [TestCase(0,10, "star")]
        [TestCase(10,20, "star")]
        [TestCase(0,25, "star")]
        [TestCase(0,25, "skjdghfdkjshfkjdshfkj")]
        [TestCase(0,10, "star wars")]
        [TestCase(0,10, "a b c d")]
        public void testFindFilmsByPartialTitle(int from, int to, string titre)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            IQueryable<Film> testNull = filmManager.FindFilmsByPartialTitle(from, to, titre);
            Console.WriteLine("Liste des films dont le titre contient : " + titre);
            if (testNull != null)
            {
                List<Film> films = testNull.ToList();
                int i = 1;
                Console.WriteLine("DEBUG nbr films = " + films.Count);
                foreach (Film film in films)
                {
                    Console.WriteLine("["+i+"]\n" + film.ToStringAll());
                    i++;
                }
            }
        }
        
        [TestCase("star")]
        [TestCase("skjdghfdkjshfkjdshfkj")]
        [TestCase("star wars")]
        [TestCase("a b c d")]
        public void testFindNumberFilmsByPartialTitle(string titre)
        {
            FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
            
            Console.WriteLine("Nombre de films dont le titre contient(" + titre + ") =  " + filmManager.FindFilmsByPartialTitle(titre));
        }

        /*[Test]
        public void tmpDelete()
        {
            try
            {
                FilmManager filmManager = new FilmManager(GetDbContextOptions.getFilmDbContextOptions());
                Comment comment = filmManager.getCommentFromId(1);
                
                comment.Film = filmManager.getFilmFromId(11);
                Console.WriteLine("commment = " + comment.ToStringWithFilm());
                Console.WriteLine();
                Console.WriteLine("Delete comment...");
                filmManager.DeleteComment(comment);
                Console.WriteLine("Apres Delete comment...");

                Assert.Pass();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }*/

        /*[Test]
        public void testConstructeurFilm()
        {
            Film film = new Film(19, "titre", DateTime.Now, 7.9, 19, "image,", new List<Actor>()
            {
                new Actor()
                {
                    Films = new List<Film>(),
                    Id = 50,
                    Name = "jojo",
                    Surname = "surname"
                }
            }, new List<Genre>()
            {
                new Genre()
                {
                    Films = new List<Film>(),
                    Id = 30,
                    Name = "drame"
                }
            }, new List<Comment>()
            {
                new Comment()
                {
                    Content = "content comment",
                    Date = DateTime.Now,
                    Film = new Film(),
                    Id = 90,
                    Rate = 9,
                    Username = "test"
                }
            } );

            Console.WriteLine(film.ToStringAll());
        }*/
        
    }
}