using System;
using System.Collections.Generic;

namespace WorkTogether.DBLib.Class;

public partial class TypeReservation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Percentage { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
