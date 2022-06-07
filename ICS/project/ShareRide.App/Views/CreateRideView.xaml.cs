﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using ShareRide.App.ViewModels;

namespace ShareRide.App.Views
{
    /// <summary>
    /// Interaction logic for RideListView.xaml
    /// </summary>
    public partial class CreateRideView : UserControlBase
    {
        public CreateRideView()
        {
            InitializeComponent();
        }
        void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Dispatcher.BeginInvoke(new Action(() => tb.SelectAll()));
        }
    }
}
