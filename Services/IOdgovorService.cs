using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FRIchat.Services
{
    public interface IOdgovorService
    {
        Task CreateOdgovorAsync(int predmetId, string vsebina, string datotekaUrl, string uporabnikId);
        Task<string> SaveImageAsync(IFormFile file);
        Task DeleteOdgovorAsync(int odgovorId);
    }
}