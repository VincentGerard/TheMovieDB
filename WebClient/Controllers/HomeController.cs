using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using FilmsDal;
using FilmsDTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebClient.Models;
using WebServerApi.Contracts.V1;
using WebServerApi.Controllers.V1;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
		static string errorMessage = "";
        private readonly ILogger<HomeController> _logger;
		static int  min = 0;
		static int max = 0;
		int filmCount = 0;
		static List<FilmDTO> listFilmDTO = new List<FilmDTO>();
		HttpClient client = new HttpClient();
		JsonSerializerOptions options = null;
		string request;
		HttpResponseMessage response;
		HtmlModel model = new HtmlModel();


		public HomeController(ILogger<HomeController> logger)
        {
			model.controller = this;
	        _logger = logger;

			options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
			client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));

			//Get Count film
			request = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetNumberFilmsFromTo;
			Console.WriteLine("request = " + request);
			response = client.GetAsync(request).Result;

			Console.WriteLine("[Index]Api Response: " + response.Content.ReadAsStringAsync().Result);

			if (!response.IsSuccessStatusCode)
			{
				//C'est casser
				Console.WriteLine("response status = fail !!!");
			}

			filmCount = JsonSerializer.Deserialize<ResponseSingleObject<int>>(response.Content.ReadAsStringAsync().Result, options).Value;

			if (filmCount == 0)
			{
				errorMessage = "DB empty!";
			}

			if (min + 5 < filmCount)
				max = min + 5;
			else
				max = filmCount - min;
        }

        public IActionResult Index()
        {
			Trace.WriteLine(filmCount);

			if(this.LoadFilm() == -1)
			{
				return RedirectToAction("Error","Home");
			}
			
			model.list = listFilmDTO;
			return View(model);
        }

		[Route("HomeController/BouttonPrevious")]
		public IActionResult BouttonPrevious()
		{
			Trace.WriteLine("BouttonPrevious");

			if(min - 5 >= 0)
			{
				max -= 5;
				min -= 5;
			}
			else
			{
				min = 0;
				max = 5;
			}
			return RedirectToAction("Index","Home");
		}
		
		[Route("HomeController/BouttonNext")]
		public IActionResult BouttonNext()
		{
			Trace.WriteLine("BouttonNext");

			if (max + 5 <= filmCount)
			{
				max += 5;
				min += 5;
			}
			else
			{
				min = max;
				max = filmCount;
			}

			return RedirectToAction("Index", "Home");
		}

		[Route("HomeController/Details/{id}")]
		public IActionResult Details(int id)
		{
			Trace.WriteLine("ID = " + id);

			request = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", id.ToString()) ;
			Trace.WriteLine(request);
			response = client.GetAsync(request).Result;
			Trace.WriteLine("Api Reponse: " + response.Content.ReadAsStringAsync().Result);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("http fail full film");
			}

			FullFilmDTO film = JsonSerializer.Deserialize<ResponsePagine<FullFilmDTO>>(response.Content.ReadAsStringAsync().Result, options).Value;

			Trace.WriteLine(film.Title);

			//poster path
			HttpResponseMessage r = client.GetAsync(ConfigurationManager.AppSettings["movieAPI"] + film.Id + ConfigurationManager.AppSettings["appendKey"]).Result;

			if (!r.IsSuccessStatusCode)
			{
				throw new Exception("http fail poster path");
			}

			string s = r.Content.ReadAsStringAsync().Result;
			var data = (JObject)JsonConvert.DeserializeObject(s);
			string newPath = data["poster_path"].Value<string>();

			film.PosterPath = ConfigurationManager.AppSettings["pathToPoster"] + newPath + ConfigurationManager.AppSettings["appendKey"];


			return View(film);
		}
		
		[Route("HomeController/DetailsActeur/{id}")]
		public IActionResult DetailsActeur(int id)
		{
			Trace.WriteLine("ID = " + id);
			
			request = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Actors.ActorBase + "/" + ApiRoute.Actors.GetActorById.Replace("{id}", id.ToString()) ;
			Trace.WriteLine(request);
			response = client.GetAsync(request).Result;
			Trace.WriteLine("Api Reponse: " + response.Content.ReadAsStringAsync().Result);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("http fail actor by id");
			}

			ActorDTO a = JsonSerializer.Deserialize<ResponsePagine<ActorDTO>>(response.Content.ReadAsStringAsync().Result, options).Value;
			
			return View(a);
		}
		
		

		private int LoadFilm()
		{
			request = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFilmsFromTo.Replace("{from}", min.ToString()).Replace("{to}", max.ToString());
			Trace.WriteLine(request);
			response = client.GetAsync(request).Result;
			Trace.WriteLine("Api Reponse: " + response.Content.ReadAsStringAsync().Result);

			if (!response.IsSuccessStatusCode)
			{
				errorMessage = "DB EMPTY!";
				return -1;
			}

			listFilmDTO = JsonSerializer.Deserialize<ResponsePagine<List<FilmDTO>>>(response.Content.ReadAsStringAsync().Result, options).Value;

			if (listFilmDTO == null || listFilmDTO.Count == 0)
			{
				errorMessage = "API returned empty list!";
				return -1;
			}

			foreach (FilmDTO f in listFilmDTO)
			{
				HttpResponseMessage r = client.GetAsync(ConfigurationManager.AppSettings["movieAPI"] + f.Id + ConfigurationManager.AppSettings["appendKey"]).Result;

				if (!r.IsSuccessStatusCode)
				{
					errorMessage = "MovideDB api error!";
					return -1;
				}

				string s = r.Content.ReadAsStringAsync().Result;
				var data = (JObject)JsonConvert.DeserializeObject(s);
				string newPath = data["poster_path"].Value<string>();

				f.PosterPath = ConfigurationManager.AppSettings["pathToPoster"] + newPath + ConfigurationManager.AppSettings["appendKey"];
			}
			return 0;
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
			Trace.WriteLine("Erreurmessage: " + errorMessage);
            return View(new ErrorViewModel { ErrorMessage = errorMessage});
        }
    }
}