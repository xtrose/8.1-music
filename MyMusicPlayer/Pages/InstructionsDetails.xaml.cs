using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;





// Namespace
namespace MyMusicPlayer.Pages
{





    // Instructions Details
    public partial class InstructionsDetails : PhoneApplicationPage
    {





        // Wird beim ersten Start der Seite ausgeführt
        public InstructionsDetails()
        {
            InitializeComponent();
        }





        //Wird bei jedem Start der Seite ausgeführt
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Herausfinden was geladen werden soll
            string ToDo = NavigationContext.QueryString["ToDo"];
            base.OnNavigatedTo(e);
            
            // Wenn Select and Play geladen wird
            if (ToDo == "SelectAndPlay")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ005_SelectAndPlayHeader;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_SelectAndPlay;
            }

            // Wenn Refresh List
            if (ToDo == "RefreshList")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ002_InstRefresh;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_HelpRefresh;
            }

            // Wenn Search
            if (ToDo == "Search")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ002_InstSearch;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_HelpSearch;
            }

            // Wenn Orientierungs-Sperre
            if (ToDo == "OL")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ002_InstLock;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_HelpOrientationLock;
            }

            // Playliste speichern
            if (ToDo == "SP")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ005_SavePlaylist;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_SavePlaylistText;
            }

            // General
            if (ToDo == "General")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.Z001_InstGeneral;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.Z001_InstGeneralTxt;
            }

            // Listen-Ansicht
            if (ToDo == "ListView")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.Z001_InstListView;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.Z001_InstListViewTxt;
            }

            // Sonstiges
            if (ToDo == "Mis")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.Z001_InstMiscellaneous;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.Z001_InstMiscellaneousTxt;
            }

            // Design Editor
            if (ToDo == "DesignEditor")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ005_InstDesignEditor;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_InstDesignEditorTxt;
            }

            // Design Editor
            if (ToDo == "DesignError")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ005_InstDesignError;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_InstDesignErrorTxt;
            }

            // Wiedergabeliste erstellen
            if (ToDo == "CreatePlaylist")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ005_HelpCreatePlaylist;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_HelpCreatePlaylistTxt;
            }

            // Cache Sysem
            if (ToDo == "CacheSystem")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ005_CacheSystem;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_CacheSystemTxt;
            }

            // DisplayProblems
            if (ToDo == "DisplayProblems")
            {
                InstHeader.Text = MyMusicPlayer.Resources.AppResources.ZZ005_DisplayProblems;
                InstDetail.Text = MyMusicPlayer.Resources.AppResources.ZZ005_DisplayProblemsTxt;
            }
        }
    }
}