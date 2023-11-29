using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebAndSoft.Internal;
using WorkTogether.DBLib.Class;


namespace WorkTogether.Wpf.ViewModels
{
    class RackViewModel : ObservableObject
    {
        #region Attributs
        /// <summary>
        /// Rack
        /// </summary>
        private ObservableCollection<Rack> _Rack;

        /// <summary>
        /// Rack séléctionné
        /// </summary>
        private Rack _SelectedRack;

        /// <summary>
        /// Délégué pour l'ajout d'une rack
        /// </summary>

        private DelegateCommand<object> _CommandAddRack;

        /// <summary>
        /// Délégué pour la supp d'une rack
        /// </summary>
        private DelegateCommand<object> _CommandDelRack;

        /// <summary>
        /// Délégué pour la modif d'une rack
        /// </summary>
        private DelegateCommand<object> _CommandModifyRack;

        #endregion

        #region Properties

        /// <summary>
        /// Obtient et défini la liste des racks
        /// </summary>
        public ObservableCollection<Rack> Racks
        {
            get => _Rack;
            set => SetProperty(nameof(Rack), ref _Rack, value);
        }

        /// <summary>
        /// obtient et défini la rack séléctionné
        /// </summary>
        public Rack? SelectedRacks
        { 
            get => _SelectedRack; 
            set => SetProperty(nameof(SelectedRacks), ref _SelectedRack, value); 
        }

        /// <summary>
        /// Obtient et défini le délégué pour l'ajout d'une espèce
        /// </summary>
        public DelegateCommand<object> CommandAddRack { get => _CommandAddRack; set => _CommandAddRack = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la supp d'une espèce
        /// </summary>
        public DelegateCommand<object> CommandDelRack { get => _CommandDelRack; set => _CommandDelRack = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la modif d'une espèce
        /// </summary>
        public DelegateCommand<object> CommandModifyRack { get => _CommandModifyRack; set => _CommandModifyRack = value; }

        #endregion

        #region Construsteur
        /// <summary>
        /// Construteur
        /// Instencie les commandes 
        /// </summary>
        public RackViewModel()
        {
            CommandAddRack = new DelegateCommand<object>(AddRack).ObservesProperty(() => this.SelectedRacks);
            CommandDelRack = new DelegateCommand<object>(DelRack).ObservesProperty(() => this.SelectedRacks);
            CommandModifyRack = new DelegateCommand<object>(ModifyRack).ObservesProperty(() => this.SelectedRacks);




            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                this.Racks = new ObservableCollection<Rack>(context.Racks);
            }
        }
        #endregion


        #region Method

        /// <summary>
        /// ajouter une nouvelle baie
        /// </summary>
        /// <param name="parameters"></param>
        internal void AddRack(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                Rack racks = new Rack() { NumberSlot = 42 };
                context.Racks.Add(racks);
                SelectedRacks = racks;
                this.Racks.Add(racks);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// supprimer une baie
        /// </summary>
        /// <param name="parameters"></param>
        internal void DelRack(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedRacks is not null)
                {
                    context.Racks.Remove(SelectedRacks);
                    this.Racks.Remove(SelectedRacks);
                    context.SaveChanges();
                }
                this.SelectedRacks = null;

            }
        }

        /// <summary>
        /// modifier une baie (pas utilisé)
        /// </summary>
        /// <param name="parameters"></param>
        internal void ModifyRack(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedRacks is not null)
                {
                    context.Update(this.SelectedRacks);
                    context.SaveChanges();
                }
                this.SelectedRacks = null;

            }
        }
        #endregion
    }
}
