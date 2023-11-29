using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WorkTogether.Wpf.ViewModels;

namespace WorkTogether.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour ListPackView.xaml
    /// </summary>
    public partial class ListPackView : UserControl
    {
        public ListPackView()
        {
            InitializeComponent();
            this.DataContext = new PackViewModel();
        }
    }
}
