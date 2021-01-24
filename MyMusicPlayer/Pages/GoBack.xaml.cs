using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;





namespace MyMusicPlayer.Pages
{





    public partial class GoBack : PhoneApplicationPage
    {





        //Wird beim ersten Stert der Seite geladen
        //---------------------------------------------------------------------------------------------------------------------------------
        public GoBack()
        {
            InitializeComponent();
        }
        //---------------------------------------------------------------------------------------------------------------------------------





        //Wird bei jedem Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------------------------------
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationService.GoBack();
        }
        //---------------------------------------------------------------------------------------------------------------------------------





    }
}