using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;





namespace MyMusicPlayer
{
    class ClassMedia
    {
        //Daten zum Song
        public int ID { get; set; }
        public string Name { get; set; }
        public string InfoString { get; set; }
        public string Artist { get; set; }

        //Daten zum Bild
        public string ImageSource { get; set; }
        public string FontSize { get; set; }
        public string ImageSize { get; set; }
        public string ImageVisibility { get; set; }

        //Daten zum zweiten Bild
        public string ImageSize2 { get; set; }
        public string ImageSource2 { get; set; }
        public string ImageVisibility2 { get; set; }

        //Aussehen der Listbox
        public string FontFamily { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }
        public string Margin { get; set; }

        //Prüfen ob bereits gröffnet ist
        public bool IsSelected { get; set; }

        //Prüfen um was es sich handelt
        public string WhatIs { get; set; }

        //Erweiterte Informationen
        public string ExtendedInfoText { get; set; }
        public string ExtendedInfoVisibility { get; set; }
        public string ExtendedInfoFontSize { get; set; }

        //Wenn Select and Play aktiv ist
        public bool SelectAndPlay {get; set;}
        
        //Gibt an ob Eintrag ausgewählt ist
        public bool Selected {get; set;}





        //Zur Liste hinzufügen
        public ClassMedia(int ID, string Name, string InfoString, string Artist, string FontSize, string ImageVisibility, string FontFamily, string Background, string Foreground, string Margin, bool IsSelected, string WhatIs, string ExtendedInfoText, string ExtendedInfoVisibility, bool SelectAndPlay, bool Selected)
        {

            //Hintergrundfarbe festlegen
            Color backgroundColor = (Color)Application.Current.Resources["PhoneBackgroundColor"];
            string AppBackgroundColor = Convert.ToString(backgroundColor);





            // Variabeln direkt übernehmen
            this.ID = ID;
            this.Name = Name;
            this.InfoString = InfoString;
            this.Artist = Artist;
            this.SelectAndPlay = SelectAndPlay;
            this.Selected = Selected;
            this.FontSize = FontSize;
            this.ImageVisibility = ImageVisibility;
            this.FontFamily = FontFamily;
            this.Background = Background;
            this.Foreground = Foreground;
            this.Margin = Margin;
            this.IsSelected = IsSelected;
            this.WhatIs = WhatIs;
            this.ExtendedInfoText = ExtendedInfoText;

            // Bilder auswählen, wenn Vordergrund Schwarz
            if (Foreground == "#FF000000")
            {
                // Wenn Select and Play aktiv
                if (SelectAndPlay == true)
                {
                    // Wenn Eintrag anders als Playlist
                    if (WhatIs != "Playlist")
                    {
                        this.ImageSource = "/Images/Select.Light.png";
                    }
                    // Wenn Eintrag Playlist
                    else
                    {
                        this.ImageSource = "/Images/Delete.Light.png";
                    }
                }
                // Wenn Select and Play nicht aktiv
                else
                {
                    this.ImageSource = "/Images/Play.Light.png";
                }
                
            }
            // Bilder auswählen, wenn Vordergrund Weiß
            else
            {
                // Wenn Select and Play aktiv
                if (SelectAndPlay == true)
                {
                    // Wenn Eintrag anders als Playlist
                    if (WhatIs != "Playlist")
                    {
                        this.ImageSource = "/Images/Select.Dark.png";
                    }
                    // Wenn Eintrag Playlist
                    else
                    {
                        this.ImageSource = "/Images/Delete.Dark.png";
                    }
                }
                // Wenn Select and Play nicht aktiv
                else
                {
                    this.ImageSource = "/Images/Play.Dark.png";
                }
            }
            
            // Größe der Bilder erstellen
            try
            {
                this.ImageSize = (Convert.ToInt32(FontSize) + (Convert.ToInt32(FontSize) / 2)).ToString();
            }
            catch
            {
                this.ImageSize = FontSize;
            }
            
            // Erweiterte Informationen erstellen, wenn aktiv
            if (ExtendedInfoVisibility == "true")
            {
                this.ExtendedInfoVisibility = "Visible";
            }
            else
            {
                this.ExtendedInfoVisibility = "Collapsed";
            }
            try
            {
                this.ExtendedInfoFontSize = ((Convert.ToInt32(FontSize) / 2) + (Convert.ToInt32(FontSize) / 4)).ToString();
            }
            catch
            {
                this.ExtendedInfoFontSize = FontSize;
            }

            // Wenn Lied ohne Album Listen Button ausgeben
            if (WhatIs == "Song" & SelectAndPlay == true & ImageVisibility != "Visible")
            {
                this.ImageVisibility = "Visible";
                try
                {
                    this.ImageSize = (Convert.ToInt32(FontSize) + (Convert.ToInt32(FontSize) / 2)).ToString();
                }
                catch
                {
                    this.ImageSize = FontSize;
                }
                if (Foreground == "#FF000000")
                {
                    this.ImageSource = "/Images/Select.Light.png";
                }
                else
                {
                    this.ImageSource = "/Images/Select.Dark.png";
                }
            }

            // Lied einzeln abspielen erstellen, nur wenn Select and Play deaktiv ist
            if (WhatIs == "Song" & SelectAndPlay == false)
            {
                this.ImageVisibility2 = "Visible";
                try
                {
                    this.ImageSize2 = (Convert.ToInt32(FontSize) + (Convert.ToInt32(FontSize) / 2)).ToString();
                }
                catch
                {
                    this.ImageSize2 = FontSize;
                }
                if (Foreground == "#FF000000")
                {
                    this.ImageSource2 = "/Images/Play.One.Light.png";
                }
                else
                {
                    this.ImageSource2 = "/Images/Play.One.Dark.png";
                }
            }
            // Bei Playliste und wenn Select and Play aktiv, Button Select ausgeben
            else if (WhatIs == "Playlist" & SelectAndPlay == true)
            {
                this.ImageVisibility2 = "Visible";
                try
                {
                    this.ImageSize2 = (Convert.ToInt32(FontSize) + (Convert.ToInt32(FontSize) / 2)).ToString();
                }
                catch
                {
                    this.ImageSize2 = FontSize;
                }
                if (Foreground == "#FF000000")
                {
                    this.ImageSource2 = "/Images/Refresh.Light.png";
                }
                else
                {
                    this.ImageSource2 = "/Images/Refresh.Dark.png";
                }
            }
            // Wenn nicht angezeigt wird
            else
            {
                this.ImageVisibility2 = "Collapsed";
                this.ImageSize2 = "0";
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
