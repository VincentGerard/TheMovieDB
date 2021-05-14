using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilmsDal;
using LibraryProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleMovieTxt
{
    public class FilmParser
    {
        private StreamReader _streamReader;
        private string _line;
        public static string formatFichierTxt = "_id‣title‣original_title‣release_date‣status‣vote_average‣vote_count‣runtime‣certification‣poster_path‣budget‣tagline‣genres :id․name‣directors :id․name‣actors:id․name․character.";
        private FilmContext _filmContext;
        
        public string DebugGetLine
        {
            get
            {
                if (_line == null)
                    return "empty";
                return _line;
            }
        }

        public FilmParser(FilmContext filmContext)
        {
            try
            {
                string moviesTxtFullPath = Properties.getMoviesTxtFullPath();
                _streamReader = new StreamReader(moviesTxtFullPath);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

            this._filmContext = filmContext;
        }

        
        public bool readAndDecodeLine()
        {
            _line = _streamReader.ReadLine();
            if (_line == null)
                return false;

            try
            {
                Film film = TextToFilm();
                this._filmContext.Films.Add(film);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            
            try
            {
                this._filmContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            
            return true;
        }

        private Film TextToFilm()
        {
            try
            {
                Film film = new Film();
                Char[] delimiterChars = {'\u2023'}; // verifier si c’est le bon délimiteur
                string[] filmdetailwords = this._line.Split(delimiterChars);
                delimiterChars[0] = '\u2016'; // verifier si c’est le bon délimiteur

                // Initialisation des champs de base du film
                film.Id = parseFilmId(filmdetailwords[0]);
                film.Title = parseFilmTitle(filmdetailwords[1]);
                film.ReleaseDate = parseFilmReleaseDate(filmdetailwords[3]);
                film.VoteAverage = parseFilmVoteAverage(filmdetailwords[5]);
                film.Runtime = parseFilmRuntime(filmdetailwords[7]);
                film.PosterPath = parseFilmPosterPath(filmdetailwords[9]);
                
                // Initialisation des champs détails du film
                if (filmdetailwords.Length == 15)
                {
                    if (!ChampsIsVide(filmdetailwords[12]))
                    {
                        string[] genres = filmdetailwords[12].Split(delimiterChars);
                        foreach (string s in genres)
                        {
                            //GESTION DES DOUBLONS !!!
                            if (s.Length > 0)
                            {
                                Genre genreFromTxt = new Genre(s);
                                Genre genreFromBd = genreExist(genreFromTxt.Name);
                                if(genreFromBd != null)
                                    film.Genres.Add(genreFromBd);
                                else
                                {
                                    film.Genres.Add(genreFromTxt);
                                }
                            }
                        }
                    }
                    
                    if (!ChampsIsVide(filmdetailwords[14]))
                    {
                        string[] acteurs = filmdetailwords[14].Split(delimiterChars);
                        foreach (string s in acteurs)
                        {
                            if (s.Length > 0)
                            {
                                Actor actorFromTxt = new Actor(s);
                                bool alreadyExistInTheCurrentFilm = false;
                                //GESTION DES DOUBLONS !!!
                                //aussi vérifier s'il n'est pas déjà dans le film
                                foreach (Actor actor in film.Actors)
                                {
                                    if (actor.Id == actorFromTxt.Id)
                                    {
                                        alreadyExistInTheCurrentFilm = true;
                                        break;
                                    }
                                }
                                
                                //vérifier si l'acteur n'existe pas déjà dans la bd
                                if (!alreadyExistInTheCurrentFilm)
                                {
                                    Actor actorFromBd = actorExist(actorFromTxt.Id);
                                    if (actorFromBd != null)//l'acteur est déjà dans la bd
                                    {
                                        film.Actors.Add(actorFromBd);
                                    }
                                    else//l'acteur n'est pas déjà dans le film et n'existe pas non plus dans la bd
                                    {
                                        film.Actors.Add(actorFromTxt);
                                    }
                                }
                            }
                            
                        }
                    }
                }
                
                return film;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw e;
            }
        }

        private IQueryable<Actor> getActorFromId(int id)
        {
            return this._filmContext.Actors.Where(actor => actor.Id == id);
        }

        private IQueryable<Genre> getGenreFromName(string name)
        {
            return this._filmContext.Genres.Where(genre => genre.Name == name);
        }

        private Genre genreExist(string name)
        {
            List<Genre> genres = new List<Genre>(this.getGenreFromName(name));
            if (genres.Count < 1) return null;
            if(genres.Count > 1)throw new Exception("DOUBLONS !!! nombre de genres pour le nom :" + name + " = " + genres.Count);
            foreach (Genre genre in genres)
            {
                return genre;
            }
            return null;
        }

        private Actor actorExist(int id)
        {
            List<Actor> actors = new List<Actor>(this.getActorFromId(id));
            if (actors.Count < 1) return null;
            if(actors.Count > 1) throw new Exception("DOUBLONS !!! nombre d'acteurs pour l'id :" + id + " = " + actors.Count);
            foreach (Actor actor in actors)
            {
                return actor;
            }

            return null;
        }

        private static int parseFilmId(string champ)
        {
            if(ChampsIsVide(champ))
                throw new Exception("no id");
            
            try
            {
                int intVal = Int32.Parse(champ);
                return intVal;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        
        private static string parseFilmTitle(string champ)
        {
            if (ChampsIsVide(champ))
                return "no title";
            return champ;
        }
        
        private static int parseFilmRuntime(string champ)
        {
            if (ChampsIsVide(champ))
                return -1;
            try
            {
                int intVal = Int32.Parse(champ);
                return intVal;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        
        private static string parseFilmPosterPath(string champ)
        {
            if (ChampsIsVide(champ))
                return "no poster path";
            return champ;
        }
        
        private static DateTime parseFilmReleaseDate(string champ)
        {
            //string = YYYY-MM-DD
            try
            {
                if (ChampsIsVide(champ))
                    return new DateTime();
                string[] dateSplit = champ.Split('-');
                if (dateSplit.Length == 3)
                {
                    int year, month, day;
                    year = Int32.Parse(dateSplit[0]);
                    month = Int32.Parse(dateSplit[1]);
                    day = Int32.Parse(dateSplit[2]);
                
                    return new DateTime(year, month, day);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            throw new Exception("erreur : parseFilmReleaseDate");
        }
        
        private static double parseFilmVoteAverage(string champ)
        {
            if (ChampsIsVide(champ))
                return -1;
            try
            {
                double doubleVal = double.Parse(champ, System.Globalization.CultureInfo.InvariantCulture);
                return doubleVal;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        //return true si la string passé en paramètre est vide (ou null),
        //return false dans le cas contraire
        private static bool ChampsIsVide(string champ)
        {
            if (champ == null) return true;
            if (champ.Length < 1) return true;
            Char[] delimiterChars = { '\u2023' };
            if (StringEqualToCharTab(champ, delimiterChars))
                return true;
            return false;
        }

        private static bool StringEqualToCharTab(string champ, char[] delimiterChars)
        {
            for (int i = 0; i < delimiterChars.Length; i++)
            {
                if (champ[i] != delimiterChars[i])
                    return false;
            }

            return true;
        }
    }
}