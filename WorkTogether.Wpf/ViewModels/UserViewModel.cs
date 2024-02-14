using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebAndSoft.Internal;
using WorkTogether.DBLib.Class;

namespace WorkTogether.Wpf.ViewModels
{
    class UserViewModel : ObservableObject
    {
        #region attributs

        /// <summary>
        /// User
        /// </summary>
        private ObservableCollection<User> _User;


        /// <summary>
        /// users séléctionnés
        /// </summary>
        private User _SelectedUser;


        /// <summary>
        /// Délégué pour l'ajout d'un user
        /// </summary>
        private DelegateCommand<object> _CommandAddUser;


        /// <summary>
        /// Délégué pour la supp d'un user
        /// </summary>
        private DelegateCommand<object> _CommandDelUser;

        /// <summary>
        /// Délégué pour la modif d'un user
        /// </summary>
        private DelegateCommand<object> _CommandModifyUser;

        #endregion

        #region Propriété
        /// <summary>
        /// Obtient et défini la liste des users
        /// </summary>
        public ObservableCollection<User> Users
        {
            get => _User;
            set => SetProperty(nameof(Users), ref _User, value);
        }

        /// <summary>
        /// Obtient et défini le pack selectionné
        /// </summary>
        public User SelectedUsers
        {
            get => _SelectedUser;
            set => SetProperty(nameof(SelectedUsers), ref _SelectedUser, value);
        }
        /// <summary>
        /// Obtient et défini le délégué pour l'ajout d'un user
        /// </summary>
        public DelegateCommand<object> CommandAddUser { get => _CommandAddUser; set => _CommandAddUser = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la supp d'un user
        /// </summary>
        public DelegateCommand<object> CommandDelUser { get => _CommandDelUser; set => _CommandDelUser = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la modif d'un pack
        /// </summary>
        public DelegateCommand<object> CommandModifyUser { get => _CommandModifyUser; set => _CommandModifyUser = value; }

        #endregion

        #region Construsteur
        /// <summary>
        /// Construteur
        /// Instencie les commandes 
        /// </summary>
        public UserViewModel()
        {
            CommandAddUser = new DelegateCommand<object>(AddUser).ObservesProperty(() => this.SelectedUsers);
            CommandDelUser = new DelegateCommand<object>(DelUser).ObservesProperty(() => this.SelectedUsers);
            CommandModifyUser = new DelegateCommand<object>(ModifyUser).ObservesProperty(() => this.SelectedUsers);

            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                this.Users = new ObservableCollection<User>(context.Users);
            }
        }
        #endregion

        #region Method

        /// <summary>
        /// ajouter un nouveau user
        /// </summary>
        /// <param name="parameters"></param>
        internal void AddUser(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                User users = new User();
                context.Users.Add(users);
                SelectedUsers = users;
                this.Users.Add(users);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// supprimer un user
        /// </summary>
        /// <param name="parameters"></param>
        internal void DelUser(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedUsers is not null)
                {
                    context.Users.Remove(SelectedUsers);
                    this.Users.Remove(SelectedUsers);
                    context.SaveChanges();
                }
                this.SelectedUsers = null;

            }
        }

        /// <summary>
        /// modifier un user 
        /// </summary>
        /// <param name="parameters"></param>
        internal void ModifyUser(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedUsers is not null)
                {
                    context.Update(this.SelectedUsers);
                    context.SaveChanges();
                }
                this.SelectedUsers = null;

            }
        }
        #endregion
    }
}
