using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;





namespace MyMusicPlayer.Pages
{





    public partial class Support : PhoneApplicationPage
    {





        //Wird am Anfang der Seite geladen
        //---------------------------------------------------------------------------------------------------------
        public Support()
        {
            //Komponenten laden
            InitializeComponent();

            //Animation vorbereiten
            Color backgroundColor = (Color)Application.Current.Resources["PhoneBackgroundColor"];
            string temp = Convert.ToString(backgroundColor);
            if (temp != "#FF000000")
            {
                //Vordergrundfarbe ändern
                string ForegroundColor = "#FF000000";
                (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;

                //Bilder ändern
                Logo2.Source = new BitmapImage(new Uri("/Images/StartUp/Logo800_2.png", UriKind.Relative));
                ImgTop.Source = new BitmapImage(new Uri("/Images/Support.Light.png", UriKind.Relative));
                ImgTop.Opacity = 0.1;
                ImgSupport.Source = new BitmapImage(new Uri("/Images/Support.Light.png", UriKind.Relative));
            }
            else
            {
                //Vordergrundfarbe ändern
                string ForegroundColor = "#FFFFFFFF";
                (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
            }
        }
        //---------------------------------------------------------------------------------------------------------





        //Button Support
        //---------------------------------------------------------------------------------------------------------
        private void LinkXtrose(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var wb = new WebBrowserTask();
            wb.URL = "http://www.xtrose.com";
            wb.Show();
        }
        //---------------------------------------------------------------------------------------------------------





        //Button Support
        //---------------------------------------------------------------------------------------------------------
        private void BtnSupport(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EmailComposeTask emailcomposer = new EmailComposeTask();
            emailcomposer.To = "xtrose@hotmail.com";
            emailcomposer.Subject = "8.1 Music Support";
            emailcomposer.Body = "";
            emailcomposer.Show();
        }
        //---------------------------------------------------------------------------------------------------------





        //Converter
        //---------------------------------------------------------------------------------------------------------------------------------
        //Farbe umwandeln
        SolidColorBrush ConvertToSolidColorBrush(string ARGB, int Alpha)
        {
            //Prüfen ob Alpha vorhanden
            byte A = Convert.ToByte(ARGB.Substring(1, 2), 16);
            if (Alpha > -1 & Alpha < 256)
            {
                A = Convert.ToByte(Alpha);
            }
            byte R = Convert.ToByte(ARGB.Substring(3, 2), 16);
            byte G = Convert.ToByte(ARGB.Substring(5, 2), 16);
            byte B = Convert.ToByte(ARGB.Substring(7, 2), 16);
            SolidColorBrush sb = new SolidColorBrush(Color.FromArgb(A, R, G, B));
            return sb;
        }
        //---------------------------------------------------------------------------------------------------------------------------------
    }
}