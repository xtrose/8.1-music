using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
//Zum laden von Qellcodes und zum erstellen und auslesem von Dateien
using System.IO;
//Zum erstellen und auslesem von Dateien
using System.IO.IsolatedStorage;
//Zum erweiterten schneiden von strings
using System.Text.RegularExpressions;
//Zum speichern von Bildern in den Isolated Storage
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone.Tasks;
using Microsoft.Phone;
using System.Windows.Media;
//Um Battery Leistung auszulesen
using Windows.Phone.Devices.Power;
//Um auszulesen ob Handy geladen wird;
using Microsoft.Phone.Info;
//Für Listbox Update
using System.Collections.ObjectModel;
//Background Agent
using Microsoft.Phone.Scheduler;
//Für Benachrichtigung vor beenden
using System.ComponentModel;





namespace MyMusicPlayer.Pages
{





    public partial class CopyDesign : PhoneApplicationPage
    {





        #region Allgemeine Variabeln
        //Allgemeine Variabeln
        //-----------------------------------------------------------------------------------------------------------------
        //IsoStore file erstellen
        IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
        IsolatedStorageFileStream filestream;
        StreamReader sr;

        //Desing Daten
        int DesignId = -1;
        string DesignName;

        //DesignString
        string DesignString;

        //Farbeinstellungen
        string ForegroundColor;
        string BackgroundColor;
        string AppForegroundColor;
        string AppBackgroundColor;
        string AppAccentColor;
        //-----------------------------------------------------------------------------------------------------------------
        #endregion





        #region Wird zum Strat der Seite geladen
        // Wird zum Start der Seite geladen
        //-----------------------------------------------------------------------------------------------------------------
        public CopyDesign()
        {
            //Komponenten laden
            InitializeComponent();

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
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        ForegroundColor = "#FFFFFFFF";
                        (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                        ImgTop.Source = new BitmapImage(new Uri("Images/Edit.Light.png", UriKind.Relative));
                        ImgTop.Opacity = 0.1;
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
        //-----------------------------------------------------------------------------------------------------------------
        #endregion





        #region Wird bei jedem Aufruf der Seite geladen
        // Wird bei jedem Aufruf der Seite geladen
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Variable für Ordner ermitteln
            DesignId = Convert.ToInt32(NavigationContext.QueryString["design"]);
            base.OnNavigatedTo(e);

            //Alle Design Ordner laden
            string[] AllDesigns = file.GetDirectoryNames("/Designs/*");
            DesignName = AllDesigns[DesignId];

            //Style Name
            TBDesignName.Text = DesignName;
        }
        //-----------------------------------------------------------------------------------------------------------------
        #endregion





        #region Prüfen ob Return gedrückt wurde
        //Prüfen ob Return gedrückt wurde
        //-----------------------------------------------------------------------------------------------------------------
        private void TBFolderName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ////Wenn Return gedrückt wurde
            string tempkey = Convert.ToString(e.Key);
            if (tempkey == "Enter")
            {

                //";" Zeichen herauslöschen
                TBDesignName.Text = Regex.Replace(TBDesignName.Text, ";", "");
                TBDesignName.Text = TBDesignName.Text.Trim();

                //Wenn Zeichen vorhanden
                if (TBDesignName.Text.Length > 0)
                {

                    //Wenn neuer Name gleich wie alter name
                    if (DesignName == TBDesignName.Text)
                    {
                        //Zurück
                        NavigationService.GoBack();
                    }

                    //Wenn neuer Name eingegeben wurde
                    else
                    {
                        //Prüfen ob Ordner bereits besteht
                        if (!file.DirectoryExists("/Designs/" + TBDesignName.Text))
                        {
                            //Versuchen Design umzubenennen
                            try
                            {
                                //Neuen Ordner erstellen
                                file.CreateDirectory("/Designs/" + TBDesignName.Text);
                                //Dateien zu neuem Ordner kopieren und löschen
                                file.CopyFile("/Designs/" + DesignName + "/Design.dat", "/Designs/" + TBDesignName.Text + "/Design.dat");
                                if (file.FileExists("/Designs/" + DesignName + "/Portrait.jpg"))
                                {
                                    file.CopyFile("/Designs/" + DesignName + "/Portrait.jpg", "/Designs/" + TBDesignName.Text + "/Portrait.jpg");
                                }
                                if (file.FileExists("/Designs/" + DesignName + "/Landscape.jpg"))
                                {
                                    file.CopyFile("/Designs/" + DesignName + "/Landscape.jpg", "/Designs/" + TBDesignName.Text + "/Landscape.jpg");
                                }
                                if (file.FileExists("/Designs/" + DesignName + "/MPBigPortrait.jpg"))
                                {
                                    file.CopyFile("/Designs/" + DesignName + "/MPBigPortrait.jpg", "/Designs/" + TBDesignName.Text + "/MPBigPortrait.jpg");
                                }
                                if (file.FileExists("/Designs/" + DesignName + "/MPBigLandscape.jpg"))
                                {
                                    file.CopyFile("/Designs/" + DesignName + "/MPBigLandscape.jpg", "/Designs/" + TBDesignName.Text + "/MPBigLandscape.jpg");
                                }

                                //Navigation zurück
                                NavigationService.GoBack();
                            }
                            catch
                            {
                                MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ErrorName);
                                TBDesignName.Text = DesignName;
                            }
                        }
                        //Wenn Ordner bereits besteht
                        else
                        {
                            MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ErrorName);
                            TBDesignName.Text = DesignName;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.ZZ002_ErrorName);
                    TBDesignName.Text = DesignName;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------  
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
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion, Funktionen




    }
}