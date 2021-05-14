using System;
using System.Collections.Generic;
using System.Linq;
using FilmsBLL;
using FilmsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerApi.Contracts.V1;

namespace WebServerApi.Controllers.V1.Genres
{
    [Route(ApiRoute.Genres.GenreBase)]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly BllManager _bllManager;

        public GenresController(BllManager bllManager)
        {
            _bllManager = bllManager;
        }

        [HttpGet(ApiRoute.Genres.GetGenresFromTo)]
        public IActionResult getGenresFromTo(int from, int to)
        {
            ResponsePagine<List<GenreDTO>> responsePagine = new ResponsePagine<List<GenreDTO>>()
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
                IQueryable<GenreDTO> queryActors = _bllManager.getGenresFromTo(from, to);
                if (queryActors == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "getGenresFromTo(from=" + from + ", to=" + to + ") RETURN null";
                }
                else
                {
                    int nbrGenre = _bllManager.getGenresFromTo();
                    if (nbrGenre != -1)
                    {
                        if (queryActors.ToList().Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = queryActors.ToList();
                            responsePagine.FirstPage = ApiRoute.Actors.ActorBase + "/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Actors.ActorBase + "/from=" +
                                                          (nbrGenre - (to - @from)).ToString() + "/to=" + nbrGenre;
                            responsePagine.TotalRecord = nbrGenre;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre de genre trouvé = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre de genre trouvé = -1";
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "getGenresFromTo(from=" + from + ", to=" + to + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        [HttpGet(ApiRoute.Genres.GetNumberGenresFromTo)]
        public IActionResult GetNumberGenresFromTo()
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
                int nbrGenre = _bllManager.getGenresFromTo();
                if (nbrGenre == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre de genre trouvé = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrGenre;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetNumberGenresFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }

        [HttpGet(ApiRoute.Genres.GetListGenreByIdFilm)]
        public IActionResult GetListGenreByIdFilm(int idFilm)
        {
            ResponseSingleObject<List<GenreDTO>> responseSingleObject = new ResponseSingleObject<List<GenreDTO>>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null
            };
            try
            {
                List<GenreDTO> genres = _bllManager.GetListFilmTypesByIdFilm(idFilm);
                if (genres == null)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre de genre trouvé = null";
                }
                else
                {
                    if (genres.Count == 0)
                    {
                        responseSingleObject.Status = StatusCodes.Status404NotFound;
                        responseSingleObject.Errors = "nombre de genre trouvé = 0";
                    }
                    else
                    {
                        responseSingleObject.Status = StatusCodes.Status200OK;
                        responseSingleObject.Value = genres;
                    }
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetListGenreByIdFilm() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }



    }
}