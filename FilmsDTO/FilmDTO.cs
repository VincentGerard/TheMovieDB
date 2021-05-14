using System;
using System.Collections.Generic;

namespace FilmsDTO
{
    public class FilmDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double VoteAverage { get; set; }//La cote qu’a obtenu le film suite aux votes des membres TMDb
        public int Runtime { get; set; }//durée du film en minute
        public string PosterPath { get; set; } //Un chemin permettant de construire l’URL vers une image de type poster pour le film
        public List<CommentDTO> Comments { get; set; } //one to many avec Comment

        public FilmDTO(int id, string title, DateTime releaseDate, double voteAverage, int runtime, string posterPath, List<CommentDTO> comments)
        : this(title, releaseDate, voteAverage, runtime, posterPath, comments)
        {
            Id = id;
        }

        public FilmDTO(string title, DateTime releaseDate, double voteAverage, int runtime, string posterPath, List<CommentDTO> comments)
        : this()
        {
            Title = title;
            ReleaseDate = releaseDate;
            VoteAverage = voteAverage;
            Runtime = runtime;
            PosterPath = posterPath;
            Comments = comments;
        }

        public FilmDTO()
        {
            Comments = new List<CommentDTO>(); 
        }

        public virtual string ToStringAll()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(ReleaseDate)}: {ReleaseDate}, {nameof(VoteAverage)}: {VoteAverage}, {nameof(Runtime)}: {Runtime}, {nameof(PosterPath)}: {PosterPath}, {nameof(Comments)}: {string.Join("/", Comments)}]";
        }
        
        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(ReleaseDate)}: {ReleaseDate}, {nameof(VoteAverage)}: {VoteAverage}, {nameof(Runtime)}: {Runtime}, {nameof(PosterPath)}: {PosterPath}]";
        }
    }
}