using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FilmsDal
{
    public class FilmManager : IDisposable
    {
        private readonly FilmContext _filmContext;
        /*public FilmContext FilmContext
        {
            get { return this._filmContext; }
        }*/

        public FilmManager(DbContextOptions options)
        {
            _filmContext = new FilmContext(options);
            //_filmContext.Database.EnsureDeleted();
            //_filmContext.Database.EnsureCreated();
        }

        //RETURN from < FILMS <= to
        public IQueryable<Film> getFilmsFromTo(int from, int to)
        {
            try
            {
                return this._filmContext.Films.OrderBy(film => film.Title).Skip(from).Take((to-from)).Select(film => new Film()
                {
                    Id = film.Id,
                    Actors = film.Actors,
                    Comments = film.Comments,
                    Genres = film.Genres,
                    PosterPath = film.PosterPath,
                    Runtime = film.Runtime,
                    ReleaseDate = film.ReleaseDate,
                    Title = film.Title,
                    VoteAverage = film.VoteAverage
                });
                //retourner des films paginés
            }
            catch (Exception)
            {
                return null;
            }

        }
        
        public int getFilmsFromTo()
        {
            //return this._filmContext.Films.Count();
            return this._filmContext.Films.Select(film => film.Id).Count();
        }
        
        public Film getFilmFromId(int id)
        {
            try
            {
                return this._filmContext.Films.Select(film => new Film()
                {
                    Id = film.Id,
                    Actors = film.Actors,
                    Comments = film.Comments,
                    Genres = film.Genres,
                    PosterPath = film.PosterPath,
                    Runtime = film.Runtime,
                    ReleaseDate = film.ReleaseDate,
                    Title = film.Title,
                    VoteAverage = film.VoteAverage
                }).First(film => film.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public IQueryable<Film> FindFilmsByPartialTitle(int from, int to, string titre)
        {
            try
            {
                return this._filmContext.Films
                    .Where(film => film.Title.ToLower().Contains(titre.ToLower()))
                    .OrderBy(film => film.Title)
                    .Select(film => new Film()
                    {
                        Id = film.Id,
                        Actors = film.Actors,
                        Comments = film.Comments,
                        Genres = film.Genres,
                        PosterPath = film.PosterPath,
                        Runtime = film.Runtime,
                        ReleaseDate = film.ReleaseDate,
                        Title = film.Title,
                        VoteAverage = film.VoteAverage
                    })
                    .Skip(from).Take((to-from));
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public int FindFilmsByPartialTitle(string titre)
        {
            try
            {
                return this._filmContext.Films
                    .Where(film => film.Title.ToLower().Contains(titre.ToLower()))
                    .Select(film => new Film()
                    {
                        Id = film.Id,
                        Actors = film.Actors,
                        Comments = film.Comments,
                        Genres = film.Genres,
                        PosterPath = film.PosterPath,
                        Runtime = film.Runtime,
                        ReleaseDate = film.ReleaseDate,
                        Title = film.Title,
                        VoteAverage = film.VoteAverage
                    })
                    .Count();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        public IQueryable<Film> FindListFilmByPartialActorName(int from, int to, string nomActeur)
        {
            try
            {
                if (nomActeur.Contains(" "))
                {
                    string[] nomActeurSplit = nomActeur.Split(" ");
                    if (nomActeurSplit.Length > 2)
                    {
                        return null;
                    }

                    if (nomActeurSplit.Length == 2)
                    {
                        return this._filmContext.Films
                            .Where(film => film.Actors.Any(
                                actor => 
                                    actor.Name.ToLower().Contains(nomActeurSplit[0].ToLower()) 
                                    || actor.Name.ToLower().Contains(nomActeurSplit[1].ToLower()) 
                                    || actor.Surname.ToLower().Contains(nomActeurSplit[0].ToLower())
                                    || actor.Surname.ToLower().Contains(nomActeurSplit[1].ToLower()))
                            )
                            .Select(film => new Film()
                            {
                                Id = film.Id,
                                Actors = film.Actors,
                                Comments = film.Comments,
                                Genres = film.Genres,
                                PosterPath = film.PosterPath,
                                Runtime = film.Runtime,
                                ReleaseDate = film.ReleaseDate,
                                Title = film.Title,
                                VoteAverage = film.VoteAverage
                            })
                            .Skip(from).Take((to-from));
                    }
                }
                //1 seul mot
                return this._filmContext.Films
                    .Where(film => film.Actors.Any(
                        actor => actor.Name.ToLower().Contains(nomActeur.ToLower()) 
                                 || actor.Surname.ToLower().Contains(nomActeur.ToLower())))
                    .Select(film => new Film()
                    {
                        Id = film.Id,
                        Actors = film.Actors,
                        Comments = film.Comments,
                        Genres = film.Genres,
                        PosterPath = film.PosterPath,
                        Runtime = film.Runtime,
                        ReleaseDate = film.ReleaseDate,
                        Title = film.Title,
                        VoteAverage = film.VoteAverage
                    })
                    .Skip(from).Take((to-from));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int FindListFilmByPartialActorName(string nomActeur)
        {
            try
            {
                if (nomActeur.Contains(" "))
                {
                    string[] nomActeurSplit = nomActeur.Split(" ");
                    if (nomActeurSplit.Length > 2)
                    {
                        return -1;
                    }

                    if (nomActeurSplit.Length == 2)
                    {
                        return this._filmContext.Films
                            .Where(film => film.Actors.Any(
                                actor => 
                                    actor.Name.ToLower().Contains(nomActeurSplit[0].ToLower()) 
                                    || actor.Name.ToLower().Contains(nomActeurSplit[1].ToLower()) 
                                    || actor.Surname.ToLower().Contains(nomActeurSplit[0].ToLower())
                                    || actor.Surname.ToLower().Contains(nomActeurSplit[1].ToLower()))
                            )
                            .Select(film => new Film()
                            {
                                Id = film.Id,
                                Actors = film.Actors,
                                Comments = film.Comments,
                                Genres = film.Genres,
                                PosterPath = film.PosterPath,
                                Runtime = film.Runtime,
                                ReleaseDate = film.ReleaseDate,
                                Title = film.Title,
                                VoteAverage = film.VoteAverage
                            }).Count();
                    }
                }
                //1 seul mot
                return this._filmContext.Films
                    .Where(film => film.Actors.Any(
                        actor => actor.Name.ToLower().Contains(nomActeur.ToLower()) 
                                 || actor.Surname.ToLower().Contains(nomActeur.ToLower())))
                    .Select(film => new Film()
                    {
                        Id = film.Id,
                        Actors = film.Actors,
                        Comments = film.Comments,
                        Genres = film.Genres,
                        PosterPath = film.PosterPath,
                        Runtime = film.Runtime,
                        ReleaseDate = film.ReleaseDate,
                        Title = film.Title,
                        VoteAverage = film.VoteAverage
                    }).Count();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //RETURN from < ACTORS <= to
        public IQueryable<Actor> getActorsFromTo(int from, int to)
        {
            try
            {
                return this._filmContext.Actors.Skip(from).Take((to-from)).Select(actor => new Actor()
                {
                    Films = actor.Films,
                    Id = actor.Id,
                    Name = actor.Name,
                    Surname = actor.Surname
                });
                //retourner des acteurs paginés
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        
        public int getActorsFromTo()
        {
            return this._filmContext.Actors.Count();
        }

        public Actor getActorFromId(int id)
        {
            try
            {
                return this._filmContext.Actors.Select(actor => new Actor()
                {
                    Films = actor.Films,
                    Id = actor.Id,
                    Name = actor.Name,
                    Surname = actor.Surname
                }).First(actor => actor.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<Actor> GetFavoriteActors(int from, int to)
        {
            try
            {
                return this._filmContext.Actors.Where(actor => actor.Films.Count >= 2).Select(actor => new Actor()
                {
                    Films = actor.Films,
                    Id = actor.Id,
                    Name = actor.Name,
                    Surname = actor.Surname
                }).Skip(from).Take((to-from));
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        
        public int GetFavoriteActors()
        {
            try
            {
                return this._filmContext.Actors.Where(actor => actor.Films.Count >= 2).Select(actor => new Actor()
                {
                    Films = actor.Films,
                    Id = actor.Id,
                    Name = actor.Name,
                    Surname = actor.Surname
                }).Count();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        //RETURN from < GENRES <= to
        public IQueryable<Genre> getGenresFromTo(int from, int to)
        {
            try
            {
                return this._filmContext.Genres.Skip(from).Take((to-from)).Select(genre => new Genre()
                {
                    Films = genre.Films,
                    Id = genre.Id,
                    Name = genre.Name
                });
                //retourner des genres paginés
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public int getGenresFromTo()
        {
            return this._filmContext.Genres.Count();
        }
        
        //RETURN from < COMMENTS <= to
        public IQueryable<Comment> getCommentsFromTo(int from, int to)
        {
            try
            {
                return this._filmContext.Comments.Skip(from).Take((to-from)).Select(comment => new Comment()
                {
                    Content = comment.Content,
                    Date = comment.Date,
                    Film = comment.Film,
                    Id = comment.Id,
                    Rate = comment.Rate,
                    Username = comment.Username
                });
                //retourner des comments paginés
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public int getCommentsFromTo()
        {
            return this._filmContext.Comments.Count();
        }
        
        public Comment getCommentFromId(int id)
        {
            try
            {
                return this._filmContext.Comments.Select(comment => new Comment()
                {
                    Content = comment.Content,
                    Date = comment.Date,
                    Film = comment.Film,
                    Id = comment.Id,
                    Rate = comment.Rate,
                    Username = comment.Username
                }).First(comment => comment.Id == id);
                //retourner des comments paginés
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public int InsertComment(Comment comment)
        {
            try
            {
                this._filmContext.SaveChanges();
                Film film = this._filmContext.Films.First(film1 => film1.Id == comment.Film.Id);
                film.Comments.Add(comment);
                //Console.WriteLine("film avant save changes :");
                //Console.WriteLine(film.ToStringAll());
                this._filmContext.Films.Update(film);
                this._filmContext.SaveChanges();

                return comment.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void DeleteComment(Comment comment)
        {
            try
            {
                this._filmContext.SaveChanges();

                //delete du comment du coté du film
                Film filmUpdate = this._filmContext.Films.Select(film => new Film()
                {
                    Id = film.Id,
                    Actors = film.Actors,
                    Comments = film.Comments,
                    Genres = film.Genres,
                    PosterPath = film.PosterPath,
                    Runtime = film.Runtime,
                    ReleaseDate = film.ReleaseDate,
                    Title = film.Title,
                    VoteAverage = film.VoteAverage
                }).First(film => film.Id == comment.Film.Id);
                if (filmUpdate.Comments.Remove(filmUpdate.Comments.First(comment1 => comment1.Id == comment.Id)) == false)
                    throw new Exception("Erreur lors du remove du comment(" + comment.Id + ") du film(" + filmUpdate.Id +
                                        ")");
                this._filmContext.SaveChanges();

                //delete du comment
                Comment deleteComment = this._filmContext.Comments.First(comment1 => comment1.Id == comment.Id);
                this._filmContext.Remove(deleteComment);
                this._filmContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            _filmContext?.Dispose();
        }
    }
}