﻿#pragma checksum "E:\Dropbox\FTP_RCS_Client\ONLINE\remotecontrolclient\remotecontrolclient\musicsearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "083EED86EFE80B1ED4E605E2B5DD622C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34011
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace remotecontrolclient {
    
    
    public partial class musicsearch : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Media.ImageBrush background;
        
        internal Microsoft.Phone.Controls.Pivot pivot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock search;
        
        internal System.Windows.Controls.TextBox searchword;
        
        internal System.Windows.Controls.Button clear;
        
        internal System.Windows.Controls.Button PgUp;
        
        internal System.Windows.Controls.Button up;
        
        internal System.Windows.Controls.Button FavAdd;
        
        internal System.Windows.Controls.Button prev;
        
        internal System.Windows.Controls.Button but_play;
        
        internal System.Windows.Controls.Button next;
        
        internal System.Windows.Controls.Button PgDw;
        
        internal System.Windows.Controls.Button down;
        
        internal System.Windows.Controls.Button close;
        
        internal System.Windows.Controls.Button OrderFolder;
        
        internal System.Windows.Controls.Button OrderAlbum;
        
        internal System.Windows.Controls.Button OrderFav;
        
        internal System.Windows.Controls.StackPanel xscan;
        
        internal System.Windows.Controls.Button left;
        
        internal System.Windows.Controls.Button right;
        
        internal System.Windows.Controls.StackPanel xscan2;
        
        internal System.Windows.Controls.Button but_fullscan;
        
        internal System.Windows.Controls.Button scan;
        
        internal System.Windows.Controls.Button but_stop;
        
        internal System.Windows.Controls.Button one;
        
        internal System.Windows.Controls.Button allfolder;
        
        internal System.Windows.Controls.Button allalbum;
        
        internal System.Windows.Controls.Button artist;
        
        internal System.Windows.Controls.Button alltrack;
        
        internal System.Windows.Controls.Button random;
        
        internal System.Windows.Controls.HyperlinkButton but_back;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/remotecontrolclient;component/musicsearch.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.background = ((System.Windows.Media.ImageBrush)(this.FindName("background")));
            this.pivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.search = ((System.Windows.Controls.TextBlock)(this.FindName("search")));
            this.searchword = ((System.Windows.Controls.TextBox)(this.FindName("searchword")));
            this.clear = ((System.Windows.Controls.Button)(this.FindName("clear")));
            this.PgUp = ((System.Windows.Controls.Button)(this.FindName("PgUp")));
            this.up = ((System.Windows.Controls.Button)(this.FindName("up")));
            this.FavAdd = ((System.Windows.Controls.Button)(this.FindName("FavAdd")));
            this.prev = ((System.Windows.Controls.Button)(this.FindName("prev")));
            this.but_play = ((System.Windows.Controls.Button)(this.FindName("but_play")));
            this.next = ((System.Windows.Controls.Button)(this.FindName("next")));
            this.PgDw = ((System.Windows.Controls.Button)(this.FindName("PgDw")));
            this.down = ((System.Windows.Controls.Button)(this.FindName("down")));
            this.close = ((System.Windows.Controls.Button)(this.FindName("close")));
            this.OrderFolder = ((System.Windows.Controls.Button)(this.FindName("OrderFolder")));
            this.OrderAlbum = ((System.Windows.Controls.Button)(this.FindName("OrderAlbum")));
            this.OrderFav = ((System.Windows.Controls.Button)(this.FindName("OrderFav")));
            this.xscan = ((System.Windows.Controls.StackPanel)(this.FindName("xscan")));
            this.left = ((System.Windows.Controls.Button)(this.FindName("left")));
            this.right = ((System.Windows.Controls.Button)(this.FindName("right")));
            this.xscan2 = ((System.Windows.Controls.StackPanel)(this.FindName("xscan2")));
            this.but_fullscan = ((System.Windows.Controls.Button)(this.FindName("but_fullscan")));
            this.scan = ((System.Windows.Controls.Button)(this.FindName("scan")));
            this.but_stop = ((System.Windows.Controls.Button)(this.FindName("but_stop")));
            this.one = ((System.Windows.Controls.Button)(this.FindName("one")));
            this.allfolder = ((System.Windows.Controls.Button)(this.FindName("allfolder")));
            this.allalbum = ((System.Windows.Controls.Button)(this.FindName("allalbum")));
            this.artist = ((System.Windows.Controls.Button)(this.FindName("artist")));
            this.alltrack = ((System.Windows.Controls.Button)(this.FindName("alltrack")));
            this.random = ((System.Windows.Controls.Button)(this.FindName("random")));
            this.but_back = ((System.Windows.Controls.HyperlinkButton)(this.FindName("but_back")));
        }
    }
}

