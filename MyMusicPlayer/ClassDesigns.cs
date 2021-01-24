using Microsoft.Xna.Framework.Media;
using System;
using System.ComponentModel;





namespace MyMusicPlayer
{
    class ClassDesigns
    {
        //Variabeln erstellen
        public string Name { get; set; }

        //Zur Liste hinzufügen
        public ClassDesigns(string Name)
        {
            this.Name = Name;
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
