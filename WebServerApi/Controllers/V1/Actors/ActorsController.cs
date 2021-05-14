using System;
using System.Collections.Generic;
using System.Linq;
using FilmsBLL;
using FilmsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerApi.Contracts.V1;

namespace WebServerApi.Controllers.V1.Actors
{
    [Route(ApiRoute.Actors.ActorBase)]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly BllManager _bllManager;

        public ActorsController(BllManager bllManager)
        {
            _bllManager = bllManager;
        }

        [HttpGet(ApiRoute.Actors.GetActorById)]
        public IActionResult GetActorById(int id)
        {
            ResponseSingleObject<ActorDTO> responseSingleObject = new ResponseSingleObject<ActorDTO>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null
            };
            try
            {
                ActorDTO a = _bllManager.getActorFromId(id);
                if (a == null)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "aucun acteur trouvé pour l'id : " + id;
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = a;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "getNumberActorsFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }
        
        [HttpGet(ApiRoute.Actors.getActorsFromTo)]
        public IActionResult getActorsFromTo(int from, int to)
        {
            ResponsePagine<List<ActorDTO>> responsePagine = new ResponsePagine<List<ActorDTO>>()
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
                IQueryable<ActorDTO> queryActors = _bllManager.getActorsFromTo(from, to);
                if (queryActors == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "getActorsFromTo(from=" + from + ", to=" + to + ") RETURN null";
                }
                else
                {
                    int nbrActor = _bllManager.getActorsFromTo();
                    if (nbrActor != -1)
                    {
                        if (queryActors.ToList().Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = queryActors.ToList();
                            responsePagine.FirstPage = ApiRoute.Actors.ActorBase + "/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Actors.ActorBase + "/from=" +
                                                          (nbrActor - (to - @from)).ToString() + "/to=" + nbrActor;
                            responsePagine.TotalRecord = nbrActor;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre d'actor trouvé = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre d'actor trouvé = -1";
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "getActorsFromTo(from=" + from + ", to=" + to + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        [HttpGet(ApiRoute.Actors.getNumberActorsFromTo)]
        public IActionResult getNumberActorsFromTo()
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
                int nbrActor = _bllManager.getActorsFromTo();
                if (nbrActor == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre d'acteur trouvé = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrActor;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "getNumberActorsFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }
        
        [HttpGet(ApiRoute.Actors.GetListActorsByIdFilm)]
        public IActionResult GetListActorsByIdFilm(int from, int to, int idFilm)
        {
            ResponsePagine<List<LightActorDTO>> responsePagine = new ResponsePagine<List<LightActorDTO>>()
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
                List<LightActorDTO> actors = _bllManager.GetListActorsByIdFilm(from, to ,idFilm);
                if (actors == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "getActorsFromTo(from=" + from + ", to=" + to + ") RETURN null";
                }
                else
                {
                    int nbrActor = _bllManager.getActorsFromTo();
                    if (nbrActor != -1)
                    {
                        if (actors.Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = actors;
                            responsePagine.FirstPage = ApiRoute.Actors.ActorBase + "/filmid=" + idFilm + "/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Actors.ActorBase + "/filmid=" + idFilm + "/from=" +
                                                          (nbrActor - (to - @from)).ToString() + "/to=" + nbrActor;
                            responsePagine.TotalRecord = nbrActor;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre d'actor trouvé = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre d'actor trouvé = -1";
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "getActorsFromTo(from=" + from + ", to=" + to + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        [HttpGet(ApiRoute.Actors.GetNumberActorsByIdFilm)]
        public IActionResult GetNumberActorsByIdFilm(int idFilm)
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
                int nbrActor = _bllManager.GetListActorsByIdFilm(idFilm);
                if (nbrActor == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre d'acteur trouvé = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrActor;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetNumberActorsByIdFilm() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }

        [HttpGet(ApiRoute.Actors.GetFavoriteActors)]
        public IActionResult GetFavoriteActors(int from, int to)
        {
            ResponsePagine<List<LightActorDTO>> responsePagine = new ResponsePagine<List<LightActorDTO>>()
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
                IQueryable<LightActorDTO> queryActors = _bllManager.GetFavoriteActors(from, to);
                if (queryActors == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "GetFavoriteActors(from=" + from + ", to=" + to + ") RETURN null";
                }
                else
                {
                    int nbrActor = _bllManager.getActorsFromTo();
                    if (nbrActor != -1)
                    {
                        if (queryActors.ToList().Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = queryActors.ToList();
                            responsePagine.FirstPage = ApiRoute.Actors.ActorBase + "/favorite/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Actors.ActorBase + "/favorite/from=" +
                                                          (nbrActor - (to - @from)).ToString() + "/to=" + nbrActor;
                            responsePagine.TotalRecord = nbrActor;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre d'actor trouvé = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre d'actor trouvé = -1";
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "GetFavoriteActors(from=" + from + ", to=" + to + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        [HttpGet(ApiRoute.Actors.GetNumberFavoriteActors)]
        public IActionResult GetNumberFavoriteActors()
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
                int nbrActor = _bllManager.GetFavoriteActors();
                if (nbrActor == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre d'acteur trouvé = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrActor;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetNumberFavoriteActors() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }
       
    }
}