using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FRIchat.Data;
using FRIchat.Models;
using Microsoft.AspNetCore.Authorization;

namespace FRIchat.Controllers
{
    
    public class UporabnikController : Controller
    {
        private readonly FRIchatContext _context;

        public UporabnikController(FRIchatContext context)
        {
            _context = context;
        }

        // GET: Uporabnik
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uporabnik.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var uporabnik = await _context.Uporabnik.ToListAsync();
            return View(uporabnik);
        }

        // GET: Uporabnik/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uporabnik = await _context.Uporabnik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uporabnik == null)
            {
                return NotFound();
            }

            return View(uporabnik);
        }

        // GET: Uporabnik/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }

        // POST: Uporabnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Email,Geslo,Telefon")] Uporabnik uporabnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uporabnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uporabnik);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckCredentials([Bind("Email, Geslo")] Uporabnik uporabnik)
        {
            if (_context.Uporabnik.Any(e => e.Email == uporabnik.Email && e.Geslo == uporabnik.Geslo))
            {
                return Redirect($"../Home");
            }
            return Redirect("/Uporabnik/Login");
        }

        // GET: Uporabnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uporabnik = await _context.Uporabnik.FindAsync(id);
            if (uporabnik == null)
            {
                return NotFound();
            }
            return View(uporabnik);
        }

        // POST: Uporabnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Email,Geslo,Telefon")] Uporabnik uporabnik)
        {
            if (id != uporabnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uporabnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UporabnikExists(uporabnik.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(uporabnik);
        }

        // GET: Uporabnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uporabnik = await _context.Uporabnik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uporabnik == null)
            {
                return NotFound();
            }

            return View(uporabnik);
        }

        // POST: Uporabnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uporabnik = await _context.Uporabnik.FindAsync(id);
            if (uporabnik != null)
            {
                _context.Uporabnik.Remove(uporabnik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UporabnikExists(int id)
        {
            return _context.Uporabnik.Any(e => e.Id == id);
        }
    }
}
