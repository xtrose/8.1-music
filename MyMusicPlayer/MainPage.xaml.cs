using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyMusicPlayer.Resources;
using Microsoft.Xna.Framework.Media;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Microsoft.Devices;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Threading;
using System.IO;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;
using Windows.Graphics.Display;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Info;
using System.Globalization;
using System.Threading;
using ImageTools;
using Microsoft.Devices.Radio;
using System.Windows.Resources;
using Microsoft.Xna.Framework.Audio;





namespace MyMusicPlayer
{





    public partial class MainPage : PhoneApplicationPage
    {





        # region Start Animation
        //Start Animation
        //---------------------------------------------------------------------------------------------------------------------------------
        int Timer_StartUp_UhrzeitMS;
        int Timer_StartUp_StartMs;
        string Timer_StartUp_Animation = "PauseStart";
        void Timer_Timer_StartUp_Tick(object sender, object e)
        {
            //uhrzeitms neu erstellen
            DateTime Uhrzeit = DateTime.Now;
            //Aktuelle Uhrzeit Millisekunden erstellen
            Timer_StartUp_UhrzeitMS = (Uhrzeit.Hour * 3600000) + (Uhrzeit.Minute * 60000) + (Uhrzeit.Second * 1000) + Uhrzeit.Millisecond;

            //Animation Pause1
            if (Timer_StartUp_Animation == "PauseStart")
            {
                //Prüfen ob Animation schon gestartet
                if (Timer_StartUp_StartMs == 0)
                {
                    Timer_StartUp_StartMs = Timer_StartUp_UhrzeitMS;
                }
                //Wenn Animation schon gestartet
                else
                {
                    //Wenn Animation beendet
                    if ((Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) > 300)
                    {
                        //Nächste Animation Starten
                        Timer_StartUp_Animation = "Img1";
                        Timer_StartUp_StartMs = 0;
                    }
                }
            }

            //Animation Img1
            if (Timer_StartUp_Animation == "Img1")
            {
                //Prüfen ob Animation schon gestartet
                if (Timer_StartUp_StartMs == 0)
                {
                    Timer_StartUp_StartMs = Timer_StartUp_UhrzeitMS;
                }
                //Wenn Animation schon gestartet
                else
                {
                    //Wenn Animation beendet
                    if ((Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) > 400)
                    {
                        //Bild endgültig ausrichten
                        Img1.Width = 221;
                        Img1.Opacity = 1.0;
                        //Nächste Animation Starten
                        Timer_StartUp_Animation = "Pause1";
                        Timer_StartUp_StartMs = 0;
                    }
                    //Wenn Animation noch läuft
                    else
                    {
                        //Prozent errechnen
                        int Prozent = 100 * 100000 / 400 * (Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) / 100000;
                        //Bild Größe berechnen
                        int NewSize = 1800 * 100000 / 100 * Prozent / 100000;
                        //Bild Transparenz berechnen
                        double opa = 0.1;
                        //string temp = Convert.ToString(Prozent);
                        //opa = Convert.ToDouble("0," + temp);
                        if (Prozent >= 100)
                        {
                            opa = 1.0;
                        }
                        else if (Prozent >= 90)
                        {
                            opa = 0.9;
                        }
                        else if (Prozent >= 80)
                        {
                            opa = 0.8;
                        }
                        else if (Prozent >= 70)
                        {
                            opa = 0.7;
                        }
                        else if (Prozent >= 60)
                        {
                            opa = 0.6;
                        }
                        else if (Prozent >= 50)
                        {
                            opa = 0.5;
                        }
                        else if (Prozent >= 40)
                        {
                            opa = 0.4;
                        }
                        else if (Prozent >= 30)
                        {
                            opa = 0.3;
                        }
                        else if (Prozent >= 20)
                        {
                            opa = 0.2;
                        }
                        else if (Prozent >= 10)
                        {
                            opa = 0.1;
                        }
                        //Bild neu erstellen
                        Img1.Width = 2021 - NewSize;
                        Img1.Opacity = opa;
                    }
                }
            }

            //Animation Pause1
            if (Timer_StartUp_Animation == "Pause1")
            {
                //Prüfen ob Animation schon gestartet
                if (Timer_StartUp_StartMs == 0)
                {
                    Timer_StartUp_StartMs = Timer_StartUp_UhrzeitMS;
                }
                //Wenn Animation schon gestartet
                else
                {
                    //Wenn Animation beendet
                    if ((Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) > 200)
                    {
                        //Nächste Animation Starten
                        Timer_StartUp_Animation = "Schein";
                        Timer_StartUp_StartMs = 0;
                    }
                }
            }

            //Animation Img1
            if (Timer_StartUp_Animation == "Schein")
            {
                //Prüfen ob Animation schon gestartet
                if (Timer_StartUp_StartMs == 0)
                {
                    //Bilder sichtbar machen
                    ImgSchein.Visibility = System.Windows.Visibility.Visible;
                    Img2.Visibility = System.Windows.Visibility.Visible;
                    //Zeit neu erstellen
                    Timer_StartUp_StartMs = Timer_StartUp_UhrzeitMS;
                }
                //Wenn Animation schon gestartet
                else
                {
                    //Wenn Animation beendet
                    if ((Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) > 400)
                    {
                        //Bild unsichtbar machen
                        ImgSchein.Visibility = System.Windows.Visibility.Collapsed;
                        //Animation umstellen
                        Timer_StartUp_Animation = "Pause2";
                    }
                    //Wenn Animation noch läuft
                    else
                    {
                        //Prozent errechnen
                        int Prozent = 100 * 100000 / 400 * (Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) / 100000;
                        //Margin neu berechnen
                        int NewMargin = 2000 * 100000 / 100 * Prozent / 100000;
                        ImgSchein.Margin = new Thickness((NewMargin - 150), 159, 0, 159);
                    }
                }
            }

            //Animation Pause2
            if (Timer_StartUp_Animation == "Pause2")
            {
                //Prüfen ob Animation schon gestartet
                if (Timer_StartUp_StartMs == 0)
                {
                    Timer_StartUp_StartMs = Timer_StartUp_UhrzeitMS;
                }
                //Wenn Animation schon gestartet
                else
                {
                    //Wenn Animation beendet
                    if ((Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) > 800)
                    {
                        //Nächste Animation Starten
                        Timer_StartUp_Animation = "Ausblenden";
                        Timer_StartUp_StartMs = 0;
                    }
                }
            }

            //Animation Img1
            if (Timer_StartUp_Animation == "Ausblenden")
            {
                //Prüfen ob Animation schon gestartet
                if (Timer_StartUp_StartMs == 0)
                {
                    //Bilder sichtbar machen
                    ImgSchein.Visibility = System.Windows.Visibility.Visible;
                    Img2.Visibility = System.Windows.Visibility.Visible;
                    //Zeit neu erstellen
                    Timer_StartUp_StartMs = Timer_StartUp_UhrzeitMS;
                }
                //Wenn Animation schon gestartet
                else
                {
                    //Wenn Animation beendet
                    if ((Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) > 400)
                    {
                        //Bilder entfernen
                        Img1.Visibility = System.Windows.Visibility.Collapsed;
                        Img2.Visibility = System.Windows.Visibility.Collapsed;
                        //Animation umstellen
                        Timer_StartUp_Animation = "PauseEnde";
                    }
                    //Wenn Animation noch läuft
                    else
                    {
                        //Prozent errechnen
                        int Prozent = 100 * 100000 / 400 * (Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) / 100000;
                        Prozent = 100 - Prozent;
                        //Bild Transparenz berechnen
                        double opa = 0.0;
                        if (Prozent >= 100)
                        {
                            opa = 1.0;
                        }
                        else if (Prozent >= 90)
                        {
                            opa = 0.9;
                        }
                        else if (Prozent >= 80)
                        {
                            opa = 0.8;
                        }
                        else if (Prozent >= 70)
                        {
                            opa = 0.7;
                        }
                        else if (Prozent >= 60)
                        {
                            opa = 0.6;
                        }
                        else if (Prozent >= 50)
                        {
                            opa = 0.5;
                        }
                        else if (Prozent >= 40)
                        {
                            opa = 0.4;
                        }
                        else if (Prozent >= 30)
                        {
                            opa = 0.3;
                        }
                        else if (Prozent >= 20)
                        {
                            opa = 0.2;
                        }
                        else if (Prozent >= 10)
                        {
                            opa = 0.1;
                        }
                        //Transparenz auf Bilder anwenden
                        Img1.Opacity = opa;
                        Img2.Opacity = opa;
                    }
                }
            }

            //Animation PauseEnde
            if (Timer_StartUp_Animation == "PauseEnde")
            {
                //Prüfen ob Animation schon gestartet
                if (Timer_StartUp_StartMs == 0)
                {
                    Timer_StartUp_StartMs = Timer_StartUp_UhrzeitMS;
                }
                //Wenn Animation schon gestartet
                else
                {
                    //Wenn Animation beendet
                    if ((Timer_StartUp_UhrzeitMS - Timer_StartUp_StartMs) > 300)
                    {
                        //Seite wechseln
                        Timer_StartUp.Stop();
                        GRAnimation.Children.Clear();
                        GRAnimation.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }


        //Animation abbrechen
        private void AnimationStop(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Seite wechseln
            Timer_StartUp.Stop();
            GRAnimation.Children.Clear();
            GRAnimation.Visibility = System.Windows.Visibility.Collapsed;
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        # region Allgemeine Variabeln
        //Allgemeine Variabeln
        //---------------------------------------------------------------------------------------------------------
        //Datenliste Songs
        ObservableCollection<ClassMedia> ListSongs = new ObservableCollection<ClassMedia>();
        ObservableCollection<ClassMedia> TempListSongs = new ObservableCollection<ClassMedia>();
        ObservableCollection<ClassMedia> TempListArtistSongs = new ObservableCollection<ClassMedia>();
        ObservableCollection<ClassMedia> ListPlaylist = new ObservableCollection<ClassMedia>();
        ObservableCollection<ClassMedia> TempListPlaylist = new ObservableCollection<ClassMedia>();

        // Liste Aktive Abspielliste
        ObservableCollection<ClassActivePlayList> ListActivPlaylist = new ObservableCollection<ClassActivePlayList>();

        //Catch Strings erstellen
        string CatchArtists = "";
        string CatchPlaylists = "";
        string CatchGenres = "";
        string CatchAlbums = "";
        string CatchAllSongs = "";

        //IsoStore file erstellen
        IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
        //Filestream erstellen
        IsolatedStorageFileStream filestream;
        //Streamreade erstellen
        StreamReader sr;
        //StreamWriter erstellen 
        StreamWriter sw;

        //Trial Zeit
        DateTime DTTrial = new DateTime();
        //Gibt an ob Trial Benachrichtigung bereits ausgegeben wurde
        bool TrialMSG = false;
        //Ob App ausgeführt wir
        bool AppIsRunning = true;

        //Timer Start Animation
        DispatcherTimer Timer_StartUp = new DispatcherTimer();
        //Timer MediaPlayer
        DispatcherTimer Timer_MediaPlayer = new DispatcherTimer();
        //Timer Settings
        DispatcherTimer Timer_Settings = new DispatcherTimer();
        //Timer ListSelector
        DispatcherTimer Timer_ListSelector = new DispatcherTimer();

        //MediaLibrary auflistung aller Songs
        MediaLibrary mediaLibrary = new MediaLibrary();

        //Ob Listen schon erstellt wurden
        bool CreateFirst = false;

        //MenuOpen
        bool MenuOpen = false;

        //String Einstellungen
        string SettingsString = "";

        //String Design Einstellungen
        string DesignString = "";

        //String Last Playback
        string LastPlaybackString = "";
        //Zum Prüfen wieviele Last Playbacks geöffnet wurden
        int LastPlaybacksCount = 0;

        //Vollversion
        bool Fullversion = false;
        //Logo beim Start
        bool LogoStart = true;
        //Erweiterte Informationen
        string ExtendedInformation = "true";
        //Zeit des anspielens
        int PlayOnTime = 7;
        //Shuffle
        bool SetShuffle = false;
        //Repead
        bool SetRepead = false;
        //Schrift Größe
        public static string ImageSize = "28";
        //Run under Lock screen
        bool RunUnderLockScreen = false;
        //Select and Play
        bool SelectAndPlay = false;
        int PlayID = -1;
        string PlaySong;
        string PlayAlbum;
        string PlayArtist;
        string PlayControl = "None";
        //Show Info
        bool ShowInfos = true;
        bool ShowInfosStart = false;

        //Farben der Auswahl erstellen
        string AppAccentColor;
        string AppBackgroundColor;
        string AppForegroundColor;
        string ListSelectorColor;
        string MediaPlayerAccentColor;
        string MediaPlayerBackgroundColor;
        string MediaPlayerBigBackgroundColor;
        string MediaPlayerBigAccentColor;
        string ArtistBackgroundColor;
        string ArtistForegroundColor;
        string AlbumBackgroundColor;
        string AlbumForegroundColor;
        string SongForegroundColor;
        string SongBackgroundColor;
        string SelectedBackgroundColor;
        string SelectedForegroundColor;
        bool BackgroundPortrait = false;
        bool BackgroundLandscape = false;
        bool MPBigBackgroundPortrait = false;
        bool MPBigBackgroundLandscape = false;
        string BackgroundColor;
        string ForegroundColor;

        //Bilder für die Auswahl erstellen
        string PlayImageBlack = "/Images/Play.Light.png";
        string PlayImageWhite = "/Images/Play.Dark.png";
        string ArtistPlayImage = "/Images/Play.Dark.png";
        string AlbumPlayImage = "/Images/Play.Dark.png";
        string SongPlayImage = "/Images/Play.Dark.png";
        string SelectedPlayImage = "/Images/Play.Dark.png";

        //Momentane Orientierung
        bool OrientationPortait = true;

        //Bool ob schließen bereits gedrückt
        bool ClosePressed = false;

        //Bool ob Zurücksetzen bereits gedrückt wurde
        int RefreshPressed = 0;

        //Eingestellte Sprache
        string cul;

        //Index des aktuellen aktiven Pivot Item
        int PivotIndex = -1;

        //Aktuelle Suche
        string Search = "";

        //Version
        string Version = "";

        //Liste Updaten beim Start
        bool UpdateListsAtStart = false;

        //Tile Einstellungen
        string TileString = "BackgroundColor=AC;LogoImage=true;SecondBackgroundColor=AC;";

        //Tile Einstellungen
        string TileBackgroundColor;
        string SecondTileBackgroundColor;
        bool LogoImage = false;

        //Einstellungen nach Laden speichern
        bool SaveSettingAfterLoad = false;

        // Angabe das App das erste mal geladen wird
        bool FirstLoad = true;

        // String um nach Update Benachrichtigung auszugeben
        string Msg = "None";
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Wird beim ersten Start der Seite ausgeführt
        //Wird beim ersten Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------
        public MainPage()
        {            
            // Wenn aktueller Song geändert wird
            MediaPlayer.ActiveSongChanged += MPActivSongChanged;

            //Prüfen ob eine Sprachdatei besteht
            if (file.FileExists("Cul.dat"))
            {
                //Spachdatei laden
                filestream = file.OpenFile("Cul.dat", FileMode.Open);
                sr = new StreamReader(filestream);
                cul = sr.ReadToEnd();
                cul = cul.TrimEnd(new char[] { '\r', '\n' });
                filestream.Close();
                //Sprache einstellen
                CultureInfo newCulture = new CultureInfo(cul);
                Thread.CurrentThread.CurrentUICulture = newCulture;
            }



            //Komponenten laden
            InitializeComponent();

            

            //Hintergrundfarbe ermitteln
            Color backgroundColor = (Color)Application.Current.Resources["PhoneBackgroundColor"];
            AppBackgroundColor = Convert.ToString(backgroundColor);
            //Animation an Hintergrundfarbe anpassen
            if (AppBackgroundColor == "#FF000000")
            {
                ImgSchein.Source = new BitmapImage(new Uri("Images/StartUp/Schein_black.png", UriKind.Relative));
                Img2.Source = new BitmapImage(new Uri("Images/StartUp/Logo800_2_black.png", UriKind.Relative));
                StartUpInfo.Foreground = new SolidColorBrush(Colors.White);
                Greetings.Foreground = new SolidColorBrush(Colors.White);
            }

            //Animation sichtbar machen
            GRAnimation.Visibility = System.Windows.Visibility.Visible;
            GRAnimation.Background = ConvertToSolidColorBrush(AppBackgroundColor,-1);

            //Timer Start Animation einstellen
            Timer_StartUp.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer_StartUp.Tick += Timer_Timer_StartUp_Tick;
            Timer_StartUp.Start();



            //Timer MediaPlayer einstellen
            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            Timer_MediaPlayer.Tick += Timer_MediaPlayer_Tick;
            Timer_MediaPlayer.Start();



            //Timer Settings einstellen
            Timer_Settings.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer_Settings.Tick += Timer_Settings_Tick;
            Timer_Settings.Stop();



            //Timer ListSelector einstellen
            Timer_ListSelector.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer_ListSelector.Tick += Timer_ListSelector_Tick;
            Timer_ListSelector.Stop();



            //Seite an Hintergrundfarbe anpassen
            //Wenn Hintergundfarbe schwarz ist
            if (AppBackgroundColor == "#FF000000")
            {
                BackgroundColor = "#FF000000";
                ForegroundColor = "#FFFFFFFF";
                AppForegroundColor = "#FFFFFFFF";
                MediaPlayerBackgroundColor = "#FF1F1F1F";
                MediaPlayerBigBackgroundColor = "#FF000000";
            }
            //Wenn Hintergrundfarbe weiß ist
            else
            {
                BackgroundColor = "#FFFFFFFF";
                ForegroundColor = "#FF000000";
                AppForegroundColor = "#FF000000";
                MediaPlayerBackgroundColor = "#FFDDDDDD";
                MediaPlayerBigBackgroundColor = "#FFFFFFFF";

                //ListSelector Bauteile ändern
                ListSelectorChangeColor();
            }



            //MediaPlayer Bilder nach aktuellen Einstellungen ändern
            try
            {
                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                if (MediaPlayer.IsShuffled.ToString() == "False")
                {
                    MPShuffle.Opacity = 0.5;
                }
                else
                {
                    MPShuffle.Opacity = 1.0;
                }
            }
            catch
            {
            }

            try
            {
                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                if (MediaPlayer.IsRepeating.ToString() == "False")
                {
                    MPRepead.Opacity = 0.5;
                }
                else
                {
                    MPRepead.Opacity = 1.0;
                }
            }
            catch
            {
            }

            MPTBRepead.Opacity = 0.0;



            //Akzentfarbe ermitteln
            AppAccentColor = ((Color)Application.Current.Resources["PhoneAccentColor"]).ToString();

            //Farben einstellen
            MediaPlayerAccentColor = AppAccentColor;
            MediaPlayerBigAccentColor = AppAccentColor;
            ListSelectorColor = AppAccentColor;
            ArtistBackgroundColor = "#FF2d2d2d";
            ArtistForegroundColor = "#FFFFFFFF";
            AlbumBackgroundColor = "#FF5b5b5b";
            AlbumForegroundColor = "#FFFFFFFF";
            SongBackgroundColor = "#FF888888";
            SongForegroundColor = "#FFFFFFFF";
            SelectedBackgroundColor = AppAccentColor;
            SelectedForegroundColor = "#FFFFFFFF";
        }
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Wird bei jedem Start der Seite ausgeführt
        //Wird bei jedem Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------------------------------
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Wenn Einstellungen noch nicht vorhanden
            if (!file.DirectoryExists("Settings"))
            {
                //Ordner Settings erstellen
                file.CreateDirectory("Settings");
                //Settings erstellen
                CreateSettings();
                //Settings/Version erstellen
                filestream = file.CreateFile("Settings/Version.dat");
                sw = new StreamWriter(filestream);
                sw.Write("0001000000450000");
                sw.Flush();
                filestream.Close();

                //Ordner LastPlayback erstellen
                file.CreateDirectory("LastPlayback");
                //LastPlayback/LastPlayback.dat erstellen
                filestream = file.CreateFile("LastPlayback/LastPlayback.dat");
                sw = new StreamWriter(filestream);
                sw.Write("");
                sw.Flush();
                filestream.Close();

                //Rate Reminder erstellen
                DateTime datetime = DateTime.Now;
                datetime = datetime.AddDays(4);
                filestream = file.CreateFile("Settings/RateReminder.txt");
                sw = new StreamWriter(filestream);
                sw.WriteLine(datetime.ToString());
                sw.Flush();
                filestream.Close();
            }



            //Version laden
            filestream = file.OpenFile("Settings/Version.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            Version = sr.ReadToEnd();
            filestream.Close();



            //Auf Version 1.3.46.0 Updaten
            //********************************************************************************************************************************************
            if (Version == "0001000000450000")
            {
                //Settings Design erstellen
                filestream = file.CreateFile("Settings/Design.dat");
                sw = new StreamWriter(filestream);
                sw.Write("");
                sw.Flush();
                filestream.Close();

                //Version updaten
                Version = "0001000300460000";
                //Settings/Version erstellen
                filestream = file.CreateFile("Settings/Version.dat");
                sw = new StreamWriter(filestream);
                sw.Write(Version);
                sw.Flush();
                filestream.Close();

                //Background Ordner erstellen
                if (!file.DirectoryExists("/Background"))
                {
                    file.CreateDirectory("/Background");
                }

                //Wenn Design Datei noch nicht exestiert
                if (!file.DirectoryExists("/Designs"))
                {
                    //Designs Ordner erstellen
                    file.CreateDirectory("/Designs");
                    //Design Konzert erstellen
                    file.CreateDirectory("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_Concert);
                    //SystemStyle
                    filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_Concert + "/SystemDesign.dat");
                    sw = new StreamWriter(filestream);
                    sw.Write("");
                    sw.Flush();
                    filestream.Close();
                    //Daten in Storage laden
                    using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Design.dat", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_Concert + "/Design.dat"))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                filestream.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                    //Daten in Storage laden
                    using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Landscape.jpg", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_Concert + "/Landscape.jpg"))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                filestream.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                    //Daten in Storage laden
                    using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Portrait.jpg", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_Concert + "/Portrait.jpg"))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                filestream.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                    //Daten in Storage laden
                    using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/MPBigLandscape.jpg", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_Concert + "/MPBigLandscape.jpg"))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                filestream.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                    //Daten in Storage laden
                    using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/MPBigPortrait.jpg", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_Concert + "/MPBigPortrait.jpg"))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                filestream.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                    //Design Weiß erstellen
                    file.CreateDirectory("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_StandardWhite);
                    //SystemStyle
                    filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_StandardWhite + "/SystemDesign.dat");
                    sw = new StreamWriter(filestream);
                    sw.Write("");
                    sw.Flush();
                    filestream.Close();
                    //Daten in Storage laden
                    using (Stream input = Application.GetResourceStream(new Uri("Designs/White/Design.dat", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_StandardWhite + "/Design.dat"))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                filestream.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                    //Design schwarz erstellen
                    file.CreateDirectory("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_StandardBlack);
                    //SystemStyle
                    filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_StandardBlack + "/SystemDesign.dat");
                    sw = new StreamWriter(filestream);
                    sw.Write("");
                    sw.Flush();
                    filestream.Close();
                    //Daten in Storage laden
                    using (Stream input = Application.GetResourceStream(new Uri("Designs/Black/Design.dat", UriKind.Relative)).Stream)
                    {
                        // Create a stream for the new file in the local folder.
                        using (filestream = file.CreateFile("/Designs/" + MyMusicPlayer.Resources.AppResources.ZZ002_StandardBlack + "/Design.dat"))
                        {
                            // Initialize the buffer.
                            byte[] readBuffer = new byte[4096];
                            int bytesRead = -1;

                            // Copy the file from the installation folder to the local folder. 
                            while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                            {
                                filestream.Write(readBuffer, 0, bytesRead);
                            }
                        }
                    }
                }



                //Neues Design erstellen
                //Daten in Storage laden
                using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Design.dat", UriKind.Relative)).Stream)
                {
                    // Create a stream for the new file in the local folder.
                    using (filestream = file.CreateFile("/Settings/Design.dat"))
                    {
                        // Initialize the buffer.
                        byte[] readBuffer = new byte[4096];
                        int bytesRead = -1;

                        // Copy the file from the installation folder to the local folder. 
                        while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                        {
                            filestream.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }
                //Daten in Storage laden
                using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Landscape.jpg", UriKind.Relative)).Stream)
                {
                    // Create a stream for the new file in the local folder.
                    using (filestream = file.CreateFile("/Background/Landscape.jpg"))
                    {
                        // Initialize the buffer.
                        byte[] readBuffer = new byte[4096];
                        int bytesRead = -1;

                        // Copy the file from the installation folder to the local folder. 
                        while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                        {
                            filestream.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }
                //Daten in Storage laden
                using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Portrait.jpg", UriKind.Relative)).Stream)
                {
                    // Create a stream for the new file in the local folder.
                    using (filestream = file.CreateFile("/Background/Portrait.jpg"))
                    {
                        // Initialize the buffer.
                        byte[] readBuffer = new byte[4096];
                        int bytesRead = -1;

                        // Copy the file from the installation folder to the local folder. 
                        while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                        {
                            filestream.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }
                //Daten in Storage laden
                using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/MPBigLandscape.jpg", UriKind.Relative)).Stream)
                {
                    // Create a stream for the new file in the local folder.
                    using (filestream = file.CreateFile("/Background/MPBigLandscape.jpg"))
                    {
                        // Initialize the buffer.
                        byte[] readBuffer = new byte[4096];
                        int bytesRead = -1;

                        // Copy the file from the installation folder to the local folder. 
                        while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                        {
                            filestream.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }
                //Daten in Storage laden
                using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/MPBigPortrait.jpg", UriKind.Relative)).Stream)
                {
                    // Create a stream for the new file in the local folder.
                    using (filestream = file.CreateFile("/Background/MPBigPortrait.jpg"))
                    {
                        // Initialize the buffer.
                        byte[] readBuffer = new byte[4096];
                        int bytesRead = -1;

                        // Copy the file from the installation folder to the local folder. 
                        while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                        {
                            filestream.Write(readBuffer, 0, bytesRead);
                        }
                    }
                }



                //Settings laden
                filestream = file.OpenFile("Settings/Settings.dat", FileMode.Open);
                sr = new StreamReader(filestream);
                SettingsString = sr.ReadToEnd();
                filestream.Close();

                //SettingsString zerlegen
                string[] SplitSettingStringUpdate = Regex.Split(SettingsString, ";");
                //Settings durchlaufen und Einstellungen umsetzen
                for (int i = 0; i < (SplitSettingStringUpdate.Count() - 1); i++)
                {
                    //Einstellung zerlegen und Prüfen
                    string[] SplitSetting = Regex.Split(SplitSettingStringUpdate[i], "=");

                    //Vollversion
                    if (SplitSetting[0] == "Fullversion")
                    {
                        Fullversion = Convert.ToBoolean(SplitSetting[1].Trim());
                    }
                }

                //Einstellungen neu erstellen
                CreateSettings();
            }
            //Auf Version 1.3.46.0 Updaten
            //********************************************************************************************************************************************



            //Auf Version 1.4.46.0 Updaten
            //********************************************************************************************************************************************
            if (Version == "0001000300460000")
            {
                //Neues Catch System anlegen
                file.CreateDirectory("/Catch");
                
                //Neuen Tile Ordner anlegen
                file.CreateDirectory("/Tiles");

                //Tile Einstellungen erstellen
                filestream = file.CreateFile("/Tiles/TileSettings.dat");
                sw = new StreamWriter(filestream);
                sw.Write(TileString);
                sw.Flush();
                filestream.Close();

                //Version updaten
                Version = "0001000400460000";
                //Settings/Version erstellen
                filestream = file.CreateFile("/Settings/Version.dat");
                sw = new StreamWriter(filestream);
                sw.Write(Version);
                sw.Flush();
                filestream.Close();

                //Einstellungen speichern nach abruf
                SaveSettingAfterLoad = true;
            }
            //********************************************************************************************************************************************



            //Auf Version 2.0.46.0 Updaten
            //********************************************************************************************************************************************
            if (Version == "0001000400460000")
            {
                //Neues Playlists System anlegen
                file.CreateDirectory("/Playlists");

                //Version updaten
                Version = "0002000000460000";
                //Settings/Version erstellen
                filestream = file.CreateFile("/Settings/Version.dat");
                sw = new StreamWriter(filestream);
                sw.Write(Version);
                sw.Flush();
                filestream.Close();

                //Einstellungen speichern nach abruf
                SaveSettingAfterLoad = true;

                // Benachrichtigung nach update ausgeben
                Msg = "SelectAndPlayBeta";
            }
            //********************************************************************************************************************************************



            //Settings laden
            filestream = file.OpenFile("Settings/Settings.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            SettingsString = sr.ReadToEnd();
            filestream.Close();



            //Design laden
            filestream = file.OpenFile("Settings/Design.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            DesignString = sr.ReadToEnd();
            filestream.Close();



            //Kompletten String erstellen
            string AllSettings = SettingsString + DesignString;



            //SettingsString zerlegen
            string[] SplitSettingString = Regex.Split(AllSettings, ";");
            //Settings durchlaufen und Einstellungen umsetzen
            for (int i = 0; i < (SplitSettingString.Count() - 1); i++)
            {
                //Einstellung zerlegen und Prüfen
                string[] SplitSetting = Regex.Split(SplitSettingString[i], "=");

                //Vollversion
                if (SplitSetting[0] == "Fullversion")
                {
                    Fullversion = Convert.ToBoolean(SplitSetting[1].Trim());
                }

                //PlayOnTime
                if (SplitSetting[0] == "PlayOnTime")
                {
                    PlayOnTime = Convert.ToInt32(SplitSetting[1].Trim());
                }

                //LogoStart
                if (SplitSetting[0] == "LogoStart")
                {
                    LogoStart = Convert.ToBoolean(SplitSetting[1].Trim());
                    if (LogoStart == false)
                    {
                        Timer_StartUp.Stop();
                        GRAnimation.Children.Clear();
                        GRAnimation.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }

                //Erweiterte Informationen
                if (SplitSetting[0] == "ExtendedInformation")
                {
                    bool tExtendedInformation = Convert.ToBoolean(SplitSetting[1]);
                    if (tExtendedInformation == true)
                    {
                        ExtendedInformation = "true";
                    }
                    else
                    {
                        ExtendedInformation = "false";
                    }
                }

                //Select and play
                if (SplitSetting[0] == "SelectAndPlay")
                {
                    // Select and play erstellen
                    SelectAndPlay = Convert.ToBoolean(SplitSetting[1].Trim());
                }

                //Shuffle
                if (SplitSetting[0] == "Shuffle")
                {
                    //Shuffle umwandeln
                    SetShuffle = Convert.ToBoolean(SplitSetting[1]);
                    //Wenn Shuffle aktiviert
                    if (SetShuffle == true)
                    {
                        // Bilder auf aktiv setzen
                        MPShuffle.Opacity = 1.0;
                        FPShuffle.Opacity = 1.0;
                        // Wenn Select and Play aktiv ist
                        if (SelectAndPlay == true)
                        {
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.IsShuffled = false;
                            }
                            catch
                            {
                            }
                        }
                        // Wenn Select and Play nicht aktiv
                        else
                        {
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.IsShuffled = true;
                            }
                            catch
                            {
                            }
                        }
                    }
                    //Wenn Shuffle nicht aktiviert
                    else
                    {
                        // Bilder auf nicht aktiv setzen
                        MPShuffle.Opacity = 0.5;
                        FPShuffle.Opacity = 0.5;
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.IsShuffled = false;
                        }
                        catch
                        {
                        }
                    }
                }

                //Repead
                if (SplitSetting[0] == "Repead")
                {
                    //Repead umwandeln
                    SetRepead = Convert.ToBoolean(SplitSetting[1]);
                    //Wenn Repead aktiviert
                    if (SetRepead == true)
                    {
                        // Bilder auf aktiv setzen
                        MPRepead.Opacity = 1.0;
                        FPRepead.Opacity = 1.0;
                        // Wenn Select and Play aktiv ist
                        if (SelectAndPlay == true)
                        {
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.IsRepeating = false;
                            }
                            catch
                            {
                            }
                        }
                        // Wenn Select ans Play nicht aktiv ist
                        else
                        {
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.IsRepeating = true;
                            }
                            catch
                            {
                            }
                        }
                    }
                    //Wenn Repeade nicht aktiviert
                    else
                    {
                        // Bilder auf nicht aktiv setzen
                        MPRepead.Opacity = 0.5;
                        FPRepead.Opacity = 0.5;
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.IsRepeating = false;
                        }
                        catch
                        {
                        }
                    }
                }

                //Run under lock screen
                if (SplitSetting[0] == "RunUnderLockScreen")
                {
                    RunUnderLockScreen = Convert.ToBoolean(SplitSetting[1]);
                    if (RunUnderLockScreen == true)
                    {
                        //BtnRunUnderLockScreen.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                        try
                        {
                            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        //BtnRunUnderLockScreen.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                        try
                        {
                            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled;
                        }
                        catch
                        {
                        }
                    }
                }

                //Farbe App Hintergrund
                if (SplitSetting[0] == "BackgroundColor")
                {
                    //Farbe erstellen
                    BackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    (App.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(BackgroundColor, -1).Color;
                    LayoutRoot.Background = ConvertToSolidColorBrush(BackgroundColor, -1);
                }

                //Farbe App Vordergrund
                if (SplitSetting[0] == "ForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        ForegroundColor = "#FF000000";
                        (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        ForegroundColor = "#FFFFFFFF";
                        (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                    }
                }

                //Farbe MediaPlayer klein Akzent
                if (SplitSetting[0] == "MediaPlayerAccentColor")
                {
                    //Farbe erstellen
                    MediaPlayerAccentColor = CreateColorFromCode(SplitSetting[1]);
                    MPStatEmpty.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
                    MPStatBarSmall.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
                }

                //Farbe MediaPlayer klein Hintergrund
                if (SplitSetting[0] == "MediaPlayerBackgroundColor")
                {
                    //Farbe erstellen
                    MediaPlayerBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    GRMediaPlayer.Background = ConvertToSolidColorBrush(MediaPlayerBackgroundColor, -1);
                    GRNote.Background = ConvertToSolidColorBrush(MediaPlayerBackgroundColor, 255);
                }

                //Farbe MediaPlayer groß Akzent
                if (SplitSetting[0] == "MediaPlayerBigAccentColor")
                {
                    //Farbe erstellen
                    MediaPlayerBigAccentColor = CreateColorFromCode(SplitSetting[1]);
                    FPStatEmpty.Fill = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, -1);
                }

                //Farbe MediaPlayer groß Hintergrund
                if (SplitSetting[0] == "MediaPlayerBigBackgroundColor")
                {
                    //Farbe erstellen
                    MediaPlayerBigBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    GRFullscreenPlayer.Background = ConvertToSolidColorBrush(MediaPlayerBigBackgroundColor, -1);
                }

                //Farbe List Selector
                if (SplitSetting[0] == "ListSelectorColor")
                {
                    //Farbe erstellen
                    ListSelectorColor = CreateColorFromCode(SplitSetting[1]);
                }

                //Schriftgröße
                if (SplitSetting[0] == "FontSize")
                {
                    ImageSize = SplitSetting[1];
                }

                //Farbe Artist Hintergrund
                if (SplitSetting[0] == "ArtistBackgroundColor")
                {
                    //Farbe erstellen
                    ArtistBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                }

                //Farbe Artist Vordergrund
                if (SplitSetting[0] == "ArtistForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        ArtistForegroundColor = "#FF000000";
                        ArtistPlayImage = PlayImageBlack;
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        ArtistForegroundColor = "#FFFFFFFF";
                        ArtistPlayImage = PlayImageWhite;
                    }
                }

                //Farbe Album Hintergrund
                if (SplitSetting[0] == "AlbumBackgroundColor")
                {
                    //Farbe erstellen
                    AlbumBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                }

                //Farbe Album Vordergrund
                if (SplitSetting[0] == "AlbumForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        AlbumForegroundColor = "#FF000000";
                        AlbumPlayImage = PlayImageBlack;
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        AlbumForegroundColor = "#FFFFFFFF";
                        AlbumPlayImage = PlayImageWhite;
                    }
                }

                //Farbe Song Hintergrund
                if (SplitSetting[0] == "SongBackgroundColor")
                {
                    //Farbe erstellen
                    SongBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                }

                //Farbe Song Vordergrund
                if (SplitSetting[0] == "SongForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        SongForegroundColor = "#FF000000";
                        SongPlayImage = PlayImageBlack;
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        SongForegroundColor = "#FFFFFFFF";
                        SongPlayImage = PlayImageWhite;
                    }
                }

                //Farbe Selected Hintergrund
                if (SplitSetting[0] == "SelectedBackgroundColor")
                {
                    //Farbe erstellen
                    SelectedBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                }

                //Farbe Selected Vordergrund
                if (SplitSetting[0] == "SelectedForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        SelectedForegroundColor = "#FF000000";
                        SelectedPlayImage = PlayImageBlack;
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        SelectedForegroundColor = "#FFFFFFFF";
                        SelectedPlayImage = PlayImageWhite;
                    }
                }

                //Show Infos
                if (SplitSetting[0] == "ShowInfos")
                {
                    // Show infos erstellen
                    ShowInfos = Convert.ToBoolean(SplitSetting[1].Trim());
                }
            }



            // Wenn Select an Play aktiv und App das erste mal geladen wird
            if (SelectAndPlay == true & FirstLoad == true)
            {
                // Bild transparenz entfernen
                ImgSelectAndPlay.Opacity = 1.0;
                // Letzte Liste laden
                if (file.FileExists("/LastPlaylist/LastPlaylist.dat"))
                {
                    // Versuchen Liste zu erstellen
                    try
                    {
                        // Letzte Liste laden
                        filestream = file.OpenFile("/LastPlaylist/LastPlaylist.dat", FileMode.Open);
                        sr = new StreamReader(filestream);
                        string LastPlaylistString = sr.ReadToEnd();
                        filestream.Close();
                        // Letzte Liste aufteilen
                        string[] splitLastPlaylist = Regex.Split(LastPlaylistString, ";ZYXXYZ;");
                        // Durchlaufen
                        for (int i = 0; i < splitLastPlaylist.Count() - 1; i++)
                        {
                            // Eintrag splitten
                            string[] splitPlaylist = Regex.Split(splitLastPlaylist[i], ";XYZZYX;");
                            // Eintrag in aktuelle Liste schreiben
                            ListActivPlaylist.Add(new ClassActivePlayList(splitPlaylist[0], splitPlaylist[1], splitPlaylist[2], splitPlaylist[3], splitPlaylist[4]));
                        }
                        
                        // PlayID auslesen
                        PlayID = -1;
                        // Fall Mediaplayer Abspielt
                        if (MediaPlayer.State.ToString() == "Playing")
                        {
                            // Mediaplayer Daten laden
                            string playingArtist = MediaPlayer.Queue.ActiveSong.Artist.Name;
                            string playingAlbum = MediaPlayer.Queue.ActiveSong.Album.Name;
                            string playingSong = MediaPlayer.Queue.ActiveSong.Name; 
                            // PlayID ermitteln
                            for (int i = 0; i < ListActivPlaylist.Count(); i++)
                            {
                                if (ListActivPlaylist[i].Artist == playingArtist & ListActivPlaylist[i].Album == playingAlbum & ListActivPlaylist[i].Song == playingSong)
                                {
                                    PlayID = i;
                                    break;
                                }
                            }
                        }
                        // Wenn PlayID anders als -1
                        if (PlayID != -1)
                        {
                            // PlayControl auf Play setzen
                            PlayControl = "Play";
                        }
                        // Neuen Shuffle Int erstellen
                        CreateShuffleInt();
                    }
                    // Falls sich Liste nicht erstellen lässt
                    catch
                    {
                        // Aktuelle Liste löschen
                        file.DeleteFile("/LastPlaylist/LastPlaylist.dat");
                        // Aktuelle Playliste leeren
                        ListActivPlaylist.Clear();
                    }
                }
            }
            // Wenn Select and Play nicht aktiv
            else
            {
                // Bild transparent machen
                ImgSelectAndPlay.Opacity = 0.5;
            }



            //Wenn Hintergrundbild Portrait vorhanden
            if (file.FileExists("/Background/Portrait.jpg"))
            {
                //Bool umwandeln
                BackgroundPortrait = true;
                //Bilder laden
                byte[] data1;
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile("/Background/Portrait.jpg", FileMode.Open, FileAccess.Read))
                    {
                        data1 = new byte[isfs.Length];
                        isfs.Read(data1, 0, data1.Length);
                        isfs.Close();
                    }
                }
                try
                {
                    MemoryStream ms = new MemoryStream(data1);
                    BitmapImage bi = new BitmapImage();
                    bi.SetSource(ms);
                    var imageBrush = new ImageBrush(); imageBrush.ImageSource = bi;
                    LayoutRoot.Background = imageBrush;
                }
                catch
                {
                    ResetDesign();
                }
            }


            //Wenn Hintergrundbild Landscape vorhanden
            if (file.FileExists("/Background/Landscape.jpg"))
            {
                //Bool umwandeln
                BackgroundLandscape = true;
            }


            //Wenn Media Player Hintergrundbild Portrait vorhanden
            if (file.FileExists("/Background/MPBigPortrait.jpg"))
            {
                //Bool umwandeln
                MPBigBackgroundPortrait = true;
                //Bilder laden
                byte[] data1;
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile("/Background/MPBigPortrait.jpg", FileMode.Open, FileAccess.Read))
                    {
                        data1 = new byte[isfs.Length];
                        isfs.Read(data1, 0, data1.Length);
                        isfs.Close();
                    }
                }
                try
                {
                    MemoryStream ms = new MemoryStream(data1);
                    BitmapImage bi = new BitmapImage();
                    bi.SetSource(ms);
                    var imageBrush = new ImageBrush();
                    imageBrush.ImageSource = bi;
                    GRFullscreenPlayer.Background = imageBrush;
                }
                catch
                {
                    ResetDesign();
                }
            }

            //Wenn Media Player Hintergrundbild Landscape vorhanden
            if (file.FileExists("/Background/MPBigLandscape.jpg"))
            {
                //Bool umwandeln
                MPBigBackgroundLandscape = true;
            }



            //Bilder nach eingestellter Vordergrundfarbe erstellen
            ChangeImagesColor();



            //Prüfen ob Reminder noch vorhanden und wenn ja, laden
            if (file.FileExists("Settings/RateReminder.txt"))
            {
                //Daten laden
                filestream = file.OpenFile("Settings/RateReminder.txt", FileMode.Open);
                sr = new StreamReader(filestream);
                string temp = sr.ReadToEnd();
                filestream.Close();
                temp = temp.TrimEnd(new char[] { '\r', '\n' });

                //Prüfen of Benachrichtigung ausgegeben wird
                DateTime DT_Reminder = Convert.ToDateTime(temp);
                DateTime DT_Now = DateTime.Now;
                int result = DateTime.Compare(DT_Reminder, DT_Now);
                if (result < 0)
                {
                    //Bewertung öffnen
                    GRRate.Visibility = System.Windows.Visibility.Visible;
                    MenuOpen = true;
                }
            }



            //Tile Settings laden
            filestream = file.OpenFile("/Tiles/TileSettings.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            TileString = sr.ReadToEnd();
            filestream.Close();


            //TileString zerlegen
            string[] SplitTileString = Regex.Split(TileString, ";");
            //TileString durchlaufen und Einstellungen umsetzen
            for (int i = 0; i < (SplitTileString.Count() - 1); i++)
            {
                //Einstellung zerlegen und Prüfen
                string[] SplitSetting = Regex.Split(SplitTileString[i], "=");

                //FirstTile Background Color
                if (SplitSetting[0] == "BackgroundColor")
                {
                    TileBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                }

                //SecondTile Background Color
                if (SplitSetting[0] == "SecondBackgroundColor")
                {
                    SecondTileBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                }

                //Logo on Image Background Color
                if (SplitSetting[0] == "LogoImage")
                {
                    LogoImage = Convert.ToBoolean(SplitSetting[1]);
                }
            }



            //Einstellungen speichern nach laden
            if (SaveSettingAfterLoad == true)
            {
                CreateSettings();
                SaveSettingAfterLoad = false;
            }



            //Prüfen ob App gerade gekauft wurde und in Einstellungen speichern
            if ((Application.Current as App).IsTrial)
            {
            }
            //Bei Kaufversion
            else
            {
                if (Fullversion == false)
                {
                    //FullVersion umstellen
                    Fullversion = true;
                    //Einstellungen neu erstellen
                    CreateSettings();
                    //Benachrichtigung ausgeben
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_PurchaseNote);
                }
            }



            //Bei Vollversion
            if (Fullversion == true)
            {
                //Trial Anzeige verstecken
                SPTrial.Visibility = System.Windows.Visibility.Collapsed;
            }
            //Bei Demoversion
            else
            {
                //Wenn Trial Zeit besteht
                if (file.FileExists("Settings/Trial.dat"))
                {
                    //Settings/Trial laden
                    filestream = file.OpenFile("Settings/Trial.dat", FileMode.Open);
                    sr = new StreamReader(filestream);
                    string temp = sr.ReadToEnd();
                    filestream.Close();
                    DTTrial = Convert.ToDateTime(temp);
                }
                //Wenn Trial Zeit nicht besteht
                else
                {
                    //Settings/Trial erstellen
                    DTTrial = DateTime.Now;
                    filestream = file.CreateFile("Settings/Trial.dat");
                    sw = new StreamWriter(filestream);
                    sw.Write(DTTrial.ToString());
                    sw.Flush();
                    filestream.Close();
                }


                //Prüfen ob Trial Zeit abgelaufen
                TimeSpan diff = DateTime.Now - DTTrial;
                int MinToGo = 2880 - Convert.ToInt32(diff.TotalMinutes);
                //Wenn Zeit abgelaufen
                if (MinToGo <= 0)
                {
                    //Angeben das Bild auf Leer gestellt wird
                    AppIsRunning = false;
                    if (TrialMSG == false)
                    {
                        MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_TrialNote);
                        TrialMSG = true;
                        TBTrial.Text = MyMusicPlayer.Resources.AppResources.Z001_TrialExpired;
                        TBTialTime.Text = "";
                    }
                }
                //Wenn Zeit nicht abgelaufen
                else
                {
                    //Restliche Zeit erstellen
                    string tTime = "";
                    int tH = MinToGo / 60;
                    if (tH == 1)
                    {
                        tTime += tH + " " + MyMusicPlayer.Resources.AppResources.Z001_Hour + "   ";
                    }
                    else
                    {
                        tTime += tH + " " + MyMusicPlayer.Resources.AppResources.Z001_Hours + "   ";
                    }
                    int TM = MinToGo - (tH * 60);
                    if (TM == 1)
                    {
                        tTime += TM + " " + MyMusicPlayer.Resources.AppResources.Z001_Minute;
                    }
                    else
                    {
                        tTime += TM + " " + MyMusicPlayer.Resources.AppResources.Z001_Minutes;
                    }
                    //Zeit ausgeben
                    TBTialTime.Text = tTime;
                }
            }



            //LastPlayback.dat laden
            filestream = file.OpenFile("LastPlayback/LastPlayback.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            LastPlaybackString = sr.ReadToEnd();
            filestream.Close();



            //Menüs schließen
            GRListSelector.Visibility = System.Windows.Visibility.Collapsed;
            //Angeben das Menüs geschlossen sind
            MenuOpen = false;



            //Listen erstellen, wenn noch nicht erstellt
            if (CreateFirst == false)
            {
                List_Music(-1, -1);
                List_Playlists("CreateNew", -1);
                CreateFirst = true;
            }



            //Listen erstellen beim zurückkommen
            if (UpdateListsAtStart == true)
            {
                //Variable zurückstellen
                UpdateListsAtStart = false;
                //Timer anweisen das Liste upgedatet wird
                Timer_Settings_Action = "RefreshList";
                Timer_Settings.Start();
            }



            // Benachrichtigung ausgeben
            if (Msg == "SelectAndPlayBeta")
            {
                MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ005_FirstNote);
            }



            //Wenn Stoppen und Löschen Tile gedrückt wurde
            try
            {
                string IfStopAndDelete = NavigationContext.QueryString["tile"];
                base.OnNavigatedTo(e);
                if (IfStopAndDelete == "flip")
                {
                    //Tile zurücksetzen
                    CreateStandardTiles();
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.Stop();
                        DateTime temp = DateTime.Now;
                        for (int i = 0; i == 0; i = 0)
                        {
                            if (MediaPlayer.State.ToString() == "Stopped")
                            {
                                Song s = Song.FromUri("empty", new Uri("empty.wma", UriKind.Relative));
                                MediaPlayer.Play(s);
                                if (temp.AddSeconds(2) < DateTime.Now)
                                {
                                    Application.Current.Terminate();
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch
            {
            }

            // Angeben das App das erste mal geladen wurde
            FirstLoad = false;
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





        #region Bilder nach ändern der Fordergrundfarbe ändern
        //Bilder nach ändern der Fordergrundfarbe ändern
        //---------------------------------------------------------------------------------------------------------------------------------
        void ChangeImagesColor()
        {
            //Wenn Vordergrundfarbe weiß ist
            if (ForegroundColor == "#FFFFFFFF")
            {
                GRUpdateList.Background = ConvertToSolidColorBrush("#FF000000", -1);
                GRRate.Background = ConvertToSolidColorBrush("#FF000000", -1);

                ImgSelectAndPlay.Source = new BitmapImage(new Uri("/Images/Select.Dark.png", UriKind.Relative));
                ImgSearch.Source = new BitmapImage(new Uri("/Images/Search.Dark.png", UriKind.Relative));
                LogoRate.Source = new BitmapImage(new Uri("/Images/Logo.Dark.png", UriKind.Relative));
                ImgLock.Source = new BitmapImage(new Uri("/Images/Lock.Dark.png", UriKind.Relative));
                ImgRefresh.Source = new BitmapImage(new Uri("/Images/Refresh.Dark.png", UriKind.Relative));
                ImgLogo.Source = new BitmapImage(new Uri("/Images/Logo.Dark.png", UriKind.Relative));
                ImgLogo.Opacity = 0.1;
                ImgGlobe.Source = new BitmapImage(new Uri("/Images/Globe.Dark.png", UriKind.Relative));
                ImgInstructions.Source = new BitmapImage(new Uri("/Images/Instruction.Dark.png", UriKind.Relative));
                ImgSupport.Source = new BitmapImage(new Uri("/Images/Support.Dark.png", UriKind.Relative));
                MPBack.Source = new BitmapImage(new Uri("/Images/Back.Dark.png", UriKind.Relative));
                MPPlayPause.Source = new BitmapImage(new Uri("/Images/Play.Dark.png", UriKind.Relative));
                MPForward.Source = new BitmapImage(new Uri("/Images/Forward.Dark.png", UriKind.Relative));
                FPBack.Source = new BitmapImage(new Uri("/Images/Back.Dark.png", UriKind.Relative));
                FPPlayPause.Source = new BitmapImage(new Uri("/Images/Play.Dark.png", UriKind.Relative));
                FPForward.Source = new BitmapImage(new Uri("/Images/Forward.Dark.png", UriKind.Relative));
                MPOpenClose.Source = new BitmapImage(new Uri("/Images/Arrow.Up.Dark.png", UriKind.Relative));
                FPOpen.Source = new BitmapImage(new Uri("/Images/Arrow.Right.Dark.png", UriKind.Relative));
                FPClose.Source = new BitmapImage(new Uri("/Images/Arrow.Left.Dark.png", UriKind.Relative));
                MPRepead.Source = new BitmapImage(new Uri("/Images/Repeat.Dark.png", UriKind.Relative));
                MPShuffle.Source = new BitmapImage(new Uri("/Images/Shuffle.Dark.png", UriKind.Relative));
                FPRepead.Source = new BitmapImage(new Uri("/Images/Repeat.Dark.png", UriKind.Relative));
                FPShuffle.Source = new BitmapImage(new Uri("/Images/Shuffle.Dark.png", UriKind.Relative));
                ImgTimerPlay.Source = new BitmapImage(new Uri("/Images/Play.Dark.png", UriKind.Relative));
                ImgTimerDelete.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                ImgXtroseLogo.Source = new BitmapImage(new Uri("/Images/Logo40X40.Dark.png", UriKind.Relative));
            }
            //Wenn Vordergrundfarbe schwarz ist
            else
            {
                GRUpdateList.Background = ConvertToSolidColorBrush("#FFFFFFFF", -1);
                GRRate.Background = ConvertToSolidColorBrush("#FFFFFFFF", -1);

                //Bilder und Logos ändern
                ImgSelectAndPlay.Source = new BitmapImage(new Uri("/Images/Select.Light.png", UriKind.Relative));
                ImgSearch.Source = new BitmapImage(new Uri("/Images/Search.Light.png", UriKind.Relative));
                LogoRate.Source = new BitmapImage(new Uri("/Images/Logo.Light.png", UriKind.Relative));
                ImgLock.Source = new BitmapImage(new Uri("/Images/Lock.Light.png", UriKind.Relative));
                ImgRefresh.Source = new BitmapImage(new Uri("/Images/Refresh.Light.png", UriKind.Relative));
                ImgLogo.Source = new BitmapImage(new Uri("/Images/Logo.Light.png", UriKind.Relative));
                ImgLogo.Opacity = 0.1;
                ImgGlobe.Source = new BitmapImage(new Uri("/Images/Globe.Light.png", UriKind.Relative));
                ImgInstructions.Source = new BitmapImage(new Uri("/Images/Instruction.Light.png", UriKind.Relative));
                ImgSupport.Source = new BitmapImage(new Uri("/Images/Support.Light.png", UriKind.Relative));
                MPBack.Source = new BitmapImage(new Uri("/Images/Back.Light.png", UriKind.Relative));
                MPPlayPause.Source = new BitmapImage(new Uri("/Images/Play.Light.png", UriKind.Relative));
                MPForward.Source = new BitmapImage(new Uri("/Images/Forward.Light.png", UriKind.Relative));
                FPBack.Source = new BitmapImage(new Uri("/Images/Back.Light.png", UriKind.Relative));
                FPPlayPause.Source = new BitmapImage(new Uri("/Images/Play.Light.png", UriKind.Relative));
                FPForward.Source = new BitmapImage(new Uri("/Images/Forward.Light.png", UriKind.Relative));
                MPOpenClose.Source = new BitmapImage(new Uri("/Images/Arrow.Up.Light.png", UriKind.Relative));
                FPOpen.Source = new BitmapImage(new Uri("/Images/Arrow.Right.Light.png", UriKind.Relative));
                FPClose.Source = new BitmapImage(new Uri("/Images/Arrow.Left.Light.png", UriKind.Relative));
                MPRepead.Source = new BitmapImage(new Uri("/Images/Repeat.Light.png", UriKind.Relative));
                MPShuffle.Source = new BitmapImage(new Uri("/Images/Shuffle.Light.png", UriKind.Relative));
                FPRepead.Source = new BitmapImage(new Uri("/Images/Repeat.Light.png", UriKind.Relative));
                FPShuffle.Source = new BitmapImage(new Uri("/Images/Shuffle.Light.png", UriKind.Relative));
                ImgTimerPlay.Source = new BitmapImage(new Uri("/Images/Play.Light.png", UriKind.Relative));
                ImgTimerDelete.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                ImgXtroseLogo.Source = new BitmapImage(new Uri("/Images/Logo40X40.Light.png", UriKind.Relative));
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





        # region Einstellungen erstellen
        //Einstellungen neu erstellen
        //---------------------------------------------------------------------------------------------------------
        //Einstellung erstellen
        void CreateSettings()
        {
            //SettingsString erstellen
            SettingsString = "Fullversion=" + Fullversion.ToString() + ";LogoStart=" + LogoStart.ToString() + ";ExtendedInformation=" + ExtendedInformation + ";Repead=" + SetRepead.ToString() + ";Shuffle=" + SetShuffle.ToString() + ";PlayOnTime=" + PlayOnTime.ToString() + ";FontSize=" + ImageSize.ToString() + ";RunUnderLockScreen=" + RunUnderLockScreen.ToString() + ";SelectAndPlay=" + SelectAndPlay.ToString() + ";ShowInfos=" + ShowInfos.ToString() + ";";

            //Einstellungen in Settings/Settings schreiben
            filestream = file.CreateFile("Settings/Settings.dat");
            sw = new StreamWriter(filestream);
            sw.Write(SettingsString);
            sw.Flush();
            filestream.Close();
        }
        //---------------------------------------------------------------------------------------------------------
        # endregion





        # region LastPlayback erstellen
        //LastPlayback neu erstellen
        //---------------------------------------------------------------------------------------------------------
        //LastPlayback neu erstellen
        void CreateLastPlayback()
        {
            //LastPlayback neu erstellen
            string[] LastPlaybackStringSplit = Regex.Split(LastPlaybackString, ";;;;;");
            LastPlaybackString = "";
            //Index festlegen, falls mehr als 100 Wiedergaben vorhanden
            int ti = -1;
            if (LastPlaybackStringSplit.Count() > 101)
            {
                ti = 1;
            }
            else
            {
                ti = 0;
            }
            //Neue Letzte Wiedergaben Liste erstellen
            for (int i = ti; i < (LastPlaybackStringSplit.Count()-1) & i < 101; i++)
            {
                string[] LastPlaybackSplit = Regex.Split(LastPlaybackStringSplit[i], ";;;");
                LastPlaybackString += LastPlaybackSplit[0] + ";;;" + LastPlaybackSplit[1] + ";;;" + LastPlaybackSplit[2] + ";;;" + LastPlaybackSplit[3] + ";;;" + LastPlaybackSplit[4] + ";;;" + LastPlaybackSplit[5] + ";;;" + LastPlaybackSplit[6] + ";;;" + LastPlaybackSplit[7] + ";;;;;";
            }

            //LastPlayback/LastPlayback.dat speichern
            filestream = file.CreateFile("LastPlayback/LastPlayback.dat");
            sw = new StreamWriter(filestream);
            sw.Write(LastPlaybackString);
            sw.Flush();
            filestream.Close();
        }
        //---------------------------------------------------------------------------------------------------------
        # endregion






        # region Auflistung der Musik
        //Auflistung Music
        //---------------------------------------------------------------------------------------------------------
        void List_Music(int ArtistID, int AlbumID)
        {
            //Prüfen ob ListSongs bereits vorhanden
            if (ListSongs.Count > 0)
            {


                //Alben öffnen oder schließen
                if (ArtistID != -1 & AlbumID == -1)
                {
                    //Temp Liste leeren
                    TempListSongs.Clear();
                    //Ausgewählter Artist Name laden
                    string SelectedArtistName = mediaLibrary.Artists[ArtistID].Name;
                    //Ausgewählter Artist Alben laden
                    var SelectedArtistAlbums = mediaLibrary.Artists[ArtistID].Albums;
                    //Ausgewählter Artist Songs laden
                    var SelectedArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                    //Alte ListSongs durchlaufen und Daten in TempListSongs übertragen
                    for (int i = 0; i < ListSongs.Count; i++)
                    {
                        //Daten aus Ausgewältem Item auslesen
                        string tName = ListSongs[i].Name;
                        string tInfoString = ListSongs[i].InfoString;
                        string tArtist = ListSongs[i].Artist;
                        string tImageSize = ListSongs[i].ImageSize;
                        string tImageVisibility = ListSongs[i].ImageVisibility;
                        string tFontFamily = ListSongs[i].FontFamily;
                        string tBackground = ListSongs[i].Background;
                        string tForeground = ListSongs[i].Foreground;
                        string tMargin = ListSongs[i].Margin;
                        bool tIsSelected = ListSongs[i].IsSelected;
                        string tWhatIs = ListSongs[i].WhatIs;
                        string tExtendedInformationText = ListSongs[i].ExtendedInfoText;


                        //Wenn Item, ausgewählter Artist ist
                        if (tName == SelectedArtistName & tWhatIs == "Artist")
                        {

                            //Wenn Item noch nicht geöffnet ist
                            if (tIsSelected == false)
                            {
                                //Artist als geöffnet in TempListSongs übertragen
                                TempListSongs.Add(new ClassMedia(TempListSongs.Count, tArtist, tInfoString, tArtist, ImageSize, tImageVisibility, "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", true, "Artist", tExtendedInformationText, ExtendedInformation, SelectAndPlay, false));
                                //Wenn Alben vorhanden
                                if (SelectedArtistAlbums.Count > 0)
                                {
                                    //Alle Songs vom Artist laden
                                    TempListArtistSongs.Clear();
                                    var TempArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                    //Songs durchlaufen und TempListArtistSongs schreiben
                                    for (int i2 = 0; i2 < TempArtistSongs.Count; i2++)
                                    {
                                        TempListArtistSongs.Add(new ClassMedia(TempListArtistSongs.Count, TempArtistSongs[i2].Name, TempArtistSongs[i2].Album.Name, "", "", "", "", "", "", "", false, "TempArtistSongs", "", "false", SelectAndPlay, false));
                                    }

                                    //Alben in TempListSongs schreiben
                                    for (int i2 = 0; i2 < SelectedArtistAlbums.Count; i2++)
                                    {
                                        //Ablum Songs laden
                                        var TempArtistAlbumsSongs = mediaLibrary.Artists[ArtistID].Albums[i2].Songs;
                                        //Erweiterte Informationen erstellen
                                        string ExtendedInfoText = "";
                                        if (TempArtistAlbumsSongs.Count == 1)
                                        {
                                            ExtendedInfoText += TempArtistAlbumsSongs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + "  |  ";
                                        }
                                        else
                                        {
                                            ExtendedInfoText += TempArtistAlbumsSongs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + "  |  ";
                                        }
                                        TimeSpan Duration = new TimeSpan();
                                        for (int i3 = 0; i3 < mediaLibrary.Artists[ArtistID].Albums[i2].Songs.Count; i3++)
                                        {
                                            TimeSpan TempDuration = mediaLibrary.Artists[ArtistID].Albums[i2].Songs[i3].Duration;
                                            Duration = Duration + TempDuration;
                                        }
                                        ExtendedInfoText += CreateDurationString(Duration);
                                        //Farben erstellen
                                        string newForeground = AlbumForegroundColor;
                                        string newBackground = AlbumBackgroundColor;
                                        //Album in Liste schreiben
                                        TempListSongs.Add(new ClassMedia(TempListSongs.Count, mediaLibrary.Artists[ArtistID].Albums[i2].Name, tArtist, tArtist, ImageSize, "Visible", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Album", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                        //Songs durchlaufen und aus Liste löschen
                                        for (int i3 = 0; i3 < TempArtistAlbumsSongs.Count; i3++)
                                        {
                                            string TempArtistAlbumsSongsName = TempArtistAlbumsSongs[i3].Name;
                                            string TempArtistAlbumsSongsAlbum = TempArtistAlbumsSongs[i3].Album.Name;
                                            for (int i4 = 0; i4 < TempListArtistSongs.Count; i4++)
                                            {
                                                if (TempArtistAlbumsSongsName == TempListArtistSongs[i4].Name & TempArtistAlbumsSongsAlbum == TempListArtistSongs[i4].InfoString)
                                                {
                                                    TempListArtistSongs.RemoveAt(i4);
                                                }
                                            }
                                        }
                                    }

                                    //Prüfen ob Songs ohne Alben vorhanden
                                    if (TempListArtistSongs.Count > 0)
                                    {
                                        //Erweiterte Informationen erstellen
                                        string ExtendedInfoText = "";
                                        if (TempListArtistSongs.Count == 1)
                                        {
                                            ExtendedInfoText += TempListArtistSongs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            ExtendedInfoText += TempListArtistSongs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        //Farben erstellen
                                        string newForeground = AlbumForegroundColor;
                                        string newBackground = AlbumBackgroundColor;
                                        //Album Sonstige Songs in Liste schreiben
                                        TempListSongs.Add(new ClassMedia(TempListSongs.Count, MyMusicPlayer.Resources.AppResources.Z001_More, tArtist, tArtist, ImageSize, "Collapsed", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "MoreSongs", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                    }
                                }

                                //Wenn keine Alben vorhanden
                                else
                                {
                                    //Songs in TempListSongs schreiben
                                    for (int i2 = 0; i2 < SelectedArtistSongs.Count(); i2++)
                                    {
                                        //Erweiterte Informationen erstellen
                                        string ExtendedInfoText = CreateDurationString(SelectedArtistSongs[i2].Duration);
                                        //Farben erstellen
                                        string newForeground = SongForegroundColor;
                                        string newBackground = SongBackgroundColor;
                                        //Song in Liste schreiben
                                        TempListSongs.Add(new ClassMedia(TempListSongs.Count, SelectedArtistSongs[i2].Name, tArtist + ";none", tArtist, ImageSize, "Collapsed", "Segoe WP Light", newBackground, newForeground, "48,24,0,0", false, "Song", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                    }
                                }
                            }

                            //Wenn Item bereits geöffnet
                            else
                            {
                                //Artist als geschlossen in TempListSongs übertragen
                                TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", false, "Artist", tExtendedInformationText, ExtendedInformation, SelectAndPlay,false));
                                //ListSongs durchlaufen bis i nächster Artist ist
                                for (int i2 = i + 1; i2 < ListSongs.Count; i2++)
                                {
                                    //Daten von altem Eintrag laden
                                    if (ListSongs[i2].Artist == SelectedArtistName)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }


                        //Wenn Item ein ListSelector Element ist
                        else if (tWhatIs == "ListSelector")
                        {
                            //Letter Index neu erstellen
                            ListSelectorListNew(tName);
                            //Letter neu eintragen
                            TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, "", "false", SelectAndPlay, false));
                        }


                        //Wenn Item nicht ausgewählter Artist und kein ListSelector Element ist
                        else
                        {
                            //Artist als geöffnet in TempListSongs übertragen
                            TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInformationText, ExtendedInformation, SelectAndPlay, false));
                        }


                    }
                }




                //Songs öffnen oder schließen, wenn "Weitere" ausgewählt 
                else if (ArtistID != -1 & AlbumID == -2)
                {
                    //Temp Liste leeren
                    TempListSongs.Clear();
                    //Ausgewählter Artist Name laden
                    string SelectedArtistName = mediaLibrary.Artists[ArtistID].Name;
                    //Ausgewählter Artist Alben laden
                    var SelectedArtistAlbums = mediaLibrary.Artists[ArtistID].Albums;

                    //Alle Songs vom Artist laden
                    TempListArtistSongs.Clear();
                    var TempArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                    //Songs durchlaufen und TempListArtistSongs schreiben
                    for (int i2 = 0; i2 < TempArtistSongs.Count; i2++)
                    {
                        //Erweiterte Informationen erstellen
                        string ExtendedInfoText = CreateDurationString(TempArtistSongs[i2].Duration);
                        //Song in Temponäre Liste scheiben
                        TempListArtistSongs.Add(new ClassMedia(TempListArtistSongs.Count, TempArtistSongs[i2].Name, TempArtistSongs[i2].Album.Name, "", "", "", "", "", "", "", false, "Song", ExtendedInfoText, "false", SelectAndPlay, false));
                    }
                    //Alben durchlaufen und Songs aus TempListArtistSongs löschen
                    for (int i2 = 0; i2 < SelectedArtistAlbums.Count; i2++)
                    {
                        var TempArtistAlbumSongs = SelectedArtistAlbums[i2].Songs;
                        for (int i3 = 0; i3 < TempArtistAlbumSongs.Count; i3++)
                        {
                            string TempArtistAlbumName = TempArtistAlbumSongs[i3].Album.Name;
                            string TempArtistAlbumSongName = TempArtistAlbumSongs[i3].Name;
                            for (int i4 = 0; i4 < TempListArtistSongs.Count; i4++)
                            {
                                if (TempArtistAlbumSongName == TempListArtistSongs[i4].Name & TempArtistAlbumName == TempListArtistSongs[i4].InfoString)
                                {
                                    TempListArtistSongs.RemoveAt(i4);
                                }
                            }
                        }
                    }

                    //Alte ListSongs durchlaufen und Daten in TempListSongs übertragen
                    for (int i = 0; i < ListSongs.Count; i++)
                    {
                        //Daten aus Ausgewältem Item auslesen
                        string tName = ListSongs[i].Name;
                        string tInfoString = ListSongs[i].InfoString;
                        string tArtist = ListSongs[i].Artist;
                        string tImageSize = ListSongs[i].ImageSize;
                        string tImageVisibility = ListSongs[i].ImageVisibility;
                        string tFontFamily = ListSongs[i].FontFamily;
                        string tBackground = ListSongs[i].Background;
                        string tForeground = ListSongs[i].Foreground;
                        string tMargin = ListSongs[i].Margin;
                        bool tIsSelected = ListSongs[i].IsSelected;
                        string tWhatIs = ListSongs[i].WhatIs;
                        string tExtendedInfoText = ListSongs[i].ExtendedInfoText;

                        //Wenn Item ausgewählter Artist und Weitere ist
                        if (tName == MyMusicPlayer.Resources.AppResources.Z001_More & tArtist == SelectedArtistName & tWhatIs == "MoreSongs")
                        {
                            //Wenn Item noch nicht geöffnet ist
                            if (tIsSelected == false)
                            {
                                //"Weitere" als geöffnet in TempListSongs übertragen
                                TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, true, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                //Songs auflisten und eintragen
                                for (int i2 = 0; i2 < TempListArtistSongs.Count; i2++)
                                {
                                    //Farben erstellen
                                    string newForeground = SongForegroundColor;
                                    string newBackground = SongBackgroundColor;
                                    //Erweiterte Informationen erstellen
                                    TempListSongs.Add(new ClassMedia(TempListSongs.Count, TempListArtistSongs[i2].Name, tArtist + ";none", tArtist, ImageSize, "Collapsed", "Segoe WP Light", newBackground, newForeground, "48,24,0,0", false, "Song", TempListArtistSongs[i2].ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                }
                            }
                            //Wenn Item bereits geöfnet
                            else
                            {
                                //"Weitere" als geschlossen in TempListSongs übertragen
                                TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, false, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                //Weitere Songs überspringen
                                i += TempListArtistSongs.Count;
                            }
                        }

                        //Wenn Item nicht ausgewählter Artist und ausgewähltes Album ist
                        else
                        {
                            //Artist als geöffnet in TempListSongs übertragen
                            TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                        }

                    }
                }



                //Songs öffnen oder schließen, wenn Album ausgewählt
                else
                {
                    //Temp Liste leeren
                    TempListSongs.Clear();
                    //Ausgewählter Artist Name laden
                    string SelectedArtistName = mediaLibrary.Artists[ArtistID].Name;
                    //Ausgewähltes Album Name laden
                    string SelectedAlbumName = mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name;
                    //Ausgewählter Artist Alben laden
                    var SelectedArtistAlbums = mediaLibrary.Artists[ArtistID].Albums;
                    //Ausgewähltes Album Songs laden
                    var SelectedAlbumSongs = mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs;
                    //Alte ListSongs durchlaufen und Daten in TempListSongs übertragen
                    for (int i = 0; i < ListSongs.Count; i++)
                    {
                        //Daten aus Ausgewältem Item auslesen
                        string tName = ListSongs[i].Name;
                        string tInfoString = ListSongs[i].InfoString;
                        string tArtist = ListSongs[i].Artist;
                        string tImageSize = ListSongs[i].ImageSize;
                        string tImageVisibility = ListSongs[i].ImageVisibility;
                        string tFontFamily = ListSongs[i].FontFamily;
                        string tBackground = ListSongs[i].Background;
                        string tForeground = ListSongs[i].Foreground;
                        string tMargin = ListSongs[i].Margin;
                        bool tIsSelected = ListSongs[i].IsSelected;
                        string tWhatIs = ListSongs[i].WhatIs;
                        string tExtendedInfoText = ListSongs[i].ExtendedInfoText;


                        //Wenn Item, ausgewählter Artist und ausgewähltes Album ist
                        if (tName == SelectedAlbumName & tArtist == SelectedArtistName & tWhatIs == "Album")
                        {
                            //Wenn Item noch nicht geöffnet ist
                            if (tIsSelected == false)
                            {
                                //Album als geöffnet in TempListSongs übertragen
                                TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, "Visible", "Segoe WP", tBackground, tForeground, "24,24,0,0", true, "Album", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                //Songs in TempListSongs schreiben
                                for (int i2 = 0; i2 < SelectedAlbumSongs.Count; i2++)
                                {
                                    //Erweiterte Informationen vom Song Laden
                                    string ExtendedInfoText = CreateDurationString(SelectedAlbumSongs[i2].Duration);
                                    //Farben erstellen
                                    string newForeground = SongForegroundColor;
                                    string newBackground = SongBackgroundColor;
                                    //Song in Liste schreiben
                                    TempListSongs.Add(new ClassMedia(TempListSongs.Count, SelectedAlbumSongs[i2].Name, SelectedArtistName + ";" + SelectedAlbumName, tArtist, ImageSize, "Visible", "Segoe WP Light", newBackground, newForeground, "48,24,0,0", false, "Song", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                }
                            }

                            //Wenn Item bereits geöffnet
                            else
                            {
                                //Album als geschlossen in TempListSongs übertragen
                                TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, "Visible", "Segoe WP", tBackground, tForeground, "24,24,0,0", false, "Album", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                //ListSongs durchlaufen bis i nächster Artist ist
                                for (int i2 = i + 1; i2 < ListSongs.Count; i2++)
                                {
                                    //Daten von altem Eintrag laden
                                    string t3Name = ListSongs[i2].Name;
                                    bool IsFromAlbum = false;
                                    //Prüfen ob Datei in Album
                                    for (int i3 = 0; i3 < SelectedAlbumSongs.Count; i3++)
                                    {
                                        if (t3Name == SelectedAlbumSongs[i3].Name)
                                        {
                                            IsFromAlbum = true;
                                        }
                                    }
                                    if (IsFromAlbum == true)
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }


                        //Wenn Item ein ListSelector Element ist
                        else if (tWhatIs == "ListSelector")
                        {
                            //Letter Index neu erstellen
                            ListSelectorListNew(tName);
                            //Letter neu eintragen
                            TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, "", "false", SelectAndPlay, false));
                        }

                        //Wenn Item nicht ausgewählter Artist und ausgewähltes Album ist
                        else
                        {
                            //Artist als geöffnet in TempListSongs übertragen
                            TempListSongs.Add(new ClassMedia(TempListSongs.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                        }
                    }
                }



                //ListSongs aus TempListSongs erstellen
                ListSongs.Clear();
                for (int i = 0; i < TempListSongs.Count; i++)
                {
                    ListSongs.Add(TempListSongs[i]);
                }
                //Artisten in Listbox ausgeben
                LBSongs.ItemsSource = ListSongs;
            }



            //Wenn ListSongs noch nicht vorhanden //List Songs neu erstellen
            else if (AppIsRunning == true)
            {
                //Liste der Songs leeren
                ListSongs.Clear();

                //Wenn CatchArtist nicht besteht
                if (CatchArtists == "")
                {
                    //Wenn /Catch/Artists.dat bereits vorhanden
                    if (file.FileExists("/Catch/Artists.dat"))
                    {
                        //Catch/Artists.dat laden
                        filestream = file.OpenFile("/Catch/Artists.dat", FileMode.Open);
                        sr = new StreamReader(filestream);
                        CatchArtists = sr.ReadToEnd();
                        filestream.Close();
                    }
                }

                //Wenn CatchArtist vorhanden
                if (CatchArtists.Length > 0)
                {
                    //ListSelector LastLetter löschen
                    LastLetter = "none";
                    ListSelectorChangeColor();

                    //Catch/Artists.dat splitten
                    string[] SplitCatchArtists = Regex.Split(CatchArtists, ";ZYXXYZ;");

                    //Catch/Artists.dat in Liste übertragen
                    for (int i = 0; i < SplitCatchArtists.Count() - 1; i++)
                    {
                        //Einträge aufsplitten
                        string[] SplitEntry = Regex.Split(SplitCatchArtists[i], ";XYZZYX;");

                        //Wenn keine Suche vorhanden, List Selector direkt übernehmen
                        if (SplitEntry[5] == "ListSelector" & Search == "")
                        {
                            if(SplitEntry[0] == "#")
                            {
                                SplitEntry[0] = "Unknown";
                            }
                            //ListSelector Eintrag neu erstellen
                            CreateListSelector(SplitEntry[0], LastLetter);
                        }

                        //Artist Eintrag neu erstellen
                        else if (SplitEntry[5] == "Artist")
                        {
                            //Variablen erstellen
                            bool AddToList = false;

                            //Wenn Suche vorhanden
                            if (Search.Length > 0)
                            {
                                //Suche prüfen
                                int CS = SplitEntry[0].ToLower().IndexOf(Search, 0);
                                if (CS != -1)
                                {
                                    //Angeben das Eintrag hinzugefügt wird
                                    AddToList = true;
                                    //List Selector Eintrag neu erstellen
                                    CreateListSelector(SplitEntry[0], LastLetter);
                                }
                            }
                            //Wenn keine Suche vorhanden
                            else
                            {
                                //Angeben das Eintrag hinzugefügt wird
                                AddToList = true;
                            }

                            //Wenn Eintrag hinzugefügt wird
                            if (AddToList == true)
                            {
                                //Artist in Liste eintragen
                                ListSongs.Add(new ClassMedia(ListSongs.Count, SplitEntry[0], SplitEntry[1], SplitEntry[2], ImageSize, SplitEntry[4], "Segoe WP Semibold", ArtistBackgroundColor, ArtistForegroundColor, "0,24,0,0", false, "Artist", SplitEntry[6], ExtendedInformation, SelectAndPlay, false));
                            }
                        }
                    }
                }

                
                //Wenn kein Catch noch nicht erstellt, Catch neu erstellen
                else
                {
                    //Alle Artisten laden
                    var Artists = mediaLibrary.Artists;
                    int cArtists = Artists.Count;

                    //Artisten Durchlaufen und nach Name auflisten
                    for (int i = 0; i < cArtists; i++)
                    {
                        //try, catch um fehlerhafte Einträge auszuschneiden
                        try
                        {
                            //Suche einbinden
                            bool AddToList = false;
                            //Wenn Suche vorhanden
                            if (Search.Length > 0)
                            {
                                //Prüfen ob Suchbegriff in Name enthalten
                                int i2 = Artists[i].Name.ToLower().IndexOf(Search, 0);
                                if (i2 != -1)
                                {
                                    AddToList = true;
                                }
                            }
                            //Wenn keine Suche vorhanden
                            else
                            {
                                AddToList = true;
                            }

                            //Wenn Eintrag hinzugefügt wird
                            if (AddToList == true)
                            {
                                //List Selector erstellen
                                CreateListSelector(Artists[i].Name, LastLetter);
                                //Erweiterte Informationen erstellen
                                string ExtendedInfoText = "";
                                if (Artists[i].Albums.Count == 1)
                                {
                                    ExtendedInfoText += Artists[i].Albums.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Album + "  |  ";
                                }
                                else if (Artists[i].Albums.Count > 1)
                                {
                                    ExtendedInfoText += Artists[i].Albums.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Albums + "  |  ";
                                }
                                if (Artists[i].Songs.Count == 1)
                                {
                                    ExtendedInfoText += Artists[i].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + "  |  ";
                                }
                                else if (Artists[i].Songs.Count > 1)
                                {
                                    ExtendedInfoText += Artists[i].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + "  |  ";
                                }
                                if (Artists[i].Songs.Count > 0)
                                {
                                    TimeSpan Duration = new TimeSpan();
                                    for (int i2 = 0; i2 < Artists[i].Songs.Count; i2++)
                                    {
                                        TimeSpan TempDuration = Artists[i].Songs[i2].Duration;
                                        Duration = Duration + TempDuration;
                                    }
                                    string DurationString = CreateDurationString(Duration);
                                    ExtendedInfoText += DurationString;
                                }
                                //Wenn keine Lieder vorhanden Abspiel Button verbergen
                                string tImageVisibility = "Visible";
                                if (Artists[i].Songs.Count == 0)
                                {
                                    tImageVisibility = "Collapsed";
                                }
                                //Farben erstellen
                                string newForeground = ArtistForegroundColor;
                                string newBackground = ArtistBackgroundColor;
                                //Artist auflisten
                                ListSongs.Add(new ClassMedia(ListSongs.Count, Artists[i].Name, Artists[i].Name, Artists[i].Name, ImageSize, tImageVisibility, "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Artist", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            }
                        }
                        catch
                        {
                        }
                    }

                    //Wenn /Catch/Artists.dat noch nicht existiert // Catch erstellen
                    if (!file.FileExists("/Catch/Artists.dat"))
                    {
                        //Liste durchlaufen und /Catch/Artists.dat  erstellen
                        for (int i = 0; i < ListSongs.Count(); i++)
                        {
                            //Listenobjekt auslesen und in /Catch/Artists.dat schreiben
                            CatchArtists += ListSongs[i].Name + ";XYZZYX;" + ListSongs[i].InfoString + ";XYZZYX;" + ListSongs[i].Artist + ";XYZZYX;" + ListSongs[i].ImageVisibility + ";XYZZYX;" + ListSongs[i].Margin + ";XYZZYX;" + ListSongs[i].WhatIs.ToString() + ";XYZZYX;" + ListSongs[i].ExtendedInfoText + ";ZYXXYZ;";
                        }
                        //Catch/Artists.dat  erstellen
                        filestream = file.CreateFile("/Catch/Artists.dat");
                        sw = new StreamWriter(filestream);
                        sw.Write(CatchArtists);
                        sw.Flush();
                        filestream.Close();
                    }
                }


                //Artisten in Listbox ausgeben
                LBSongs.ItemsSource = ListSongs;
            }


            // Liste durchlaufen und mit aktueller Playliste abglichen
            if (SelectAndPlay == true)
            {
                // Variabeln
                string UpToDateArtist = "";
                string UpToDateAlbum = "";
                // Komplette Liste durchlaufen
                for (int i = 0; i < ListSongs.Count(); i++)
                {
                    // Wenn aktueller Eintrag Artist ist
                    if(ListSongs[i].WhatIs == "Artist")
                    {
                        try
                        {
                            // Aktuellen Artisten festlegen
                            UpToDateArtist = ListSongs[i].Name;
                            // Prüfen wieviele Songs der Artist hat
                            string splitString = ListSongs[i].ExtendedInfoText;
                            splitString = splitString.Replace("|", ";");
                            string[] split_ExtendedInfoText = Regex.Split(splitString, ";");
                            string SongsText = "";
                            if (split_ExtendedInfoText.Count() == 3)
                            {
                                SongsText = split_ExtendedInfoText[1].Trim();
                            }
                            else
                            {
                                SongsText = split_ExtendedInfoText[0].Trim();
                            }
                            string[] splitSongsText = Regex.Split(SongsText, " ");
                            int ArtistSongs = Convert.ToInt32(splitSongsText[0]);

                            // Prüfen wieviel Songs des Artisten in der Abspielliste sind
                            int PlaylistSongs = 0;
                            for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                            {
                                if (ListActivPlaylist[i2].Artist == UpToDateArtist)
                                {
                                    PlaylistSongs++;
                                }
                            }
                            // Wenn alle Songs ausgewählt
                            if (ArtistSongs == PlaylistSongs)
                            {
                                ListSongs[i].Background = SelectedBackgroundColor;
                                ListSongs[i].Foreground = SelectedForegroundColor;
                            }
                            // Wenn nicht alle Songs ausgewählt
                            else
                            {
                                ListSongs[i].Background = ArtistBackgroundColor;
                                ListSongs[i].Foreground = ArtistForegroundColor;
                            }
                        }
                        catch { }
                    }
                    // Wenn aktueller Eintrag ein Album ist
                    else if (ListSongs[i].WhatIs == "Album")
                    {
                        try
                        {
                            // Aktuelles Album festlegen
                            UpToDateAlbum = ListSongs[i].Name;
                            // Prüfen wieviele Songs das Album hat
                            string splitString = ListSongs[i].ExtendedInfoText;
                            splitString = splitString.Replace("|", ";");
                            string[] split_ExtendedInfoText = Regex.Split(splitString, ";");
                            string SongsText = "";
                            if (split_ExtendedInfoText.Count() == 2)
                            {
                                SongsText = split_ExtendedInfoText[0].Trim();
                            }
                            else
                            {
                                // SongsText = split_ExtendedInfoText[0].Trim();
                            }
                            string[] splitSongsText = Regex.Split(SongsText, " ");
                            int AlbumSongs = Convert.ToInt32(splitSongsText[0]);
                            // Prüfen wieviel Songs des Albums in der Abspielliste sind
                            int PlaylistSongs = 0;
                            for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                            {
                                if (ListActivPlaylist[i2].Artist == UpToDateArtist & ListActivPlaylist[i2].Album == UpToDateAlbum)
                                {
                                    PlaylistSongs++;
                                }
                            }
                            // Wenn alle Songs ausgewählt
                            if (AlbumSongs == PlaylistSongs)
                            {
                                ListSongs[i].Background = SelectedBackgroundColor;
                                ListSongs[i].Foreground = SelectedForegroundColor;
                            }
                            // Wenn nicht alle Songs ausgewählt
                            else
                            {
                                ListSongs[i].Background = AlbumBackgroundColor;
                                ListSongs[i].Foreground = AlbumForegroundColor;
                            }
                        }
                        catch
                        { }
                    }
                    // Wenn aktueller Eintrag ein Song ist
                    else if (ListSongs[i].WhatIs == "Song")
                    {
                        try
                        {
                            // Aktuelle Playliste durchlaufen und Prüfen ob Song vorhanden
                            bool SongSelected = false;
                            for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                            {
                                if (ListActivPlaylist[i2].Artist == UpToDateArtist & ListActivPlaylist[i2].Song == ListSongs[i].Name)
                                {
                                    SongSelected = true;
                                }
                            }
                            // Wenn Song ausgeählt
                            if (SongSelected == true)
                            {
                                ListSongs[i].Background = SelectedBackgroundColor;
                                ListSongs[i].Foreground = SelectedForegroundColor;
                            }
                            // Wenn Song nicht ausgewählt
                            else
                            {
                                ListSongs[i].Background = SongBackgroundColor;
                                ListSongs[i].Foreground = SongForegroundColor;
                            }
                        }
                        catch
                        { }
                    }
                }
                // Liste neu erstellen
                LBSongs.ItemsSource = null;
                LBSongs.ItemsSource = ListSongs;
            }


            //Listbox index auf 0 stellen
            LBSongs_ChangeSelection = false;
            try
            {
                LBSongs.SelectedIndex = -1;
            }
            catch
            {
            }
            LBSongs_ChangeSelection = true;
        }
        //---------------------------------------------------------------------------------------------------------





        //Auswahl aus Music
        //---------------------------------------------------------------------------------------------------------
        //Variabeln
        string LBSongs_LastClicked = "none";
        bool LBSongs_ChangeSelection = true;

        //Wenn auswahl geändert //Kommt nach der Auswahl der Buttons
        private void LBSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Wenn Selection ausgeführt wir
            if (LBSongs_ChangeSelection == true)
            {
                //Prüfen ob ausgeführt wird
                if (LBSongs.SelectedIndex != -1)
                {
                    //Index festlegen
                    int tIndex = LBSongs.SelectedIndex;

                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListSongs[tIndex].ID;
                    string tName = ListSongs[tIndex].Name;
                    string tInfoString = ListSongs[tIndex].InfoString;
                    string tArtist = ListSongs[tIndex].Artist;
                    string tImageSize = ListSongs[tIndex].ImageSize;
                    string tImageVisibility = ListSongs[tIndex].ImageVisibility;
                    string tFontFamily = ListSongs[tIndex].FontFamily;
                    string tBackground = ListSongs[tIndex].Background;
                    string tForeground = ListSongs[tIndex].Foreground;
                    string tMargin = ListSongs[tIndex].Margin;
                    bool tIsSelected = ListSongs[tIndex].IsSelected;
                    string tWhatIs = ListSongs[tIndex].WhatIs;


                    //Wenn Play gedrückt wurde
                    if (LBSongs_LastClicked == "Play")
                    {
                        //Wenn Artist ausgewählt
                        if (tWhatIs == "Artist")
                        {
                            //Variablen erstellen
                            int ArtistID = -1;
                            //Prüfen welche ID der Artist hat
                            var Artists = mediaLibrary.Artists;
                            int cArtists = Artists.Count;
                            //Artisten Durchlaufen und nach Name suchen
                            for (int i = 0; i < cArtists; i++)
                            {
                                //ID ermitteln
                                if (tName == Artists[i].Name)
                                {
                                    ArtistID = i;
                                    break;
                                }
                            }
                            //Variabeln erstellen
                            bool PlayFail = false;
                            //Songs von Artist laden
                            var SongsToPlay = mediaLibrary.Artists[ArtistID].Songs;

                            // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                            if (SelectAndPlay == true)
                            {
                                // Variabeln erstellen
                                string[] AddArtist = new string[SongsToPlay.Count()];
                                string[] AddAlbum = new string[SongsToPlay.Count()];
                                string[] AddSong = new string[SongsToPlay.Count()];
                                string[] AddGenre = new string[SongsToPlay.Count()];
                                string[] AddDuration = new string[SongsToPlay.Count()];
                                // Songs to Play durchlaufen und Prüfen ob alle ausgewählt sind
                                for (int i2 = 0; i2 < SongsToPlay.Count(); i2++)
                                {
                                    // Song Daten laden
                                    AddArtist[i2] = SongsToPlay[i2].Artist.ToString();
                                    AddAlbum[i2] = SongsToPlay[i2].Album.ToString();
                                    AddSong[i2] = SongsToPlay[i2].Name.ToString();
                                    AddGenre[i2] = SongsToPlay[i2].Genre.Name.ToString();
                                    AddDuration[i2] = SongsToPlay[i2].Duration.ToString();
                                }
                                // Liste der Songs druchlaufen und Prüfen ob alle Songs vorhanden
                                bool AllSongExists = true;
                                string SongsToDelete = "";
                                // Variable hinzugefügter Songs
                                int TempSongsAdded = 0;
                                // Ausgewählte Songs durchlaufen
                                for (int i2 = 0; i2 < AddArtist.Count(); i2++)
                                {
                                    // Prüfvariable ob Song existiert
                                    bool SongExists = false;
                                    // Aktive Playlist durchlaufen
                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                    {
                                        // Prüfen ob Song bereits existiert
                                        if (ListActivPlaylist[i3].Artist == AddArtist[i2] & ListActivPlaylist[i3].Album == AddAlbum[i2] & ListActivPlaylist[i3].Song == AddSong[i2])
                                        {
                                            SongExists = true;
                                            SongsToDelete += i3 + ";";
                                            break;
                                        }
                                    }
                                    // Wenn Song nicht existiert
                                    if (SongExists == false)
                                    {
                                        // Song hinzufügen
                                        ListActivPlaylist.Add(new ClassActivePlayList(AddArtist[i2], AddAlbum[i2], AddSong[i2], AddGenre[i2], AddDuration[i2]));
                                        // Anzahl hinzugefügter Songs erhöhen
                                        TempSongsAdded++;
                                        // Angeben das nicht alle Songs hinzugefügt waren
                                        AllSongExists = false;
                                    }
                                }
                                // Wenn alle Songs bereits vorhanden // Songs aus Aktiver Song Liste löschen
                                if (AllSongExists == true & SongsToDelete.Length > 0)
                                {
                                    // String mit zu löschenden Songs durchlaufen
                                    string[] split_SongsToDelete = Regex.Split(SongsToDelete, ";");
                                    int[] int_SongsToDelete = new int[split_SongsToDelete.Count() - 1];
                                    for (int i2 = 0; i2 < int_SongsToDelete.Count(); i2++)
                                    {
                                        int_SongsToDelete[i2] = Convert.ToInt32(split_SongsToDelete[i2]);
                                    }
                                    Array.Sort(int_SongsToDelete);
                                    // Songs aus akiver Liste löschen
                                    for (int i2 = int_SongsToDelete.Count() - 1; i2 >= 0; i2--)
                                    {
                                        // Songs aus Liste entfernen
                                        ListActivPlaylist.RemoveAt(int_SongsToDelete[i2]);
                                        // Hinzugefügte Songs verringern
                                        TempSongsAdded--;
                                    }
                                }

                                // Wenn alle Songs bereits vorhanden
                                if (AllSongExists == true)
                                {
                                    // Auswahl der Songs in der Listbox aufheben
                                    bool ThisArtist = true;
                                    for(int i2 = tIndex; i2 <= (tIndex + 100000000) & i2 < ListSongs.Count(); i2++)
                                    {
                                        // Prüfen ob Auswahl dieser oder nächster Artist
                                        if (ListSongs[i2].WhatIs == "Artist")
                                        {
                                            // Wenn Aktueller Artist
                                            if (ThisArtist == true)
                                            {
                                                // Angeben das dieser Artist
                                                ThisArtist = false;
                                                // Auswahl aufheben
                                                ListSongs[i2].Background = ArtistBackgroundColor;
                                                ListSongs[i2].Foreground = ArtistForegroundColor;
                                            }
                                            // Wenn nächster Artsit
                                            else
                                            {
                                                // Stoppen
                                                break;
                                            }
                                        }
                                        // Wenn Album
                                        else if (ListSongs[i2].WhatIs == "Album")
                                        {
                                            // Auswahl aufheben
                                            ListSongs[i2].Background = AlbumBackgroundColor;
                                            ListSongs[i2].Foreground = AlbumForegroundColor;
                                        }
                                        // Wenn Song
                                        else if (ListSongs[i2].WhatIs == "Song")
                                        {
                                            // Auswahl aufheben
                                            ListSongs[i2].Background = SongBackgroundColor;
                                            ListSongs[i2].Foreground = SongForegroundColor;
                                        }
                                    }
                                }
                                // Wenn nicht alle Songs vorhanden waren
                                else
                                {
                                    // Auswahl der Songs in der Listbox aufheben
                                    bool ThisArtist = true;
                                    for (int i2 = tIndex; i2 <= (tIndex + 10000000) & i2 < ListSongs.Count(); i2++)
                                    {
                                        // Prüfen ob Auswahl dieser oder nächster Artist
                                        if (ListSongs[i2].WhatIs == "Artist")
                                        {
                                            // Wenn Aktueller Artist
                                            if (ThisArtist == true)
                                            {
                                                // Angeben das dieser Artist
                                                ThisArtist = false;
                                                // Auswahl aufheben
                                                ListSongs[i2].Background = SelectedBackgroundColor;
                                                ListSongs[i2].Foreground = SelectedForegroundColor;
                                            }
                                            // Wenn nächster Artsit
                                            else
                                            {
                                                // Stoppen
                                                break;
                                            }
                                        }
                                        // Wenn Album oder Song
                                        else
                                        {
                                            // Auswahl aufheben
                                            if (ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "Song")
                                            {
                                                ListSongs[i2].Background = SelectedBackgroundColor;
                                                ListSongs[i2].Foreground = SelectedForegroundColor;
                                            }
                                        }
                                    }
                                }

                                // Liste neu erstellen
                                LBSongs.ItemsSource = null;
                                LBSongs.ItemsSource = ListSongs;
                                // Als letzte Liste speichern
                                SaveLastPlaylist();
                                // Benachrichtigung erstellen
                                string TempNote = "";
                                // Wenn Songs verringert wurden
                                if (TempSongsAdded < 0)
                                {
                                    // Songs erstellen
                                    TempSongsAdded = TempSongsAdded - (2 * TempSongsAdded);
                                    if (TempSongsAdded == 1)
                                    {
                                        TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                    }
                                    else
                                    {
                                        TempNote = "- " + TempSongsAdded + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                                    }
                                }
                                // Wenn Songs erhöht wurden
                                else
                                {
                                    if (TempSongsAdded == 1)
                                    {
                                        TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                    }
                                    else
                                    {
                                        TempNote = "+ " + TempSongsAdded + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                                    }
                                }
                                // Anzahl gesamter Lieder
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += 1 + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                // Benachrichtigung ausgeben
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }

                            // Wenn Select and Play nicht aktiv ist
                            else
                            {
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay);
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Artists[ArtistID].Songs.Count() == 1)
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Artist + " | " + mediaLibrary.Artists[ArtistID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                    }
                                    else
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Artist + " | " + mediaLibrary.Artists[ArtistID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                    }
                                    LastPlaybackString += mediaLibrary.Artists[ArtistID].Name + ";;;Artist;;;" + mediaLibrary.Artists[ArtistID].Name + ";;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                        }

                        //Wenn Album ausgewählt
                        if (tWhatIs == "Album")
                        {
                            //Variablen erstellen
                            int ArtistID = -1;
                            int AlbumID = -1;
                            //Prüfen welche ID der Artist hat
                            var Artists = mediaLibrary.Artists;
                            //Artisten Durchlaufen und nach Name suchen
                            for (int i = 0; i < Artists.Count; i++)
                            {
                                //ID ermitteln
                                if (tArtist == Artists[i].Name)
                                {
                                    ArtistID = i;
                                    break;
                                }
                            }
                            //Wenn Artisten ID vorhanden, Artist Alben laden
                            if (ArtistID != -1)
                            {
                                var Albums = mediaLibrary.Artists[ArtistID].Albums;
                                //Alben Durchlaufen und nach Name suchen
                                for (int i = 0; i < Albums.Count; i++)
                                {
                                    //ID ermitteln
                                    if (tName == Albums[i].Name)
                                    {
                                        AlbumID = i;
                                        break;
                                    }
                                }
                            }
                            //Variabeln erstellen
                            bool PlayFail = false;
                            //Songs von Album laden
                            var SongsToPlay = mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs;

                            // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                            if (SelectAndPlay == true)
                            {
                                // Variabeln erstellen
                                string[] AddArtist = new string[SongsToPlay.Count()];
                                string[] AddAlbum = new string[SongsToPlay.Count()];
                                string[] AddSong = new string[SongsToPlay.Count()];
                                string[] AddGenre = new string[SongsToPlay.Count()];
                                string[] AddDuration = new string[SongsToPlay.Count()];
                                // Anzahl hinzugefügter Songs
                                int TempSongsAdded = 0;
                                // Songs to Play durchlaufen und Daten in Arrays schreiben
                                for (int i2 = 0; i2 < SongsToPlay.Count(); i2++)
                                {
                                    // Song Daten laden
                                    AddArtist[i2] = SongsToPlay[i2].Artist.ToString();
                                    AddAlbum[i2] = SongsToPlay[i2].Album.ToString();
                                    AddSong[i2] = SongsToPlay[i2].Name.ToString();
                                    AddGenre[i2] = SongsToPlay[i2].Genre.Name.ToString();
                                    AddDuration[i2] = SongsToPlay[i2].Duration.ToString();
                                }
                                // Liste der Songs druchlaufen und Prüfen ob alle Songs vorhanden
                                bool AllSongExists = true;
                                string SongsToDelete = "";
                                // Ausgewählte Songs durchlaufen
                                for (int i2 = 0; i2 < AddArtist.Count(); i2++)
                                {
                                    // Prüfvariable ob Song existiert
                                    bool SongExists = false;
                                    // Aktive Playlist durchlaufen
                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                    {
                                        // Prüfen ob Song bereits existiert
                                        if (ListActivPlaylist[i3].Artist == AddArtist[i2] & ListActivPlaylist[i3].Album == AddAlbum[i2] & ListActivPlaylist[i3].Song == AddSong[i2])
                                        {
                                            SongExists = true;
                                            SongsToDelete += i3 + ";";
                                            break;
                                        }
                                    }
                                    // Wenn Song nicht existiert
                                    if (SongExists == false)
                                    {
                                        // Song hinzufügen
                                        ListActivPlaylist.Add(new ClassActivePlayList(AddArtist[i2], AddAlbum[i2], AddSong[i2], AddGenre[i2], AddDuration[i2]));
                                        // Anzahl hinzugefügter Songs erhöhen
                                        TempSongsAdded++;
                                        // Angeben das nicht alle Songs hinzugefügt waren
                                        AllSongExists = false;
                                    }
                                }
                                // Wenn alle Songs bereits vorhanden // Songs aus Aktiver Song Liste löschen
                                if (AllSongExists == true & SongsToDelete.Length > 0)
                                {
                                    // String mit zu löschenden Songs durchlaufen
                                    string[] split_SongsToDelete = Regex.Split(SongsToDelete, ";");
                                    int[] int_SongsToDelete = new int[split_SongsToDelete.Count() - 1];
                                    for (int i2 = 0; i2 < int_SongsToDelete.Count(); i2++)
                                    {
                                        int_SongsToDelete[i2] = Convert.ToInt32(split_SongsToDelete[i2]);
                                    }
                                    Array.Sort(int_SongsToDelete);
                                    // Songs aus akiver Liste löschen
                                    for (int i2 = int_SongsToDelete.Count() -1; i2 >= 0; i2--)
                                    {
                                        // Song löschen
                                        ListActivPlaylist.RemoveAt(int_SongsToDelete[i2]);
                                        // Anzahl hinzugefügter Songs verringern
                                        TempSongsAdded--;
                                    }
                                }

                                // Wenn alle Songs bereits vorhanden
                                if (AllSongExists == true)
                                {
                                    // Auswahl der Songs in der Listbox aufheben
                                    bool ThisAlbum = true;
                                    for (int i2 = tIndex; i2 <= (tIndex + 100000000) & i2 < ListSongs.Count(); i2++)
                                    {
                                        // Prüfen ob Auswahl dieser oder nächstes Album
                                        if (ListSongs[i2].WhatIs != "Song")
                                        {
                                            // Wenn Aktuelles Album
                                            if (ThisAlbum == true)
                                            {
                                                // Angeben das dieses Album
                                                ThisAlbum = false;
                                                // Auswahl aufheben
                                                ListSongs[i2].Background = AlbumBackgroundColor;
                                                ListSongs[i2].Foreground = AlbumForegroundColor;
                                            }
                                            // Wenn nächster Album, oder Artist
                                            else
                                            {
                                                // Stoppen
                                                break;
                                            }
                                        }
                                            // Wenn Song
                                        else if (ListSongs[i2].WhatIs == "Song")
                                        {
                                            // Auswahl aufheben
                                            ListSongs[i2].Background = SongBackgroundColor;
                                            ListSongs[i2].Foreground = SongForegroundColor;
                                        }
                                    }
                                    // Auswahl des Artisten aufheben
                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                    {
                                        // Prüfen ob Artist erreicht
                                        if (ListSongs[i2].WhatIs == "Artist")
                                        {
                                            ListSongs[i2].Background = ArtistBackgroundColor;
                                            ListSongs[i2].Foreground = ArtistForegroundColor;
                                            break;
                                        }
                                    }
                                }
                                // Wenn nicht alle Songs vorhanden waren
                                else
                                {
                                    // Auswahl der Songs in der Listbox erstellen
                                    bool ThisAlbum = true;
                                    for (int i2 = tIndex; i2 <= (tIndex + 10000000) & i2 < ListSongs.Count(); i2++)
                                    {
                                        // Prüfen ob Auswahl dieser oder nächster Album
                                        if (ListSongs[i2].WhatIs != "Song")
                                        {
                                            // Wenn Aktuelles Album
                                            if (ThisAlbum == true)
                                            {
                                                // Angeben das dieses Album
                                                ThisAlbum = false;
                                                // Auswahl erstellen
                                                ListSongs[i2].Background = SelectedBackgroundColor;
                                                ListSongs[i2].Foreground = SelectedForegroundColor;
                                            }
                                            // Wenn nächstes Album
                                            else
                                            {
                                                // Stoppen
                                                break;
                                            }
                                        }
                                        // Wenn Album oder Song
                                        else
                                        {
                                            // Auswahl erstellen
                                            if (ListSongs[i2].WhatIs == "Song")
                                            {
                                                ListSongs[i2].Background = SelectedBackgroundColor;
                                                ListSongs[i2].Foreground = SelectedForegroundColor;
                                            }
                                        }
                                    }
                                    // Alle Songs des Artisten laden
                                    var ArtistAllSongs = mediaLibrary.Artists[ArtistID].Songs;
                                    string ArtistName = mediaLibrary.Artists[ArtistID].Name;
                                    string[] split_ArtistAllSongs = new string[ArtistAllSongs.Count()];
                                    for (int i2 = 0; i2 < split_ArtistAllSongs.Count(); i2++)
                                    {
                                        split_ArtistAllSongs[i2] = ArtistAllSongs[i2].Name;
                                    }
                                    // Prüfen ob bereits alle Songs des Artisten vorhanden
                                    bool AllSongsSelected = true;
                                    // Liste der Songs des Artisten durchlaufen
                                    for (int i2 = 0; i2 < split_ArtistAllSongs.Count(); i2++)
                                    {
                                        // Liste der Ausgewählten Songs durchlaufen
                                        bool SongIsSelected = false;
                                        for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                        {
                                            // Prüfen ob Song vorhanden
                                            if (ListActivPlaylist[i3].Artist == ArtistName & ListActivPlaylist[i3].Song == split_ArtistAllSongs[i2])
                                            {
                                                SongIsSelected = true;
                                            }
                                        }
                                        // Wenn Song nicht ausgewählt ist
                                        if (SongIsSelected == false)
                                        {
                                            AllSongsSelected = true;
                                            break;
                                        }
                                    }
                                    // Wenn alle Songs ausgewählt
                                    if (AllSongsSelected == true)
                                    {
                                        // List Songs zurücklaufen bis Artist erreicht und diesen auswählen
                                        for (int i2 = tIndex; i2 >= 0; i2--)
                                        {
                                            // Prüfen ob Artist erreicht
                                            if (ListSongs[i2].WhatIs == "Artist")
                                            {
                                                ListSongs[i2].Background = SelectedBackgroundColor;
                                                ListSongs[i2].Foreground = SelectedForegroundColor;
                                                break;
                                            }
                                        }
                                    }
                                }

                                // Liste neu erstellen
                                LBSongs.ItemsSource = null;
                                LBSongs.ItemsSource = ListSongs;
                                // Als letzte Liste speichern
                                SaveLastPlaylist();
                                // Benachrichtigung erstellen
                                string TempNote = "";
                                // Wenn Songs verringert wurden
                                if (TempSongsAdded < 0)
                                {
                                    // Songs erstellen
                                    TempSongsAdded = TempSongsAdded - (2 * TempSongsAdded);
                                    if (TempSongsAdded == 1)
                                    {
                                        TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                    }
                                    else
                                    {
                                        TempNote = "- " + TempSongsAdded + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                                    }
                                }
                                // Wenn Songs erhöht wurden
                                else
                                {
                                    if (TempSongsAdded == 1)
                                    {
                                        TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                    }
                                    else
                                    {
                                        TempNote = "+ " + TempSongsAdded + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                                    }
                                }
                                // Anzahl gesamter Lieder
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += 1 + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                // Benachrichtigung ausgeben
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                            // Wenn Select and Play NICHT aktiv
                            else
                            {
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay);
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                            }

                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                //Extended info erstellen
                                string ExtendedInfoText = "";
                                if (mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() == 1)
                                {
                                    ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                }
                                else
                                {
                                    ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                }
                                LastPlaybackString += mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;Album;;;" + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Artist + ";;;" + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                CreateLastPlayback();
                            }
                        }

                        //Wenn Song ausgewählt wurde
                        if (tWhatIs == "Song")
                        {
                            //Variablen erstellen
                            int ArtistID = -1;
                            int AlbumID = -1;
                            int SongID = -1;

                            //Infos zerlegen
                            string[] TempInfoSplit = Regex.Split(tInfoString, ";");

                            //Wenn nur Song ohne Album
                            if (TempInfoSplit[1] == "none")
                            {
                                //Prüfen welche ID der Artist hat
                                var Artists = mediaLibrary.Artists;
                                //Artisten Durchlaufen und nach Name suchen
                                for (int i = 0; i < Artists.Count; i++)
                                {
                                    //ID ermitteln
                                    if (TempInfoSplit[0] == Artists[i].Name)
                                    {
                                        ArtistID = i;
                                        break;
                                    }
                                }
                                //Wenn Artisten ID vorhanden, Artist Songs laden
                                try
                                {
                                    var Songs = mediaLibrary.Artists[ArtistID].Songs;
                                    //Songs Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Songs.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (tName == Songs[i].Name)
                                        {
                                            SongID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Artist laden
                                    var SongsToPlay = mediaLibrary.Artists[ArtistID].Songs[SongID];

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay.Artist.ToString();
                                        string AddAlbum = SongsToPlay.Album.ToString();
                                        string AddSong = SongsToPlay.Name.ToString();
                                        string AddGenre = SongsToPlay.Genre.Name.ToString();
                                        string AddDuration = SongsToPlay.Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0 ; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay.Album.Name != "" & SongsToPlay.Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay.Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song; 
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }

                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            //Song abspielen
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay);
                                            //Extended info erstellen
                                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Artists[ArtistID].Songs[SongID].Name + ";;;Song;;;" + mediaLibrary.Artists[ArtistID].Songs[SongID].Artist + ";;;" + mediaLibrary.Artists[ArtistID].Songs[SongID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                //Wenn keine Artisten ID vorhanden, Song direkt laden
                                catch
                                {
                                    var Songs = mediaLibrary.Songs;
                                    //Songs Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Songs.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (tName == Songs[i].Name)
                                        {
                                            SongID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Album laden
                                    var SongsToPlay = mediaLibrary.Songs[SongID];

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay.Artist.ToString();
                                        string AddAlbum = SongsToPlay.Album.ToString();
                                        string AddSong = SongsToPlay.Name.ToString();
                                        string AddGenre = SongsToPlay.Genre.Name.ToString();
                                        string AddDuration = SongsToPlay.Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay.Album.Name != "" & SongsToPlay.Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay.Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }
                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay);
                                            //Extended info erstellen
                                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Songs[SongID].Name + ";;;Song;;;" + mediaLibrary.Songs[SongID].Artist + ";;;" + mediaLibrary.Songs[SongID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }

                            //Wenn Song aus Album geladen wird
                            else
                            {
                                //Prüfen welche ID der Artist hat
                                var Artists = mediaLibrary.Artists;
                                //Artisten Durchlaufen und nach Name suchen
                                for (int i = 0; i < Artists.Count; i++)
                                {
                                    //ID ermitteln
                                    if (TempInfoSplit[0] == Artists[i].Name)
                                    {
                                        ArtistID = i;
                                        break;
                                    }
                                }
                                //Wenn Artisten ID vorhanden, Artist Alben laden
                                try
                                {
                                    var Albums = mediaLibrary.Artists[ArtistID].Albums;
                                    //Songs Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Albums.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (TempInfoSplit[1] == Albums[i].Name)
                                        {
                                            AlbumID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Artist laden
                                    var SongsToPlay = mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs;
                                    //Album durchlaufen um SongID zu ermitteln
                                    int StartID = -1;
                                    for (int i = 0; i < SongsToPlay.Count(); i++)
                                    {
                                        if (SongsToPlay[i].Name == tName)
                                        {
                                            StartID = i;
                                            break;
                                        }
                                    }

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay[StartID].Artist.ToString();
                                        string AddAlbum = SongsToPlay[StartID].Album.ToString();
                                        string AddSong = SongsToPlay[StartID].Name.ToString();
                                        string AddGenre = SongsToPlay[StartID].Genre.Name.ToString();
                                        string AddDuration = SongsToPlay[StartID].Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay[StartID].Album.Name != "" & SongsToPlay[StartID].Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay[StartID].Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }
                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            //Extended info erstellen
                                            string ExtendedInfoText = "";
                                            if (mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() == 1)
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            }
                                            else
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                            }
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;Album;;;" + mediaLibrary.Artists[ArtistID].Name + ";;;" + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                            //Song abspielen
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay, StartID);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                //Wenn keine Artisten ID vorhanden, Song direkt laden
                                catch
                                {
                                    var Albums = mediaLibrary.Albums;
                                    //Alben Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Albums.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (TempInfoSplit[1] == Albums[i].Name)
                                        {
                                            AlbumID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Artist laden
                                    var SongsToPlay = mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs;
                                    //Album durchlaufen um SongID zu ermitteln
                                    int StartID = -1;
                                    for (int i = 0; i < SongsToPlay.Count(); i++)
                                    {
                                        if (SongsToPlay[i].Name == tName)
                                        {
                                            StartID = i;
                                            break;
                                        }
                                    }

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay[StartID].Artist.ToString();
                                        string AddAlbum = SongsToPlay[StartID].Album.ToString();
                                        string AddSong = SongsToPlay[StartID].Name.ToString();
                                        string AddGenre = SongsToPlay[StartID].Genre.Name.ToString();
                                        string AddDuration = SongsToPlay[StartID].Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay[StartID].Album.Name != "" & SongsToPlay[StartID].Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay[StartID].Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }
                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            //Extended info erstellen
                                            string ExtendedInfoText = "";
                                            if (mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() == 1)
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            }
                                            else
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                            }
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;Album;;;" + mediaLibrary.Artists[ArtistID].Name + ";;;" + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                            //Song abspielen
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay, StartID);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }



                    //Wenn Name gedrückt wurde
                    else if (LBSongs_LastClicked == "Name")
                    {
                        //Wenn Artist ausgewählt
                        if (tWhatIs == "Artist")
                        {
                            //Variablen erstellen
                            int ArtistID = -1;
                            //Prüfen welche ID der Artist hat
                            var Artists = mediaLibrary.Artists;
                            int cArtists = Artists.Count;
                            //Artisten Durchlaufen und nach Name suchen
                            for (int i = 0; i < cArtists; i++)
                            {
                                //ID ermitteln
                                if (tName == Artists[i].Name)
                                {
                                    ArtistID = i;
                                    break;
                                }
                            }
                            if (ArtistID != -1)
                            {
                                //Artist, Alben laden
                                List_Music(ArtistID, -1);
                            }
                        }

                        //Wenn Album ausgewählt
                        if (tWhatIs == "Album")
                        {
                            //Variablen erstellen
                            int ArtistID = -1;
                            int AlbumID = -1;
                            //Prüfen welche ID der Artist hat
                            var Artists = mediaLibrary.Artists;
                            //Artisten Durchlaufen und nach Name suchen
                            for (int i = 0; i < Artists.Count; i++)
                            {
                                //ID ermitteln
                                if (tArtist == Artists[i].Name)
                                {
                                    ArtistID = i;
                                    break;
                                }
                            }
                            //Wenn Artisten ID vorhanden, Artist Alben laden
                            if (ArtistID != -1)
                            {
                                var Albums = mediaLibrary.Artists[ArtistID].Albums;
                                //Alben Durchlaufen und nach Name suchen
                                for (int i = 0; i < Albums.Count; i++)
                                {
                                    //ID ermitteln
                                    if (tName == Albums[i].Name)
                                    {
                                        AlbumID = i;
                                        break;
                                    }
                                }
                            }
                            //Artist, Alben laden
                            if (ArtistID != -1 & AlbumID != -1)
                            {
                                List_Music(ArtistID, AlbumID);
                            }
                        }

                        //Wenn Weitere Songs ausgewählt wurden
                        if (tWhatIs == "MoreSongs")
                        {
                            //Variablen erstellen
                            int ArtistID = -1;
                            int AlbumID = -2;
                            //Prüfen welche ID der Artist hat
                            var Artists = mediaLibrary.Artists;
                            //Artisten Durchlaufen und nach Name suchen
                            for (int i = 0; i < Artists.Count; i++)
                            {
                                //ID ermitteln
                                if (tArtist == Artists[i].Name)
                                {
                                    ArtistID = i;
                                    break;
                                }
                            }
                            //Artist, Alben laden
                            if (ArtistID != -1 & AlbumID == -2)
                            {
                                List_Music(ArtistID, AlbumID);
                            }
                        }

                        //Wenn Song ausgewählt wurde
                        if (tWhatIs == "Song")
                        {
                            //Variablen erstellen
                            int ArtistID = -1;
                            int AlbumID = -1;
                            int SongID = -1;
                            //Infos zerlegen
                            string[] TempInfoSplit = Regex.Split(tInfoString, ";");

                            //Wenn nur Song ohne Album
                            if (TempInfoSplit[1] == "none")
                            {
                                //Prüfen welche ID der Artist hat
                                var Artists = mediaLibrary.Artists;
                                //Artisten Durchlaufen und nach Name suchen
                                for (int i = 0; i < Artists.Count; i++)
                                {
                                    //ID ermitteln
                                    if (TempInfoSplit[0] == Artists[i].Name)
                                    {
                                        ArtistID = i;
                                        break;
                                    }
                                }
                                //Wenn Artisten ID vorhanden, Artist Songs laden
                                try
                                {
                                    var Songs = mediaLibrary.Artists[ArtistID].Songs;
                                    //Songs Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Songs.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (tName == Songs[i].Name)
                                        {
                                            SongID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Artist laden
                                    var SongsToPlay = mediaLibrary.Artists[ArtistID].Songs[SongID];

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay.Artist.ToString();
                                        string AddAlbum = SongsToPlay.Album.ToString();
                                        string AddSong = SongsToPlay.Name.ToString();
                                        string AddGenre = SongsToPlay.Genre.Name.ToString();
                                        string AddDuration = SongsToPlay.Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay.Album.Name != "" & SongsToPlay.Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay.Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }
                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            //Song abspielen
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay);
                                            //Extended info erstellen
                                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Artists[ArtistID].Songs[SongID].Name + ";;;Song;;;" + mediaLibrary.Artists[ArtistID].Songs[SongID].Artist + ";;;" + mediaLibrary.Artists[ArtistID].Songs[SongID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                        }
                                        catch
                                        {
                                        }
                                    }

                                }
                                //Wenn keine Artisten ID vorhanden, Song direkt laden
                                catch
                                {
                                    var Songs = mediaLibrary.Songs;
                                    //Songs Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Songs.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (tName == Songs[i].Name)
                                        {
                                            SongID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Album laden
                                    var SongsToPlay = mediaLibrary.Songs[SongID];

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay.Artist.ToString();
                                        string AddAlbum = SongsToPlay.Album.ToString();
                                        string AddSong = SongsToPlay.Name.ToString();
                                        string AddGenre = SongsToPlay.Genre.Name.ToString();
                                        string AddDuration = SongsToPlay.Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay.Album.Name != "" & SongsToPlay.Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay.Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }
                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay);
                                            //Extended info erstellen
                                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Songs[SongID].Name + ";;;Song;;;" + mediaLibrary.Songs[SongID].Artist + ";;;" + mediaLibrary.Songs[SongID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }

                            //Wenn Song aus Album geladen wird
                            else
                            {
                                //Prüfen welche ID der Artist hat
                                var Artists = mediaLibrary.Artists;
                                //Artisten Durchlaufen und nach Name suchen
                                for (int i = 0; i < Artists.Count; i++)
                                {
                                    //ID ermitteln
                                    if (TempInfoSplit[0] == Artists[i].Name)
                                    {
                                        ArtistID = i;
                                        break;
                                    }
                                }
                                //Wenn Artisten ID vorhanden, Artist Alben laden
                                try
                                {
                                    var Albums = mediaLibrary.Artists[ArtistID].Albums;
                                    //Songs Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Albums.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (TempInfoSplit[1] == Albums[i].Name)
                                        {
                                            AlbumID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Artist laden
                                    var SongsToPlay = mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs;
                                    //Album durchlaufen um SongID zu ermitteln
                                    int StartID = -1;
                                    for (int i = 0; i < SongsToPlay.Count(); i++)
                                    {
                                        if (SongsToPlay[i].Name == tName)
                                        {
                                            StartID = i;
                                            break;
                                        }
                                    }

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay[StartID].Artist.ToString();
                                        string AddAlbum = SongsToPlay[StartID].Album.ToString();
                                        string AddSong = SongsToPlay[StartID].Name.ToString();
                                        string AddGenre = SongsToPlay[StartID].Genre.Name.ToString();
                                        string AddDuration = SongsToPlay[StartID].Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay[AlbumID].Album.Name != "" & SongsToPlay[AlbumID].Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay[AlbumID].Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }
                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            //Extended info erstellen
                                            string ExtendedInfoText = "";
                                            if (mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() == 1)
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            }
                                            else
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                            }
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;Album;;;" + mediaLibrary.Artists[ArtistID].Name + ";;;" + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                            //Song abspielen
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay, StartID);
                                        }
                                        catch
                                        {
                                        }
                                    }

                                }
                                //Wenn keine Artisten ID vorhanden, Song direkt laden
                                catch
                                {
                                    var Albums = mediaLibrary.Albums;
                                    //Alben Durchlaufen und nach Name suchen
                                    for (int i = 0; i < Albums.Count; i++)
                                    {
                                        //ID ermitteln
                                        if (TempInfoSplit[1] == Albums[i].Name)
                                        {
                                            AlbumID = i;
                                            break;
                                        }
                                    }
                                    //Songs von Artist laden
                                    var SongsToPlay = mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs;
                                    //Album durchlaufen um SongID zu ermitteln
                                    int StartID = -1;
                                    for (int i = 0; i < SongsToPlay.Count(); i++)
                                    {
                                        if (SongsToPlay[i].Name == tName)
                                        {
                                            StartID = i;
                                            break;
                                        }
                                    }

                                    // Wenn Select and Play aktiv // Song zu Liste hinzufügen oder entfernen
                                    if (SelectAndPlay == true)
                                    {
                                        // Song Daten laden
                                        string AddArtist = SongsToPlay[StartID].Artist.ToString();
                                        string AddAlbum = SongsToPlay[StartID].Album.ToString();
                                        string AddSong = SongsToPlay[StartID].Name.ToString();
                                        string AddGenre = SongsToPlay[StartID].Genre.Name.ToString();
                                        string AddDuration = SongsToPlay[StartID].Duration.ToString();
                                        // Liste der Songs druchlaufen und Prüfen ob bereits vorhanden
                                        bool SongExists = false;
                                        int DeleteSong = 1000000;
                                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                        {
                                            if (ListActivPlaylist[i2].Artist == AddArtist & ListActivPlaylist[i2].Album == AddAlbum & ListActivPlaylist[i2].Song == AddSong)
                                            {
                                                SongExists = true;
                                                DeleteSong = i2;
                                                break;
                                            }
                                        }
                                        // Wenn Song noch nicht vorhanden
                                        if (SongExists == false)
                                        {
                                            // Song hinzufügen
                                            ListActivPlaylist.Add(new ClassActivePlayList(AddArtist, AddAlbum, AddSong, AddGenre, AddDuration));
                                            ListSongs[tIndex].Background = SelectedBackgroundColor;
                                            ListSongs[tIndex].Foreground = SelectedForegroundColor;
                                            // Alle Songs des Artisten laden und prüfen ob alle ausgewählt
                                            var ArtistSongs = mediaLibrary.Artists[ArtistID].Songs;
                                            bool AllSongsSelected = true;
                                            // Liste aller Songs durchlaufen
                                            for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                            {
                                                bool SongSelected = false;
                                                // Liste der Aktiven Playlist durchlaufen
                                                for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                {
                                                    if (ListActivPlaylist[i3].Song == ArtistSongs[i2].Name & ListActivPlaylist[i3].Artist == ArtistSongs[i2].Artist.Name)
                                                    {
                                                        SongSelected = true;
                                                    }
                                                }
                                                // Wenn Song nicht ausgewählt
                                                if (SongSelected == false)
                                                {
                                                    AllSongsSelected = false;
                                                    break;
                                                }
                                            }
                                            // Wenn Alle Songs des Artisten ausgewählt sind
                                            if (AllSongsSelected == true)
                                            {
                                                // ListSongs nach hinten durchlaufen und Artist auswählen
                                                for (int i2 = tIndex; i2 >= 0; i2--)
                                                {
                                                    if (ListSongs[i2].WhatIs == "Artist")
                                                    {
                                                        ListSongs[i2].Background = SelectedBackgroundColor;
                                                        ListSongs[i2].Foreground = SelectedForegroundColor;
                                                        break;
                                                    }
                                                }
                                            }
                                            // Wenn Album vorhanden
                                            if (SongsToPlay[AlbumID].Album.Name != "" & SongsToPlay[AlbumID].Album.Name != null)
                                            {
                                                // Album des Songs laden
                                                var AlbumSongs = SongsToPlay[AlbumID].Album.Songs;
                                                AllSongsSelected = true;
                                                // Liste Aller Songs durchlaufen
                                                for (int i2 = 0; i2 < AlbumSongs.Count(); i2++)
                                                {
                                                    bool SongSelected = false;
                                                    // Liste der Aktiven Playlist durchlaufen
                                                    for (int i3 = 0; i3 < ListActivPlaylist.Count(); i3++)
                                                    {
                                                        if (ListActivPlaylist[i3].Song == AlbumSongs[i2].Name & ListActivPlaylist[i3].Artist == AlbumSongs[i2].Artist.Name)
                                                        {
                                                            SongSelected = true;
                                                        }
                                                    }
                                                    // Wenn Song nicht ausgewählt
                                                    if (SongSelected == false)
                                                    {
                                                        AllSongsSelected = false;
                                                        break;
                                                    }
                                                }
                                                // Wenn Alle Songs des Albums ausgewählt sind
                                                if (AllSongsSelected == true)
                                                {
                                                    // ListSongs nach hinten durchlaufen und Album auswählen
                                                    for (int i2 = tIndex; i2 >= 0; i2--)
                                                    {
                                                        if (ListSongs[i2].WhatIs == "Album")
                                                        {
                                                            ListSongs[i2].Background = SelectedBackgroundColor;
                                                            ListSongs[i2].Foreground = SelectedForegroundColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        // Wenn Song bereits vorhanden
                                        else
                                        {
                                            // Song löschen
                                            ListActivPlaylist.RemoveAt(DeleteSong);
                                            ListSongs[tIndex].Background = SongBackgroundColor;
                                            ListSongs[tIndex].Foreground = SongForegroundColor;
                                            // Liste der Songs nach hinten durchlaufen und Album und Artist abwählen
                                            bool UnselectAlbum = false;
                                            for (int i2 = tIndex; i2 >= 0; i2--)
                                            {
                                                // Album abwählen, wenn nicht Sontige Lieder
                                                if ((ListSongs[i2].WhatIs == "Album" | ListSongs[i2].WhatIs == "MoreSongs") & UnselectAlbum == false)
                                                {
                                                    UnselectAlbum = true;
                                                    if (ListSongs[i2].WhatIs == "Album")
                                                    {
                                                        ListSongs[i2].Background = AlbumBackgroundColor;
                                                        ListSongs[i2].Foreground = AlbumForegroundColor;
                                                    }
                                                }
                                                // Artist abwählen
                                                if (ListSongs[i2].WhatIs == "Artist")
                                                {
                                                    ListSongs[i2].Background = ArtistBackgroundColor;
                                                    ListSongs[i2].Foreground = ArtistForegroundColor;
                                                    break;
                                                }
                                            }
                                        }
                                        // Liste neu erstellen
                                        LBSongs.ItemsSource = null;
                                        LBSongs.ItemsSource = ListSongs;
                                        // Als letzte Liste speichern
                                        SaveLastPlaylist();
                                        // Benachrichtigung erstellen
                                        string TempNote = "";
                                        if (SongExists == true)
                                        {
                                            TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        else
                                        {
                                            TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                        }
                                        if (ListActivPlaylist.Count() == 1)
                                        {
                                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                        }
                                        else
                                        {
                                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                        }
                                        // Benachrichtigung ausgeben
                                        TBNote.Text = TempNote;
                                        Timer_Settings_Action = "Note";
                                        Timer_Settings_DTStart = DateTime.MinValue;
                                        Timer_Settings.Start();
                                    }
                                    // Wenn Select and Play nicht aktiv ist
                                    else
                                    {
                                        //Songs versuchen abzuspielen
                                        try
                                        {
                                            //Extended info erstellen
                                            string ExtendedInfoText = "";
                                            if (mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() == 1)
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                            }
                                            else
                                            {
                                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                            }
                                            //Song in Last Played schreiben
                                            LastPlaybackString += mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;Album;;;" + mediaLibrary.Artists[ArtistID].Name + ";;;" + mediaLibrary.Artists[ArtistID].Albums[AlbumID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                            CreateLastPlayback();
                                            //Song abspielen
                                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                            MediaPlayer.Play(SongsToPlay, StartID);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }

                        //Wenn ListSelector ausgewählt wird
                        if (tWhatIs == "ListSelector")
                        {
                            //ListSelector sichtbar machen
                            GRListSelector.Visibility = System.Windows.Visibility.Visible;
                            //Erste Liste unsichtbar machen
                            LBSongs.Opacity = 0.0;
                            //MenuOpen
                            MenuOpen = true;
                        }
                    }



                    //Wenn Nur Song abspielen gedrückt wurde
                    else if (LBSongs_LastClicked == "PlaySong")
                    {
                        //Variablen erstellen
                        int ArtistID = -1;
                        int AlbumID = -1;
                        int SongID = -1;

                        //Infos zerlegen
                        string[] TempInfoSplit = Regex.Split(tInfoString, ";");

                        //Prüfen welche ID der Artist hat
                        var Artists = mediaLibrary.Artists;
                        //Artisten Durchlaufen und nach Name suchen
                        for (int i = 0; i < Artists.Count; i++)
                        {
                            //ID ermitteln
                            if (TempInfoSplit[0] == Artists[i].Name)
                            {
                                ArtistID = i;
                                break;
                            }
                        }
                        //Wenn Artisten ID vorhanden, Artist Songs laden
                        try
                        {
                            var Songs = mediaLibrary.Artists[ArtistID].Songs;
                            //Songs Durchlaufen und nach Name suchen
                            for (int i = 0; i < Songs.Count; i++)
                            {
                                //ID ermitteln
                                if (tName == Songs[i].Name)
                                {
                                    SongID = i;
                                    break;
                                }
                            }
                            //Songs von Artist laden
                            var SongsToPlay = mediaLibrary.Artists[ArtistID].Songs[SongID];
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Song abspielen
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Play(SongsToPlay);
                                //Extended info erstellen
                                string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                //Song in Last Played schreiben
                                LastPlaybackString += mediaLibrary.Artists[ArtistID].Songs[SongID].Name + ";;;Song;;;" + mediaLibrary.Artists[ArtistID].Songs[SongID].Artist + ";;;" + mediaLibrary.Artists[ArtistID].Songs[SongID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                CreateLastPlayback();
                            }
                            catch
                            {
                            }
                        }
                        //Wenn keine Artisten ID vorhanden, Song direkt laden
                        catch
                        {
                            var Songs = mediaLibrary.Songs;
                            //Songs Durchlaufen und nach Name suchen
                            for (int i = 0; i < Songs.Count; i++)
                            {
                                //ID ermitteln
                                if (tName == Songs[i].Name)
                                {
                                    SongID = i;
                                    break;
                                }
                            }
                            //Songs von Album laden
                            var SongsToPlay = mediaLibrary.Songs[SongID];
                            //Songs versuchen abzuspielen
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Play(SongsToPlay);
                                //Extended info erstellen
                                string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                //Song in Last Played schreiben
                                LastPlaybackString += mediaLibrary.Songs[SongID].Name + ";;;Song;;;" + mediaLibrary.Songs[SongID].Artist + ";;;" + mediaLibrary.Songs[SongID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                CreateLastPlayback();
                            }
                            catch
                            {
                            }
                        }
                    }
                }



                //LBSongs_LastClicked zurücksetzen
                LBSongs_LastClicked = "none";
                //Auswahl aufheben
                try
                {
                    LBSongs.SelectedIndex = -1;
                }
                catch
                {
                }
            }
        }



        //Wenn Play gedrück wurde //Kommt vor wenn auswahl geändert
        private void LBSongs_ButtonPlay(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBSongs_LastClicked = "Play";
        }

        //Wenn Name gedrück wurde //Kommt vor wenn auswahl geändert
        private void LBSongs_ButtonName(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBSongs_LastClicked = "Name";
        }

        //Wenn PlayAlbum gedrückt wurde
        private void LBSongs_ButtonPlaySong(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBSongs_LastClicked = "PlaySong";
        }
        //---------------------------------------------------------------------------------------------------------
        # endregion





        # region Auflistung der Wiedergabelisten
        //Auflistung Playlists
        //---------------------------------------------------------------------------------------------------------
        void List_Playlists(string WhatToDo, int ID)
        {
            //TempListPlaylist leeren
            TempListPlaylist.Clear();



            //Wenn Liste neu erstellt wird
            if (WhatToDo == "CreateNew" &  AppIsRunning == true)
            {
                //Farben erstellen
                string newForeground = ArtistForegroundColor;
                string newBackground = ArtistBackgroundColor;
                string ExtendedInfoText;

                //Wenn LastPlaybacks vorhanden und Select and play deaktiviert ist
                if (LastPlaybackString != "" & Search == "" & SelectAndPlay != true)
                {
                    //LastPlaybacks erweiterte Informationen erstellen
                    string[] LastPlaybackStringSplit = Regex.Split(LastPlaybackString, ";;;;;");
                    ExtendedInfoText = (LastPlaybackStringSplit.Count() - 1).ToString() + " " + MyMusicPlayer.Resources.AppResources.Z001_Playbacks;
                    //LastPlaybacks eintragen
                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playbacks, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "LastPlaybacks", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                }


                //Playlist erstellen, wenn keine Suche vorhanden
                if (Search == "")
                {
                    // Wenn Select and Play nicht aktiv ist
                    if (SelectAndPlay != true)
                    {
                        //Wiedergabelisten erweiterte Informationen erstellen
                        if (mediaLibrary.Playlists.Count == 1)
                        {
                            ExtendedInfoText = mediaLibrary.Playlists.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlist;
                        }
                        else
                        {
                            ExtendedInfoText = mediaLibrary.Playlists.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlists;
                        }
                        //Wiedergabelisten Eintrag
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Playlists", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                    // Wenn select and Play aktiv ist
                    else
                    {
                        // Wenn Ordner mit Wiedergabelisten noch nicht vorhanden
                        if (!file.DirectoryExists("/Playlists"))
                        {
                            file.CreateDirectory("/Playlists");
                        }
                        // Ordner mit Wiedergabelisten laden
                        string[] Playlists = file.GetFileNames("/Playlists/");
                        //Wiedergabelisten erweiterte Informationen erstellen
                        if (Playlists.Count() == 1)
                        {
                            ExtendedInfoText = Playlists.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlist;
                        }
                        else
                        {
                            ExtendedInfoText = Playlists.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlists;
                        }
                        //Wiedergabelisten Eintrag
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Playlists", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                }
                //Playlists erstellen, wenn Suche vorhanden
                else
                {
                    // Wenn Select and Play nicht aktiv ist
                    if (SelectAndPlay != true)
                    {
                        //Variabeln erstellen
                        int TempCount = 0;
                        //Playlists laden
                        var TempPlaylists = mediaLibrary.Playlists;
                        //Playlists durchlaufen und durchsuchen
                        for (int i = 0; i < TempPlaylists.Count(); i++)
                        {//Prüfen ob Suchbegriff in Name enthalten
                            int i2 = TempPlaylists[i].Name.ToLower().IndexOf(Search, 0);
                            if (i2 != -1)
                            {
                                TempCount++;
                            }
                        }
                        //Wiedergabelisten erweiterte Informationen erstellen
                        if (TempCount == 1)
                        {
                            ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlist;
                        }
                        else
                        {
                            ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlists;
                        }
                        //Wiedergabelisten Eintrag
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Playlists", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }

                    // Wenn Select and Play aktiv ist
                    else
                    {
                        // Wenn Ordner mit Wiedergabelisten noch nicht vorhanden
                        if (!file.DirectoryExists("/Playlists"))
                        {
                            file.CreateDirectory("/Playlists");
                        }
                        //Variabeln erstellen
                        int TempCount = 0;
                        // Ordner mit Wiedergabelisten laden
                        string[] Playlists = file.GetFileNames("/Playlists");
                        //Playlists durchlaufen und durchsuchen
                        for (int i = 0; i < Playlists.Count(); i++)
                        {
                            //Prüfen ob Suchbegriff in Name enthalten
                            int i2 = Playlists[i].ToLower().IndexOf(Search, 0);
                            if (i2 != -1)
                            {
                                TempCount++;
                            }
                        }
                        //Wiedergabelisten erweiterte Informationen erstellen
                        if (TempCount == 1)
                        {
                            ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlist;
                        }
                        else
                        {
                            ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Playlists;
                        }
                        //Wiedergabelisten Eintrag
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Playlists", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                }



                //Genres erstellen, wenn keine Suche vorhanden
                if (Search == "")
                {
                    //Genres erweiterte Informationen erstellen
                    if (mediaLibrary.Genres.Count == 1)
                    {
                        ExtendedInfoText = mediaLibrary.Genres.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Genre;
                    }
                    else
                    {
                        ExtendedInfoText = mediaLibrary.Genres.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Genres;
                    }
                    //Genres Eintrag
                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Genres, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Genres", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                }
                //Genres erstellen wenn Suche vorhanden
                else
                {
                    //Variabeln erstellen
                    int TempCount = 0;
                    //Playlists laden
                    var TempGenres = mediaLibrary.Genres;
                    //Playlists durchlaufen und durchsuchen
                    for (int i = 0; i < TempGenres.Count(); i++)
                    {//Prüfen ob Suchbegriff in Name enthalten
                        int i2 = TempGenres[i].Name.ToLower().IndexOf(Search, 0);
                        if (i2 != -1)
                        {
                            TempCount++;
                        }
                    }
                    //Wiedergabelisten erweiterte Informationen erstellen
                    if (TempCount == 1)
                    {
                        ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Genre;
                    }
                    else
                    {
                        ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Genres;
                    }
                    //Genres Eintrag
                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Genres, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Genres", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                }


                //Alben erstellen, wenn keine Suche vorhanden
                if (Search == "")
                {
                    //Albums erweiterte Informationen erstellen
                    if (mediaLibrary.Albums.Count == 1)
                    {
                        ExtendedInfoText = mediaLibrary.Albums.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Album;
                    }
                    else
                    {
                        ExtendedInfoText = mediaLibrary.Albums.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Albums;
                    }
                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Albums, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Albums", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                }
                //Alben erstellen, wenn Suche vorhanden
                else
                {
                    //Variabeln erstellen
                    int TempCount = 0;
                    //Playlists laden
                    var TempAlbums = mediaLibrary.Albums;
                    //Playlists durchlaufen und durchsuchen
                    for (int i = 0; i < TempAlbums.Count(); i++)
                    {//Prüfen ob Suchbegriff in Name enthalten
                        int i2 = TempAlbums[i].Name.ToLower().IndexOf(Search, 0);
                        if (i2 != -1)
                        {
                            TempCount++;
                        }
                    }
                    //Wiedergabelisten erweiterte Informationen erstellen
                    if (TempCount == 1)
                    {
                        ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Album;
                    }
                    else
                    {
                        ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Albums;
                    }
                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Albums, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "Albums", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                }



                //Alle Songs erstellen, wenn keine Suche vorhanden
                if (Search == "")
                {
                    //Alle Songs erweiterte Informationen erstellen
                    if (mediaLibrary.Songs.Count == 1)
                    {
                        ExtendedInfoText = mediaLibrary.Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                    }
                    else
                    {
                        ExtendedInfoText = mediaLibrary.Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                    }
                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_AllSongs, "", "", ImageSize, "Visibile", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "AllSongs", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                }
                //Alle Songs erstellen, wenn Suche vorhanden
                else
                {
                    //Variabeln erstellen
                    int TempCount = 0;
                    //Playlists laden
                    var TempSongs = mediaLibrary.Songs;
                    //Playlists durchlaufen und durchsuchen
                    for (int i = 0; i < TempSongs.Count(); i++)
                    {//Prüfen ob Suchbegriff in Name enthalten
                        int i2 = TempSongs[i].Name.ToLower().IndexOf(Search, 0);
                        if (i2 != -1)
                        {
                            TempCount++;
                        }
                    }
                    //Wiedergabelisten erweiterte Informationen erstellen
                    if (TempCount == 1)
                    {
                        ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                    }
                    else
                    {
                        ExtendedInfoText = TempCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                    }
                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_AllSongs, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", newBackground, newForeground, "0,24,0,0", false, "AllSongs", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                }
            }





            //Playlisten öffnen oder schließen
            else if (WhatToDo == "OpenClosePlaylists")
            {
                // Wenn Select and Play nicht aktiv ist
                if (SelectAndPlay != true)
                {
                    //Liste durchlaufen und Playlists suchen
                    for (int i = 0; i < ListPlaylist.Count; i++)
                    {
                        //Daten aus Ausgewältem Item auslesen
                        int tID = ListPlaylist[i].ID;
                        string tName = ListPlaylist[i].Name;
                        var tInfoString = ListPlaylist[i].InfoString;
                        var tArtist = ListPlaylist[i].Artist;
                        string tImageSize = ListPlaylist[i].ImageSize;
                        string tImageVisibility = ListPlaylist[i].ImageVisibility;
                        string tFontFamily = ListPlaylist[i].FontFamily;
                        string tBackground = ListPlaylist[i].Background;
                        string tForeground = ListPlaylist[i].Foreground;
                        string tMargin = ListPlaylist[i].Margin;
                        bool tIsSelected = ListPlaylist[i].IsSelected;
                        string tWhatIs = ListPlaylist[i].WhatIs;
                        string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                        //Wenn ausgewähltes Item Playlists
                        if (tWhatIs == "Playlists")
                        {
                            //Wenn geöffnet wird
                            if (tIsSelected == false)
                            {
                                //Playlists als geöffnet eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", true, "Playlists", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));

                                //Farben erstellen
                                string newForeground = AlbumForegroundColor;
                                string newBackground = AlbumBackgroundColor;

                                //Wenn CatchPlaylists noch nicht vorhanden
                                if (CatchPlaylists == "")
                                {
                                    //Wenn /Catch/Playlists.dat vorhanden //CatchPlaylists erstellen
                                    if (file.FileExists("/Catch/Playlists.dat"))
                                    {
                                        //Catch/Artists.dat laden
                                        filestream = file.OpenFile("/Catch/Playlists.dat", FileMode.Open);
                                        sr = new StreamReader(filestream);
                                        CatchPlaylists = sr.ReadToEnd();
                                        filestream.Close();
                                    }
                                }

                                //Wenn CatchPlaylists vorhanden
                                if (CatchPlaylists != "")
                                {
                                    //CatchPlaylists splitten
                                    string[] SplitCatchPlaylists = Regex.Split(CatchPlaylists, ";ZYXXYZ;");

                                    //CatchPlaylists in Liste übertragen
                                    for (int i2 = 0; i2 < SplitCatchPlaylists.Count() - 1; i2++)
                                    {
                                        //Einträge aufsplitten
                                        string[] SplitEntry = Regex.Split(SplitCatchPlaylists[i2], ";XYZZYX;");

                                        //Variablen erstellen
                                        bool AddToList = false;

                                        //Wenn Suche vorhanden
                                        if (Search.Length > 0)
                                        {
                                            //Suche prüfen
                                            int CS = SplitEntry[0].ToLower().IndexOf(Search, 0);
                                            if (CS != -1)
                                            {
                                                //Angeben das Eintrag hinzugefügt wird
                                                AddToList = true;
                                            }
                                        }
                                        //Wenn keine Suche vorhanden
                                        else
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }

                                        //Wenn Eintrag hinzugefügt wird
                                        if (AddToList == true)
                                        {
                                            //Playlist in Liste eintragen
                                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, SplitEntry[0], SplitEntry[1], "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Playlist", SplitEntry[2], ExtendedInformation, SelectAndPlay, false));
                                        }
                                    }
                                }

                                //Wenn CatchPlaylists nicht vorhanden
                                else
                                {
                                    //Playlist Daten laden
                                    var TempPlaylists = mediaLibrary.Playlists;
                                    //Playlists in Liste eintragen
                                    for (int i2 = 0; i2 < TempPlaylists.Count; i2++)
                                    {
                                        //Variablen erstellen
                                        bool AddToList = false;

                                        //Wenn Suche vorhanden
                                        if (Search.Length > 0)
                                        {
                                            //Suche prüfen
                                            int CS = TempPlaylists[i2].Name.ToLower().IndexOf(Search, 0);
                                            if (CS != -1)
                                            {
                                                //Angeben das Eintrag hinzugefügt wird
                                                AddToList = true;
                                            }
                                        }
                                        //Wenn keine Suche vorhanden
                                        else
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }

                                        //Erweiterte Informationen erstellen
                                        string ExtendedInfoText;
                                        if (TempPlaylists[i2].Songs.Count == 1)
                                        {
                                            ExtendedInfoText = TempPlaylists[i2].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + "  |  ";
                                        }
                                        else
                                        {
                                            ExtendedInfoText = TempPlaylists[i2].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + "  |  ";
                                        }
                                        TimeSpan Duration = new TimeSpan();
                                        for (int i3 = 0; i3 < TempPlaylists[i2].Songs.Count; i3++)
                                        {
                                            TimeSpan TempDuration = TempPlaylists[i2].Songs[i3].Duration;
                                            Duration = Duration + TempDuration;
                                        }
                                        ExtendedInfoText += CreateDurationString(Duration);

                                        //Wenn Eintrag erstellt wird
                                        if (AddToList == true)
                                        {
                                            //Wiedergabeliste in Liste schreiben
                                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, TempPlaylists[i2].Name, i2.ToString(), "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Playlist", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                        }

                                        //CatchPlaylists erstellen
                                        CatchPlaylists += TempPlaylists[i2].Name + ";XYZZYX;" + i2.ToString() + ";XYZZYX;" + ExtendedInfoText + ";ZYXXYZ;";
                                    }
                                    //Catch/Playlists.dat speichern
                                    filestream = file.CreateFile("/Catch/Playlists.dat");
                                    sw = new StreamWriter(filestream);
                                    sw.Write(CatchPlaylists);
                                    sw.Flush();
                                    filestream.Close();
                                }
                            }

                            //Wenn geschlossen wird
                            else
                            {
                                //Playlists als geschlossen eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", false, "Playlists", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                for (int i2 = (i + 1); i2 < ListPlaylist.Count; i2++)
                                {
                                    if (ListPlaylist[i2].WhatIs == "Playlist" | ListPlaylist[i2].WhatIs == "Playlists" | ListPlaylist[i2].WhatIs == "Song")
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        //Wenn ausgewähltes Item nicht Playlist ist
                        else
                        {
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                        }
                    }
                }


                // Wenn Select and Play aktiv ist
                else if (SelectAndPlay == true)
                { 
                    //Liste durchlaufen und Playlists suchen
                    for (int i = 0; i < ListPlaylist.Count; i++)
                    {
                        //Daten aus Ausgewältem Item auslesen
                        int tID = ListPlaylist[i].ID;
                        string tName = ListPlaylist[i].Name;
                        var tInfoString = ListPlaylist[i].InfoString;
                        var tArtist = ListPlaylist[i].Artist;
                        string tImageSize = ListPlaylist[i].ImageSize;
                        string tImageVisibility = ListPlaylist[i].ImageVisibility;
                        string tFontFamily = ListPlaylist[i].FontFamily;
                        string tBackground = ListPlaylist[i].Background;
                        string tForeground = ListPlaylist[i].Foreground;
                        string tMargin = ListPlaylist[i].Margin;
                        bool tIsSelected = ListPlaylist[i].IsSelected;
                        string tWhatIs = ListPlaylist[i].WhatIs;
                        string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                        //Wenn ausgewähltes Item Playlists
                        if (tWhatIs == "Playlists")
                        {
                            //Wenn geöffnet wird
                            if (tIsSelected == false)
                            {
                                //Playlists als geöffnet eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", true, "Playlists", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));

                                //Farben erstellen
                                string newForeground = AlbumForegroundColor;
                                string newBackground = AlbumBackgroundColor;

                                // Ordner mit Wiedergabelisten laden
                                string[] PlaylistsFiles = file.GetFileNames("/Playlists/");

                                // Wiedergabelisten durchlaufen
                                for(int i2 = 0; i2 < PlaylistsFiles.Count(); i2++)
                                {
                                    // Name zerlegen
                                    string[] SplitPlaylistFile = Regex.Split(PlaylistsFiles[i2], ".dat");
                                    string PlaylistName = SplitPlaylistFile[0];

                                    //Variablen erstellen
                                    bool AddToList = false;

                                    //Wenn Suche vorhanden
                                    if (Search.Length > 0)
                                    {
                                        //Suche prüfen
                                        int CS = PlaylistName.ToLower().IndexOf(Search, 0);
                                        if (CS != -1)
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }
                                    }
                                    //Wenn keine Suche vorhanden
                                    else
                                    {
                                        //Angeben das Eintrag hinzugefügt wird
                                        AddToList = true;
                                    }

                                    // Wenn hinzugefügt wird
                                    if (AddToList == true)
                                    {
                                        // Playlist laden
                                        filestream = file.OpenFile("/Playlists/" + PlaylistsFiles[i2], FileMode.Open);
                                        sr = new StreamReader(filestream);
                                        string PlaylistsString = sr.ReadToEnd();
                                        filestream.Close();

                                        // Letzte Liste aufteilen
                                        string[] splitPlaylists = Regex.Split(PlaylistsString, ";ZYXXYZ;");

                                        // Gesamte Laufzeit erstellen
                                        TimeSpan Duration = new TimeSpan();

                                        // Durchlaufen und gesamte Laufzeit ermitteln
                                        for (int i3 = 0; i3 < splitPlaylists.Count() - 1; i3++)
                                        {
                                            try
                                            {
                                                // Eintrag splitten
                                                string[] splitPlaylist = Regex.Split(splitPlaylists[i3], ";XYZZYX;");
                                                string[] splitDuration = Regex.Split(splitPlaylist[4], ":");
                                                TimeSpan AddTimeSpan = new TimeSpan(Convert.ToInt32(splitDuration[0]), Convert.ToInt32(splitDuration[1]), Convert.ToInt32(splitDuration[1]));
                                                Duration = Duration + AddTimeSpan;
                                            }
                                            catch
                                            {    }
                                        }

                                        //Erweiterte Informationen erstellen
                                        string ExtendedInfoText;
                                        if ((splitPlaylists.Count() - 1) == 1)
                                        {
                                            ExtendedInfoText = (splitPlaylists.Count() - 1) + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + "  |  ";
                                        }
                                        else
                                        {
                                            ExtendedInfoText = (splitPlaylists.Count() - 1) + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + "  |  ";
                                        }
                                        ExtendedInfoText += CreateDurationString(Duration);
                                        //Wiedergabeliste in Liste schreiben
                                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, PlaylistName, i2.ToString(), "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Playlist", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                    }
                                }
                            }

                            //Wenn geschlossen wird
                            else
                            {
                                //Playlists als geschlossen eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Playlists, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", false, "Playlists", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                for (int i2 = (i + 1); i2 < ListPlaylist.Count; i2++)
                                {
                                    if (ListPlaylist[i2].WhatIs == "Playlist" | ListPlaylist[i2].WhatIs == "Playlists" | ListPlaylist[i2].WhatIs == "Song")
                                    {
                                        i++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        //Wenn ausgewähltes Item nicht Playlist ist
                        else
                        {
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                        }
                    }
                }
            }





            //Genres öffnen oder schließen
            else if (WhatToDo == "OpenCloseGenres")
            {
                //Liste durchlaufen und Genres suchen
                for (int i = 0; i < ListPlaylist.Count; i++)
                {
                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListPlaylist[i].ID;
                    string tName = ListPlaylist[i].Name;
                    string tInfoString = ListPlaylist[i].InfoString;
                    var tArtist = ListPlaylist[i].Artist;
                    string tImageSize = ListPlaylist[i].ImageSize;
                    string tImageVisibility = ListPlaylist[i].ImageVisibility;
                    string tFontFamily = ListPlaylist[i].FontFamily;
                    string tBackground = ListPlaylist[i].Background;
                    string tForeground = ListPlaylist[i].Foreground;
                    string tMargin = ListPlaylist[i].Margin;
                    bool tIsSelected = ListPlaylist[i].IsSelected;
                    string tWhatIs = ListPlaylist[i].WhatIs;
                    string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                    //Wenn ausgewähltes Item Genres
                    if (tWhatIs == "Genres")
                    {
                        //Wenn geöffnet wird
                        if (tIsSelected == false)
                        {
                            //Genres als geöffnet eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Genres, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", true, "Genres", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));

                            //Farben erstellen
                            string newForeground = AlbumForegroundColor;
                            string newBackground = AlbumBackgroundColor;

                            //Wenn CatchGenres noch nicht vorhanden
                            if (CatchGenres == "")
                            {
                                //Wenn /Catch/Genres.dat vorhanden //CatchGenres erstellen
                                if (file.FileExists("/Catch/Genres.dat"))
                                {
                                    //Catch/Genres.dat laden
                                    filestream = file.OpenFile("/Catch/Genres.dat", FileMode.Open);
                                    sr = new StreamReader(filestream);
                                    CatchGenres = sr.ReadToEnd();
                                    filestream.Close();
                                }
                            }

                            //Wenn CatchGenres vorhanden
                            if (CatchGenres != "")
                            {
                                //CatchGenres splitten
                                string[] SplitCatchGenres = Regex.Split(CatchGenres, ";ZYXXYZ;");

                                //CatchGenres in Liste übertragen
                                for (int i2 = 0; i2 < SplitCatchGenres.Count() - 1; i2++)
                                {
                                    //Einträge aufsplitten
                                    string[] SplitEntry = Regex.Split(SplitCatchGenres[i2], ";XYZZYX;");

                                    //Variablen erstellen
                                    bool AddToList = false;

                                    //Wenn Suche vorhanden
                                    if (Search.Length > 0)
                                    {
                                        //Suche prüfen
                                        int CS = SplitEntry[0].ToLower().IndexOf(Search, 0);
                                        if (CS != -1)
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }
                                    }
                                    //Wenn keine Suche vorhanden
                                    else
                                    {
                                        //Angeben das Eintrag hinzugefügt wird
                                        AddToList = true;
                                    }

                                    //Wenn Eintrag hinzugefügt wird
                                    if (AddToList == true)
                                    {
                                        //Playlist in Liste eintragen
                                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, SplitEntry[0], SplitEntry[1], "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Genre", SplitEntry[2], ExtendedInformation, SelectAndPlay, false));
                                    }
                                }
                            }

                            //Wenn CatchGenres nicht vorhanden
                            else
                            {
                                //Genres Daten laden
                                var TempGenres = mediaLibrary.Genres;
                                //Genres in Liste eintragen
                                for (int i2 = 0; i2 < TempGenres.Count; i2++)
                                {
                                    //Variablen erstellen
                                    bool AddToList = false;

                                    //Wenn Suche vorhanden
                                    if (Search.Length > 0)
                                    {
                                        //Suche prüfen
                                        int CS = TempGenres[i2].Name.ToLower().IndexOf(Search, 0);
                                        if (CS != -1)
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }
                                    }
                                    //Wenn keine Suche vorhanden
                                    else
                                    {
                                        //Angeben das Eintrag hinzugefügt wird
                                        AddToList = true;
                                    }

                                    //Erweiterte Informationen erstellen
                                    string ExtendedInfoText;
                                    if (TempGenres[i2].Songs.Count == 1)
                                    {
                                        ExtendedInfoText = TempGenres[i2].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + "  |  ";
                                    }
                                    else
                                    {
                                        ExtendedInfoText = TempGenres[i2].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + "  |  ";
                                    }
                                    TimeSpan Duration = new TimeSpan();
                                    for (int i3 = 0; i3 < TempGenres[i2].Songs.Count; i3++)
                                    {
                                        TimeSpan TempDuration = TempGenres[i2].Songs[i3].Duration;
                                        Duration = Duration + TempDuration;
                                    }
                                    ExtendedInfoText += CreateDurationString(Duration);

                                    //Wenn Eintrag erstellt wird
                                    if (AddToList == true)
                                    {
                                        //Genres in Liste schreiben
                                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, TempGenres[i2].Name, i2.ToString(), "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Genre", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                    }

                                    //CatchGenres erstellen
                                    CatchGenres += TempGenres[i2].Name + ";XYZZYX;" + i2.ToString() + ";XYZZYX;" + ExtendedInfoText + ";ZYXXYZ;";
                                }
                                //Catch/Genres.dat speichern
                                filestream = file.CreateFile("/Catch/Genres.dat");
                                sw = new StreamWriter(filestream);
                                sw.Write(CatchGenres);
                                sw.Flush();
                                filestream.Close();
                            }
                        }

                        //Wenn geschlossen wird
                        else
                        {
                            //Genres als geschlossen eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Genres, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", false, "Genres", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            for (int i2 = (i + 1); i2 < ListPlaylist.Count; i2++)
                            {
                                if (ListPlaylist[i2].WhatIs == "Genres" | ListPlaylist[i2].WhatIs == "Genre" | ListPlaylist[i2].WhatIs == "Song")
                                {
                                    i++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    //Wenn ausgewähltes Item nicht Genres ist
                    else
                    {
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                }
            }





            //Albums öffnen oder schließen
            else if (WhatToDo == "OpenCloseAlbums")
            {
                //Liste durchlaufen und Albums suchen
                for (int i = 0; i < ListPlaylist.Count; i++)
                {
                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListPlaylist[i].ID;
                    string tName = ListPlaylist[i].Name;
                    string tInfoString = ListPlaylist[i].InfoString;
                    string tArtist = ListPlaylist[i].Artist;
                    string tImageSize = ListPlaylist[i].ImageSize;
                    string tImageVisibility = ListPlaylist[i].ImageVisibility;
                    string tFontFamily = ListPlaylist[i].FontFamily;
                    string tBackground = ListPlaylist[i].Background;
                    string tForeground = ListPlaylist[i].Foreground;
                    string tMargin = ListPlaylist[i].Margin;
                    bool tIsSelected = ListPlaylist[i].IsSelected;
                    string tWhatIs = ListPlaylist[i].WhatIs;
                    string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                    //Wenn ausgewähltes Item Albums
                    if (tWhatIs == "Albums")
                    {
                        //Wenn geöffnet wird
                        if (tIsSelected == false)
                        {
                            //Albums als geöffnet eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Albums, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", true, "Albums", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));

                            //Farben erstellen
                            string newForeground = AlbumForegroundColor;
                            string newBackground = AlbumBackgroundColor;

                            //Wenn CatchAlbums noch nicht vorhanden
                            if (CatchAlbums == "")
                            {
                                //Wenn /Catch/Albums.dat vorhanden //CatchAlbums erstellen
                                if (file.FileExists("/Catch/Albums.dat"))
                                {
                                    //Catch/Albums.dat laden
                                    filestream = file.OpenFile("/Catch/Albums.dat", FileMode.Open);
                                    sr = new StreamReader(filestream);
                                    CatchAlbums = sr.ReadToEnd();
                                    filestream.Close();
                                }
                            }

                            //Wenn CatchAlbums vorhanden
                            if (CatchAlbums != "")
                            {
                                //CatchAlbums splitten
                                string[] SplitCatchAlbums = Regex.Split(CatchAlbums, ";ZYXXYZ;");

                                //CatchAlbums in Liste übertragen
                                for (int i2 = 0; i2 < SplitCatchAlbums.Count() - 1; i2++)
                                {
                                    //Einträge aufsplitten
                                    string[] SplitEntry = Regex.Split(SplitCatchAlbums[i2], ";XYZZYX;");

                                    //Variablen erstellen
                                    bool AddToList = false;

                                    //Wenn Suche vorhanden
                                    if (Search.Length > 0)
                                    {
                                        //Suche prüfen
                                        int CS = SplitEntry[0].ToLower().IndexOf(Search, 0);
                                        if (CS != -1)
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }
                                    }
                                    //Wenn keine Suche vorhanden
                                    else
                                    {
                                        //Angeben das Eintrag hinzugefügt wird
                                        AddToList = true;
                                    }

                                    //Wenn Eintrag hinzugefügt wird
                                    if (AddToList == true)
                                    {
                                        //Playlist in Liste eintragen
                                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, SplitEntry[0], SplitEntry[1], "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Album", SplitEntry[2], ExtendedInformation, SelectAndPlay, false));
                                    }
                                }
                            }

                            //Wenn CatchAlbums nicht vorhanden
                            else
                            {
                                //Albums Daten laden
                                var TempAlbums = mediaLibrary.Albums;
                                //Albums in Liste eintragen
                                for (int i2 = 0; i2 < TempAlbums.Count; i2++)
                                {
                                    //Variablen erstellen
                                    bool AddToList = false;

                                    //Wenn Suche vorhanden
                                    if (Search.Length > 0)
                                    {
                                        //Suche prüfen
                                        int CS = TempAlbums[i2].Name.ToLower().IndexOf(Search, 0);
                                        if (CS != -1)
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }
                                    }
                                    //Wenn keine Suche vorhanden
                                    else
                                    {
                                        //Angeben das Eintrag hinzugefügt wird
                                        AddToList = true;
                                    }

                                    //Erweiterte Informationen erstellen
                                    string ExtendedInfoText;
                                    if (TempAlbums[i2].Songs.Count == 1)
                                    {
                                        ExtendedInfoText = TempAlbums[i2].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + "  |  ";
                                    }
                                    else
                                    {
                                        ExtendedInfoText = TempAlbums[i2].Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + "  |  ";
                                    }
                                    TimeSpan Duration = new TimeSpan();
                                    for (int i3 = 0; i3 < TempAlbums[i2].Songs.Count; i3++)
                                    {
                                        TimeSpan TempDuration = TempAlbums[i2].Songs[i3].Duration;
                                        Duration = Duration + TempDuration;
                                    }
                                    ExtendedInfoText += CreateDurationString(Duration);

                                    //Wenn Eintrag erstellt wird
                                    if (AddToList == true)
                                    {
                                        //Albums in Liste schreiben
                                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, TempAlbums[i2].Name, i2.ToString(), "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "24,24,0,0", false, "Album", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                    }

                                    //CatchAlbums erstellen
                                    CatchAlbums += TempAlbums[i2].Name + ";XYZZYX;" + i2.ToString() + ";XYZZYX;" + ExtendedInfoText + ";ZYXXYZ;";
                                }
                                //Catch/Albums.dat speichern
                                filestream = file.CreateFile("/Catch/Albums.dat");
                                sw = new StreamWriter(filestream);
                                sw.Write(CatchAlbums);
                                sw.Flush();
                                filestream.Close();
                            }
                        }

                        //Wenn geschlossen wird
                        else
                        {
                            //Albums als geschlossen eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, MyMusicPlayer.Resources.AppResources.Z001_Albums, "", "", ImageSize, "Collapsed", "Segoe WP Semibold", tBackground, tForeground, "0,24,0,0", false, "Albums", tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            for (int i2 = (i + 1); i2 < ListPlaylist.Count; i2++)
                            {
                                if (ListPlaylist[i2].WhatIs == "Albums" | ListPlaylist[i2].WhatIs == "Album" | ListPlaylist[i2].WhatIs == "Song")
                                {
                                    i++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    //Wenn ausgewähltes Item nicht Albums ist
                    else
                    {
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                }
            }





            //Playlist öffnen oder schließen
            else if (WhatToDo == "OpenClosePlaylist")
            {
                //Liste durchlaufen und Playlist suchen
                for (int i = 0; i < ListPlaylist.Count; i++)
                {
                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListPlaylist[i].ID;
                    string tName = ListPlaylist[i].Name;
                    string tInfoString = ListPlaylist[i].InfoString;
                    var tArtist = ListPlaylist[i].Artist;
                    string tImageSize = ListPlaylist[i].ImageSize;
                    string tImageVisibility = ListPlaylist[i].ImageVisibility;
                    string tFontFamily = ListPlaylist[i].FontFamily;
                    string tBackground = ListPlaylist[i].Background;
                    string tForeground = ListPlaylist[i].Foreground;
                    string tMargin = ListPlaylist[i].Margin;
                    bool tIsSelected = ListPlaylist[i].IsSelected;
                    string tWhatIs = ListPlaylist[i].WhatIs;
                    string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                    //Versuchen ID zu bekommen
                    int tempID = -1;
                    try
                    {
                        tempID = Convert.ToInt32(tInfoString);
                    }
                    catch
                    {
                    }

                    //Wenn ausgewähltes Item Playlist
                    if (tWhatIs == "Playlist" & ID == tempID)
                    {
                        // Wenn Select and Play nicht aktiv ist
                        if (SelectAndPlay == false)
                        {
                            //Wenn geöffnet wird
                            if (tIsSelected == false)
                            {
                                //Playlist als geöffnet eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, true, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                //Playlist Daten laden
                                var TempSongs = mediaLibrary.Playlists[ID].Songs;
                                //Playlists in Liste eintragen
                                for (int i2 = 0; i2 < TempSongs.Count; i2++)
                                {
                                    //Erweiterte Informationen erstellen
                                    string ExtendedInfoText = CreateDurationString(TempSongs[i2].Duration);
                                    //Farben erstellen
                                    string newForeground = SongForegroundColor;
                                    string newBackground = SongBackgroundColor;
                                    //Playlist in Liste schreiben
                                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, TempSongs[i2].Name, "Playlist;" + ID + ";" + i2.ToString(), "", ImageSize, "Visibile", "Segoe WP Light", newBackground, newForeground, "48,24,0,0", false, "Song", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                }
                            }
                            //Wenn geschlossen wird
                            else
                            {
                                //Playlists als geschlossen eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, false, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                //Playlist Daten laden und i erhöhen
                                i = i + mediaLibrary.Playlists[ID].Songs.Count;
                            }
                        }
                        // Wenn Select and Play aktiv ist
                        else if (SelectAndPlay == true)
                        {
                            // Playliste anhand der ID laden
                            string[] PlaylistFiles = file.GetFileNames("/Playlists/");
                            // Playliste Daten laden
                            filestream = file.OpenFile("/Playlists/" + PlaylistFiles[tempID], FileMode.Open);
                            sr = new StreamReader(filestream);
                            string PlaylistString = sr.ReadToEnd();
                            filestream.Close();
                            // Playlist aufteilen
                            string[] splitPlaylistString = Regex.Split(PlaylistString, ";ZYXXYZ;");
                            //Wenn geöffnet wird
                            if (tIsSelected == false)
                            {
                                //Playlist als geöffnet eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, true, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                // Durchlaufen
                                for (int i2 = 0; i2 < splitPlaylistString.Count() - 1; i2++)
                                {
                                    // Eintrag splitten
                                    string[] splitPlaylist = Regex.Split(splitPlaylistString[i2], ";XYZZYX;");
                                    // Duration in ExtendedInfoText schreiben
                                    splitPlaylist[4] = splitPlaylist[4].Replace(".", "XXX");
                                    string[] splitDuration = Regex.Split(splitPlaylist[4], "XXX");
                                    string ExtendedInfoText = splitDuration[0];
                                    //Playlist in Liste schreiben
                                    TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, splitPlaylist[2], "Playlist;" + ID + ";" + i2.ToString(), "", ImageSize, "Visibile", "Segoe WP Light", SongBackgroundColor, SongForegroundColor, "48,24,0,0", false, "Song", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                }
                            }
                            //Wenn geschlossen wird
                            else
                            {
                                //Playlists als geschlossen eintragen
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, false, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                //Playlist Daten laden und i erhöhen
                                i = i + (splitPlaylistString.Count() - 1);
                            }
                        }
                    }
                    //Wenn ausgewähltes Item nicht Playlist ist
                    else
                    {
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false)); 
                    }
                }
            }


            //Genre öffnen oder schließen
            else if (WhatToDo == "OpenCloseGenre")
            {
                //Liste durchlaufen und Genre suchen
                for (int i = 0; i < ListPlaylist.Count; i++)
                {
                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListPlaylist[i].ID;
                    string tName = ListPlaylist[i].Name;
                    string tInfoString = ListPlaylist[i].InfoString;
                    string tArtist = ListPlaylist[i].Artist;
                    string tImageSize = ListPlaylist[i].ImageSize;
                    string tImageVisibility = ListPlaylist[i].ImageVisibility;
                    string tFontFamily = ListPlaylist[i].FontFamily;
                    string tBackground = ListPlaylist[i].Background;
                    string tForeground = ListPlaylist[i].Foreground;
                    string tMargin = ListPlaylist[i].Margin;
                    bool tIsSelected = ListPlaylist[i].IsSelected;
                    string tWhatIs = ListPlaylist[i].WhatIs;
                    string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                    //Versuchen ID zu bekommen
                    int tempID = -1;
                    try
                    {
                        tempID = Convert.ToInt32(tInfoString);
                    }
                    catch
                    {
                    }

                    //Wenn ausgewähltes Item Genre
                    if (tWhatIs == "Genre" & ID == tempID)
                    {
                        //Wenn geöffnet wird
                        if (tIsSelected == false)
                        {
                            //Genre als geöffnet eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, true, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            //Genre Daten laden
                            var TempSongs = mediaLibrary.Genres[ID].Songs;
                            //Songs in Liste eintragen
                            for (int i2 = 0; i2 < TempSongs.Count; i2++)
                            {
                                //Erweiterte Informationen erstellen
                                string ExtendedInfoText = CreateDurationString(TempSongs[i2].Duration);
                                //Farben erstellen
                                string newForeground = SongForegroundColor;
                                string newBackground = SongBackgroundColor;
                                //Genre in Liste schreiben
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, TempSongs[i2].Name, "Genre;" + ID + ";" + i2.ToString(), "", ImageSize, "Visibile", "Segoe WP Light", newBackground, newForeground, "48,24,0,0", false, "Song", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            }
                        }
                        //Wenn geschlossen wird 
                        else
                        {
                            //Genre als geschlossen eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, false, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            //Playlist Daten laden und i erhöhen
                            i = i + mediaLibrary.Genres[ID].Songs.Count;
                        }
                    }
                    //Wenn ausgewähltes Item nicht Genre ist
                    else
                    {
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay,false));
                    }
                }
            }


            //Album öffnen oder schließen
            else if (WhatToDo == "OpenCloseAlbum")
            {
                //Liste durchlaufen und Album suchen
                for (int i = 0; i < ListPlaylist.Count; i++)
                {
                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListPlaylist[i].ID;
                    string tName = ListPlaylist[i].Name;
                    string tInfoString = ListPlaylist[i].InfoString;
                    var tArtist = ListPlaylist[i].Artist;
                    string tImageSize = ListPlaylist[i].ImageSize;
                    string tImageVisibility = ListPlaylist[i].ImageVisibility;
                    string tFontFamily = ListPlaylist[i].FontFamily;
                    string tBackground = ListPlaylist[i].Background;
                    string tForeground = ListPlaylist[i].Foreground;
                    string tMargin = ListPlaylist[i].Margin;
                    bool tIsSelected = ListPlaylist[i].IsSelected;
                    string tWhatIs = ListPlaylist[i].WhatIs;
                    string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                    //Versuchen ID zu bekommen
                    int tempID = -1;
                    try
                    {
                        tempID = Convert.ToInt32(tInfoString);
                    }
                    catch
                    {
                    }

                    //Wenn ausgewähltes Item Album
                    if (tWhatIs == "Album" & ID == tempID)
                    {
                        //Wenn geöffnet wird
                        if (tIsSelected == false)
                        {
                            //Album als geöffnet eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, true, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            //Album Daten laden
                            var TempSongs = mediaLibrary.Albums[ID].Songs;
                            //Songs in Liste eintragen
                            for (int i2 = 0; i2 < TempSongs.Count; i2++)
                            {
                                //Erweiterte Informationen erstellen
                                string ExtendedInfoText = CreateDurationString(TempSongs[i2].Duration);
                                //Farben erstellen
                                string newForeground = SongForegroundColor;
                                string newBackground = SongBackgroundColor;
                                //Album in Liste schreiben
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, TempSongs[i2].Name, "Album;" + ID + ";" + i2.ToString(), "", ImageSize, "Visibile", "Segoe WP Light", newBackground, newForeground, "48,24,0,0", false, "Song", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            }
                        }
                        //Wenn geschlossen wird
                        else
                        {
                            //Album als geschlossen eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, false, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            //Album Daten laden und i erhöhen
                            i = i + mediaLibrary.Albums[ID].Songs.Count;
                        }
                    }
                    //Wenn ausgewähltes Item nicht Album ist
                    else
                    {
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                }
            }

            //Playlist öffnen oder schließen
            else if (WhatToDo == "OpenCloseAllSongs")
            {
                //Liste durchlaufen und AllSongs suchen
                for (int i = 0; i < ListPlaylist.Count; i++)
                {
                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListPlaylist[i].ID;
                    string tName = ListPlaylist[i].Name;
                    string tInfoString = ListPlaylist[i].InfoString;
                    var tArtist = ListPlaylist[i].Artist;
                    string tImageSize = ListPlaylist[i].ImageSize;
                    string tImageVisibility = ListPlaylist[i].ImageVisibility;
                    string tFontFamily = ListPlaylist[i].FontFamily;
                    string tBackground = ListPlaylist[i].Background;
                    string tForeground = ListPlaylist[i].Foreground;
                    string tMargin = ListPlaylist[i].Margin;
                    bool tIsSelected = ListPlaylist[i].IsSelected;
                    string tWhatIs = ListPlaylist[i].WhatIs;
                    string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                    //Wenn ausgewähltes Item Playlist
                    if (tWhatIs == "AllSongs")
                    {
                        //Wenn geöffnet wird
                        if (tIsSelected == false)
                        {
                            //Alle Lieder als geöffnet eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, true, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            
                            //Farben erstellen
                            string newForeground = AlbumForegroundColor;
                            string newBackground = AlbumBackgroundColor;

                            //Wenn CatchAllSongs noch nicht vorhanden
                            if (CatchAllSongs == "")
                            {
                                //Wenn /Catch/AllSongs.dat vorhanden //CatchAllSongs erstellen
                                if (file.FileExists("/Catch/AllSongs.dat"))
                                {
                                    //Catch/AllSongs.dat laden
                                    filestream = file.OpenFile("/Catch/AllSongs.dat", FileMode.Open);
                                    sr = new StreamReader(filestream);
                                    CatchAllSongs = sr.ReadToEnd();
                                    filestream.Close();
                                }
                            }

                            //Wenn CatchAllSongs vorhanden
                            if (CatchAllSongs != "")
                            {
                                //CatchAllSongs splitten
                                string[] SplitCatchAllSongs = Regex.Split(CatchAllSongs, ";ZYXXYZ;");

                                //CatchAllSongs in Liste übertragen
                                for (int i2 = 0; i2 < SplitCatchAllSongs.Count() - 1; i2++)
                                {
                                    //Einträge aufsplitten
                                    string[] SplitEntry = Regex.Split(SplitCatchAllSongs[i2], ";XYZZYX;");

                                    //Variablen erstellen
                                    bool AddToList = false;

                                    //Wenn Suche vorhanden
                                    if (Search.Length > 0)
                                    {
                                        //Suche prüfen
                                        int CS = SplitEntry[0].ToLower().IndexOf(Search, 0);
                                        if (CS != -1)
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }
                                    }
                                    //Wenn keine Suche vorhanden
                                    else
                                    {
                                        //Angeben das Eintrag hinzugefügt wird
                                        AddToList = true;
                                    }

                                    //Wenn Eintrag hinzugefügt wird
                                    if (AddToList == true)
                                    {
                                        //Playlist in Liste eintragen
                                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, SplitEntry[0], SplitEntry[1], "", ImageSize, "Visibile", "Segoe WP", newBackground, newForeground, "48,24,0,0", false, "Song", SplitEntry[2], ExtendedInformation, SelectAndPlay, false));
                                    }
                                }
                            }

                            //Wenn CatchAllSongs nicht vorhanden
                            else
                            {
                                //AllSongs Daten laden
                                var TempAllSongs = mediaLibrary.Songs;
                                //AllSongsin Liste eintragen
                                for (int i2 = 0; i2 < TempAllSongs.Count; i2++)
                                {
                                    //Variablen erstellen
                                    bool AddToList = false;

                                    //Wenn Suche vorhanden
                                    if (Search.Length > 0)
                                    {
                                        //Suche prüfen
                                        int CS = TempAllSongs[i2].Name.ToLower().IndexOf(Search, 0);
                                        if (CS != -1)
                                        {
                                            //Angeben das Eintrag hinzugefügt wird
                                            AddToList = true;
                                        }
                                    }
                                    //Wenn keine Suche vorhanden
                                    else
                                    {
                                        //Angeben das Eintrag hinzugefügt wird
                                        AddToList = true;
                                    }

                                    //Erweiterte Informationen erstellen
                                    string ExtendedInfoText = CreateDurationString(TempAllSongs[i2].Duration);

                                    //Wenn Eintrag erstellt wird
                                    if (AddToList == true)
                                    {
                                        //Albums in Liste schreiben
                                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, TempAllSongs[i2].Name, "AllSongs;" + i2.ToString() + ";" + i2.ToString(), "", ImageSize, "Visibile", "Segoe WP Light", newBackground, newForeground, "48,24,0,0", false, "Song", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                                    }

                                    //CatchAllSongs erstellen
                                    CatchAllSongs += TempAllSongs[i2].Name + ";XYZZYX;AllSongs;" + i2.ToString() + ";" + i2.ToString() + ";XYZZYX;" + ExtendedInfoText + ";ZYXXYZ;";
                                }
                                //Catch/AllSongs.dat speichern
                                filestream = file.CreateFile("/Catch/AllSongs.dat");
                                sw = new StreamWriter(filestream);
                                sw.Write(CatchAllSongs);
                                sw.Flush();
                                filestream.Close();
                            }
                        }

                        //Wenn geschlossen wird
                        else
                        {
                            //Alle Songs als geschlossen eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, false, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            //Songs Daten laden und i erhöhen
                            i = i + mediaLibrary.Songs.Count;
                        }
                    }
                    //Wenn ausgewähltes Item nicht Playlist ist
                    else
                    {
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                }
            }

            //LastPlayback öffnen oder schließen
            else if (WhatToDo == "OpenCloseLastPlaybacks")
            {
                //Liste durchlaufen und AllSongs suchen
                for (int i = 0; i < ListPlaylist.Count; i++)
                {
                    //Daten aus Ausgewältem Item auslesen
                    int tID = ListPlaylist[i].ID;
                    string tName = ListPlaylist[i].Name;
                    string tInfoString = ListPlaylist[i].InfoString;
                    string tArtist = ListPlaylist[i].Artist;
                    string tImageSize = ListPlaylist[i].ImageSize;
                    string tImageVisibility = ListPlaylist[i].ImageVisibility;
                    string tFontFamily = ListPlaylist[i].FontFamily;
                    string tBackground = ListPlaylist[i].Background;
                    string tForeground = ListPlaylist[i].Foreground;
                    string tMargin = ListPlaylist[i].Margin;
                    bool tIsSelected = ListPlaylist[i].IsSelected;
                    string tWhatIs = ListPlaylist[i].WhatIs;
                    string tExtendedInfoText = ListPlaylist[i].ExtendedInfoText;

                    //Wenn ausgewähltes Item LastPlaybacks ist
                    if (tWhatIs == "LastPlaybacks")
                    {
                        //Wenn geöffnet wird
                        if (tIsSelected == false)
                        {
                            //LastPlaybacks als geöffnet eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, true, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            string[] LastPlaybackStringSplit = Regex.Split(LastPlaybackString, ";;;;;");
                            LastPlaybacksCount = (LastPlaybackStringSplit.Count() - 1);
                            //Alle LastPlaybacks in Liste eintragen
                            for (int i2 = (LastPlaybackStringSplit.Count() - 2); i2 > -1; i2--)
                            {
                                //LastPlayback zerlegen
                                string[] LastPlaybackSplit = Regex.Split(LastPlaybackStringSplit[i2], ";;;");
                                //Erweiterte Informationen erstellen
                                string ExtendedInfoText = LastPlaybackSplit[7];
                                string AllInfos = LastPlaybackSplit[0] + ";;;" + LastPlaybackSplit[1] + ";;;" + LastPlaybackSplit[2] + ";;;" + LastPlaybackSplit[3] + ";;;" + LastPlaybackSplit[4] + ";;;" + LastPlaybackSplit[5] + ";;;" + LastPlaybackSplit[6] + ";;;" + LastPlaybackSplit[7];
                                //Farben erstellen
                                string newForeground = AlbumForegroundColor;
                                string newBackground = AlbumBackgroundColor;
                                //Album in Liste schreiben
                                TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, LastPlaybackSplit[0], AllInfos, "", ImageSize, "Visibile", "Segoe WP Light", newBackground, newForeground, "24,24,0,0", false, "LastPlayback", ExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            }
                        }
                        //Wenn geschlossen wird
                        else
                        {
                            //Alle Songs als geschlossen eintragen
                            TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, false, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                            //Songs Daten laden und i erhöhen
                            i = i + LastPlaybacksCount;
                        }
                    }
                    //Wenn ausgewähltes Item nicht Playlist ist
                    else
                    {
                        TempListPlaylist.Add(new ClassMedia(TempListPlaylist.Count, tName, tInfoString, tArtist, ImageSize, tImageVisibility, tFontFamily, tBackground, tForeground, tMargin, tIsSelected, tWhatIs, tExtendedInfoText, ExtendedInformation, SelectAndPlay, false));
                    }
                }
            }



            // Wenn Eintrag nur sortiert wird
            else if (WhatToDo == "Sort")
            {
               // Tempplaylist aus Playlist erstellen
                for (int i = 0; i < ListPlaylist.Count(); i++)
                {
                    // Eintrag von TempPlaylist in Playlist übertragen
                    TempListPlaylist.Add(ListPlaylist[i]);
                }
            }



            // ListPlaylist aus TempListPlaylist erstellen und prüfen welche Lieder vorhanden
            ListPlaylist.Clear();
            for (int i = 0; i < TempListPlaylist.Count; i++)
            {
                // Wenn Select and Play aktiv
                if (SelectAndPlay == true)
                {
                    // Wenn ID anders als -1, werden die Playlisten neu erstellt
                    if (TempListPlaylist[i].WhatIs == "Playlists")
                    {
                        // Alle weiteren Einträge 
                    }

                    // Wenn ausgewählter Eintrag ein Genre ist
                    else if (TempListPlaylist[i].WhatIs == "Genre")
                    {
                        // Erweiterte Informationen laden
                        try
                        {
                            // Erweiterte Informationen zerlegen
                            string[] splitExtendedInfos = Regex.Split(TempListPlaylist[i].ExtendedInfoText, " ");
                            // Anzahl der Lieder daraus raus lesen
                            int GenreSongs = Convert.ToInt32(splitExtendedInfos[0]);
                            int PlaylistGenreSongs = 0;
                            // Prüfen wie viele Songs aus Genre vorhanden
                            for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                            {
                                // Prüfen ob Song ausgewählt ist
                                if (TempListPlaylist[i].Name == ListActivPlaylist[i2].Genre)
                                {
                                    PlaylistGenreSongs++;
                                }
                            }
                            // Wenn alle Songs des Genres vorhanden
                            if (GenreSongs == PlaylistGenreSongs)
                            {
                                TempListPlaylist[i].Background = SelectedBackgroundColor;
                                TempListPlaylist[i].Foreground = SelectedForegroundColor;
                            }
                            // Wenn nicht alle Songs des Genres vorhanden
                            else
                            {
                                TempListPlaylist[i].Background = AlbumBackgroundColor;
                                TempListPlaylist[i].Foreground = AlbumForegroundColor;
                            }
                        }
                        catch 
                        { }
                    }

                    // Wenn ausgewählter Eintrag ein Album ist
                    else if (TempListPlaylist[i].WhatIs == "Album")
                    {
                        // Erweiterte Informationen laden
                        try
                        {
                            // Erweiterte Informationen zerlegen
                            string[] splitExtendedInfos = Regex.Split(TempListPlaylist[i].ExtendedInfoText, " ");
                            // Anzahl der Lieder daraus raus lesen
                            int AlbumSongs = Convert.ToInt32(splitExtendedInfos[0]);
                            int PlaylistAlbumSongs = 0;
                            // Prüfen wie viele Songs aus Genre vorhanden
                            for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                            {
                                // Prüfen ob Song ausgewählt ist
                                if (TempListPlaylist[i].Name == ListActivPlaylist[i2].Album)
                                {
                                    PlaylistAlbumSongs++;
                                }
                            }
                            // Wenn alle Songs des Albums vorhanden
                            if (AlbumSongs == PlaylistAlbumSongs)
                            {
                                TempListPlaylist[i].Background = SelectedBackgroundColor;
                                TempListPlaylist[i].Foreground = SelectedForegroundColor;
                            }
                            // Wenn nicht alle Songs des Albums vorhanden
                            else
                            {
                                TempListPlaylist[i].Background = AlbumBackgroundColor;
                                TempListPlaylist[i].Foreground = AlbumForegroundColor;
                            }
                        }
                        catch
                        { }
                    }

                    // Wenn ausgewählter Eintrag ein Alle Lieder ist
                    else if (TempListPlaylist[i].WhatIs == "AllSongs")
                    {
                        // Wenn Alle Lieder ausgewählt sind
                        if (ListActivPlaylist.Count() == mediaLibrary.Songs.Count)
                        {
                            TempListPlaylist[i].Background = SelectedBackgroundColor;
                            TempListPlaylist[i].Foreground = SelectedForegroundColor;
                        }
                        // Wenn nicht alle Lieder ausgewählt sind
                        else
                        {
                            TempListPlaylist[i].Background = ArtistBackgroundColor;
                            TempListPlaylist[i].Foreground = ArtistForegroundColor;
                        }
                    }

                    // Wenn ausgewählter Eintrag ein Song ist
                    else if (TempListPlaylist[i].WhatIs == "Song")
                    {
                        // Prüfvariable
                        bool SongExists = false;
                        // Prüfen on Song in Aktueller Anspielliste vorhanden
                        for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                        {
                            // Prüfen ob Song ausgewählt ist
                            if (TempListPlaylist[i].Name == ListActivPlaylist[i2].Song)
                            {
                                SongExists = true;
                                break;
                            }
                        }
                        // Wenn Songs bereits in Wiedergabeliste
                        if (SongExists == true)
                        {
                            TempListPlaylist[i].Background = SelectedBackgroundColor;
                            TempListPlaylist[i].Foreground = SelectedForegroundColor;
                        }
                        // Wenn Song nicht in der aktuellen Wiedergabeliste
                        else
                        {
                            TempListPlaylist[i].Background = SongBackgroundColor;
                            TempListPlaylist[i].Foreground = SongForegroundColor;
                        }
                    }
                }
                // Eintrag von TempPlaylist in Playlist übertragen
                ListPlaylist.Add(TempListPlaylist[i]);
            }
            //Liste verlinken
            LBPlaylists.ItemsSource = ListPlaylist;
        }
        //---------------------------------------------------------------------------------------------------------





        //Auswahl aus Playlists
        //---------------------------------------------------------------------------------------------------------
        //Variabeln
        string LBPlaylists_LastClicked = "none";

        //Wenn auswahl geändert //Kommt nach der Auswahl der Buttons
        private void LBPlaylists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Prüfen ob ausgeführt wird
            if (LBPlaylists.SelectedIndex != -1)
            {
                int tIndex = LBPlaylists.SelectedIndex;

                //Daten aus Ausgewältem Item auslesen
                int tID = ListPlaylist[tIndex].ID;
                string tName = ListPlaylist[tIndex].Name;
                string tInfoString = ListPlaylist[tIndex].InfoString;
                var tArtist = ListPlaylist[tIndex].Artist;
                string tImageSize = ListPlaylist[tIndex].ImageSize;
                string tImageVisibility = ListPlaylist[tIndex].ImageVisibility;
                string tFontFamily = ListPlaylist[tIndex].FontFamily;
                string tBackground = ListPlaylist[tIndex].Background;
                string tForeground = ListPlaylist[tIndex].Foreground;
                string tMargin = ListPlaylist[tIndex].Margin;
                bool tIsSelected = ListPlaylist[tIndex].IsSelected;
                string tWhatIs = ListPlaylist[tIndex].WhatIs;
                string tExtendedInfoText = ListPlaylist[tIndex].ExtendedInfoText;


                // Wenn Play ausgewählt wurde // Button 1
                if (LBPlaylists_LastClicked == "Play")
                {
                    //Wenn Playlists gedrückt wurde
                    if (tWhatIs == "Playlist")
                    {
                        // Wenn Select and Play nicht aktiv
                        if (SelectAndPlay == false)
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            //Playlist ID ermitteln
                            int TempPlaylistID = Convert.ToInt32(tInfoString);
                            //Playliste laden
                            var SongsToPlay = mediaLibrary.Playlists[TempPlaylistID].Songs;
                            //Songs versuchen abzuspielen
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Play(SongsToPlay);
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                //Extended info erstellen
                                string ExtendedInfoText = "";
                                if (mediaLibrary.Playlists[TempPlaylistID].Songs.Count() == 1)
                                {
                                    ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + mediaLibrary.Playlists[TempPlaylistID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                }
                                else
                                {
                                    ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + mediaLibrary.Playlists[TempPlaylistID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                }
                                LastPlaybackString += mediaLibrary.Playlists[TempPlaylistID].Name + ";;;Playlist;;;" + mediaLibrary.Playlists[TempPlaylistID].Name + ";;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                CreateLastPlayback();
                            }
                        }

                        // Wenn Select and Play aktiv
                        else if (SelectAndPlay == true)
                        {
                            // Playlist ID ermitteln
                            int TempPlaylistID = Convert.ToInt32(tInfoString);
                            // Namen der Wiedergabeliste ermitteln
                            string[] AllPlaylists = file.GetFileNames("/Playlists/");
                            string PlaylistFileName = AllPlaylists[TempPlaylistID];
                            string[] splitFileName = Regex.Split(PlaylistFileName, ".");
                            string PlaylistName = splitFileName[0];
                            // Abfrage ob Playliste gelöscht werden soll
                            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ005_DeletePlaylist + " " + PlaylistName, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            {
                                // Wiedergabeliste löschen
                                try
                                {
                                    // Wiedergabeliste löschen
                                    file.DeleteFile("/Playlists/" + PlaylistFileName);
                                }
                                catch
                                { }
                                // Wiedergabelisten schließen
                                List_Playlists("OpenClosePlaylists", -1);
                                // Wiedergabelisten öffnen
                                List_Playlists("OpenClosePlaylists", -1);
                                // Liste neu erstellen
                                //List_Playlists("Sort", 1);
                            }
                        }

                    }



                    // Genre abspielen, wenn Genre ausgewählt wurde und Select and Play nicht aktiv ist
                    else if (tWhatIs == "Genre" & SelectAndPlay == false)
                    {
                        //Variabeln erstellen
                        bool PlayFail = false;
                        //Genres ID ermitteln
                        int TempGenresID = Convert.ToInt32(tInfoString);
                        //Playliste laden
                        var SongsToPlay = mediaLibrary.Genres[TempGenresID].Songs;
                        //Songs versuchen abzuspielen
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.Play(SongsToPlay);
                        }
                        catch
                        {
                            PlayFail = true;
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                        }
                        //In LastPlayback schreiben
                        if (PlayFail == false)
                        {
                            //Extended info erstellen
                            string ExtendedInfoText = "";
                            if (mediaLibrary.Genres[TempGenresID].Songs.Count() == 1)
                            {
                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + mediaLibrary.Genres[TempGenresID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                            }
                            else
                            {
                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + mediaLibrary.Genres[TempGenresID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                            }
                            LastPlaybackString += mediaLibrary.Genres[TempGenresID].Name + ";;;Genre;;;" + mediaLibrary.Genres[TempGenresID].Name + ";;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                            CreateLastPlayback();
                        }
                    }
                    // Genre zur aktuellen Wiedergabe hinzufügen, wenn Genre ausgewählt wurde und Select and Play aktiv ist
                    else if (tWhatIs == "Genre" & SelectAndPlay == true)
                    {
                        //Genres ID ermitteln
                        int TempGenresID = Convert.ToInt32(tInfoString);
                        // Genre Name laden
                        string GenreName = mediaLibrary.Genres[TempGenresID].Name;
                        // Gesamte anzahl der Songs des Genres laden
                        int GenreSongs = mediaLibrary.Genres[TempGenresID].Songs.Count();
                        int TempGenreSongs = 0;
                        // Benachrichtigung erstellen
                        string TempNote = "";
                        // Aktuelle Wiedergabe durchlaufen
                        for (int i = 0; i < ListActivPlaylist.Count(); i++)
                        {
                            // Prüfen ob lied aus Genre vorhanden
                            if (GenreName == ListActivPlaylist[i].Genre)
                            {
                                // Anzahl erhöhen
                                TempGenreSongs++;
                            }
                        }
                        // Wenn noch nicht alle Songs vorhanden
                        if (TempGenreSongs != GenreSongs)
                        {
                            // Anzahl hinzugrfügter Songs
                            int TempSongsAdded = 0;
                            // Genre durchlaufen
                            for (int i = 0; i < GenreSongs; i++)
                            {
                                // Prüfvariable
                                bool SongExists = false;
                                // Lied Daten laden
                                string SongArtist = mediaLibrary.Genres[TempGenresID].Songs[i].Artist.Name;
                                string SongAlbum = mediaLibrary.Genres[TempGenresID].Songs[i].Album.Name;
                                string SongName = mediaLibrary.Genres[TempGenresID].Songs[i].Name;
                                // Aktuelle Wiedergabe durchlaufen
                                for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                {
                                    // Prüfen ob Song bereits vorhanden
                                    if (SongArtist == ListActivPlaylist[i2].Artist & SongName == ListActivPlaylist[i2].Song & SongAlbum == ListActivPlaylist[i2].Album)
                                    {
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    // Song hinzufügen
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, mediaLibrary.Genres[TempGenresID].Songs[i].Genre.Name, mediaLibrary.Genres[TempGenresID].Songs[i].Duration.ToString()));
                                    // Anzahl hinzugefügter Songs erhöhen
                                    TempSongsAdded++;
                                }
                            }
                            // Benachrichtigung erstellen
                            if (TempSongsAdded == 1)
                            {
                                TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | "; 
                            }
                            else
                            {
                                TempNote = "+ " + TempSongsAdded + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                            }
                            if(ListActivPlaylist.Count() == 1)
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                            }
                            else
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                            }
                        }
                        // Wenn bereits alle Songs vorhanden
                        else
                        {
                            // Anzahl gelöschter Songs
                            int TempSongsRemoved = 0;
                            // Aktuelle Wiedergabe durchlaufen
                            for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                            {
                                // Wenn Eintrag, ausgewähltes Genre hat
                                if (ListActivPlaylist[i].Genre == GenreName)
                                {
                                    // Eintrag löschen
                                    ListActivPlaylist.RemoveAt(i);
                                    // Anzahl gelöschter einträge verringern
                                    TempSongsRemoved++;
                                }
                            }
                            // Benachrichtigung erstellen
                            if (TempSongsRemoved == 1)
                            {
                                TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                            }
                            else
                            {
                                TempNote = "- " + TempSongsRemoved + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                            }
                            if (ListActivPlaylist.Count() == 1)
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                            }
                            else
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                            }
                        }
                        // Aktuelle Wiedergabeliste neu erstellen
                        LBCurrentPlaylist.ItemsSource = null;
                        LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                        // Aktuelle Wiedergabeliste spoeichern
                        SaveLastPlaylist();
                        // Liste neu erstellen
                        List_Playlists("Sort", -1);
                        // Benachrichtigung ausgeben
                        TBNote.Text = TempNote;
                        Timer_Settings_Action = "Note";
                        Timer_Settings_DTStart = DateTime.MinValue;
                        Timer_Settings.Start();
                    }



                    //Album abspielen, wenn Album ausgewählt wurde und Select and Play nicht aktiv ist
                    else if (tWhatIs == "Album" & SelectAndPlay == false)
                    {
                        //Variabeln erstellen
                        bool PlayFail = false;
                        //Album ID ermitteln
                        int TempAlbumsID = Convert.ToInt32(tInfoString);
                        //Playliste laden
                        var SongsToPlay = mediaLibrary.Albums[TempAlbumsID].Songs;
                        //Playliste auswählen

                        //Songs versuchen abzuspielen
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.Play(SongsToPlay);
                        }
                        catch
                        {
                            PlayFail = true;
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                        }
                        //In LastPlayback schreiben
                        if (PlayFail == false)
                        {
                            //Extended info erstellen
                            string ExtendedInfoText = "";
                            if (mediaLibrary.Albums[TempAlbumsID].Songs.Count() == 1)
                            {
                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Albums[TempAlbumsID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                            }
                            else
                            {
                                ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Albums[TempAlbumsID].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                            }
                            LastPlaybackString += mediaLibrary.Albums[TempAlbumsID].Name + ";;;Album;;;" + mediaLibrary.Albums[TempAlbumsID].Artist + ";;;" + mediaLibrary.Albums[TempAlbumsID].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                            CreateLastPlayback();
                        }
                    }
                    //Album auswählen, wenn Album ausgewählt wurde und Select and Play aktiv ist
                    else if (tWhatIs == "Album" & SelectAndPlay == true)
                    {
                        //Album ID ermitteln
                        int TempAlbumID = Convert.ToInt32(tInfoString);
                        // Album Name laden
                        string AlbumName = mediaLibrary.Albums[TempAlbumID].Name;
                        // Gesamte anzahl der Songs des Albums laden
                        int AlbumSongs = mediaLibrary.Albums[TempAlbumID].Songs.Count();
                        int TempAlbumSongs = 0;
                        // Benachrichtigung erstellen
                        string TempNote = "";
                        // Aktuelle Wiedergabe durchlaufen
                        for (int i = 0; i < ListActivPlaylist.Count(); i++)
                        {
                            // Prüfen ob lied aus Album vorhanden
                            if (AlbumName == ListActivPlaylist[i].Album)
                            {
                                // Anzahl erhöhen
                                TempAlbumSongs++;
                            }
                        }
                        // Wenn noch nicht alle Songs vorhanden
                        if (TempAlbumSongs != AlbumSongs)
                        {
                            // Anzahl hinzugefügter Songs
                            int TempSongsAdded = 0;
                            // Album durchlaufen
                            for (int i = 0; i < AlbumSongs; i++)
                            {
                                // Prüfvariable
                                bool SongExists = false;
                                // Lied Daten laden
                                string SongArtist = mediaLibrary.Albums[TempAlbumID].Songs[i].Artist.Name;
                                string SongAlbum = mediaLibrary.Albums[TempAlbumID].Songs[i].Album.Name;
                                string SongName = mediaLibrary.Albums[TempAlbumID].Songs[i].Name;
                                // Aktuelle Wiedergabe durchlaufen
                                for (int i2 = 0; i2 < ListActivPlaylist.Count(); i2++)
                                {
                                    // Prüfen ob Song bereits vorhanden
                                    if (SongArtist == ListActivPlaylist[i2].Artist & SongName == ListActivPlaylist[i2].Song & SongAlbum == ListActivPlaylist[i2].Album)
                                    {
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    // Song hinzufügen
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, mediaLibrary.Albums[TempAlbumID].Songs[i].Genre.Name, mediaLibrary.Albums[TempAlbumID].Songs[i].Duration.ToString()));
                                    // Anzahl hinzugefügter Songs erhöhen
                                    TempSongsAdded++;
                                }
                            }
                            // Benachrichtigung erstellen
                            if (TempSongsAdded == 1)
                            {
                                TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                            }
                            else
                            {
                                TempNote = "+ " + TempSongsAdded + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                            }
                            if (ListActivPlaylist.Count() == 1)
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                            }
                            else
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                            }
                        }
                        // Wenn bereits alle Songs vorhanden
                        else
                        {
                            // Anzahl entfernter Songs
                            int TempSongsRemoved = 0;
                            // Aktuelle Wiedergabe durchlaufen
                            for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                            {
                                // Wenn Eintrag, ausgewähltes Album hat
                                if (ListActivPlaylist[i].Album == AlbumName)
                                {
                                    // Song entfernen
                                    ListActivPlaylist.RemoveAt(i);
                                    // Anzahl entfernter Songs erhöhen
                                    TempSongsRemoved++;
                                }
                            }
                            // Benachrichtigung erstellen
                            if (TempSongsRemoved == 1)
                            {
                                TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                            }
                            else
                            {
                                TempNote = "- " + TempSongsRemoved + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                            }
                            if (ListActivPlaylist.Count() == 1)
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                            }
                            else
                            {
                                TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                            }
                        }
                        // Aktuelle Wiedergabeliste neu erstellen
                        LBCurrentPlaylist.ItemsSource = null;
                        LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                        // Aktuelle Wiedergabeliste speichern
                        SaveLastPlaylist();
                        // Liste neu erstellen
                        List_Playlists("Sort", -1);
                        // Benachrichtigung ausgeben
                        TBNote.Text = TempNote;
                        Timer_Settings_Action = "Note";
                        Timer_Settings_DTStart = DateTime.MinValue;
                        Timer_Settings.Start();
                    }



                    // Wenn Song ausgewählt wurde
                    else if (tWhatIs == "Song")
                    {
                        //Daten auswerten
                        string[] SplitData = Regex.Split(tInfoString, ";");

                        //Song abspielen, wenn Song von Playlist ausgewählt
                        if (SplitData[0] == "Playlist")
                        {
                            // Wenn Select and Play nicht aktiv ist
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Playliste laden
                                var SongsToPlay = mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[2]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs.Count() == 1)
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                    }
                                    else
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                    }
                                    LastPlaybackString += mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Name + ";;;Playlist;;;" + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Name + ";;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            // Wenn Select and play aktiv ist
                            else if (SelectAndPlay == true)
                            {
                                // Playliste anhand der ID laden
                                string[] PlaylistFiles = file.GetFileNames("/Playlists/");
                                // Playliste Daten laden
                                filestream = file.OpenFile("/Playlists/" + PlaylistFiles[Convert.ToInt32(SplitData[1])], FileMode.Open);
                                sr = new StreamReader(filestream);
                                string PlaylistString = sr.ReadToEnd();
                                filestream.Close();
                                // Playlist aufteilen
                                string[] splitPlaylistString = Regex.Split(PlaylistString, ";ZYXXYZ;");
                                // Lied Daten aufteilen
                                string[] splitPlaylistItem = Regex.Split(splitPlaylistString[Convert.ToInt32(SplitData[2])], ";XYZZYX;");
                                // Prüfvariable
                                bool SongExist = false;
                                // Playliste durchlaufen und prüfen ob Song bereits in der Playliste ist
                                for (int i = 0; i < ListActivPlaylist.Count(); i++)
                                {
                                    // Prüfen ob Song vorhanden
                                    if (ListActivPlaylist[i].Artist == splitPlaylistItem[0] & ListActivPlaylist[i].Album == splitPlaylistItem[1] & ListActivPlaylist[i].Song == splitPlaylistItem[2])
                                    {
                                        // Song aus aktiver Liste löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        SongExist = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExist == false)
                                {
                                    // Lied in aktuelle Wiedergabe schreiben
                                    ListActivPlaylist.Add(new ClassActivePlayList(splitPlaylistItem[0], splitPlaylistItem[1], splitPlaylistItem[2], splitPlaylistItem[3], splitPlaylistItem[4]));
                                }
                                // Liste aktuallisieren
                                List_Playlists("Sort", -1);
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExist == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                        }

                        //Song abspielen, wenn Song von Genre aus ausgewählt wurde
                        else if (SplitData[0] == "Genre")
                        {
                            // Wenn Select and Play nicht aktiv
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Genre laden
                                var SongsToPlay = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[2]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs.Count() == 1)
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                    }
                                    else
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                    }
                                    LastPlaybackString += mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Name + ";;;Genre;;;" + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Name + ";;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            // Wenn Select and Play nicht aktiv
                            else if (SelectAndPlay == true)
                            {
                                //Song Daten laden
                                string SongArtist = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Artist.Name;
                                string SongAlbum = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Album.Name;
                                string SongName = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name;
                                string SongGenre = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Genre.Name;
                                string SongDuration = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Duration.ToString();
                                // Prüfvariable
                                bool SongExists = false;
                                // Aktuelle Wiedergabeliste durchlaufen
                                for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                                {
                                    // Wenn Song bereits vorhanden
                                    if(SongArtist == ListActivPlaylist[i].Artist & SongAlbum == ListActivPlaylist[i].Album & SongName == ListActivPlaylist[i].Song)
                                    {
                                        // Eintrag löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        // Angeben das Song vorhanden
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, SongGenre, SongDuration));
                                }
                                // Aktuelle Wiedergabeliste neu erstellen
                                LBCurrentPlaylist.ItemsSource = null;
                                LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                                // Aktuelle Wiedergabeliste speichern
                                SaveLastPlaylist();
                                // Liste neu erstellen
                                List_Playlists("Sort", -1);
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExists == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                        }

                        //Song abspielen, wenn Song von Album aus ausgewählt wurde
                        else if (SplitData[0] == "Album")
                        {
                            // Wenn Select and Play nicht aktiv
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Album laden
                                var SongsToPlay = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[2]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs.Count() == 1)
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                    }
                                    else
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                    }
                                    LastPlaybackString += mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Name + ";;;Album;;;" + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Artist + ";;;" + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            // Wenn Select and Play nicht aktiv
                            else if (SelectAndPlay == true)
                            {
                                //Song Daten laden
                                string SongArtist = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Artist.Name;
                                string SongAlbum = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Album.Name;
                                string SongName = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name;
                                string SongGenre = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Genre.Name;
                                string SongDuration = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Duration.ToString();
                                // Prüfvariable
                                bool SongExists = false;
                                // Aktuelle Wiedergabeliste durchlaufen
                                for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                                {
                                    // Wenn Song bereits vorhanden
                                    if(SongArtist == ListActivPlaylist[i].Artist & SongAlbum == ListActivPlaylist[i].Album & SongName == ListActivPlaylist[i].Song)
                                    {
                                        // Eintrag löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        // Angeben das Song vorhanden
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, SongGenre, SongDuration));
                                }
                                // Aktuelle Wiedergabeliste neu erstellen
                                LBCurrentPlaylist.ItemsSource = null;
                                LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                                // Aktuelle Wiedergabeliste speichern
                                SaveLastPlaylist();
                                // Liste neu erstellen
                                List_Playlists("Sort", -1);
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExists == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                        }



                        // Alle Songs abspielen, wenn Song von allen Liedern ausgewählt wurde und Select and Play nicht aktiv ist
                        else if (SplitData[0] == "AllSongs")
                        {
                            // Wenn Select and Play nicht aktiv
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Alle Songs laden
                                var SongsToPlay = mediaLibrary.Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[1]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Songs.Count == 1)
                                    {
                                        ExtendedInfoText = mediaLibrary.Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                    }
                                    else
                                    {
                                        ExtendedInfoText = mediaLibrary.Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                    }
                                    ExtendedInfoText += " | " + TimeSpanString();

                                    LastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_AllSongs + ";;;AllSongs;;;none;;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            // Wenn Select and Play nicht aktiv
                            else if (SelectAndPlay == true)
                            {
                                //Song Daten laden
                                string SongArtist = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Artist.Name;
                                string SongAlbum = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Album.Name;
                                string SongName = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Name;
                                string SongGenre = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Genre.Name;
                                string SongDuration = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Duration.ToString();
                                // Prüfvariable
                                bool SongExists = false;
                                // Aktuelle Wiedergabeliste durchlaufen
                                for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                                {
                                    // Wenn Song bereits vorhanden
                                    if (SongArtist == ListActivPlaylist[i].Artist & SongAlbum == ListActivPlaylist[i].Album & SongName == ListActivPlaylist[i].Song)
                                    {
                                        // Eintrag löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        // Angeben das Song vorhanden
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, SongGenre, SongDuration));
                                }
                                // Aktuelle Wiedergabeliste neu erstellen
                                LBCurrentPlaylist.ItemsSource = null;
                                LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                                // Aktuelle Wiedergabeliste speichern
                                SaveLastPlaylist();
                                // Liste neu erstellen
                                List_Playlists("Sort", -1);
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExists == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                        }
                    }



                    // Alle Songs abspielen, wenn Song von allen Liedern ausgewählt wurde und Select and Play nicht aktiv ist
                    else if (tWhatIs == "AllSongs" & SelectAndPlay == false)
                    {
                        //Variabeln erstellen
                        bool PlayFail = false;
                        //Alle Songs laden
                        var SongsToPlay = mediaLibrary.Songs;
                        //Songs versuchen abzuspielen
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.Play(SongsToPlay);
                        }
                        catch
                        {
                            PlayFail = true;
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                        }
                        //In LastPlayback schreiben
                        if (PlayFail == false)
                        {
                            //Extended info erstellen
                            string ExtendedInfoText = "";
                            if (mediaLibrary.Songs.Count == 1)
                            {
                                ExtendedInfoText = SongsToPlay.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                            }
                            else
                            {
                                ExtendedInfoText = SongsToPlay.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                            }
                            ExtendedInfoText += " | " + TimeSpanString();

                            LastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_AllSongs + ";;;AllSongs;;;none;;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                            CreateLastPlayback();
                        }
                    }
                    // Alle Song zur aktuellen Wiedergabeliste hinzufügen oder entfernen, wenn select and play aktiv ist
                    else if (tWhatIs == "AllSongs" & SelectAndPlay == true)
                    {
                        // Benachrichtigung erstellen
                        string TempNote = "";
                        // Wenn aktuelle Wiedergabe weniger Songs als alle Songs, Alle Songs hinzufügen
                        if (ListActivPlaylist.Count() < mediaLibrary.Songs.Count)
                        {
                            // Benachrichtigung erstellen
                            if (mediaLibrary.Songs.Count - ListActivPlaylist.Count() == 1)
                            {
                                TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                            }
                            else
                            {
                                TempNote = "+ " + (mediaLibrary.Songs.Count - ListActivPlaylist.Count()).ToString() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | ";
                            }
                            // Aktuelle Wiedergabe leeren
                            ListActivPlaylist.Clear();
                            // Alle Songs durchlaufen 
                            for (int i = 0; i < mediaLibrary.Songs.Count; i++)
                            {
                                // Alle Songs zur aktuellen Wiedergabe hinzufügen
                                ListActivPlaylist.Add(new ClassActivePlayList(mediaLibrary.Songs[i].Artist.Name, mediaLibrary.Songs[i].Album.Name, mediaLibrary.Songs[i].Name, mediaLibrary.Songs[i].Genre.Name, mediaLibrary.Songs[i].Duration.ToString()));
                            }
                        }
                        // Wenn beriets alle Songs in aktueller Wiedergabeliste
                        else
                        {
                            // Aktuelle Wiedergabe leeren
                            ListActivPlaylist.Clear();
                            // Benachrichtigung erstellen
                            TempNote = "- " + mediaLibrary.Songs.Count + " | ";
                        }
                        // Aktuelle Wiedergabeliste neu erstellen
                        LBCurrentPlaylist.ItemsSource = null;
                        LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                        // Aktuelle Wiedergabeliste spoeichern
                        SaveLastPlaylist();
                        // Liste neu erstellen
                        List_Playlists("Sort", -1);
                        // Benachrichtigung ausgeben
                        if (ListActivPlaylist.Count() == 1)
                        {
                            TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                        }
                        else
                        {
                            TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                        }
                        TBNote.Text = TempNote;
                        Timer_Settings_Action = "Note";
                        Timer_Settings_DTStart = DateTime.MinValue;
                        Timer_Settings.Start();
                    }



                    //Wenn LastPlaybacks ausgewählt wurde
                    else if (tWhatIs == "LastPlayback")
                    {
                        //Last Playback info String zerlegen
                        string[] LastPlaybackInfo = Regex.Split(tInfoString, ";;;");

                        //Alle Songs abspielen, wenn in Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "AllSongs")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            //Playliste laden
                            var SongsToPlay = mediaLibrary.Songs;
                            //Songs versuchen abzuspielen
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Play(SongsToPlay);
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                TempLastPlaybackString += mediaLibrary.Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Song abspielen, wenn Song in Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Song")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Songs vom Artist laden
                                var AllArtist = mediaLibrary.Artists;
                                for (int i = 0; i < AllArtist.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllArtist[i].Name == LastPlaybackInfo[2])
                                    {
                                        var ArtistSongs = AllArtist[i].Songs;
                                        for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                        {
                                            if (ArtistSongs[i2].Name == LastPlaybackInfo[3])
                                            {
                                                var SongsToPlay = ArtistSongs[i2];
                                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                                MediaPlayer.Play(SongsToPlay);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Album Abspielen, wenn Album von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Album")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int AlbumSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Songs vom Artist laden
                                var AllArtist = mediaLibrary.Artists;
                                for (int i = 0; i < AllArtist.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllArtist[i].Name == LastPlaybackInfo[2])
                                    {
                                        var ArtistAlbums = AllArtist[i].Albums;
                                        for (int i2 = 0; i2 < ArtistAlbums.Count(); i2++)
                                        {
                                            if (ArtistAlbums[i2].Name == LastPlaybackInfo[3])
                                            {
                                                var SongsToPlay = ArtistAlbums[i2].Songs;
                                                AlbumSongsCount = SongsToPlay.Count();
                                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                                MediaPlayer.Play(SongsToPlay);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (AlbumSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + AlbumSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + AlbumSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Genre Abspielen, wenn Genre von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Genre")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int GenreSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Songs vom Artist laden
                                var AllGenres = mediaLibrary.Genres;
                                for (int i = 0; i < AllGenres.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllGenres[i].Name == LastPlaybackInfo[2])
                                    {
                                        var SongsToPlay = AllGenres[i].Songs;
                                        GenreSongsCount = SongsToPlay.Count();
                                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                        MediaPlayer.Play(SongsToPlay);
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (GenreSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + GenreSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + GenreSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Playlist Abspielen, wenn Playlist von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Playlist")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int PlaylistSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Playlists laden
                                var AllPlaylists = mediaLibrary.Playlists;
                                for (int i = 0; i < AllPlaylists.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllPlaylists[i].Name == LastPlaybackInfo[2])
                                    {
                                        var SongsToPlay = AllPlaylists[i].Songs;
                                        PlaylistSongsCount = SongsToPlay.Count();
                                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                        MediaPlayer.Play(SongsToPlay);
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (PlaylistSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + PlaylistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + PlaylistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Artist Abspielen, wenn Artist von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Artist")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int ArtistSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Artists laden
                                var AllArtists = mediaLibrary.Artists;
                                for (int i = 0; i < AllArtists.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllArtists[i].Name == LastPlaybackInfo[2])
                                    {
                                        var SongsToPlay = AllArtists[i].Songs;
                                        ArtistSongsCount = SongsToPlay.Count();
                                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                        MediaPlayer.Play(SongsToPlay);
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (ArtistSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Artist + " | " + ArtistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Artist + " | " + ArtistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }
                    }
                }




                //Wenn Name gedrückt wurde
                else if (LBPlaylists_LastClicked == "Name")
                {
                    //Wenn LastPlaybacks gedrückt wurde
                    if (tWhatIs == "LastPlaybacks")
                    {
                        //Playlisten öffnen oder schließen
                        List_Playlists("OpenCloseLastPlaybacks", -1);
                    }

                    //Wenn Playlists gedrückt wurde
                    else if (tWhatIs == "Playlists")
                    {
                        //Playlisten öffnen oder schließen
                        List_Playlists("OpenClosePlaylists", -1);
                    }

                    //Wenn Genres gedrückt wurde
                    else if (tWhatIs == "Genres")
                    {
                        //Genres öffnen oder schließen
                        List_Playlists("OpenCloseGenres", -1);
                    }

                    //Wenn Albums gedrückt wurde
                    else if (tWhatIs == "Albums")
                    {
                        //Albums öffnen oder schließen
                        List_Playlists("OpenCloseAlbums", -1);
                    }

                    //Wenn Alle Lieder gedrückt wurde
                    else if (tWhatIs == "AllSongs")
                    {
                        //Albums öffnen oder schließen
                        List_Playlists("OpenCloseAllSongs", -1);
                    }

                    //Wenn Playlist gedrückt wurde
                    else if (tWhatIs == "Playlist")
                    {
                        //Playlist ID ermitteln
                        int TempPlaylistID = Convert.ToInt32(tInfoString);
                        //Playlist öffnen oder schließen
                        List_Playlists("OpenClosePlaylist", TempPlaylistID);
                    }

                    //Wenn Genre gedrückt wurde
                    else if (tWhatIs == "Genre")
                    {
                        //Genre ID ermitteln
                        int TempGenreID = Convert.ToInt32(tInfoString);
                        //Genre öffnen oder schließen
                        List_Playlists("OpenCloseGenre", TempGenreID);
                    }

                    //Wenn Albums gedrückt wurde
                    else if (tWhatIs == "Album")
                    {
                        //Album ID ermitteln
                        int TempAlbumID = Convert.ToInt32(tInfoString);
                        //Album öffnen oder schließen
                        List_Playlists("OpenCloseAlbum", TempAlbumID);
                    }

                    //Wenn Song ausgewählt wurde
                    else if (tWhatIs == "Song")
                    {
                        //Daten auswerten
                        string[] SplitData = Regex.Split(tInfoString, ";");

                        //Song abspielen, wenn Song von Playlist aus ausgewählt wurde
                        if (SplitData[0] == "Playlist")
                        {
                            // Wenn Select and Play nicht aktiv
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Playliste laden
                                var SongsToPlay = mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[2]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs.Count() == 1)
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                    }
                                    else
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                    }
                                    LastPlaybackString += mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Name + ";;;Playlist;;;" + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Name + ";;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            // Wenn Select and play aktiv ist
                            else if (SelectAndPlay == true)
                            {
                                // Playliste anhand der ID laden
                                string[] PlaylistFiles = file.GetFileNames("/Playlists/");
                                // Playliste Daten laden
                                filestream = file.OpenFile("/Playlists/" + PlaylistFiles[Convert.ToInt32(SplitData[1])], FileMode.Open);
                                sr = new StreamReader(filestream);
                                string PlaylistString = sr.ReadToEnd();
                                filestream.Close();
                                // Playlist aufteilen
                                string[] splitPlaylistString = Regex.Split(PlaylistString, ";ZYXXYZ;");
                                // Lied Daten aufteilen
                                string[] splitPlaylistItem = Regex.Split(splitPlaylistString[Convert.ToInt32(SplitData[2])], ";XYZZYX;");
                                // Prüfvariable
                                bool SongExist = false;
                                // Playliste durchlaufen und prüfen ob Song bereits in der Playliste ist
                                for (int i = 0; i < ListActivPlaylist.Count(); i++)
                                {
                                    // Prüfen ob Song vorhanden
                                    if (ListActivPlaylist[i].Artist == splitPlaylistItem[0] & ListActivPlaylist[i].Album == splitPlaylistItem[1] & ListActivPlaylist[i].Song == splitPlaylistItem[2])
                                    {
                                        // Song aus aktiver Liste löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        SongExist = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExist == false)
                                {
                                    // Lied in aktuelle Wiedergabe schreiben
                                    ListActivPlaylist.Add(new ClassActivePlayList(splitPlaylistItem[0], splitPlaylistItem[1], splitPlaylistItem[2], splitPlaylistItem[3], splitPlaylistItem[4]));
                                }
                                // Liste aktuallisieren
                                List_Playlists("Sort", Convert.ToInt32(SplitData[1]));
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExist == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                        }

                        //Song aus Genre auswählen
                        else if (SplitData[0] == "Genre")
                        {
                            // Genre abspielen, wenn Select and Play nicht aktiv
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Genre laden
                                var SongsToPlay = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[2]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs.Count() == 1)
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                    }
                                    else
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                    }
                                    LastPlaybackString += mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Name + ";;;Genre;;;" + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Name + ";;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            // Wenn Select and Play nicht aktiv
                            else if (SelectAndPlay == true)
                            {
                                //Song Daten laden
                                string SongArtist = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Artist.Name;
                                string SongAlbum = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Album.Name;
                                string SongName = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name;
                                string SongGenre = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Genre.Name;
                                string SongDuration = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Duration.ToString();
                                // Prüfvariable
                                bool SongExists = false;
                                // Aktuelle Wiedergabeliste durchlaufen
                                for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                                {
                                    // Wenn Song bereits vorhanden
                                    if (SongArtist == ListActivPlaylist[i].Artist & SongAlbum == ListActivPlaylist[i].Album & SongName == ListActivPlaylist[i].Song)
                                    {
                                        // Eintrag löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        // Angeben das Song vorhanden
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, SongGenre, SongDuration));
                                }
                                // Aktuelle Wiedergabeliste neu erstellen
                                LBCurrentPlaylist.ItemsSource = null;
                                LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                                // Aktuelle Wiedergabeliste speichern
                                SaveLastPlaylist();
                                // Liste neu erstellen
                                List_Playlists("Sort", -1);
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExists == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                        }

                        //Song aus Album auswählen
                        else if (SplitData[0] == "Album")
                        {
                            // Wenn Select and Play nicht aktiv, Album abspielen
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Album laden
                                var SongsToPlay = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[2]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs.Count() == 1)
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();
                                    }
                                    else
                                    {
                                        ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString();
                                    }
                                    LastPlaybackString += mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Name + ";;;Album;;;" + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Artist + ";;;" + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            // Wenn Select and Play aktiv, Song zur aktuellen Wiedergabe hinzufügen
                            else if (SelectAndPlay == true)
                            {
                                //Song Daten laden
                                string SongArtist = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Artist.Name;
                                string SongAlbum = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Album.Name;
                                string SongName = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name;
                                string SongGenre = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Genre.Name;
                                string SongDuration = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Duration.ToString();
                                // Prüfvariable
                                bool SongExists = false;
                                // Aktuelle Wiedergabeliste durchlaufen
                                for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                                {
                                    // Wenn Song bereits vorhanden
                                    if (SongArtist == ListActivPlaylist[i].Artist & SongAlbum == ListActivPlaylist[i].Album & SongName == ListActivPlaylist[i].Song)
                                    {
                                        // Eintrag löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        // Angeben das Song vorhanden
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, SongGenre, SongDuration));
                                }
                                // Aktuelle Wiedergabeliste neu erstellen
                                LBCurrentPlaylist.ItemsSource = null;
                                LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                                // Aktuelle Wiedergabeliste speichern
                                SaveLastPlaylist();
                                // Liste neu erstellen
                                List_Playlists("Sort", -1);
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExists == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }
                        }


                        //Song aus allen Songs auswählen
                        else if (SplitData[0] == "AllSongs")
                        {
                            // Wenn select and Play nicht aktiv, alle Songs absoeieln
                            if (SelectAndPlay == false)
                            {
                                //Variabeln erstellen
                                bool PlayFail = false;
                                //Alle Songs laden
                                var SongsToPlay = mediaLibrary.Songs;
                                //Songs versuchen abzuspielen
                                try
                                {
                                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                    MediaPlayer.Play(SongsToPlay, Convert.ToInt32(SplitData[1]));
                                }
                                catch
                                {
                                    PlayFail = true;
                                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                                }
                                //In LastPlayback schreiben
                                if (PlayFail == false)
                                {
                                    //Extended info erstellen
                                    string ExtendedInfoText = "";
                                    if (mediaLibrary.Songs.Count == 1)
                                    {
                                        ExtendedInfoText = mediaLibrary.Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                    }
                                    else
                                    {
                                        ExtendedInfoText = mediaLibrary.Songs.Count + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                    }
                                    ExtendedInfoText += " | " + TimeSpanString();

                                    LastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_AllSongs + ";;;AllSongs;;;none;;;none;;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                                    CreateLastPlayback();
                                }
                            }
                            //Wenn Select and Play aktiv, Song zur aktuellen Wiedergabe hinzufügen
                            else if (SelectAndPlay == true)
                            {
                                //Song Daten laden
                                string SongArtist = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Artist.Name;
                                string SongAlbum = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Album.Name;
                                string SongName = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Name;
                                string SongGenre = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Genre.Name;
                                string SongDuration = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Duration.ToString();
                                // Prüfvariable
                                bool SongExists = false;
                                // Aktuelle Wiedergabeliste durchlaufen
                                for (int i = ListActivPlaylist.Count() - 1; i >= 0; i--)
                                {
                                    // Wenn Song bereits vorhanden
                                    if (SongArtist == ListActivPlaylist[i].Artist & SongAlbum == ListActivPlaylist[i].Album & SongName == ListActivPlaylist[i].Song)
                                    {
                                        // Eintrag löschen
                                        ListActivPlaylist.RemoveAt(i);
                                        // Angeben das Song vorhanden
                                        SongExists = true;
                                        break;
                                    }
                                }
                                // Wenn Song noch nicht existiert
                                if (SongExists == false)
                                {
                                    ListActivPlaylist.Add(new ClassActivePlayList(SongArtist, SongAlbum, SongName, SongGenre, SongDuration));
                                }
                                // Aktuelle Wiedergabeliste neu erstellen
                                LBCurrentPlaylist.ItemsSource = null;
                                LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                                // Aktuelle Wiedergabeliste speichern
                                SaveLastPlaylist();
                                // Liste neu erstellen
                                List_Playlists("Sort", -1);
                                // Benachrichtigung ausgeben
                                string TempNote = "";
                                if (SongExists == true)
                                {
                                    TempNote = "- 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                else
                                {
                                    TempNote = "+ 1 " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | ";
                                }
                                if (ListActivPlaylist.Count() == 1)
                                {
                                    TempNote += "1 " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                                }
                                else
                                {
                                    TempNote += ListActivPlaylist.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                                }
                                TBNote.Text = TempNote;
                                Timer_Settings_Action = "Note";
                                Timer_Settings_DTStart = DateTime.MinValue;
                                Timer_Settings.Start();
                            }

                        }
                    }


                    //Wenn LastPlaybacks ausgewählt wurde
                    else if (tWhatIs == "LastPlayback")
                    {
                        //Last Playback info String zerlegen
                        string[] LastPlaybackInfo = Regex.Split(tInfoString, ";;;");

                        //Alle Songs abspielen, wenn in Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "AllSongs")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            //Playliste laden
                            var SongsToPlay = mediaLibrary.Songs;
                            //Songs versuchen abzuspielen
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Play(SongsToPlay);
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                TempLastPlaybackString += mediaLibrary.Songs.Count() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Song abspielen, wenn Song in Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Song")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Songs vom Artist laden
                                var AllArtist = mediaLibrary.Artists;
                                for (int i = 0; i < AllArtist.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllArtist[i].Name == LastPlaybackInfo[2])
                                    {
                                        var ArtistSongs = AllArtist[i].Songs;
                                        for (int i2 = 0; i2 < ArtistSongs.Count(); i2++)
                                        {
                                            if (ArtistSongs[i2].Name == LastPlaybackInfo[3])
                                            {
                                                var SongsToPlay = ArtistSongs[i2];
                                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                                MediaPlayer.Play(SongsToPlay);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Album Abspielen, wenn Album von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Album")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int AlbumSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Songs vom Artist laden
                                var AllArtist = mediaLibrary.Artists;
                                for (int i = 0; i < AllArtist.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllArtist[i].Name == LastPlaybackInfo[2])
                                    {
                                        var ArtistAlbums = AllArtist[i].Albums;
                                        for (int i2 = 0; i2 < ArtistAlbums.Count(); i2++)
                                        {
                                            if (ArtistAlbums[i2].Name == LastPlaybackInfo[3])
                                            {
                                                var SongsToPlay = ArtistAlbums[i2].Songs;
                                                AlbumSongsCount = SongsToPlay.Count();
                                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                                MediaPlayer.Play(SongsToPlay);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (AlbumSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + AlbumSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Album + " | " + AlbumSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Genre Abspielen, wenn Genre von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Genre")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int GenreSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Songs vom Artist laden
                                var AllGenres = mediaLibrary.Genres;
                                for (int i = 0; i < AllGenres.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllGenres[i].Name == LastPlaybackInfo[2])
                                    {
                                        var SongsToPlay = AllGenres[i].Songs;
                                        GenreSongsCount = SongsToPlay.Count();
                                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                        MediaPlayer.Play(SongsToPlay);
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (GenreSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + GenreSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Genre + " | " + GenreSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Playlist Abspielen, wenn Playlist von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Playlist")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int PlaylistSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Playlists laden
                                var AllPlaylists = mediaLibrary.Playlists;
                                for (int i = 0; i < AllPlaylists.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllPlaylists[i].Name == LastPlaybackInfo[2])
                                    {
                                        var SongsToPlay = AllPlaylists[i].Songs;
                                        PlaylistSongsCount = SongsToPlay.Count();
                                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                        MediaPlayer.Play(SongsToPlay);
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (PlaylistSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + PlaylistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Playlist + " | " + PlaylistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }

                        //Artist Abspielen, wenn Artist von Last Playbacks ausgewählt wurde
                        if (LastPlaybackInfo[1] == "Artist")
                        {
                            //Variabeln erstellen
                            bool PlayFail = false;
                            int ArtistSongsCount = -1;
                            //Songs versuchen abzuspielen
                            try
                            {
                                //Alle Artists laden
                                var AllArtists = mediaLibrary.Artists;
                                for (int i = 0; i < AllArtists.Count(); i++)
                                {
                                    //Wenn Artist ist
                                    if (AllArtists[i].Name == LastPlaybackInfo[2])
                                    {
                                        var SongsToPlay = AllArtists[i].Songs;
                                        ArtistSongsCount = SongsToPlay.Count();
                                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                        MediaPlayer.Play(SongsToPlay);
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                PlayFail = true;
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                            }
                            //In LastPlayback schreiben
                            if (PlayFail == false)
                            {
                                string TempLastPlaybackString = "";
                                for (int i = 0; i < 7; i++)
                                {
                                    TempLastPlaybackString += LastPlaybackInfo[i] + ";;;";
                                }
                                if (ArtistSongsCount == 1)
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Artist + " | " + ArtistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString() + ";;;;;";
                                }
                                else
                                {
                                    TempLastPlaybackString += MyMusicPlayer.Resources.AppResources.Z001_Artist + " | " + ArtistSongsCount + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs + " | " + TimeSpanString() + ";;;;;";
                                }
                                LastPlaybackString += TempLastPlaybackString;
                                CreateLastPlayback();
                            }
                        }
                    }
                }



                //Wenn nur Song abgespielt wird // Button 1
                else if (LBPlaylists_LastClicked == "PlaySong")
                {
                    // Wenn Select and Play aktiv und Playliste ausgewählt ist
                    if ( SelectAndPlay == true & tWhatIs == "Playlist")
                    {
                        try
                        {
                            // Aktive Playliste leeren
                            ListActivPlaylist.Clear();
                            // Playliste laden
                            string[] allPlaylist = file.GetFileNames("/Playlists/");
                            // Letzte Liste laden
                            filestream = file.OpenFile("/Playlists/" + allPlaylist[Convert.ToInt32(tInfoString)], FileMode.Open);
                            sr = new StreamReader(filestream);
                            string PlaylistString = sr.ReadToEnd();
                            filestream.Close();
                            // Playliste aufteilen
                            string[] splitPlaylists = Regex.Split(PlaylistString, ";ZYXXYZ;");
                            // Durchlaufen
                            for (int i = 0; i < splitPlaylists.Count() - 1; i++)
                            {
                                // Eintrag splitten
                                string[] splitPlaylist = Regex.Split(splitPlaylists[i], ";XYZZYX;");
                                // Eintrag in aktuelle Liste schreiben
                                ListActivPlaylist.Add(new ClassActivePlayList(splitPlaylist[0], splitPlaylist[1], splitPlaylist[2], splitPlaylist[3], splitPlaylist[4]));
                            }

                            // Letzte Wiedergabe speichern
                            SaveLastPlaylist();
                            CreateShuffleInt();

                            // PlayID auslesen
                            PlayID = -1;
                            // Falls Mediaplayer abspielt
                            if (MediaPlayer.State.ToString() == "Playing")
                            {
                                // Mediaplayer Daten laden
                                string playingArtist = MediaPlayer.Queue.ActiveSong.Artist.Name;
                                string playingAlbum = MediaPlayer.Queue.ActiveSong.Album.Name;
                                string playingSong = MediaPlayer.Queue.ActiveSong.Name;
                                // PlayID ermitteln
                                for (int i = 0; i < ListActivPlaylist.Count(); i++)
                                {
                                    if (ListActivPlaylist[i].Artist == playingArtist & ListActivPlaylist[i].Album == playingAlbum & ListActivPlaylist[i].Song == playingSong)
                                    {
                                        PlayID = i;
                                        break;
                                    }
                                }
                            }
                            // Wenn PlayID anders als -1
                            if (PlayID != -1)
                            {
                                // PlayControl auf Play setzen
                                PlayControl = "Play";
                            }
                            // Playlist Liste neu erstellen
                            List_Playlists("Sort", -1);
                            // string der Anzahl der Songs erstellen
                            string NoteSongs = "";
                            if (ListActivPlaylist.Count == 1)
                            {
                                NoteSongs = ListActivPlaylist.Count().ToString() + " " + MyMusicPlayer.Resources.AppResources.Z001_Song;
                            }
                            else
                            {
                                NoteSongs = ListActivPlaylist.Count().ToString() + " " + MyMusicPlayer.Resources.AppResources.Z001_Songs;
                            }
                            // Benachrichtigung ausgeben das Playliste geladen
                            TBNote.Text = allPlaylist[Convert.ToInt32(tInfoString)].Replace(".dat", "") + " | " + NoteSongs;
                            Timer_Settings_Action = "Note";
                            Timer_Settings_DTStart = DateTime.MinValue;
                            Timer_Settings.Start();
                        }
                        // Falls sich Liste nicht erstellen lässt
                        catch
                        {
                            // Aktuelle Playliste leeren
                            ListActivPlaylist.Clear();
                        }
                    }



                    //Daten auswerten
                    string[] SplitData = Regex.Split(tInfoString, ";");

                    // Wenn nur Song abspielen Button in der Playliste gedrückt wird
                    if (SplitData[0] == "Playlist")
                    {
                        //Variabeln erstellen
                        bool PlayFail = false;
                        //Playliste laden
                        var SongsToPlay = mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])];
                        //Songs versuchen abzuspielen
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.Play(SongsToPlay);
                        }
                        catch
                        {
                            PlayFail = true;
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                        }
                        //In LastPlayback schreiben
                        if (PlayFail == false)
                        {
                            //Extended info erstellen
                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();

                            LastPlaybackString += mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name + ";;;Song;;;" + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Artist + ";;;" + mediaLibrary.Playlists[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                            CreateLastPlayback();
                        }
                    }

                    //Song abspielen, wenn Song von Genre aus ausgewählt wurde
                    if (SplitData[0] == "Genre")
                    {
                        //Variabeln erstellen
                        bool PlayFail = false;
                        //Genre laden
                        var SongsToPlay = mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])];
                        //Songs versuchen abzuspielen
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.Play(SongsToPlay);
                        }
                        catch
                        {
                            PlayFail = true;
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                        }
                        //In LastPlayback schreiben
                        if (PlayFail == false)
                        {
                            //Extended info erstellen
                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();

                            LastPlaybackString += mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name + ";;;Song;;;" + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Artist + ";;;" + mediaLibrary.Genres[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                            CreateLastPlayback();
                        }
                    }

                    //Song abspielen, wenn Song von Album aus ausgewählt wurde
                    if (SplitData[0] == "Album")
                    {
                        //Variabeln erstellen
                        bool PlayFail = false;
                        //Album laden
                        var SongsToPlay = mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])];
                        //Songs versuchen abzuspielen
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.Play(SongsToPlay);
                        }
                        catch
                        {
                            PlayFail = true;
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                        }
                        //In LastPlayback schreiben
                        if (PlayFail == false)
                        {
                            //Extended info erstellen
                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();

                            LastPlaybackString += mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name + ";;;Song;;;" + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Artist + ";;;" + mediaLibrary.Albums[Convert.ToInt32(SplitData[1])].Songs[Convert.ToInt32(SplitData[2])].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                            CreateLastPlayback();
                        }
                    }


                    //Song abspielen, wenn Song von allen Liedern ausgewählt wurde
                    if (SplitData[0] == "AllSongs")
                    {
                        //Variabeln erstellen
                        bool PlayFail = false;
                        //Playliste laden
                        var SongsToPlay = mediaLibrary.Songs[Convert.ToInt32(SplitData[1])];
                        //Songs versuchen abzuspielen
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.Play(SongsToPlay);
                        }
                        catch
                        {
                            PlayFail = true;
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteFail);
                        }
                        //In LastPlayback schreiben
                        if (PlayFail == false)
                        {
                            //Extended info erstellen
                            string ExtendedInfoText = MyMusicPlayer.Resources.AppResources.Z001_Song + " | " + TimeSpanString();

                            LastPlaybackString += mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Name + ";;;Song;;;" + mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Artist + ";;;" + mediaLibrary.Songs[Convert.ToInt32(SplitData[1])].Name + ";;;none;;;none;;;none;;;" + ExtendedInfoText + ";;;;;";
                            CreateLastPlayback();
                        }
                    }
                }
            }



            //LBSongs_LastClicked zurücksetzen
            LBPlaylists_LastClicked = "none";
            //Auswahl aufheben
            try
            {
                LBPlaylists.SelectedIndex = -1;
            }
            catch
            {
            }
        }



        //Wenn Play gedrück wurde //Kommt vor wenn auswahl geändert
        private void LBPlaylists_ButtonPlay(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBPlaylists_LastClicked = "Play";
        }

        //Wenn Name gedrück wurde //Kommt vor wenn auswahl geändert
        private void LBPlaylists_ButtonName(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBPlaylists_LastClicked = "Name";
        }

        //Wenn PlaySong gedrückt wurde
        private void LBPlaylists_ButtonPlaySong(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBPlaylists_LastClicked = "PlaySong";
        }
        //---------------------------------------------------------------------------------------------------------
        # endregion





        # region Methoden um letzte Playliste zu speichern und neuen Shuffle Int zu erstellen
        // Methode um letzte Playliste zu speichern
        //---------------------------------------------------------------------------------------------------------
        void SaveLastPlaylist()
        {
            // Wenn Select and Play aktiv
            if (SelectAndPlay == true)
            {
                // Wenn Ordner noch nicht vorhanden
                if (!file.DirectoryExists("/LastPlaylist"))
                {
                    file.CreateDirectory("/LastPlaylist");
                }
                // Last Playlist string erstellen
                string string_LastPlaylist = "";
                // Aktuelle Playliste durchlaufen und in String eintragen
                for (int i = 0; i < ListActivPlaylist.Count(); i++)
                {
                    string_LastPlaylist += ListActivPlaylist[i].Artist + ";XYZZYX;";
                    string_LastPlaylist += ListActivPlaylist[i].Album + ";XYZZYX;";
                    string_LastPlaylist += ListActivPlaylist[i].Song + ";XYZZYX;";
                    string_LastPlaylist += ListActivPlaylist[i].Genre + ";XYZZYX;";
                    string_LastPlaylist += ListActivPlaylist[i].Duration + ";ZYXXYZ;";
                }
                // Aktuelle Playliste als letzte Playliste speichern
                filestream = file.CreateFile("/LastPlaylist/LastPlaylist.dat");
                sw = new StreamWriter(filestream);
                sw.Write(string_LastPlaylist);
                sw.Flush();
                filestream.Close();
            }

            // Neuen Shuffle Int erstellen
            CreateShuffleInt();
        }



        // Methode um neuen Shuffle Int zu erstellen
        //---------------------------------------------------------------------------------------------------------
        // Variabeln
        int[] ShuffleInt;
        // Methode
        void CreateShuffleInt()
        {
            // Neuen Shuffle Int erstellen
            ShuffleInt = new int[ListActivPlaylist.Count];
            // Schleife erstellen um 10 fach zu Shuffeln
            for (int i2 = 0; i2 < 10; i2++)
            {
                // Tempstring erstellen
                string TempString = ";";
                // Wenn i2 = 0 ist
                if (i2 == 0)
                {
                    // Tempstring aus Aktiver Playliste erstellen
                    for (int i = 0; i < ListActivPlaylist.Count(); i++)
                    {
                        TempString += i + ";";
                    }
                }
                // Wenn i2 != 0
                else
                {
                    // Tempstring aus ShuffleInt erstellen
                    for (int i = 0; i < ShuffleInt.Count(); i++)
                    {
                        TempString += ShuffleInt[i] + ";";
                    }
                }
                // Temp String durchlaufen
                for (int i = 0; i < ListActivPlaylist.Count(); i++)
                {
                    // ShuffleInt neu mischen
                    string[] splitTempString = Regex.Split(TempString, ";");
                    Random rand = new Random();
                    int RandomTempInt = rand.Next(1, (splitTempString.Count() - 1));
                    int TempInt = Convert.ToInt32(splitTempString[RandomTempInt]);
                    ShuffleInt[i] = TempInt;
                    TempString = TempString.Replace(";" + TempInt.ToString() + ";", ";");
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------
        # endregion





        # region MediaPlayer
        //MediaPlayer
        //---------------------------------------------------------------------------------------------------------------------------------
        //Button Play / Pause / Play on, Button Down
        private void MPPlayPause_Down(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Angeben welcher Button gedrückt wird
            MPButtonPressed = "PlayPause";
            //Farbe des Hintergrunds änders
            MPPlayPauseBg.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
            FPPlayPauseBg.Fill = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, -1);
        }
        //Button Play / Pause / Play on, Button Up
        private void MPPlayPause_Up(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Button kurz gedrückt wurde, Aktion Play, Pause
            if (MPButtonPressed == "PlayPause")
            {
                // Wenn Selecta and Play nicht aktiv
                if (SelectAndPlay == false)
                {
                    //Wenn PlayOn aktiviert
                    if (MPPlayOn == true)
                    {
                        //PlayOn deaktivieren
                        MPPlayOn = false;
                    }
                    //Wenn PlayOn nicht aktiviert ist
                    else
                    {
                        //Wenn Status Pausiert
                        if (MPState == "Paused")
                        {
                            //Versuchen Player weiter laufen lassen
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Resume();
                            }
                            catch
                            {
                            }
                        }
                        //Wenn Status Playing
                        if (MPState == "Playing")
                        {
                            //Versuchen Player zu pausieren
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Pause();
                            }
                            catch
                            {
                            }
                        }
                        //Wenn Status gestoppt
                        if (MPState == "Stopped")
                        {
                            //Versuchen Player zu starten
                            try
                            {
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Resume();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                // Wenn Select and Play aktiv
                else if (SelectAndPlay == true)
                {
                    //Wenn PlayOn aktiviert
                    if (MPPlayOn == true)
                    {
                        //PlayOn deaktivieren
                        MPPlayOn = false;
                    }
                    //Wenn PlayOn nicht aktiviert ist
                    else
                    {
                        //Wenn Status Pausiert
                        if (MPState == "Paused")
                        {
                            //Versuchen Player weiter laufen lassen
                            try
                            {
                                // Player weiter laufen lasse
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Resume();
                                // Anegeben das Player läuft
                                PlayControl = "Play";
                            }
                            catch
                            {
                            }
                        }
                        //Wenn Status Playing
                        if (MPState == "Playing")
                        {
                            //Versuchen Player zu pausieren
                            try
                            {
                                // Player Pausieren
                                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                                MediaPlayer.Pause();
                                // Anegeben das Player pausiert
                                PlayControl = "Pause";
                            }
                            catch
                            {
                            }
                        }
                        //Wenn Status gestoppt und Song in aktiver Playliste vorhanden
                        if (MPState == "Stopped" & ListActivPlaylist.Count() > 0)
                        {
                            // Versuchen wenn Play ID != -1
                            if (PlayID == -1 | PlayID >= ListActivPlaylist.Count())
                            {
                                PlayID = 0;
                            }
                            // Wenn Shuffle Aktiv ist
                            if (SetShuffle == true)
                            {
                                SelectAndPlay_Play(ShuffleInt[PlayID]);
                            }
                            // Wenn Shuffle nicht aktiv ist
                            else
                            {
                                SelectAndPlay_Play(PlayID);
                            }
                            // Angeben das Player läuft
                            PlayControl = "Play";
                        }
                    }
                }
                //Angeben das kein Button gedrückt ist
                MPButtonPressed = "none";
                MPButtonPressStart = DateTime.MinValue;
                //Hintergrundfarbe des Buttons zurückstellen
                MPPlayPauseBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
                FPPlayPauseBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
            }
        }
        //Button Play / Pause / Play on, Button Leave
        private void MPPlayPause_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Angeben das kein Button gedrückt ist
            MPButtonPressed = "none";
            //Hintergrundfarbe des Buttons zurückstellen
            MPPlayPauseBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
            FPPlayPauseBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
        }



        //Button Forward
        private void MPForward_Down(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Farbe des Hintergrundes ändern
            MPForwardBg.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
            FPForwardBg.Fill = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, -1);
        }
        private void MPForward_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Angeben das näachstes Lied geladen wird
            SongForward();
            MPForwardBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
            FPForwardBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
        }
        private void MPForward_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MPForwardBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
            FPForwardBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
        }
        private void SongForward()
        {
            // Wenn Select and Play Aktiviert und PlayControl Play ist
            if (SelectAndPlay == true & PlayControl == "Play")
            {
                // Wenn noch Lieder in der aktuellen Wiedergabeliste // Sicherung, falls Lieder gelöscht werden
                if (ListActivPlaylist.Count() > 0)
                {
                    // Versuchen aktuelle ID des Songs zu ermitteln
                    int ID = -1;
                    for (int i = 0; i < ListActivPlaylist.Count(); i++)
                    {
                        // Wenn aktueller Song ist
                        if (PlayArtist == ListActivPlaylist[i].Artist & PlayAlbum == ListActivPlaylist[i].Album & PlaySong == ListActivPlaylist[i].Song)
                        {
                            ID = i;
                            break;
                        }
                    }
                    // Wenn ID nicht geladen wurde
                    if (ID == -1)
                    {
                        ID = PlayID;
                    }

                    // Wenn Shuffle aktiv
                    if (SetShuffle == true)
                    {
                        // Shuffle ID erstellen
                        int ShuffleID = -1;
                        // ID in Shuffle int suchen
                        for (int i = 0; i < ShuffleInt.Count(); i++)
                        {
                            // Wenn ID gefunden
                            if (ShuffleInt[i] == ID)
                            {
                                ShuffleID = i;
                            }
                        }
                        // Shuffle ID erhöhen
                        ShuffleID++;
                        // Wenn Shuffle ID >= ShuffleInt.Count()
                        if (ShuffleID >= ShuffleInt.Count())
                        {
                            ShuffleID = 0;
                        }
                        // Song anhand der Shuffle ID laden
                        SelectAndPlay_Play(ShuffleInt[ShuffleID]);
                        // PlayNextSong if paused deaktivieren, falls aktiv
                        PlayNextSongIfPaused = false;
                    }

                    // Wenn Shuffle nicht aktiv ist
                    else if (SetShuffle == false)
                    {
                        // ID erhöhen
                        ID++;
                        // Wenn keine Songs mehr vorhanden
                        if (ID >= ListActivPlaylist.Count())
                        {
                            // ID zurücksetzen
                            ID = 0;
                        }
                        SelectAndPlay_Play(ID);
                        // PlayNextSong if paused deaktivieren, falls aktiv
                        PlayNextSongIfPaused = false;
                    }
                }
            }

            // Wenn Select and Play nicht aktiviert ist
            else if (SelectAndPlay == false)
            {
                //Versuchen nächsten Song zu laden
                try
                {
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    MediaPlayer.MoveNext();
                }
                catch
                {
                }
            }
        }



        //Button Back
        private void MPBack_Down(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Farbe des Hintergrundes ändern
            MPBackBg.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
            FPBackBg.Fill = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, -1);
        }
        private void MPBack_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Letzten Song laden
            SongBack();
            MPBackBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
            FPBackBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
        }
        private void MPBack_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MPBackBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
            FPBackBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
        }
        private void SongBack()
        {
            // Wenn Select and Play Aktiviert und PlayControl Play ist
            if (SelectAndPlay == true & PlayControl == "Play")
            {
                //Position ermitteln
                Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                TimeSpan TempPosition = MediaPlayer.PlayPosition;
                //Wenn schon länger als 5 Sekunden läuft
                if (TempPosition.TotalSeconds > 5)
                {
                    // Versuchen aktuelle ID des Songs zu ermitteln
                    int ID = -1;
                    for (int i = 0; i < ListActivPlaylist.Count(); i++)
                    {
                        // Wenn aktueller Song ist
                        if (PlayArtist == ListActivPlaylist[i].Artist & PlayAlbum == ListActivPlaylist[i].Album & PlaySong == ListActivPlaylist[i].Song)
                        {
                            ID = i;
                            break;
                        }
                    }
                    // Wenn ID vorhanden
                    if (ID != -1)
                    {
                        SelectAndPlay_Play(ID);
                        // PlayNextSong if paused deaktivieren, falls aktiv
                        PlayNextSongIfPaused = false;
                    }
                }
                //Wenn kürzer als 5 Sekunden läuft
                else
                {
                    // Wenn noch Lieder in der aktuellen Wiedergabeliste // Sicherung, falls Lieder gelöscht werden
                    if (ListActivPlaylist.Count() > 0)
                    {
                        // Versuchen aktuelle ID des Songs zu ermitteln
                        int ID = -1;
                        for (int i = 0; i < ListActivPlaylist.Count(); i++)
                        {
                            // Wenn aktueller Song ist
                            if (PlayArtist == ListActivPlaylist[i].Artist & PlayAlbum == ListActivPlaylist[i].Album & PlaySong == ListActivPlaylist[i].Song)
                            {
                                ID = i;
                                break;
                            }
                        }
                        // Wenn ID nicht geladen wurde
                        if (ID == -1)
                        {
                            ID = PlayID;
                        }

                        // Wenn Shuffle aktiv
                        if (SetShuffle == true)
                        {
                            // Shuffle ID erstellen
                            int ShuffleID = -1;
                            // ID in Shuffle int suchen
                            for (int i = 0; i < ShuffleInt.Count(); i++)
                            {
                                // Wenn ID gefunden
                                if (ShuffleInt[i] == ID)
                                {
                                    ShuffleID = i;
                                }
                            }
                            // Shuffle ID verringern
                            ShuffleID--;
                            // Wenn Shuffle ID >= ShuffleInt.Count()
                            if (ShuffleID < 0)
                            {
                                ShuffleID = ShuffleInt[ShuffleInt.Count() - 1];
                            }
                            // Song anhand der Shuffle ID laden
                            SelectAndPlay_Play(ShuffleInt[ShuffleID]);
                            // PlayNextSong if paused deaktivieren, falls aktiv
                            PlayNextSongIfPaused = false;
                        }

                        // Wenn Shuffle nicht aktiv ist
                        else if (SetShuffle == false)
                        {
                            // ID verringern
                            ID--;
                            // Wenn keine Songs mehr vorhanden
                            if (ID < 0)
                            {
                                // ID zurücksetzen
                                ID = (ListActivPlaylist.Count() - 1);
                            }
                            SelectAndPlay_Play(ID);
                            // PlayNextSong if paused deaktivieren, falls aktiv
                            PlayNextSongIfPaused = false;
                        }
                    }
                }
            }

            // Wenn Select and Play nicht aktiviert ist
            else if (SelectAndPlay == false)
            {
                //Auf Anfang gehen oder vorherigen Song laden
                try
                {
                    //Position ermitteln
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    TimeSpan TempPosition = MediaPlayer.PlayPosition;
                    //Wenn schon länger als 5 Sekunden läuft
                    if (TempPosition.TotalSeconds > 5)
                    {
                        MediaPlayer.MovePrevious();
                        MediaPlayer.MoveNext();
                    }
                    //Wenn kürzer als 5 Sekunden läuft
                    else
                    {
                        MediaPlayer.MovePrevious();
                    }
                }
                catch
                {
                }
            }
        }



        //MediaPlayer öffnen oder schließen
        private void MPOpenClose_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Mediaplayer geschlossen
            if (MPAnimationStatus == "Closed")
            {
                Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                MPAnimationStatus = "Open";
            }
            else if (MPAnimationStatus == "Opened")
            {
                Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                MPAnimationStatus = "Close";
            }
        }



        //FullscreenPlayer öffnen oder schließen
        private void FPOpenClose_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Mediaplayer geschlossen
            if (FPAnimationStatus == "Closed")
            {
                Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                FPAnimationStatus = "Open";
            }
            else if (FPAnimationStatus == "Opened")
            {
                Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                FPAnimationStatus = "Close";
            }
        }



        //MediaPlayer Shuffle
        private void MPShuffle_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Wenn Shuffle deaktiviert ist
            if (SetShuffle == false)
            {
                // Shuffle aktivieren
                SetShuffle = true;
                MPShuffle.Opacity = 1.0;
                FPShuffle.Opacity = 1.0;
                // Einstellungen speichern
                CreateSettings();

                // Wenn Select and Play aktiv
                if (SelectAndPlay == true)
                {
                    // Shuffle deaktivieren
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.IsShuffled = false;
                    }
                    catch
                    {
                    }
                }
                // Wenn Select and Play nicht aktiv
                else
                {
                    // Shuffle deaktivieren
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.IsShuffled = true;
                    }
                    catch
                    {
                    }
                }
            }

            // Wenn Shuffle aktiviert ist
            else
            {
                // Shuffle deaktivieren
                SetShuffle = false;
                MPShuffle.Opacity = 0.5;
                FPShuffle.Opacity = 0.5;
                // Einstellungen speichern
                CreateSettings();

                // Shuffle deaktivieren
                try
                {
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    MediaPlayer.IsShuffled = false;
                }
                catch
                {
                }
            }
        }



        //MediaPlayer Repead
        private void MPRepead_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Wenn Repead deaktiviert ist
            if (SetRepead == false)
            {
                // Repead aktivieren
                SetRepead = true;
                MPRepead.Opacity = 1.0;
                FPRepead.Opacity = 1.0;
                // Einstellungen speichern
                CreateSettings();

                // Wenn Select and Play aktiv
                if (SelectAndPlay == true)
                {
                    // Repead deaktivieren
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.IsRepeating = false;
                    }
                    catch
                    {
                    }
                }
                // Wenn Select and Play nicht aktiv
                else
                {
                    // Repead deaktivieren
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.IsRepeating = true;
                    }
                    catch
                    {
                    }
                }
            }

            // Wenn Repead aktiviert ist
            else
            {
                // Repead deaktivieren
                SetRepead = false;
                MPRepead.Opacity = 0.5;
                FPRepead.Opacity = 0.5;
                // Einstellungen speichern
                CreateSettings();

                // Repead deaktivieren
                try
                {
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    MediaPlayer.IsRepeating = false;
                }
                catch
                {
                }
            }
        }



        //Variabeln, Timer MediaPlayer
        string MPState = "none";                        //Status des MediaPlayers
        string MPAnimationStatus = "Closed";            //Status Animation, MediaPlayer klein / Open, Close
        string FPAnimationStatus = "Closed";            //Status Animation, MediaPlayer groß / Open, Close
        DateTime MPAnimationStart = DateTime.MinValue;  //Zeitgeben Animation, MediaPlayer klein / Open, Close
        DateTime FPAnimationStart = DateTime.MinValue;  //Zeitgeben Animation, MediaPlayer groß / Open, Close
        string ActiveSongName = "";                     //Momentaner Song
        string ActiveSongAlbum = "";                    //Momentanes Album
        string ActiveSongArtist = "";                   //Momentaner Artist
        TimeSpan ActiveSongDuration = new TimeSpan();   //Zeitgeber für Song Spielzeit
        Stream AlbumStream;                             //Stream des Albumbildes
        DateTime MPButtonPressStart = DateTime.MinValue;     //Zeitgeber wie lange Button gedrückt wird
        string MPButtonPressed = "none";                //Angabe welcher Button gedrückt ist
        bool MPPlayOn = false;                          //Angabe ob Titel nur angespielt werden
        bool PlayNextSongIfPaused = false;              // Angabe ob nächstes Lied geladen wird, wenn Player den Status "Paused" erreicht


        //Timer MediaPlayer
        void Timer_MediaPlayer_Tick(object sender, object e)
        {
            //Wenn einer der Buttons gehalten wird
            if (MPButtonPressed != "none")
            {
                //Wenn Zeitgeber noch nicht aktiv
                if (MPButtonPressStart == DateTime.MinValue)
                {
                    //Zeitgeber aktivieren
                    MPButtonPressStart = DateTime.Now;
                }
                //Wenn Zeitgeber bereits läuft
                else
                {
                    //Prüfen ob Zeitgeber bereits länger als 2 Sekunden läuft
                    if (MPButtonPressStart.AddSeconds(0.8) < DateTime.Now)
                    {
                        //Wenn Play / Pause / Play on gehalten wird
                        if (MPButtonPressed == "PlayPause")
                        {
                            //Wenn Mediaplayer etwas abspielt
                            if (MPState == "Playing")
                            {
                                //Wenn PlayOn aktiviert ist
                                if (MPPlayOn == true)
                                {
                                    //PlayOn deaktivieren
                                    MPPlayOn = false;
                                }
                                //Wenn PlayOn nicht aktiviert ist
                                else
                                {
                                    //PlayOn aktivieren
                                    MPPlayOn = true;
                                }
                            }
                        }
                        //Variabeln zurücksetzen
                        MPButtonPressed = "none";
                        MPButtonPressStart = DateTime.MinValue;
                    }
                }
            }


            //Mediaplayer, Status festlegen
            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
            MPState = MediaPlayer.State.ToString();

            //Mediaplayer, Aktuellen Song, Daten laden
            if (MediaPlayer.Queue.Count > 0)
            {
                string tActiveSongName = MediaPlayer.Queue.ActiveSong.Name;
                string tActiveSongArtist = MediaPlayer.Queue.ActiveSong.Artist.Name;
                string tActiveSongAlbum = MediaPlayer.Queue.ActiveSong.Album.Name;
                //Wenn Aktueller Song anders als Momentaner Song
                if (ActiveSongName != tActiveSongName | ActiveSongArtist != tActiveSongArtist | ActiveSongAlbum != tActiveSongAlbum)
                {
                    //Song Daten erneuern
                    ActiveSongName = tActiveSongName;
                    ActiveSongArtist = tActiveSongArtist;
                    ActiveSongAlbum = tActiveSongAlbum;
                    MPSongName.Text = ActiveSongName;
                    MPSongArtist.Text = ActiveSongArtist;
                    MPSongAlbum.Text = ActiveSongAlbum;
                    FPSongName.Text = ActiveSongName;
                    FPSongArtist.Text = ActiveSongArtist;
                    FPSongAlbum.Text = ActiveSongAlbum;

                    //Gesamtzeit erstellen
                    ActiveSongDuration = MediaPlayer.Queue.ActiveSong.Duration;

                    //Wenn Bild vorhanden //Bild laden
                    if (MediaPlayer.Queue.ActiveSong.Album.HasArt)
                    {
                        AlbumStream = MediaPlayer.Queue.ActiveSong.Album.GetAlbumArt();
                        WriteableBitmap AlbumArtImage = new WriteableBitmap(1, 1);
                        AlbumArtImage.SetSource(AlbumStream);
                        AlbumArtImage = AlbumArtImage.Resize(348, 348, WriteableBitmapExtensions.Interpolation.Bilinear);
                        MPImageAlbumImage.Source = AlbumArtImage;
                        FPImageAlbumImage.Source = AlbumArtImage;
                        //Neue Tile Bilder erstellen
                        CreateNewTiles(AlbumStream);
                    }
                    //Wenn kein Bild vorhanden //Leeres Bild laden
                    else
                    {
                        //Album Art
                        FPImageAlbumImage.Source = new BitmapImage(new Uri("Images/300X300.png", UriKind.Relative));
                        MPImageAlbumImage.Source = new BitmapImage(new Uri("Images/300X300.png", UriKind.Relative));
                    }
                }
            }


            //Wenn Player läuft, Zeit errechnen
            if (MPState == "Playing")
            {
                //Zeit errechnen
                try
                {
                    //Gesamtzeit zerlegen
                    TimeSpan TSDuration = ActiveSongDuration;
                    string SongDuration = CreateDurationString(TSDuration);
                    //Wenn gesamtzeit nicht ermittelt werden kann
                    if (SongDuration == "00:00:00")
                    {
                        SongDuration = "⚠";
                    }

                    //Aktuelle Zeit ermitteln
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    TimeSpan ActiveSongPlayTime = MediaPlayer.PlayPosition;
                    string SongPlayTime = CreateDurationString(ActiveSongPlayTime);

                    // Wenn Select and Play aktiv
                    if (SelectAndPlay == true)
                    {
                        // Wenn Lied nur noch weniger als eine Minute läuft
                        if (ActiveSongPlayTime.Add(new TimeSpan(0,0,1)) > TSDuration)
                        {
                            PlayNextSongIfPaused = true;
                        }
                    }

                    //Wenn Anspielen aktiviert ist, und Anspielzeit abgelaufen ist
                    if (MPPlayOn == true & ActiveSongPlayTime.TotalSeconds > PlayOnTime)
                    {
                        //Versuchen nächsten Song zu laden
                        try
                        {
                            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                            MediaPlayer.MoveNext();
                        }
                        catch
                        {
                        }
                    }
                    //Wenn anspielen nicht aktiviert ist
                    else
                    {
                        //Zeiten eintragen
                        MPPlayTime.Text = SongPlayTime;
                        FPPlayTime.Text = SongPlayTime;
                        FPTime.Text = SongDuration;

                        //Wenn gesamtzeit nicht ermittelt werden kann
                        if (SongDuration == "⚠")
                        {
                            //Warnsymbol ausgeben
                            MPTime.Text = "⚠";
                            //Wenn Orientierung Portrait
                            if (OrientationPortait == true)
                            {
                                MPStatBar.Width = 260;
                                MPStatBarSmall.Width = 0;
                                FPStatBar.Width = 350;
                            }
                            //Wenn Orientierung Landscape
                            else
                            {
                                MPStatBar.Width = 200;
                                MPStatBarSmall.Width = 2;
                                MPStatBarSmall.Height = 0;
                                FPStatBar.Width = 350;
                            }
                        }

                        //Wenn Gesamtzeit ermittelt werden kann
                        else
                        {
                            //Gesamtzeit ausgeben
                            MPTime.Text = SongDuration;

                            //Bar, Prozent ermitteln
                            double ges = ActiveSongDuration.TotalSeconds;
                            double pos = ActiveSongPlayTime.TotalSeconds;
                            double per = Convert.ToDouble(100) / ges * pos;

                            //Wenn Orientierung Portrait
                            if (OrientationPortait == true)
                            {
                                //Bar ermitteln, mittleren Player 
                                double bar = Convert.ToDouble(260) / Convert.ToDouble(100) * per;
                                MPStatBar.Width = bar;
                                //Bar ermitteln kleiner Player
                                bar = Convert.ToDouble(480) / Convert.ToDouble(100) * per;
                                MPStatBarSmall.Width = bar;
                                MPStatBarSmall.Height = 2;
                                //Bar ermitteln Fullscreen Player
                                bar = Convert.ToDouble(350) / Convert.ToDouble(100) * per;
                                FPStatBar.Width = bar;
                            }
                            //Wenn Orientierung Landscape
                            else
                            {
                                //Bar ermitteln, mittleren Player 
                                double bar = Convert.ToDouble(200) / Convert.ToDouble(100) * per;
                                MPStatBar.Width = bar;
                                //Bar ermitteln kleiner Player
                                bar = Convert.ToDouble(480) / Convert.ToDouble(100) * per;
                                MPStatBarSmall.Width = 2;
                                MPStatBarSmall.Height = bar;
                                //Bar ermitteln Fullscreen Player
                                bar = Convert.ToDouble(350) / Convert.ToDouble(100) * per;
                                FPStatBar.Width = bar;
                            }
                        }
                    }
                }
                catch
                {
                }
            }


            //Wenn Player läuft, Buttons, Bilder umstellen
            if (MPState == "Playing")
            {
                //Button Wiedergabe Stoppen umstellen
                BtnStopPlayback.Content = MyMusicPlayer.Resources.AppResources.Z001_Playback;

                //Wenn PlayOn deaktiviert ist
                if (MPPlayOn == true)
                {
                    //Bild auf Play setzen
                    if (ForegroundColor == "#FFFFFFFF")
                    {
                        MPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Dark.png", UriKind.Relative));
                        FPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Dark.png", UriKind.Relative));
                    }
                    else
                    {
                        MPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Light.png", UriKind.Relative));
                        FPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Light.png", UriKind.Relative));
                    }
                }
                //Wenn PlayOn nicht aktiv ist
                else
                {
                    //Bild auf Pause setzen
                    if (ForegroundColor == "#FFFFFFFF")
                    {
                        MPPlayPause.Source = new BitmapImage(new Uri("Images/Pause.Dark.png", UriKind.Relative));
                        FPPlayPause.Source = new BitmapImage(new Uri("Images/Pause.Dark.png", UriKind.Relative));
                    }
                    else
                    {
                        MPPlayPause.Source = new BitmapImage(new Uri("Images/Pause.Light.png", UriKind.Relative));
                        FPPlayPause.Source = new BitmapImage(new Uri("Images/Pause.Light.png", UriKind.Relative));
                    }
                    //PlayOn Hintergrund leeren
                    MPPlayOnBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
                    FPPlayOnBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
                }
            }



            //Wenn Player Stoppt oder Pause, Bild umstellen
            if (MPState == "Stopped" | MPState == "Paused")
            {
                // Wenn Select and Play Aktiviert und PlayControl Play ist
                if (SelectAndPlay == true & PlayControl == "Play")
                {
                    // Wenn beim Pausieren auf nächsten Song gestellt wird
                    if (PlayNextSongIfPaused == true)
                    {
                        // Variable ob Mediaplayer gesoppt wird
                        bool StopPlayer = false;
                        // Versuchen aktuelle ID des Songs zu ermitteln
                        int ID = -1;
                        for (int i = 0; i < ListActivPlaylist.Count(); i++)
                        {
                            // Wenn aktueller Song ist
                            if (PlayArtist == ListActivPlaylist[i].Artist & PlayAlbum == ListActivPlaylist[i].Album & PlaySong == ListActivPlaylist[i].Song)
                            {
                                ID = i;
                                break;
                            }
                        }
                        // Wenn ID nicht geladen wurde
                        if (ID == -1)
                        {
                            ID = PlayID;
                        }
                        // ID erhöhen
                        ID++;
                        // Wenn keine Songs mehr vorhanden
                        if (ID >= ListActivPlaylist.Count())
                        {
                            // Wenn Repead eingeschaltet
                            if (SetRepead == true)
                            {
                                ID = 0;
                            }
                            // Wenn Repead nicht eingeschaltet
                            else
                            {
                                ID = 0;
                                StopPlayer = true;
                            }
                        }
                        // Wenn nächster Song abgespielt wird
                        if (StopPlayer == false)
                        {
                            SelectAndPlay_Play(ID);
                        }
                        // Wenn Player angehalten wird
                        else
                        {
                            PlayControl = "None";
                        }
                        // Angeben das nächster Song nicht geladen wird
                        PlayNextSongIfPaused = false;
                    }
                }

                // Wenn Select and Play nicht aktiviert ist
                else
                {
                    //Button Wiedergabe Stoppen umstellen
                    BtnStopPlayback.Content = MyMusicPlayer.Resources.AppResources.Z001_Pause;

                    //Bild auf Play stellen
                    if (ForegroundColor == "#FFFFFFFF")
                    {
                        MPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Dark.png", UriKind.Relative));
                        FPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Dark.png", UriKind.Relative));
                    }
                    else
                    {
                        MPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Light.png", UriKind.Relative));
                        FPPlayPause.Source = new BitmapImage(new Uri("Images/Play.Light.png", UriKind.Relative));
                    }
                    //PlayOn deaktivieren
                    MPPlayOn = false;
                }
            }


            //Wenn Player Stoppt, Zeit und Balken zurücksetzten
            if (MPState == "Stopped")
            {
                //Button Wiedergabe Stoppen umstellen
                BtnStopPlayback.Content = MyMusicPlayer.Resources.AppResources.Z001_Stop;

                //Balken zurücksetzen
                MPStatBarSmall.Width = 0;
                MPStatBar.Width = 0;
                FPStatBar.Width = 0;
                //Zeiten zurücksetzen
                MPPlayTime.Text = "00:00:00";
                MPTime.Text = "00:00:00";
                FPPlayTime.Text = "00:00:00";
                FPTime.Text = "00:00:00";
            }


            //Benachrichtigung Blinken
            if (MPPlayOn == true)
            {
                if (DateTime.Now.Millisecond > 500)
                {
                    MPPlayOnBg.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
                    FPPlayOnBg.Fill = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, -1);
                }
                else
                {
                    MPPlayOnBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
                    FPPlayOnBg.Fill = ConvertToSolidColorBrush("#00000000", -1);
                }
            }



            //Mediaplayer klein, Animation öffnen
            if (MPAnimationStatus == "Open")
            {
                //Mediaplayer klein, Animation öffnen, wenn Orientierung Portrait
                if (OrientationPortait == true)
                {
                    //Wenn Animation noch nicht läuft
                    if (MPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        MPAnimationStart = DateTime.Now;
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime MPAnimationNow = DateTime.Now;
                        int MPAnimationNowMS = (MPAnimationNow.Hour * 3600000) + (MPAnimationNow.Minute * 60000) + (MPAnimationNow.Second * 1000) + MPAnimationNow.Millisecond;
                        int MPAnimationStartMS = (MPAnimationStart.Hour * 3600000) + (MPAnimationStart.Minute * 60000) + (MPAnimationStart.Second * 1000) + MPAnimationStart.Millisecond;
                        //Player öffnen
                        int MPMSmax = 200;
                        //Wenn Animation noch läuft
                        if ((MPAnimationNowMS - MPAnimationStartMS) < MPMSmax)
                        {
                            int MPAnimationP = 10000000 / MPMSmax * (MPAnimationNowMS - MPAnimationStartMS) / 100000;
                            int MPAnimationM = 12800000 / 100 * MPAnimationP / 100000;
                            GRMediaPlayer.Margin = new Thickness(0, 0, 0, (0 - (128 - MPAnimationM)));
                            MPStatBarSmall.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                            LBSongs.Margin = new Thickness(0, -18, 0, 200);
                            LBPlaylists.Margin = new Thickness(0, -18, 0, 200);
                            LBCurrentPlaylist.Margin = new Thickness(0, -18, 0, 200);
                            SVSetting.Margin = new Thickness(0, -18, 0, 200);
                            SVAbout.Margin = new Thickness(0, 0, -18, 200);
                            MPAnimationStart = DateTime.MinValue;
                            if (ForegroundColor == "#FF000000")
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Down.Light.png", UriKind.Relative));
                            }
                            else
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Down.Dark.png", UriKind.Relative));
                            }
                            MPAnimationStatus = "Opened";
                            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        }
                    }
                }

                //Mediaplayer klein, Animation öffnen, wenn Orientierung Landscape
                else
                {
                    //Wenn Animation noch nicht läuft, Zeit erstellen
                    if (MPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        MPAnimationStart = DateTime.Now;
                    }
                    //Animation vollständig abschließen
                    else if (MPAnimationStart == DateTime.MinValue.AddSeconds(1))
                    {
                        //Margin der Inhalte erstellen
                        LBSongs.Margin = new Thickness(0, -18, 300, 0);
                        LBPlaylists.Margin = new Thickness(0, -18, 300, 0);
                        LBCurrentPlaylist.Margin = new Thickness(0, -18, 300, 0);
                        SVSetting.Margin = new Thickness(0, -18, 300, 0);
                        SVAbout.Margin = new Thickness(0, -18, 300, 0);
                        SPTopButtons.Margin = new Thickness(6, 12, 324, 0);
                        TBSearch.Margin = new Thickness(10, -6, 300, 0);
                        RTSearch.Margin = new Thickness(25, 9, 315, 0);
                        //Animation vollständig beenden
                        MPAnimationStart = DateTime.MinValue;
                        MPAnimationStatus = "Opened";
                        Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime MPAnimationNow = DateTime.Now;
                        int MPAnimationNowMS = (MPAnimationNow.Hour * 3600000) + (MPAnimationNow.Minute * 60000) + (MPAnimationNow.Second * 1000) + MPAnimationNow.Millisecond;
                        int MPAnimationStartMS = (MPAnimationStart.Hour * 3600000) + (MPAnimationStart.Minute * 60000) + (MPAnimationStart.Second * 1000) + MPAnimationStart.Millisecond;
                        //Player öffnen
                        int MPMSmax = 200;
                        //Wenn Animation noch läuft
                        if ((MPAnimationNowMS - MPAnimationStartMS) < MPMSmax)
                        {
                            int MPAnimationP = 10000000 / MPMSmax * (MPAnimationNowMS - MPAnimationStartMS) / 100000;
                            int MPAnimationM = 22800000 / 100 * MPAnimationP / 100000;
                            GRMediaPlayer.Margin = new Thickness(0, 0, (0 - (228 - MPAnimationM)), 0);
                            MPStatBarSmall.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                            if (ForegroundColor == "#FF000000")
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Right.Light.png", UriKind.Relative));
                            }
                            else
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Right.Dark.png", UriKind.Relative));
                            }
                            //Zeit einstellen, das Animation nach letzten Frame abgeschlossen wird
                            MPAnimationStart = DateTime.MinValue.AddSeconds(1);
                        }
                    }
                }
            }



            //Mediaplayer klein, Animation schließen
            if (MPAnimationStatus == "Close")
            {
                //Mediaplayer klein, Animation schließen, wenn Orientierung Portrait
                if (OrientationPortait == true)
                {
                    //Wenn Animation noch nicht läuft
                    if (MPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        MPAnimationStart = DateTime.Now;
                        //LBSongs wieder vergrößern
                        LBSongs.Margin = new Thickness(0, -18, 0, 72);
                        LBPlaylists.Margin = new Thickness(0, -18, 0, 72);
                        LBCurrentPlaylist.Margin = new Thickness(0, -18, 0, 72);
                        SVSetting.Margin = new Thickness(0, -18, 0, 72);
                        SVAbout.Margin = new Thickness(0, -18, 0, 72);
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime MPAnimationNow = DateTime.Now;
                        int MPAnimationNowMS = (MPAnimationNow.Hour * 3600000) + (MPAnimationNow.Minute * 60000) + (MPAnimationNow.Second * 1000) + MPAnimationNow.Millisecond;
                        int MPAnimationStartMS = (MPAnimationStart.Hour * 3600000) + (MPAnimationStart.Minute * 60000) + (MPAnimationStart.Second * 1000) + MPAnimationStart.Millisecond;
                        //Player öffnen
                        int MPMSmax = 200;
                        //Wenn Animation noch läuft
                        if ((MPAnimationNowMS - MPAnimationStartMS) < MPMSmax)
                        {
                            int MPAnimationP = 10000000 / MPMSmax * (MPAnimationNowMS - MPAnimationStartMS) / 100000;
                            int MPAnimationM = 12800000 / 100 * MPAnimationP / 100000;
                            GRMediaPlayer.Margin = new Thickness(0, 0, 0, (0 - (MPAnimationM)));
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRMediaPlayer.Margin = new Thickness(0, 0, 0, -128);
                            MPStatBarSmall.Visibility = System.Windows.Visibility.Visible;
                            MPAnimationStart = DateTime.MinValue;
                            if (ForegroundColor == "#FF000000")
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Up.Light.png", UriKind.Relative));
                            }
                            else
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Up.Dark.png", UriKind.Relative));
                            }
                            MPAnimationStatus = "Closed";
                            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        }
                    }
                }

                //Mediaplayer klein, Animation schließen, wenn Orientierung Landscape
                else
                {
                    //Wenn Animation noch nicht läuft, beim ersten durchlauf Margins der Listen ändern
                    if (MPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        MPAnimationStart = DateTime.MinValue.AddSeconds(1);
                        //LBSongs wieder vergrößern
                        LBSongs.Margin = new Thickness(0, -18, 72, 0);
                        LBPlaylists.Margin = new Thickness(0, -18, 72, 0);
                        LBCurrentPlaylist.Margin = new Thickness(0, -18, 72, 0);
                        SVSetting.Margin = new Thickness(0, -18, 72, 0);
                        SVAbout.Margin = new Thickness(0, -18, 72, 0);
                        SPTopButtons.Margin = new Thickness(6, 12, 96, 0);
                        TBSearch.Margin = new Thickness(10, -6, 72, 0);
                        RTSearch.Margin = new Thickness(25, 9, 87, 0);
                    }
                    //Wenn Animation läuft, beim zweiten Durchlauf Zeit umstellen
                    else if(MPAnimationStart == DateTime.MinValue.AddSeconds(1))
                    {
                        //Startzeit festlegen
                        MPAnimationStart = DateTime.Now;
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime MPAnimationNow = DateTime.Now;
                        int MPAnimationNowMS = (MPAnimationNow.Hour * 3600000) + (MPAnimationNow.Minute * 60000) + (MPAnimationNow.Second * 1000) + MPAnimationNow.Millisecond;
                        int MPAnimationStartMS = (MPAnimationStart.Hour * 3600000) + (MPAnimationStart.Minute * 60000) + (MPAnimationStart.Second * 1000) + MPAnimationStart.Millisecond;
                        //Player öffnen
                        int MPMSmax = 200;
                        //Wenn Animation noch läuft
                        if ((MPAnimationNowMS - MPAnimationStartMS) < MPMSmax)
                        {
                            int MPAnimationP = 10000000 / MPMSmax * (MPAnimationNowMS - MPAnimationStartMS) / 100000;
                            int MPAnimationM = 22800000 / 100 * MPAnimationP / 100000;
                            GRMediaPlayer.Margin = new Thickness(0, 0, (0 - MPAnimationM), 0);
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRMediaPlayer.Margin = new Thickness(0, 0, -228, 0);
                            MPStatBarSmall.Visibility = System.Windows.Visibility.Visible;
                            MPAnimationStart = DateTime.MinValue;
                            if (ForegroundColor == "#FF000000")
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Left.Light.png", UriKind.Relative));
                            }
                            else
                            {
                                MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Left.Dark.png", UriKind.Relative));
                            }
                            MPAnimationStatus = "Closed";
                            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        }
                    }
                }
            }



            //Mediaplayer groß, Animation öffnen
            if (FPAnimationStatus == "Open")
            {
                //Mediaplayer groß, Animation öffnen, wenn Orientierung Portrait
                if (OrientationPortait == true)
                {
                    //Wenn Animation noch nicht läuft
                    if (FPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        FPAnimationStart = DateTime.Now;
                        GRFullscreenPlayer.Visibility = System.Windows.Visibility.Visible;
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime FPAnimationNow = DateTime.Now;
                        int FPAnimationNowMS = (FPAnimationNow.Hour * 3600000) + (FPAnimationNow.Minute * 60000) + (FPAnimationNow.Second * 1000) + FPAnimationNow.Millisecond;
                        int FPAnimationStartMS = (FPAnimationStart.Hour * 3600000) + (FPAnimationStart.Minute * 60000) + (FPAnimationStart.Second * 1000) + FPAnimationStart.Millisecond;
                        //Player öffnen
                        int FPMSmax = 300;
                        //Wenn Animation noch läuft
                        if ((FPAnimationNowMS - FPAnimationStartMS) < FPMSmax)
                        {
                            int FPAnimationP = 10000000 / FPMSmax * (FPAnimationNowMS - FPAnimationStartMS) / 100000;
                            int FPAnimationM = 48000000 / 100 * FPAnimationP / 100000;
                            GRFullscreenPlayer.Margin = new Thickness((-480 + FPAnimationM), 0, 0, 0);
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRFullscreenPlayer.Margin = new Thickness(0, 0, 0, 0);
                            FPAnimationStart = DateTime.MinValue;
                            FPAnimationStatus = "Opened";
                            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        }
                    }
                }

                //Mediaplayer groß, Animation öffnen, wenn Orientierung Landscape
                else
                {
                    //Wenn Animation noch nicht läuft
                    if (FPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        FPAnimationStart = DateTime.Now;
                        GRFullscreenPlayer.Visibility = System.Windows.Visibility.Visible;
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime FPAnimationNow = DateTime.Now;
                        int FPAnimationNowMS = (FPAnimationNow.Hour * 3600000) + (FPAnimationNow.Minute * 60000) + (FPAnimationNow.Second * 1000) + FPAnimationNow.Millisecond;
                        int FPAnimationStartMS = (FPAnimationStart.Hour * 3600000) + (FPAnimationStart.Minute * 60000) + (FPAnimationStart.Second * 1000) + FPAnimationStart.Millisecond;
                        //Player öffnen
                        int FPMSmax = 300;
                        //Wenn Animation noch läuft
                        if ((FPAnimationNowMS - FPAnimationStartMS) < FPMSmax)
                        {
                            int FPAnimationP = 10000000 / FPMSmax * (FPAnimationNowMS - FPAnimationStartMS) / 100000;
                            int FPAnimationM = 48000000 / 100 * FPAnimationP / 100000;
                            GRFullscreenPlayer.Margin = new Thickness(0, (480 - FPAnimationM), 0, 0);
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRFullscreenPlayer.Margin = new Thickness(0, 0, 0, 0);
                            FPAnimationStart = DateTime.MinValue;
                            FPAnimationStatus = "Opened";
                            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        }
                    }
                }
            }



            //Mediaplayer groß, Animation schließen
            if (FPAnimationStatus == "Close")
            {
                //Mediaplayer groß, Animation schließen, wenn Orientierung Portrait
                if (OrientationPortait == true)
                {
                    //Wenn Animation noch nicht läuft
                    if (FPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        FPAnimationStart = DateTime.Now;
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime FPAnimationNow = DateTime.Now;
                        int FPAnimationNowMS = (FPAnimationNow.Hour * 3600000) + (FPAnimationNow.Minute * 60000) + (FPAnimationNow.Second * 1000) + FPAnimationNow.Millisecond;
                        int FPAnimationStartMS = (FPAnimationStart.Hour * 3600000) + (FPAnimationStart.Minute * 60000) + (FPAnimationStart.Second * 1000) + FPAnimationStart.Millisecond;
                        //Player öffnen
                        int FPMSmax = 300;
                        //Wenn Animation noch läuft
                        if ((FPAnimationNowMS - FPAnimationStartMS) < FPMSmax)
                        {
                            int FPAnimationP = 10000000 / FPMSmax * (FPAnimationNowMS - FPAnimationStartMS) / 100000;
                            int FPAnimationM = 48000000 / 100 * FPAnimationP / 100000;
                            GRFullscreenPlayer.Margin = new Thickness((0 - FPAnimationM), 0, 0, 0);
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRFullscreenPlayer.Margin = new Thickness(-480, 0, 0, 0);
                            GRFullscreenPlayer.Visibility = System.Windows.Visibility.Collapsed;
                            FPAnimationStart = DateTime.MinValue;
                            FPAnimationStatus = "Closed";
                            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        }
                    }
                }

                //Mediaplayer groß, Animation schließen, wenn Orientierung Landscape
                else
                {
                    //Wenn Animation noch nicht läuft
                    if (FPAnimationStart == DateTime.MinValue)
                    {
                        //Startzeit festlegen
                        FPAnimationStart = DateTime.Now;
                    }
                    //Wenn Animation bereits läuft
                    else
                    {
                        //Zeiten in Millisekunden umwandeln
                        DateTime FPAnimationNow = DateTime.Now;
                        int FPAnimationNowMS = (FPAnimationNow.Hour * 3600000) + (FPAnimationNow.Minute * 60000) + (FPAnimationNow.Second * 1000) + FPAnimationNow.Millisecond;
                        int FPAnimationStartMS = (FPAnimationStart.Hour * 3600000) + (FPAnimationStart.Minute * 60000) + (FPAnimationStart.Second * 1000) + FPAnimationStart.Millisecond;
                        //Player öffnen
                        int FPMSmax = 300;
                        //Wenn Animation noch läuft
                        if ((FPAnimationNowMS - FPAnimationStartMS) < FPMSmax)
                        {
                            int FPAnimationP = 10000000 / FPMSmax * (FPAnimationNowMS - FPAnimationStartMS) / 100000;
                            int FPAnimationM = 48000000 / 100 * FPAnimationP / 100000;
                            GRFullscreenPlayer.Margin = new Thickness(0, FPAnimationM, 0, 0);
                        }
                        //Wenn Animation beendet
                        else
                        {
                            //Animation beenden
                            GRFullscreenPlayer.Margin = new Thickness(0, 480, 0, 0);
                            GRFullscreenPlayer.Visibility = System.Windows.Visibility.Collapsed;
                            FPAnimationStart = DateTime.MinValue;
                            FPAnimationStatus = "Closed";
                            Timer_MediaPlayer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        }
                    }
                }
            }


            //Pivot Items kontrollieren um nicht aktive Items auszublenden
            if (MainPivotItem.SelectedIndex != PivotIndex)
            {
                //Wenn Menüs offen sind, Pivot Item zurücksetzen
                if (MenuOpen == true)
                {
                    MainPivotItem.SelectedIndex = PivotIndex;
                }

                //Wenn kein Menü offen ist, nicht verwendete Bereiche verbergen
                else
                {
                    //Index umstellen
                    int PivotIndexOld = PivotIndex;
                    PivotIndex = MainPivotItem.SelectedIndex;

                    //Pivot Items verbergen
                    if (PivotIndex == 0)
                    {
                        // Bauteile sichtbar und unsichtbar machen
                        LBSongs.Visibility = System.Windows.Visibility.Visible;
                        LBPlaylists.Visibility = System.Windows.Visibility.Collapsed;
                        LBCurrentPlaylist.Visibility = System.Windows.Visibility.Collapsed;
                        SVSetting.Visibility = System.Windows.Visibility.Collapsed;
                        SVAbout.Visibility = System.Windows.Visibility.Collapsed;
                        // Wenn vorheriger Index 4 war
                        if (PivotIndexOld == 4)
                        {
                            // Suche schließen wenn offen
                            if (SearchStatus == "Opened")
                            {
                                CloseSearch();
                            }
                        }
                        // Wenn vorheriger Index nicht 4 war
                        else
                        {
                            if (SearchStatus == "Opened")
                            {
                                TBSearch.Visibility = System.Windows.Visibility.Visible;
                                RTSearch.Visibility = System.Windows.Visibility.Visible;
                                SPTopButtons.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                TBSearch.Visibility = System.Windows.Visibility.Collapsed;
                                RTSearch.Visibility = System.Windows.Visibility.Collapsed;
                                SPTopButtons.Visibility = System.Windows.Visibility.Visible;
                            }
                        }
                        // Buttonleiste sichtbar machen
                        SPTopButtons01.Visibility = System.Windows.Visibility.Visible;
                        SPTopButtons02.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else if (PivotIndex == 1)
                    {
                        // Aktive Playlist löschen
                        LBCurrentPlaylist.ItemsSource = null;
                        // Bauteile sichtbar und unsichtbar machen
                        LBSongs.Visibility = System.Windows.Visibility.Collapsed;
                        LBPlaylists.Visibility = System.Windows.Visibility.Visible;
                        LBCurrentPlaylist.Visibility = System.Windows.Visibility.Collapsed;
                        SVSetting.Visibility = System.Windows.Visibility.Collapsed;
                        SVAbout.Visibility = System.Windows.Visibility.Collapsed;
                        // Wenn vorheriger Index 2 oder 3 war
                        if (PivotIndexOld == 2 | PivotIndexOld == 3)
                        {
                            // Suche schließen wenn offen
                            if (SearchStatus == "Opened")
                            {
                                CloseSearch();
                            }
                        }
                        // Wenn vorheriger Index nicht 2 oder 3 war
                        else
                        {
                            if (SearchStatus == "Opened")
                            {
                                TBSearch.Visibility = System.Windows.Visibility.Visible;
                                RTSearch.Visibility = System.Windows.Visibility.Visible;
                                SPTopButtons.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            else
                            {
                                TBSearch.Visibility = System.Windows.Visibility.Collapsed;
                                RTSearch.Visibility = System.Windows.Visibility.Collapsed;
                                SPTopButtons.Visibility = System.Windows.Visibility.Visible;
                            }
                        }
                        // Buttonsleiste Anzeigen
                        SPTopButtons01.Visibility = System.Windows.Visibility.Visible;
                        SPTopButtons02.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else if (PivotIndex == 2)
                    {
                        // Wenn Select and Play aktiviert ist
                        if (SelectAndPlay == true)
                        {
                            // PlayID auslesen
                            PlayID = -1;
                            // Fall Mediaplayer Abspielt
                            if (MediaPlayer.State.ToString() == "Playing")
                            {
                                // Mediaplayer Daten laden
                                string playingArtist = MediaPlayer.Queue.ActiveSong.Artist.Name;
                                string playingAlbum = MediaPlayer.Queue.ActiveSong.Album.Name;
                                string playingSong = MediaPlayer.Queue.ActiveSong.Name;
                                // PlayID ermitteln
                                for (int i = 0; i < ListActivPlaylist.Count(); i++)
                                {
                                    if (ListActivPlaylist[i].Artist == playingArtist & ListActivPlaylist[i].Album == playingAlbum & ListActivPlaylist[i].Song == playingSong)
                                    {
                                        PlayID = i;
                                        break;
                                    }
                                }
                            }
                            // Wenn PlayID anders als -1
                            if (PlayID != -1)
                            {
                                // PlayControl auf Play setzen
                                PlayControl = "Play";
                            }

                            // Aktive Playliste durchlaufen und sortieren
                            for (int i = 0; i < ListActivPlaylist.Count(); i++)
                            {
                                // Wenn Song momentan abgespielt wird
                                if (PlayID == i)
                                {
                                    ListActivPlaylist[i].Background = SelectedBackgroundColor;
                                    ListActivPlaylist[i].Foreground = SelectedForegroundColor;
                                }
                                // Wenn Song nicht abgespielt wird
                                else
                                {
                                    ListActivPlaylist[i].Background = SongBackgroundColor;
                                    ListActivPlaylist[i].Foreground = SongForegroundColor;
                                }
                                ListActivPlaylist[i].FontSize = ImageSize;
                                ListActivPlaylist[i].ExtendedInfoFontSize = ((Convert.ToInt32(ImageSize) / 2) + (Convert.ToInt32(ImageSize) / 4)).ToString();
                                ListActivPlaylist[i].ImageSize = (Convert.ToInt32(ImageSize) + (Convert.ToInt32(ImageSize) / 2)).ToString();
                                ListActivPlaylist[i].ImageSize2 = (Convert.ToInt32(ImageSize) + (Convert.ToInt32(ImageSize) / 2)).ToString();
                                if (i > 0)
                                {
                                    if (SongForegroundColor == "#FFFFFFFF")
                                    {
                                        ListActivPlaylist[i].ImageSource2 = "/Images/Arrow.Up.Big.Dark.png";
                                    }
                                    else
                                    {
                                        ListActivPlaylist[i].ImageSource2 = "/Images/Arrow.Up.Big.Light.png";
                                    }
                                }
                                if (i < ListActivPlaylist.Count() - 1)
                                {
                                    if (SongForegroundColor == "#FFFFFFFF")
                                    {
                                        ListActivPlaylist[i].ImageSource = "/Images/Arrow.Down.Big.Dark.png";
                                    }
                                    else
                                    {
                                        ListActivPlaylist[i].ImageSource = "/Images/Arrow.Down.Big.Light.png";
                                    }
                                }
                                if (SongForegroundColor == "#FFFFFFFF")
                                {
                                    ListActivPlaylist[i].ImageSource3 = "/Images/Delete.Dark.png";
                                }
                                else
                                {
                                    ListActivPlaylist[i].ImageSource = "/Images/Delete.Light.png";
                                }
                            }
                            LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                            // Bauteile Sichtbar und unsichtbar machen
                            LBSongs.Visibility = System.Windows.Visibility.Collapsed;
                            LBPlaylists.Visibility = System.Windows.Visibility.Collapsed;
                            LBCurrentPlaylist.Visibility = System.Windows.Visibility.Visible;
                            SVSetting.Visibility = System.Windows.Visibility.Collapsed;
                            SVAbout.Visibility = System.Windows.Visibility.Collapsed;
                            TBSearch.Visibility = System.Windows.Visibility.Collapsed;
                            RTSearch.Visibility = System.Windows.Visibility.Collapsed;
                            SPTopButtons.Visibility = System.Windows.Visibility.Collapsed;
                            // Wenn Select and Play aktiv
                            if (SelectAndPlay == true)
                            {
                                SPTopButtons.Visibility = System.Windows.Visibility.Visible;
                            }
                            // Wenn Select and Play nich aktiv
                            else
                            {
                                SPTopButtons.Visibility = System.Windows.Visibility.Collapsed;
                            }
                            SPTopButtons01.Visibility = System.Windows.Visibility.Collapsed;
                            SPTopButtons02.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            if (PivotIndexOld < PivotIndex)
                            {
                                MainPivotItem.SelectedIndex = 3;
                            }
                            else
                            {
                                MainPivotItem.SelectedIndex = 1;
                            }
                        }

                        // Suche schließen wenn offen
                        if (SearchStatus == "Opened")
                        {
                            CloseSearch();
                        }
                    }
                    else if (PivotIndex == 3)
                    {
                        // Aktive Playlist löschen
                        LBCurrentPlaylist.ItemsSource = null;
                        // Bauteile sichtbar und unsichtbar machen
                        LBSongs.Visibility = System.Windows.Visibility.Collapsed;
                        LBPlaylists.Visibility = System.Windows.Visibility.Collapsed;
                        SVSetting.Visibility = System.Windows.Visibility.Visible;
                        SVAbout.Visibility = System.Windows.Visibility.Collapsed;
                        TBSearch.Visibility = System.Windows.Visibility.Collapsed;
                        RTSearch.Visibility = System.Windows.Visibility.Collapsed;
                        SPTopButtons.Visibility = System.Windows.Visibility.Collapsed;
                        // Suche schließen wenn offen
                        if (SearchStatus == "Opened")
                        {
                            CloseSearch();
                        }
                    }
                    else if (PivotIndex == 4)
                    {
                        // Bauteile sichtbar und unsichtbar machen
                        LBSongs.Visibility = System.Windows.Visibility.Collapsed;
                        LBPlaylists.Visibility = System.Windows.Visibility.Collapsed;
                        SVSetting.Visibility = System.Windows.Visibility.Collapsed;
                        SVAbout.Visibility = System.Windows.Visibility.Visible;
                        TBSearch.Visibility = System.Windows.Visibility.Collapsed;
                        RTSearch.Visibility = System.Windows.Visibility.Collapsed;
                        SPTopButtons.Visibility = System.Windows.Visibility.Collapsed;
                        // Suche schließen wenn offen
                        if (SearchStatus == "Opened")
                        {
                            CloseSearch();
                        }
                    }
                }
            }



            //Timer ablauf
            if (TimerIsRunning == true)
            {
                //Zeitspanne erstellen
                TimeSpan TimerTimeSpan = StopTimerTimer - DateTime.Now;
                //Wenn Zeit noch aktiv
                if (TimerTimeSpan.TotalMilliseconds > 0)
                {
                    //Sekunden gesamt erstellen
                    int secges = Convert.ToInt32(TimerTimeSpan.TotalSeconds);
                    //Stunden errechen
                    int h = secges / 3600;
                    string sh = h.ToString();
                    if (sh.Length < 2)
                    {
                        sh = "0" + sh;
                    }
                    //Minuten errechnen
                    int m = (secges - (h * 3600)) / 60;
                    string sm = m.ToString();
                    if (sm.Length < 2)
                    {
                        sm = "0" + sm;
                    }
                    //Sekunden errechen
                    int s = (secges - (h * 3600) - (m * 60));
                    string ss = s.ToString();
                    if (ss.Length < 2)
                    {
                        ss = "0" + ss;
                    }
                    //String ausgeben
                    string RestTime = sh + ":" + sm + ":" + ss;
                    TB_Timer.Text = RestTime;
                }
                //Wenn Zeit abgelaufen
                else
                {
                    //Wiedergabe stoppen
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.Stop();
                        Application.Current.Terminate();
                    }
                    catch
                    {
                    }
                    //Timer löschen
                    TB_Timer.IsReadOnly = false;
                    TB_Timer.Text = "";
                    TimerIsRunning = false;
                    //Bilder umwandeln
                    ImgTimerDelete.Opacity = 0.5;
                    ImgTimerPlay.Opacity = 1.0;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        # region Einstellungen
        //Einstellungen
        //---------------------------------------------------------------------------------------------------------------------------------
        //Variabeln
        string Timer_Settings_Action = "None";
        string SearchStatus = "Closed";
        string SearchAction = "None";
        DateTime SearchStart = DateTime.MinValue;
        bool OrientationLock = false;
        DateTime Timer_Settings_DTStart = DateTime.MinValue;


        // Hilfe öffnen
        private void OpenHelp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Zur Hilfe Navigieren
            NavigationService.Navigate(new Uri("/Pages/Instructions.xaml", UriKind.Relative));
        }


        //Oreintation Lock
        private void ImgLock_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Orientation Lock aus
            if (OrientationLock == false)
            {
                //Drehung laden
                string NewOrientation = Orientation.ToString();
                //Wenn Orientierung Landscape
                if (NewOrientation == "LandscapeLeft" | NewOrientation == "LandscapeRight")
                {
                    this.SupportedOrientations = SupportedPageOrientation.Landscape;
                }
                //Wenn Drehung Portrait
                else
                {
                    this.SupportedOrientations = SupportedPageOrientation.Portrait;
                }

                //Angeben das Drehung Gelockt
                OrientationLock = true;
                ImgLock.Opacity = 1.0;
            }
            //Wenn OrientationsLock an
            else
            {
                //Orientierung aufheben
                this.SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

                //Angeben das Drehung Gelockt
                OrientationLock = false;
                ImgLock.Opacity = 0.5;
            }
        }


        // Select and Play
        private void ImgSelectAndPlayClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Wenn Select and Play nicht aktiv
            if (SelectAndPlay == false)
            {
                // Select and Play aktivieren
                SelectAndPlay = true;
                ImgSelectAndPlay.Opacity = 1.0;
                // Prüfen ob App unter dem Sperrbildschirm läuft
                if (PhoneApplicationService.Current.ApplicationIdleDetectionMode == IdleDetectionMode.Enabled)
                {
                    // App unter dem Sperrbildschirm ausführen
                    try
                    {
                        PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                        RunUnderLockScreen = true;
                        CreateSettings();
                    }
                    catch
                    {
                    }
                }
                // Repead ausschalten
                try
                {
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    MediaPlayer.IsRepeating = false;
                }
                catch
                {
                }
                // Shuffle ausschalten
                try
                {
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    MediaPlayer.IsShuffled = false;
                }
                catch
                {
                }
            }
            // Wenn Select and Play Aktiv
            else
            {
                // Select and play deaktivieren
                SelectAndPlay = false;
                ImgSelectAndPlay.Opacity = 0.5;
                // Wenn Repead eingeschaltet
                if (SetRepead == true)
                {
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.IsRepeating = true;
                    }
                    catch
                    {
                    }
                }
                // Wenn Shuffle eingeschaltet
                if (SetShuffle == true)
                {
                    try
                    {
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.IsShuffled = true;
                    }
                    catch
                    {
                    }
                }
            }
            // Einstellungen neu erstellen
            CreateSettings();
            // Listen Schließen
            CloseLists();
        }


        //Button um Cache zu erneuern
        private void BtnRenewCache_Click(object sender, RoutedEventArgs e)
        {
            //Timer angeben das Catch gelöscht wird
            Timer_Settings_Action = "ClearCatch";
            Timer_Settings.Start();
        }


        //Button um Wiedergabe zu stoppen
        private void BtnStopPlayback_Click(object sender, RoutedEventArgs e)
        {
            //Nach Abfrage, Mediaplayer stoppen
            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_StopPlaybackNote, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //Tile zurücksetzen
                CreateStandardTiles();
                try
                {
                    Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                    MediaPlayer.Stop();
                    DateTime temp = DateTime.Now;
                    for (int i = 0; i == 0; i=0)
                    {
                        if (MediaPlayer.State.ToString() == "Stopped")
                        {
                            Song s = Song.FromUri("empty", new Uri("empty.wma", UriKind.Relative));
                            MediaPlayer.Play(s);
                            if (temp.AddSeconds(2) < DateTime.Now)
                            {
                                Application.Current.Terminate();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }


        //Button um letzte Wiedergaben löschen
        private void BtnDeleteLastPlaybacks_Click(object sender, RoutedEventArgs e)
        {
            //Nach Abfrage, Mediaplayer stoppen
            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_DeleteLastPlaybacksNote, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //LastPlayback/LastPlayback.dat speichern
                LastPlaybackString = "";
                filestream = file.CreateFile("LastPlayback/LastPlayback.dat");
                sw = new StreamWriter(filestream);
                sw.Write(LastPlaybackString);
                sw.Flush();
                filestream.Close();
                //Liste Playlists neu erstellen
                ListPlaylist.Clear();
                List_Playlists("CreateNew", -1);
            }
        }


        //Button Einstellungen
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            //Angeben das Listen upgadatet werden beim zurück kommen
            UpdateListsAtStart = true;
            //Zu Design Settings gehen
            NavigationService.Navigate(new Uri("/Pages/SystemSettings.xaml", UriKind.Relative));
        }


        //Button Design Einstellungen
        private void BtnDesignSettings_Click(object sender, RoutedEventArgs e)
        {
            //Angeben das Listen upgadatet werden beim zurück kommen
            UpdateListsAtStart = true;
            //Zu Design Settings gehen
            NavigationService.Navigate(new Uri("/Pages/ColorSettings.xaml", UriKind.Relative));
        }


        //Button Tile Einstellungen
        private void BtnStartScreenSettings_Click(object sender, RoutedEventArgs e)
        {
            //Angeben das Listen upgadatet werden beim zurück kommen
            UpdateListsAtStart = true;
            //Zu Design Settings gehen
            NavigationService.Navigate(new Uri("/Pages/TileSettings.xaml", UriKind.Relative));
        }


        // Button Benachrichtigung
        private void GRNote_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Wenn Benachrichtigung ausgegeben wird das Catch erneuert wird
            if (Timer_Settings_Action == "RefreshList2")
            {
                // Neue Benachrichtigung laden
                TBNote.Text = MyMusicPlayer.Resources.AppResources.ZZ003_ClickClearCatch2;
                // Timer Zeit zurücksetzen
                Timer_Settings_DTStart = DateTime.MinValue;
                Timer_Settings_Action = "RefreshList3";
            }
            // Wenn Catch erneuert wird
            else if (Timer_Settings_Action == "RefreshList3")
            {
                // Catch leeren
                Timer_Settings_Action = "ClearCatch";
            }
        }


        //Timer Einstellungen
        void Timer_Settings_Tick(object sender, object e)
        {
            // Wenn zurück Button gedrückt wurde
            if (Timer_Settings_Action == "Back")
            {
                //Wenn Timer noch nicht läuft
                if (Timer_Settings_DTStart == DateTime.MinValue)
                {
                    //Timer setzen
                    Timer_Settings_DTStart  = DateTime.Now;
                    // Benachrichtigung ausgeben
                    GRNote.Visibility = System.Windows.Visibility.Visible;

                }
                //Wenn Timer bereits läuft
                else
                {
                    //Zeit festlegen
                    DateTime DtNow = DateTime.Now;
                    //Prüfen ob Zeit überschritten
                    if ((Timer_Settings_DTStart.AddSeconds(2)) < DtNow)
                    {
                        Timer_Settings_Action = "None";
                        GRNote.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }



            // Wenn Benachrichtigung ausgegeben wird
            else if (Timer_Settings_Action == "Note")
            {
                //Wenn Timer noch nicht läuft
                if (Timer_Settings_DTStart == DateTime.MinValue)
                {
                    //Timer setzen
                    Timer_Settings_DTStart = DateTime.Now;
                    //Grid anzeigen
                    GRNote.Visibility = System.Windows.Visibility.Visible;

                }
                //Wenn Timer bereits läuft
                else
                {
                    //Zeit festlegen
                    DateTime DtNow = DateTime.Now;
                    //Prüfen ob Zeit überschritten
                    if ((Timer_Settings_DTStart.AddSeconds(2)) < DtNow)
                    {
                        Timer_Settings_Action = "None";
                        GRNote.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }



            //Listen schließen
            else if (Timer_Settings_Action == "RefreshList")
            {
                if (GRUpdateList.Visibility == System.Windows.Visibility.Collapsed)
                {
                    GRUpdateList.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    //Listen erneuern
                    CloseLists();
                    // Benachrichtigung ausgeben um Catch zu löschen
                    TBNote.Text = MyMusicPlayer.Resources.AppResources.ZZ003_ClickClearCatch;
                    // Zeit zurücksetzen
                    Timer_Settings_DTStart = DateTime.MinValue;
                    // Timer Aktion ändern
                    Timer_Settings_Action = "RefreshList2";
                }
            }



            // Wenn Refresh bereits gedrückt wurde
            else if (Timer_Settings_Action == "RefreshList2")
            {
                //Wenn Timer noch nicht läuft
                if (Timer_Settings_DTStart == DateTime.MinValue)
                {
                    //Timer setzen
                    Timer_Settings_DTStart = DateTime.Now;
                    //Grid anzeigen
                    GRNote.Visibility = System.Windows.Visibility.Visible;

                }
                //Wenn Timer bereits läuft
                else
                {
                    //Zeit festlegen
                    DateTime DtNow = DateTime.Now;
                    //Prüfen ob Zeit überschritten
                    if ((Timer_Settings_DTStart.AddSeconds(2)) < DtNow)
                    {
                        Timer_Settings_Action = "None";
                        GRNote.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }



            // Wenn Refresh bereits gedrückt wurde
            else if (Timer_Settings_Action == "RefreshList3")
            {
                //Wenn Timer noch nicht läuft
                if (Timer_Settings_DTStart == DateTime.MinValue)
                {
                    //Timer setzen
                    Timer_Settings_DTStart = DateTime.Now;
                    //Grid anzeigen
                    GRNote.Visibility = System.Windows.Visibility.Visible;

                }
                //Wenn Timer bereits läuft
                else
                {
                    //Zeit festlegen
                    DateTime DtNow = DateTime.Now;
                    //Prüfen ob Zeit überschritten
                    if ((Timer_Settings_DTStart.AddSeconds(2)) < DtNow)
                    {
                        Timer_Settings_Action = "None";
                        GRNote.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }



            //Catch löschen und neu erstellen
            else if (Timer_Settings_Action == "ClearCatch")
            {
                if (GRUpdateList.Visibility == System.Windows.Visibility.Collapsed)
                {
                    GRUpdateList.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    //Listen erneuern
                    ClearCatch();
                }
            }


            //Focus löschen
            else if (Timer_Settings_Action == "Unfocus")
            {
                Focus();
                Timer_Settings_Action = "None";
            }



            // Wenn Timer keine Aktion zugewiesen
            else
            {
                // Timer Zeit zurücksetzen
                Timer_Settings_DTStart = DateTime.MinValue;
                // Benachrichtigung verschwinden lassen
                GRNote.Visibility = System.Windows.Visibility.Collapsed;
                // Geschlossen gedrückt zurücksetzen
                ClosePressed = false;
                // Timer anhalten
                Timer_Settings.Stop();
            }
        }



        //Listen schließen
        void CloseLists()
        {
            //List Selector Variabeln löschen
            ClearListSelectorVars();
            //Listen neu erstellen
            ListPlaylist.Clear();
            List_Playlists("CreateNew", -1);
            ListSongs.Clear();
            List_Music(-1, -1);
            //Info unsichtbar machen
            GRUpdateList.Visibility = System.Windows.Visibility.Collapsed;
            //Timer zurückstellen
            Timer_Settings_Action = "None";
            //Ausgeben das nach drei mal klicken Catch neu erstellt wird
            RefreshPressed = 1;
        }



        //Wenn Timer Textfeld gedrückt wird
        bool TimerIsRunning = false;
        DateTime StopTimerTimer;
        private void TBTimer_Click(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            //Wenn Run under Lockscreen deaktiviert ist
            if (RunUnderLockScreen == false)
            {
                //Abfragen ob aktiviert wird
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ003_TimerRunNote, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    RunUnderLockScreen = true;
                    CreateSettings();
                    try
                    {
                        PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                        //BtnRunUnderLockScreen.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    Timer_Settings_Action = "Unfocus";
                    Timer_Settings.Start();
                }
            }
        }

        //Wenn Timer Play gedrückt wird
        private void TimerPlay_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Run under LockScreen aktiv ist
            if (RunUnderLockScreen == true & TB_Timer.Text.Length > 0)
            {
                //Versuchen in Zahl umzuwandeln
                int number = 0;
                try
                {
                    number = int.Parse(TB_Timer.Text.Trim());
                    StartTimer(number);
                }
                catch
                {
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ003_ErrorRunUnder);
                    TB_Timer.Text = "";
                }
            }
        }

        //Wenn Timer löschen gedrückt wird
        private void TimerDelete_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Timer läuft
            if (TimerIsRunning == true)
            {
                //Timer löschen
                TB_Timer.IsReadOnly = false;
                TB_Timer.Text = "";
                TimerIsRunning = false;
                //Bilder umwandeln
                ImgTimerDelete.Opacity = 0.5;
                ImgTimerPlay.Opacity = 1.0;
            }
        }

        //Timer starten
        void StartTimer(int number)
        {
            //Wenn Eingabe richtig
            if (number <= 360 & number > 0)
            {
                //Timer starten
                TB_Timer.IsReadOnly = true;
                TimerIsRunning = true;
                //Bilder umwandeln
                ImgTimerDelete.Opacity = 1.0;
                ImgTimerPlay.Opacity = 0.5;
                //Timer Zeit festlegen
                StopTimerTimer = DateTime.Now.AddMinutes(number);
            }
            //Wenn Eingabe falsch
            else
            {
                MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ003_ErrorRunUnder);
                TB_Timer.Text = "";
            }
        }



        //Button Run under lock screen
        private void BtnRunUnderLockScreen_Click(object sender, RoutedEventArgs e)
        {
            if (RunUnderLockScreen == false)
            {
                RunUnderLockScreen = true;
                // BtnRunUnderLockScreen.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                CreateSettings();
                try
                {
                    PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                }
                catch
                {
                }
            }
            else
            {
                RunUnderLockScreen = false;
                // BtnRunUnderLockScreen.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                CreateSettings();
                try
                {
                    PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled;
                }
                catch
                {
                }
            }
        }


        //Catch Leeren
        void ClearCatch()
        {
            //Catch Dateien löschen
            if (file.FileExists("/Catch/Albums.dat"))
            {
                file.DeleteFile("/Catch/Albums.dat");
            }
            if (file.FileExists("/Catch/AllSongs.dat"))
            {
                file.DeleteFile("/Catch/AllSongs.dat");
            }
            if (file.FileExists("/Catch/Artists.dat"))
            {
                file.DeleteFile("/Catch/Artists.dat");
            }
            if (file.FileExists("/Catch/Genres.dat"))
            {
                file.DeleteFile("/Catch/Genres.dat");
            }
            if (file.FileExists("/Catch/Playlists.dat"))
            {
                file.DeleteFile("/Catch/Playlists.dat");
            }
            //Catch Variabeln zurücksetzen
            CatchAlbums = "";
            CatchAllSongs = "";
            CatchArtists = "";
            CatchGenres = "";
            CatchPlaylists = "";

            //List Selector Variabeln löschen
            ClearListSelectorVars();
            //Listen neu erstellen
            ListPlaylist.Clear();
            List_Playlists("CreateNew", -1);
            ListSongs.Clear();
            List_Music(-1, -1);
            //Info unsichtbar machen
            GRUpdateList.Visibility = System.Windows.Visibility.Collapsed;
            //Ausgabe verbergen
            GRNote.Visibility = System.Windows.Visibility.Collapsed;
            //Timer zurückstellen
            Timer_Settings_Action = "None";
        }



        //Listen schließen
        private void CloseAllLists_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Suche löschen
            Search = "";
            TBSearch.Text = "";
            CloseSearch();
            //Timer anweisen das Liste upgedatet wird
            Timer_Settings_Action = "RefreshList";
            Timer_Settings.Start();
        }



        //Lied wechseln durch verschieben //Mini Mediaplayer
        bool TransformEnd = false;
        int PivotIndexTemp = -1;
        int WhatToDo = 0;
        //Bewegung starten
        private void OnDragStarted(object sender, DragStartedGestureEventArgs e)
        {
            //Pivot Index merken
            PivotIndexTemp = MainPivotItem.SelectedIndex;
        }
        //Während der Bewegung
        private void OnDragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            //Bewegung erstellen
            transform.TranslateX += e.HorizontalChange;
            transform.TranslateY += e.VerticalChange;

            if (TransformEnd == false)
            {
                if (transform.TranslateX > 50)
                {
                    TransformEnd = true;
                    transform.TranslateX = 0;
                    WhatToDo = 1;
                }
                if (transform.TranslateX < -50)
                {
                    TransformEnd = true;
                    transform.TranslateX = 0;
                    WhatToDo = -1;
                }
            }
            else
            {
                transform.TranslateX = 0;
            }


            if (transform.TranslateY != 0)
            {
                transform.TranslateY = 0;
            }
        }
        //Wenn Bewegung beendet
        private void OnDragCompleted(object sender, DragCompletedGestureEventArgs e)
        {
            //Bewegung zurücksetzen
            transform.TranslateX = 0;
            transform.TranslateY = 0;
            //Variable zurücksetzen
            TransformEnd = false;
            //Pivot zurücksetzen
            MainPivotItem.SelectedIndex = PivotIndexTemp;
            //Angeben was zu tun ist
            if (WhatToDo == 1)
            {
                if (MediaPlayer.Queue.Count > 0)
                {
                    // Song zurück
                    SongBack();
                }
            }
            else if (WhatToDo == -1)
            {
                if (MediaPlayer.Queue.Count > 0)
                {
                    // Nächsten Song
                    SongForward();
                }
            }
            WhatToDo = 0;
        }



        // Aktionen Aktuelle Abspielliste
        int LBCurrentPlaylist_SelectedIndex = -1;
        string LBCurrentPlaylist_Selection = "None";

        // Eintrag aus aktueller Abspielliste auswählen
        private void LBCurrentPlaylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Wenn ausgeführt wird
            if (LBCurrentPlaylist.SelectedIndex != -1)
            {
                // Index feslegen
                LBCurrentPlaylist_SelectedIndex = LBCurrentPlaylist.SelectedIndex;

                // Wenn Eintrag nach unten verschoben wird
                if (LBCurrentPlaylist_Selection == "Down")
                {
                    // Einträge laden
                    string tempArtist1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Artist;
                    string tempAlbum1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Album;
                    string tempSong1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Song;
                    string tempBackground1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Background;
                    string tempForeground1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Foreground;
                    string tempArtist2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Artist;
                    string tempAlbum2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Album;
                    string tempSong2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Song;
                    string tempBackground2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Background;
                    string tempForeground2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Foreground;
                    // Einträge neu erstellen
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Artist = tempArtist2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Album = tempAlbum2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Song = tempSong2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Background = tempBackground2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Foreground = tempForeground2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Artist = tempArtist1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Album = tempAlbum1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Song = tempSong1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Background = tempBackground1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex + 1].Foreground = tempForeground1;
                    // Einträge neu erstellen
                    LBCurrentPlaylist.ItemsSource = null;
                    LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                }
                // Wenn Eintrag nach oben verschoben wird
                else if (LBCurrentPlaylist_Selection == "Up")
                {
                    // Einträge laden
                    string tempArtist1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Artist;
                    string tempAlbum1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Album;
                    string tempSong1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Song;
                    string tempBackground1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Background;
                    string tempForeground1 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Foreground;
                    string tempArtist2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Artist;
                    string tempAlbum2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Album;
                    string tempSong2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Song;
                    string tempBackground2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex -1 ].Background;
                    string tempForeground2 = ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Foreground;
                    // Einträge neu erstellen
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Artist = tempArtist2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Album = tempAlbum2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Song = tempSong2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Background = tempBackground2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex].Foreground = tempForeground2;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Artist = tempArtist1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Album = tempAlbum1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Song = tempSong1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Background = tempBackground1;
                    ListActivPlaylist[LBCurrentPlaylist_SelectedIndex - 1].Foreground = tempForeground1;
                    // Einträge neu erstellen
                    LBCurrentPlaylist.ItemsSource = null;
                    LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                }
                // Wenn Eintrag delöscht wird
                else if (LBCurrentPlaylist_Selection == "Delete")
                {
                    // Eintrag löschen
                    ListActivPlaylist.RemoveAt(LBCurrentPlaylist_SelectedIndex);
                    // Einträge neu erstellen
                    LBCurrentPlaylist.ItemsSource = null;
                    LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                    // Letzte Wiedergabe speichern
                    SaveLastPlaylist();
                    // Shuffle neu erstellen
                    CreateShuffleInt();
                }
                // Wenn Eintrag ausgewählt wird
                else
                {
                    // Aktuelle Play ID erstellen
                    PlayID = LBCurrentPlaylist_SelectedIndex;
                    // Angeben das Play gedrückt wurde
                    PlayControl = "Play";
                    // Song abspielen
                    SelectAndPlay_Play(PlayID);
                }

                // Auswahl zurücksetzen
                LBCurrentPlaylist_Selection = "None";

                // Index zurücksetzen
                try
                {
                    LBCurrentPlaylist.SelectedIndex = -1;
                }
                catch
                {
                }
            }
        }

        // Select and Play Playliste Eintrag nach unten verschieben
        private void LBCurrentPlaylist_ButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBCurrentPlaylist_Selection = "Down";
        }

        // Select and Play Playliste Eintrag nach oben verschieben
        private void LBCurrentPlaylist_ButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBCurrentPlaylist_Selection = "Up";
        }

        // Select and Play Playliste Eintrag löschen
        private void LBCurrentPlaylist_ButtonDelete(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LBCurrentPlaylist_Selection = "Delete";
        }

        // Methode um Eintrag abzuspielen
        void SelectAndPlay_Play(int ID)
        {
            // Eintrag suchen
            var AllArtist = mediaLibrary.Artists;
            int ArtistID = -1;
            // Artisten durchlaufen und auswahl suchen
            for (int i = 0; i < AllArtist.Count(); i++)
            {
                // Artist ID heraussuchen
                if (AllArtist[i].Name == ListActivPlaylist[ID].Artist)
                {
                    ArtistID = i;
                    break;
                }
            }
            // Wenn Artist ID -1 ist, Fehlermeldung ausgeben
            if (ArtistID == -1)
            {
                // Fehlermeldung ausgeben
            }
            else
            {
                //Artist Songs laden
                var ArtistSongs = AllArtist[ArtistID].Songs;
                int SongID = -1;
                // Songs durchlaufen und Song suchen
                for (int i = 0; i < ArtistSongs.Count(); i++)
                {
                    // Song ID heraussuchen
                    if (ArtistSongs[i].Name == ListActivPlaylist[ID].Song)
                    {
                        SongID = i;
                        break;
                    }
                }
                // Wenn Song ID -1 ist, Fehlermeldung ausgeben
                if (SongID == -1)
                {
                    // Fehlermeldung ausgeben
                }
                else
                {
                    try
                    {
                        // Song laden
                        var SongToPlay = ArtistSongs[SongID];
                        Microsoft.Xna.Framework.FrameworkDispatcher.Update();
                        MediaPlayer.Play(SongToPlay);
                        // Song Daten erstellen
                        PlayArtist = ListActivPlaylist[ID].Artist;
                        PlayAlbum = ListActivPlaylist[ID].Album;
                        PlaySong = ListActivPlaylist[ID].Song;
                        // Aktive Playliste durchlaufen und ausgewählten Song ändern
                        for (int i = 0; i < ListActivPlaylist.Count(); i++)
                        {
                            // Auswahl erstellen
                            if (i == ID)
                            {
                                ListActivPlaylist[i].Foreground = SelectedForegroundColor;
                                ListActivPlaylist[i].Background = SelectedBackgroundColor;
                            }
                            // Auswahl aufheben
                            else
                            {
                                ListActivPlaylist[i].Foreground = SongForegroundColor;
                                ListActivPlaylist[i].Background = SongBackgroundColor;
                            }
                        }
                        // Auswahl in Abspielliste ändern
                        LBCurrentPlaylist.ItemsSource = null;
                        LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                    }
                    catch
                    {
                    }
                }
            }
        }



        // Wird ausgeführt wenn sich Playerstatus ändert
        private void MPActivSongChanged(object sender, EventArgs e)
        {
            // NotImplentedException verwerfen
            try
            {
                throw new NotImplementedException();
            }
            catch
            {
            }
        }



        // Aktuelle Wiedergabe leeren
        private void DeletePlaylist_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Abfrabe, ob aktuelle Wiedergabeliste geleert werden soll
            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ005_ClearCurrent, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                // Aktuelle Wiedergabeliste leeren
                ListActivPlaylist.Clear();
                // Leere Wiedergabe verlinken
                LBCurrentPlaylist.ItemsSource = null;
                LBCurrentPlaylist.ItemsSource = ListActivPlaylist;
                //Suche löschen
                Search = "";
                TBSearch.Text = "";
                CloseSearch();
                // Listen erneuern
                CloseLists();
            }
        }



        // Atkuelle Wiedergabe speichern
        private void SavePlaylist_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Suche und speichern geschlossen
            if (SearchStatus == "Closed")
            {
                // Prüfen ob Lieder in Aktueller Wiedergabe vorhanden
                if (ListActivPlaylist.Count() > 0)
                {
                    // Eingabefeld öffnen
                    OpenSearch();
                    // Angeben das Suche
                    SearchAction = "Save";
                }
                // Wenn keine Lieder in aktueller Wiedergabe vorhanden
                else
                {
                    // Benachrichtigung ausgeben
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ005_NoSongsAvailable);
                }
            }
            //Wenn Suche und speichern offen
            else if (SearchStatus == "Opened")
            {
                // Eingabefeld schließen
                CloseSearch();
                // Angabe löschen
                SearchAction = "None";
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        # region Suche
        //---------------------------------------------------------------------------------------------------------------------------------
        //Button Suche
        private void ImgSearch_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Suche und speichern geschlossen
            if (SearchStatus == "Closed")
            {
                // Suchfeld öffnen
                OpenSearch();
                // Angeben das Suche
                SearchAction = "Search";
            }
            //Wenn Suche und speichern offen
            else if (SearchStatus == "Opened")
            {
                // Suchefeld schließen
                CloseSearch();
                // Angabe löschen
                SearchAction = "None";
            }
        }


        //Buttons bei der Suche und dem speichern der Wiedergabeliste auffangen
        private void TBSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Wenn Return gedrückt wurde
            string tempkey = Convert.ToString(e.Key);
            if (tempkey == "Enter")
            {
                // Ungewollte Buchstaben entfernen
                TBSearch.Text = TBSearch.Text.Replace(".", "");
                TBSearch.Text = TBSearch.Text.Replace("+", "");
                TBSearch.Text = TBSearch.Text.Replace("=", "");
                //Wenn Name vorhanden, Suche erstellen oder Wiedergabeliste speichern
                if (TBSearch.Text.Length > 0)
                {
                    // Wenn Suche
                    if (SearchAction == "Search")
                    {
                        //Focus löschen
                        this.Focus();
                        //Suche erstellen
                        Search = TBSearch.Text.ToLower();
                    }
                    
                    // Beim speichern der Wiedergabeliste
                    else if (SearchAction == "Save")
                    {
                        // Prüfen ob Playlists Ordner vorhanden
                        if (!file.DirectoryExists("/Playlists"))
                        {
                            // Ordner erstellen
                            file.CreateDirectory("/Playlists");
                        }
                        // Prüfvariable
                        bool SaveList = true;
                        // Prüfen ob bereits eine Liste mit dem selben Namen vorhanden                        
                        if (file.FileExists("/Playlists/" + TBSearch.Text + ".dat"))
                        {
                            // Abfrage ob bereits vorhandene Liste überschrieben werden soll
                            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ005_PlaylistAvailable, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            {
                            }
                            else
                            {
                                SaveList = false;
                            }
                        }
                        // Wenn Wiedergabeliste gespeichert wird
                        if (SaveList == true)
                        {
                            // Playlist string erstellen
                            string string_Playlist = "";
                            // Aktuelle Playliste durchlaufen und in String eintragen
                            for (int i = 0; i < ListActivPlaylist.Count(); i++)
                            {
                                string_Playlist += ListActivPlaylist[i].Artist + ";XYZZYX;";
                                string_Playlist += ListActivPlaylist[i].Album + ";XYZZYX;";
                                string_Playlist += ListActivPlaylist[i].Song + ";XYZZYX;";
                                string_Playlist += ListActivPlaylist[i].Genre + ";XYZZYX;";
                                string_Playlist += ListActivPlaylist[i].Duration + ";ZYXXYZ;";
                            }
                            try
                            {
                                // Aktuelle Playliste als Playliste speichern
                                filestream = file.CreateFile("/Playlists/" + TBSearch.Text + ".dat");
                                sw = new StreamWriter(filestream);
                                sw.Write(string_Playlist);
                                sw.Flush();
                                filestream.Close();
                                // Suchleiste schließen
                                CloseSearch();
                            }
                            catch
                            {
                                // Bei Fehler Nachricht ausgeben das Liste nicht gespeichert werden kann
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ErrorName);
                            }
                        }
                    }
                }
                //Wenn kein Name vorhanden, Suche löschen
                else
                {
                    //Focus löschen
                    this.Focus();
                    //Suche löschen
                    Search = "";
                }
                //Timer anweisen das Liste upgedatet wird
                CloseSearch();
                // Listen schließen
                CloseLists();
                //Timer_Settings_Action = "RefreshList";
                //Timer_Settings.Start();
            }
        }


        //Suche öffnen
        void OpenSearch()
        {
            //Suche schließen
            SearchStatus = "Opened";
            TBSearch.Visibility = System.Windows.Visibility.Visible;
            RTSearch.Visibility = System.Windows.Visibility.Visible;
            SPTopButtons.Visibility = System.Windows.Visibility.Collapsed;
            TBSearch.Focus();
        }


        //Suche schließen
        void CloseSearch()
        {
            //Suche schließen
            SearchStatus = "Closed";
            SearchAction = "None";
            TBSearch.Text = "";
            TBSearch.Visibility = System.Windows.Visibility.Collapsed;
            RTSearch.Visibility = System.Windows.Visibility.Collapsed;
            SPTopButtons.Visibility = System.Windows.Visibility.Visible;        
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        # region Buttons About und Bewertungsaufruf
        //About Buttons
        //---------------------------------------------------------------------------------------------------------
        //Button Buy
        private void BtnBuy(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();
            _marketPlaceDetailTask.Show();
        }



        //Button Rate
        private void BtnRate(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarketplaceReviewTask review = new MarketplaceReviewTask();
            review.Show();
        }



        //Button Other Apps
        private void BtnOther(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();
            marketplaceSearchTask.SearchTerms = "xtrose";
            marketplaceSearchTask.Show();
        }



        //Button Facebook
        private void BtnFacebook(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var wb = new WebBrowserTask();
            wb.URL = "http://www.facebook.com/xtrose.xtrose";
            wb.Show();
        }



        //Button Support
        private void BtnSupport(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Support.xaml", UriKind.Relative));
        }



        //Button Sprache
        private void BtnLanguage(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Rename Profile öffnen
            NavigationService.Navigate(new Uri("/Pages/Language.xaml", UriKind.Relative));
        }



        //Button Instructions
        private void BtnInstructions(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Instructions.xaml", UriKind.Relative));
        }



        //Bewertungsaufruf, jetzt bewerten
        private void BtnRateRate_click(object sender, RoutedEventArgs e)
        {
            //Zu bewertungen gehen
            MarketplaceReviewTask review = new MarketplaceReviewTask();
            review.Show();
            //Rate File löschen
            file.DeleteFile("Settings/RateReminder.txt");
            //Rate verbergen
            GRRate.Visibility = System.Windows.Visibility.Collapsed;
            MenuOpen = false;
        }
        //Bewertungsaufruf, Später bewerten
        private void BtnRateLater_click(object sender, RoutedEventArgs e)
        {
            //Rate File neu erstellen
            DateTime datetime = DateTime.Now;
            datetime = datetime.AddDays(4);
            filestream = file.CreateFile("Settings/RateReminder.txt");
            sw = new StreamWriter(filestream);
            sw.WriteLine(datetime.ToString());
            sw.Flush();
            filestream.Close();
            //Rate verbergen
            GRRate.Visibility = System.Windows.Visibility.Collapsed;
            MenuOpen = false;
        }
        //Bewertungsaufruf, Nie bewerten
        private void BtnRateNever_click(object sender, RoutedEventArgs e)
        {
            //Rate File löschen
            file.DeleteFile("Settings/RateReminder.txt");
            //Rate verbergen
            GRRate.Visibility = System.Windows.Visibility.Collapsed;
            MenuOpen = false;
        }
        //---------------------------------------------------------------------------------------------------------
        # endregion





        #region Back Button
        //Back Button
        //---------------------------------------------------------------------------------------------------------------------------------
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //Wenn Suche offen
            if (SearchStatus == "Opened")
            {
                //Suche schließen
                CloseSearch();
                //Zurück oder beenden abbrechen
                e.Cancel = true;
            }
            //Wenn FullscreenPlayer offen
            else if (FPAnimationStatus == "Opened")
            {
                //Fullscreenplayer schließen
                FPAnimationStatus = "Close";
                //Zurück oder beenden abbrechen
                e.Cancel = true;
            }
            //Wenn FullscreenPlayer geschlossen
            else
            {
                //Prüfen ob Menü offen ist und alle Menüs schließen
                if (MenuOpen == true)
                {
                    //Menüs schließen
                    GRListSelector.Visibility = System.Windows.Visibility.Collapsed;
                    if (MainPivotItem.SelectedIndex == 0)
                    {
                        LBSongs.Opacity = 1.0;

                        LBSongs.Visibility = System.Windows.Visibility.Visible;
                    }
                    GRRate.Visibility = System.Windows.Visibility.Collapsed;
                    //Angeben das Menüs geschlossen sind
                    MenuOpen = false;

                    //Zurück oder beenden abbrechen
                    e.Cancel = true;
                }

                //Prüfen ob schon mal zurück gedrückt wurde
                else
                {
                    // Wenn noch nicht zurück gedrückt wurde
                    if (ClosePressed == false)
                    {
                        // Angeben das zurück gedrückt wurde
                        ClosePressed = true;
                        // Benachrichtigung erstellen
                        TBNote.Text = MyMusicPlayer.Resources.AppResources.ZZ002_AgainClose;
                        // Angeben das Benachrichtigung
                        Timer_Settings_Action = "Back";
                        // Zeit zurücksetzen
                        Timer_Settings_DTStart = DateTime.MinValue;
                        // Timer Starten
                        Timer_Settings.Start();
                        //Zurück oder beenden abbrechen
                        e.Cancel = true;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        #region terminate
        //---------------------------------------------------------------------------------------------------------------------------------
        private static void Terminate()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





        # region Live Tiles erstellen
        //Neue LiveTiles erstellen
        //---------------------------------------------------------------------------------------------------------------------------------
        //Album Tile erstellen
        void CreateNewTiles(Stream AlbumStream)
        {
            //Album Art laden
            WriteableBitmap Art = new WriteableBitmap(0, 0);

            //Mittleres Tile erstellen
            Art.SetSource(AlbumStream);
            Art = Art.Resize(336, 336, WriteableBitmapExtensions.Interpolation.Bilinear);

            //Tile Bild laden
            WriteableBitmap Icon = new WriteableBitmap(0, 0);
            using (Stream input = Application.GetResourceStream(new Uri("Images/TileMediumCreate.png", UriKind.Relative)).Stream)
            {
                Icon.SetSource(input);
            }
            //Mittleres Tile erstellen
            WriteableBitmap MediumTile = new WriteableBitmap(336, 336);
            MediumTile.Blit(new Rect(0, 0, 336, 336), Art, new Rect(0, 0, 336, 336));

            //Wenn Logo eingeblendet wird
            if (LogoImage == true)
            {
                MediumTile.Blit(new Rect(0, 0, 336, 336), Icon, new Rect(0, 0, 336, 336));
            }
            //Datei in Isolated Storage schreiben
            var isolatedStorageFileStream = file.CreateFile("Shared/ShellContent/336.jpg");
            MediumTile.SaveJpeg(isolatedStorageFileStream, 336, 336, 0, 100);
            isolatedStorageFileStream.Close();
            string MediumTileUri = "isostore:/Shared/ShellContent/336.jpg";

            //Großes Tile erstellen
            Art.SetSource(AlbumStream);
            Art = Art.Resize(336, 336, WriteableBitmapExtensions.Interpolation.Bilinear);
            //Tile Bild laden
            Icon = new WriteableBitmap(0, 0);
            using (Stream input = Application.GetResourceStream(new Uri("Images/TileLargeCreate.png", UriKind.Relative)).Stream)
            {
                Icon.SetSource(input);
            }
            //Mittleres Tile erstellen
            MediumTile = new WriteableBitmap(691, 336);
            MediumTile.Blit(new Rect(0, 0, 336, 336), Art, new Rect(0, 0, 336, 336));
            MediumTile.Blit(new Rect(336, 0, 224, 224), Art, new Rect(0, 0, 336, 336));
            MediumTile.Blit(new Rect(336, 224, 112, 112), Art, new Rect(0, 0, 336, 336));
            MediumTile.Blit(new Rect(448, 224, 112, 112), Art, new Rect(0, 0, 336, 336));
            MediumTile.Blit(new Rect(560, 0, 131, 131), Art, new Rect(0, 0, 336, 336));
            MediumTile.Blit(new Rect(560, 131, 131, 131), Art, new Rect(0, 0, 336, 336));
            MediumTile.Blit(new Rect(560, 262, 131, 131), Art, new Rect(0, 0, 336, 336));
            //Wenn Logo eingeblendet wird
            if (LogoImage == true)
            {
                MediumTile.Blit(new Rect(0, 0, 691, 336), Icon, new Rect(0, 0, 691, 336));
            }
            //Datei in Isolated Storage schreiben
            isolatedStorageFileStream = file.CreateFile("Shared/ShellContent/691.jpg");
            MediumTile.SaveJpeg(isolatedStorageFileStream, 691, 336, 0, 100);
            isolatedStorageFileStream.Close();
            string LargeTileUri = "isostore:/Shared/ShellContent/691.jpg";

            //Tile erstellen
            ShellTile Tile = ShellTile.ActiveTiles.First();
            FlipTileData TileData = new FlipTileData()
            {
                Title = "",
                BackgroundImage = new Uri(MediumTileUri, UriKind.Absolute),
                WideBackgroundImage = new Uri(LargeTileUri, UriKind.Absolute),
            };
            Tile.Update(TileData);
        }





        //Standard Tile wiederherstellen
        void CreateStandardTiles()
        {

            WriteableBitmap FirstTile = new WriteableBitmap(336, 336);
            WriteableBitmap FirstTileIcon = new WriteableBitmap(336, 336);
            // Wenn nicht transparent
            if (TileBackgroundColor != "*" & TileBackgroundColor != null)
            {
                //Hintergrundfarbe einfügen
                FirstTile.Clear(ConvertToSolidColorBrush(TileBackgroundColor, -1).Color);
            }
            //Tile Bild laden
            using (Stream input = Application.GetResourceStream(new Uri("TileMedium.png", UriKind.Relative)).Stream)
            {
                FirstTileIcon.SetSource(input);
            }
            //Bild zusammensetzen
            FirstTile.Blit(new Rect(0, 0, 336, 336), FirstTileIcon, new Rect(0, 0, 336, 336));


            // Bild erstellen
            Grid grid = new Grid
            {
                Width = 336,
                Height = 336
            };
            Image img = new Image();
            img.Source = FirstTile;
            grid.Children.Add(img);
            // Writeable Bitmap aus Grid erstellen
            WriteableBitmap wbmp = new WriteableBitmap(grid, null);
            // Extended Image aus Writeable Bitmap erstellen
            ExtendedImage extendImage = wbmp.ToImage();

            // Bild speichern
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists("Shared/ShellContent/First336.png"))
                {
                    store.DeleteFile("Shared/ShellContent/First336.png");
                }
                using (var stream = store.OpenFile("Shared/ShellContent/First336.png", System.IO.FileMode.OpenOrCreate))
                {
                    extendImage.WriteToStream(stream, "Shared/ShellContent/First336.png");
                }
            }




            //Erstes Tile groß erstellen
            WriteableBitmap FirstTileWidth = new WriteableBitmap(691, 336);
            WriteableBitmap FirstTileIconWidth = new WriteableBitmap(691, 336);
            // Wenn nicht transparent
            if (TileBackgroundColor != "*" & TileBackgroundColor != null)
            {
                //Hintergrundfarbe einfügen
                FirstTileWidth.Clear(ConvertToSolidColorBrush(TileBackgroundColor, -1).Color);
            }
            //Tile Bild laden
            using (Stream input = Application.GetResourceStream(new Uri("TileLarge.png", UriKind.Relative)).Stream)
            {
                FirstTileIconWidth.SetSource(input);
            }
            //Bild zusammensetzen
            FirstTileWidth.Blit(new Rect(0, 0, 691, 336), FirstTileIconWidth, new Rect(0, 0, 691, 336));


            // Bild erstellen
            Grid gridWidth = new Grid
            {
                Width = 691,
                Height = 336
            };
            Image imgWidth = new Image();
            imgWidth.Source = FirstTileWidth;
            gridWidth.Children.Add(imgWidth);
            // Writeable Bitmap aus Grid erstellen
            WriteableBitmap wbmpWidth = new WriteableBitmap(gridWidth, null);
            // Extended Image aus Writeable Bitmap erstellen
            ExtendedImage extendImageWidth = wbmpWidth.ToImage();

            // Bild speichern
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists("Shared/ShellContent/First691.png"))
                {
                    store.DeleteFile("Shared/ShellContent/First691.png");
                }
                using (var stream = store.OpenFile("Shared/ShellContent/First691.png", System.IO.FileMode.OpenOrCreate))
                {
                    extendImageWidth.WriteToStream(stream, "Shared/ShellContent/First691.png");
                }
            }

            //Erstes Tile neu erstellen
            ShellTile Tile = ShellTile.ActiveTiles.First();
            FlipTileData First = new FlipTileData();
            First.Title = MyMusicPlayer.Resources.AppResources.Z001_AppTitle;
            First.SmallBackgroundImage = new Uri("isostore:/Shared/ShellContent/First336.png", UriKind.Absolute);
            First.BackgroundImage = new Uri("isostore:/Shared/ShellContent/First336.png", UriKind.Absolute);
            First.WideBackgroundImage = new Uri("isostore:/Shared/ShellContent/First691.png", UriKind.Absolute);
            Tile.Update(First);
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        # region Orientation ändern
        //Orientation ändern
        private void ChangeOrientation(object sender, OrientationChangedEventArgs e)
        {
            //Drehung ändern
            string NewOrientation = Orientation.ToString();

            //Wenn Orientierung Landscape
            if (NewOrientation == "LandscapeLeft" | NewOrientation == "LandscapeRight")
            {
                // Orientierung umstellen
                OrientationPortait = false;

                // Bewertungs Grid Orientierung umstellen
                GRRate.Height = 480;
                GRRate.Width = 854;

                //Mediplayer klein Status Abschließen
                if (MPAnimationStatus == "Closed" | MPAnimationStatus == "Close")
                {
                    MPAnimationStatus = "Closed";
                    MPStatBarSmall.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    MPAnimationStatus = "Opened";
                    MPStatBarSmall.Visibility = System.Windows.Visibility.Collapsed;
                }
                MPAnimationStart = DateTime.MinValue;

                //MediaPlayer klein Orienetierung ändern
                GRMediaPlayer.Height = 480;
                GRMediaPlayer.Width = 300;
                GRMediaPlayer.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                GRMediaPlayer.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                //Wenn Mediaplayer klein geöffnet
                if (MPAnimationStatus == "Opened")
                {
                    GRMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                    LBSongs.Margin = new Thickness(0, -18, 300, 0);
                    LBPlaylists.Margin = new Thickness(0, -18, 300, 0);
                    LBCurrentPlaylist.Margin = new Thickness(0, -18, 300, 0);
                    SVSetting.Margin = new Thickness(0, -18, 300, 0);
                    SVAbout.Margin = new Thickness(0, -18, 300, 0);
                    SPTopButtons.Margin = new Thickness(6, 12, 324, 0);
                    TBSearch.Margin = new Thickness(10, -6, 300, 0);
                    RTSearch.Margin = new Thickness(25, 9, 315, 0);
                }
                else
                {
                    GRMediaPlayer.Margin = new Thickness(0, 0, -228, 0);
                    LBSongs.Margin = new Thickness(0, -18, 72, 0);
                    LBPlaylists.Margin = new Thickness(0, -18, 72, 0);
                    LBCurrentPlaylist.Margin = new Thickness(0, -18, 72, 0);
                    SVSetting.Margin = new Thickness(0, -18, 72, 0);
                    SVAbout.Margin = new Thickness(0, -18, 72, 0);
                    SPTopButtons.Margin = new Thickness(6, 12, 96, 0);
                    TBSearch.Margin = new Thickness(10, -6, 72, 0);
                    RTSearch.Margin = new Thickness(25, 9, 87, 0);
                }

                MPBackBg.Margin = new Thickness(13, 301, 0, 131);

                MPBack.Margin = new Thickness(12, 301, 0, 131);

                MPPlayPauseBg.Margin = new Thickness(13, 217, 0, 215);

                MPPlayOnBg.Margin = new Thickness(13, 215, 0, 215);

                MPPlayPause.Margin = new Thickness(12, 216, 0, 216);

                MPForwardBg.Margin = new Thickness(13, 131, 0, 301);

                MPForward.Margin = new Thickness(12, 131, 0, 301);

                MPImageFrame.Height = 200;
                MPImageFrame.Width = 200;
                MPImageFrame.Margin = new Thickness(78, 60, 0, 0);

                MPImageAlbumImage.Height = 198;
                MPImageAlbumImage.Width = 198;
                MPImageAlbumImage.Margin = new Thickness(79, 61, 0, 0);

                MPSPData.Width = 200;
                MPSPData.Margin = new Thickness(78, 276, 0, 0);

                MPRepead.Height = 48;
                MPRepead.Margin = new Thickness(170, 414, 0, 0);

                MPShuffle.Margin = new Thickness(237, 414, 0, 0);

                MPStatFrame.Width = 200;
                MPStatFrame.Margin = new Thickness(78, 356, 0, 0);

                MPStatEmpty.Width = 198;
                MPStatEmpty.Margin = new Thickness(79, 357, 0, 0);

                MPStatBar.Width = 200;
                MPStatBar.Margin = new Thickness(78, 356, 0, 0);

                MPPlayTime.Margin = new Thickness(78, 373, 0, 0);

                MPTime.Margin = new Thickness(0,373,22,0);

                MPStatBarSmall.Height = 480;
                MPStatBarSmall.Width = 2;
                MPStatBarSmall.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                MPStatBarSmall.Margin = new Thickness(70, 0, 0, 0);

                //Wenn Mediaplayer klein geöffnet
                if (MPAnimationStatus == "Opened")
                {
                    //Wenn Vordergrundfarbe Schwarz ist
                    if (ForegroundColor == "#FF000000")
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Right.Light.png", UriKind.Relative));
                    }
                    //Wenn Vordergrundfarbe weiß ist
                    else
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Right.Dark.png", UriKind.Relative));
                    }
                }
                else
                {
                    //Wenn Vordergrundfarbe Schwarz ist
                    if (ForegroundColor == "#FF000000")
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Left.Light.png", UriKind.Relative));
                    }
                    //Wenn Vordergrundfarbe weiß ist
                    else
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Left.Dark.png", UriKind.Relative));
                    }
                }
                MPOpenClose.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                MPOpenClose.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                MPOpenClose.Margin = new Thickness(25,12,0,0);

                MPOpenCloseBg.Width = 65;
                MPOpenCloseBg.Height = 48;
                MPOpenCloseBg.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                MPOpenCloseBg.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                MPOpenCloseBg.Margin = new Thickness(0, 0, 0, 0);

                //Wenn Vordergrundfarbe Schwarz ist
                if (ForegroundColor == "#FF000000")
                {
                    FPOpen.Source = new BitmapImage(new Uri("Images/Arrow.Up.Light.png", UriKind.Relative));
                }
                //Wenn Vordergrundfarbe Weiß ist
                else
                {
                    FPOpen.Source = new BitmapImage(new Uri("Images/Arrow.Up.Dark.png", UriKind.Relative));
                }
                FPOpen.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                FPOpen.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPOpen.Margin = new Thickness(25, 0, 0, 12);

                FPOpenBg.Width = 65;
                FPOpenBg.Height = 48;
                FPOpenBg.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                FPOpenBg.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPOpenBg.Margin = new Thickness(0, 0, 0, 0);


                //Mediplayer Groß Status Abschließen
                if (FPAnimationStatus == "Closed" | FPAnimationStatus == "Close")
                {
                    FPAnimationStatus = "Closed";
                }
                else
                {
                    FPAnimationStatus = "Opened";
                }
                FPAnimationStart = DateTime.MinValue;

                //MediaPlayer groß Daten ändern
                GRFullscreenPlayer.Width = 854;
                GRFullscreenPlayer.Height = 480;
                GRFullscreenPlayer.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                GRFullscreenPlayer.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                //Wenn Mediaplayer groß geöffnet
                if (FPAnimationStatus == "Opened")
                {
                    GRFullscreenPlayer.Margin = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    GRFullscreenPlayer.Margin = new Thickness(0, 0, 0, 480);
                }

                FPGRImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPGRImage.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPGRImage.Margin = new Thickness(40, 65, 0, 0);

                FPSPButtons.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPSPButtons.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPSPButtons.Margin = new Thickness(420, 65, 0, 0);

                FPSPSongData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPSPSongData.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPSPSongData.Margin = new Thickness(420, 170, 0, 0);

                FPGRBar.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPGRBar.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPGRBar.Margin = new Thickness(420, 300, 0, 0);

                FPGRButtons2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPGRButtons2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPGRButtons2.Margin = new Thickness(420, 372, 0, 0);

                RTCloseBg.Margin = new Thickness(310, 0, 0, 0);
                FPClose.Margin = new Thickness(330, 14, 0, 0);
                FPRepead.Margin = new Thickness(0, 0, 0, 0);
                FPShuffle.Margin = new Thickness(88, 0, -8, 0);
                FPTBRepead.Margin = new Thickness(132, 5, 0, 0);

                //Wenn Vordergrundfarbe Schwarz ist
                if (ForegroundColor == "#FF000000")
                {
                    FPClose.Source = new BitmapImage(new Uri("Images/Arrow.Down.Light.png", UriKind.Relative));
                }
                //Wenn Vordergrundfarbe Weiß ist
                else
                {
                    FPClose.Source = new BitmapImage(new Uri("Images/Arrow.Down.Dark.png", UriKind.Relative));
                }


                //ListSelector umstellen
                GRListSelector.Height = 480;
                GRListSelector.Width = 854;

                GridSharp.Margin = new Thickness(14, 22, 0, 0);
                Grida.Margin = new Thickness(126, 22, 0, 0);
                Gridb.Margin = new Thickness(238, 22, 0, 0);
                Gridc.Margin = new Thickness(350, 22, 0, 0);
                Gridd.Margin = new Thickness(462, 22, 0, 0);
                Gride.Margin = new Thickness(574, 22, 0, 0);
                Gridf.Margin = new Thickness(686, 22, 0, 0);
                Gridg.Margin = new Thickness(14, 134, 0, 0);
                Gridh.Margin = new Thickness(126, 134, 0, 0);
                Gridi.Margin = new Thickness(238, 134, 0, 0);
                Gridj.Margin = new Thickness(350, 134, 0, 0);
                Gridk.Margin = new Thickness(462, 134, 0, 0);
                Gridl.Margin = new Thickness(574, 134, 0, 0);
                Gridm.Margin = new Thickness(686, 134, 0, 0);
                Gridn.Margin = new Thickness(14, 246, 0, 0);
                Grido.Margin = new Thickness(126, 246, 0, 0);
                Gridp.Margin = new Thickness(238, 246, 0, 0);
                Gridq.Margin = new Thickness(350, 246, 0, 0);
                Gridr.Margin = new Thickness(462, 246, 0, 0);
                Grids.Margin = new Thickness(574, 246, 0, 0);
                Gridt.Margin = new Thickness(686, 246, 0, 0);
                Gridu.Margin = new Thickness(14, 358, 0, 0);
                Gridv.Margin = new Thickness(126, 358, 0, 0);
                Gridw.Margin = new Thickness(238, 358, 0, 0);
                Gridx.Margin = new Thickness(350, 358, 0, 0);
                Gridy.Margin = new Thickness(462, 358, 0, 0);
                Gridz.Margin = new Thickness(574, 358, 0, 0);
                GridGlobal.Margin = new Thickness(686, 358, 0, 0);

                //Wartebildschirm umstellen
                GRUpdateList.Height = 480;
                GRUpdateList.Width = 854;

                //Farbe einstellen
                LayoutRoot.Background = ConvertToSolidColorBrush(BackgroundColor, -1);

                //Hintergrundbild umstellen
                if (BackgroundLandscape == true)
                {
                    //Wenn Ordner der Hintergrundbilder vorhanden
                    if (file.DirectoryExists("/Background"))
                    {
                        if (file.FileExists("/Background/Landscape.jpg"))
                        {
                            //Bilder laden
                            byte[] data1;
                            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                            {
                                using (IsolatedStorageFileStream isfs = isf.OpenFile("/Background/Landscape.jpg", FileMode.Open, FileAccess.Read))
                                {
                                    data1 = new byte[isfs.Length];
                                    isfs.Read(data1, 0, data1.Length);
                                    isfs.Close();
                                }
                            }
                            try
                            {
                                MemoryStream ms = new MemoryStream(data1);
                                BitmapImage bi = new BitmapImage();
                                bi.SetSource(ms);
                                var imageBrush = new ImageBrush();
                                imageBrush.ImageSource = bi;
                                LayoutRoot.Background = imageBrush;
                            }
                            catch
                            {
                                ResetDesign();
                            }
                        }
                    }
                }

                //Media Player Hintergrundfarbe umstellen
                GRFullscreenPlayer.Background = ConvertToSolidColorBrush(MediaPlayerBigBackgroundColor, -1);

                //Media Player Hintergrundbild umstellen
                if (MPBigBackgroundLandscape == true)
                {
                    //Wenn Ordner der Hintergrundbilder vorhanden
                    if (file.DirectoryExists("/Background"))
                    {
                        if (file.FileExists("/Background/MPBigLandscape.jpg"))
                        {
                            //Bilder laden
                            byte[] data1;
                            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                            {
                                using (IsolatedStorageFileStream isfs = isf.OpenFile("/Background/MPBigLandscape.jpg", FileMode.Open, FileAccess.Read))
                                {
                                    data1 = new byte[isfs.Length];
                                    isfs.Read(data1, 0, data1.Length);
                                    isfs.Close();
                                }
                            }
                            try
                            {
                                MemoryStream ms = new MemoryStream(data1);
                                BitmapImage bi = new BitmapImage();
                                bi.SetSource(ms);
                                var imageBrush = new ImageBrush();
                                imageBrush.ImageSource = bi;
                                GRFullscreenPlayer.Background = imageBrush;
                            }
                            catch
                            {
                                ResetDesign();
                            }
                        }
                    }
                }
            }



            //Wenn Orientierung Portrait
            else
            {
                //Orientierung umstellen
                OrientationPortait = true;

                // Bewertungs Grid Orientierung umstellen
                GRRate.Height = 854;
                GRRate.Width = 480;

                //Mediplayer klein Status Abschließen
                if (MPAnimationStatus == "Closed" | MPAnimationStatus == "Close")
                {
                    MPAnimationStatus = "Closed";
                    MPStatBarSmall.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    MPAnimationStatus = "Opened";
                    MPStatBarSmall.Visibility = System.Windows.Visibility.Collapsed;
                }
                MPAnimationStart = DateTime.MinValue;

                //Suche Bild Orientierung ändern
                SPTopButtons.Margin = new Thickness(6, 12, 24, 0);
                TBSearch.Margin = new Thickness(10, -6, 0, 0);
                RTSearch.Margin = new Thickness(25, 9, 15, 0);

                //MediaPlayer klein Orienetierung ändern
                GRMediaPlayer.Height = 200;
                GRMediaPlayer.Width = 480;
                GRMediaPlayer.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                GRMediaPlayer.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                //Wenn Mediaplayer klein geöffnet
                if (MPAnimationStatus == "Opened")
                {
                    GRMediaPlayer.Margin = new Thickness(0, 0, 0, 0);
                    LBSongs.Margin = new Thickness(0, -18, 0, 200);
                    LBPlaylists.Margin = new Thickness(0, -18, 0, 200);
                    LBCurrentPlaylist.Margin = new Thickness(0, -18, 0, 200);
                    SVSetting.Margin = new Thickness(0, -18, 0, 200);
                    SVAbout.Margin = new Thickness(0, -18, 0, 200);
                }
                else
                {
                    GRMediaPlayer.Margin = new Thickness(0, 0, 0, -128);
                    LBSongs.Margin = new Thickness(0, -18, 0, 72);
                    LBPlaylists.Margin = new Thickness(0, -18, 0, 72);
                    LBCurrentPlaylist.Margin = new Thickness(0, -18, 0, 72);
                    SVSetting.Margin = new Thickness(0, -18, 0, 72);
                    SVAbout.Margin = new Thickness(0, -18, 0, 72);
                }

                MPBackBg.Margin = new Thickness(132, 13, 0, 0);

                MPBack.Margin = new Thickness(131, 12, 0, 0);

                MPPlayPauseBg.Margin = new Thickness(217, 13, 0, 0);

                MPPlayOnBg.Margin = new Thickness(217, 13, 0, 0);

                MPPlayPause.Margin = new Thickness(216, 12, 0, 0);

                MPForwardBg.Margin = new Thickness(302, 13, 0, 0);

                MPForward.Margin = new Thickness(301, 12, 0, 0);

                MPImageFrame.Height = 100;
                MPImageFrame.Width = 100;
                MPImageFrame.Margin = new Thickness(10, 83, 0, 0);

                MPImageAlbumImage.Height = 98;
                MPImageAlbumImage.Width = 98;
                MPImageAlbumImage.Margin = new Thickness(11, 84, 0, 0);

                MPSPData.Width = 260;
                MPSPData.Margin = new Thickness(124, 80, 96, 52);

                MPRepead.Margin = new Thickness(419, 81, 0, 0);

                MPShuffle.Margin = new Thickness(419, 141, 0, 0);

                MPStatFrame.Width = 260;
                MPStatFrame.Margin = new Thickness(125, 149, 0, 0);

                MPStatEmpty.Width = 258;
                MPStatEmpty.Margin = new Thickness(126, 150, 0, 0);

                MPStatBar.Width = 260;
                MPStatBar.Margin = new Thickness(125, 149, 0, 0);

                MPPlayTime.Margin = new Thickness(126, 165, 0, 0);

                MPTime.Margin = new Thickness(0, 165, 95, 0);

                MPStatBarSmall.Height = 2;
                MPStatBarSmall.Width = 480;
                MPStatBarSmall.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                MPStatBarSmall.Margin = new Thickness(0, 70, 0, 0);

                //Wenn Mediaplayer klein geöffnet
                if (MPAnimationStatus == "Opened")
                {
                    //Wenn Vordergrundfarbe Schwarz ist
                    if (ForegroundColor == "#FF000000")
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Down.Light.png", UriKind.Relative));
                    }
                    //Wenn Vordergrundfarbe weiß ist
                    else
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Down.Dark.png", UriKind.Relative));
                    }
                }
                else
                {
                    //Wenn Vordergrundfarbe Schwarz ist
                    if (ForegroundColor == "#FF000000")
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Up.Light.png", UriKind.Relative));
                    }
                    //Wenn Vordergrundfarbe weiß ist
                    else
                    {
                        MPOpenClose.Source = new BitmapImage(new Uri("Images/Arrow.Up.Dark.png", UriKind.Relative));
                    }
                }
                MPOpenClose.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                MPOpenClose.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                MPOpenClose.Margin = new Thickness(0, 25, 12, 0);

                MPOpenCloseBg.Width = 48;
                MPOpenCloseBg.Height = 65;
                MPOpenCloseBg.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                MPOpenCloseBg.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                MPOpenCloseBg.Margin = new Thickness(0, 0, 0, 0);

                //Wenn Vordergrundfarbe Schwarz ist
                if (ForegroundColor == "#FF000000")
                {
                    FPOpen.Source = new BitmapImage(new Uri("Images/Arrow.Right.Light.png", UriKind.Relative));
                }
                //Wenn Vordergrundfarbe weiß ist
                else
                {
                    FPOpen.Source = new BitmapImage(new Uri("Images/Arrow.Right.Dark.png", UriKind.Relative));
                }
                FPOpen.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPOpen.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPOpen.Margin = new Thickness(12, 25, 0, 0);

                FPOpenBg.Width = 48;
                FPOpenBg.Height = 65;
                FPOpenBg.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPOpenBg.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                FPOpenBg.Margin = new Thickness(0, 0, 0, 0);


                //Mediplayer Groß Status Abschließen
                if (FPAnimationStatus == "Closed" | FPAnimationStatus == "Close")
                {
                    FPAnimationStatus = "Closed";
                }
                else
                {
                    FPAnimationStatus = "Opened";
                }
                FPAnimationStart = DateTime.MinValue;

                //MediaPlayer groß Daten ändern
                GRFullscreenPlayer.Width = 480;
                GRFullscreenPlayer.Height = 854;
                GRFullscreenPlayer.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                GRFullscreenPlayer.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                //Wenn Mediaplayer groß geöffnet
                if (FPAnimationStatus == "Opened")
                {
                    GRFullscreenPlayer.Margin = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    GRFullscreenPlayer.Margin = new Thickness(-480, 0, 0, 0);
                }

                FPGRImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                FPGRImage.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPGRImage.Margin = new Thickness(0, 40, 0, 0);

                FPSPButtons.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                FPSPButtons.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPSPButtons.Margin = new Thickness(0, 430, 0, 0);

                FPSPSongData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPSPSongData.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                FPSPSongData.Margin = new Thickness(0, 530, 0, 0);

                FPGRBar.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPGRBar.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                FPGRBar.Margin = new Thickness(0, 650, 0, 0);

                FPGRButtons2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                FPGRButtons2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                FPGRButtons2.Margin = new Thickness(0, 736, 0, 0);

                RTCloseBg.Margin = new Thickness(-15, 0, 0, 0);
                FPClose.Margin = new Thickness(0, 14, 0, 0);
                FPRepead.Margin = new Thickness(222, 0, 0, 0);
                FPShuffle.Margin = new Thickness(310, 0, -8, 0);
                FPTBRepead.Margin = new Thickness(226, 5, 0, 0);

                //Wenn Vordergrundfarbe Schwarz ist
                if (ForegroundColor == "#FF000000")
                {
                    FPClose.Source = new BitmapImage(new Uri("Images/Arrow.Left.Light.png", UriKind.Relative));
                }
                //Wenn Vordergrundfarbe weiß ist
                else
                {
                    FPClose.Source = new BitmapImage(new Uri("Images/Arrow.Left.Dark.png", UriKind.Relative));
                }


                //ListSelector umstellen
                GRListSelector.Height = 854;
                GRListSelector.Width = 480;

                GridSharp.Margin = new Thickness(22, 14, 0, 0);
                Grida.Margin = new Thickness(134, 14, 0, 0);
                Gridb.Margin = new Thickness(246, 14, 0, 0);
                Gridc.Margin = new Thickness(358, 14, 0, 0);
                Gridd.Margin = new Thickness(22, 126, 0, 0);
                Gride.Margin = new Thickness(134, 126, 0, 0);
                Gridf.Margin = new Thickness(246, 126, 0, 0);
                Gridg.Margin = new Thickness(358, 126, 0, 0);
                Gridh.Margin = new Thickness(22, 238, 0, 0);
                Gridi.Margin = new Thickness(134, 238, 0, 0);
                Gridj.Margin = new Thickness(246, 238, 0, 0);
                Gridk.Margin = new Thickness(358, 238, 0, 0);
                Gridl.Margin = new Thickness(22, 350, 0, 0);
                Gridm.Margin = new Thickness(134, 350, 0, 0);
                Gridn.Margin = new Thickness(246, 350, 0, 0);
                Grido.Margin = new Thickness(358, 350, 0, 0);
                Gridp.Margin = new Thickness(22, 462, 0, 0);
                Gridq.Margin = new Thickness(134, 462, 0, 0);
                Gridr.Margin = new Thickness(246, 462, 0, 0);
                Grids.Margin = new Thickness(358, 462, 0, 0);
                Gridt.Margin = new Thickness(22, 574, 0, 0);
                Gridu.Margin = new Thickness(134, 574, 0, 0);
                Gridv.Margin = new Thickness(246, 574, 0, 0);
                Gridw.Margin = new Thickness(358, 574, 0, 0);
                Gridx.Margin = new Thickness(22, 686, 0, 0);
                Gridy.Margin = new Thickness(134, 686, 0, 0);
                Gridz.Margin = new Thickness(246, 686, 0, 0);
                GridGlobal.Margin = new Thickness(358, 686, 0, 0);

                //Wartebildschirm umstellen
                GRUpdateList.Height = 854;
                GRUpdateList.Width = 480;

                //Hintergrundfarbe einstellen
                LayoutRoot.Background = ConvertToSolidColorBrush(BackgroundColor, -1);

                //Hintergrundbild umstellen
                if (BackgroundPortrait == true)
                {
                    //Wenn Ordner der Hintergrundbilder vorhanden
                    if (file.DirectoryExists("/Background"))
                    {
                        if (file.FileExists("/Background/Portrait.jpg"))
                        {
                            //Bilder laden
                            byte[] data1;
                            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                            {
                                using (IsolatedStorageFileStream isfs = isf.OpenFile("/Background/Portrait.jpg", FileMode.Open, FileAccess.Read))
                                {
                                    data1 = new byte[isfs.Length];
                                    isfs.Read(data1, 0, data1.Length);
                                    isfs.Close();
                                }
                            }
                            try
                            {
                                MemoryStream ms = new MemoryStream(data1);
                                BitmapImage bi = new BitmapImage();
                                bi.SetSource(ms);
                                var imageBrush = new ImageBrush();
                                imageBrush.ImageSource = bi;
                                LayoutRoot.Background = imageBrush;
                            }
                            catch
                            {
                                ResetDesign();
                            }
                        }
                    }
                }

                //Media Player Hintergrundfarbe umstellen
                GRFullscreenPlayer.Background = ConvertToSolidColorBrush(MediaPlayerBigBackgroundColor, -1);

                //Media Player Hintergrundbild umstellen
                if (MPBigBackgroundPortrait == true)
                {
                    //Wenn Ordner der Hintergrundbilder vorhanden
                    if (file.DirectoryExists("/Background"))
                    {
                        if (file.FileExists("/Background/MPBigPortrait.jpg"))
                        {
                            //Bilder laden
                            byte[] data1;
                            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                            {
                                using (IsolatedStorageFileStream isfs = isf.OpenFile("/Background/MPBigPortrait.jpg", FileMode.Open, FileAccess.Read))
                                {
                                    data1 = new byte[isfs.Length];
                                    isfs.Read(data1, 0, data1.Length);
                                    isfs.Close();
                                }
                            }
                            try
                            {
                                MemoryStream ms = new MemoryStream(data1);
                                BitmapImage bi = new BitmapImage();
                                bi.SetSource(ms);
                                var imageBrush = new ImageBrush(); imageBrush.ImageSource = bi;
                                GRFullscreenPlayer.Background = imageBrush;
                            }
                            catch
                            {
                                ResetDesign();
                            }
                        }
                    }
                }

            }
        }
        # endregion





        #region LongListSelector
        //Eigener Long List Selector Buttons
        //---------------------------------------------------------------------------------------------------------
        //Aktueller Anfangsbuchstabe
        string LastLetter = "none";



        //Variabeln
        int lsSharp = -1;
        int lsa = -1;
        int lsb = -1;
        int lsc = -1;
        int lsd = -1;
        int lse = -1;
        int lsf = -1;
        int lsg = -1;
        int lsh = -1;
        int lsi = -1;
        int lsj = -1;
        int lsk = -1;
        int lsl = -1;
        int lsm = -1;
        int lsn = -1;
        int lso = -1;
        int lsp = -1;
        int lsq = -1;
        int lsr = -1;
        int lss = -1;
        int lst = -1;
        int lsu = -1;
        int lsv = -1;
        int lsw = -1;
        int lsx = -1;
        int lsy = -1;
        int lsz = -1;
        int lsGlobal = -1;
        


        //List Selector erstellen
        void CreateListSelector(string Artist, string LastLetter)
        {
            //Artist the, The, THE entfernen
            Artist = Artist.Trim();
            if (Artist.Length > 3)
            {
                string ArtistFirstLetters = Artist.Substring(0, 3);
                if (ArtistFirstLetters == "The" | ArtistFirstLetters == "the" | ArtistFirstLetters == "THE")
                {
                    Artist = Artist.Substring(3, (Artist.Length - 3));
                    Artist = Artist.Trim();
                }
            }

            //Farbe umwandeln
            if (ListSelectorColor == "AC")
            {
                ListSelectorColor = Application.Current.Resources["PhoneAccentColor"].ToString();
            }
            SolidColorBrush sbListSelectorColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            if (ListSelectorColor.Length == 9)
            {
                byte A = Convert.ToByte(ListSelectorColor.Substring(1, 2), 16);
                byte R = Convert.ToByte(ListSelectorColor.Substring(3, 2), 16);
                byte G = Convert.ToByte(ListSelectorColor.Substring(5, 2), 16);
                byte B = Convert.ToByte(ListSelectorColor.Substring(7, 2), 16);
                sbListSelectorColor = new SolidColorBrush(Color.FromArgb(A, R, G, B));
            }
            //Variabeln
            string FirstLetter = Artist[0].ToString();
            bool Done = false;
            bool Available = false;



            //Prüfen ob Unknown ist
            if (Artist == "Unknown" & LastLetter == "none")
            {
                lsSharp = ListSongs.Count;
                ListSongs.Add(new ClassMedia(ListSongs.Count, "#", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                GridSharp.Background = sbListSelectorColor;
                Done = true;
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob 0-9 ist
            Regex prof = new Regex("[0-9]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsSharp == -1)
                {
                    lsSharp = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "#", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    GridSharp.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob aA ist
            prof = new Regex("[Aa Àà Áá Ââ Ãã Ää Åå Ǻǻ Āā Ăă Ąą Ǎǎ Ǟǟ Ǡǡ Ȁȁ Ȧȧ Ⱥⱥ Ḁḁ ẚ Ạạ Ảả Ấấ Ầầ Ấấ Ẩẩ Ẫẫ Ậậ Ắắ Ằằ Ẳẳ Ẵẵ Ặặ Ææ Ǽǽ Ǣǣ Ɑɑ Ɐɐ Ɒɒ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsa == -1)
                {
                    lsa = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "a", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Grida.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob bB ist
            prof = new Regex("[Bb Ḃḃ  Ḅḅ  Ḇḇ  Ƀƀ  Ɓɓ  Ƃƃ  ᵬ  ᶀ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsb == -1)
                {
                    lsb = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "b", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridb.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob cC ist
            prof = new Regex("[Cc Çç  Ćć  Ĉĉ  Ċċ  Čč  Ƈƈ  Ȼȼ  Ḉḉ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsc == -1)
                {
                    lsc = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "c", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridc.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob dD ist
            prof = new Regex("[Dd Đð  Ďď  Đđ  Ɖɖ  Ɗɗ  Ƌƌ  ƍ  Ḋḋ  Ḍḍ  Ḏḏ  Ḑḑ  Ḓḓ  ẟ  D́d́]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsd == -1)
                {
                    lsd = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "d", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridd.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob eE ist
            prof = new Regex("[Ee Èè  Éé  Êê  Ëë  Ēē  Ĕĕ  Ėė  Ė̄ė̄  Ęę  Ěě  Ǝǝ  Ɛɛ  Əə  Ȅȅ  Ȇȇ  Ȩȩ  Ɇɇ  Ḕḕ  Ḗḗ  Ḙḙ  Ḛḛ  Ḝḝ  Ẹẹ  Ẻẻ  Ẽẽ  Ếế  Ềề  Ểể  Ễễ  Ệệ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lse == -1)
                {
                    lse = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "e", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gride.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob fF ist
            prof = new Regex("[Ff Ḟḟ  Ƒƒ  ᵮ  ᶂ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsf == -1)
                {
                    lsf = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "f", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridf.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob gG ist
            prof = new Regex("[Gg Ĝĝ  Ğğ  Ġġ  Ģģ  Ɠɠ  Ǥǥ  Ǧǧ  Ǵǵ  Ḡḡ  G̃]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsg == -1)
                {
                    lsg = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "g", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridg.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob hH ist
            prof = new Regex("[Hh Ĥĥ  Ħħ  Ƕƕ  Ȟȟ  Ḣḣ  Ḥḥ  Ḧḧ  Ḩḩ  Ḫḫ  ẖ  Ⱨⱨ  Ɥɥ  Ɦɦ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsh == -1)
                {
                    lsh = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "h", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridh.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob iI ist
            prof = new Regex("[Ii Ìì  Íí  Îî  Ïï  Ĩĩ  Īī  Ĭĭ  Įį  İi  Iı  Ĳĳ  Ɩɩ  Ɨɨ  Ǐǐ  Ȉȉ  Ȋȋ  Ḭḭ  Ḯḯ  Ỉỉ  Ịị]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsi == -1)
                {
                    lsi = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "i", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridi.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob jJ ist
            prof = new Regex("[Jj Ĵĵ  Ɉɉ  ǰ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsj == -1)
                {
                    lsj = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "j", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridj.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob kK ist
            prof = new Regex("[Kk Ķķ  ĸ  Ǩǩ  Ƙƙ  Ḱḱ  Ḳḳ  Ḵḵ  Ⱪⱪ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsk == -1)
                {
                    lsk = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "k", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridk.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob lL ist
            prof = new Regex("[Ll Ĺĺ  Ļļ  Ľľ  Ŀŀ  Łł  Ƚƚ  ƛ  ȴ  Ḷḷ  Ḹḹ  Ḻḻ  Ḽḽ  Ⱡⱡ  Ɫɫ  Ỻỻ  Ꝇꝇ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsl == -1)
                {
                    lsl = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "l", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridl.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob mM ist
            prof = new Regex("[Mm Ɯɯ  Ḿḿ  Ṁṁ  Ṃṃ  Ɱɱ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsm == -1)
                {
                    lsm = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "m", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridm.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob nN ist
            prof = new Regex("[Nn Ññ  Ńń  Ņņ  Ňň  ŉ  Ŋŋ  Ɲɲ  Ƞƞ  Ǹǹ  Ṅṅ  Ṇṇ  Ṉṉ  Ṋṋ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsn == -1)
                {
                    lsn = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "n", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridn.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob oO ist
            prof = new Regex("[Oo Òò  Óó  Ôô  Õõ  Öö  Øø  Ǿǿ  Ōō  Ŏŏ  Őő  Ɔɔ  Ɵɵ  Ơơ  Ǒǒ  Ǫǫ  Ǭǭ  Ȍȍ  Ȏȏ  Ȣȣ  Ȫȫ  Ȭȭ  Ȯȯ  Ȱȱ  Ṍṍ  Ṏṏ  Ṑṑ  Ṓṓ  Ọọ  Ỏỏ  Ốố  Ồồ  Ổổ  Ỗỗ  Ộộ  Ớớ  Ờờ  Ởở  Ỡỡ  Ợợ  O͘o͘  Ꝋꝋ  Ꝍꝍ  Ꝏꝏ  ⱺ  Œœ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lso == -1)
                {
                    lso = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "o", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Grido.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob pP ist
            prof = new Regex("[Pp Ƥƥ  Ṕṕ  Ṗṗ  Ᵽᵽ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsp == -1)
                {
                    lsp = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "p", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridp.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob qQ ist
            prof = new Regex("[Qq Ɋɋ  Ꝗꝗ  Ꝙꝙ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsq == -1)
                {
                    lsq = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "q", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridq.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob rR ist
            prof = new Regex("[Rr Ŕŕ  Ŗŗ  Řř  Ʀ  Ȑȑ  Ȓȓ  Ɍɍ  Ṙṙ  Ṛṛ  Ṝṝ  Ṟṟ  Ɽɽ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsr == -1)
                {
                    lsr = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "r", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridr.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob sS ist
            prof = new Regex("[Ss Śś  Şş  Ŝŝ  Šš  Șș  Ṥṥ  Ṧṧ  Ṡṡ  Ṣṣ  Ṩṩ  ȿ  ʂ  ᵴ  ᶊ  Ꞩꞩ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lss == -1)
                {
                    lss = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "s", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Grids.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob tT ist
            prof = new Regex("[Tt Ţţ  Ťť  Ŧŧ  ƫ  Ƭƭ  Ʈʈ  Țț  Ṫṫ  Ṭṭ  Ṯṯ  Ṱṱ  T̈ẗ  T́t́]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lst == -1)
                {
                    lst = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "t", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridt.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob uU ist
            prof = new Regex("[Uu Úú  Ûû  Ùù  Üü  Ŭŭ  Ũũ  Ūū  Ůů  Űű  Ųų  Ưư  Ǔǔ  Ǖǖ  Ǘǘ  Ǚǚ  Ǜǜ  Ȕȕ  Ȗȗ  Ʉʉ  Ṳṳ  Ṵṵ  Ṷṷ  Ṹṹ  Ṻṻ  Ụụ  Ủủ  Ứứ  Ừừ  Ửử  Ữữ  Ựự]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsu == -1)
                {
                    lsu = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "u", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridu.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob vV ist
            prof = new Regex("[Vv Ʋʋ  Ʌʌ  Ṽṽ  Ṿṿ  ⱴ  ⱱ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsv == -1)
                {
                    lsv = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "v", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridv.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob wW ist
            prof = new Regex("[Ww Ŵŵ  Ẁẁ  Ẃẃ  Ẅẅ  Ẇẇ  Ẉẉ  ẘ  Ⱳⱳ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsw == -1)
                {
                    lsw = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "w", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Done = true;
                    Gridw.Background = sbListSelectorColor;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob xX ist
            prof = new Regex("[Xx Ẍẍ  Ẋẋ  ×]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsx == -1)
                {
                    lsx = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "x", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridx.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob yY ist
            prof = new Regex("[Yy Ýý  Ÿÿ  Ŷŷ  Ƴƴ  Ȳȳ  Ɏɏ  Ẏẏ  Ỳỳ  ẙ  Ỵỵ  Ỷỷ  Ỹỹ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsy == -1)
                {
                    lsy = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "y", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridy.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Prüfen ob zZ ist
            prof = new Regex("[Zz Źź  Żż  Žž  Ƶƶ  Ȥȥ  Ɀɀ  Ẑẑ  Ẓẓ  Ẕẕ  Ⱬⱬ  Ʒʒ  Ƹƹ  Ǯǯ]");
            if (prof.IsMatch(Artist[0].ToString()) & Done == false)
            {
                if (lsz == -1)
                {
                    lsz = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "z", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    Gridz.Background = sbListSelectorColor;
                    Done = true;
                }
                //Wenn Global vorhanden, Global löschen
                if (lsGlobal != -1)
                {
                    ListSongs.RemoveAt(lsGlobal);
                    lsGlobal = -1;
                }
                Available = true;
            }

            //Wenn Buchstaben nicht vorhanden, als Global listen
            if (Available == false)
            {
                if (lsGlobal == -1)
                {
                    lsGlobal = ListSongs.Count;
                    ListSongs.Add(new ClassMedia(ListSongs.Count, "@", "", "", ImageSize, "Collapsed", "Segoe WP Semibold", ListSelectorColor, SelectedForegroundColor, "0,36,0,0", false, "ListSelector", "", "false", SelectAndPlay, false));
                    GridGlobal.Background = sbListSelectorColor;
                    Done = true;
                }

            }
        }



        //List Selector Buttons
        private void LSSharp_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsSharp != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "Sharp";
                Timer_ListSelector.Start();
            }
        }
        private void LSa_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsa != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "a";
                Timer_ListSelector.Start();
            }
        }
        private void LSb_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsb != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "b";
                Timer_ListSelector.Start();
            }
        }
        private void LSc_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsc != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "c";
                Timer_ListSelector.Start();
            }
        }
        private void LSd_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsd != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "d";
                Timer_ListSelector.Start();
            }
        }
        private void LSe_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lse != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "e";
                Timer_ListSelector.Start();
            }
        }
        private void LSf_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsf != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "f";
                Timer_ListSelector.Start();
            }
        }
        private void LSg_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsg != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "g";
                Timer_ListSelector.Start();
            }
        }
        private void LSh_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsh != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "h";
                Timer_ListSelector.Start();
            }
        }
        private void LSi_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsi != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "i";
                Timer_ListSelector.Start();
            }
        }
        private void LSj_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsj != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "j";
                Timer_ListSelector.Start();
            }
        }
        private void LSk_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsk != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "k";
                Timer_ListSelector.Start();
            }
        }
        private void LSl_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsl != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "l";
                Timer_ListSelector.Start();
            }
        }
        private void LSm_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsm != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "m";
                Timer_ListSelector.Start();
            }
        }
        private void LSn_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsn != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "n";
                Timer_ListSelector.Start();
            }
        }
        private void LSo_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lso != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "o";
                Timer_ListSelector.Start();
            }
        }
        private void LSp_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsp != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "p";
                Timer_ListSelector.Start();
            }
        }
        private void LSq_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsq != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "q";
                Timer_ListSelector.Start();
            }
        }
        private void LSr_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsr != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "r";
                Timer_ListSelector.Start();
            }
        }
        private void LSs_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lss != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "s";
                Timer_ListSelector.Start();
            }
        }
        private void LSt_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lst != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "t";
                Timer_ListSelector.Start();
            }
        }
        private void LSu_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsu != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "u";
                Timer_ListSelector.Start();
            }
        }
        private void LSv_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsv != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "v";
                Timer_ListSelector.Start();
            }
        }
        private void LSw_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsw != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "w";
                Timer_ListSelector.Start();
            }
        }
        private void LSx_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsx != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "x";
                Timer_ListSelector.Start();
            }
        }
        private void LSy_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsy != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "y";
                Timer_ListSelector.Start();
            }
        }
        private void LSz_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsz != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "z";
                Timer_ListSelector.Start();
            }
        }
        private void LSGlobal_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsGlobal != -1)
            {
                LBSongs_ChangeSelection = false;
                LBSongs.SelectedIndex = (LBSongs.Items.Count - 1);
                NextSelectedLetter = "Global";
                Timer_ListSelector.Start();
            }
        }



        //ListSelector neu Liste
        void ListSelectorListNew(string Letter)
        {
            if (Letter == "#")
            {
                lsSharp = TempListSongs.Count();
            }
            if (Letter == "a")
            {
                lsa = TempListSongs.Count();
            }
            if (Letter == "b")
            {
                lsb = TempListSongs.Count();
            }
            if (Letter == "c")
            {
                lsc = TempListSongs.Count();
            }
            if (Letter == "d")
            {
                lsd = TempListSongs.Count();
            }
            if (Letter == "e")
            {
                lse = TempListSongs.Count();
            }
            if (Letter == "f")
            {
                lsf = TempListSongs.Count();
            }
            if (Letter == "g")
            {
                lsg = TempListSongs.Count();
            }
            if (Letter == "h")
            {
                lsh = TempListSongs.Count();
            }
            if (Letter == "i")
            {
                lsi = TempListSongs.Count();
            }
            if (Letter == "j")
            {
                lsj = TempListSongs.Count();
            }
            if (Letter == "k")
            {
                lsk = TempListSongs.Count();
            }
            if (Letter == "l")
            {
                lsl = TempListSongs.Count();
            }
            if (Letter == "m")
            {
                lsm = TempListSongs.Count();
            }
            if (Letter == "n")
            {
                lsn = TempListSongs.Count();
            }
            if (Letter == "o")
            {
                lso = TempListSongs.Count();
            }
            if (Letter == "p")
            {
                lsp = TempListSongs.Count();
            }
            if (Letter == "q")
            {
                lsq = TempListSongs.Count();
            }
            if (Letter == "r")
            {
                lsr = TempListSongs.Count();
            }
            if (Letter == "s")
            {
                lss = TempListSongs.Count();
            }
            if (Letter == "t")
            {
                lst = TempListSongs.Count();
            }
            if (Letter == "u")
            {
                lsu = TempListSongs.Count();
            }
            if (Letter == "v")
            {
                lsv = TempListSongs.Count();
            }
            if (Letter == "w")
            {
                lsw = TempListSongs.Count();
            }
            if (Letter == "x")
            {
                lsx = TempListSongs.Count();
            }
            if (Letter == "y")
            {
                lsy = TempListSongs.Count();
            }
            if (Letter == "z")
            {
                lsz = TempListSongs.Count();
            }
        }


        //ListSelector Farben wechseln
        void ListSelectorChangeColor()
        {
            GridSharp.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Grida.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridb.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridc.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridd.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gride.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridf.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridg.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridh.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridi.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridj.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridk.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridl.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridm.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridn.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Grido.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridp.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridq.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridr.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Grids.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridt.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridu.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridv.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridw.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridx.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridy.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            Gridz.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
            GridGlobal.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
        }



        //Variabeln zurückstellen
        void ClearListSelectorVars()
        {
            lsSharp = -1;
            lsa = -1;
            lsb = -1;
            lsc = -1;
            lsd = -1;
            lse = -1;
            lsf = -1;
            lsg = -1;
            lsh = -1;
            lsi = -1;
            lsj = -1;
            lsk = -1;
            lsl = -1;
            lsm = -1;
            lsn = -1;
            lso = -1;
            lsp = -1;
            lsq = -1;
            lsr = -1;
            lss = -1;
            lst = -1;
            lsu = -1;
            lsv = -1;
            lsw = -1;
            lsx = -1;
            lsy = -1;
            lsz = -1;
            lsGlobal = -1;
        }



        //Variabeln Timer ListSelector
        string NextSelectedLetter = "none";
        bool NextSelectedDone = false;
        //Timer ListSelector
        void Timer_ListSelector_Tick(object sender, object e)
        {
            //Wenn Letze der Liste ausgewählt
            if (LBSongs.SelectedIndex == LBSongs.Items.Count - 1)
            {
                //Buchstaben auswählen
                if (NextSelectedLetter == "Sharp")
                {
                    LBSongs.SelectedIndex = lsSharp;
                }
                if (NextSelectedLetter == "a")
                {
                    LBSongs.SelectedIndex = lsa;
                }
                if (NextSelectedLetter == "b")
                {
                    LBSongs.SelectedIndex = lsb;
                }
                if (NextSelectedLetter == "c")
                {
                    LBSongs.SelectedIndex = lsc;
                }
                if (NextSelectedLetter == "d")
                {
                    LBSongs.SelectedIndex = lsd;
                }
                if (NextSelectedLetter == "e")
                {
                    LBSongs.SelectedIndex = lse;
                }
                if (NextSelectedLetter == "f")
                {
                    LBSongs.SelectedIndex = lsf;
                }
                if (NextSelectedLetter == "g")
                {
                    LBSongs.SelectedIndex = lsg;
                }
                if (NextSelectedLetter == "h")
                {
                    LBSongs.SelectedIndex = lsh;
                }
                if (NextSelectedLetter == "i")
                {
                    LBSongs.SelectedIndex = lsi;
                }
                if (NextSelectedLetter == "j")
                {
                    LBSongs.SelectedIndex = lsj;
                }
                if (NextSelectedLetter == "k")
                {
                    LBSongs.SelectedIndex = lsk;
                }
                if (NextSelectedLetter == "l")
                {
                    LBSongs.SelectedIndex = lsl;
                }
                if (NextSelectedLetter == "m")
                {
                    LBSongs.SelectedIndex = lsm;
                }
                if (NextSelectedLetter == "n")
                {
                    LBSongs.SelectedIndex = lsn;
                }
                if (NextSelectedLetter == "o")
                {
                    LBSongs.SelectedIndex = lso;
                }
                if (NextSelectedLetter == "p")
                {
                    LBSongs.SelectedIndex = lsp;
                }
                if (NextSelectedLetter == "q")
                {
                    LBSongs.SelectedIndex = lsq;
                }
                if (NextSelectedLetter == "r")
                {
                    LBSongs.SelectedIndex = lsr;
                }
                if (NextSelectedLetter == "s")
                {
                    LBSongs.SelectedIndex = lss;
                }
                if (NextSelectedLetter == "t")
                {
                    LBSongs.SelectedIndex = lst;
                }
                if (NextSelectedLetter == "u")
                {
                    LBSongs.SelectedIndex = lsu;
                }
                if (NextSelectedLetter == "v")
                {
                    LBSongs.SelectedIndex = lsv;
                }
                if (NextSelectedLetter == "w")
                {
                    LBSongs.SelectedIndex = lsw;
                }
                if (NextSelectedLetter == "x")
                {
                    LBSongs.SelectedIndex = lsx;
                }
                if (NextSelectedLetter == "y")
                {
                    LBSongs.SelectedIndex = lsy;
                }
                if (NextSelectedLetter == "z")
                {
                    LBSongs.SelectedIndex = lsz;
                }
                if (NextSelectedLetter == "Global")
                {
                    LBSongs.SelectedIndex = lsGlobal;
                }
                NextSelectedDone = true;
            }


            //Wenn Aktion ausgeführt wurde
            if (NextSelectedDone == true)
            {
                //Listbox Deselectieren
                try
                {
                    LBSongs.SelectedIndex = -1;
                }
                catch
                {
                }
                LBSongs_ChangeSelection = true;
                //Lististe wieder anzeigen
                LBSongs.Opacity = 1.0;
                //ListSelector verbergen
                GRListSelector.Visibility = System.Windows.Visibility.Collapsed;
                //Menu als geschlossen
                MenuOpen = false;
                //Timer stoppen
                Timer_ListSelector.Stop();
            }
        }
        # endregion





        # region Konverter, Funktionen usw.
        //Converter
        //---------------------------------------------------------------------------------------------------------------------------------
        //Farbe umwandeln
        SolidColorBrush ConvertToSolidColorBrush(string ARGB, int Alpha)
        {
            //Prüfen ob Alpha vorhanden
            byte A = Convert.ToByte(ARGB.Substring(1, 2), 16);
            if (Alpha > -1 & Alpha <= 255)
            {
                A = Convert.ToByte(Alpha);
            }
            byte R = Convert.ToByte(ARGB.Substring(3, 2), 16);
            byte G = Convert.ToByte(ARGB.Substring(5, 2), 16);
            byte B = Convert.ToByte(ARGB.Substring(7, 2), 16);
            SolidColorBrush sb = new SolidColorBrush(Color.FromArgb(A, R, G, B));
            return sb;
        }


        //Content für Farbbutton erstellen
        string CreateColorButtonContent(string ARGB)
        {
            if (ARGB == AppAccentColor)
            {
                ARGB = MyMusicPlayer.Resources.AppResources.Z001_AccentColor;
            }
            else if (ARGB == AppBackgroundColor)
            {
                ARGB = MyMusicPlayer.Resources.AppResources.Z001_PhoneBackground;
            }
            else if (ARGB == AppForegroundColor)
            {
                ARGB = MyMusicPlayer.Resources.AppResources.Z001_ForegroundColor;
            }
            return ARGB;
        }


        //Farbe aus Speichercodes erstellen
        string CreateColorFromCode(string Code)
        {
            if (Code == "AC")
            {
                Code = AppAccentColor;
            }
            else if (Code == "BG")
            {
                Code = AppBackgroundColor;
            }
            else if (Code == "FG")
            {
                Code = AppForegroundColor;
            }
            return Code;
        }


        //Farbe für Einstellung umwandeln
        string CreateSettingsColor(string ARGB)
        {
            if (ARGB == AppAccentColor)
            {
                ARGB = "AC";
            }
            else if (ARGB == AppBackgroundColor)
            {
                ARGB = "BG";
            }
            else if (ARGB == AppForegroundColor)
            {
                ARGB = "FG";
            }
            return ARGB;
        }


        //Zeit umwandeln
        public string CreateDurationString(TimeSpan Duration)
        {
            //Variabeln erstellen+
            string DurationString = "";

            //Wenn Tage vorhanden
            if (Duration.Days > 0)
            {
                DurationString += Duration.Days.ToString() + ":";
            }

            //Wenn Stunden vorhanden
            if (Duration.Hours > 0)
            {
                //Wenn Stunden größer als 9
                if (Duration.Hours.ToString().Length > 1)
                {
                    DurationString += Duration.Hours.ToString() + ":";
                }
                //Wenn Stunden kleiner als 9
                else
                {
                    DurationString += "0" + Duration.Hours.ToString() + ":";
                }
            }
            //Wenn keine Stunden vorhanden
            else
            {
                DurationString += "00:";
            }

            //Wenn Minuten vorhanden
            if (Duration.Minutes > 0)
            {
                //Wenn Minuten größer als 9
                if (Duration.Minutes.ToString().Length > 1)
                {
                    DurationString += Duration.Minutes.ToString() + ":";
                }
                //Wenn Minuten kleiner als 9
                else
                {
                    DurationString += "0" + Duration.Minutes.ToString() + ":";
                }
            }
            //Wenn keien Minuten vorhanden
            else
            {
                DurationString += "00:";
            }

            //Wenn Sekunden vorhanden
            if (Duration.Seconds > 0)
            {
                //Wenn Sekunden größer als 9
                if (Duration.Seconds.ToString().Length > 1)
                {
                    DurationString += Duration.Seconds.ToString();
                }
                //Wenn Sekunden kleiner als 9
                else
                {
                    DurationString += "0" + Duration.Seconds.ToString();
                }
            }
            //Wenn keine Sekunden vorhanden
            else
            {
                DurationString += "00";
            }

            //String zurückgeben
            return DurationString;
        }


        //Datum und Zeit in String ausgeben
        public string TimeSpanString()
        {
            string TimeSpanString = "";
            try
            {
                DateTime TempDt = DateTime.Now;
                string TempDtMin = TempDt.Minute.ToString();
                if (TempDtMin.Length == 1)
                {
                    TempDtMin = "0" + TempDtMin;
                }
                string TempDtH = TempDt.Hour.ToString();
                if (TempDtH.Length == 1)
                {
                    TempDtH = "0" + TempDtH;
                }
                TimeSpanString += TempDt.Day + "." + TempDt.Month + "." + TempDt.Year + " " + TempDtH + ":" + TempDtMin;
                return TimeSpanString;
            }
            catch
            {
            }
            return TimeSpanString;
        }


        //Design automatisch zurücksetzen
        void ResetDesign()
        {
            //MesageBox ausgeben
            MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ005_DesignError);

            //Design löschen
            if (file.FileExists("/Settings/Design.dat"))
            {
                file.DeleteFile("/Settings/Design.dat");
            }

            //Alle vorhandenen Bilder löschen
            if (file.FileExists("/Background/Portrait.jpg"))
            {
                file.DeleteFile("/Background/Portrait.jpg");
            }
            if (file.FileExists("/Background/Landscape.jpg"))
            {
                file.DeleteFile("/Background/Landscape.jpg");
            }
            if (file.FileExists("/Background/MPBigPortrait.jpg"))
            {
                file.DeleteFile("/Background/MPBigPortrait.jpg");
            }
            if (file.FileExists("/Background/MPBigLandscape.jpg"))
            {
                file.DeleteFile("/Background/MPBigLandscape.jpg");
            }

            //Neues Design erstellen
            //Daten in Storage laden
            using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Design.dat", UriKind.Relative)).Stream)
            {
                // Create a stream for the new file in the local folder.
                using (filestream = file.CreateFile("/Settings/Design.dat"))
                {
                    // Initialize the buffer.
                    byte[] readBuffer = new byte[4096];
                    int bytesRead = -1;

                    // Copy the file from the installation folder to the local folder. 
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        filestream.Write(readBuffer, 0, bytesRead);
                    }
                }
            }
            //Daten in Storage laden
            using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Landscape.jpg", UriKind.Relative)).Stream)
            {
                // Create a stream for the new file in the local folder.
                using (filestream = file.CreateFile("/Background/Landscape.jpg"))
                {
                    // Initialize the buffer.
                    byte[] readBuffer = new byte[4096];
                    int bytesRead = -1;

                    // Copy the file from the installation folder to the local folder. 
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        filestream.Write(readBuffer, 0, bytesRead);
                    }
                }
            }
            //Daten in Storage laden
            using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/Portrait.jpg", UriKind.Relative)).Stream)
            {
                // Create a stream for the new file in the local folder.
                using (filestream = file.CreateFile("/Background/Portrait.jpg"))
                {
                    // Initialize the buffer.
                    byte[] readBuffer = new byte[4096];
                    int bytesRead = -1;

                    // Copy the file from the installation folder to the local folder. 
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        filestream.Write(readBuffer, 0, bytesRead);
                    }
                }
            }
            //Daten in Storage laden
            using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/MPBigLandscape.jpg", UriKind.Relative)).Stream)
            {
                // Create a stream for the new file in the local folder.
                using (filestream = file.CreateFile("/Background/MPBigLandscape.jpg"))
                {
                    // Initialize the buffer.
                    byte[] readBuffer = new byte[4096];
                    int bytesRead = -1;

                    // Copy the file from the installation folder to the local folder. 
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        filestream.Write(readBuffer, 0, bytesRead);
                    }
                }
            }
            //Daten in Storage laden
            using (Stream input = Application.GetResourceStream(new Uri("Designs/Concert/MPBigPortrait.jpg", UriKind.Relative)).Stream)
            {
                // Create a stream for the new file in the local folder.
                using (filestream = file.CreateFile("/Background/MPBigPortrait.jpg"))
                {
                    // Initialize the buffer.
                    byte[] readBuffer = new byte[4096];
                    int bytesRead = -1;

                    // Copy the file from the installation folder to the local folder. 
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        filestream.Write(readBuffer, 0, bytesRead);
                    }
                }
            }

            //Catch leeren
            Timer_Settings_Action = "UpdateList";
            Timer_Settings.Start();
            //GoBack laden, um Seite neu zu erstellen
            NavigationService.Navigate(new Uri("/Pages/GoBack.xaml", UriKind.Relative));
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion, Funktionen 
    }
}
