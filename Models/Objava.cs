namespace FRIchat.Models;

public class Objava
{
    public int Id { get; set; }
    public string Naslov { get; set; }
    public string Vsebina { get; set; }
    public int Tip { get; set; }
    public string DatumObjave { get; set; }
    public int Status { get; set; }
    public int PredmetId { get; set; }
    public int Avtor { get; set; }
}