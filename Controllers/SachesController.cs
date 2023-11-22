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
            sach.SachId = id;

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
        public IActionResult TongKet()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TongKet(IFormCollection Form)
        {
            Console.WriteLine("Start Running");
            var Thang = Form["Thang"];
            Console.WriteLine(Thang);
            var Sach = _context.Sach.ToList();
            var viewModelList = new List<HienHoaDon>();
            foreach (var i in Sach)
            {
                var NhapSach = _context.Nhap.FirstOrDefault(s => s.TenSach == i.Name);

                // Check if NhapSach is not null before accessing its properties
                if (NhapSach != null)
                {
                    var TonCuoiNam = i.Amount;
                    var PhatSinh = _context.Nhap
                        .Where(o => o.DateImport.Month == Int32.Parse(Thang) && o.TenSach == i.Name)
                        .Sum(o => o.AmountImport);

                    var TonDauNam = TinhTonDauNam(i.Name, Thang);

                    // Perform further operations or return values as needed
                    var viewModel = new HienHoaDon
                    {
                        TenSach = i.Name,
                        TonCuoiNam = TonCuoiNam,
                        PhatSinh = PhatSinh,
                        TonDauNam = TonDauNam
                    };

                    // Add the view model to the list
                    viewModelList.Add(viewModel);
                }
            }
            return View("List",viewModelList);
            
        }
            private bool SachExists(int id)
            {
                return (_context.Sach?.Any(e => e.SachId == id)).GetValueOrDefault();
            }
            private int TinhTonDauNam(string TenSach, string thang)
            {
                var NhapSach = _context.Nhap.Where(s => s.TenSach == TenSach && s.DateImport.Month == Int32.Parse(thang)).ToList();
                var recordWithHighestDay = NhapSach.OrderByDescending(o => o.DateImport.Day).FirstOrDefault();
                var HoaDonThang = _context.Nhap.Where(o => o.DateImport.Month == Int32.Parse(thang)).ToList();
                var HoaDonThangTuNgayNhapCuoiCung = HoaDonThang.Where(o => o.DateImport.Day >= recordWithHighestDay.DateImport.Day).ToList();
                var TongBan = HoaDonThangTuNgayNhapCuoiCung.Sum(o => o.AmountImport);
                var ton = recordWithHighestDay.SachSauNhap - TongBan;
                return ton;
            }
    }
}
