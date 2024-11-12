using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FRIchat.Data;
using FRIchat.Models;

namespace FRIchat.Controllers
{
    public class OdgovorObjavaController : Controller
    {
        private readonly FRIchatContext _context;

        public OdgovorObjavaController(FRIchatContext context)
        {
            _context = context;
        }

        // GET: OdgovorObjava
        public async Task<IActionResult> Index()
        {
            return View(await _context.OdgovorObjava.ToListAsync());
        }

        // GET: OdgovorObjava/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovorObjava = await _context.OdgovorObjava
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odgovorObjava == null)
            {
                return NotFound();
            }

            return View(odgovorObjava);
        }

        // GET: OdgovorObjava/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OdgovorObjava/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ObjavaId,OdgovorId")] OdgovorObjava odgovorObjava)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odgovorObjava);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(odgovorObjava);
        }

        // GET: OdgovorObjava/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovorObjava = await _context.OdgovorObjava.FindAsync(id);
            if (odgovorObjava == null)
            {
                return NotFound();
            }
            return View(odgovorObjava);
        }

        // POST: OdgovorObjava/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ObjavaId,OdgovorId")] OdgovorObjava odgovorObjava)
        {
            if (id != odgovorObjava.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odgovorObjava);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdgovorObjavaExists(odgovorObjava.Id))
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
            return View(odgovorObjava);
        }

        // GET: OdgovorObjava/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovorObjava = await _context.OdgovorObjava
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odgovorObjava == null)
            {
                return NotFound();
            }

            return View(odgovorObjava);
        }

        // POST: OdgovorObjava/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odgovorObjava = await _context.OdgovorObjava.FindAsync(id);
            if (odgovorObjava != null)
            {
                _context.OdgovorObjava.Remove(odgovorObjava);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdgovorObjavaExists(int id)
        {
            return _context.OdgovorObjava.Any(e => e.Id == id);
        }
    }
}
