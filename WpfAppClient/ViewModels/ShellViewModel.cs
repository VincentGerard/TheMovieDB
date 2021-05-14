using FilmsDal;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Imaging;
using WpfAppClient.Models;
using WpfAppClient.Views;

namespace WpfAppClient.ViewModels
{
	public class ShellViewModel : INotifyPropertyChanged
	{
		
		ShellModel shellModel = null;

		private string _searchText = "";

		private BitmapImage _image1 = null;
		private BitmapImage _image2 = null;
		private BitmapImage _image3 = null;
		private BitmapImage _image4 = null;
		private BitmapImage _image5 = null;

		private string _titre1 = "";
		private string _titre2 = "";
		private string _titre3 = "";
		private string _titre4 = "";
		private string _titre5 = "";

		private string _duree1 = "";
		private string _duree2 = "";
		private string _duree3 = "";
		private string _duree4 = "";
		private string _duree5 = "";

		#region Icons
		private BitmapImage _film1Icon1 = null;
		private BitmapImage _film1Icon2 = null;
		private BitmapImage _film1Icon3 = null;
		private BitmapImage _film1Icon4 = null;
		private BitmapImage _film1Icon5 = null;
		private BitmapImage _film1Icon6 = null;
		private BitmapImage _film1Icon7 = null;
		private BitmapImage _film1Icon8 = null;
		private BitmapImage _film1Icon9 = null;
		private BitmapImage _film1Icon10 = null;
		private BitmapImage _film1Icon11 = null;
		private BitmapImage _film1Icon12 = null;
		private BitmapImage _film1Icon13 = null;
		private BitmapImage _film1Icon14 = null;
		private BitmapImage _film1Icon15 = null;
		private BitmapImage _film1Icon16 = null;
		private BitmapImage _film1Icon17 = null;
		private BitmapImage _film1Icon18 = null;
		private BitmapImage _film1Icon19 = null;
		private BitmapImage _film1Icon20 = null;

		private BitmapImage _film2Icon1 = null;
		private BitmapImage _film2Icon2 = null;
		private BitmapImage _film2Icon3 = null;
		private BitmapImage _film2Icon4 = null;
		private BitmapImage _film2Icon5 = null;
		private BitmapImage _film2Icon6 = null;
		private BitmapImage _film2Icon7 = null;
		private BitmapImage _film2Icon8 = null;
		private BitmapImage _film2Icon9 = null;
		private BitmapImage _film2Icon10 = null;
		private BitmapImage _film2Icon11 = null;
		private BitmapImage _film2Icon12 = null;
		private BitmapImage _film2Icon13 = null;
		private BitmapImage _film2Icon14 = null;
		private BitmapImage _film2Icon15 = null;
		private BitmapImage _film2Icon16 = null;
		private BitmapImage _film2Icon17 = null;
		private BitmapImage _film2Icon18 = null;
		private BitmapImage _film2Icon19 = null;
		private BitmapImage _film2Icon20 = null;

		private BitmapImage _film3Icon1 = null;
		private BitmapImage _film3Icon2 = null;
		private BitmapImage _film3Icon3 = null;
		private BitmapImage _film3Icon4 = null;
		private BitmapImage _film3Icon5 = null;
		private BitmapImage _film3Icon6 = null;
		private BitmapImage _film3Icon7 = null;
		private BitmapImage _film3Icon8 = null;
		private BitmapImage _film3Icon9 = null;
		private BitmapImage _film3Icon10 = null;
		private BitmapImage _film3Icon11 = null;
		private BitmapImage _film3Icon12 = null;
		private BitmapImage _film3Icon13 = null;
		private BitmapImage _film3Icon14 = null;
		private BitmapImage _film3Icon15 = null;
		private BitmapImage _film3Icon16 = null;
		private BitmapImage _film3Icon17 = null;
		private BitmapImage _film3Icon18 = null;
		private BitmapImage _film3Icon19 = null;
		private BitmapImage _film3Icon20 = null;

		private BitmapImage _film4Icon1 = null;
		private BitmapImage _film4Icon2 = null;
		private BitmapImage _film4Icon3 = null;
		private BitmapImage _film4Icon4 = null;
		private BitmapImage _film4Icon5 = null;
		private BitmapImage _film4Icon6 = null;
		private BitmapImage _film4Icon7 = null;
		private BitmapImage _film4Icon8 = null;
		private BitmapImage _film4Icon9 = null;
		private BitmapImage _film4Icon10 = null;
		private BitmapImage _film4Icon11 = null;
		private BitmapImage _film4Icon12 = null;
		private BitmapImage _film4Icon13 = null;
		private BitmapImage _film4Icon14 = null;
		private BitmapImage _film4Icon15 = null;
		private BitmapImage _film4Icon16 = null;
		private BitmapImage _film4Icon17 = null;
		private BitmapImage _film4Icon18 = null;
		private BitmapImage _film4Icon19 = null;
		private BitmapImage _film4Icon20 = null;

