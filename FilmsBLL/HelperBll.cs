using System;
using System.Collections.Generic;
using FilmsDal;
using FilmsDTO;

namespace FilmsBLL
{
    public class HelperBll
    {
        public static FilmDTO convertFilm(Film film)
        {
            List<CommentDTO> commentsDtoDuFilm = new List<CommentDTO>();
            if (film == null) return null;
            
            if (film.Comments != null)
            {
                foreach (Comment filmComment in film.Comments)
                {
                    commentsDtoDuFilm.Add(new CommentDTO()
                    {
                        Content = filmComment.Content,
                        Date = filmComment.Date,
                        Film = null,
                        Id = filmComment.Id,
                        Rate = filmComment.Rate,
                        Username = filmComment.Username
                    });
                }
            }
            return new FilmDTO(film.Id, film.Title, film.ReleaseDate, film.VoteAverage, film.Runtime,
                film.PosterPath, /*actorsDtoDuFilm, genresDtoDuFilm,*/ commentsDtoDuFilm);
        }
        
        public static Film convertFilm(FilmDTO filmDto)
        {
            List<Comment> commentsDuFilm = new List<Comment>();

            if (filmDto == null) return null;
            
            if (filmDto.Comments != null)
            {
                foreach (CommentDTO filmComment in filmDto.Comments)
                {
                    commentsDuFilm.Add(new Comment()
                    {
                        Content = filmComment.Content,
                        Date = filmComment.Date,
                        Film = null,
                        Id = filmComment.Id,
                        Rate = filmComment.Rate,
                        Username = filmComment.Username
                    });
                }
            }
            return new Film(filmDto.Id, filmDto.Title, filmDto.ReleaseDate, filmDto.VoteAverage, filmDto.Runtime,
                filmDto.PosterPath, commentsDuFilm);
        }
        
        public static FullFilmDTO convertFullFilm(Film film)
        {
            List<ActorDTO> actorsDtoDuFilm = new List<ActorDTO>();
            
            if (film == null) return null;
            
            if (film.Actors != null)
            {
                foreach (Actor filmActor in film.Actors)
                {
                    actorsDtoDuFilm.Add(new ActorDTO()
                    {
                        Id = filmActor.Id,
                        Name = filmActor.Name,
                        Surname = filmActor.Surname
                    });
                }
            }

            List<GenreDTO> genresDtoDuFilm = new List<GenreDTO>();
            if (film.Genres != null)
            {
                foreach (Genre filmGenre in film.Genres)
                {
                    genresDtoDuFilm.Add(new GenreDTO()
                    {
                        Id = filmGenre.Id,
                        Name = filmGenre.Name
                    });
                }
            }

            List<CommentDTO> commentsDtoDuFilm = new List<CommentDTO>();
            if (film.Comments != null)
            {
                foreach (Comment filmComment in film.Comments)
                {
                    commentsDtoDuFilm.Add(new CommentDTO()
                    {
                        Content = filmComment.Content,
                        Date = filmComment.Date,
                        Film = null,
                        Id = filmComment.Id,
                        Rate = filmComment.Rate,
                        Username = filmComment.Username
                    });
                }
            }
            return new FullFilmDTO(film.Id, film.Title, film.ReleaseDate, film.VoteAverage, film.Runtime,
                film.PosterPath, actorsDtoDuFilm, genresDtoDuFilm, commentsDtoDuFilm);
        }

        public static List<FilmDTO> convertFilms(List<Film> films)
        {
            List<FilmDTO> filmDtos = new List<FilmDTO>();
            if (films != null)
            {
                foreach (Film film in films)
                {
                    filmDtos.Add(convertFilm(film));
                }
            }

            return filmDtos;
        }

        public static CommentDTO convertComment(Comment comment)
        {
            if(comment != null) 
                return new CommentDTO(comment.Id, comment.Content, comment.Rate, comment.Username, comment.Date, convertFilm(comment.Film));
            return null;
        }
        
        public static Comment convertComment(CommentDTO commentDto)
        {
            if(commentDto != null)
                return new Comment(commentDto.Id, commentDto.Content, commentDto.Rate, commentDto.Username, commentDto.Date, convertFilm(commentDto.Film));
            return null;
        }

        public static List<CommentDTO> convertComments(List<Comment> comments)
        {
            List<CommentDTO> commentDtos = new List<CommentDTO>();
            if (comments != null)
            {
                foreach (Comment comment in comments)
                {
                    commentDtos.Add(convertComment(comment));
                }
            }

            return commentDtos;
        }

        public static GenreDTO convertGenre(Genre genre)
        {
            //List<FilmDTO> filmsDtoDeGenre = new List<FilmDTO>();

            if (genre == null) return null;
            
            // if (genre.Films != null)
            // {
            //     foreach (Film genreFilm in genre.Films)
            //     {
            //         filmsDtoDeGenre.Add(convertFilm(genreFilm));
            //     }
            // }
            return new GenreDTO(genre.Id, genre.Name/*, filmsDtoDeGenre*/);
        }

        public static List<GenreDTO> convertGenres(List<Genre> genres)
        {
            List<GenreDTO> genreDtos = new List<GenreDTO>();
            if (genres != null)
            {
                foreach (Genre genre in genres)
                {
                    genreDtos.Add(convertGenre(genre));
                }
            }

            return genreDtos;
        }

        public static ActorDTO convertActor(Actor actor)
        {
            List<FilmDTO> filmsDtoDeActor = new List<FilmDTO>();

            if (actor == null) return null;
            
            if (actor.Films != null)
            {
                foreach (Film actorFilm in actor.Films)
                {
                    filmsDtoDeActor.Add(convertFilm(actorFilm));
                }
            }
            return new ActorDTO(actor.Id, actor.Name, actor.Surname, filmsDtoDeActor);
        }

        public static List<ActorDTO> convertActors(List<Actor> actors)
        {
            List<ActorDTO> actorDtos = new List<ActorDTO>();
            if (actors != null)
            {
                foreach (Actor actor in actors)
                {
                    actorDtos.Add(convertActor(actor));
                }
            }

            return actorDtos;
        }

        public static LightActorDTO ConvertLightActorDto(Actor actor)
        {
            if(actor != null)
                return new LightActorDTO(actor.Id, actor.Name, actor.Surname);
            return null;
        }
    }
}