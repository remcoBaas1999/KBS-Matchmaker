﻿using MatchMakerClassLibrary;
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

namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        //Creates a page with a chatpartner and own data.
        public ChatPage(UserData chatPartner,UserData ownData)
        {
            InitializeComponent();
            ChatPartnerName.Text = chatPartner.realName;
            //ChatPartnerPicture = "";
        }
    }
}
