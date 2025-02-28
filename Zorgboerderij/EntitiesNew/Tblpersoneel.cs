using System;
using System.Collections.Generic;

namespace Zorgboerderij.EntitiesNew;

public partial class Tblpersoneel
{
    public int Persid { get; set; }

    public string? Voornaam { get; set; }

    public string? Achternaam { get; set; }

    public string? Foto { get; set; }

    public int? Maandag { get; set; }

    public int? Dinsdag { get; set; }

    public int? Woensdag { get; set; }

    public int? Donderdag { get; set; }

    public int? Vrijdag { get; set; }

    public int? Zaterdag { get; set; }

    public int? Afwezig { get; set; }

    public int? Groepskleur { get; set; }
}
