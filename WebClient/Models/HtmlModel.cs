using FilmsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Controllers;

namespace WebClient.Models
{
	public class HtmlModel
	{
		public List<FilmDTO> list = new List<FilmDTO>();
		public HomeController controller = null;
	}
}
