namespace FRIchat.Models;

public class Odgovor
{
    public int Id { get; set; }
    public string Vsebina { get; set; }
    public string DatumObjave { get; set; }
    public int UporabnikId { get; set; }
    public int PredmetId { get; set; }
    public Predmet Predmet { get; set; }

}