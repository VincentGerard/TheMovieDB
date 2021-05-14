using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsDal
{
    public class Film
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double VoteAverage { get; set; }//La cote qu’a obtenu le film suite aux votes des membres TMDb
        public int Runtime { get; set; }//durée du film en minute
        public string PosterPath { get; set; } //Un chemin permettant de construire l’URL vers une image de type poster pour le film
        
        public List<Actor> Actors { get; set; }//relation many-to-many avec Actor
        public List<Genre> Genres { get; set; }//relation many-to-many avec Genre
        public List<Comment> Comments { get; set; } //one to many avec Comment

        public Film(int id, string title, DateTime releaseDate, double voteAverage, int runtime, string posterPath, List<Actor> actors, List<Genre> genres, List<Comment> comments) 
            : this(title, releaseDate, voteAverage, runtime, posterPath, actors, genres, comments)
        {
            Id = id;
        }

        public Film(string title, DateTime releaseDate, double voteAverage, int runtime, string posterPath, List<Actor> actors, List<Genre> genres, List<Comment> comments) : this()
        {
            Title = title;
            ReleaseDate = releaseDate;
            VoteAverage = voteAverage;
            Runtime = runtime;
            PosterPath = posterPath;
            Actors = actors;
            Genres = genres;
            Comments = comments;
        }
        
        public Film(int id, string title, DateTime releaseDate, double voteAverage, int runtime, string posterPath, List<Comment> comments) : this()
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            VoteAverage = voteAverage;
            Runtime = runtime;
            PosterPath = posterPath;
            Comments = comments;
        }

        public Film()
        {
            Actors = new List<Actor>();
            Comments = new List<Comment>();
            Genres = new List<Genre>();
        }

        public string ToStringAll()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(ReleaseDate)}: {ReleaseDate}, {nameof(VoteAverage)}: {VoteAverage}, {nameof(Runtime)}: {Runtime}, {nameof(PosterPath)}: {PosterPath}, {nameof(Actors)}: {string.Join("/", Actors)}, {nameof(Genres)}: {string.Join("/", Genres)}, {nameof(Comments)}: {string.Join("/", Comments)}]";
        }
        
        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(ReleaseDate)}: {ReleaseDate}, {nameof(VoteAverage)}: {VoteAverage}, {nameof(Runtime)}: {Runtime}, {nameof(PosterPath)}: {PosterPath}]";
        }
        
        
    }
}