using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tblorganisatie
{
    public int Orgid { get; set; }

    public string? Organisatie { get; set; }

    public string? Plaats { get; set; }

    public string? Code { get; set; }

    public string? Orgidweb { get; set; }

    public virtual ICollection<Tblinlog> Tblinlogs { get; set; } = new List<Tblinlog>();
}
