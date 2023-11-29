using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebAndSoft.Internal;
using WorkTogether.DBLib.Class;

namespace WorkTogether.Wpf.ViewModels
{
    class PackViewModel : ObservableObject
    {
        #region attributs

        /// <summary>
        /// packs
        /// </summary>
        private ObservableCollection<Pack> _Pack;


        /// <summary>
        /// packs séléctionnés
        /// </summary>
        private Pack _SelectedPack;


        /// <summary>
        /// Délégué pour l'ajout d'un pack
        /// </summary>
        private DelegateCommand<object> _CommandAddPack;


        /// <summary>
        /// Délégué pour la supp d'un pack
        /// </summary>
        private DelegateCommand<object> _CommandDelPack;

        /// <summary>
        /// Délégué pour la modif d'un pack
        /// </summary>
        private DelegateCommand<object> _CommandModifyPack;

        #endregion

        #region Propriété
        /// <summary>
        /// Obtient et défini la liste des packs
        /// </summary>
        public ObservableCollection<Pack> Packs
        { 
            get => _Pack;
            set => SetProperty(nameof(Packs), ref _Pack, value);
        }

        /// <summary>
        /// Obtient et défini le pack selectionné
        /// </summary>
        public Pack SelectedPacks
        { 
            get => _SelectedPack;
            set => SetProperty(nameof(SelectedPacks), ref _SelectedPack, value);
        }
        /// <summary>
        /// Obtient et défini le délégué pour l'ajout d'un pack
        /// </summary>
        public DelegateCommand<object> CommandAddPack { get => _CommandAddPack; set => _CommandAddPack = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la supp d'un pack
        /// </summary>
        public DelegateCommand<object> CommandDelPack { get => _CommandDelPack; set => _CommandDelPack = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la modif d'un pack
        /// </summary>
        public DelegateCommand<object> CommandModifyPack { get => _CommandModifyPack; set => _CommandModifyPack = value; }

        #endregion


        #region Construsteur
        /// <summary>
        /// Construteur
        /// Instencie les commandes 
        /// </summary>
        public PackViewModel()
        {
            CommandAddPack = new DelegateCommand<object>(AddPack).ObservesProperty(() => this.SelectedPacks);
            CommandDelPack = new DelegateCommand<object>(DelPack).ObservesProperty(() => this.SelectedPacks);
            CommandModifyPack = new DelegateCommand<object>(ModifyPack).ObservesProperty(() => this.SelectedPacks);

            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                this.Packs = new ObservableCollection<Pack>(context.Packs);
            }
        }
        #endregion

        #region Method

        /// <summary>
        /// ajouter un nouveau pack
        /// </summary>
        /// <param name="parameters"></param>
        internal void AddPack(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                Pack packs = new Pack();
                context.Packs.Add(packs);
                SelectedPacks = packs;
                this.Packs.Add(packs);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// supprimer une baie
        /// </summary>
        /// <param name="parameters"></param>
        internal void DelPack(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedPacks is not null)
                {
                    context.Packs.Remove(SelectedPacks);
                    this.Packs.Remove(SelectedPacks);
                    context.SaveChanges();
                }
                this.SelectedPacks = null;

            }
        }

        /// <summary>
        /// modifier une baie (pas utilisé)
        /// </summary>
        /// <param name="parameters"></param>
        internal void ModifyPack(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedPacks is not null)
                {
                    context.Update(this.SelectedPacks);
                    context.SaveChanges();
                }
                this.SelectedPacks = null;

            }
        }
        #endregion
    }
}
