using System;

namespace FilmsDTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }//Content ?
        public int Rate { get; set; }//de 1 à 5
        public string Username { get; set; }
        public DateTime Date { get; set; }        
        public FilmDTO Film { get; set; } //one to many avec film

        public CommentDTO(int id, string content, int rate, string username, DateTime date, FilmDTO film)
        : this(content, rate, username, date, film)
        {
            Id = id;
        }

        public CommentDTO(string content, int rate, string username, DateTime date, FilmDTO film)
        {
            Content = content;
            Rate = rate;
            Username = username;
            Date = date;
            Film = film;
        }

        public CommentDTO()
        {
        }

        public string ToStringWithFilm()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Content)}: {Content}, {nameof(Rate)}: {Rate}, {nameof(Username)}: {Username}, {nameof(Date)}: {Date}, {nameof(Film)}: {Film}]";
        }
        
        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Content)}: {Content}, {nameof(Rate)}: {Rate}, {nameof(Username)}: {Username}, {nameof(Date)}: {Date}]";
        }
    }
}