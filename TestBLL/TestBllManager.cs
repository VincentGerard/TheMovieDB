using System;
using System.Collections.Generic;
using System.Linq;
using FilmsBLL;
using FilmsDal;
using FilmsDTO;
using NUnit.Framework;

namespace TestBLL
{
    [TestFixture]
    public class TestBllManager
    {
        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetFilmsFromTo(int from, int to)
        {
            BllManager bllManager = new BllManager();
            List<FilmDTO> films = bllManager.getFilmsFromTo(from, to).ToList();
            Console.WriteLine("Films de " + from + " a " + to + " :");
            int i = 1;
            foreach (FilmDTO film in films)
            {
                Console.WriteLine("Film [" + i + "]" + " : " + film.ToStringAll());
                i++;
            }
            Console.WriteLine("fin de : " + "Films de " + from + " a " + to);
            
            Assert.Pass();
        }

        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetActorsFromTo(int from, int to)
        {
            BllManager bllManager = new BllManager();
            List<ActorDTO> actors = bllManager.getActorsFromTo(from, to).ToList();
            Console.WriteLine("Acteurs de " + from + " a " + to + " :");
            int i = 1;
            foreach (ActorDTO actor in actors)
            {
                Console.WriteLine("Acteurs [" + i + "]" + " : " + actor.ToStringWithFilms());
                i++;
            }
            Console.WriteLine("Acteurs de : " + "Acteurs de " + from + " a " + to);
            
            Assert.Pass();
        }

        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetGenresFromTo(int from, int to)
        {
            BllManager bllManager = new BllManager();
            List<GenreDTO> genres = bllManager.getGenresFromTo(from, to).ToList();

            Console.WriteLine("Genres de " + from + " a " + to + " :");
            int i = 1;
            foreach (GenreDTO genre in genres)
            {
                Console.WriteLine("Genres [" + i + "]" + " : " + genre/*.ToStringWithFilms()*/);
                i++;
            }
            Console.WriteLine("Genres de : " + "Genres de " + from + " a " + to);
            
            Assert.Pass();
        }

        [TestCase(0, 10)]
        [TestCase(10, 20)]
        [TestCase(0, 20)]
        [TestCase(0, 25)]
        public void testGetCommentsFromTo(int from, int to)
        {
            BllManager bllManager = new BllManager();
            List<CommentDTO> comments = bllManager.getCommentsFromTo(from, to).ToList();

            Console.WriteLine("Comments de " + from + " a " + to + " :");
            int i = 1;
            foreach (CommentDTO comment in comments)
            {
                Console.WriteLine("Comments [" + i + "]" + " : " + comment.ToStringWithFilm());
                i++;
            }
            Console.WriteLine("Comments de : " + "Comments de " + from + " a " + to);
            
            Assert.Pass();
        }
        
        [TestCase(0,10, 11)]//film star wars
        [TestCase(10,20, 11)]//film star wars
        [TestCase(0,25, 11)]//film star wars
        [TestCase(0,25, 10)]//aucun film pour l'id 10
        public void testGetListActorsByIdFilm(int from, int to, int idFilm)
        {
            BllManager bllManager = new BllManager();
            List<LightActorDTO> lightActorDtos = bllManager.GetListActorsByIdFilm(from, to, idFilm).ToList();
            Console.WriteLine("liste des acteurs pour le film : " + idFilm);
            int i = 0;
            foreach (LightActorDTO lightActorDto in lightActorDtos)
            {
                Console.WriteLine("["+i+"]"+lightActorDto);
                i++;
            }
            
            Assert.Pass();
        }
        
        [TestCase(11)]
        [TestCase(10)]
        public void testGetNumberActorsByIdFilm(int idFilm)
        {
            BllManager bllManager = new BllManager();
            Console.WriteLine("nombre d'acteur pour le fiml ("+idFilm+") = " + bllManager.GetListActorsByIdFilm(idFilm));
            
            Assert.Pass();
        }
        
        [TestCase(11)]
        [TestCase(10)]
        public void testGetListFilmTypesByIdFilm(int idFilm)
        {
            BllManager bllManager = new BllManager();
            List<GenreDTO> genreDtos = bllManager.GetListFilmTypesByIdFilm(idFilm);
            Console.WriteLine("Liste des genres pour le film : " + idFilm);
            foreach (GenreDTO genreDto in genreDtos)
            {
                Console.WriteLine(genreDto);
            }
            
            Assert.Pass();
        }

