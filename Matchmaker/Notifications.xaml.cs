using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
namespace Matchmaker {

    public partial class Notifications : Page {
        public Notifications() {
            InitializeComponent();
        }

        private void goBack_MouseDown(object sender, MouseButtonEventArgs e) {
            //Go back to homepage
            NavigationService.GoBack();
            
        }

        private void RefreshNotificationsButton_MouseDown(object sender, MouseButtonEventArgs e) {
            for (int i = 0; i < 360; i+=15) {
                RotateTransform rotateTransform = new RotateTransform(i);
                RefreshNotificationsButton.RenderTransform = rotateTransform;
                Thread.Sleep(25);
                System.Windows.Forms.Application.DoEvents();
            }
            
        }
    }
}
