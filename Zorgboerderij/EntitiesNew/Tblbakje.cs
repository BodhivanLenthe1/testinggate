using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tblbakje
{
    public int Bid { get; set; }

    public string? Foto { get; set; }

    public string? Titel { get; set; }

    public string? Kleur { get; set; }

    public virtual ICollection<Tbldp> Tbldps { get; set; } = new List<Tbldp>();
}
