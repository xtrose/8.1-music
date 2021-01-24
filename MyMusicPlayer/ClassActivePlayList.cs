using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;





// Namespace
namespace MyMusicPlayer
{





    // Klasse der Aktiven Abspielliste
    class ClassActivePlayList
    {
        // Variabeln
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Song { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string ExtendedInfoText { get; set; }
        public string ExtendedInfoFontSize { get; set; }
        public string ImageSource { get; set; }
        public string ImageSource2 { get; set; }
        public string ImageSize { get; set; }
        public string ImageSize2 { get; set; }
        public string ImageSource3 { get; set; }
        public string ImageSize3 { get; set; }



        // Zur Liste hinzufügen
        public ClassActivePlayList (string Artist, string Album, string Song, string Genre, string Duration)
        {
            this.Artist = Artist;
            this.Album = Album;
            this.Song = Song;
            this.Genre = Genre;
            this.Duration = Duration;
            this.FontFamily = "Segoe WP Light";
            
            this.ExtendedInfoText = Artist;
            if (Album != "" & Album != null)
            {
                this.ExtendedInfoText += " | " + Album;
            }
        }



        //PropertyChangedEventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
