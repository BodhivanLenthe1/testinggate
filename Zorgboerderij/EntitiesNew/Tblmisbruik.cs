using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tblmisbruik
{
    public int Misbruikid { get; set; }

    public string? Soort { get; set; }

    public string? Inlognaam { get; set; }

    public DateOnly? Datum { get; set; }

    public TimeOnly? Tijd { get; set; }

    public string? Ip { get; set; }
}
