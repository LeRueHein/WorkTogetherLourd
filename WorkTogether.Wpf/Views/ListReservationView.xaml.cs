using System.Windows;
using System.Windows.Controls;
using WorkTogether.Wpf.ViewModels;
using System.IO;


namespace WorkTogether.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour ListReservationView.xaml
    /// </summary>
    public partial class ListReservationView : UserControl
    {
        public ListReservationView()
        {
            InitializeComponent();
            this.DataContext = new ReservationViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((ReservationViewModel)this.DataContext).ExportToPdf();

        }
    }
}
