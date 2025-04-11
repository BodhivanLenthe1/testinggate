using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tbldp
{
    public int Dpid { get; set; }

    public int? clientId { get; set; }

    public string? Dagid { get; set; }

    public int? Bid { get; set; }

    public string? Kleur { get; set; }

    public int? Sid { get; set; }

    public int? Sid2 { get; set; }

    public int? Soort { get; set; }

    public DateTime? Datum { get; set; }

    public int? Volgorde { get; set; }

    public virtual Tblbakje? BidNavigation { get; set; }

    public virtual Tblclienten? Client { get; set; }
}