		private BitmapImage _film5Icon1 = null;
		private BitmapImage _film5Icon2 = null;
		private BitmapImage _film5Icon3 = null;
		private BitmapImage _film5Icon4 = null;
		private BitmapImage _film5Icon5 = null;
		private BitmapImage _film5Icon6 = null;
		private BitmapImage _film5Icon7 = null;
		private BitmapImage _film5Icon8 = null;
		private BitmapImage _film5Icon9 = null;
		private BitmapImage _film5Icon10 = null;
		private BitmapImage _film5Icon11 = null;
		private BitmapImage _film5Icon12 = null;
		private BitmapImage _film5Icon13 = null;
		private BitmapImage _film5Icon14 = null;
		private BitmapImage _film5Icon15 = null;
		private BitmapImage _film5Icon16 = null;
		private BitmapImage _film5Icon17 = null;
		private BitmapImage _film5Icon18 = null;
		private BitmapImage _film5Icon19 = null;
		private BitmapImage _film5Icon20 = null;

		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public ShellViewModel()
		{
			Trace.WriteLine("[ShellViewModel]Debut");
			shellModel = new ShellModel();
			#region Image
			Image1 = shellModel.getImage(1);
			Image2 = shellModel.getImage(2);
			Image3 = shellModel.getImage(3);
			Image4 = shellModel.getImage(4);
			Image5 = shellModel.getImage(5);
			#endregion

			#region Titre
			Titre1 = shellModel.getTitre(1);
			Titre2 = shellModel.getTitre(2);
			Titre3 = shellModel.getTitre(3);
			Titre4 = shellModel.getTitre(4);
			Titre5 = shellModel.getTitre(5);
			#endregion

			#region Duree
			Duree1 = shellModel.getDuree(1);
			Duree2 = shellModel.getDuree(2);
			Duree3 = shellModel.getDuree(3);
			Duree4 = shellModel.getDuree(4);
			Duree5 = shellModel.getDuree(5);
			#endregion

			#region Icon
			Film1Icon1 = shellModel.getIcon(1, 1);
			Film1Icon2 = shellModel.getIcon(1, 2);
			Film1Icon3 = shellModel.getIcon(1, 3);
			Film1Icon4 = shellModel.getIcon(1, 4);
			Film1Icon5 = shellModel.getIcon(1, 5);
			Film1Icon6 = shellModel.getIcon(1, 6);
			Film1Icon7 = shellModel.getIcon(1, 7);
			Film1Icon8 = shellModel.getIcon(1, 8);
			Film1Icon9 = shellModel.getIcon(1, 9);
			Film1Icon10 = shellModel.getIcon(1, 10);
			Film1Icon11 = shellModel.getIcon(1, 11);
			Film1Icon12 = shellModel.getIcon(1, 12);
			Film1Icon13 = shellModel.getIcon(1, 13);
			Film1Icon14 = shellModel.getIcon(1, 14);
			Film1Icon15 = shellModel.getIcon(1, 15);
			Film1Icon16 = shellModel.getIcon(1, 16);
			Film1Icon17 = shellModel.getIcon(1, 17);
			Film1Icon18 = shellModel.getIcon(1, 18);
			Film1Icon19 = shellModel.getIcon(1, 19);
			Film1Icon20 = shellModel.getIcon(1, 20);
			Film2Icon1 = shellModel.getIcon(2, 1);
			Film2Icon2 = shellModel.getIcon(2, 2);
			Film2Icon3 = shellModel.getIcon(2, 3);
			Film2Icon4 = shellModel.getIcon(2, 4);
			Film2Icon5 = shellModel.getIcon(2, 5);
			Film2Icon6 = shellModel.getIcon(2, 6);
			Film2Icon7 = shellModel.getIcon(2, 7);
			Film2Icon8 = shellModel.getIcon(2, 8);
			Film2Icon9 = shellModel.getIcon(2, 9);
			Film2Icon10 = shellModel.getIcon(2, 10);
			Film2Icon11 = shellModel.getIcon(2, 11);
			Film2Icon12 = shellModel.getIcon(2, 12);
			Film2Icon13 = shellModel.getIcon(2, 13);
			Film2Icon14 = shellModel.getIcon(2, 14);
			Film2Icon15 = shellModel.getIcon(2, 15);
			Film2Icon16 = shellModel.getIcon(2, 16);
			Film2Icon17 = shellModel.getIcon(2, 17);
			Film2Icon18 = shellModel.getIcon(2, 18);
			Film2Icon19 = shellModel.getIcon(2, 19);
			Film2Icon20 = shellModel.getIcon(2, 20);
			Film3Icon1 = shellModel.getIcon(3, 1);
			Film3Icon2 = shellModel.getIcon(3, 2);
			Film3Icon3 = shellModel.getIcon(3, 3);
			Film3Icon4 = shellModel.getIcon(3, 4);
			Film3Icon5 = shellModel.getIcon(3, 5);
			Film3Icon6 = shellModel.getIcon(3, 6);
			Film3Icon7 = shellModel.getIcon(3, 7);
			Film3Icon8 = shellModel.getIcon(3, 8);
			Film3Icon9 = shellModel.getIcon(3, 9);
			Film3Icon10 = shellModel.getIcon(3, 10);
			Film3Icon11 = shellModel.getIcon(3, 11);
			Film3Icon12 = shellModel.getIcon(3, 12);
			Film3Icon13 = shellModel.getIcon(3, 13);
			Film3Icon14 = shellModel.getIcon(3, 14);
			Film3Icon15 = shellModel.getIcon(3, 15);
			Film3Icon16 = shellModel.getIcon(3, 16);
			Film3Icon17 = shellModel.getIcon(3, 17);
			Film3Icon18 = shellModel.getIcon(3, 18);
			Film3Icon19 = shellModel.getIcon(3, 19);
			Film3Icon20 = shellModel.getIcon(3, 20);
			Film4Icon1 = shellModel.getIcon(4, 1);
			Film4Icon2 = shellModel.getIcon(4, 2);
			Film4Icon3 = shellModel.getIcon(4, 3);
			Film4Icon4 = shellModel.getIcon(4, 4);
			Film4Icon5 = shellModel.getIcon(4, 5);
			Film4Icon6 = shellModel.getIcon(4, 6);
			Film4Icon7 = shellModel.getIcon(4, 7);
			Film4Icon8 = shellModel.getIcon(4, 8);
			Film4Icon9 = shellModel.getIcon(4, 9);
			Film4Icon10 = shellModel.getIcon(4, 10);
			Film4Icon11 = shellModel.getIcon(4, 11);
			Film4Icon12 = shellModel.getIcon(4, 12);
			Film4Icon13 = shellModel.getIcon(4, 13);
			Film4Icon14 = shellModel.getIcon(4, 14);
			Film4Icon15 = shellModel.getIcon(4, 15);
			Film4Icon16 = shellModel.getIcon(4, 16);
			Film4Icon17 = shellModel.getIcon(4, 17);
			Film4Icon18 = shellModel.getIcon(4, 18);
			Film4Icon19 = shellModel.getIcon(4, 19);
			Film4Icon20 = shellModel.getIcon(4, 20);
			Film5Icon1 = shellModel.getIcon(5, 1);
			Film5Icon2 = shellModel.getIcon(5, 2);
			Film5Icon3 = shellModel.getIcon(5, 3);
			Film5Icon4 = shellModel.getIcon(5, 4);
			Film5Icon5 = shellModel.getIcon(5, 5);
			Film5Icon6 = shellModel.getIcon(5, 6);
			Film5Icon7 = shellModel.getIcon(5, 7);
			Film5Icon8 = shellModel.getIcon(5, 8);
			Film5Icon9 = shellModel.getIcon(5, 9);
			Film5Icon10 = shellModel.getIcon(5, 10);
			Film5Icon11 = shellModel.getIcon(5, 11);
			Film5Icon12 = shellModel.getIcon(5, 12);
			Film5Icon13 = shellModel.getIcon(5, 13);
			Film5Icon14 = shellModel.getIcon(5, 14);
			Film5Icon15 = shellModel.getIcon(5, 15);
			Film5Icon16 = shellModel.getIcon(5, 16);
			Film5Icon17 = shellModel.getIcon(5, 17);
			Film5Icon18 = shellModel.getIcon(5, 18);
			Film5Icon19 = shellModel.getIcon(5, 19);
			Film5Icon20 = shellModel.getIcon(5, 20);
			#endregion

			//Image1 = new BitmapImage(new Uri("https://image.tmdb.org/t/p/original/ojDg0PGvs6R9xYFodRct2kdI6wC.jpg?api_key=2742a4a95911c545482b8eff15183dcf"));

		}

