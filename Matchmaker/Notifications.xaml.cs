﻿using System;
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

namespace Matchmaker {

    public partial class Notifications : Page {
        public Notifications() {
            InitializeComponent();
        }

        private void goBack_MouseDown(object sender, MouseButtonEventArgs e) {
            //Go back to homepage
            NavigationService.GoBack();
            
        }
    }
}