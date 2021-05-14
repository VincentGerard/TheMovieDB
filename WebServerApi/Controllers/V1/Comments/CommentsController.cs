using System;
using System.Collections.Generic;
using System.Linq;
using FilmsBLL;
using FilmsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerApi.Contracts.V1;

namespace WebServerApi.Controllers.V1.Comments
{
    [Route(ApiRoute.Comments.CommentBase)]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly BllManager _bllManager;

        public CommentsController(BllManager bllManager)
        {
            _bllManager = bllManager;
        }

        [HttpGet(ApiRoute.Comments.GetCommentsFromTo)]
        public IActionResult GetCommentsFromTo(int from, int to)
        {
            ResponsePagine<List<CommentDTO>> responsePagine = new ResponsePagine<List<CommentDTO>>()
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
                IQueryable<CommentDTO> queryComments = _bllManager.getCommentsFromTo(from, to);
                if (queryComments == null)
                {
                    responsePagine.Status = StatusCodes.Status404NotFound;
                    responsePagine.Errors = "GetCommentsFromTo(from=" + from + ", to=" + to + ") RETURN null";
                }
                else
                {
                    int nbrActor = _bllManager.getActorsFromTo();
                    if (nbrActor != -1)
                    {
                        if (queryComments.ToList().Count > 0)
                        {
                            responsePagine.Status = StatusCodes.Status200OK;
                            responsePagine.Succeded = true;
                            responsePagine.Value = queryComments.ToList();
                            responsePagine.FirstPage = ApiRoute.Actors.ActorBase + "/from=0/to=" + ((to - @from).ToString());
                            responsePagine.LastPage = ApiRoute.Actors.ActorBase + "/from=" +
                                                          (nbrActor - (to - @from)).ToString() + "/to=" + nbrActor;
                            responsePagine.TotalRecord = nbrActor;
                        }
                        else
                        {
                            responsePagine.Status = StatusCodes.Status404NotFound;
                            responsePagine.Errors = "nombre de comments trouvé = 0";
                        }
                    }
                    else
                    {
                        responsePagine.Status = StatusCodes.Status404NotFound;
                        responsePagine.Errors = "nombre de comments trouvé = -1";
                    }
                }
            }
            catch (Exception e)
            {
                responsePagine.Errors =
                    "GetCommentsFromTo(from=" + from + ", to=" + to + ") EXCEPTION : " + e.ToString();
            }
            
            return StatusCode(responsePagine.Status, responsePagine);
        }

        [HttpGet(ApiRoute.Comments.GetNumberCommentsFromTo)]
        public IActionResult GetNumberCommentsFromTo()
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
                int nbrComment = _bllManager.getCommentsFromTo();
                if (nbrComment == -1)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "nombre de comments trouvé = -1";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = nbrComment;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetNumberCommentsFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }

        [HttpGet(ApiRoute.Comments.GetCommentFromId)]
        public IActionResult GetCommentFromId(int id)
        {
            ResponseSingleObject<CommentDTO> responseSingleObject = new ResponseSingleObject<CommentDTO>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null
            };
            try
            {
                CommentDTO commentDto = _bllManager.getCommentFromId(id);
                if (commentDto == null)
                {
                    responseSingleObject.Status = StatusCodes.Status404NotFound;
                    responseSingleObject.Errors = "aucun comment trouvé";
                }
                else
                {
                    responseSingleObject.Status = StatusCodes.Status200OK;
                    responseSingleObject.Value = commentDto;
                }
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetNumberCommentsFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }

        [HttpPost(ApiRoute.Comments.InsertComment)]
        public IActionResult InsertComment(CommentDTO commentDto)
        {
            ResponseCreated<CommentDTO> responseSingleObject = new ResponseCreated<CommentDTO>()
            {
                Status = StatusCodes.Status500InternalServerError,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null,
                LocationUri = null
            };
            try
            {
                int idCreated = _bllManager.InsertComment(commentDto);
                responseSingleObject.Status = StatusCodes.Status201Created;
                responseSingleObject.Succeded = true;
				string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string locationUri = baseUrl + "/" + ApiRoute.Comments.GetCommentFromId.Replace("{id}", idCreated.ToString());
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "InsertComment() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }

        [HttpDelete(ApiRoute.Comments.DeleteComment)]
        public IActionResult DeleteComment(int id)
        {
            ResponseSingleObject<CommentDTO> responseSingleObject = new ResponseSingleObject<CommentDTO>()
            {
                Status = StatusCodes.Status404NotFound,
                Errors = null,
                Message = null,
                Succeded = false,
                Value = null,
            };
            try
            {
                CommentDTO commentDto = _bllManager.getCommentFromId(id);   
                _bllManager.DeleteComment(commentDto);
                return Ok(commentDto);
            }
            catch (Exception e)
            {
                responseSingleObject.Errors =
                    "GetNumberCommentsFromTo() EXCEPTION : " + e.ToString();
            }

            return StatusCode(responseSingleObject.Status, responseSingleObject);
        }
        
    }
}