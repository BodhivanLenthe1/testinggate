using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tblafwezig
{
    public int Afid { get; set; }

    public int? clientId { get; set; }

    public DateTime? Datum { get; set; }

    public int? Afsoort { get; set; }

    public virtual Tblclienten? Client { get; set; }
}
