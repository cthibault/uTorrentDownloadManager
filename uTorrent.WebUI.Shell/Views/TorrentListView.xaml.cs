﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uTorrent.WebUI.Shell.Views.Interfaces;

namespace uTorrent.WebUI.Shell.Views
{
    /// <summary>
    /// Interaction logic for TorrentListView.xaml
    /// </summary>
    public partial class TorrentListView : UserControl, ITorrentListView
    {
        public TorrentListView()
        {
            InitializeComponent();
        }
    }
}
