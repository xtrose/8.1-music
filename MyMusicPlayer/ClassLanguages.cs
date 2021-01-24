using System;
using System.ComponentModel;





namespace MyMusicPlayer
{





    class ClassLanguages
    {
        public string name { get; set; }
        public string code { get; set; }
        public string translation { get; set; }

        public ClassLanguages(string name, string code, string translation)
        {
            this.name = name;
            this.code = code;
            this.translation = translation;
        }

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
