using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tblinlog
{
    public int Inlogid { get; set; }

    public string? Inlognaam { get; set; }

    public DateOnly? Datum { get; set; }

    public TimeOnly? Tijd { get; set; }

    public string? Ip { get; set; }

    public int? Correct { get; set; }

    public string Wachtwoord { get; set; } = null!;

    public int? Orgid { get; set; }

    public virtual Tblorganisatie? Org { get; set; }
}
