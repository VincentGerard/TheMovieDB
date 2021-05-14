using System.Collections.Generic;

namespace FilmsDTO
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<FilmDTO> Films { get; set; }//relation many-to-many avec Film
                
        public ActorDTO(int id, string name, string surname, List<FilmDTO> films) 
            : this(name, surname, films)
        {
            Id = id;
        }

        public ActorDTO(string name, string surname, List<FilmDTO> films) : this()
        {
            Name = name;
            Surname = surname;
            Films = films;
        }

        public ActorDTO()
        {
            Films = new List<FilmDTO>();
        }

        public string ToStringWithFilms()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Surname)}: {Surname}, {nameof(Films)}: {string.Join("/", Films)}]";
        }
        
        public override string ToString()
        {
            return this.Name + " " + this.Surname;
        }
    }
}