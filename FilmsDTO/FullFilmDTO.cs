using System;
using System.Collections.Generic;

namespace FilmsDTO
{
    public class FullFilmDTO : FilmDTO
    {
        public List<ActorDTO> Actors { get; set; }//relation many-to-many avec Actor
        public List<GenreDTO> Genres { get; set; }//relation many-to-many avec Genre

        public FullFilmDTO(int id, string title, DateTime releaseDate, double voteAverage, int runtime, 
            string posterPath, List<ActorDTO> actors, List<GenreDTO> genres, List<CommentDTO> comments)
        : base(id, title, releaseDate, voteAverage, runtime, posterPath, comments)
        {
            Actors = actors;
            Genres = genres;
        }

        public FullFilmDTO(string title, DateTime releaseDate, double voteAverage, int runtime, 
            string posterPath, List<ActorDTO> actors, List<GenreDTO> genres, List<CommentDTO> comments)
        : base(title, releaseDate, voteAverage, runtime, posterPath, comments)
        {
            Actors = actors;
            Genres = genres;
        }

        public FullFilmDTO() : base()
        {
            Actors = new List<ActorDTO>();
            Genres = new List<GenreDTO>();
        }

        public override string ToStringAll()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(ReleaseDate)}: {ReleaseDate}, {nameof(VoteAverage)}: {VoteAverage}, {nameof(Runtime)}: {Runtime}, {nameof(PosterPath)}: {PosterPath}, \n{nameof(Actors)}: {string.Join("/", Actors)}, \n{nameof(Genres)}: {string.Join("/", Genres)}, \n{nameof(Comments)}: {string.Join("/", Comments)}]";
        }
        
        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(ReleaseDate)}: {ReleaseDate}, {nameof(VoteAverage)}: {VoteAverage}, {nameof(Runtime)}: {Runtime}, {nameof(PosterPath)}: {PosterPath}]";
        }
    }
}