        [TestCase(0, 10, "james")]
        [TestCase(10, 20, "james")]
        [TestCase(0, 25, "james")]
        [TestCase(0, 25, "skjdghfdkjshfkjdshfkj")] //aucun acteur n'a ce nom
        [TestCase(0, 10, "Anthony James")] //nom + prénom
        [TestCase(0, 10, "a b c d")] //plus de 2 mots
        public void testFindListFilmByPartialActorName(int from, int to, string nomActeur)
        {
            BllManager bllManager = new BllManager();
            IQueryable<FilmDTO> testNull = bllManager.FindListFilmByPartialActorName(from, to, nomActeur);
            Console.WriteLine("Liste des films dans lequel un acteur dont on donne un nom partiellement ou entierement : " + nomActeur);
            if (testNull != null)
            {
                List<FilmDTO> films = testNull.ToList();
                int i = 1;
                Console.WriteLine("DEBUG nbr films = " + films.Count);
                foreach (FilmDTO film in films)
                {
                    Console.WriteLine("["+i+"]\n" + film.ToStringAll());
                    i++;
                }
            }
            
            Assert.Pass();
        }
        
        [TestCase(0,10)]
        [TestCase(10,20)]
        [TestCase(0,25)]
        public void TestGetFavoriteActors(int from, int to)
        {
            BllManager bllManager = new BllManager();
            List<LightActorDTO> actors = bllManager.GetFavoriteActors(from, to).ToList();
            Console.WriteLine("Acteur qui joue dans 2 films ou plus : de " + from + ", to " + to);
            int i = 1;
            foreach (LightActorDTO actor in actors)
            {
                Console.WriteLine("[" + i + "]" + actor);
                i++;
            }
            
            Assert.Pass();
        }
        
        [TestCase(11)]//Star Wars
        [TestCase(10)]//rien !
        public void testGetFullFilmDetailsByIdFilm(int idFilm)
        {
            BllManager bllManager = new BllManager();
            FullFilmDTO film = bllManager.GetFullFilmDetailsByIdFilm(idFilm);
            if(film != null)
                Console.WriteLine("Film pour l'id(" + idFilm + ") : \n" + film.ToStringAll());
            else
                Console.WriteLine("Pas de Film trouvé pour l'id : " + idFilm);
            
            Assert.Pass();
        }
        
        [Test]
        public void testGetNumberFilmsFromTo()
        {
            BllManager bllManager = new BllManager();
            Console.WriteLine("nombre de films dans la bd = " + bllManager.getFilmsFromTo());
        }
        
        [Test]
        public void testGetNumberActorsFromTo()
        {
            BllManager bllManager = new BllManager();
            Console.WriteLine("nombre d'acteurs dans la bd = " + bllManager.getActorsFromTo());
        }
        
        [Test]
        public void testGetNumberGenresFromTo()
        {
            BllManager bllManager = new BllManager();
            Console.WriteLine("nombre de genres dans la bd = " + bllManager.getGenresFromTo());
        }
        
        [Test]
        public void testGetNumberCommentsFromTo()
        {
            BllManager bllManager = new BllManager();
            Console.WriteLine("nombre de comments dans la bd = " + bllManager.getCommentsFromTo());
        }
        
        [TestCase("james")]
        [TestCase("skjdghfdkjshfkjdshfkj")]//aucun acteur n'a ce nom
        [TestCase("Anthony James")]//nom + prénom
        [TestCase("a b c d")]//plus de 2 mots
        public void testNumberFindListFilmByPartialActorName(string nomActeur)
        {
            BllManager bllManager = new BllManager();
            Console.WriteLine("nombre de films pour l'acteur dont le nom contient(" + nomActeur + ") = " + bllManager.FindListFilmByPartialActorName(nomActeur));
        }
        
        [Test]
        public void testGetNumberFavoriteActor()
        {
            BllManager bllManager = new BllManager();
            Console.WriteLine("nombre d'acteur qui jouent dans au moins 2 films : " + bllManager.GetFavoriteActors());
        }
        
        [TestCase(0,10, "star")]
        [TestCase(10,20, "star")]
        [TestCase(0,25, "star")]
        [TestCase(0,25, "skjdghfdkjshfkjdshfkj")]
        [TestCase(0,10, "star wars")]
        [TestCase(0,10, "a b c d")]
        public void testFindFilmsByPartialTitle(int from, int to, string titre)
        {
            BllManager bllManager = new BllManager();
            IQueryable<FilmDTO> testNull = bllManager.FindFilmsByPartialTitle(from, to, titre);
            Console.WriteLine("Liste des films dont le titre contient : " + titre);
            if (testNull != null)
            {
                List<FilmDTO> films = testNull.ToList();
                int i = 1;
                Console.WriteLine("DEBUG nbr films = " + films.Count);
                foreach (FilmDTO film in films)
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
            BllManager bllManager = new BllManager();
            
            Console.WriteLine("Nombre de films dont le titre contient(" + titre + ") =  " + bllManager.FindFilmsByPartialTitle(titre));
        }
        
    }
}