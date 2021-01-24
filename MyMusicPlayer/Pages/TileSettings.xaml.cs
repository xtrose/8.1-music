using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using ImageTools;





namespace MyMusicPlayer.Pages
{





    public partial class TileSettings : PhoneApplicationPage
    {





        #region Allgemeine Variabeln
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

        //DesignString
        string DesignString;
        string TileString;
        
        //Farbeinstellungen
        string ForegroundColor;
        string BackgroundColor;
        string AppForegroundColor;
        string AppBackgroundColor;
        string AppAccentColor;

        //Tile Einstellungen
        string TileBackgroundColor;
        string SecondTileBackgroundColor;
        bool LogoImage = false;

        //Angeben das Menü offen ist
        bool MenuOpen = false;

        //Prüfen ob Fliptile schon erstellt wurde
        ShellTile oTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("flip".ToString()));
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Wird beim ersten Start der Seite ausgeführt
        //Wird beim ersten Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------
        public TileSettings()
        {
            //Komponenten laden
            InitializeComponent();

            //Timer Settings einstellen
            Timer_Settings.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer_Settings.Tick += Timer_Settings_Tick;
            Timer_Settings.Stop();

            //Farben auf Standard einstellen
            ForegroundColor = (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color.ToString();
            BackgroundColor = (App.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush).Color.ToString();
            AppForegroundColor = (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color.ToString();
            AppBackgroundColor = (App.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush).Color.ToString();
            AppAccentColor = ((Color)Application.Current.Resources["PhoneAccentColor"]).ToString();

            //Design laden
            filestream = file.OpenFile("Settings/Design.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            DesignString = sr.ReadToEnd();
            filestream.Close();

            //Design zerlegen und Daten laden
            string[] DesignOptions = Regex.Split(DesignString, ";");
            //Design duchlaufen und Optionen Heraus fischen
            for (int i = 0; i < DesignOptions.Count(); i++)
            {
                //Design Options zerlegen
                string[] SplitSetting = Regex.Split(DesignOptions[i], "=");

                //Farbe App Hintergrund
                if (SplitSetting[0] == "BackgroundColor")
                {
                    //Hintergrundfarbe erstellen
                    BackgroundColor = CreateColorFromCode(SplitSetting[1]);
                    //Hintergrundfarbe anwenden
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
                        ImgDeleteBackground.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                        ImgDeleteBackground2.Source = new BitmapImage(new Uri("/Images/Delete.Light.png", UriKind.Relative));
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        ForegroundColor = "#FFFFFFFF";
                        (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                        ImgLogo.Source = new BitmapImage(new Uri("/Images/Settings.Dark.png", UriKind.Relative));
                        ImgLogo.Opacity = 0.1;
                        ImgDeleteBackground.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                        ImgDeleteBackground2.Source = new BitmapImage(new Uri("/Images/Delete.Dark.png", UriKind.Relative));
                    }
                }
            }

            //Wenn Hintergundbild Portrait vorhanden
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
                MemoryStream ms = new MemoryStream(data1);
                BitmapImage bi = new BitmapImage();
                bi.SetSource(ms);
                var imageBrush = new ImageBrush();
                imageBrush.ImageSource = bi;
                LayoutRoot.Background = imageBrush;
            }
        }
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Wird bei jedem Start der Seite ausgeführt
        //Wird bei jedem Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------------------------------
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
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
                    if (SplitSetting[1] == "*" | SplitSetting[1] == "")
                    {
                        BtnBackgroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
                    }
                    else
                    {
                        TileBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                        BtnBackgroundColor.Content = CreateColorButtonContent(TileBackgroundColor);
                        RTBackgroundColorDemo.Fill = ConvertToSolidColorBrush(TileBackgroundColor, -1);
                    }
                }

                //SecondTile Background Color
                if (SplitSetting[0] == "SecondBackgroundColor" | SplitSetting[1] == "")
                {
                    if (SplitSetting[1] == "*" | SplitSetting[1] == "")
                    {
                        BtnSecondBackgroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
                    }
                    else
                    {
                        SecondTileBackgroundColor = CreateColorFromCode(SplitSetting[1]);
                        BtnSecondBackgroundColor.Content = CreateColorButtonContent(SecondTileBackgroundColor);
                        RTSecondBackgroundColorDemo.Fill = ConvertToSolidColorBrush(SecondTileBackgroundColor, -1);
                    }
                }

                //Logo on Image Background Color
                if (SplitSetting[0] == "LogoImage")
                {
                    LogoImage = Convert.ToBoolean(SplitSetting[1]);
                    if (LogoImage == true)
                    {
                        BtnLogoAlbumImage.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                    }
                    else
                    {
                        BtnLogoAlbumImage.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                    }
                }
            }

