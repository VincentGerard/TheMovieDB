using System;
using System.Collections.Generic;
using System.Linq;
using FilmsDal;
using FilmsDTO;
using LibraryProperties;
using Microsoft.EntityFrameworkCore;

namespace FilmsBLL
{
    public class BllManager : IDisposable
    {
        private readonly FilmManager _filmManager;

        public BllManager()
        {
			string databaseFullPath =
				"C:\\Users\\Vincent\\OneDrive - Enseignement de la Province de Liège\\Cours\\B3\\.Net\\Labo\\Final\\laboprognet-2227-gerard-zeevaert\\filmsSqlite.db";
			//arnaud :
			//"C:\\Users\\admin\\OneDrive - Enseignement de la Province de Liège\\laboprognet_2227_gerard_zeevaert\\GestionFilms\\laboprognet-2227-gerard-zeevaert\\filmsSqlite.db"

			//vinc:
			//"C:\\Users\\Vincent\\OneDrive - Enseignement de la Province de Liège\\Cours\\B3\\.Net\\Labo\\Final\\laboprognet-2227-gerard-zeevaert\\filmsSqlite.db"
			DbContextOptionsBuilder<FilmContext> optionsBuilder = new DbContextOptionsBuilder<FilmContext>();
            Console.WriteLine("DEBUG FULL PATH = " + databaseFullPath);
            optionsBuilder.UseSqlite("Data Source=" + databaseFullPath + ";Cache=Shared");
            _filmManager = new FilmManager(optionsBuilder.Options);
        }
        
        /*public BllManager(DbContextOptionsBuilder<FilmContext> optionsBuilder)
        {
            _filmManager = new FilmManager(optionsBuilder.Options);
        }*/

        public void Dispose()
        {
            _filmManager?.Dispose();
        }
        
