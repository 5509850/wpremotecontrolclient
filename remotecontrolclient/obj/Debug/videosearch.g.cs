﻿#pragma checksum "E:\Dropbox\FTP_RCS_Client\ONLINE\remotecontrolclient\remotecontrolclient\videosearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B57178F0E922F441F4043442D611B63"
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
    
    
    public partial class videosearch : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock search;
        
        internal System.Windows.Controls.TextBox searchword;
        
        internal System.Windows.Controls.Button clear;
        
        internal System.Windows.Controls.Button left;
        
        internal System.Windows.Controls.Button scan;
        
        internal System.Windows.Controls.Button right;
        
        internal System.Windows.Controls.Button but_fullscan;
        
        internal System.Windows.Controls.Button but_stop;
        
        internal System.Windows.Controls.Button PgUp;
        
        internal System.Windows.Controls.Button up;
        
        internal System.Windows.Controls.Button FavAdd;
        
        internal System.Windows.Controls.Button prev;
        
        internal System.Windows.Controls.Button but_play;
        
        internal System.Windows.Controls.Button next;
        
        internal System.Windows.Controls.Button PgDw;
        
        internal System.Windows.Controls.Button down;
        
        internal System.Windows.Controls.Button Delete;
        
        internal System.Windows.Controls.Button OrderTime;
        
        internal System.Windows.Controls.Button OrderFile;
        
        internal System.Windows.Controls.Button OrderFolder;
        
        internal System.Windows.Controls.Button close;
        
        internal System.Windows.Controls.Button OrderFav;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/remotecontrolclient;component/videosearch.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.search = ((System.Windows.Controls.TextBlock)(this.FindName("search")));
            this.searchword = ((System.Windows.Controls.TextBox)(this.FindName("searchword")));
            this.clear = ((System.Windows.Controls.Button)(this.FindName("clear")));
            this.left = ((System.Windows.Controls.Button)(this.FindName("left")));
            this.scan = ((System.Windows.Controls.Button)(this.FindName("scan")));
            this.right = ((System.Windows.Controls.Button)(this.FindName("right")));
            this.but_fullscan = ((System.Windows.Controls.Button)(this.FindName("but_fullscan")));
            this.but_stop = ((System.Windows.Controls.Button)(this.FindName("but_stop")));
            this.PgUp = ((System.Windows.Controls.Button)(this.FindName("PgUp")));
            this.up = ((System.Windows.Controls.Button)(this.FindName("up")));
            this.FavAdd = ((System.Windows.Controls.Button)(this.FindName("FavAdd")));
            this.prev = ((System.Windows.Controls.Button)(this.FindName("prev")));
            this.but_play = ((System.Windows.Controls.Button)(this.FindName("but_play")));
            this.next = ((System.Windows.Controls.Button)(this.FindName("next")));
            this.PgDw = ((System.Windows.Controls.Button)(this.FindName("PgDw")));
            this.down = ((System.Windows.Controls.Button)(this.FindName("down")));
            this.Delete = ((System.Windows.Controls.Button)(this.FindName("Delete")));
            this.OrderTime = ((System.Windows.Controls.Button)(this.FindName("OrderTime")));
            this.OrderFile = ((System.Windows.Controls.Button)(this.FindName("OrderFile")));
            this.OrderFolder = ((System.Windows.Controls.Button)(this.FindName("OrderFolder")));
            this.close = ((System.Windows.Controls.Button)(this.FindName("close")));
            this.OrderFav = ((System.Windows.Controls.Button)(this.FindName("OrderFav")));
        }
    }
}
