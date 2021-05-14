using FilmsDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebServerApi.Contracts.V1;
using WebServerApi.Controllers.V1;
using WpfAppClient.ViewModels;

namespace WpfAppClient.Views
{
	/// <summary>
	/// Interaction logic for CommentView.xaml
	/// </summary>
	public partial class CommentView : Window, INotifyPropertyChanged
	{
		private HttpClient client2 = new HttpClient();
		JsonSerializerOptions options = null;
		FullFilmDTO _fullFilm = new FullFilmDTO();
		public event PropertyChangedEventHandler PropertyChanged;
		string _titre = "coucou";
		string _duree = "144";
		public static int _id = 2;
		BitmapImage _imageSource = null;
		List<ListBoxComment> _listBoxComments = new List<ListBoxComment>();
		List<ActorDTO> _listActeur = new List<ActorDTO>();

		public CommentView(int newId)
		{
			Trace.WriteLine("[CommentView]Debut");
			DataContext = this;
			InitializeComponent();

			_id = newId;

			options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};


			client2.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
			client2.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));
			//recuperer le film
			//Charger le film

			HttpResponseMessage reponse2 = client2.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", Id.ToString())).Result;
			if (!reponse2.IsSuccessStatusCode)
			{
				Trace.WriteLine("[Error][ShellModel][getIcon]Api reponse on GetListGenreByIdFilm");
				App.Current.Shutdown();
			}

			 _fullFilm = System.Text.Json.JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(reponse2.Content.ReadAsStringAsync().Result, options).Value;


			//List De comment test
			List<ListBoxComment> newList = new List<ListBoxComment>();
			_fullFilm.Comments.OrderBy(comment => comment.Date);
			foreach (CommentDTO c in _fullFilm.Comments)
			{
				newList.Add(new ListBoxComment() {Comment = c.Content,Rate = c.Rate.ToString(), Date = c.Date.ToString(), Username = c.Username});
			}
			ListComment = newList;

			//Titre
			Titre = _fullFilm.Title;
			//Duree
			Duree = _fullFilm.Runtime.ToString();
			//List Actors
			ListActeur = _fullFilm.Actors;



		}

		public int Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				OnPropertyChanged();
			}
		}


		public string Titre
		{
			get
			{
				if (_fullFilm.Comments == null)
				{
					return _titre;
				}
				else
				{
					return _titre + " " + _fullFilm.VoteAverage + " (" + _fullFilm.Comments.Count + ")";
				}
			}
			set
			{
				_titre = value;
				OnPropertyChanged();
			}
		}

		public string Duree
		{
			get
			{
				int h = int.Parse(_duree) / 60;
				int m = int.Parse(_duree) - h * 60;
				return string.Format("{0}h{1:00}", h, m);
			}
			set
			{
				_duree = value;
				OnPropertyChanged();
			}
		}


		public List<ListBoxComment> ListComment
		{
			get
			{
				return _listBoxComments;
			}
			set
			{
				_listBoxComments = value;
				OnPropertyChanged();
			}
		}

		public List<ActorDTO> ListActeur
		{
			get
			{
				return _listActeur;
			}
			set
			{
				_listActeur = value;
				OnPropertyChanged();
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void BouttonAjouter_Click(object sender, RoutedEventArgs e)
		{
			if(TextBoxCommentaire.Text.Length != 0 && TextBoxNom.Text.Length != 0 && TextBoxNote.Text.Length != 0)
			{
				CommentDTO newComment = new CommentDTO(TextBoxCommentaire.Text, int.Parse(TextBoxNote.Text), TextBoxNom.Text, DateTime.Now, _fullFilm);

				var jsonText = JsonConvert.SerializeObject(newComment);

				//api
				var content = new StringContent(jsonText, Encoding.UTF8, "application/json");

				var message = client2.PostAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Comments.CommentBase + "/" + ApiRoute.Comments.InsertComment, content);

				Trace.WriteLine(message.Result.Content.ReadAsStringAsync().Result);

				HttpResponseMessage reponse2 = client2.GetAsync(ConfigurationManager.AppSettings["baseUrl"] + ApiRoute.Films.FilmBase + "/" + ApiRoute.Films.GetFullFilmDetailsByIdFilm.Replace("{idFilm}", Id.ToString())).Result;
				if (!reponse2.IsSuccessStatusCode)
				{
					Trace.WriteLine("[Error][ShellModel][getIcon]Api reponse on GetListGenreByIdFilm");
					App.Current.Shutdown();
				}

				_fullFilm = System.Text.Json.JsonSerializer.Deserialize<ResponseSingleObject<FullFilmDTO>>(reponse2.Content.ReadAsStringAsync().Result, options).Value;
				
				//List De comment test
				List<ListBoxComment> newList = new List<ListBoxComment>();
				_fullFilm.Comments.OrderBy(comment => comment.Date);
				foreach (CommentDTO c in _fullFilm.Comments)
				{
					newList.Add(new ListBoxComment() { Comment = c.Content, Rate = c.Rate.ToString(), Date = c.Date.ToString(), Username = c.Username });
				}
				ListComment = newList;
			}
		}
	}



	public class ListBoxComment
	{
		public string Comment { get; set; }
		public string Rate { get; set; }
		public string Date { get; set; }
		public string Username { get; set; }
	}
}
