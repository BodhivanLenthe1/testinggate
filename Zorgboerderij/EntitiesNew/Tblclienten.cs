using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tblclienten
{
    public int clientId { get; set; }

    public string? Voornaam { get; set; }

    public string? Achternaam { get; set; }

    public string? Foto { get; set; }

    public int? Maandag { get; set; }

    public int? Dinsdag { get; set; }

    public int? Woensdag { get; set; }

    public int? Donderdag { get; set; }

    public int? Vrijdag { get; set; }

    public int? Zaterdag { get; set; }

    public int? Pin { get; set; }

    public int? Groepskleur { get; set; }

    public virtual ICollection<Tblafwezig> Tblafwezigs { get; set; } = new List<Tblafwezig>();

    public virtual ICollection<Tbldp> Tbldps { get; set; } = new List<Tbldp>();
}
