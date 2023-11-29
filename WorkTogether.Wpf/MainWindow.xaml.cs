using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Windows;
using WorkTogether.Wpf.ViewModels;
using WorkTogether.Wpf.Views;

namespace WorkTogether.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ConnexionViewModel();


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((ConnexionViewModel)this.DataContext).ConnexionValidator();
        }


        private void ButtonAccueil_Click(object sender, RoutedEventArgs e)
        {
            DockPanelShow.Children.Clear();
            DockPanelShow.Children.Add(new WelcomeView());
        }

        private void ButtonBaies_Click(object sender, RoutedEventArgs e)
        {
            DockPanelShow.Children.Clear();
            DockPanelShow.Children.Add(new ListRackView());
        }

        private void ButtonPack_Click(object sender, RoutedEventArgs e)
        {
            DockPanelShow.Children.Clear();
            DockPanelShow.Children.Add(new ListPackView());
        }

        private void ButtonClient_Click(object sender, RoutedEventArgs e)
        {
            DockPanelShow.Children.Clear();
            DockPanelShow.Children.Add(new ListClientView());
        }

        private void ButtonCompte_Click(object sender, RoutedEventArgs e)
        {
            DockPanelShow.Children.Clear();
        }

        private void ButtonReservation_Click(object sender, RoutedEventArgs e)
        {
            DockPanelShow.Children.Clear();
            DockPanelShow.Children.Add(new ListReservationView());
        }
    }
}