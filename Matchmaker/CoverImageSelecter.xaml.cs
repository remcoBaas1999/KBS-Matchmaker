using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

            //Get the cover images available
            Dictionary<string, string> coverImages = MatchmakerAPI_Client.GetCoverImages();

            foreach(KeyValuePair<string, string> keyValue in coverImages) {
                images.Add(new Img(keyValue));
            }

            //Add the images to the list in the window
            ImageList.ItemsSource = images;
        }

        //Save button
        private async void btnSave_Click(object sender1, RoutedEventArgs e) {
            object image = ImageList.SelectedItem;
            if (image == null) {
                //If no image is selected, show a message
                MessageBox.Show("Please select a cover image!", "");
            } else {
                //Set the selected image as the user's cover image
                MatchMakerClassLibrary.CoverImageData coverImageData = new MatchMakerClassLibrary.CoverImageData();
                coverImageData.userID = userID;
                coverImageData.imageName = (image as Img).Src.Key;

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                if (await MatchmakerAPI_Client.PostNewCoverImageDataAsync(coverImageData)) {
                    MessageBox.Show("Image saved!", "");
                }
            }
        }

        //Exit button, closes the window
        private void btnExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }

    public class Img {
        public KeyValuePair<string, string> Src { get; set; }
        public Img (KeyValuePair<string, string> keyValue) {
            Src = keyValue;
        }
    }
}
