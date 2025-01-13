using System.ComponentModel.DataAnnotations;

namespace FRIchat.Models;

public class Odgovor
{
    public int Id { get; set; }
    public string Vsebina { get; set; }
    public string DatotekaUrl { get; set; }
    public string DatumObjave { get; set; }
    public string UporabnikId { get; set; }
    [Required]
    public int PredmetId { get; set; }
    public Predmet Predmet { get; set; }

}