﻿#pragma checksum "E:\Dropbox\FTP_RCS_Client\ONLINE\remotecontrolclient\remotecontrolclient\PowerMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C12749AD1A2C4ADFF85B6804738E3D2E"
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
    
    
    public partial class PowerMenu : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Media.ImageBrush background;
        
        internal System.Windows.Controls.Button bt_on;
        
        internal System.Windows.Controls.Button bt_poweroff;
        
        internal System.Windows.Controls.Button bt_reboot;
        
        internal System.Windows.Controls.Button bt_shutdown;
        
        internal System.Windows.Controls.Button bt_hibernate;
        
        internal System.Windows.Controls.Button bt_standby;
        
        internal System.Windows.Controls.Button bt_lockscr;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/remotecontrolclient;component/PowerMenu.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.background = ((System.Windows.Media.ImageBrush)(this.FindName("background")));
            this.bt_on = ((System.Windows.Controls.Button)(this.FindName("bt_on")));
            this.bt_poweroff = ((System.Windows.Controls.Button)(this.FindName("bt_poweroff")));
            this.bt_reboot = ((System.Windows.Controls.Button)(this.FindName("bt_reboot")));
            this.bt_shutdown = ((System.Windows.Controls.Button)(this.FindName("bt_shutdown")));
            this.bt_hibernate = ((System.Windows.Controls.Button)(this.FindName("bt_hibernate")));
            this.bt_standby = ((System.Windows.Controls.Button)(this.FindName("bt_standby")));
            this.bt_lockscr = ((System.Windows.Controls.Button)(this.FindName("bt_lockscr")));
            this.but_back = ((System.Windows.Controls.HyperlinkButton)(this.FindName("but_back")));
        }
    }
}

