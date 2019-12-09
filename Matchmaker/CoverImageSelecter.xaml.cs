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
        public int userID { get; set; }
        
        public CoverImageSelecter() {
            InitializeComponent();
            List<Img> images = new List<Img>();

            string[] files = new string[1];

            //files = Directory.GetFiles(@"https://145.44.233.207/images/covers/");
            files = Directory.GetFiles(@"../../Images");

            for (int i = 0; i < files.Length; i++) {
                images.Add(new Img() { Src = files[i] });
            }

            ImageList.ItemsSource = images;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e) {
            object image = ImageList.SelectedItem;
            if (image == null) {
                MessageBox.Show("Please select a cover image!", "");
            } else {
                MatchMakerClassLibrary.CoverImageData coverImageData = new MatchMakerClassLibrary.CoverImageData();
                coverImageData.userID = userID;
                coverImageData.imageName = (image as Img).Src;

                Console.WriteLine($"Src: {(image as Img).Src}");

                if (await MatchmakerAPI_Client.PostNewCoverImageDataAsync(coverImageData)) {
                    MessageBox.Show("Image saved!", "");
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }

    public class Img {
        public string Src { get; set; }
    }
}
