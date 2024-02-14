using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WorkTogether.DBLib.Class;

public partial class Rack : IEditableObject
{
    
    public int Id { get; set; }

    public int NumberSlot { get; set; }

    







    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
    

    private Rack backupCopy;

    /// <summary>
    /// Sauvegarde une copie des données originales
    /// </summary>
    public void BeginEdit()
    {
        backupCopy = MemberwiseClone() as Rack;
    }

    /// <summary>
    /// Annule les modifications en restaurant les données originales
    /// </summary>
    public void CancelEdit()
    {
        if (backupCopy != null)
        {
            Id = backupCopy.Id;
            NumberSlot = backupCopy.NumberSlot;
            
        }
    }

    public void EndEdit()
    {
        using ClientLegerBddContext context = new ClientLegerBddContext();
        {
            context.Entry(this).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

}


