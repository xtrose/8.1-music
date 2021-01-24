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
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Microsoft.Phone.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Media;
using System.Text;





namespace MyMusicPlayer.Pages
{





    public partial class ColorSettings : PhoneApplicationPage
    {





        //Allgemeine Variabeln
        //---------------------------------------------------------------------------------------------------------
        //IsoStore file erstellen
        IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
        //Filestream erstellen
        IsolatedStorageFileStream filestream;
        //Streamreade erstellen
        StreamReader sr;
        //StreamWriter erstellen 
        StreamWriter sw;

        //Timer Settings
        DispatcherTimer Timer_Settings = new DispatcherTimer();
        //Timer online Components
        DispatcherTimer dt = new DispatcherTimer();

        //Liste Designs
        ObservableCollection<ClassDesigns> ListDesigns = new ObservableCollection<ClassDesigns>();
        ObservableCollection<ClassOnlineDesigns> ListOnlineDesigns = new ObservableCollection<ClassOnlineDesigns>();
        ObservableCollection<ClassOnlineDesignImages> ListOnlineDesignImages = new ObservableCollection<ClassOnlineDesignImages>();
        
        //MenuOpen
        bool MenuOpen = false;

        //String aller Einstellungen
        string DesignString = "";

        //Vollversion
        bool Fullversion = false;

        //Logo beim Start
        bool LogoStart = true;

        //Erweiterte Informationen
        string ExtendedInformation = "true";

        //Zeit des anspielens
        int PlayOnTime = 10;

        //Farben der Auswahl erstellen
        string AppAccentColor;
        string AppBackgroundColor;
        string AppForegroundColor;
        string ImageSize = "26";
        string ListSelectorColor;
        string MediaPlayerBackgroundColor;
        string MediaPlayerAccentColor;
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

        // Online Design Variabeln
        int Designs_Area = 1;

        //Einstellungen repeat und Random
        bool SetShuffle = false;
        bool SetRepead = false;

        //PhotoCooserTask erstellen
        PhotoChooserTask photoChooserTask;

        // String für die Post Variablen
        Dictionary<string, string> post_parameters = new Dictionary<string, string>();
        //---------------------------------------------------------------------------------------------------------





        #region Wird beim ersten Start der Seite ausgeführt
        //Wird beim ersten Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------
        public ColorSettings()
        {
            //Komponenten laden
            InitializeComponent();


            //PhotoCooser Task
            photoChooserTask = new PhotoChooserTask();
            //Angeben was PhotoCooserTask ausführt wenn Bild ausgewählt
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);


            //Timer Settings einstellen
            Timer_Settings.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer_Settings.Tick += Timer_Settings_Tick;
            Timer_Settings.Stop();

            //Timer online Components
            dt.Stop();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 200);
            dt.Tick += new EventHandler(dt_Tick);


