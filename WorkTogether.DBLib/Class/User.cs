using System;
using System.Collections.Generic;

namespace WorkTogether.DBLib.Class;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    /// <summary>
    /// (DC2Type:json)
    /// </summary>
    public string Roles { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
