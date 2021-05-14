namespace FilmsDTO
{
    public class LightActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } 
                
        public LightActorDTO(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

        public LightActorDTO(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public LightActorDTO()
        {
        }

        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Surname)}: {Surname}]";
        }
    }
}