using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using WorkTogether.DBLib.Class;

namespace WorkTogether.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// utilisateur
        /// </summary>
        private User _User;

        /// <summary>
        /// obtient et défini le <see cref="_User"/>
        /// </summary>
        public User User
        { 
            get => _User; 
            set => SetProperty(nameof(User), ref _User, value);
        }


        #region Events

        /// <summary>
        /// Se produit quand une propriété a été changé 
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Se produit quand une propriété va être changé 
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;
        #endregion

        #region Methods

        /// <summary>
        /// Déclenche l'évènement PropertyChanging <see cref="PropertyChanging"/>
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui va changé</param>
        private void OnPropertyChanging(string propertyName) => this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


        /// <summary>
        /// Déclenche l'évènement PropertyChanged <see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui à changé</param>
        private void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        /// <summary>
        /// Assigne une propriété et déclenche les évènements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        protected void SetProperty<T>(string propertyName, ref T field, T value)
        {
            if ((field == null && value != null) || field?.Equals(value) == false)
            {
                OnPropertyChanging(propertyName);
                field = value;
                OnPropertyChanged(propertyName);


            }
        }


        #endregion


    }

}
