using Microsoft.Xna.Framework.Media;
using System;
using System.ComponentModel;





namespace MyMusicPlayer
{
    class ClassPlaylist
    {
        //Variabeln erstellen
        public Song MySong { get; set; }

        //Zur Liste hinzufügen
        public ClassPlaylist(Song MySong)
        {
            this.MySong = MySong;
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
