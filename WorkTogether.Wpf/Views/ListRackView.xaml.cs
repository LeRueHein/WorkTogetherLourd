using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WorkTogether.Wpf.ViewModels;

namespace WorkTogether.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour ListRackView.xaml
    /// </summary>
    public partial class ListRackView : UserControl
    {
        public ListRackView()
        {
            InitializeComponent();
            this.DataContext = new RackViewModel();
        }
    }
}
