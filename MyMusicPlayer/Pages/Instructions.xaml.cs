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





// Namespace
namespace MyMusicPlayer.Pages
{





    // Instructions
    public partial class Instructions : PhoneApplicationPage
    {





        // Wird beim ersten Start der Seite geladen
        public Instructions()
        {
            // Komponenten laden
            InitializeComponent();
        }



        // Detail Select and Play
        private void SelectAndPlayClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=SelectAndPlay", UriKind.Relative));
        }


        // Detail Refresh List
        private void RefreshListClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=RefreshList", UriKind.Relative));
        }


        // Suche
        private void SearchClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=Search", UriKind.Relative));
        }


        // Orientierungs Sperre
        private void OLClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=OL", UriKind.Relative));
        }


        // Wiedergabeliste speichern
        private void SPClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=SP", UriKind.Relative));
        }


        // General
        private void GeneralClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=General", UriKind.Relative));
        }

        // Listen-ansicht
        private void ListViewClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=ListView", UriKind.Relative));
        }

        // Sonstiges
        private void MisClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=Mis", UriKind.Relative));
        }

        // Design Editor
        private void DesignEditorClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=DesignEditor", UriKind.Relative));
        }

        // Design Error
        private void DesignErrorClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=DesignError", UriKind.Relative));
        }

        // Wiedergabeliste erstellen
        private void CreatePlaylistClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=CreatePlaylist", UriKind.Relative));
        }

        // Cache System
        private void CacheSystemClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=CacheSystem", UriKind.Relative));
        }

        // Display Problems
        private void DisplayProblemsClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InstructionsDetails.xaml?ToDo=DisplayProblems", UriKind.Relative));
        }

    }
}