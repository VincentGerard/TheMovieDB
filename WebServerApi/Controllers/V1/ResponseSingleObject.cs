using System;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServerApi.Controllers.V1
{
	[Serializable]
    public class ResponseSingleObject<T> 
    {
        public int Status { get; set; }
        public T Value { get; set; }
        public bool Succeded { get; set; }
        public string Errors { get; set; }
        public string Message { get; set; }

        /*public ActionResult ToActionResult()
        {
            switch (Status)
            {
                case StatusCodes.Status200OK :
                    return new OkObjectResult(this);
                case StatusCodes.Status404NotFound:
                    return NotFoundResult(this);

            }
            return new ObjectResult(new ExceptionResult(new Exception("Bad status code : status = " + Status), false));
        }*/
    }
}