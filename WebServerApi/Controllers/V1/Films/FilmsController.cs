using System;
using System.Collections.Generic;
using System.Linq;
using FilmsBLL;
using FilmsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerApi.Contracts.V1;

namespace WebServerApi.Controllers.V1.Films
{
    [Route(ApiRoute.Films.FilmBase)]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly BllManager _bllManager;

        public FilmsController(BllManager bllManager)
        {
            _bllManager = bllManager;
        }

        [HttpGet(ApiRoute.Films.GetFilmsFromTo)]
        public IActionResult getFilmsFromTo(int from, int to) //RETURN from < FILMS <= to
        {
            ResponsePagine<List<FilmDTO>> responsePagine = new ResponsePagine<List<FilmDTO>>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null,
                FirstPage = null,
                From = from,
                To = to,
                LastPage = null,
                TotalRecord = -1,
            };
            try
            {
                IQueryable<FilmDTO> queryFilm = _bllManager.getFilmsFromTo(from, to);
                if (queryFilm == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "getFilmFromTo(from=" + from + ", to=" + to + ") RETURN null";
                }
                else
                {
                    int nbrFilm = _bllManager.getFilmsFromTo();
                    if (nbrFilm != -1)
                    {
                        if (queryFilm.ToList().Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = queryFilm.ToList();
                            responsePagine.FirstPage = ApiRoute.Films.FilmBase + "/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Films.FilmBase + "/from=" +
                                                          (nbrFilm - (to - @from)).ToString() + "/to=" + nbrFilm;
                            responsePagine.TotalRecord = nbrFilm;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre de films trouvé = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre de films trouvé = -1";
                    
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "getFilmFromTo(from=" + from + ", to=" + to + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        [HttpGet(ApiRoute.Films.GetNumberFilmsFromTo)]
        public IActionResult getNumberFilmsFromTo()
        {
            ResponseSingleObject<int> responseSingleObject = new ResponseSingleObject<int>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = -1
            };
            try
            {
                int nbrFilms = _bllManager.getFilmsFromTo();
                if (nbrFilms == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre de films trouvé = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrFilms;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "getNumberFilmsFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }

        [HttpGet(ApiRoute.Films.FindFilmsByPartialTitle)]
        public IActionResult FindFilmsByPartialTitle(int from, int to, string titre)
        {
            ResponsePagine<List<FilmDTO>> responsePagine = new ResponsePagine<List<FilmDTO>>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null,
                FirstPage = null,
                From = from,
                To = to,
                LastPage = null,
                TotalRecord = -1,
            };
            try
            {
                IQueryable<FilmDTO> queryFilm = _bllManager.FindFilmsByPartialTitle(from, to, titre);
                if (queryFilm == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "FindFilmsByPartialTitle(from=" + from + ", to=" + to + ", titre="+ titre + ") RETURN null";
                }
                else
                {
                    int nbrFilm = _bllManager.FindFilmsByPartialTitle(titre);
                    if (nbrFilm != -1)
                    {
                        if (queryFilm.ToList().Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = queryFilm.ToList();
                            responsePagine.FirstPage = ApiRoute.Films.FilmBase + "/titre="+ titre + "/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Films.FilmBase + "/titre="+ titre + "/from=" +
                                                          (nbrFilm - (to - @from)).ToString() + "/to=" + nbrFilm;
                            responsePagine.TotalRecord = nbrFilm;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre de films trouvé dont le titre contient("+ titre + ") = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre de films trouvé dont le titre contient("+ titre + ") = -1";
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "FindFilmsByPartialTitle(from=" + from + ", to=" + to + ", titre=" + titre + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        

        [HttpGet(ApiRoute.Films.GetNumberFilmsByPartialTitle)]
        public IActionResult FindNumberFilmsByPartialTitle(string titre)
        {
            ResponseSingleObject<int> responseSingleObject = new ResponseSingleObject<int>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = -1
            };
            try
            {
                int nbrFilms = _bllManager.FindFilmsByPartialTitle(titre);
                if (nbrFilms == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre de films trouvé dont le titre contient(" + titre + ") = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrFilms;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "getNumberFilmsFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }
        
        
        // FindListFilmByPartialActorName(…) : récupère la liste des films (List<FilmDTO>)
        // dans lequel l’acteur dont on donne un nom partiellement ou entièrement 
        [HttpGet(ApiRoute.Films.FindListFilmByPartialActorName)]
        public IActionResult FindListFilmByPartialActorName(int from, int to, string nomActeur)
        {
            ResponsePagine<List<FilmDTO>> responsePagine = new ResponsePagine<List<FilmDTO>>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null,
                FirstPage = null,
                From = from,
                To = to,
                LastPage = null,
                TotalRecord = -1,
            };
            try
            {
                IQueryable<FilmDTO> queryFilm = _bllManager.FindListFilmByPartialActorName(from, to, nomActeur);
                if (queryFilm == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "FindListFilmByPartialActorName(from=" + from + ", to=" + to + ", nom auteur="+ nomActeur + ") RETURN null";
                }
                else
                {
                    int nbrFilm = _bllManager.FindListFilmByPartialActorName(nomActeur);
                    if (nbrFilm != -1)
                    {
                        if (queryFilm.ToList().Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = queryFilm.ToList();
                            responsePagine.FirstPage = ApiRoute.Films.FilmBase + "/nomacteur="+ nomActeur + "/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Films.FilmBase + "/nomacteur="+ nomActeur + "/from=" +
                                                          (nbrFilm - (to - @from)).ToString() + "/to=" + nbrFilm;
                            responsePagine.TotalRecord = nbrFilm;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre de films trouvé pour l'acteur dont le nom contient("+ nomActeur + ") = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre de films trouvé pour l'acteur dont le nom contient("+ nomActeur + ") = -1";
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "FindListFilmByPartialActorName(from=" + from + ", to=" + to + ", nom auteur="+ nomActeur + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        [HttpGet(ApiRoute.Films.GetNumberFilmByPartialActorName)]
        public ActionResult<int> FindNumberFilmByPartialActorName(string nomActeur)
        {
            ResponseSingleObject<int> responseSingleObject = new ResponseSingleObject<int>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = -1
            };
            try
            {
                int nbrFilms = _bllManager.FindListFilmByPartialActorName(nomActeur);
                if (nbrFilms == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre de films trouvé pour l'acteur dont le nom contient("+ nomActeur + ") = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrFilms;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "nombre de films trouvé pour l'acteur dont le nom contient("+ nomActeur + ") EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }
        
        
        //récupère la totalité des données d’un film dont on donne l’Id.
        [HttpGet(ApiRoute.Films.GetFullFilmDetailsByIdFilm)]
        public IActionResult GetFullFilmDetailsByIdFilm(int idFilm)
        {
            ResponseSingleObject<FullFilmDTO> responseSingleObject = new ResponseSingleObject<FullFilmDTO>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null
            };
            try
            {
                FullFilmDTO fullFilmDto = _bllManager.GetFullFilmDetailsByIdFilm(idFilm);
                if (fullFilmDto == null)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "aucun film trouve pour l id("+ idFilm + ")";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = fullFilmDto;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetFullFilmDetailsByIdFilm pour le film dont l id =("+ idFilm + ") EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }

        [HttpGet(ApiRoute.Films.GetFilmById)]
        public IActionResult GetFilmById(int idFilm)
        {
            ResponseSingleObject<FilmDTO> responseSingleObject = new ResponseSingleObject<FilmDTO>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null
            };
            try
            {
                FilmDTO filmDto = _bllManager.GetFilmById(idFilm);
                if (filmDto == null)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "aucun film trouve pour l id("+ idFilm + ")";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = filmDto;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetFilmById pour le film dont l id =("+ idFilm + ") EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }
        
    }
}