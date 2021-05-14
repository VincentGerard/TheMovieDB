using System.Collections.Generic;

namespace FilmsDTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<FilmDTO> Films { get; set; }//relation many-to-many avec Film
        

        public GenreDTO(int id, string name/*, List<FilmDTO> films*/)
        {
            Id = id;
            Name = name;
            //Films = films;
        }

        public GenreDTO(string name/*, List<FilmDTO> films*/)
        {
            Name = name;
            //Films = films;
        }

        public GenreDTO()
        {
            //Films = new List<FilmDTO>();
        }

        // public string ToStringWithFilms()
        // {
        //     return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Films)}: {string.Join("/", Films)}]";
        // }
        
        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}]";
        }
    }
}