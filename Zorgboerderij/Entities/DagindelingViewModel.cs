namespace Zorgboerderij.Entities
{
    public class DagindelingViewModel
    {
        public string Dag { get; set; }
        public List<Clienten> Clienten { get; set; }
        public List<Personeel> Personeel { get; set; }
        public Clienten Client { get; set; }
        public List<Dagindeling> Dagindelingen { get; set; }
        public List<Bakjes> Bakjes { get; set; }
        public List<Clienten> AlleClienten { get; set; }
    }


}
