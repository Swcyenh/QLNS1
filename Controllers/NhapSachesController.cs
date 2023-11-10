using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLNS1.Data;
using QLNS1.Models;

namespace QLNS1.Controllers
{
    public class NhapSachesController : Controller
    {
        private readonly QLNS1Context _context;

        public NhapSachesController(QLNS1Context context)
        {
            _context = context;
        }

        // GET: NhapSaches
        public async Task<IActionResult> Index()
        {
              return _context.Nhap != null ? 
                          View(await _context.Nhap.ToListAsync()) :
                          Problem("Entity set 'QLNS1Context.Nhap'  is null.");
        }

        // GET: NhapSaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nhap == null)
            {
                return NotFound();
            }

            var nhapSach = await _context.Nhap
                .FirstOrDefaultAsync(m => m.SachId == id);
            if (nhapSach == null)
            {
                return NotFound();
            }

            return View(nhapSach);
        }

        // GET: NhapSaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhapSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SachId,AmountImport,DateImport")] NhapSach nhapSach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhapSach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhapSach);
        }

        // GET: NhapSaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nhap == null)
            {
                return NotFound();
            }

            var nhapSach = await _context.Nhap.FindAsync(id);
            if (nhapSach == null)
            {
                return NotFound();
            }
            return View(nhapSach);
        }

        // POST: NhapSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SachId,AmountImport,DateImport")] NhapSach nhapSach)
        {
            if (id != nhapSach.SachId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhapSach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhapSachExists(nhapSach.SachId))
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
            return View(nhapSach);
        }

        // GET: NhapSaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nhap == null)
            {
                return NotFound();
            }

            var nhapSach = await _context.Nhap
                .FirstOrDefaultAsync(m => m.SachId == id);
            if (nhapSach == null)
            {
                return NotFound();
            }

            return View(nhapSach);
        }

        // POST: NhapSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nhap == null)
            {
                return Problem("Entity set 'QLNS1Context.Nhap'  is null.");
            }
            var nhapSach = await _context.Nhap.FindAsync(id);
            if (nhapSach != null)
            {
                _context.Nhap.Remove(nhapSach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhapSachExists(int id)
        {
          return (_context.Nhap?.Any(e => e.SachId == id)).GetValueOrDefault();
        }
    }
}
