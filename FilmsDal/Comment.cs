using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FilmsDal
{
    public class Comment
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] 
        [Key] public int Id { get; set; }
        public string Content { get; set; }//Content ?
        public int Rate { get; set; }//de 1 à 5
        public string Username { get; set; }
        public DateTime Date { get; set; }
        
        public Film Film { get; set; } //one to many avec film

        public Comment(int id, string content, int rate, string username, DateTime date, Film film)
        : this(content, rate, username, date, film)
        {
            Id = id;
        }

        public Comment(string content, int rate, string username, DateTime date, Film film)
        {
            Content = content;
            Rate = rate;
            Username = username;
            Date = date;
            Film = film;
        }

        public Comment()
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