using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Matchmaker {
    public partial class CoverImageSelecter : Window {
        public CoverImageSelecter() {
            InitializeComponent();
            List<Img> images = new List<Img>();

            string[] files = new string[1];
            
            //files = Directory.GetFiles(@"https://145.44.233.207/images/covers/");
            files = Directory.GetFiles(@"../../Images");

            Console.WriteLine(files.Length);

            for (int i = 0; i < files.Length; i++) {
                images.Add(new Img() { Src = files[i] });
            }

            ImageList.ItemsSource = images;
		}

        private async void btnSaveExit_Click(object sender, RoutedEventArgs e) {
            foreach (object o in ImageList.SelectedItems) {
                //save image to user
                UserData userData = new UserData();
                //userData.id = ;
                userData.coverImage = (o as Img).Src;

                if (await MatchmakerAPI_Client.PostNewUserDataAsync(userData)) {
                    Close();
                }
            }
        }
    }

	public class Img {
		public string Src { get; set; }
	}
}
