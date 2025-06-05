using System.Collections.Generic;
using Zorgboerderij.Entities;

namespace Zorgboerderij.Models
{
    public class DagplanningBewerkenViewModel
    {
        public Clienten Client { get; set; }
        public string Dag { get; set; }
        public List<Dagindeling> Dagplanning { get; set; }
        public List<Bakjes> Bakjes { get; set; }
    }
}