            //Hintergrundfarbe ermitteln
            Color backgroundColor = (Color)Application.Current.Resources["PhoneBackgroundColor"];
            AppBackgroundColor = Convert.ToString(backgroundColor);

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
            }


            //Akzentfarbe ermitteln
            AppAccentColor = ((Color)Application.Current.Resources["PhoneAccentColor"]).ToString();

            //Mediaplayer Hintergrundfarben erstellen
            BtnMediaPlayerBackgroundColor.Content = CreateColorButtonContent(MediaPlayerBackgroundColor);
            RTMediaPlayerBackgroundDemo.Fill = ConvertToSolidColorBrush(MediaPlayerBackgroundColor, -1);


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
        # endregion





        #region Wird bei jedem Start der Seite ausgeführt
        //Wird bei jedem Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------------------------------
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Ordner Settings laden
            filestream = file.OpenFile("Settings/Design.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            DesignString = sr.ReadToEnd();
            filestream.Close();

            //SettingsString zerlegen
            string[] SplitSettingString = Regex.Split(DesignString, ";");
            //Settings durchlaufen und Einstellungen umsetzen
            for (int i = 0; i < (SplitSettingString.Count() - 1); i++)
            {
                //Einstellung zerlegen und Prüfen
                string[] SplitSetting = Regex.Split(SplitSettingString[i], "=");

                //Farbe App Hintergrund
                if (SplitSetting[0] == "BackgroundColor")
                {
                    //Farbe erstellen
                    BackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    BtnBackgroundColor.Content = CreateColorButtonContent(BackgroundColor);
                    RTBackgroundColorDemo.Fill = ConvertToSolidColorBrush(BackgroundColor, -1);
                    LayoutRoot.Background = ConvertToSolidColorBrush(BackgroundColor, -1);
                }

                //Farbe App Vordergrund
                if (SplitSetting[0] == "ForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        ForegroundColor = "#FF000000";
                        BtnForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                        (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        ForegroundColor = "#FFFFFFFF";
                        BtnForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                        (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                    }
                }

                //Farbe MediaPlayer klein Akzent
                if (SplitSetting[0] == "MediaPlayerAccentColor")
                {
                    //Farbe erstellen
                    MediaPlayerAccentColor = CreateColorFromCode(SplitSetting[1]);
                    BtnMediaPlayerAccentColor.Content = CreateColorButtonContent(MediaPlayerAccentColor);
                    RTMediaPlayerAccentDemo.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
                }

                //Farbe MediaPlayer klein Hintergrund
                if (SplitSetting[0] == "MediaPlayerBackgroundColor")
                {
                    //Farbe erstellen
                    MediaPlayerBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    BtnMediaPlayerBackgroundColor.Content = CreateColorButtonContent(MediaPlayerBackgroundColor);
                    RTMediaPlayerBackgroundDemo.Fill = ConvertToSolidColorBrush(MediaPlayerBackgroundColor, -1);
                }

                //Farbe MediaPlayer groß Akzent
                if (SplitSetting[0] == "MediaPlayerBigAccentColor")
                {
                    //Farbe erstellen
                    MediaPlayerBigAccentColor = CreateColorFromCode(SplitSetting[1]);
                    BtnMediaPlayerBigAccentColor.Content = CreateColorButtonContent(MediaPlayerBigAccentColor);
                    RTMediaPlayerBigAccentDemo.Fill = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, -1);
                }

                //Farbe MediaPlayer groß Hintergrund
                if (SplitSetting[0] == "MediaPlayerBigBackgroundColor")
                {
                    //Farbe erstellen
                    MediaPlayerBigBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    BtnMediaPlayerBigBackgroundColor.Content = CreateColorButtonContent(MediaPlayerBigBackgroundColor);
                    RTMediaPlayerBigBackgroundDemo.Fill = ConvertToSolidColorBrush(MediaPlayerBigBackgroundColor, -1);
                    GRDesignMenu.Background = ConvertToSolidColorBrush(MediaPlayerBigBackgroundColor, -1);
                }

                //Farbe List Selector
                if (SplitSetting[0] == "ListSelectorColor")
                {
                    //Farbe erstellen
                    ListSelectorColor = CreateColorFromCode(SplitSetting[1]);
                    BtnListSelectorColor.Content = CreateColorButtonContent(ListSelectorColor);
                    RTListSelectorDemo.Fill = ConvertToSolidColorBrush(ListSelectorColor, -1);
                }

                //Farbe Artist Hintergrund
                if (SplitSetting[0] == "ArtistBackgroundColor")
                {
                    //Farbe erstellen
                    ArtistBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    BtnArtistBackgroundColor.Content = CreateColorButtonContent(ArtistBackgroundColor);
                    GRArtistDemo.Background = ConvertToSolidColorBrush(ArtistBackgroundColor, -1);
                }

                //Farbe Artist Vordergrund
                if (SplitSetting[0] == "ArtistForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        ArtistForegroundColor = "#FF000000";
                        BtnArtistForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                        TBArtistDemo.Foreground = ConvertToSolidColorBrush("#FF000000", -1);
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        ArtistForegroundColor = "#FFFFFFFF";
                        BtnArtistForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                        TBArtistDemo.Foreground = ConvertToSolidColorBrush("#FFFFFFFF", -1);
                    }
                }

                //Farbe Album Hintergrund
                if (SplitSetting[0] == "AlbumBackgroundColor")
                {
                    //Farbe erstellen
                    AlbumBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    BtnAlbumBackgroundColor.Content = CreateColorButtonContent(AlbumBackgroundColor);
                    GRAlbumDemo.Background = ConvertToSolidColorBrush(AlbumBackgroundColor, -1);
                }

                //Farbe Album Vordergrund
                if (SplitSetting[0] == "AlbumForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        AlbumForegroundColor = "#FF000000";
                        BtnAlbumForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                        TBAlbumDemo.Foreground = ConvertToSolidColorBrush("#FF000000", -1);
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        AlbumForegroundColor = "#FFFFFFFF";
                        BtnAlbumForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                        TBAlbumDemo.Foreground = ConvertToSolidColorBrush("#FFFFFFFF", -1);
                    }
                }

                //Farbe Song Hintergrund
                if (SplitSetting[0] == "SongBackgroundColor")
                {
                    //Farbe erstellen
                    SongBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    BtnSongBackgroundColor.Content = CreateColorButtonContent(SongBackgroundColor);
                    GRSongDemo.Background = ConvertToSolidColorBrush(SongBackgroundColor, -1);
                }

                //Farbe Song Vordergrund
                if (SplitSetting[0] == "SongForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        SongForegroundColor = "#FF000000";
                        BtnSongForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                        TBSongDemo.Foreground = ConvertToSolidColorBrush("#FF000000", -1);
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        SongForegroundColor = "#FFFFFFFF";
                        BtnSongForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                        TBSongDemo.Foreground = ConvertToSolidColorBrush("#FFFFFFFF", -1);
                    }
                }

                //Farbe Selected Hintergrund
                if (SplitSetting[0] == "SelectedBackgroundColor")
                {
                    //Farbe erstellen
                    SelectedBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    BtnSelectedBackgroundColor.Content = CreateColorButtonContent(SelectedBackgroundColor);
                    GRSelectedDemo.Background = ConvertToSolidColorBrush(SelectedBackgroundColor, -1);
                }

                //Farbe Selected Vordergrund
                if (SplitSetting[0] == "SelectedForegroundColor")
                {
                    //Wenn Farbe schwarz ist
                    if (SplitSetting[1] == "#FF000000")
                    {
                        SelectedForegroundColor = "#FF000000";
                        BtnSelectedForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                        TBSelectedDemo.Foreground = ConvertToSolidColorBrush("#FF000000", -1);
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        SelectedForegroundColor = "#FFFFFFFF";
                        BtnSelectedForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                        TBSelectedDemo.Foreground = ConvertToSolidColorBrush("#FFFFFFFF", -1);
                    }
                }
            }



            //Wenn Hintergundbild Portrait vorhanden
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
                MemoryStream ms = new MemoryStream(data1);
                BitmapImage bi = new BitmapImage();
                bi.SetSource(ms);
                var imageBrush = new ImageBrush();
                imageBrush.ImageSource = bi;
                LayoutRoot.Background = imageBrush;
                BtnBackgroundPicturePortrait.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                ImgDeleteBackgroundPicturePortrait.Opacity = 1.0;
            }
            else
            {
                BtnBackgroundPicturePortrait.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                ImgDeleteBackgroundPicturePortrait.Opacity = 0.5;
            }

            //Wenn Hintergundbild Landscape vorhanden
            if (file.FileExists("/Background/Landscape.jpg"))
            {
                //Bool umwandeln
                BackgroundLandscape = true;
                BtnBackgroundPictureLandscape.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                ImgDeleteBackgroundPictureLandscape.Opacity = 1.0;
            }
            else
            {
                BtnBackgroundPictureLandscape.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                ImgDeleteBackgroundPictureLandscape.Opacity = 0.5;
            }



            //Wenn Hintergundbild Media Player Portrait vorhanden
            if (file.FileExists("/Background/MPBigPortrait.jpg"))
            {
                //Bool umwandeln
                MPBigBackgroundPortrait = true;
                BtnMPBigBackgroundPicturePortrait.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                ImgMPBigDeleteBackgroundPicturePortrait.Opacity = 1.0;
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
                MemoryStream ms = new MemoryStream(data1);
                BitmapImage bi = new BitmapImage();
                bi.SetSource(ms);
                var imageBrush = new ImageBrush();
                imageBrush.ImageSource = bi;
                GRDesignMenu.Background = imageBrush;
            }
            else
            {
                BtnMPBigBackgroundPicturePortrait.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                ImgMPBigDeleteBackgroundPicturePortrait.Opacity = 0.5;
            }

            //Wenn Hintergundbild Media Player Landscape vorhanden
            if (file.FileExists("/Background/MPBigLandscape.jpg"))
            {
                //Bool umwandeln
                MPBigBackgroundLandscape = true;
                BtnMPBigBackgroundPictureLandscape.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                ImgMPBigDeleteBackgroundPictureLandscape.Opacity = 1.0;
            }
            else
            {
                BtnMPBigBackgroundPictureLandscape.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                ImgMPBigDeleteBackgroundPictureLandscape.Opacity = 0.5;
            }



            //Designs laden
            LoadingDesigns();


            //Bilder, Farben anpassen
            ChangeImagesColor();
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
                GRInstall.Background = ConvertToSolidColorBrush("#FF000000", -1);

                ImgLogo.Source = new BitmapImage(new Uri("/Images/Brush.Dark.png", UriKind.Relative));
                ImgLogo.Opacity = 0.2;
                ImgDesignMenu.Source = new BitmapImage(new Uri("/Images/Brush.Dark.png", UriKind.Relative));
                ImgDesignMenu.Opacity = 0.2;
                ImgDeleteBackgroundPictureLandscape.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                ImgDeleteBackgroundPicturePortrait.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                ImgMPBigDeleteBackgroundPictureLandscape.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                ImgMPBigDeleteBackgroundPicturePortrait.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                ImgDesignOpen.Source = new BitmapImage(new Uri("/Images/Brush.Dark.png", UriKind.Relative));
                ImgDesignEdit.Source = new BitmapImage(new Uri("/Images/Edit.Dark.png", UriKind.Relative));
                ImgDesignCopy.Source = new BitmapImage(new Uri("/Images/Copy.Dark.png", UriKind.Relative));
                ImgDesignDelete.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                ImgPictureSave.Source = new BitmapImage(new Uri("/Images/Save.Dark.png", UriKind.Relative));
                ImgImgLoad.Source = new BitmapImage(new Uri("/Images/Globe.Dark.png", UriKind.Relative));
                ImgShare.Source = new BitmapImage(new Uri("/Images/Share.Dark.png", UriKind.Relative));
                ArrowBack.Source = new BitmapImage(new Uri("/Images/Arrow.Left.Big.Dark.png", UriKind.Relative));
                ArrowNext.Source = new BitmapImage(new Uri("/Images/Arrow.Right.Big.Dark.png", UriKind.Relative));
            }
            //Wenn Vordergrundfarbe schwarz ist
            else
            {
                GRInstall.Background = ConvertToSolidColorBrush("#FFFFFFFF", -1);

                //Bilder und Logos ändern
                ImgLogo.Source = new BitmapImage(new Uri("/Images/Brush.Light.png", UriKind.Relative));
                ImgLogo.Opacity = 0.1;
                ImgDesignMenu.Source = new BitmapImage(new Uri("/Images/Brush.Light.png", UriKind.Relative));
                ImgDesignMenu.Opacity = 0.1;
                ImgDeleteBackgroundPictureLandscape.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                ImgDeleteBackgroundPicturePortrait.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                ImgMPBigDeleteBackgroundPictureLandscape.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                ImgMPBigDeleteBackgroundPicturePortrait.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                ImgDesignOpen.Source = new BitmapImage(new Uri("/Images/Brush.Light.png", UriKind.Relative));
                ImgDesignEdit.Source = new BitmapImage(new Uri("/Images/Edit.Light.png", UriKind.Relative));
                ImgDesignCopy.Source = new BitmapImage(new Uri("/Images/Copy.Light.png", UriKind.Relative));
                ImgDesignDelete.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                ImgPictureSave.Source = new BitmapImage(new Uri("/Images/Save.Light.png", UriKind.Relative));
                ImgImgLoad.Source = new BitmapImage(new Uri("/Images/Globe.Light.png", UriKind.Relative));
                ImgShare.Source = new BitmapImage(new Uri("/Images/Share.Light.png", UriKind.Relative));
                ArrowBack.Source = new BitmapImage(new Uri("/Images/Arrow.Left.Big.Light.png", UriKind.Relative));
                ArrowNext.Source = new BitmapImage(new Uri("/Images/Arrow.Right.Big.Light.png", UriKind.Relative));
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





         #region Designs Laden
        //Designs laden
        //---------------------------------------------------------------------------------------------------------------------------------
        void LoadingDesigns()
        {
            //Liste der Designs leeren
            ListDesigns.Clear();

            //Designs laden
            string[] AllDesigns = file.GetDirectoryNames("/Designs/*");
            //Designs auflisten
            for (int i = 0; i < AllDesigns.Count(); i++)
            {
                ListDesigns.Add(new ClassDesigns(AllDesigns[i]));
            }

            //Designs verknüpfen
            LBDesigns.ItemsSource = ListDesigns;
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





        # region Einstellungen erstellen
        //Einstellungen neu erstellen
        //---------------------------------------------------------------------------------------------------------
        //Einstellung erstellen
        void CreateDesignSettings()
        {
            //ListSelector Farbe umwandeln
            string tListSelectorColor = CreateSettingsColor(ListSelectorColor);
            //MediaPlayer klein Akzentfarbe umstellen
            string tMediaPlayerAccentColor = CreateSettingsColor(MediaPlayerAccentColor);
            //MediaPlayer klein Hintergrundfarbe umstellen
            string tMediaPlayerBackgroundColor = CreateSettingsColor(MediaPlayerBackgroundColor);
            //MediaPlayer groß Akzentfarbe umstellen
            string tMediaPlayerBigAccentColor = CreateSettingsColor(MediaPlayerBigAccentColor);
            //MediaPlayer groß Hintergrundfarbe umstellen
            string tMediaPlayerBigBackgroundColor = CreateSettingsColor(MediaPlayerBigBackgroundColor);
            //Artist Hintergrundfarbe umstellen
            string tArtistBackgroundColor = CreateSettingsColor(ArtistBackgroundColor);
            //Album Hintergrundfarbe umstellen
            string tAlbumBackgroundColor = CreateSettingsColor(AlbumBackgroundColor);
            //Song Hintergrundfarbe umstellen
            string tSongBackgroundColor = CreateSettingsColor(SongBackgroundColor);
            //Auswahl Hintergrundfarbe umstellen
            string tSelectedBackgroundColor = CreateSettingsColor(SelectedBackgroundColor);
            //Auswahl Vordergrundfarbe umstellen
            string tBackgroundColor = CreateSettingsColor(BackgroundColor);

            //DesignSettingsString erstellen
            DesignString = "ListSelectorColor=" + tListSelectorColor + ";MediaPlayerAccentColor=" + tMediaPlayerAccentColor + ";MediaPlayerBackgroundColor=" + tMediaPlayerBackgroundColor + ";MediaPlayerBigAccentColor=" + tMediaPlayerBigAccentColor + ";MediaPlayerBigBackgroundColor=" + tMediaPlayerBigBackgroundColor + ";ArtistBackgroundColor=" + tArtistBackgroundColor + ";ArtistForegroundColor=" + ArtistForegroundColor + ";AlbumBackgroundColor=" + tAlbumBackgroundColor + ";AlbumForegroundColor=" + AlbumForegroundColor + ";SongBackgroundColor=" + tSongBackgroundColor + ";SongForegroundColor=" + SongForegroundColor + ";SelectedBackgroundColor=" + tSelectedBackgroundColor + ";SelectedForegroundColor=" + SelectedForegroundColor + ";BackgroundColor=" + tBackgroundColor + ";ForegroundColor=" + ForegroundColor + ";BackgroundPortrait=" + BackgroundPortrait.ToString() + ";BackgroundLandscape=" + BackgroundLandscape.ToString() + ";";

            //Einstellungen in Settings/Settings schreiben
            filestream = file.CreateFile("Settings/Design.dat");
            sw = new StreamWriter(filestream);
            sw.Write(DesignString);
            sw.Flush();
            filestream.Close();
        }
        //---------------------------------------------------------------------------------------------------------
        # endregion





        # region Einstellungen
        //Einstellungen
        //---------------------------------------------------------------------------------------------------------------------------------
        //Variabeln
        string Timer_Settings_Action = "none";
        string ColorPicker_Action = "none";
        string SelectedBackgroundImage = "Portrait";


        //Hintergrundbild Portrait erstellen
        private void BtnBackgroundPicturePortrait_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackgroundImage = "Portrait";
            try
            {
                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                // Catch the exception, but no handling is necessary.
            }
        }
        //Hintergrundbild Portrait löschen
        private void BtnBackgroundPicturePortraitDelete_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Bild existiert
            if (BackgroundPortrait == true)
            {
                //Wenn Datei besteht
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_DeletePicture, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    if (file.FileExists("/Background/Portrait.jpg"))
                    {
                        file.DeleteFile("/Background/Portrait.jpg");
                        ImgDeleteBackgroundPicturePortrait.Opacity = 0.5;
                        BackgroundPortrait = false;
                        LayoutRoot.Background = ConvertToSolidColorBrush(BackgroundColor, -1);
                        BtnBackgroundPicturePortrait.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
                    }
                }
            }
        }


        //Hintergrundbild Landscape erstellen
        private void BtnBackgroundPictureLandscape_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackgroundImage = "Landscape";
            try
            {
                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                // Catch the exception, but no handling is necessary.
            }
        }
        //Hintergrundbild Landscape löschen 
        private void BtnBackgroundPictureLandscapeDelete_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Bild existiert
            if (BackgroundLandscape == true)
            {
                //Wenn Datei besteht
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_DeletePicture, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    if (file.FileExists("/Background/Landscape.jpg"))
                    {
                        file.DeleteFile("/Background/Landscape.jpg");
                        ImgDeleteBackgroundPictureLandscape.Opacity = 0.5;
                        BackgroundLandscape = false;
                        BtnBackgroundPictureLandscape.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
                    }
                }
            }
        }



        //Media Player Hintergrundbild Portrait erstellen
        private void BtnMPBigBackgroundPicturePortrait_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackgroundImage = "MPBigPortrait";
            try
            {
                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                // Catch the exception, but no handling is necessary.
            }
        }
        //Media Player Hintergrundbild Portrait löschen
        private void BtnMPBigBackgroundPicturePortraitDelete_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Bild existiert
            if (MPBigBackgroundPortrait == true)
            {
                //Wenn Datei besteht
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_DeletePicture, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    if (file.FileExists("/Background/MPBigPortrait.jpg"))
                    {
                        file.DeleteFile("/Background/MPBigPortrait.jpg");
                        ImgMPBigDeleteBackgroundPicturePortrait.Opacity = 0.5;
                        MPBigBackgroundPortrait = false;
                        BtnMPBigBackgroundPicturePortrait.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
                    }
                }
            }
        }


        //Media Player Hintergrundbild Landscape erstellen
        private void BtnMPBigBackgroundPictureLandscape_Click(object sender, RoutedEventArgs e)
        {
            SelectedBackgroundImage = "MPBigLandscape";
            try
            {
                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                // Catch the exception, but no handling is necessary.
            }
        }
        //Media Player Hintergrundbild Landscape löschen 
        private void BtnMPBigBackgroundPictureLandscapeDelete_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn Bild existiert
            if (MPBigBackgroundLandscape == true)
            {
                //Wenn Datei besteht
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_DeletePicture, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    if (file.FileExists("/Background/MPBigLandscape.jpg"))
                    {
                        file.DeleteFile("/Background/MPBigLandscape.jpg");
                        ImgMPBigDeleteBackgroundPictureLandscape.Opacity = 0.5;
                        MPBigBackgroundLandscape = false;
                        BtnMPBigBackgroundPictureLandscape.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
                    }
                }
            }
        }



        //Farbe Listen Auswahl
        private void BtnListSelectorColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "ListSelectorColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(ListSelectorColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(ListSelectorColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = ListSelectorColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }


        //Farbe MediaPlayer klein Akzent Auswahl
        private void BtnMediaPlayerAccentColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "MediaPlayerAccentColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(MediaPlayerAccentColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(MediaPlayerAccentColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = MediaPlayerAccentColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }


        //Farbe MediaPlayer klein Hintergrund Auswahl
        private void BtnMediaPlayerBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "MediaPlayerBackgroundColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(MediaPlayerBackgroundColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(MediaPlayerBackgroundColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = MediaPlayerBackgroundColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }



        //Farbe MediaPlayer groß Akzent Auswahl
        private void BtnMediaPlayerBigAccentColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "MediaPlayerBigAccentColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(MediaPlayerBigAccentColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = MediaPlayerBigAccentColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }


        //Farbe MediaPlayer groß Hintergrund Auswahl
        private void BtnMediaPlayerBigBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "MediaPlayerBigBackgroundColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(MediaPlayerBigBackgroundColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(MediaPlayerBigBackgroundColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = MediaPlayerBigBackgroundColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }


        //Farbe Künstler Hintergrund Auswahl
        private void BtnArtistBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "ArtistBackgroundColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(ArtistBackgroundColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(ArtistBackgroundColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = ArtistBackgroundColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }



        //Farbe Künstler Vordergrund Auswahl
        private void BtnArtistForegroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Wenn Farbe weiß ist
            if (ArtistForegroundColor == "#FFFFFFFF")
            {
                ArtistForegroundColor = "#FF000000";
                BtnArtistForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                TBArtistDemo.Foreground = ConvertToSolidColorBrush(ArtistForegroundColor, -1);
            }
            //Wenn Farbe schwarz ist
            else
            {
                ArtistForegroundColor = "#FFFFFFFF";
                BtnArtistForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                TBArtistDemo.Foreground = ConvertToSolidColorBrush(ArtistForegroundColor, -1);
            }
            //Einstellungen neu erstellen
            CreateDesignSettings();
            //Timer anweisen das Liste upgedatet wird
            Timer_Settings_Action = "UpdateList";
            Timer_Settings.Start();
        }



        //Farbe Album Hintergrund Auswahl
        private void BtnAlbumBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "AlbumBackgroundColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(AlbumBackgroundColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(AlbumBackgroundColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = AlbumBackgroundColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }



        //Farbe Album Vordergrund Auswahl
        private void BtnAlbumForegroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Wenn Farbe weiß ist
            if (AlbumForegroundColor == "#FFFFFFFF")
            {
                AlbumForegroundColor = "#FF000000";
                BtnAlbumForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                TBAlbumDemo.Foreground = ConvertToSolidColorBrush(AlbumForegroundColor, -1);
            }
            //Wenn Farbe schwarz ist
            else
            {
                AlbumForegroundColor = "#FFFFFFFF";
                BtnAlbumForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                TBAlbumDemo.Foreground = ConvertToSolidColorBrush(AlbumForegroundColor, -1);
            }
            //Einstellungen neu erstellen
            CreateDesignSettings();
            //Timer anweisen das Liste upgedatet wird
            Timer_Settings_Action = "UpdateList";
            Timer_Settings.Start();
        }



        //Farbe Song Hintergrund Auswahl
        private void BtnSongBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "SongBackgroundColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(SongBackgroundColor, 255);
            CP.Color = sb.Color;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(SongBackgroundColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            CPColorGrid.Background = sb;
            //Farbe in Color Schreiben
            CPColor.Text = SongBackgroundColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }



        //Farbe Song Vordergrund Auswahl
        private void BtnSongForegroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Wenn Farbe weiß ist
            if (SongForegroundColor == "#FFFFFFFF")
            {
                SongForegroundColor = "#FF000000";
                BtnSongForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                TBSongDemo.Foreground = ConvertToSolidColorBrush(SongForegroundColor, -1);
            }
            //Wenn Farbe schwarz ist
            else
            {
                SongForegroundColor = "#FFFFFFFF";
                BtnSongForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                TBSongDemo.Foreground = ConvertToSolidColorBrush(SongForegroundColor, -1);
            }
            //Einstellungen neu erstellen
            CreateDesignSettings();
            //Timer anweisen das Liste upgedatet wird
            Timer_Settings_Action = "UpdateList";
            Timer_Settings.Start();
        }


        //Farbe Selected Hintergrund Auswahl
        private void BtnSelectedBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "SelectedBackgroundColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(SelectedBackgroundColor, 255);
            CP.Color = sb.Color;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(SelectedBackgroundColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            CPColorGrid.Background = sb;
            //Farbe in Color Schreiben
            CPColor.Text = SelectedBackgroundColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }



        //Farbe Selected Vordergrund Auswahl
        private void BtnSelectedForegroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Wenn Farbe weiß ist
            if (SelectedForegroundColor == "#FFFFFFFF")
            {
                SelectedForegroundColor = "#FF000000";
                BtnSelectedForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                TBSelectedDemo.Foreground = ConvertToSolidColorBrush(SelectedForegroundColor, -1);
            }
            //Wenn Farbe schwarz ist
            else
            {
                SelectedForegroundColor = "#FFFFFFFF";
                BtnSelectedForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                TBSelectedDemo.Foreground = ConvertToSolidColorBrush(SelectedForegroundColor, -1);
            }
            //Einstellungen neu erstellen
            CreateDesignSettings();
            //Timer anweisen das Liste upgedatet wird
            Timer_Settings_Action = "UpdateList";
            Timer_Settings.Start();
        }



        //Farbe App Hintergrund
        private void BtnBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Color picker vorbereiten
            ColorPicker_Action = "BackgroundColor";
            //Farbe in Colorpicker einstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(BackgroundColor, 255);
            CP.Color = sb.Color;
            CPColorGrid.Background = sb;
            //Alpha Wert erstellen
            byte byteA = Convert.ToByte(BackgroundColor.Substring(1, 2), 16);
            CPSlider.Value = Convert.ToInt32(byteA);
            //Farbe in Color Schreiben
            CPColor.Text = BackgroundColor;
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }



        //Farbe App Vordergrund Auswahl
        private void BtnForegroundColor_Click(object sender, RoutedEventArgs e)
        {
            //Prüfen ob Fehlerhaft
            bool ColorError = false;
            //Wenn Farbe weiß ist
            if (ForegroundColor == "#FFFFFFFF")
            {
                //Wenn Hintergrundfarbe schwarz ist
                if (BackgroundColor == "#FF000000")
                {
                    ColorError = true;
                }
                else
                {
                    ForegroundColor = "#FF000000";
                    BtnForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_Black;
                    (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                    ChangeImagesColor();
                }
            }
            //Wenn Farbe schwarz ist
            else
            {
                //Wenn Hintergrundfarbe weiß ist
                if (BackgroundColor == "#FFFFFFFF")
                {
                    ColorError = true;
                }
                else
                {
                    ForegroundColor = "#FFFFFFFF";
                    BtnForegroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_White;
                    (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                    ChangeImagesColor();
                }
            }

            //Wenn  Fehler vorhanden
            if (ColorError == true)
            {
                MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ErrorForeground);
            }
            else
            {
                //Einstellungen neu erstellen
                CreateDesignSettings();
                //Farbe umstellen
                //Timer anweisen das Liste upgedatet wird
                Timer_Settings_Action = "UpdateList";
                Timer_Settings.Start();
            }
        }


        //Timer Einstellungen
        void Timer_Settings_Tick(object sender, object e)
        {
            //Bei ColorPicker
            if (Timer_Settings_Action == "ColorPicker")
            {
                //Alpha umstellen
                int tempAlpha = Convert.ToInt32(CPSlider.Value);
                //Wert in Anzeige umstellen
                CPTransparency.Text = tempAlpha.ToString();
                //Farbfeld umstellen
                SolidColorBrush sb = ConvertToSolidColorBrush(CP.Color.ToString(), tempAlpha);
                CPColorGrid.Background = sb;
                Color tempColor = sb.Color;
                CPColor.Text = tempColor.ToString();
            }
        }

        //Color Picker einstellungen
        private void CP_Yes_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Farbe auf Eingestellte Farbe stellen
            CreateColor();
        }
        private void CP_No_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Color Picker schließen
            GRColor.Visibility = System.Windows.Visibility.Collapsed;
            //Angeben das Menüs geschlossen sind
            MenuOpen = false;
        }
        private void CP_AccentColor_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Textbox umstellen
            CPColor.Text = AppAccentColor;
            //Farbe erstellen
            CreateColor();
        }
        private void CP_BackgroundColor_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Textbox umstellen
            CPColor.Text = AppBackgroundColor;
            //Farbe erstellen
            CreateColor();
        }
        private void CP_ForegroundColor_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Textbox umstellen
            CPColor.Text = AppForegroundColor;
            //Farbe erstellen
            CreateColor();
        }
        private void CP_ColorChanged(object sender, Color color)
        {
            //Farbfeld umstellen
            SolidColorBrush sb = ConvertToSolidColorBrush(CP.Color.ToString(), Convert.ToInt32(CPSlider.Value));
            CPColorGrid.Background = sb;
            Color tempColor = sb.Color;
            CPColor.Text = tempColor.ToString();
        }
        private void CPSlider_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            //Timer deaktivieren
            Timer_Settings_Action = "none";
            Timer_Settings.Stop();
        }
        private void CPSlider_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            //Timer aktivieren
            Timer_Settings_Action = "ColorPicker";
            Timer_Settings.Start();
        }

        private void CreateColor()
        {
            //Gibt an ob Daten gespeichert werden
            bool SaveData = true;

            //Farbe auf Eingestellte Farbe stellen
            if (ColorPicker_Action == "ListSelectorColor")
            {
                ListSelectorColor = CPColor.Text;
                BtnListSelectorColor.Content = CreateColorButtonContent(CPColor.Text);
                RTListSelectorDemo.Fill = ConvertToSolidColorBrush(ListSelectorColor, -1);
            }
            if (ColorPicker_Action == "MediaPlayerAccentColor")
            {
                MediaPlayerAccentColor = CPColor.Text;
                BtnMediaPlayerAccentColor.Content = CreateColorButtonContent(CPColor.Text);
                RTMediaPlayerAccentDemo.Fill = ConvertToSolidColorBrush(MediaPlayerAccentColor, -1);
            }
            if (ColorPicker_Action == "MediaPlayerBackgroundColor")
            {
                MediaPlayerBackgroundColor = CPColor.Text;
                BtnMediaPlayerBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                RTMediaPlayerBackgroundDemo.Fill = ConvertToSolidColorBrush(MediaPlayerBackgroundColor, -1);
            }
            if (ColorPicker_Action == "MediaPlayerBigAccentColor")
            {
                MediaPlayerBigAccentColor = CPColor.Text;
                BtnMediaPlayerBigAccentColor.Content = CreateColorButtonContent(CPColor.Text);
                RTMediaPlayerBigAccentDemo.Fill = ConvertToSolidColorBrush(MediaPlayerBigAccentColor, -1);
            }
            if (ColorPicker_Action == "MediaPlayerBigBackgroundColor")
            {
                MediaPlayerBigBackgroundColor = CPColor.Text;
                BtnMediaPlayerBigBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                RTMediaPlayerBigBackgroundDemo.Fill = ConvertToSolidColorBrush(MediaPlayerBigBackgroundColor, -1);
            }
            if (ColorPicker_Action == "ArtistBackgroundColor")
            {
                ArtistBackgroundColor = CPColor.Text;
                BtnArtistBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                GRArtistDemo.Background = ConvertToSolidColorBrush(ArtistBackgroundColor, -1);
            }
            if (ColorPicker_Action == "AlbumBackgroundColor")
            {
                AlbumBackgroundColor = CPColor.Text;
                BtnAlbumBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                GRAlbumDemo.Background = ConvertToSolidColorBrush(AlbumBackgroundColor, -1);
            }
            if (ColorPicker_Action == "SongBackgroundColor")
            {
                SongBackgroundColor = CPColor.Text;
                BtnSongBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                GRSongDemo.Background = ConvertToSolidColorBrush(SongBackgroundColor, -1);
            }
            if (ColorPicker_Action == "SelectedBackgroundColor")
            {
                SelectedBackgroundColor = CPColor.Text;
                BtnSelectedBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                GRSelectedDemo.Background = ConvertToSolidColorBrush(SelectedBackgroundColor, -1);
            }
            if (ColorPicker_Action == "BackgroundColor")
            {
                if (CPColor.Text == ForegroundColor)
                {
                    //Fehlermeldung ausgeben
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ErrorForeground);
                    //Angeben das Daten nicht gespeichert werden
                    SaveData = false;
                }
                else
                {
                    BackgroundColor = CPColor.Text;
                    LayoutRoot.Background = ConvertToSolidColorBrush(BackgroundColor, -1);
                    BtnBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                    RTBackgroundColorDemo.Fill = ConvertToSolidColorBrush(BackgroundColor, -1);
                }
            }

            //Wenn Daten gespeichert werden
            if (SaveData == true)
            {
                //Einstellungen neu erstellen
                CreateDesignSettings();
                //Color Picker schließen
                GRColor.Visibility = System.Windows.Visibility.Collapsed;
                ColorPicker_Action = "none";
                //Angeben das Menüs geschlossen sind
                MenuOpen = false;
                //Timer anweisen das Liste upgedatet wird
                Timer_Settings_Action = "UpdateList";
                Timer_Settings.Start();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        # region Konverter, Funktionen usw.
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
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion, Funktionen 





        # region Bild  ausschneiden und speichern
        //Bild  ausschneiden und speichern
        //---------------------------------------------------------------------------------------------------------------------------------
        //Bild Variabeln
        WriteableBitmap tempBitmap = new WriteableBitmap(0, 0);

        //Wenn Bild ausgewählt wurde
        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            //Ausgewähltes Bild verarbeiten
            if (e.TaskResult == TaskResult.OK)
            {
                //Bild in Writeable Bitmap speichern
                tempBitmap.SetSource(e.ChosenPhoto);

                //Bild größe auslesen
                double imgWidth = tempBitmap.PixelWidth;
                double imgHeight = tempBitmap.PixelHeight;
                double rest;
                double percent;


                //Wenn Portrait
                if (SelectedBackgroundImage == "Portrait" | SelectedBackgroundImage == "MPBigPortrait")
                {
                    //Versuchen Bild zu schneiden
                    percent = Convert.ToDouble(100) / imgWidth * Convert.ToDouble(480);
                    //Wenn Format in Ordnung
                    if ((imgHeight / 100 * percent) >= 854)
                    {
                        //Schnittkante errechnen errechnen
                        rest = ((imgHeight / 100 * percent) - Convert.ToDouble(854)) / Convert.ToDouble(2);
                        //Bild an Größe anpassen
                        tempBitmap = tempBitmap.Resize(480, Convert.ToInt32((imgHeight / 100 * percent)), WriteableBitmapExtensions.Interpolation.Bilinear);
                        //Bild schneiden
                        tempBitmap = tempBitmap.Crop(0, Convert.ToInt32(rest), 480, 854);
                    }
                    //Wenn anders geschnitten wird
                    else
                    {
                        //Prozent errechnen
                        percent = Convert.ToDouble(100) / imgHeight * Convert.ToDouble(854);
                        //Schnittkante errechnen errechnen
                        rest = ((imgWidth / 100 * percent) - Convert.ToDouble(480)) / Convert.ToDouble(2);
                        //Bild an Größe anpassen
                        tempBitmap = tempBitmap.Resize(Convert.ToInt32((imgWidth / 100 * percent)), 854, WriteableBitmapExtensions.Interpolation.Bilinear);
                        //Bild schneiden
                        tempBitmap = tempBitmap.Crop(Convert.ToInt32(rest), 0, 480, 854);
                    }
                }

                //Wenn Landscape
                else
                {
                    //Versuchen Bild zu schneiden
                    percent = Convert.ToDouble(100) / imgWidth * Convert.ToDouble(854);
                    //Wenn Format in Ordnung
                    if ((imgHeight / 100 * percent) >= 480)
                    {
                        //Schnittkante errechnen errechnen
                        rest = ((imgHeight / 100 * percent) - Convert.ToDouble(480)) / Convert.ToDouble(2);
                        //Bild an Größe anpassen
                        tempBitmap = tempBitmap.Resize(854, Convert.ToInt32((imgHeight / 100 * percent)), WriteableBitmapExtensions.Interpolation.Bilinear);
                        //Bild schneiden
                        tempBitmap = tempBitmap.Crop(0, Convert.ToInt32(rest), 854, 480);
                    }
                    //Wenn anders geschnitten wird
                    else
                    {
                        //Prozent errechnen
                        percent = Convert.ToDouble(100) / imgHeight * Convert.ToDouble(480);
                        //Schnittkante errechnen errechnen
                        rest = ((imgWidth / 100 * percent) - Convert.ToDouble(854)) / Convert.ToDouble(2);
                        //Bild an Größe anpassen
                        tempBitmap = tempBitmap.Resize(Convert.ToInt32((imgWidth / 100 * percent)), 854, WriteableBitmapExtensions.Interpolation.Bilinear);
                        //Bild schneiden
                        tempBitmap = tempBitmap.Crop(Convert.ToInt32(rest), 0, 854, 480);
                    }
                }


                //Datei in Isolated Storage schreiben
                if (!file.DirectoryExists("/Background"))
                {
                    file.CreateDirectory("/Background");
                }
                if (file.FileExists("/Background/" + SelectedBackgroundImage + ".jpg"))
                {
                    file.DeleteFile("/Background/" + SelectedBackgroundImage + ".jpg");
                }
                var isolatedStorageFileStream = file.CreateFile("/Background/" + SelectedBackgroundImage + ".jpg");
                if (SelectedBackgroundImage == "Portrait" | SelectedBackgroundImage == "MPBigPortrait")
                {
                    tempBitmap.SaveJpeg(isolatedStorageFileStream, 480, 854, 0, 80);
                }
                else
                {
                    tempBitmap.SaveJpeg(isolatedStorageFileStream, 854, 480, 0, 80);
                }
                isolatedStorageFileStream.Close();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





        #region Save Design und Design auswahl
        //Back Button
        //---------------------------------------------------------------------------------------------------------------------------------
        //Variabeln
        int SelectedDesign = -1;
        string DesignName = "";
        bool SystemDesign = false;



        //Design speichern
        private void BtnSaveDesign(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Zu SaveDesign verlinken
            NavigationService.Navigate(new Uri("/Pages/SaveDesign.xaml", UriKind.Relative));
        }



        //Design aus Liste wählen
        private void LBDesigns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Wenn angewendet wird
            if (LBDesigns.SelectedIndex != -1)
            {
                //SelectedDesign erstellen
                SelectedDesign = LBDesigns.SelectedIndex;

                //Name eintragen
                DesignName = ListDesigns[SelectedDesign].Name;
                TBDesignMenuName.Text = DesignName;

                //Prüfen ob System Design
                if (file.FileExists("/Designs/" + ListDesigns[SelectedDesign].Name + "/SystemDesign.dat"))
                {
                    SystemDesign = true;
                    ImgDesignDelete.Opacity = 0.5;
                    TBDesignDelete.Opacity = 0.5;
                    TBDesignEdit.Opacity = 0.5;
                    ImgDesignEdit.Opacity = 0.5;
                }
                else
                {
                    SystemDesign = false;
                    ImgDesignDelete.Opacity = 1.0;
                    TBDesignDelete.Opacity = 1.0;
                    TBDesignEdit.Opacity = 1.0;
                    ImgDesignEdit.Opacity = 1.0;
                }

                //Design Menü öffnen
                GRDesignMenu.Visibility = System.Windows.Visibility.Visible;
                MenuOpen = true;

                //Design abwählen
                try
                {
                    LBDesigns.SelectedIndex = -1;
                }
                catch
                {
                }
            }
        }



        //Design löschen
        private void BtnDeleteDesign(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn SystemDesign.dat nicht vorhanden
            if (!file.FileExists("/Designs/" + DesignName + "/SystemDesign.dat"))
            {
                //Warnung ausgeben
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_Delete, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //Design löschen
                    DeleteDirectory("/Designs/" + DesignName + "/");
                    //Designs neu laden
                    LoadingDesigns();
                    //Menü schließen
                    GRDesignMenu.Visibility = System.Windows.Visibility.Collapsed;
                    MenuOpen = false;
                }
            }
        }



        //Button Desing kopieren
        private void BtnCopyDesign(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Menü schließen
            GRDesignMenu.Visibility = System.Windows.Visibility.Collapsed;
            MenuOpen = false;
            //Zu RenameDesign verlinken
            NavigationService.Navigate(new Uri("/Pages/CopyDesign.xaml?design=" + SelectedDesign.ToString(), UriKind.Relative));
        }



        //Button Design umbenennen
        private void BtnRemaneDesign(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn SystemDesign.dat nicht vorhanden
            if (!file.FileExists("/Designs/" + DesignName + "/SystemDesign.dat"))
            {
                //Menü schließen
                GRDesignMenu.Visibility = System.Windows.Visibility.Collapsed;
                MenuOpen = false;
                //Zu RenameDesign verlinken
                NavigationService.Navigate(new Uri("/Pages/RenameDesign.xaml?design=" + SelectedDesign.ToString(), UriKind.Relative));
            }
        }


        //Button Design veröffentlichen
        private void BtnShareDesign(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Wenn SystemDesign.dat nicht vorhanden
            if (!file.FileExists("/Designs/" + DesignName + "/SystemDesign.dat"))
            {
                //Benachrichtigung ausgeben ausgeben
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_DesignShareNote, MyMusicPlayer.Resources.AppResources.ZZ002_Notification, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //Portrait Bild speichern
                    if (BackgroundPortrait == true)
                    {
                        // Create a new stream from isolated storage, and save the JPEG file to the media library on Windows Phone.
                        IsolatedStorageFileStream stream = file.OpenFile("/Background/Portrait.jpg", FileMode.Open, FileAccess.Read);
                        // Save the image to the camera roll or saved pictures album.
                        MediaLibrary library = new MediaLibrary();
                        try
                        {
                            // Save the image to the saved pictures album.
                            Picture pic = library.SavePicture("/Background/Portrait.jpg", stream);
                        }
                        catch
                        {
                        }
                    }
                    //Landscape Bild speichern
                    if (BackgroundLandscape == true)
                    {
                        // Create a new stream from isolated storage, and save the JPEG file to the media library on Windows Phone.
                        IsolatedStorageFileStream stream = file.OpenFile("/Background/Landscape.jpg", FileMode.Open, FileAccess.Read);
                        // Save the image to the camera roll or saved pictures album.
                        MediaLibrary library = new MediaLibrary();
                        try
                        {
                            // Save the image to the saved pictures album.
                            Picture pic = library.SavePicture("/Background/Landscape.jpg", stream);
                        }
                        catch
                        {
                        }
                    }
                    //Fullscreenplayer Portrait Bild speichern
                    if (MPBigBackgroundPortrait == true)
                    {
                        // Create a new stream from isolated storage, and save the JPEG file to the media library on Windows Phone.
                        IsolatedStorageFileStream stream = file.OpenFile("/Background/MPBigPortrait.jpg", FileMode.Open, FileAccess.Read);
                        // Save the image to the camera roll or saved pictures album.
                        MediaLibrary library = new MediaLibrary();
                        try
                        {
                            // Save the image to the saved pictures album.
                            Picture pic = library.SavePicture("/Background/MPBigPortrait.jpg", stream);
                        }
                        catch
                        {
                        }
                    }
                    //Fullscreenplayer Landscape Bild speichern
                    if (MPBigBackgroundLandscape == true)
                    {
                        // Create a new stream from isolated storage, and save the JPEG file to the media library on Windows Phone.
                        IsolatedStorageFileStream stream = file.OpenFile("/Background/MPBigLandscape.jpg", FileMode.Open, FileAccess.Read);
                        // Save the image to the camera roll or saved pictures album.
                        MediaLibrary library = new MediaLibrary();
                        try
                        {
                            // Save the image to the saved pictures album.
                            Picture pic = library.SavePicture("/Background/Landscape.jpg", stream);
                        }
                        catch
                        {
                        }
                    }
                    
                    //E-Mail öffnen
                    EmailComposeTask emailcomposer = new EmailComposeTask();
                    emailcomposer.To = "xtrose@hotmail.com";
                    emailcomposer.Subject = "8.1 Music Design";
                    emailcomposer.Body = MyMusicPlayer.Resources.AppResources.ZZ002_DesignShare_01 + "\n\n" + MyMusicPlayer.Resources.AppResources.ZZ002_DesignShare_02 + "\n\n" + MyMusicPlayer.Resources.AppResources.ZZ002_DesignShare_04 + "\n\n\n\n\n\n\n\n" + MyMusicPlayer.Resources.AppResources.ZZ002_DesignShare_03 + "\n----------------\n" + DesignString + "\n----------------\n\n";
                    emailcomposer.Show();
                }
            }
        }


        //Button Design anwenden
        private void BtnApplyDesign(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Warnung ausgeben
            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_WarningApplyDesign, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //Design löschen
                if (file.FileExists("/Settings/Design.dat"))
                {
                    file.DeleteFile("/Settings/Design.dat");
                }

                //Design kopieren
                file.CopyFile("/Designs/" + DesignName + "/Design.dat", "/Settings/Design.dat");

                //Wenn ob ordner vorhanden
                if (file.DirectoryExists("/Background"))
                {
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
                }
                //Wenn kein Ordner vorhanden
                else
                {
                    //Ordner erstellen
                    file.CreateDirectory("/Background");
                }

                //Bilder kopieren
                if (file.FileExists("/Designs/" + DesignName + "/Portrait.jpg"))
                {
                    file.CopyFile("/Designs/" + DesignName + "/Portrait.jpg", "/Background/Portrait.jpg");
                }
                if (file.FileExists("/Designs/" + DesignName + "/MPBigPortrait.jpg"))
                {
                    file.CopyFile("/Designs/" + DesignName + "/MPBigPortrait.jpg", "/Background/MPBigPortrait.jpg");
                }
                if (file.FileExists("/Designs/" + DesignName + "/Landscape.jpg"))
                {
                    file.CopyFile("/Designs/" + DesignName + "/Landscape.jpg", "/Background/Landscape.jpg");
                }
                if (file.FileExists("/Designs/" + DesignName + "/MPBigLandscape.jpg"))
                {
                    file.CopyFile("/Designs/" + DesignName + "/MPBigLandscape.jpg", "/Background/MPBigLandscape.jpg");
                }

                //Menü verbergen
                GRDesignMenu.Visibility = System.Windows.Visibility.Collapsed;
                MenuOpen = false;

                //Zu GoBack
                NavigationService.Navigate(new Uri("/Pages/GoBack.xaml", UriKind.Relative));
            }
        }



        //Button Design zurücksetzen
        private void BtnResetDesignSettings_Click(object sender, RoutedEventArgs e)
        {
            //Warnung ausgeben
            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ResetColorSettings, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
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
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





        #region Ordner mit gesamten Inhalt löschen
        //Ordner mit gesamten Inhalt löschen
        //---------------------------------------------------------------------------------------------------------
        public void DeleteDirectory(string target_dir)
        {
            try
            {
                //Ordner löschen
                IsolatedStorageFile file2 = IsolatedStorageFile.GetUserStoreForApplication();
                string[] files = file2.GetFileNames(target_dir);
                //string[] dirs = file11.GetDirectoryNames(target_dir);
                foreach (string file in files)
                {
                    file2.DeleteFile(target_dir + file);
                }
                file2.DeleteDirectory(target_dir);
            }
            catch
            {
            }
        }
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Back Button
        //Back Button
        //---------------------------------------------------------------------------------------------------------------------------------
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //Prüfen ob Menü offen ist und alle Menüs schließen
            if (MenuOpen == true)
            {
                //Menüs schließen
                GRColor.Visibility = System.Windows.Visibility.Collapsed;
                GRDesignMenu.Visibility = System.Windows.Visibility.Collapsed;
                GRInstall.Visibility = System.Windows.Visibility.Collapsed;
                StatusTimer = "none";
                dt.Stop();
                //Angeben das Menüs geschlossen sind
                MenuOpen = false;

                //Zurück oder beenden abbrechen
                e.Cancel = true;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion









        #region online components
        //Online Componenten laden
        //---------------------------------------------------------------------------------------------------------------------------------
        //Variabeln
        string SourceWebsite = "http://www.xtrose.com";
        string SourceUrl = "http://www.xtrose.com/xtrose/apps/8_1_music/files/";
        string StatusTimer = "none";
        string Source = "";
        string CheckSum = "";
        private void BtnConnect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StatusTimer == "none")
            {
                //Button umstellen
                TxtConnect.Text = MyMusicPlayer.Resources.AppResources.ZZ002_ConnectingNow;
                // Post Variablen erstellen
                post_parameters = new Dictionary<string, string>();
                post_parameters.Add("api", "1");
                post_parameters.Add("id", "14");
                post_parameters.Add("s", "R2OzRdeh71YthTJCtV3r");
                //Timer starten
                dt.Start();
                //Timer Status angeben
                StatusTimer = "LoadWebsite";
                //Seite versuchen zu erreichen
                GetSourceCode();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------





        //Aktion versuchen Seite zu erreichen und Quelltext in String laden
        //---------------------------------------------------------------------------------------------------------
        //Webseite versuchen zu erreichen
        public void GetSourceCode()
        {
            try
            {
                // Abfrage erstellen
                HttpWebRequest request = HttpWebRequest.CreateHttp(SourceWebsite);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                // POST Method Abfrage starten
                request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);
            }
            catch
            {
                //Wenn Webseite nicht erreichbar, Fehlermeldung ausgeben
                MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ConnectionFailed);
                //Zurück zum Start
                //NavigationService.GoBack();
            }
        }

        // POST Method Abfrage
        public void GetRequestStreamCallback(IAsyncResult callbackResult)
        {
            string post_data = "";
            foreach (string key in post_parameters.Keys)
            {
                post_data += HttpUtility.UrlEncode(key) + "="
                      + HttpUtility.UrlEncode(post_parameters[key]) + "&";
            }

            HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
            // End the stream request operation
            Stream postStream = request.EndGetRequestStream(callbackResult);

            // Create the post data
            byte[] byteArray = Encoding.UTF8.GetBytes(post_data);

            // Add the post data to the web request
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            // Start the web request
            request.BeginGetResponse(new AsyncCallback(handle_response), request);
        }

        //Quelltext in String speichern
        public void handle_response(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;

            try
            {
                if (request != null)
                {
                    using (WebResponse response = request.EndGetResponse(result))
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            //Quelltext laden
                            Source = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        //---------------------------------------------------------------------------------------------------------





        //Timer, Ablauf aller aktionen
        //---------------------------------------------------------------------------------------------------------
        //Variabeln
        int StartMS = 0;
        int UhrzeiMS = 0;

        //Variabeln Installation
        int InstallID = -1;
        string InstallName = "";
        bool InstallPortrait = false;
        bool InstallMPPortrait = false;
        bool InstallLandscape = false;
        bool InstallMPLandscape = false;
        int InstallFile = 0;
        //Aktion
        void dt_Tick(object sender, EventArgs e)
        {
            //Styles laden
            if (StatusTimer == "LoadWebsite")
            {
                //Prüfen ob Timeout
                bool TimeOut = false;


                //Aktuelle Uhrzeit Millisekunden erstellen
                DateTime Uhrzeit = DateTime.Now;
                UhrzeiMS = (Uhrzeit.Hour * 3600000) + (Uhrzeit.Minute * 60000) + (Uhrzeit.Second * 1000) + Uhrzeit.Millisecond;


                //Prüfen ob StartMS vorhanden
                if (StartMS == 0)
                {
                    StartMS = UhrzeiMS;
                }
                else
                {
                    if ((StartMS + 10000) < UhrzeiMS)
                    {
                        TimeOut = true;
                    }
                }


                //Prüfen ob Time out
                if (TimeOut == false)
                {
                    //Prüfen ob Quelle geladen
                    if (Source != "")
                    {
                        //wenn feedtemp = feed, Quelltext komplett geladen
                        if (Source == CheckSum)
                        {
                            //Timer Status löschen
                            StatusTimer = "none";
                            //feedtemp löschen
                            CheckSum = "";
                            //StartMS löschen
                            StartMS = 0;
                            //StackPanel verstecken
                            //StpConnect.Visibility = System.Windows.Visibility.Collapsed;
                            //Timer Stoppen
                            dt.Stop();
                            //Listbox erstellen
                            CreateDesignList();
                        }
                        //wenn feedtemp != feed, Quelltext noch nicht komplett geladen
                        else
                        {
                            //feedtemp zu aktuellem Feed machen
                            CheckSum = Source;
                        }
                    }
                }
                //Bei TimeOut
                else
                {
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_ConnectionTimeOut);
                    //Timer Status löschen
                    StatusTimer = "none";
                    //feedtemp löschen
                    CheckSum = "";
                    //StartMS löschen
                    StartMS = 0;
                    //Button umstellen
                    TxtConnect.Text = MyMusicPlayer.Resources.AppResources.ZZ002_ConnectXtrose;
                    //Timer Stoppen
                    dt.Stop();
                }
            }


            //Bilder downloaden
            if (StatusTimer == "DownloadImages")
            {
                //Datei Herunterladen wenn nicht bereits eine andere runtergeladen wird
                if (DownloadID != TempID & DownloadID < ListOnlineDesigns.Count() & DownloadID < (Designs_Area * 10))
                {
                    //TempID auf DownloadID stellen
                    TempID = DownloadID;
                    //url aus ausgwälten Bild laden
                    string url = "";
                    url = SourceUrl + ListOnlineDesigns[DownloadID].ID + ".jpg?time=" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                    //Datei öffnen
                    WebClient client = new WebClient();
                    client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
                    client.OpenReadAsync(new Uri(url));

                }

                //Wenn alle Dateien Heruntergeladen sind
                else if (DownloadID == ListOnlineDesigns.Count() | DownloadID == (Designs_Area * 10))
                {
                    //StackPanel verstecken
                    StpConnect.Visibility = System.Windows.Visibility.Collapsed;
                    //Bilder anzeigen
                    LBOnlineDesigns.ItemsSource = ListOnlineDesignImages; 

                    // Area Buttons erstellen
                    if (DownloadID < ListOnlineDesigns.Count())
                    {
                        ArrowNext.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        ArrowNext.Visibility = System.Windows.Visibility.Collapsed;
                    }

                    if (Designs_Area > 1)
                    {
                        ArrowBack.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        ArrowBack.Visibility = System.Windows.Visibility.Collapsed;
                    }

                    // Ausgabe erstellen
                    int Start_Text = (Designs_Area * 10) - 9;
                    int End_Text = DownloadID;
                    TBDesignsArea.Text = Start_Text.ToString() + " - " + End_Text.ToString() + " / " + ListOnlineDesigns.Count().ToString();

                    //Timer Status ändern
                    StatusTimer = "none";
                    //Timer stoppen
                    dt.Stop();
                }
            }


            //Bilder downloaden
            if (StatusTimer == "InstallDesign")
            {
                //Design Datei herunterladen
                if (InstallFile == 0)
                {
                    //url aus ausgwälten Bild laden
                    string url = "";
                    url = SourceUrl  + "Design_" + InstallID + ".txt";
                    //Datei öffnen
                    WebClient client = new WebClient();
                    client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_CreateInstallFiles);
                    client.OpenReadAsync(new Uri(url));

                }

                //Portrait herunterladen
                if (InstallFile == 1)
                {
                    //Wenn Portrait existiert
                    if (InstallPortrait == true)
                    {
                        //url aus ausgwälten Bild laden
                        string url = "";
                        url = SourceUrl + "Portrait_" + InstallID + ".jpg";

                        //Datei öffnen
                        WebClient client = new WebClient();
                        client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_CreateInstallFiles);
                        client.OpenReadAsync(new Uri(url));
                    }
                    //Wenn Portrait nicht existiert
                    else
                    {
                        InstallFile++;
                    }
                }

                //MP Portrait Herunterladen
                if (InstallFile == 2)
                {
                    //Wenn MP Portrait existiert
                    if (InstallMPPortrait == true)
                    {
                        //url aus ausgwälten Bild laden
                        string url = "";
                        url = SourceUrl + "MPBigPortrait_" + InstallID + ".jpg";

                        //Datei öffnen
                        WebClient client = new WebClient();
                        client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_CreateInstallFiles);
                        client.OpenReadAsync(new Uri(url));
                    }
                    //Wenn MP Portrait nicht existiert
                    else
                    {
                        InstallFile++;
                    }
                }

                //Landscape Herunterladen
                if (InstallFile == 3)
                {
                    //Wenn Landscape existiert
                    if (InstallLandscape == true)
                    {
                        //url aus ausgwälten Bild laden
                        string url = "";
                        url = SourceUrl + "Landscape_" + InstallID + ".jpg";

                        //Datei öffnen
                        WebClient client = new WebClient();
                        client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_CreateInstallFiles);
                        client.OpenReadAsync(new Uri(url));
                    }
                    //Wenn Landscape nicht existiert
                    else
                    {
                        InstallFile++;
                    }
                }

                //MP Landscape Herunterladen
                if (InstallFile == 4)
                {
                    //Wenn Landscape existiert
                    if (InstallMPLandscape == true)
                    {
                        //url aus ausgwälten Bild laden
                        string url = "";
                        url = SourceUrl + "MPBigLandscape_" + InstallID + ".jpg";

                        //Datei öffnen
                        WebClient client = new WebClient();
                        client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_CreateInstallFiles);
                        client.OpenReadAsync(new Uri(url));
                    }
                    //Wenn MP Landscape nicht existiert
                    else
                    {
                        InstallFile++;
                    }
                }

                //Installation abschließen
                if (InstallFile == 5)
                {
                    //Installation abschließen
                    CompleteInatallation();
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------





        //Designs Liste erstellen
        //---------------------------------------------------------------------------------------------------------
        //Variabeln
        int DownloadID = 0;
        int TempID = 999999999;

        //Aktion
        void CreateDesignList()
        {
            //Quelle zerlegen und in Liste schreiben
            string[] splitSource = Regex.Split(Source, ";;;");
            //Einzele Designs durchlaufen
            for (int i = 0; i < (splitSource.Count() - 1); i++)
            {
                //Design zerlegen
                string[] splitDesign = Regex.Split(splitSource[i], ";");
                
                //Variabeln erstellen
                int ID = -1;
                string Name = "";
                string Category = "";
                string Autor = "";
                bool Portrait = false;
                bool Landscape = false;
                bool MPPortrait = false;
                bool MPLandscape = false;

                //Design durchlaufen und eintragen
                for (int i2 = 0; i2 < splitDesign.Count(); i2++)
                {
                    //Optionen aufteilen und zuordnen
                    string[] splitOption = Regex.Split(splitDesign[i2], "=");
                    splitOption[0] = splitOption[0].Trim();

                    if (splitOption[0] == "ID")
                    {
                        ID = Convert.ToInt32(splitOption[1]);
                    }
                    if (splitOption[0] == "Name")
                    {
                        Name = splitOption[1];
                    }
                    if (splitOption[0] == "Category")
                    {
                        Category = splitOption[1];
                    }
                    if (splitOption[0] == "Autor")
                    {
                        Autor = splitOption[1];
                    }
                    if (splitOption[0] == "Portrait")
                    {
                        Portrait = Convert.ToBoolean(splitOption[1]);
                    }
                    if (splitOption[0] == "MPPortrait")
                    {
                        MPPortrait = Convert.ToBoolean(splitOption[1]);
                    }
                    if (splitOption[0] == "Landscape")
                    {
                        Landscape = Convert.ToBoolean(splitOption[1]);
                    }
                    if (splitOption[0] == "MPLandscape")
                    {
                        MPLandscape = Convert.ToBoolean(splitOption[1]);
                    }
                }

                //Eintrag der Liste hinzufügen
                ListOnlineDesigns.Add(new ClassOnlineDesigns(ID, Name, Category, Autor, Portrait, Landscape, MPPortrait, MPLandscape));

            }

            // Area festlegen
            Designs_Area = 1;

            //Liste erstellen
            StatusTimer = "DownloadImages";
            dt.Start();

            DownloadID = 0;
        }
        //---------------------------------------------------------------------------------------------------------





        //Bilder herunterladen
        //---------------------------------------------------------------------------------------------------------
        //Aktion
        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            //Alte Datei in Isolated Storage löschen
            if (file.FileExists("temp.jpg"))
            {
                file.DeleteFile("temp.jpg");
            }
            //Datei in Isolated Storage laden
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("temp.jpg", System.IO.FileMode.Create, file))
            {
                byte[] buffer = new byte[1024];
                while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }

            //Writeable Bitmap erstellen
            BitmapImage tempImage = new BitmapImage();
            byte[] data1;
            {
                using (IsolatedStorageFileStream isfs = file.OpenFile("temp.jpg", FileMode.Open, FileAccess.Read))
                {
                    data1 = new byte[isfs.Length];
                    isfs.Read(data1, 0, data1.Length);
                    isfs.Close();
                }
            }
            MemoryStream ms = new MemoryStream(data1);
            tempImage.SetSource(ms);

            //Bild in Liste schreiben
            ListOnlineDesignImages.Add(new ClassOnlineDesignImages(ListOnlineDesigns[DownloadID].ID, tempImage, ListOnlineDesigns[DownloadID].Name, ListOnlineDesigns[DownloadID].Autor, ListOnlineDesigns[DownloadID].Category));

            //DownloadID erhöhen
            DownloadID++;
        }
        //---------------------------------------------------------------------------------------------------------





        //Design installieren
        //---------------------------------------------------------------------------------------------------------        
        private void InstallDesign_Click(object sender, SelectionChangedEventArgs e)
        {
            if (LBOnlineDesigns.SelectedIndex != -1)
            {
                //Selected Item auswählen
                int ListIndex = LBOnlineDesigns.SelectedIndex;

                //Benachrichtigungs Abfrage ausgeben
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_InstallNotification, MyMusicPlayer.Resources.AppResources.ZZ002_Notification, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //ID in kompletten Designs suchen
                    for (int i = 0; i < ListOnlineDesigns.Count(); i++)
                    {
                        if (ListOnlineDesigns[i].ID == ListOnlineDesignImages[ListIndex].ID)
                        {
                            InstallPortrait = ListOnlineDesigns[i].Portrait;
                            InstallMPPortrait = ListOnlineDesigns[i].MPPortrait;
                            InstallLandscape = ListOnlineDesigns[i].Landscape;
                            InstallMPLandscape = ListOnlineDesigns[i].MPLandscape;
                            InstallID = ListOnlineDesigns[i].ID;
                            break;
                        }
                    }

                    //Variabeln erstellen
                    InstallName = ListOnlineDesignImages[ListIndex].Name;
                    InstallFile = 0;

                    //Installationsbildschim anzeigen
                    GRInstall.Visibility = System.Windows.Visibility.Visible;
                    MenuOpen = true;

                    //Installation beginnen
                    StatusTimer = "InstallDesign";
                    dt.Start();
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------





        //Designs herunterladen
        //---------------------------------------------------------------------------------------------------------
        void client_CreateInstallFiles(object sender, OpenReadCompletedEventArgs e)
        {
            //MP Portrait speichern
            if (InstallFile == 4)
            {
                if (file.FileExists("/TempDownloads/MPBigLandscape.jpg"))
                {
                    file.DeleteFile("/TempDownloads/MPBigLandscape.jpg");
                }
                //Datei in Isolated Storage laden
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("/TempDownloads/MPBigLandscape.jpg", System.IO.FileMode.Create, file))
                {
                    byte[] buffer = new byte[1024];
                    while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                //InstallFile erhöhen
                InstallFile++;
            }

            //Landscape speichern
            if (InstallFile == 3)
            {
                if (file.FileExists("/TempDownloads/Landscape.jpg"))
                {
                    file.DeleteFile("/TempDownloads/Landscape.jpg");
                }
                //Datei in Isolated Storage laden
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("/TempDownloads/Landscape.jpg", System.IO.FileMode.Create, file))
                {
                    byte[] buffer = new byte[1024];
                    while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                //InstallFile erhöhen
                InstallFile++;
            }

            //MP Portrait speichern
            if (InstallFile == 2)
            {
                if (file.FileExists("/TempDownloads/MPBigPortrait.jpg"))
                {
                    file.DeleteFile("/TempDownloads/MPBigPortrait.jpg");
                }
                //Datei in Isolated Storage laden
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("/TempDownloads/MPBigPortrait.jpg", System.IO.FileMode.Create, file))
                {
                    byte[] buffer = new byte[1024];
                    while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                //InstallFile erhöhen
                InstallFile++;
            }

            //Portrait speichern
            if (InstallFile == 1)
            {
                if (file.FileExists("/TempDownloads/Portrait.jpg"))
                {
                    file.DeleteFile("/TempDownloads/Portrait.jpg");
                }
                //Datei in Isolated Storage laden
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("/TempDownloads/Portrait.jpg", System.IO.FileMode.Create, file))
                {
                    byte[] buffer = new byte[1024];
                    while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                //InstallFile erhöhen
                InstallFile++;
            }

            //Design.dat speichern
            if (InstallFile == 0)
            {
                if (!file.DirectoryExists("/TempDownloads"))
                {
                    file.CreateDirectory("/TempDownloads");
                }
                if (file.FileExists("/TempDownloads/Design.dat"))
                {
                    file.DeleteFile("/TempDownloads/Design.dat");
                }
                //Datei in Isolated Storage laden
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("/TempDownloads/Design.dat", System.IO.FileMode.Create, file))
                {
                    byte[] buffer = new byte[1024];
                    while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                //InstallFile erhöhen
                InstallFile++;
            }    
        }
        //---------------------------------------------------------------------------------------------------------





        //Installation abschießen
        //---------------------------------------------------------------------------------------------------------
        void CompleteInatallation()
        {
            //Ordner erstellen
            int count = 0;
            string InstallDirectory = "";
            for (int i = 0; i < 1000; i++)
            {
                if (i == 0)
                {
                    if (!file.DirectoryExists("/Designs/" + InstallName))
                    {
                        InstallDirectory = "/Designs/" + InstallName;
                        break;
                    }
                    else
                    {
                        i++;

                    }
                }
                else
                {
                    if (!file.DirectoryExists("/Designs/" + InstallName + " " + i))
                    {
                        InstallDirectory = "/Designs/" + InstallName + " " + i;
                        break;
                    }
                    else
                    {
                        i++;

                    }
                }
            }
            file.CreateDirectory(InstallDirectory);

            //Daten kopieren
            file.CopyFile("/TempDownloads/Design.dat", InstallDirectory + "/Design.dat");
            if (InstallPortrait == true)
            {
                file.CopyFile("/TempDownloads/Portrait.jpg", InstallDirectory + "/Portrait.jpg");
            }
            if (InstallMPPortrait == true)
            {
                file.CopyFile("/TempDownloads/MPBigPortrait.jpg", InstallDirectory + "/MPBigPortrait.jpg");
            }
            if (InstallLandscape == true)
            {
                file.CopyFile("/TempDownloads/Landscape.jpg", InstallDirectory + "/Landscape.jpg");
            }
            if (InstallMPLandscape == true)
            {
                file.CopyFile("/TempDownloads/MPBigLandscape.jpg", InstallDirectory + "/MPBigLandscape.jpg");
            }

            //Designs laden
            LoadingDesigns();

            //Anzeige verbergen
            GRInstall.Visibility = System.Windows.Visibility.Collapsed;
            MenuOpen = false;
            dt.Stop();
            StatusTimer = "none";

            //Listbox abwählen
            try
            {
                LBOnlineDesigns.SelectedIndex = -1;
            }
            catch
            {
            }
        }




        // Buttons Area
        private void BtnDesignNext(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StatusTimer != "DownloadImages")
            {
                // Listbox leeren
                ListOnlineDesignImages.Clear();
                // Area erhöhen
                Designs_Area++;
                // DownloadID neu erstellen
                DownloadID = (Designs_Area * 10) - 10;
                TempID = 999999999;
                // Designs laden
                StatusTimer = "DownloadImages";
                dt.Start();
            }
        }

        private void BtnDesignBack(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StatusTimer != "DownloadImages")
            {
                // Listbox leeren
                ListOnlineDesignImages.Clear();
                // Area erhöhen
                Designs_Area--;
                // DownloadID neu erstellen
                DownloadID = (Designs_Area * 10) - 10;
                TempID = 999999999;
                // Designs laden
                StatusTimer = "DownloadImages";
                dt.Start();
            }
        }
        //---------------------------------------------------------------------------------------------------------
        #endregion





    }
}