using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsDal
{
    public class Actor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Film> Films { get; set; }//relation many-to-many avec Film
        
        public Actor(string text) : this()// Constructeur d’objet Actor
        {
            //surname = nom de famille
            //name = prenom
            //si 1 mot uniquement name
            string[] acteurdetail;
            //string tmp;
            Char[] delimiterChars = { '\u2024' };
            acteurdetail = text.Split(delimiterChars);
            Id = Int32.Parse(acteurdetail[0]);
            SetNameSurnameFromTxt(acteurdetail[1]);
            //delimiterChars[0] = '/';
            //tmp = acteurdetail[2];
            //string[] characterdetail = tmp.Split(delimiterChars);
        }

        private void SetNameSurnameFromTxt(string txt)
        {
            string[] nomPrenom = txt.Split(" ");
            /*Console.WriteLine("DEBUG nomPrenom :");
            if (nomPrenom != null)
            {
                int i = 0;
                foreach (string s in nomPrenom)
                {
                    Console.WriteLine("(" + i + ") = " + s);
                    i++;
                }
            }*/

            if (nomPrenom.Length == 1)
            {
                Name = nomPrenom[0];
                Surname = "";
            }
            else if (nomPrenom.Length == 2)
            {
                Name = nomPrenom[0];
                Surname = nomPrenom[1];
            }
            else //nomPrenom.Length == 0
            {
                Name = "Pas de nom";
                Surname = "";
            }

            //Console.WriteLine("DEBUG fin de la fonction SetNameSurnameFromTxt : Name = " + Name + ", Surname = " + Surname);
        }

        public Actor(int id, string name, string surname, List<Film> films) : this(name, surname, films)
        {
            Id = id;
        }

        public Actor(string name, string surname, List<Film> films) : this()
        {
            Name = name;
            Surname = surname;
            Films = films;
        }

        public Actor()
        {
            Films = new List<Film>();
        }

        public string ToStringWithFilms()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Surname)}: {Surname}, {nameof(Films)}: {string.Join("/", Films)}]";
        }
        
        public override string ToString()
        {
            return $"[{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Surname)}: {Surname}]";
        }
    }
}