﻿#pragma checksum "D:\Moses\Projekte\Windows\Windows Phone\8.1 Music\MyMusicPlayer\MyMusicPlayer\Pages\TileSettings.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4A346B98703C81BDACCCC0DA9F4CB484"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MyMusicPlayer.Pages {
    
    
    public partial class TileSettings : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image ImgLogo;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Image ImgFirstTile;
        
        internal System.Windows.Controls.Button BtnBackgroundColor;
        
        internal System.Windows.Shapes.Rectangle RTBackgroundColorDemo;
        
        internal System.Windows.Controls.Image ImgDeleteBackground;
        
        internal System.Windows.Controls.Button BtnLogoAlbumImage;
        
        internal System.Windows.Controls.Button BtnResetMainTile;
        
        internal System.Windows.Controls.Image ImgSecondTile;
        
        internal System.Windows.Controls.TextBlock TBClicktoCreate;
        
        internal System.Windows.Controls.Button BtnSecondBackgroundColor;
        
        internal System.Windows.Shapes.Rectangle RTSecondBackgroundColorDemo;
        
        internal System.Windows.Controls.Image ImgDeleteBackground2;
        
        internal System.Windows.Controls.Button BtnSecondResetTile;
        
        internal System.Windows.Controls.Grid GRColor;
        
        internal System.Windows.Controls.StackPanel SPColor;
        
        internal Coding4Fun.Toolkit.Controls.ColorPicker CP;
        
        internal System.Windows.Controls.TextBlock CPTransparency;
        
        internal System.Windows.Controls.Slider CPSlider;
        
        internal System.Windows.Controls.TextBlock CPColor;
        
        internal System.Windows.Controls.Grid CPColorGrid;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyMusicPlayer;component/Pages/TileSettings.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ImgLogo = ((System.Windows.Controls.Image)(this.FindName("ImgLogo")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.ImgFirstTile = ((System.Windows.Controls.Image)(this.FindName("ImgFirstTile")));
            this.BtnBackgroundColor = ((System.Windows.Controls.Button)(this.FindName("BtnBackgroundColor")));
            this.RTBackgroundColorDemo = ((System.Windows.Shapes.Rectangle)(this.FindName("RTBackgroundColorDemo")));
            this.ImgDeleteBackground = ((System.Windows.Controls.Image)(this.FindName("ImgDeleteBackground")));
            this.BtnLogoAlbumImage = ((System.Windows.Controls.Button)(this.FindName("BtnLogoAlbumImage")));
            this.BtnResetMainTile = ((System.Windows.Controls.Button)(this.FindName("BtnResetMainTile")));
            this.ImgSecondTile = ((System.Windows.Controls.Image)(this.FindName("ImgSecondTile")));
            this.TBClicktoCreate = ((System.Windows.Controls.TextBlock)(this.FindName("TBClicktoCreate")));
            this.BtnSecondBackgroundColor = ((System.Windows.Controls.Button)(this.FindName("BtnSecondBackgroundColor")));
            this.RTSecondBackgroundColorDemo = ((System.Windows.Shapes.Rectangle)(this.FindName("RTSecondBackgroundColorDemo")));
            this.ImgDeleteBackground2 = ((System.Windows.Controls.Image)(this.FindName("ImgDeleteBackground2")));
            this.BtnSecondResetTile = ((System.Windows.Controls.Button)(this.FindName("BtnSecondResetTile")));
            this.GRColor = ((System.Windows.Controls.Grid)(this.FindName("GRColor")));
            this.SPColor = ((System.Windows.Controls.StackPanel)(this.FindName("SPColor")));
            this.CP = ((Coding4Fun.Toolkit.Controls.ColorPicker)(this.FindName("CP")));
            this.CPTransparency = ((System.Windows.Controls.TextBlock)(this.FindName("CPTransparency")));
            this.CPSlider = ((System.Windows.Controls.Slider)(this.FindName("CPSlider")));
            this.CPColor = ((System.Windows.Controls.TextBlock)(this.FindName("CPColor")));
            this.CPColorGrid = ((System.Windows.Controls.Grid)(this.FindName("CPColorGrid")));
        }
    }
}
