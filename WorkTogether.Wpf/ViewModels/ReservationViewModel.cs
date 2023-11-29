using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using WebAndSoft.Internal;
using WorkTogether.DBLib.Class;

namespace WorkTogether.Wpf.ViewModels
{
    class ReservationViewModel : ObservableObject
    {

        #region attributs

        /// <summary>
        /// reservations
        /// </summary>
        private ObservableCollection<Reservation> _Reservation;


        /// <summary>
        /// Reservation séléctionnés
        /// </summary>
        private Reservation _SelectedReservation;


        /// <summary>
        /// Délégué pour l'ajout d'une Reservation
        /// </summary>
        private DelegateCommand<object> _CommandAddReservation;


        /// <summary>
        /// Délégué pour la supp d'une Reservation
        /// </summary>
        private DelegateCommand<object> _CommandDelReservation;

        /// <summary>
        /// Délégué pour la modif d'une Reservation
        /// </summary>
        private DelegateCommand<object> _CommandModifyReservation;

        #endregion

        #region Propriété
        /// <summary>
        /// Obtient et défini la liste des Reservations
        /// </summary>
        public ObservableCollection<Reservation> Reservations
        {
            get => _Reservation;
            set => SetProperty(nameof(Reservations), ref _Reservation, value);
        }

        /// <summary>
        /// Obtient et défini la Reservation selectionné
        /// </summary>
        public Reservation SelectedReservations
        { 
            get => _SelectedReservation;
            set => SetProperty(nameof(SelectedReservations), ref _SelectedReservation, value);
        }
        /// <summary>
        /// Obtient et défini le délégué pour l'ajout d'une Reservation
        /// </summary>
        public DelegateCommand<object> CommandAddReservation { get => _CommandAddReservation; set => _CommandAddReservation = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la supp d'une Reservation
        /// </summary>
        public DelegateCommand<object> CommandDelReservation { get => _CommandDelReservation; set => _CommandDelReservation = value; }

        /// <summary>
        /// Obtient et défini le délégué pour la modif d'une Reservation
        /// </summary>
        public DelegateCommand<object> CommandModifyReservation { get => _CommandModifyReservation; set => _CommandModifyReservation = value; }

        #endregion


        #region Construsteur
        /// <summary>
        /// Construteur
        /// Instencie les commandes 
        /// </summary>
        public ReservationViewModel()
        {
            CommandAddReservation = new DelegateCommand<object>(AddReservation).ObservesProperty(() => this.SelectedReservations);
            CommandDelReservation = new DelegateCommand<object>(DelReservation).ObservesProperty(() => this.SelectedReservations);
            CommandModifyReservation = new DelegateCommand<object>(ModifyReservation).ObservesProperty(() => this.SelectedReservations);


            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {

                this.Reservations = new ObservableCollection<Reservation>(context.Reservations.Include(r => r.Pack).Include(r => r.User));
            }
        }
        #endregion

        #region Method

        /// <summary>
        /// ajouter une nouvelle Reservation
        /// </summary>
        /// <param name="parameters"></param>
        internal void AddReservation(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                Reservation reservations = new Reservation();
                context.Reservations.Add(reservations);
                SelectedReservations = reservations;
                this.Reservations.Add(reservations);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// supprimer une Reservation
        /// </summary>
        /// <param name="parameters"></param>
        internal void DelReservation(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedReservations is not null)
                {
                    context.Reservations.Remove(SelectedReservations);
                    this.Reservations.Remove(SelectedReservations);
                    context.SaveChanges();
                }
                this.SelectedReservations = null;

            }
        }

        /// <summary>
        /// modifier une Reservation
        /// </summary>
        /// <param name="parameters"></param>
        internal void ModifyReservation(object parameters = null)
        {
            using (ClientLegerBddContext context = new ClientLegerBddContext())
            {
                if (SelectedReservations is not null)
                {
                    context.Update(this.SelectedReservations);
                    context.SaveChanges();
                }
                this.SelectedReservations = null;

            }
        }


        internal void ExportToPdf()
        {



            // Créez une instance du fichier PDF en créant une instance du PDF   
            // Classe Writer utilisant le document et le filestrem dans le constructeur.  
            StringBuilder stringBuilder = new();


            stringBuilder.AppendLine("Liste des réservations : " + Environment.NewLine);


            foreach (var resa in this.Reservations)
            {
                stringBuilder.AppendLine("Code : " + resa.Code + "Prix : " + resa.Price + "€" + "Nom du pack" + resa.Pack.Name);
            }





            System.IO.FileStream fs = new FileStream("toto.pdf", FileMode.Create);


            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Créez une instance du fichier PDF en créant une instance du PDF   
            // Classe Writer utilisant le document et le filestrem dans le constructeur.  

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.Open();
            // Ajoutez une phrase simple et bien connue au document de manière fluide  
            document.Add(new iTextSharp.text.Paragraph(stringBuilder.ToString()));
            // Ferme le document  
            document.Close();
            // Ferme l'instance du rédacteur  
            writer.Close();
            // Toujours fermer explicitement les descripteurs de fichiers ouverts  
            fs.Close();
        }
        #endregion
    }
}

