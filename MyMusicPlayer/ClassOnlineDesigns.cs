using Microsoft.Xna.Framework.Media;
using System;
using System.ComponentModel;





namespace MyMusicPlayer
{
    class ClassOnlineDesigns
    {
        //Variabeln erstellen
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Autor { get; set; }
        public bool Portrait { get; set; }
        public bool Landscape { get; set; }
        public bool MPPortrait { get; set; }
        public bool MPLandscape { get; set; }

        //Zur Liste hinzufügen
        public ClassOnlineDesigns(int ID, string Name, string Category, string Autor, bool Portrait, bool Landscape, bool MPPortrait, bool MPLandscape)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
            this.Autor = Autor;
            this.Portrait = Portrait;
            this.MPPortrait = MPPortrait;
            this.Landscape = Landscape;
            this.MPLandscape = MPLandscape;
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