		//C# ne supporte pas les "indexed properties" donc on va faire pleins de property simple
		#region Properties

		public string SearchText
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_searchText))
					return _searchText;

				_searchText = _searchText.Normalize(NormalizationForm.FormD);
				var chars = _searchText.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
				return new string(chars).Normalize(NormalizationForm.FormC);
			}
			set
			{
				_searchText = value;
				this.CurrentPage();
				OnPropertyChanged();
			}
		}

		

		#region Image
		public BitmapImage Image1
		{
			get
			{
				return _image1;
			}
			set
			{
				_image1 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Image2
		{
			get
			{
				return _image2;
			}
			set
			{
				_image2 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Image3
		{
			get
			{
				return _image3;
			}
			set
			{
				_image3 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Image4
		{
			get
			{
				return _image4;
			}
			set
			{
				_image4 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Image5
		{
			get
			{
				return _image5;
			}
			set
			{
				_image5 = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region Titre
		public string Titre1
		{
			get
			{
				return _titre1;
			}
			set
			{
				_titre1 = value;
				OnPropertyChanged();
			}
		}
		public string Titre2
		{
			get
			{
				return _titre2;
			}
			set
			{
				_titre2 = value;
				OnPropertyChanged();
			}
		}
		public string Titre3
		{
			get
			{
				return _titre3;
			}
			set
			{
				_titre3 = value;
				OnPropertyChanged();
			}
		}
		public string Titre4
		{
			get
			{
				return _titre4;
			}
			set
			{
				_titre4 = value;
				OnPropertyChanged();
			}
		}
		public string Titre5
		{
			get
			{
				return _titre5;
			}
			set
			{
				_titre5 = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Duree
		public string Duree1
		{
			get
			{
				return _duree1;
			}
			set
			{
				_duree1= value;
				OnPropertyChanged();
			}
		}
		public string Duree2
		{
			get
			{
				return _duree2;
			}
			set
			{
				_duree2 = value;
				OnPropertyChanged();
			}
		}
		public string Duree3
		{
			get
			{
				return _duree3;
			}
			set
			{
				_duree3 = value;
				OnPropertyChanged();
			}
		}
		public string Duree4
		{
			get
			{
				return _duree4;
			}
			set
			{
				_duree4 = value;
				OnPropertyChanged();
			}
		}
		public string Duree5
		{
			get
			{
				return _duree5;
			}
			set
			{
				_duree5 = value;
				OnPropertyChanged();
			}
		}


		#endregion

		#region Icons
		public BitmapImage Film1Icon1
		{
			get
			{
				return _film1Icon1;
			}
			set
			{
				_film1Icon1 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon2
		{
			get
			{
				return _film1Icon2;
			}
			set
			{
				_film1Icon2 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon3
		{
			get
			{
				return _film1Icon3;
			}
			set
			{
				_film1Icon3 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon4
		{
			get
			{
				return _film1Icon4;
			}
			set
			{
				_film1Icon4 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon5
		{
			get
			{
				return _film1Icon5;
			}
			set
			{
				_film1Icon5 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon6
		{
			get
			{
				return _film1Icon6;
			}
			set
			{
				_film1Icon6 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon7
		{
			get
			{
				return _film1Icon7;
			}
			set
			{
				_film1Icon7 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon8
		{
			get
			{
				return _film1Icon8;
			}
			set
			{
				_film1Icon8 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon9
		{
			get
			{
				return _film1Icon9;
			}
			set
			{
				_film1Icon9 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon10
		{
			get
			{
				return _film1Icon10;
			}
			set
			{
				_film1Icon10 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon11
		{
			get
			{
				return _film1Icon11;
			}
			set
			{
				_film1Icon11 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon12
		{
			get
			{
				return _film1Icon12;
			}
			set
			{
				_film1Icon12 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon13
		{
			get
			{
				return _film1Icon13;
			}
			set
			{
				_film1Icon13 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon14
		{
			get
			{
				return _film1Icon14;
			}
			set
			{
				_film1Icon14 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon15
		{
			get
			{
				return _film1Icon15;
			}
			set
			{
				_film1Icon15 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon16
		{
			get
			{
				return _film1Icon16;
			}
			set
			{
				_film1Icon16 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon17
		{
			get
			{
				return _film1Icon17;
			}
			set
			{
				_film1Icon17 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon18
		{
			get
			{
				return _film1Icon18;
			}
			set
			{
				_film1Icon18 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon19
		{
			get
			{
				return _film1Icon19;
			}
			set
			{
				_film1Icon19 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film1Icon20
		{
			get
			{
				return _film1Icon20;
			}
			set
			{
				_film1Icon20 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon1
		{
			get
			{
				return _film2Icon1;
			}
			set
			{
				_film2Icon1 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon2
		{
			get
			{
				return _film2Icon2;
			}
			set
			{
				_film2Icon2 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon3
		{
			get
			{
				return _film2Icon3;
			}
			set
			{
				_film2Icon3 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon4
		{
			get
			{
				return _film2Icon4;
			}
			set
			{
				_film2Icon4 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon5
		{
			get
			{
				return _film2Icon5;
			}
			set
			{
				_film2Icon5 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon6
		{
			get
			{
				return _film2Icon6;
			}
			set
			{
				_film2Icon6 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon7
		{
			get
			{
				return _film2Icon7;
			}
			set
			{
				_film2Icon7 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon8
		{
			get
			{
				return _film2Icon8;
			}
			set
			{
				_film2Icon8 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon9
		{
			get
			{
				return _film2Icon9;
			}
			set
			{
				_film2Icon9 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon10
		{
			get
			{
				return _film2Icon10;
			}
			set
			{
				_film2Icon10 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon11
		{
			get
			{
				return _film2Icon11;
			}
			set
			{
				_film2Icon11 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon12
		{
			get
			{
				return _film2Icon12;
			}
			set
			{
				_film2Icon12 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon13
		{
			get
			{
				return _film2Icon13;
			}
			set
			{
				_film2Icon13 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon14
		{
			get
			{
				return _film2Icon14;
			}
			set
			{
				_film2Icon14 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon15
		{
			get
			{
				return _film2Icon15;
			}
			set
			{
				_film2Icon15 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon16
		{
			get
			{
				return _film2Icon16;
			}
			set
			{
				_film2Icon16 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon17
		{
			get
			{
				return _film2Icon17;
			}
			set
			{
				_film2Icon17 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon18
		{
			get
			{
				return _film2Icon18;
			}
			set
			{
				_film2Icon18 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon19
		{
			get
			{
				return _film2Icon19;
			}
			set
			{
				_film2Icon19 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film2Icon20
		{
			get
			{
				return _film2Icon20;
			}
			set
			{
				_film2Icon20 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon1
		{
			get
			{
				return _film3Icon1;
			}
			set
			{
				_film3Icon1 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon2
		{
			get
			{
				return _film3Icon2;
			}
			set
			{
				_film3Icon2 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon3
		{
			get
			{
				return _film3Icon3;
			}
			set
			{
				_film3Icon3 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon4
		{
			get
			{
				return _film3Icon4;
			}
			set
			{
				_film3Icon4 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon5
		{
			get
			{
				return _film3Icon5;
			}
			set
			{
				_film3Icon5 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon6
		{
			get
			{
				return _film3Icon6;
			}
			set
			{
				_film3Icon6 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon7
		{
			get
			{
				return _film3Icon7;
			}
			set
			{
				_film3Icon7 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon8
		{
			get
			{
				return _film3Icon8;
			}
			set
			{
				_film3Icon8 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon9
		{
			get
			{
				return _film3Icon9;
			}
			set
			{
				_film3Icon9 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon10
		{
			get
			{
				return _film3Icon10;
			}
			set
			{
				_film3Icon10 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon11
		{
			get
			{
				return _film3Icon11;
			}
			set
			{
				_film3Icon11 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon12
		{
			get
			{
				return _film3Icon12;
			}
			set
			{
				_film3Icon12 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon13
		{
			get
			{
				return _film3Icon13;
			}
			set
			{
				_film3Icon13 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon14
		{
			get
			{
				return _film3Icon14;
			}
			set
			{
				_film3Icon14 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon15
		{
			get
			{
				return _film3Icon15;
			}
			set
			{
				_film3Icon15 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon16
		{
			get
			{
				return _film3Icon16;
			}
			set
			{
				_film3Icon16 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon17
		{
			get
			{
				return _film3Icon17;
			}
			set
			{
				_film3Icon17 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon18
		{
			get
			{
				return _film3Icon18;
			}
			set
			{
				_film3Icon18 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon19
		{
			get
			{
				return _film3Icon19;
			}
			set
			{
				_film3Icon19 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film3Icon20
		{
			get
			{
				return _film3Icon20;
			}
			set
			{
				_film3Icon20 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon1
		{
			get
			{
				return _film4Icon1;
			}
			set
			{
				_film4Icon1 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon2
		{
			get
			{
				return _film4Icon2;
			}
			set
			{
				_film4Icon2 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon3
		{
			get
			{
				return _film4Icon3;
			}
			set
			{
				_film4Icon3 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon4
		{
			get
			{
				return _film4Icon4;
			}
			set
			{
				_film4Icon4 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon5
		{
			get
			{
				return _film4Icon5;
			}
			set
			{
				_film4Icon5 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon6
		{
			get
			{
				return _film4Icon6;
			}
			set
			{
				_film4Icon6 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon7
		{
			get
			{
				return _film4Icon7;
			}
			set
			{
				_film4Icon7 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon8
		{
			get
			{
				return _film4Icon8;
			}
			set
			{
				_film4Icon8 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon9
		{
			get
			{
				return _film4Icon9;
			}
			set
			{
				_film4Icon9 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon10
		{
			get
			{
				return _film4Icon10;
			}
			set
			{
				_film4Icon10 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon11
		{
			get
			{
				return _film4Icon11;
			}
			set
			{
				_film4Icon11 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon12
		{
			get
			{
				return _film4Icon12;
			}
			set
			{
				_film4Icon12 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon13
		{
			get
			{
				return _film4Icon13;
			}
			set
			{
				_film4Icon13 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon14
		{
			get
			{
				return _film4Icon14;
			}
			set
			{
				_film4Icon14 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon15
		{
			get
			{
				return _film4Icon15;
			}
			set
			{
				_film4Icon15 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon16
		{
			get
			{
				return _film4Icon16;
			}
			set
			{
				_film4Icon16 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon17
		{
			get
			{
				return _film4Icon17;
			}
			set
			{
				_film4Icon17 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon18
		{
			get
			{
				return _film4Icon18;
			}
			set
			{
				_film4Icon18 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon19
		{
			get
			{
				return _film4Icon19;
			}
			set
			{
				_film4Icon19 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film4Icon20
		{
			get
			{
				return _film4Icon20;
			}
			set
			{
				_film4Icon20 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon1
		{
			get
			{
				return _film5Icon1;
			}
			set
			{
				_film5Icon1 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon2
		{
			get
			{
				return _film5Icon2;
			}
			set
			{
				_film5Icon2 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon3
		{
			get
			{
				return _film5Icon3;
			}
			set
			{
				_film5Icon3 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon4
		{
			get
			{
				return _film5Icon4;
			}
			set
			{
				_film5Icon4 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon5
		{
			get
			{
				return _film5Icon5;
			}
			set
			{
				_film5Icon5 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon6
		{
			get
			{
				return _film5Icon6;
			}
			set
			{
				_film5Icon6 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon7
		{
			get
			{
				return _film5Icon7;
			}
			set
			{
				_film5Icon7 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon8
		{
			get
			{
				return _film5Icon8;
			}
			set
			{
				_film5Icon8 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon9
		{
			get
			{
				return _film5Icon9;
			}
			set
			{
				_film5Icon9 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon10
		{
			get
			{
				return _film5Icon10;
			}
			set
			{
				_film5Icon10 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon11
		{
			get
			{
				return _film5Icon11;
			}
			set
			{
				_film5Icon11 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon12
		{
			get
			{
				return _film5Icon12;
			}
			set
			{
				_film5Icon12 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon13
		{
			get
			{
				return _film5Icon13;
			}
			set
			{
				_film5Icon13 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon14
		{
			get
			{
				return _film5Icon14;
			}
			set
			{
				_film5Icon14 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon15
		{
			get
			{
				return _film5Icon15;
			}
			set
			{
				_film5Icon15 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon16
		{
			get
			{
				return _film5Icon16;
			}
			set
			{
				_film5Icon16 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon17
		{
			get
			{
				return _film5Icon17;
			}
			set
			{
				_film5Icon17 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon18
		{
			get
			{
				return _film5Icon18;
			}
			set
			{
				_film5Icon18 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon19
		{
			get
			{
				return _film5Icon19;
			}
			set
			{
				_film5Icon19 = value;
				OnPropertyChanged();
			}
		}
		public BitmapImage Film5Icon20
		{
			get
			{
				return _film5Icon20;
			}
			set
			{
				_film5Icon20 = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region Details
		public RelayCommand Details1
		{
			get
			{
				return new RelayCommand(() => this.ExecDetails1());
			}
		}
		private void ExecDetails1()
		{
			//On test si la case est vide
			if(shellModel.getIcon(1,1) != null)
			{
				int id = shellModel.getId(1);
				if(id == -1)
				{
					Trace.WriteLine("[ExecDetails1]Error getId returned -1");
					return;
				}
				CommentView commentView = new CommentView(id);
				commentView.ShowDialog();
			}
		}

		public RelayCommand Details2
		{
			get
			{
				return new RelayCommand(() => this.ExecDetails2());
			}
		}
		private void ExecDetails2()
		{
			//On test si la case est vide
			if (shellModel.getIcon(2, 1) != null)
			{
				int id = shellModel.getId(2);
				if (id == -1)
				{
					Trace.WriteLine("[ExecDetails1]Error getId returned -1");
					return;
				}
				CommentView commentView = new CommentView(id);
				commentView.ShowDialog();
			}
		}

		public RelayCommand Details3
		{
			get
			{
				return new RelayCommand(() => this.ExecDetails3());
			}
		}
		private void ExecDetails3()
		{
			//On test si la case est vide
			if (shellModel.getIcon(3, 1) != null)
			{
				int id = shellModel.getId(3);
				if (id == -1)
				{
					Trace.WriteLine("[ExecDetails1]Error getId returned -1");
					return;
				}
				CommentView commentView = new CommentView(id);
				commentView.ShowDialog();
			}
		}

		public RelayCommand Details4
		{
			get
			{
				return new RelayCommand(() => this.ExecDetails4());
			}
		}
		private void ExecDetails4()
		{
			//On test si la case est vide
			if (shellModel.getIcon(4, 1) != null)
			{
				int id = shellModel.getId(4);
				if (id == -1)
				{
					Trace.WriteLine("[ExecDetails1]Error getId returned -1");
					return;
				}
				CommentView commentView = new CommentView(id);
				commentView.ShowDialog();
			}
		}

		public RelayCommand Details5
		{
			get
			{
				return new RelayCommand(() => this.ExecDetails5());
			}
		}
		private void ExecDetails5()
		{
			//On test si la case est vide
			if (shellModel.getIcon(5, 1) != null)
			{
				int id = shellModel.getId(5);
				if (id == -1)
				{
					Trace.WriteLine("[ExecDetails1]Error getId returned -1");
					return;
				}
				CommentView commentView = new CommentView(id);
				commentView.ShowDialog();
			}
		}

		#endregion

		#region BouttonNext/Previous

		public RelayCommand Next
		{
			get
			{
				return new RelayCommand(() => this.NextPage());
			}
		}

		private void CurrentPage()
		{
			if (SearchText.Length == 0)
				shellModel.LoadCurrentPage(null);
			else
				shellModel.LoadCurrentPage(SearchText);
			
			#region Image
			Image1 = shellModel.getImage(1);
			Image2 = shellModel.getImage(2);
			Image3 = shellModel.getImage(3);
			Image4 = shellModel.getImage(4);
			Image5 = shellModel.getImage(5);
			#endregion

			#region Titre
			Titre1 = shellModel.getTitre(1);
			Titre2 = shellModel.getTitre(2);
			Titre3 = shellModel.getTitre(3);
			Titre4 = shellModel.getTitre(4);
			Titre5 = shellModel.getTitre(5);
			#endregion

			#region Duree
			Duree1 = shellModel.getDuree(1);
			Duree2 = shellModel.getDuree(2);
			Duree3 = shellModel.getDuree(3);
			Duree4 = shellModel.getDuree(4);
			Duree5 = shellModel.getDuree(5);
			#endregion

			#region Icon
			Film1Icon1 = shellModel.getIcon(1, 1);
			Film1Icon2 = shellModel.getIcon(1, 2);
			Film1Icon3 = shellModel.getIcon(1, 3);
			Film1Icon4 = shellModel.getIcon(1, 4);
			Film1Icon5 = shellModel.getIcon(1, 5);
			Film1Icon6 = shellModel.getIcon(1, 6);
			Film1Icon7 = shellModel.getIcon(1, 7);
			Film1Icon8 = shellModel.getIcon(1, 8);
			Film1Icon9 = shellModel.getIcon(1, 9);
			Film1Icon10 = shellModel.getIcon(1, 10);
			Film1Icon11 = shellModel.getIcon(1, 11);
			Film1Icon12 = shellModel.getIcon(1, 12);
			Film1Icon13 = shellModel.getIcon(1, 13);
			Film1Icon14 = shellModel.getIcon(1, 14);
			Film1Icon15 = shellModel.getIcon(1, 15);
			Film1Icon16 = shellModel.getIcon(1, 16);
			Film1Icon17 = shellModel.getIcon(1, 17);
			Film1Icon18 = shellModel.getIcon(1, 18);
			Film1Icon19 = shellModel.getIcon(1, 19);
			Film1Icon20 = shellModel.getIcon(1, 20);
			Film2Icon1 = shellModel.getIcon(2, 1);
			Film2Icon2 = shellModel.getIcon(2, 2);
			Film2Icon3 = shellModel.getIcon(2, 3);
			Film2Icon4 = shellModel.getIcon(2, 4);
			Film2Icon5 = shellModel.getIcon(2, 5);
			Film2Icon6 = shellModel.getIcon(2, 6);
			Film2Icon7 = shellModel.getIcon(2, 7);
			Film2Icon8 = shellModel.getIcon(2, 8);
			Film2Icon9 = shellModel.getIcon(2, 9);
			Film2Icon10 = shellModel.getIcon(2, 10);
			Film2Icon11 = shellModel.getIcon(2, 11);
			Film2Icon12 = shellModel.getIcon(2, 12);
			Film2Icon13 = shellModel.getIcon(2, 13);
			Film2Icon14 = shellModel.getIcon(2, 14);
			Film2Icon15 = shellModel.getIcon(2, 15);
			Film2Icon16 = shellModel.getIcon(2, 16);
			Film2Icon17 = shellModel.getIcon(2, 17);
			Film2Icon18 = shellModel.getIcon(2, 18);
			Film2Icon19 = shellModel.getIcon(2, 19);
			Film2Icon20 = shellModel.getIcon(2, 20);
			Film3Icon1 = shellModel.getIcon(3, 1);
			Film3Icon2 = shellModel.getIcon(3, 2);
			Film3Icon3 = shellModel.getIcon(3, 3);
			Film3Icon4 = shellModel.getIcon(3, 4);
			Film3Icon5 = shellModel.getIcon(3, 5);
			Film3Icon6 = shellModel.getIcon(3, 6);
			Film3Icon7 = shellModel.getIcon(3, 7);
			Film3Icon8 = shellModel.getIcon(3, 8);
			Film3Icon9 = shellModel.getIcon(3, 9);
			Film3Icon10 = shellModel.getIcon(3, 10);
			Film3Icon11 = shellModel.getIcon(3, 11);
			Film3Icon12 = shellModel.getIcon(3, 12);
			Film3Icon13 = shellModel.getIcon(3, 13);
			Film3Icon14 = shellModel.getIcon(3, 14);
			Film3Icon15 = shellModel.getIcon(3, 15);
			Film3Icon16 = shellModel.getIcon(3, 16);
			Film3Icon17 = shellModel.getIcon(3, 17);
			Film3Icon18 = shellModel.getIcon(3, 18);
			Film3Icon19 = shellModel.getIcon(3, 19);
			Film3Icon20 = shellModel.getIcon(3, 20);
			Film4Icon1 = shellModel.getIcon(4, 1);
			Film4Icon2 = shellModel.getIcon(4, 2);
			Film4Icon3 = shellModel.getIcon(4, 3);
			Film4Icon4 = shellModel.getIcon(4, 4);
			Film4Icon5 = shellModel.getIcon(4, 5);
			Film4Icon6 = shellModel.getIcon(4, 6);
			Film4Icon7 = shellModel.getIcon(4, 7);
			Film4Icon8 = shellModel.getIcon(4, 8);
			Film4Icon9 = shellModel.getIcon(4, 9);
			Film4Icon10 = shellModel.getIcon(4, 10);
			Film4Icon11 = shellModel.getIcon(4, 11);
			Film4Icon12 = shellModel.getIcon(4, 12);
			Film4Icon13 = shellModel.getIcon(4, 13);
			Film4Icon14 = shellModel.getIcon(4, 14);
			Film4Icon15 = shellModel.getIcon(4, 15);
			Film4Icon16 = shellModel.getIcon(4, 16);
			Film4Icon17 = shellModel.getIcon(4, 17);
			Film4Icon18 = shellModel.getIcon(4, 18);
			Film4Icon19 = shellModel.getIcon(4, 19);
			Film4Icon20 = shellModel.getIcon(4, 20);
			Film5Icon1 = shellModel.getIcon(5, 1);
			Film5Icon2 = shellModel.getIcon(5, 2);
			Film5Icon3 = shellModel.getIcon(5, 3);
			Film5Icon4 = shellModel.getIcon(5, 4);
			Film5Icon5 = shellModel.getIcon(5, 5);
			Film5Icon6 = shellModel.getIcon(5, 6);
			Film5Icon7 = shellModel.getIcon(5, 7);
			Film5Icon8 = shellModel.getIcon(5, 8);
			Film5Icon9 = shellModel.getIcon(5, 9);
			Film5Icon10 = shellModel.getIcon(5, 10);
			Film5Icon11 = shellModel.getIcon(5, 11);
			Film5Icon12 = shellModel.getIcon(5, 12);
			Film5Icon13 = shellModel.getIcon(5, 13);
			Film5Icon14 = shellModel.getIcon(5, 14);
			Film5Icon15 = shellModel.getIcon(5, 15);
			Film5Icon16 = shellModel.getIcon(5, 16);
			Film5Icon17 = shellModel.getIcon(5, 17);
			Film5Icon18 = shellModel.getIcon(5, 18);
			Film5Icon19 = shellModel.getIcon(5, 19);
			Film5Icon20 = shellModel.getIcon(5, 20);
			#endregion

			//Reset tout les bindings
			OnPropertyChanged(string.Empty);
		}

		private void NextPage()
		{
			//Charger les 5 prochain films
			shellModel.loadNextMovies(SearchText);
			//Mettre a jours les propreties
			#region Image
			Image1 = shellModel.getImage(1);
			Image2 = shellModel.getImage(2);
			Image3 = shellModel.getImage(3);
			Image4 = shellModel.getImage(4);
			Image5 = shellModel.getImage(5);
			#endregion

			#region Titre
			Titre1 = shellModel.getTitre(1);
			Titre2 = shellModel.getTitre(2);
			Titre3 = shellModel.getTitre(3);
			Titre4 = shellModel.getTitre(4);
			Titre5 = shellModel.getTitre(5);
			#endregion

			#region Duree
			Duree1 = shellModel.getDuree(1);
			Duree2 = shellModel.getDuree(2);
			Duree3 = shellModel.getDuree(3);
			Duree4 = shellModel.getDuree(4);
			Duree5 = shellModel.getDuree(5);
			#endregion

			#region Icon
			Film1Icon1 = shellModel.getIcon(1, 1);
			Film1Icon2 = shellModel.getIcon(1, 2);
			Film1Icon3 = shellModel.getIcon(1, 3);
			Film1Icon4 = shellModel.getIcon(1, 4);
			Film1Icon5 = shellModel.getIcon(1, 5);
			Film1Icon6 = shellModel.getIcon(1, 6);
			Film1Icon7 = shellModel.getIcon(1, 7);
			Film1Icon8 = shellModel.getIcon(1, 8);
			Film1Icon9 = shellModel.getIcon(1, 9);
			Film1Icon10 = shellModel.getIcon(1, 10);
			Film1Icon11 = shellModel.getIcon(1, 11);
			Film1Icon12 = shellModel.getIcon(1, 12);
			Film1Icon13 = shellModel.getIcon(1, 13);
			Film1Icon14 = shellModel.getIcon(1, 14);
			Film1Icon15 = shellModel.getIcon(1, 15);
			Film1Icon16 = shellModel.getIcon(1, 16);
			Film1Icon17 = shellModel.getIcon(1, 17);
			Film1Icon18 = shellModel.getIcon(1, 18);
			Film1Icon19 = shellModel.getIcon(1, 19);
			Film1Icon20 = shellModel.getIcon(1, 20);
			Film2Icon1 = shellModel.getIcon(2, 1);
			Film2Icon2 = shellModel.getIcon(2, 2);
			Film2Icon3 = shellModel.getIcon(2, 3);
			Film2Icon4 = shellModel.getIcon(2, 4);
			Film2Icon5 = shellModel.getIcon(2, 5);
			Film2Icon6 = shellModel.getIcon(2, 6);
			Film2Icon7 = shellModel.getIcon(2, 7);
			Film2Icon8 = shellModel.getIcon(2, 8);
			Film2Icon9 = shellModel.getIcon(2, 9);
			Film2Icon10 = shellModel.getIcon(2, 10);
			Film2Icon11 = shellModel.getIcon(2, 11);
			Film2Icon12 = shellModel.getIcon(2, 12);
			Film2Icon13 = shellModel.getIcon(2, 13);
			Film2Icon14 = shellModel.getIcon(2, 14);
			Film2Icon15 = shellModel.getIcon(2, 15);
			Film2Icon16 = shellModel.getIcon(2, 16);
			Film2Icon17 = shellModel.getIcon(2, 17);
			Film2Icon18 = shellModel.getIcon(2, 18);
			Film2Icon19 = shellModel.getIcon(2, 19);
			Film2Icon20 = shellModel.getIcon(2, 20);
			Film3Icon1 = shellModel.getIcon(3, 1);
			Film3Icon2 = shellModel.getIcon(3, 2);
			Film3Icon3 = shellModel.getIcon(3, 3);
			Film3Icon4 = shellModel.getIcon(3, 4);
			Film3Icon5 = shellModel.getIcon(3, 5);
			Film3Icon6 = shellModel.getIcon(3, 6);
			Film3Icon7 = shellModel.getIcon(3, 7);
			Film3Icon8 = shellModel.getIcon(3, 8);
			Film3Icon9 = shellModel.getIcon(3, 9);
			Film3Icon10 = shellModel.getIcon(3, 10);
			Film3Icon11 = shellModel.getIcon(3, 11);
			Film3Icon12 = shellModel.getIcon(3, 12);
			Film3Icon13 = shellModel.getIcon(3, 13);
			Film3Icon14 = shellModel.getIcon(3, 14);
			Film3Icon15 = shellModel.getIcon(3, 15);
			Film3Icon16 = shellModel.getIcon(3, 16);
			Film3Icon17 = shellModel.getIcon(3, 17);
			Film3Icon18 = shellModel.getIcon(3, 18);
			Film3Icon19 = shellModel.getIcon(3, 19);
			Film3Icon20 = shellModel.getIcon(3, 20);
			Film4Icon1 = shellModel.getIcon(4, 1);
			Film4Icon2 = shellModel.getIcon(4, 2);
			Film4Icon3 = shellModel.getIcon(4, 3);
			Film4Icon4 = shellModel.getIcon(4, 4);
			Film4Icon5 = shellModel.getIcon(4, 5);
			Film4Icon6 = shellModel.getIcon(4, 6);
			Film4Icon7 = shellModel.getIcon(4, 7);
			Film4Icon8 = shellModel.getIcon(4, 8);
			Film4Icon9 = shellModel.getIcon(4, 9);
			Film4Icon10 = shellModel.getIcon(4, 10);
			Film4Icon11 = shellModel.getIcon(4, 11);
			Film4Icon12 = shellModel.getIcon(4, 12);
			Film4Icon13 = shellModel.getIcon(4, 13);
			Film4Icon14 = shellModel.getIcon(4, 14);
			Film4Icon15 = shellModel.getIcon(4, 15);
			Film4Icon16 = shellModel.getIcon(4, 16);
			Film4Icon17 = shellModel.getIcon(4, 17);
			Film4Icon18 = shellModel.getIcon(4, 18);
			Film4Icon19 = shellModel.getIcon(4, 19);
			Film4Icon20 = shellModel.getIcon(4, 20);
			Film5Icon1 = shellModel.getIcon(5, 1);
			Film5Icon2 = shellModel.getIcon(5, 2);
			Film5Icon3 = shellModel.getIcon(5, 3);
			Film5Icon4 = shellModel.getIcon(5, 4);
			Film5Icon5 = shellModel.getIcon(5, 5);
			Film5Icon6 = shellModel.getIcon(5, 6);
			Film5Icon7 = shellModel.getIcon(5, 7);
			Film5Icon8 = shellModel.getIcon(5, 8);
			Film5Icon9 = shellModel.getIcon(5, 9);
			Film5Icon10 = shellModel.getIcon(5, 10);
			Film5Icon11 = shellModel.getIcon(5, 11);
			Film5Icon12 = shellModel.getIcon(5, 12);
			Film5Icon13 = shellModel.getIcon(5, 13);
			Film5Icon14 = shellModel.getIcon(5, 14);
			Film5Icon15 = shellModel.getIcon(5, 15);
			Film5Icon16 = shellModel.getIcon(5, 16);
			Film5Icon17 = shellModel.getIcon(5, 17);
			Film5Icon18 = shellModel.getIcon(5, 18);
			Film5Icon19 = shellModel.getIcon(5, 19);
			Film5Icon20 = shellModel.getIcon(5, 20);
			#endregion

			//Reset tout les bindings
			OnPropertyChanged(string.Empty);
		}

		public RelayCommand Previous
		{
			get
			{
				return new RelayCommand(() => this.PreviousPage());
			}
		}
		private void PreviousPage()
		{
			//Charger les 5 prochain films
			shellModel.loadPreviousMovies(SearchText);
			//Mettre a jours les propreties
			#region Image
			Image1 = shellModel.getImage(1);
			Image2 = shellModel.getImage(2);
			Image3 = shellModel.getImage(3);
			Image4 = shellModel.getImage(4);
			Image5 = shellModel.getImage(5);
			#endregion

			#region Titre
			Titre1 = shellModel.getTitre(1);
			Titre2 = shellModel.getTitre(2);
			Titre3 = shellModel.getTitre(3);
			Titre4 = shellModel.getTitre(4);
			Titre5 = shellModel.getTitre(5);
			#endregion

			#region Duree
			Duree1 = shellModel.getDuree(1);
			Duree2 = shellModel.getDuree(2);
			Duree3 = shellModel.getDuree(3);
			Duree4 = shellModel.getDuree(4);
			Duree5 = shellModel.getDuree(5);
			#endregion

			#region Icon
			Film1Icon1 = shellModel.getIcon(1, 1);
			Film1Icon2 = shellModel.getIcon(1, 2);
			Film1Icon3 = shellModel.getIcon(1, 3);
			Film1Icon4 = shellModel.getIcon(1, 4);
			Film1Icon5 = shellModel.getIcon(1, 5);
			Film1Icon6 = shellModel.getIcon(1, 6);
			Film1Icon7 = shellModel.getIcon(1, 7);
			Film1Icon8 = shellModel.getIcon(1, 8);
			Film1Icon9 = shellModel.getIcon(1, 9);
			Film1Icon10 = shellModel.getIcon(1, 10);
			Film1Icon11 = shellModel.getIcon(1, 11);
			Film1Icon12 = shellModel.getIcon(1, 12);
			Film1Icon13 = shellModel.getIcon(1, 13);
			Film1Icon14 = shellModel.getIcon(1, 14);
			Film1Icon15 = shellModel.getIcon(1, 15);
			Film1Icon16 = shellModel.getIcon(1, 16);
			Film1Icon17 = shellModel.getIcon(1, 17);
			Film1Icon18 = shellModel.getIcon(1, 18);
			Film1Icon19 = shellModel.getIcon(1, 19);
			Film1Icon20 = shellModel.getIcon(1, 20);
			Film2Icon1 = shellModel.getIcon(2, 1);
			Film2Icon2 = shellModel.getIcon(2, 2);
			Film2Icon3 = shellModel.getIcon(2, 3);
			Film2Icon4 = shellModel.getIcon(2, 4);
			Film2Icon5 = shellModel.getIcon(2, 5);
			Film2Icon6 = shellModel.getIcon(2, 6);
			Film2Icon7 = shellModel.getIcon(2, 7);
			Film2Icon8 = shellModel.getIcon(2, 8);
			Film2Icon9 = shellModel.getIcon(2, 9);
			Film2Icon10 = shellModel.getIcon(2, 10);
			Film2Icon11 = shellModel.getIcon(2, 11);
			Film2Icon12 = shellModel.getIcon(2, 12);
			Film2Icon13 = shellModel.getIcon(2, 13);
			Film2Icon14 = shellModel.getIcon(2, 14);
			Film2Icon15 = shellModel.getIcon(2, 15);
			Film2Icon16 = shellModel.getIcon(2, 16);
			Film2Icon17 = shellModel.getIcon(2, 17);
			Film2Icon18 = shellModel.getIcon(2, 18);
			Film2Icon19 = shellModel.getIcon(2, 19);
			Film2Icon20 = shellModel.getIcon(2, 20);
			Film3Icon1 = shellModel.getIcon(3, 1);
			Film3Icon2 = shellModel.getIcon(3, 2);
			Film3Icon3 = shellModel.getIcon(3, 3);
			Film3Icon4 = shellModel.getIcon(3, 4);
			Film3Icon5 = shellModel.getIcon(3, 5);
			Film3Icon6 = shellModel.getIcon(3, 6);
			Film3Icon7 = shellModel.getIcon(3, 7);
			Film3Icon8 = shellModel.getIcon(3, 8);
			Film3Icon9 = shellModel.getIcon(3, 9);
			Film3Icon10 = shellModel.getIcon(3, 10);
			Film3Icon11 = shellModel.getIcon(3, 11);
			Film3Icon12 = shellModel.getIcon(3, 12);
			Film3Icon13 = shellModel.getIcon(3, 13);
			Film3Icon14 = shellModel.getIcon(3, 14);
			Film3Icon15 = shellModel.getIcon(3, 15);
			Film3Icon16 = shellModel.getIcon(3, 16);
			Film3Icon17 = shellModel.getIcon(3, 17);
			Film3Icon18 = shellModel.getIcon(3, 18);
			Film3Icon19 = shellModel.getIcon(3, 19);
			Film3Icon20 = shellModel.getIcon(3, 20);
			Film4Icon1 = shellModel.getIcon(4, 1);
			Film4Icon2 = shellModel.getIcon(4, 2);
			Film4Icon3 = shellModel.getIcon(4, 3);
			Film4Icon4 = shellModel.getIcon(4, 4);
			Film4Icon5 = shellModel.getIcon(4, 5);
			Film4Icon6 = shellModel.getIcon(4, 6);
			Film4Icon7 = shellModel.getIcon(4, 7);
			Film4Icon8 = shellModel.getIcon(4, 8);
			Film4Icon9 = shellModel.getIcon(4, 9);
			Film4Icon10 = shellModel.getIcon(4, 10);
			Film4Icon11 = shellModel.getIcon(4, 11);
			Film4Icon12 = shellModel.getIcon(4, 12);
			Film4Icon13 = shellModel.getIcon(4, 13);
			Film4Icon14 = shellModel.getIcon(4, 14);
			Film4Icon15 = shellModel.getIcon(4, 15);
			Film4Icon16 = shellModel.getIcon(4, 16);
			Film4Icon17 = shellModel.getIcon(4, 17);
			Film4Icon18 = shellModel.getIcon(4, 18);
			Film4Icon19 = shellModel.getIcon(4, 19);
			Film4Icon20 = shellModel.getIcon(4, 20);
			Film5Icon1 = shellModel.getIcon(5, 1);
			Film5Icon2 = shellModel.getIcon(5, 2);
			Film5Icon3 = shellModel.getIcon(5, 3);
			Film5Icon4 = shellModel.getIcon(5, 4);
			Film5Icon5 = shellModel.getIcon(5, 5);
			Film5Icon6 = shellModel.getIcon(5, 6);
			Film5Icon7 = shellModel.getIcon(5, 7);
			Film5Icon8 = shellModel.getIcon(5, 8);
			Film5Icon9 = shellModel.getIcon(5, 9);
			Film5Icon10 = shellModel.getIcon(5, 10);
			Film5Icon11 = shellModel.getIcon(5, 11);
			Film5Icon12 = shellModel.getIcon(5, 12);
			Film5Icon13 = shellModel.getIcon(5, 13);
			Film5Icon14 = shellModel.getIcon(5, 14);
			Film5Icon15 = shellModel.getIcon(5, 15);
			Film5Icon16 = shellModel.getIcon(5, 16);
			Film5Icon17 = shellModel.getIcon(5, 17);
			Film5Icon18 = shellModel.getIcon(5, 18);
			Film5Icon19 = shellModel.getIcon(5, 19);
			Film5Icon20 = shellModel.getIcon(5, 20);
			#endregion

			//Reset tout les bindings
			OnPropertyChanged(string.Empty);
		}
		#endregion

		#endregion

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
