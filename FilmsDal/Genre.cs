using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmsDal
{
    public class Genre
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Film> Films { get; set; }//relation many-to-many avec Film
        
        public Genre(string text) : this() // Constructeur de FilmType (type de film)
        {
            string[] genredetail;
            Char[] delimiterChars = { '\u2024' };
            genredetail = text.Split(delimiterChars);
            //Id = Int32.Parse(genredetail[0]);
            Name = genredetail[1];
        }

        public Genre(int id, string name, List<Film> films) : this(name, films)
        {
            Id = id;
        }

        public Genre(string name, List<Film> films) : this()
        {
            Name = name;
            Films = films;
        }

        public Genre()
        {
            Films = new List<Film>();
        }

        public string ToStringWithFilms()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Films)}: {string.Join("/", Films)}]";
        }
        
        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}]";
        }
    }
}