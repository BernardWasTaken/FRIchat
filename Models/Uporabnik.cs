using Microsoft.AspNetCore.Identity;

namespace FRIchat.Models;

public class Uporabnik : IdentityUser
{
    public int Id { get; set; }
    public string Ime { get; set; }
    public string? Email { get; set; }
    public string Geslo { get; set; }
    public string Telefon { get; set; }
}