using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WorkTogether.DBLib.Class;

public partial class Pack : IEditableObject
{
    
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int NumberSlot { get; set; }
   

   
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    /// <summary>
    /// copy des données
    /// </summary>
    private Pack backupCopy;


    /// <summary>
    /// Sauvegarde une copie des données originales
    /// </summary>
    public void BeginEdit()
    {
        backupCopy = MemberwiseClone() as Pack;
    }

    /// <summary>
    /// Annule les modifications en restaurant les données originales
    /// </summary>
    public void CancelEdit()
    { 
        if (backupCopy != null)
        {
            Id = backupCopy.Id;
            Name = backupCopy.Name;
            Price = backupCopy.Price;
            NumberSlot = backupCopy.NumberSlot;
        }
    }

    /// <summary>
    /// confirmation de l'edit
    /// </summary>
    public void EndEdit()
    {
        using ClientLegerBddContext context = new ClientLegerBddContext();
        {
            context.Entry(this).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
