using Syncfusion.XlsIO;
using System;
using System.Windows;
using WebAndSoft.Internal;
using WorkTogether.DBLib.Class;

namespace WorkTogether.Wpf.ViewModels
{
    internal class ConnexionViewModel : ObservableObject
    {
        #region Attributs
        /// <summary>
        /// login
        /// </summary>
        private string _Login;

        /// <summary>
        /// Password
        /// </summary>
        private string _Password;

        /// <summary>
        /// est log ou pas
        /// </summary>
        private bool _IsLogin;

        #endregion

        #region Propriété

        /// <summary>
        /// obtient et récupère le login
        /// </summary>
        public string Login { get => _Login; set => _Login = value; }

        /// <summary>
        /// obtient et récupère le password
        /// </summary>
        public string Password { get => _Password; set => _Password = value; }

        /// <summary>
        /// obtient et récupère le user s'il est login
        /// </summary>
        public bool IsLogin { get => _IsLogin; set => SetProperty(nameof(IsLogin), ref _IsLogin, value); }

        #endregion

        #region Method
        /// <summary>
        /// Methode pour vérifier le login et mot de passe des users
        /// </summary>
        internal void ConnexionValidator()
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {

                User? user = context.Users.FirstOrDefault(u => u.Email == Login);
                if (user != null)
                {

                    string userPassword = user.Password.Replace("$2y$13$", "$2a$13$");
                    IsLogin = BCrypt.Net.BCrypt.Verify(Password, userPassword);
                    (Application.Current as App).User = user;
                }
                if (!IsLogin)
                {
                    MessageBox.Show("Votre email ou votre mot de passe est faux !", "Erreur", MessageBoxButton.OK,MessageBoxImage.Exclamation);

                }



            }
        }
        #endregion
    }
}
