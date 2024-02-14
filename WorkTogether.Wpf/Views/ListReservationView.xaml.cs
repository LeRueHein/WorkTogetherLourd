using System.Windows;
using System.Windows.Controls;
using WorkTogether.Wpf.ViewModels;
using System.IO;
using System.Diagnostics;


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


            var psi = new ProcessStartInfo();
            psi.FileName = @"c:\windows\explorer.exe";
            psi.Arguments = "C:\\Users\\Utilisateur\\Desktop\\WorkToGether\\WorkTogether\\WorkTogether.Wpf\\bin\\Debug\\net8.0-windows";
            Process.Start(psi);

        }
        
    }
}