            //Wenn zweites Tile besteht
            if (oTile != null && oTile.NavigationUri.ToString().Contains("flip"))
            {
                TBClicktoCreate.Visibility = System.Windows.Visibility.Collapsed;
            }
            //Wenn zweites Tile nicht besteht
            else
            {
                TBClicktoCreate.Visibility = System.Windows.Visibility.Visible;
            }

            //Tiles erstellen
            CreateTiles();
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        #endregion





        #region Farb Einstellungen neu erstellen
        //Farb Einstellungen neu erstellen
        //---------------------------------------------------------------------------------------------------------
        void CreateColorSettings()
        {
            //Erstes Tile Hintergrundfarbe
            string tTileBackgroundColor = "*";
            if (TileBackgroundColor != "*" & TileBackgroundColor != "")
            {
                tTileBackgroundColor = CreateSettingsColor(TileBackgroundColor);
            }

            //Zweites Tile Hintergrundfarbe
            string tSecondTileBackgroundColor = "*";
            if (SecondTileBackgroundColor != "*" & SecondTileBackgroundColor != "")
            {
                tSecondTileBackgroundColor = CreateSettingsColor(SecondTileBackgroundColor);
            }
            
            //TileSTring erstellen
            TileString = "BackgroundColor=" + tTileBackgroundColor + ";LogoImage=" + LogoImage.ToString() +";SecondBackgroundColor=" + tSecondTileBackgroundColor + ";";

            //Tile Einstellungen erstellen
            filestream = file.CreateFile("/Tiles/TileSettings.dat");
            sw = new StreamWriter(filestream);
            sw.Write(TileString);
            sw.Flush();
            filestream.Close();
        }
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Tiles erstellen
        //Tiles erstellem
        //---------------------------------------------------------------------------------------------------------
        void CreateTiles()
        {
            //Erstes Tile erstellen
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
            
            //Bild ausgeben
            ImgFirstTile.Source = wbmp;




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




            //Zweites Tile erstellen
            WriteableBitmap SecondTile = new WriteableBitmap(336, 336);
            WriteableBitmap SecondTileIcon = new WriteableBitmap(336, 336);
            //Hintergrundfarbe einfügen
            if (SecondTileBackgroundColor != "*" & SecondTileBackgroundColor != null)
            {
                SecondTile.Clear(ConvertToSolidColorBrush(SecondTileBackgroundColor, -1).Color);
            }

            //Tile Bild laden
            using (Stream input = Application.GetResourceStream(new Uri("SecondTileMedium.png", UriKind.Relative)).Stream)
            {
                SecondTileIcon.SetSource(input);
            }
            //Bild zusammensetzen
            SecondTile.Blit(new Rect(0, 0, 336, 336), SecondTileIcon, new Rect(0, 0, 336, 336));

            // Bild erstellen
            Grid gridSecond = new Grid
            {
                Width = 336,
                Height = 336
            };
            Image imgSecond = new Image();
            imgSecond.Source = SecondTile;
            gridSecond.Children.Add(imgSecond);
            // Writeable Bitmap aus Grid erstellen
            WriteableBitmap wbmpSecond = new WriteableBitmap(gridSecond, null);
            // Extended Image aus Writeable Bitmap erstellen
            ExtendedImage extendImageSecond = wbmpSecond.ToImage();

            // Bild speichern
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists("Shared/ShellContent/Second336.png"))
                {
                    store.DeleteFile("Shared/ShellContent/Second336.png");
                }
                using (var stream = store.OpenFile("Shared/ShellContent/Second336.png", System.IO.FileMode.OpenOrCreate))
                {
                    extendImageSecond.WriteToStream(stream, "Shared/ShellContent/Second336.png");
                }
            }
            // Bild ausgeben
            ImgSecondTile.Source = wbmpSecond;



            //Zweites Tile erstellen
            WriteableBitmap SecondTileWidth = new WriteableBitmap(691, 336);
            WriteableBitmap SecondTileIconWidth = new WriteableBitmap(691, 336);
            //Hintergrundfarbe einfügen
            if (SecondTileBackgroundColor != "*" & SecondTileBackgroundColor != null)
            {
                SecondTileWidth.Clear(ConvertToSolidColorBrush(SecondTileBackgroundColor, -1).Color);
            }

            //Tile Bild laden
            using (Stream input = Application.GetResourceStream(new Uri("SecondTileLarge.png", UriKind.Relative)).Stream)
            {
                SecondTileIconWidth.SetSource(input);
            }
            //Bild zusammensetzen
            SecondTileWidth.Blit(new Rect(0, 0, 691, 336), SecondTileIconWidth, new Rect(0, 0, 691, 336));
            // Second Tile groß erstellen
            Grid gridSecondWidth = new Grid
            {
                Width = 691,
                Height = 336
            };
            Image imgSecondWidth = new Image();
            imgSecondWidth.Source = SecondTileWidth;
            gridSecondWidth.Children.Add(imgSecondWidth);
            // Writeable Bitmap aus Grid erstellen
            WriteableBitmap wbmpSecondWidth = new WriteableBitmap(gridSecondWidth, null);
            // Extended Image aus Writeable Bitmap erstellen
            ExtendedImage extendImageSecondWidth = wbmp.ToImage();

            // Bild speichern
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists("Shared/ShellContent/Second691.png"))
                {
                    store.DeleteFile("Shared/ShellContent/Second691.png");
                }
                using (var stream = store.OpenFile("Shared/ShellContent/Second691.png", System.IO.FileMode.OpenOrCreate))
                {
                    extendImageSecondWidth.WriteToStream(stream, "Shared/ShellContent/Second691.png");
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


            //Second Tile neu erstellen
            if (oTile != null && oTile.NavigationUri.ToString().Contains("flip"))
            {
                FlipTileData oFliptile = new FlipTileData();
                oFliptile.Title = Title = "";
                oFliptile.SmallBackgroundImage = new Uri("isostore:/Shared/ShellContent/Second336.png", UriKind.Absolute);
                oFliptile.BackgroundImage = new Uri("isostore:/Shared/ShellContent/Second336.png", UriKind.Absolute);
                oFliptile.WideBackgroundImage = new Uri("isostore:/Shared/ShellContent/Second691.png", UriKind.Absolute);
                oTile.Update(oFliptile);
            }
        }
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Einstellungen
        //Einstellungen
        //---------------------------------------------------------------------------------------------------------
        //Variabeln
        string Timer_Settings_Action = "none";
        string ColorPicker_Action = "none";



        //Tile Hintergrundfarbe
        private void BtnBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            if (TileBackgroundColor != "*" & TileBackgroundColor != null & TileBackgroundColor != "")
            {
                //Farbe in Colorpicker einstellen
                SolidColorBrush sb = ConvertToSolidColorBrush(TileBackgroundColor, 255);
                CP.Color = sb.Color;
                CPColorGrid.Background = sb;
                //Alpha Wert erstellen
                byte byteA = Convert.ToByte(TileBackgroundColor.Substring(1, 2), 16);
                CPSlider.Value = Convert.ToInt32(byteA);
                //Farbe in Color Schreiben
                CPColor.Text = TileBackgroundColor;
            }
            //Color picker vorbereiten
            ColorPicker_Action = "TileBackgroundColor";
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }

        //Logo über Tile anzeigen
        private void BtnLogoImage_Click(object sender, RoutedEventArgs e)
        {
            //Wenn Logo Image aus
            if (LogoImage == false)
            {
                LogoImage = true;
                BtnLogoAlbumImage.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
            }
            else
            {
                LogoImage = false;
                BtnLogoAlbumImage.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
            }
            //Einstellungen speichern
            CreateColorSettings();
        }

        //Tile zurücksetzen
        private void BtnResetTile_Click(object sender, RoutedEventArgs e)
        {
            //Farbe zurücksetzen
            TileBackgroundColor = ((Color)Application.Current.Resources["PhoneAccentColor"]).ToString();
            RTBackgroundColorDemo.Fill = ConvertToSolidColorBrush(TileBackgroundColor, -1);
            //Einstellungen neu erstellen
            CreateTiles();
            //Einstellungen speichern
            CreateColorSettings();
        }

        //Second Tile Hintergrundfarbe
        private void BtnSecondBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            if (SecondTileBackgroundColor != "*" & SecondTileBackgroundColor != null & SecondTileBackgroundColor != "")
            {
                //Farbe in Colorpicker einstellen
                SolidColorBrush sb = ConvertToSolidColorBrush(SecondTileBackgroundColor, 255);
                CP.Color = sb.Color;
                CPColorGrid.Background = sb;
                //Alpha Wert erstellen
                byte byteA = Convert.ToByte(SecondTileBackgroundColor.Substring(1, 2), 16);
                CPSlider.Value = Convert.ToInt32(byteA);
                //Farbe in Color Schreiben
                CPColor.Text = SecondTileBackgroundColor;
            }
            //Color picker vorbereiten
            ColorPicker_Action = "SecondTileBackgroundColor";
            //Color picker sichtbar machen
            GRColor.Visibility = System.Windows.Visibility.Visible;
            MenuOpen = true;
        }

        //Second Tile zurücksetzen
        private void BtnSecondResetTile_Click(object sender, RoutedEventArgs e)
        {
            //Farbe zurücksetzen
            SecondTileBackgroundColor = ((Color)Application.Current.Resources["PhoneAccentColor"]).ToString();
            RTSecondBackgroundColorDemo.Fill = ConvertToSolidColorBrush(SecondTileBackgroundColor, -1);
            //Einstellungen neu erstellen
            CreateTiles();
            //Einstellungen speichern
            CreateColorSettings();
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
            //Farbe auf Eingestellte Farbe stellen
            if (ColorPicker_Action == "TileBackgroundColor")
            {
                TileBackgroundColor = CPColor.Text;
                BtnBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                RTBackgroundColorDemo.Fill = ConvertToSolidColorBrush(TileBackgroundColor, -1);
            }
            if (ColorPicker_Action == "SecondTileBackgroundColor")
            {
                SecondTileBackgroundColor = CPColor.Text;
                BtnSecondBackgroundColor.Content = CreateColorButtonContent(CPColor.Text);
                RTSecondBackgroundColorDemo.Fill = ConvertToSolidColorBrush(SecondTileBackgroundColor, -1);
            }

            //Einstellungen speichern
            CreateColorSettings();
            //Einstellungen neu erstellen
            CreateTiles();
            //Color Picker schließen
            GRColor.Visibility = System.Windows.Visibility.Collapsed;
            ColorPicker_Action = "none";
            //Angeben das Menüs geschlossen sind
            MenuOpen = false;
        }



        private void ClearFirstTileColor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Button auf no stellen
            BtnBackgroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
            // Farbe löschen
            TileBackgroundColor = "*";
            //Einstellungen speichern
            CreateColorSettings();
            //Einstellungen neu erstellen
            CreateTiles();
        }



        private void ClearSecondTileColor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Button auf no stellen
            BtnSecondBackgroundColor.Content = MyMusicPlayer.Resources.AppResources.Z001_No;
            // Farbe löschen
            SecondTileBackgroundColor = "*";
            //Einstellungen speichern
            CreateColorSettings();
            //Einstellungen neu erstellen
            CreateTiles();
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
                    //Angeben das Menüs geschlossen sind
                    MenuOpen = false;

                    //Zurück oder beenden abbrechen
                    e.Cancel = true;
                }
        }
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion





        #region Tile erstellen
        //Flip Tile erstellen oder updaten
        //-----------------------------------------------------------------------------------------------------------------
        //Wenn Flip Tile bereits erstellt //Flip Tile updaten
        void BtnCreateTile_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (oTile != null && oTile.NavigationUri.ToString().Contains("flip"))
            {
                CreateTiles();
            }
            //Wenn Flip Tile noch nicht erstellt wurde //Flip Tile erstellen
            else
            {
                //Tiles erstellen
                CreateTiles();
                // once it is created flip tile
                Uri tileUri = new Uri("/MainPage.xaml?tile=flip", UriKind.Relative);
                ShellTileData tileData = this.CreateFlipTileData();
                ShellTile.Create(tileUri, tileData, true);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------





        // Flip Tile erstellen //Wenn noch nicht erstellt wurde
        //-----------------------------------------------------------------------------------------------------------------
        private ShellTileData CreateFlipTileData()
        {
            return new FlipTileData()
            {
                Title = "",
                SmallBackgroundImage = new Uri("isostore:/Shared/ShellContent/Second336.png", UriKind.Absolute),
                BackgroundImage = new Uri("isostore:/Shared/ShellContent/Second336.png", UriKind.Absolute),
            };
        }
        //-----------------------------------------------------------------------------------------------------------------
        #endregion





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
    }
}