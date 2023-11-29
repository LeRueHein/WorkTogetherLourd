using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WorkTogether.Wpf.ViewModels;

namespace WorkTogether.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour ListClientView.xaml
    /// </summary>
    public partial class ListClientView : UserControl
    {
        public ListClientView()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel();
        }
    }
}
