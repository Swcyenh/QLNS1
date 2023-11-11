using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLNS1.Data;
using QLNS1.Models;

namespace QLNS1.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class SachesController : Controller
    {
        private readonly QLNS1Context _context;

        public SachesController(QLNS1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Sach != null ? 
                          View(await _context.Sach.ToListAsync()) :
                          Problem("Entity set 'QLNS1Context.Sach'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sach == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach
                .FirstOrDefaultAsync(m => m.SachId == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Display()
        {
            if (_context.Sach == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach.ToListAsync();
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,Type,Amount,Price,Picture")] Sach sach)
        {

                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Saches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sach == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SachId,Name,Author,Type,Amount,Price,Picture")] Sach sach)
        {
            if (id != sach.SachId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.SachId))
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
            return View(sach);
        }

        // GET: Saches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sach == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach
                .FirstOrDefaultAsync(m => m.SachId == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sach == null)
            {
                return Problem("Entity set 'QLNS1Context.Sach'  is null.");
            }
            var sach = await _context.Sach.FindAsync(id);
            if (sach != null)
            {
                _context.Sach.Remove(sach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
          return (_context.Sach?.Any(e => e.SachId == id)).GetValueOrDefault();
        }
    }
}
