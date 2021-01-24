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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.IO.IsolatedStorage;
using System.IO;





namespace MyMusicPlayer.Pages
{





    public partial class Language : PhoneApplicationPage
    {





        //Variabeln erstellen
        //---------------------------------------------------------------------------------------------------------
        //Neue Datenliste erstellen //ClassStyles
        ObservableCollection<ClassLanguages> datalist = new ObservableCollection<ClassLanguages>();
        
        // Index momentaner Sprache
        int SelectedLanguage = -1;
        //---------------------------------------------------------------------------------------------------------





        //Wird am Anfang der Seite geladen
        //---------------------------------------------------------------------------------------------------------
        public Language()
        {
            //Komponenten laden
            InitializeComponent();

            //Hintergrundfarbe prüfen
            Color backgroundColor = (Color)Application.Current.Resources["PhoneBackgroundColor"];
            string temp = Convert.ToString(backgroundColor);

            //Icons ändern
            if (temp != "#FF000000")
            {
                //Vordergrundfarbe ändern
                string ForegroundColor = "#FF000000";
                (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;

                //Icons ändern
                ImgTop.Source = new BitmapImage(new Uri("/Images/Globe.Light.png", UriKind.Relative));
                ImgTop.Opacity = 0.1;
            }
            else
            {
                //Vordergrundfarbe ändern
                string ForegroundColor = "#FFFFFFFF";
                (App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color = ConvertToSolidColorBrush(ForegroundColor, -1).Color;
            }

            //Sprachen erstellen
            CreateLanguages();
        }
        //---------------------------------------------------------------------------------------------------------





        //Wird am Anfang der Seite geladen
        //---------------------------------------------------------------------------------------------------------
        void CreateLanguages()
        {
            // Variablen laden
            string AT = MyMusicPlayer.Resources.AppResources.ZZ005_AutoTranslate;

            // Sprachen Klasse erstellen
            string[] LangCodes = { "az-Latn-AZ", "id-ID", "ms-MY", "ca-ES", "cs-CZ", "da-DK", "de-DE", "en-GB", "en", "es-ES", "es-MX", "fil-PH", "fr-FR", "hr-HR", "it-IT", "lt-LT", "lv-LV", "hu-HU", "nl-NL", "nb-NO", "pl-PL", "pt-BR", "pt-PT", "ro-RO", "sq-AL", "fi-FI", "sk-SK", "sv-SE", "vi-VN", "tr-TR", "el-GR", "be-BY", "bg-BG", "mk-MK", "ru-RU", "he-IL", "ar-SA", "fa-IR", "hi-IN", "th-TH", "ko-KR", "zh-CN", "zh-TW", "ja-JP", "sr-Latn-CS", "uk-UA" };
            string[] LangNames = { "Azərbaycan", "Bahasa Indonesia", "Behasa Melayu", "català", "Čeština", "dansk", "deutsch", "English (United States)", "English (international)", "español (España)", "Español (México)", "Filipino", "Français", "hrvatski", "italiano", "Lietuvių", "Latviešu", "magyar", "Nederlands", "norsk", "polski", "português (Brasil)", "português (Portugal)", "română", "Shqip", "suomi", "Slovenský", "Svenska", "Tiếng Việt", "Türkçe", "Ελληνικά", "Беларуска", "Български", "македонски", "русский", "עברית", "العربية", "فارسی", "हिंदी", "ไทย", "한국어", "简体中文", "繁體中文", "日本語", "српски", "Український" };
            string[] LangTranslations = { AT, AT, AT, AT, AT, AT, "Moses Rivera", "Moses Rivera", "Moses Rivera", AT, AT, AT, AT, AT, "Francesco Nunziata", AT, AT, "Schmuter Norbert", AT, AT, AT, AT, AT, "Schmuter Norbert", AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, AT, "WanderMax", "WanderMax", "WanderMax", AT, AT };

            //Prüfen wieviel Sprachen
            int cLang = LangNames.Count();

            //Sprachen durchlaufen
            datalist.Clear();
            for (int i = 0; i < cLang; i++)
            {
                datalist.Add(new ClassLanguages(LangNames[i], LangCodes[i], LangTranslations[i]));
                if (LangCodes[i] == MyMusicPlayer.Resources.AppResources.ResourceLanguage)
                {
                    SelectedLanguage = i;
                }
            }

            //Sprachen in Listbox Setzen
            LBLangList.ItemsSource = datalist;

            // Versuchen richtige Sprache anzuwählen
            SelectLang = false;
            try
            {
                LBLangList.SelectedIndex = SelectedLanguage;
            }
            catch { }
            SelectLang = true;
        }
        //---------------------------------------------------------------------------------------------------------





        //Neue Sprachdatei erstellen
        //---------------------------------------------------------------------------------------------------------
        //Variabeln
        bool SelectLang = true;
        //Aktion
        private void LocList_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            //Prüfen ob Aktion ausgefüfrt wird
            if (SelectLang == true)
            {
                //Index ermitteln
                int SI = LBLangList.SelectedIndex;

                //Code aus Array laden
                string cul = (datalist[SI] as ClassLanguages).code;
                string lang = (datalist[SI] as ClassLanguages).name;

                //Abfrage ob Sprache geändert werden soll
                if (MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z002_NoteLanguage + "\n" + lang, MyMusicPlayer.Resources.AppResources.Z001_Warning, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //Sprache ändern
                    CultureInfo newCulture = new CultureInfo(cul);
                    Thread.CurrentThread.CurrentUICulture = newCulture;

                    //IsoStore file erstellen
                    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                    //Prüfen ob alte Datei vorhanden
                    if (file.FileExists("Cul.dat"))
                    {
                        file.DeleteFile("Cul.dat");
                    }
                    //Neue Datei erstellen
                    IsolatedStorageFileStream filestream = file.CreateFile("Cul.dat");
                    StreamWriter sw = new StreamWriter(filestream);
                    sw.WriteLine(Convert.ToString(cul));
                    sw.Flush();
                    filestream.Close();

                    //Benachrichtigung ausgeben
                    MessageBox.Show(MyMusicPlayer.Resources.AppResources.Z002_NoteLanguage2);

                    //Zurück
                    NavigationService.GoBack();
                }
                // Wenn Sprache nicht verändert wird
                else
                {
                    // Versuchen richtige Sprache anzuwählen
                    SelectLang = false;
                    try
                    {
                        LBLangList.SelectedIndex = SelectedLanguage;
                    }
                    catch { }
                    SelectLang = true;
                }
            }
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