        //RETURN from < FILMS <= to
        public IQueryable<FilmDTO> getFilmsFromTo(int from, int to)
        {
            try
            {
                return this._filmManager.getFilmsFromTo(from, to).Select(film => new FilmDTO()
                {
                    //Actors = HelperBll.convertActors(film.Actors),
                    Comments = HelperBll.convertComments(film.Comments),
                    //Genres = HelperBll.convertGenres(film.Genres),
                    Id = film.Id,
                    PosterPath = film.PosterPath,
                    ReleaseDate = film.ReleaseDate,
                    Runtime = film.Runtime,
                    Title = film.Title,
                    VoteAverage = film.VoteAverage

                });
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public int getFilmsFromTo()
        {
            return this._filmManager.getFilmsFromTo();
        }

        //RETURN from < ACTORS <= to
        public IQueryable<ActorDTO> getActorsFromTo(int from, int to)
        {
            try
            {
                return this._filmManager.getActorsFromTo(from, to).Select(actor => new ActorDTO()
                {
                    Films = HelperBll.convertFilms(actor.Films),
                    Id = actor.Id,
                    Name = actor.Name,
                    Surname = actor.Surname
                });
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public int getActorsFromTo()
        {
            return this._filmManager.getActorsFromTo();
        }
        
        public ActorDTO getActorFromId(int id)
        {
            return HelperBll.convertActor(this._filmManager.getActorFromId(id));
        }
        
        //RETURN from < GENRES <= to
        public IQueryable<GenreDTO> getGenresFromTo(int from, int to)
        {
            try
            {
                return this._filmManager.getGenresFromTo(from, to).Select(genre => new GenreDTO()
                {
                    //Films = HelperBll.convertFilms(genre.Films),
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public int getGenresFromTo()
        {
            return this._filmManager.getGenresFromTo();
        }
        
        //RETURN from < COMMENTS <= to
        public IQueryable<CommentDTO> getCommentsFromTo(int from, int to)
        {
            try
            {
                return this._filmManager.getCommentsFromTo(from, to).Select(comment => new CommentDTO()
                {
                    Content = comment.Content,
                    Date = comment.Date,
                    Film = HelperBll.convertFilm(comment.Film),
                    Id = comment.Id,
                    Rate = comment.Rate,
                    Username = comment.Username
                });
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public int getCommentsFromTo()
        {
            return this._filmManager.getCommentsFromTo();
        }
        
        public CommentDTO getCommentFromId(int id)
        {
            try
            {
                return HelperBll.convertComment(this._filmManager.getCommentFromId(id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        //public IQueryable<Film> getFilmsFromToWhere(int from, int to, Func<Film, bool> whereCondition)
        //{
        //    return this._filmContext.Films.Where(whereCondition).Skip(from).Take((to-from));
        //    //retourner des films paginés
        //}
        
        // GetListActorsByIdFilm(...) : récupère la liste des acteurs d’un film 
        public List<LightActorDTO> GetListActorsByIdFilm(int from, int to, int idFilm)
        {
            try
            {
                Film film = this._filmManager.getFilmFromId(idFilm);
                if(film == null) return new List<LightActorDTO>();

                List<LightActorDTO> lightActorDtos = new List<LightActorDTO>();
                List<Actor> filmActors = film.Actors.Skip(from).Take((to - from)).ToList();
                foreach (Actor filmActor in filmActors)
                {
                    lightActorDtos.Add(HelperBll.ConvertLightActorDto(filmActor));
                }

                return lightActorDtos;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public int GetListActorsByIdFilm(int idFilm)
        {
            try
            {
                Film film = this._filmManager.getFilmFromId(idFilm);
                if(film == null) return -1;

                return film.Actors.Count;
            }
            catch (Exception)
            {
                return -1;
            }
            
        }
        
        // GetListFilmTypesByIdFilm(...) : récupère la liste des genres d’un film
        public List<GenreDTO> GetListFilmTypesByIdFilm(int idFilm)
        {
            try
            {
                Film film = this._filmManager.getFilmFromId(idFilm);
                if(film == null) return new List<GenreDTO>();
            
                List<GenreDTO> genreDtos = new List<GenreDTO>();
                foreach (Genre filmGenre in film.Genres)
                {
                    genreDtos.Add(HelperBll.convertGenre(filmGenre));
                }

                return genreDtos;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        
        // FindListFilmByPartialActorName(…) : récupère la liste des films (List<FilmDTO>)
        // dans lequel l’acteur dont on donne un nom partiellement ou entièrement 
        public IQueryable<FilmDTO> FindListFilmByPartialActorName(int from, int to, string nomActeur)
        {
            try
            {
                return this._filmManager.FindListFilmByPartialActorName(from, to, nomActeur).Select(film => new FilmDTO()
                {
                    //Actors = HelperBll.convertActors(film.Actors),
                    Comments = HelperBll.convertComments(film.Comments),
                    //Genres = HelperBll.convertGenres(film.Genres),
                    Id = film.Id,
                    PosterPath = film.PosterPath,
                    ReleaseDate = film.ReleaseDate,
                    Runtime = film.Runtime,
                    Title = film.Title,
                    VoteAverage = film.VoteAverage
                });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int FindListFilmByPartialActorName(string nomActeur)
        {
            return this._filmManager.FindListFilmByPartialActorName(nomActeur);
        }
        
        public IQueryable<FilmDTO> FindFilmsByPartialTitle(int from, int to, string nomActeur)
        {
            try
            {
                return this._filmManager.FindFilmsByPartialTitle(from, to, nomActeur).Select(film => new FilmDTO()
                {
                    //Actors = HelperBll.convertActors(film.Actors),
                    Comments = HelperBll.convertComments(film.Comments),
                    //Genres = HelperBll.convertGenres(film.Genres),
                    Id = film.Id,
                    PosterPath = film.PosterPath,
                    ReleaseDate = film.ReleaseDate,
                    Runtime = film.Runtime,
                    Title = film.Title,
                    VoteAverage = film.VoteAverage
                });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int FindFilmsByPartialTitle(string nomActeur)
        {
            return this._filmManager.FindFilmsByPartialTitle(nomActeur);
        }

        //d. GetFavoriteActors(…) : récupère la liste des acteurs ayant joué dans 2 films au moins.
        public IQueryable<LightActorDTO> GetFavoriteActors(int from, int to)
        {
            try
            {
                return this._filmManager.GetFavoriteActors(from, to).Select(actor => new LightActorDTO()
                {
                    Id = actor.Id,
                    Name = actor.Name,
                    Surname = actor.Surname
                });
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public int GetFavoriteActors()
        {
            return this._filmManager.GetFavoriteActors();
        }
        
        //GetFullFilmDetailsByIdFilm(…) : récupère la totalité des données d’un film dont on donne l’Id.
        public FullFilmDTO GetFullFilmDetailsByIdFilm(int idFilm)
        {
            try
            {
                return HelperBll.convertFullFilm(this._filmManager.getFilmFromId(idFilm));
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public FilmDTO GetFilmById(int idFilm)
        {
            try
            {
                return HelperBll.convertFilm(this._filmManager.getFilmFromId(idFilm));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int InsertComment(CommentDTO commentDto)
        {
            try
            {
                return this._filmManager.InsertComment(HelperBll.convertComment(commentDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteComment(CommentDTO commentDto)
        {
            try
            {
                this._filmManager.DeleteComment(HelperBll.convertComment(commentDto));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        /*
        e. GetFullFilmDetailsByIdFilm(…) : récupère la totalité des données d’un film dont
            on donne l’Id. Cette méthode retourne un objet de la classe FullFilmDTO qui
        réutilise une classe déjà créée plus tôt et qui contient les détails en supplément.
            
            f. InsertComment(…) : insère un nouveau commentaire sur un film
            identifié par son Id. Les données doivent être initialisés dans les couches
        supérieure dans un objet CommentDTO passé en paramètre à cette méthode.*/

    }
}