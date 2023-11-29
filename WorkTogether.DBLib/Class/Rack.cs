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

    public void BeginEdit()
    {
        // Sauvegarde une copie des données originales
        backupCopy = MemberwiseClone() as Rack;
    }

    public void CancelEdit()
    {
        // Annule les modifications en restaurant les données originales
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


