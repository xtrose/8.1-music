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
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Phone.Info;
using System.Text;





namespace MyMusicPlayer.Pages
{





    public partial class SystemSettings : PhoneApplicationPage
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
        //Einstellungs String
        string SettingsString;

        //Vollversion
        bool Fullversion = false;
        //Logo beim Start
        bool LogoStart = true;
        //Erweiterte Informationen
        string ExtendedInformation = "true";
        //Zeit des anspielens
        int PlayOnTime = 10;
        //Shuffle
        bool SetShuffle = false;
        //Repead
        bool SetRepead = false;
        //Schrift Größe
        string ImageSize = "26";

        //Farbeinstellungen
        string ForegroundColor;
        string BackgroundColor;
        string AppForegroundColor;
        string AppBackgroundColor;
        string AppAccentColor;

        // String für die Post Variablen
        Dictionary<string, string> post_parameters = new Dictionary<string, string>();
        //---------------------------------------------------------------------------------------------------------
        #endregion





        #region Wird beim ersten Start der Seite ausgeführt
        //Wird beim ersten Start der Seite ausgeführt
        //---------------------------------------------------------------------------------------------------------
        public SystemSettings()
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
                    }
                    //Wenn Farbe weiß ist
                    else
                    {
                        ForegroundColor = "#FFFFFFFF";
                        (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
                        ImgLogo.Source = new BitmapImage(new Uri("Images/Settings.Light.png", UriKind.Relative));
                        ImgLogo.Opacity = 0.1;
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
            //Settings laden
            filestream = file.OpenFile("Settings/Settings.dat", FileMode.Open);
            sr = new StreamReader(filestream);
            SettingsString = sr.ReadToEnd();
            filestream.Close();


            //SettingsString zerlegen
            string[] SplitSettingString = Regex.Split(SettingsString, ";");
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
                    TBPlayOnTimeDemo.Text = PlayOnTime.ToString();
                    SliderPlayOnTime.Value = PlayOnTime;
                }

                //FontSize
                if (SplitSetting[0] == "FontSize")
                {
                    ImageSize = SplitSetting[1].Trim();
                    TBFontSizeDemo.Text = ImageSize;
                    TBFontSizeDemo.FontSize = Convert.ToInt32(ImageSize);
                    SliderFontSize.Value = Convert.ToInt32(ImageSize);
                }

                //LogoStart
                if (SplitSetting[0] == "LogoStart")
                {
                    LogoStart = Convert.ToBoolean(SplitSetting[1].Trim());
                    if (LogoStart == true)
                    {
                        BtnLogoStart.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                    }
                    else
                    {
                        BtnLogoStart.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                    }
                }

                //Erweiterte Informationen
                if (SplitSetting[0] == "ExtendedInformation")
                {
                    bool tExtendedInformation = Convert.ToBoolean(SplitSetting[1]);
                    if (tExtendedInformation == true)
                    {
                        ExtendedInformation = "true";
                        BtnExtendedInfo.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
                    }
                    else
                    {
                        ExtendedInformation = "false";
                        BtnExtendedInfo.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
                    }
                }

                //Shuffle
                if (SplitSetting[0] == "Shuffle")
                {
                    //Shuffle umwandeln
                    SetShuffle = Convert.ToBoolean(SplitSetting[1]);
                }

                //Repead
                if (SplitSetting[0] == "Repead")
                {
                    //Repead umwandeln
                    SetRepead = Convert.ToBoolean(SplitSetting[1]);
                }
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
            SettingsString = "Fullversion=" + Fullversion.ToString() + ";LogoStart=" + LogoStart.ToString() + ";ExtendedInformation=" + ExtendedInformation + ";Repead=" + SetRepead.ToString() + ";Shuffle=" + SetShuffle.ToString() + ";PlayOnTime=" + PlayOnTime.ToString() + ";FontSize=" + ImageSize.ToString() + ";";

            //Einstellungen in Settings/Settings schreiben
            filestream = file.CreateFile("Settings/Settings.dat");
            sw = new StreamWriter(filestream);
            sw.Write(SettingsString);
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



        //PlayOnTime, Slider
        private void SliderPlayOnTime_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            //Slider Daten umwandeln
            int TempPlayOnTime = Convert.ToInt32(SliderPlayOnTime.Value);
            PlayOnTime = Convert.ToInt32(TempPlayOnTime);
            TBPlayOnTimeDemo.Text = PlayOnTime.ToString();
            //Einstellungen speichern
            CreateSettings();
            //Timer Stoppen
            Timer_Settings_Action = "none";
            Timer_Settings.Stop();
        }
        private void SliderPlayOnTime_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            //Timer Starten
            Timer_Settings_Action = "PlayOnTime";
            Timer_Settings.Start();
        }



        //Font Size, Slider
        private void SliderFontSize_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            //Slider Daten umwandeln
            int TempImageSize = Convert.ToInt32(SliderFontSize.Value);
            ImageSize = TempImageSize.ToString();
            TBFontSizeDemo.Text = ImageSize;
            TBFontSizeDemo.FontSize = TempImageSize;
            //Einstellungen speichern
            CreateSettings();
            //Timer anweisen das Liste upgedatet wird
            Timer_Settings_Action = "UpdateList";
            Timer_Settings.Start();
        }
        private void SliderFontSize_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            //Timer Starten
            Timer_Settings_Action = "FontSize";
            Timer_Settings.Start();
        }



        //Logo am Start anzeigen
        private void BtnLogoStart_Click(object sender, RoutedEventArgs e)
        {
            //Logo umstellen
            if (LogoStart == false)
            {
                LogoStart = true;
                BtnLogoStart.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
            }
            else
            {
                LogoStart = false;
                BtnLogoStart.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
            }
            //Einstellungen speichern
            CreateSettings();
        }



        //Erweiterte Informationen
        private void BtnExtendedInfo_Click(object sender, RoutedEventArgs e)
        {
            //Erweiterte Informationen umstellen
            if (ExtendedInformation == "true")
            {
                ExtendedInformation = "false";
                BtnExtendedInfo.Content = MyMusicPlayer.Resources.AppResources.Z001_Off;
            }
            else
            {
                ExtendedInformation = "true";
                BtnExtendedInfo.Content = MyMusicPlayer.Resources.AppResources.Z001_On;
            }
            //Einstellungen speichern
            CreateSettings();
            //Timer anweisen das Liste upgedatet wird
            Timer_Settings_Action = "UpdateList";
            Timer_Settings.Start();
        }


        //Einstellungen zurücksetzen
        private void BtnResetSettings_Click(object sender, RoutedEventArgs e)
        {
            //Nach Abfrage, Einstellungen zurücksetzen
            if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_RestoreSettings, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //Einstellungen zurücksetzen
                ImageSize = "28";

                PlayOnTime = 7;
                TBPlayOnTimeDemo.Text = PlayOnTime.ToString();
                SliderPlayOnTime.Value = PlayOnTime;

                ExtendedInformation = "true";
                BtnExtendedInfo.Content = MyMusicPlayer.Resources.AppResources.Z001_On;

                LogoStart = true;
                BtnLogoStart.Content = MyMusicPlayer.Resources.AppResources.Z001_On;

                SetShuffle = false;

                SetRepead = false;

                //Einstellungen neu erstellen
                CreateSettings();
            }
        }


        //Variabeln, auf Update prüfen
        string SourceWebsite = "http://www.xtrose.com";
        string Source = "";
        string CheckSum = "";
        DateTime CheckUpdateStart = DateTime.MinValue;
        //Button, auf Update Prüfen
        private void BtnCheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Post Variablen erstellen
            post_parameters = new Dictionary<string, string>();
            post_parameters.Add("api", "1");
            post_parameters.Add("id", "13");
            post_parameters.Add("s", "d34hYXC5ROvQqBWQbsmI");
            //Zeit zurücksetzen
            CheckUpdateStart = DateTime.MinValue;
            //Timer starten
            Timer_Settings.Start();
            //Timer Status angeben
            Timer_Settings_Action = "LoadWebsite";
            GRUpdateList.Visibility = System.Windows.Visibility.Visible;
            TBUpdateList.Text = MyMusicPlayer.Resources.AppResources.Z001_Connecting;
            //Seite versuchen zu erreichen
            GetSourceCode();
        }
        //Quelle Verarbeiten, auf Update prüfen
        void CheckIfFullVersion()
        {
            //Variabeln
            bool UpdateAvailable = false;
            //Wenn Quelle verfügbar und keine Vollversion
            if (Source != "" & Fullversion == false)
            {
                //Anonymous ID ermitteln
                string MyUserID = UserExtendedProperties.GetValue("ANID2") as string;
                //Quelle zerlegen
                string[] SourceSplit = Regex.Split(Source, ";;;");
                //IDs durchlaufen und Prüfen
                for (int i = 0; i < SourceSplit.Count(); i++)
                {
                    //Wenn ID vorhanden
                    if (SourceSplit[i].Trim() == MyUserID)
                    {
                        //Vollversion erstellen
                        Fullversion = true;
                        UpdateAvailable = true;
                        CreateSettings();
                        //Benachrichtigung ausgeben
                        MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoteUpdateFullVersion);
                        break;
                    }
                }
            }
            //Wenn kein Update verfügbar
            if (UpdateAvailable == false)
            {
                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_NoUpdateAvailable);
            }
            //Timer zurückstellen
            CheckSum = "";
            GRUpdateList.Visibility = System.Windows.Visibility.Collapsed;
            TBUpdateList.Text = MyMusicPlayer.Resources.AppResources.Z001_OneMomentPlease;
            Timer_Settings_Action = "none";
            Timer_Settings.Stop();
        }



        //Timer Einstellungen
        void Timer_Settings_Tick(object sender, object e)
        {
            //Auf Vollversion prüfen
            if (Timer_Settings_Action == "LoadWebsite")
            {
                //Prüfen ob Connection Time Out
                bool ConnectionTimeOut = false;
                if (CheckUpdateStart == DateTime.MinValue)
                {
                    CheckUpdateStart = DateTime.Now;
                }
                else
                {
                    if (CheckUpdateStart.AddSeconds(10) < DateTime.Now)
                    {
                        ConnectionTimeOut = true;
                    }
                }
                //Preüfen ob Time out
                if (ConnectionTimeOut == false)
                {
                    //Prüfen ob Quelle geladen
                    if (Source != "")
                    {
                        //wenn feedtemp = feed, Quelltext komplett geladen
                        if (Source == CheckSum)
                        {
                            //Timer Status löschen
                            Timer_Settings_Action = "none";
                            //feedtemp löschen
                            CheckSum = "";
                            //Timer Stoppen
                            Timer_Settings.Stop();
                            //Listbox erstellen
                            CheckIfFullVersion();
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
                    //TimeOut Benachrichtigung ausgeben
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_ConnectionTimeOut);
                    //feedtemp löschen
                    CheckSum = "";
                    GRUpdateList.Visibility = System.Windows.Visibility.Collapsed;
                    TBUpdateList.Text = MyMusicPlayer.Resources.AppResources.Z001_OneMomentPlease;
                    Timer_Settings_Action = "none";
                    Timer_Settings.Stop();
                }
            }


            //Bei PlayOnTime
            if (Timer_Settings_Action == "PlayOnTime")
            {
                //Slider bewegen und PlayOnTimeDemo umstellen
                int TempPlayOnTime = Convert.ToInt32(SliderPlayOnTime.Value);
                PlayOnTime = Convert.ToInt32(TempPlayOnTime);
                TBPlayOnTimeDemo.Text = PlayOnTime.ToString();
                SliderPlayOnTime.Value = PlayOnTime;
            }


            //Bei FontSize
            if (Timer_Settings_Action == "FontSize")
            {
                //Slider bewegen und Größe umstellen
                int TempImageSize = Convert.ToInt32(SliderFontSize.Value);
                ImageSize = TempImageSize.ToString();
                TBFontSizeDemo.Text = ImageSize;
                TBFontSizeDemo.FontSize = TempImageSize;
                SliderFontSize.Value = Convert.ToInt32(SliderFontSize.Value);
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
                MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z001_ConnectionFailed);
                //Timer und Grid zurüchsetzen
                GRUpdateList.Visibility = System.Windows.Visibility.Collapsed;
                TBUpdateList.Text = MyMusicPlayer.Resources.AppResources.Z001_OneMomentPlease;
                Timer_Settings_Action = "none";
                Timer_Settings.Stop();
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
        //---------------------------------------------------------------------------------------------------------------------------------
        # endregion, Funktionen 





    }
}