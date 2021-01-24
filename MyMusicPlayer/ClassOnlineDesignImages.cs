using Microsoft.Xna.Framework.Media;
using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;





namespace MyMusicPlayer
{
    class ClassOnlineDesignImages
    {
        //Variabeln erstellen
        public int ID { get; set; }
        public BitmapImage Image { get; set; }
        public string Name { get; set; }
        public string Autor { get; set; }
        public string Category { get; set; }

        //Zur Liste hinzufügen
        public ClassOnlineDesignImages(int ID, BitmapImage Image, string Name, string Autor, string Category)
        {
            this.ID = ID;
            this.Image = Image;
            this.Name = Name;
            this.Autor = Autor;
            this.Category = Category;
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
