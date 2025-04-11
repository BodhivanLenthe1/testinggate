namespace Zorgboerderij.Entities;
public class DagBezettingViewModel
{
    public string Dag { get; set; }
    public int AantalClienten { get; set; }
    public int AantalPersoneel { get; set; }
    public int ClientOchtend { get; set; }
    public int ClientMiddag { get; set; }
    public int ClientHeleDag { get; set; }

    public int PersoneelOchtend { get; set; }
    public int PersoneelMiddag { get; set; }
    public int PersoneelHeleDag { get; set; }
    public List<Clienten> ClientList { get; set; }
    public List<Personeel> PersoneelList { get; set; }

}
