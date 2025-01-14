using FRIchat.Data;
using FRIchat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FRIchat.Services
{
    public class OdgovorService : IOdgovorService
    {
        private readonly FRIchatContext _context;
        private readonly UserManager<Uporabnik> _userManager;
        private readonly IWebHostEnvironment _environment;

        public OdgovorService(FRIchatContext context, UserManager<Uporabnik> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        public async Task CreateOdgovorAsync(int predmetId, string vsebina, string datotekaUrl, string uporabnikId)
        {
            var odgovor = new Odgovor
            {
                Vsebina = String.Empty,
                DatumObjave = DateTime.Now.ToString("MM-dd HH:mm"),
                PredmetId = predmetId,
                DatotekaUrl = String.Empty,
                UporabnikId = uporabnikId,
            };
            if (!String.IsNullOrEmpty(datotekaUrl))
            {
                odgovor.DatotekaUrl = datotekaUrl;
            }

            if (!String.IsNullOrEmpty(vsebina))
            {
                odgovor.Vsebina = vsebina;
            }
            _context.Add(odgovor);
            await _context.SaveChangesAsync();
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return $"/uploads/{fileName}";
        }
    }
}