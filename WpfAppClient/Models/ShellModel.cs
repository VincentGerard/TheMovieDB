using FilmsDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Windows.Media.Imaging;
using WebServerApi.Controllers.V1;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net.Http;
using System.Windows;
using System.Configuration;
using WebServerApi.Contracts.V1;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WpfAppClient.Models
{
	public class ShellModel
	{
		private int _filmCount = 0;
		private int _currentFilmNumber = 0;
		private int _nextFilmNumber = 0;
		private List<FilmDTO> _listFilmDTO = null;
		private List<FullFilmDTO> _listFullFilmDTO = new List<FullFilmDTO>();
		HttpClient client = new HttpClient();
		JsonSerializerOptions options = null;

		public ShellModel()
		{
			options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
			client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));

			this.LoadCurrentPage(null);
		}

		public BitmapImage getImage(int number)
		{
			if(number <= _listFilmDTO.Count)
			{
				HttpResponseMessage r = client.GetAsync(ConfigurationManager.AppSettings["movieAPI"] + _listFilmDTO.ElementAt(number - 1).Id + ConfigurationManager.AppSettings["appendKey"]).Result;

				if(!r.IsSuccessStatusCode)
				{
					Trace.WriteLine("[ShellModel][getImage]Api error");
					return null;
				}

				string s = r.Content.ReadAsStringAsync().Result;
				var data = (JObject)JsonConvert.DeserializeObject(s);
				string newPath = data["poster_path"].Value<string>();

				return new BitmapImage(new Uri(ConfigurationManager.AppSettings["pathToPoster"] + newPath + ConfigurationManager.AppSettings["appendKey"]));
			}
			return null;
		}

		public string getTitre(int number)
		{
			if (number <= _listFilmDTO.Count)
				return _listFilmDTO.ElementAt(number - 1).Title;
			return null;
		}

		public string getDuree(int number)
		{
			if (_listFilmDTO.Count < number)
				return null;
			int t = _listFilmDTO.ElementAt(number - 1).Runtime;
			int h = t / 60;
			int m = t - h * 60;
			return string.Format("{0}h{1:00}", h, m);
		}
		
		public BitmapImage getIcon(int film,int number)
		{
			if (film <= _listFilmDTO.Count && number <= _listFullFilmDTO.ElementAt(film - 1).Genres.Count)
				return new BitmapImage(new Uri(ConfigurationManager.AppSettings["pathToIcons"] + _listFullFilmDTO.ElementAt(film - 1).Genres.ElementAt(number - 1).Name.ToLower() + ".png"));
			return null;
		}

		public int getId(int number)
		{
			if (_listFilmDTO.Count >= number)
			{
				return _listFilmDTO.ElementAt(number - 1).Id;
			}
			return -1;
		}

		public void loadNextMovies(string searchText)
		{
			if(searchText == null || searchText.Length == 0)
			{
				if (_currentFilmNumber + 5 >= _filmCount)
					return;

				if (_currentFilmNumber + 5 >= _filmCount)
				{
					_currentFilmNumber += 5;
					_nextFilmNumber = _filmCount - _currentFilmNumber;
				}
				else
				{
					_currentFilmNumber += 5;
					_nextFilmNumber = _currentFilmNumber + 5;
				}




				_listFullFilmDTO = new List<FullFilmDTO>();
				_listFilmDTO = new List<FilmDTO>();

				//Recuperer les 5 premiers films
				Trace.WriteLine("Api Call: " + ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFilmsFromTo.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", (_nextFilmNumber).ToString()));
				HttpResponseMessage reponse3 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFilmsFromTo.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", (_nextFilmNumber).ToString())).Result;

				if (!reponse3.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][ShellModel]Api reponse on _listFilmDTO");
					App.Current.Shutdown();
					return;
				}

				_listFilmDTO = JsonSerializer.Deserialize<ResponsePagine<List<FilmDTO>>>(reponse3.Content.ReadAsStringAsync().Result, options).Value;

				//Remplir les 5 fullFilmDTO
				for (int i = 0; i < 5; i++)
				{
					if (_listFilmDTO.Count <= i)
					{
						//Si il n'y a pas de film
						break;
					}
					FilmDTO f = _listFilmDTO.ElementAt(i);
					HttpResponseMessage reponse2 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", f.Id.ToString())).Result;
					if (!reponse2.IsSuccessStatusCode)
					{
						Trace.WriteLine("[Error][ShellModel][getIcon]Api reponse on GetListGenreByIdFilm");
						App.Current.Shutdown();
					}

					FullFilmDTO fullFilm = JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(reponse2.Content.ReadAsStringAsync().Result, options).Value;
					_listFullFilmDTO.Add(fullFilm);
				}
			}
			else
			{
				if (_currentFilmNumber + 5 >= _filmCount)
					return;

				if (_currentFilmNumber + 5 >= _filmCount)
				{
					_currentFilmNumber += 5;
					_nextFilmNumber = _filmCount - _currentFilmNumber;
				}
				else
				{
					_currentFilmNumber += 5;
					_nextFilmNumber = _currentFilmNumber + 5;
				}

				_listFullFilmDTO = new List<FullFilmDTO>();
				_listFilmDTO = new List<FilmDTO>();

				string request2 = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.FindFilmsByPartialTitle.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", _nextFilmNumber.ToString()).Replace("{titre}", searchText);
				Trace.WriteLine("[LoadCurrentPage]Api Call: " + request2);
				HttpResponseMessage r2 = client.GetAsync(request2).Result;
				Trace.WriteLine("[LoadCurrentPage]Api Response: " + r2.Content.ReadAsStringAsync().Result);

				if (!r2.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][LoadCurrentPage]Api reponse on _listFilmDTO");
					App.Current.Shutdown();
				}

				_listFilmDTO = JsonSerializer.Deserialize<ResponsePagine<List<FilmDTO>>>(r2.Content.ReadAsStringAsync().Result, options).Value;

				if (_listFilmDTO == null)
					_listFilmDTO = new List<FilmDTO>();

				//Remplir les 5 fullFilmDTO
				for (int i = 0; i < 5; i++)
				{
					if (_listFilmDTO.Count <= i)
					{
						//Si il n'y a pas de film
						break;
					}
					FilmDTO f = _listFilmDTO.ElementAt(i);
					HttpResponseMessage r3 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", f.Id.ToString())).Result;
					if (!r3.IsSuccessStatusCode)
					{
						Trace.WriteLine("[Error][LoadCurrentPage]Load 5 Film");
						App.Current.Shutdown();
					}

					FullFilmDTO fullFilm = JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(r3.Content.ReadAsStringAsync().Result, options).Value;
					_listFullFilmDTO.Add(fullFilm);
				}
			}
		}

		public void loadPreviousMovies(string searchText)
		{
			if(searchText == null || searchText.Length == 0)
			{
				if (_currentFilmNumber - 5 <= 0)
					return;

				if (_currentFilmNumber - 5 <= 0)
				{
					_currentFilmNumber -= 5;
					_nextFilmNumber = _filmCount - _currentFilmNumber;
				}
				else
				{
					_currentFilmNumber -= 5;
					_nextFilmNumber = _currentFilmNumber - 5;
				}




				_listFullFilmDTO = new List<FullFilmDTO>();
				_listFilmDTO = new List<FilmDTO>();

				//Recuperer les 5 premiers films
				Trace.WriteLine("Api Call: " + ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFilmsFromTo.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", (_nextFilmNumber).ToString()));
				HttpResponseMessage reponse = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFilmsFromTo.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", (_nextFilmNumber).ToString())).Result;

				if (!reponse.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][ShellModel]Api reponse on _listFilmDTO");
					return;
				}

				_listFilmDTO = JsonSerializer.Deserialize<ResponsePagine<List<FilmDTO>>>(reponse.Content.ReadAsStringAsync().Result, options).Value;



				//Remplir les 5 fullFilmDTO
				for (int i = 0; i < 5; i++)
				{
					if (_listFilmDTO.Count <= i)
					{
						//Si il n'y a pas de film
						break;
					}
					FilmDTO f = _listFilmDTO.ElementAt(i);
					HttpResponseMessage reponse2 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", f.Id.ToString())).Result;
					if (!reponse2.IsSuccessStatusCode)
					{
						Trace.WriteLine("[Error][ShellModel][getIcon]Api reponse on GetListGenreByIdFilm");
						App.Current.Shutdown();
					}

					FullFilmDTO fullFilm = JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(reponse2.Content.ReadAsStringAsync().Result, options).Value;
					_listFullFilmDTO.Add(fullFilm);
				}
			}
			else
			{
				if (_currentFilmNumber - 5 <= 0)
					return;

				if (_currentFilmNumber - 5 <= 0)
				{
					_currentFilmNumber -= 5;
					_nextFilmNumber = _filmCount - _currentFilmNumber;
				}
				else
				{
					_currentFilmNumber -= 5;
					_nextFilmNumber = _currentFilmNumber - 5;
				}




				_listFullFilmDTO = new List<FullFilmDTO>();
				_listFilmDTO = new List<FilmDTO>();

				string request2 = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.FindFilmsByPartialTitle.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", _nextFilmNumber.ToString()).Replace("{titre}", searchText);
				Trace.WriteLine("[LoadCurrentPage]Api Call: " + request2);
				HttpResponseMessage r2 = client.GetAsync(request2).Result;
				Trace.WriteLine("[LoadCurrentPage]Api Response: " + r2.Content.ReadAsStringAsync().Result);

				if (!r2.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][LoadCurrentPage]Api reponse on _listFilmDTO");
					App.Current.Shutdown();
				}

				_listFilmDTO = JsonSerializer.Deserialize<ResponsePagine<List<FilmDTO>>>(r2.Content.ReadAsStringAsync().Result, options).Value;

				if (_listFilmDTO == null)
					_listFilmDTO = new List<FilmDTO>();

				//Remplir les 5 fullFilmDTO
				for (int i = 0; i < 5; i++)
				{
					if (_listFilmDTO.Count <= i)
					{
						//Si il n'y a pas de film
						break;
					}
					FilmDTO f = _listFilmDTO.ElementAt(i);
					HttpResponseMessage r3 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", f.Id.ToString())).Result;
					if (!r3.IsSuccessStatusCode)
					{
						Trace.WriteLine("[Error][LoadCurrentPage]Load 5 Film");
						App.Current.Shutdown();
					}

					FullFilmDTO fullFilm = JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(r3.Content.ReadAsStringAsync().Result, options).Value;
					_listFullFilmDTO.Add(fullFilm);
				}
			}
		}

		public void LoadCurrentPage(string searchText)
		{
			_currentFilmNumber = 0;
			_nextFilmNumber = 0;
			Trace.WriteLine("\n\nSearchText: " + searchText);
			if(searchText == null || searchText.Length == 0)
			{
				//Charge les 5 premier films
				//Recuperer le nombre de film dans le db
				Trace.WriteLine("[LoadCurrentPage]Api Call: " + ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetNumberFilmsFromTo);
				HttpResponseMessage r = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetNumberFilmsFromTo).Result;

				Trace.WriteLine("[LoadCurrentPage]Api Response: " + r.Content.ReadAsStringAsync().Result);

				if (!r.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][ShellModel]Api reponse on count");
					App.Current.Shutdown();
				}

				_filmCount = JsonSerializer.Deserialize<ResponseSingleObject<int>>(r.Content.ReadAsStringAsync().Result, options).Value;

				Trace.WriteLine("[LoadCurrentPage]Film count: " + _filmCount);

				if (_filmCount == 0)
				{
					_currentFilmNumber = 0;
					_nextFilmNumber = 0;
					_listFilmDTO = new List<FilmDTO>();
					_listFullFilmDTO = new List<FullFilmDTO>();
					return;
				}

				if (_currentFilmNumber + 5 > _filmCount)
					_nextFilmNumber = _filmCount - _currentFilmNumber;
				else
					_nextFilmNumber = _currentFilmNumber + 5;

				//Recuperer les premiers films
				Trace.WriteLine("[LoadCurrentPage]Api Call: " + ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFilmsFromTo.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", _nextFilmNumber.ToString()));
				HttpResponseMessage r2 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFilmsFromTo.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", _nextFilmNumber.ToString())).Result;
				Trace.WriteLine("[LoadCurrentPage]Api Response: " + r2.Content.ReadAsStringAsync().Result);

				if (!r2.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][LoadCurrentPage]Api reponse on _listFilmDTO");
				}

				_listFilmDTO = JsonSerializer.Deserialize<ResponsePagine<List<FilmDTO>>>(r2.Content.ReadAsStringAsync().Result, options).Value;

				//Remplir les 5 fullFilmDTO
				for (int i = 0; i < 5; i++)
				{
					FilmDTO f = _listFilmDTO.ElementAt(i);
					HttpResponseMessage r3 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", f.Id.ToString())).Result;
					if (!r3.IsSuccessStatusCode)
					{
						Trace.WriteLine("[Error][LoadCurrentPage]Load 5 Film");
						App.Current.Shutdown();
					}

					FullFilmDTO fullFilm = JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(r3.Content.ReadAsStringAsync().Result, options).Value;
					_listFullFilmDTO.Add(fullFilm);
				}
			}
			else
			{
				//Charge les 5 premier films avec titre
				//Recuperer le nombre de film dans le db
			
				string request = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetNumberFilmsByPartialTitle.Replace("{titre}",searchText);
				Trace.WriteLine("Api Call: " + request);
				HttpResponseMessage r = client.GetAsync(request).Result;

				Trace.WriteLine("[LoadCurrentPage]Api Response: " + r.Content.ReadAsStringAsync().Result);

				if (!r.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][ShellModel]Api reponse on count");
					App.Current.Shutdown();
				}

				_filmCount = JsonSerializer.Deserialize<ResponseSingleObject<int>>(r.Content.ReadAsStringAsync().Result, options).Value;

				if (_filmCount == 0)
				{
					_currentFilmNumber = 0;
					_nextFilmNumber = 0;
					_listFilmDTO = new List<FilmDTO>();
					_listFullFilmDTO = new List<FullFilmDTO>();
					return;
				}

				Trace.WriteLine("[LoadCurrentPage]Film count: " + _filmCount);

				if (_currentFilmNumber + 5 > _filmCount)
					_nextFilmNumber = _filmCount - _currentFilmNumber;
				else
					_nextFilmNumber = _currentFilmNumber + 5;

				//Recuperer les premiers films

				string request2 = ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.FindFilmsByPartialTitle.Replace("{from}", _currentFilmNumber.ToString()).Replace("{to}", _nextFilmNumber.ToString()).Replace("{titre}",searchText);
				Trace.WriteLine("[LoadCurrentPage]Api Call: " + request2);
				HttpResponseMessage r2 = client.GetAsync(request2).Result;
				Trace.WriteLine("[LoadCurrentPage]Api Response: " + r2.Content.ReadAsStringAsync().Result);

				if (!r2.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][LoadCurrentPage]Api reponse on _listFilmDTO");
					App.Current.Shutdown();
				}

				_listFilmDTO = JsonSerializer.Deserialize<ResponsePagine<List<FilmDTO>>>(r2.Content.ReadAsStringAsync().Result, options).Value;

				if (_listFilmDTO == null)
					_listFilmDTO = new List<FilmDTO>();

				//Remplir les 5 fullFilmDTO
				for (int i = 0; i < 5; i++)
				{
					if (_listFilmDTO.Count <= i)
					{
						//Si il n'y a pas de film
						break;
					}
					FilmDTO f = _listFilmDTO.ElementAt(i);
					HttpResponseMessage r3 = client.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", f.Id.ToString())).Result;
					if (!r3.IsSuccessStatusCode)
					{
						Trace.WriteLine("[Error][LoadCurrentPage]Load 5 Film");
						App.Current.Shutdown();
					}

					FullFilmDTO fullFilm = JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(r3.Content.ReadAsStringAsync().Result, options).Value;
					_listFullFilmDTO.Add(fullFilm);
				}
			}
		}
	}
}
