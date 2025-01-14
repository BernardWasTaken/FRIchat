using System.Net;
using System.Net.Http.Headers;
using FRIchat.Data;
using FRIchat.Models;
using FRIchat.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FRIchat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IOdgovorService _IOdgovorService;
        public ApiController(IOdgovorService IOdgovorService)
        {
            _IOdgovorService = IOdgovorService;
        }
        
        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveMessage([FromBody] SaveMessageModel model)
        {
            Console.WriteLine("!!!Form!!");
            Console.WriteLine(model.vsebina);
            if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            string userId = HttpContext.User.Identity.Name;

            await _IOdgovorService.CreateOdgovorAsync(model.predmetId, model.vsebina??"", model.datotekaUrl??"", userId);

            return Ok();
        }
        
        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                if (file.Length > 0)
                {
                    string folderName = "uploads";
                    string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    // Get filename
                    if (ContentDispositionHeaderValue.TryParse(file.ContentDisposition, out var contentDisposition))
                    {
                        // Important to get the actual filename
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(contentDisposition.FileName.Trim('"'));
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        return Ok(new { filePath = $"/uploads/{fileName}" });
                    }
                    else
                    {
                        return BadRequest("Invalid file content.");
                    }
                
                }
                else
                {
                    return BadRequest("No file uploaded.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        
        public class SaveMessageModel
        {
            public int predmetId { get; set; }
            public string? vsebina { get; set; }
            public string? datotekaUrl { get; set; }
        }
    }
}
