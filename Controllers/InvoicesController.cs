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
    public class InvoicesController : Controller
    {
        private readonly QLNS1Context _context;

        public InvoicesController(QLNS1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Sach != null ? 
                          View(await _context.Sach.ToListAsync()) :
                          Problem("Entity set 'QLNS1Context.Invoice'  is null.");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("MaHoaDon,TenKhachHang,TenSach,TheLoai,SoLuong,Gia,Date")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                var sach = _context.Sach.FirstOrDefault(s => s.Name == invoice.TenSach && s.Type == invoice.TheLoai);
                var tempsach = sach.Amount;
                if ( tempsach - invoice.SoLuong  >= 10)
                {
                    invoice.Gia = sach.Price * invoice.SoLuong;
                    invoice.Date = DateTime.Now;
                    _context.Add(invoice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("SoLuong", "Số lượng sách không đủ");
                    return View(invoice);
                }
                
            }
            ModelState.AddModelError("SoLuong", "Số lượng sách không đủ");
            return View(invoice);
        }

        private bool InvoiceExists(int id)
        {
          return (_context.Invoice?.Any(e => e.MaHoaDon == id)).GetValueOrDefault();
        }
    }
}
