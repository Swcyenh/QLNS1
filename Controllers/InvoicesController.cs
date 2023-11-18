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
        public async Task<IActionResult> Create([Bind("MaHoaDon,TenKhachHang,TenSach,TheLoai,SoLuong,Gia,Date,sdt,Debt")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                var sach = _context.Sach.FirstOrDefault(s => s.Name == invoice.TenSach && s.Type == invoice.TheLoai);
                if (sach != null)
                {
                    var tempsach = sach.Amount;
                    Console.WriteLine(tempsach);
                    if ( tempsach - invoice.SoLuong  >= 10)
                    {
                        if (invoice.Debt == 0)
                        {
                            invoice.Gia = sach.Price * invoice.SoLuong;
                            invoice.Date = DateTime.Now;
                            sach.Amount -= invoice.SoLuong;
                            _context.Add(invoice);
                            _context.Update(sach);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            //TODO: update user debt
                            var user = _context.Users.FirstOrDefault(u =>  u.PhoneNumber == invoice.sdt);
                            invoice.Gia = sach.Price * invoice.SoLuong;
                            invoice.Date = DateTime.Now;
                            sach.Amount -= invoice.SoLuong;
                            user.TienNo += invoice.Gia;
                            user.NgayNo = DateTime.Now.ToString();
                            _context.Add(invoice);
                            _context.Update(sach);
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("SoLuong", "Số lượng sách không đủ");
                        return View(invoice);
                    }

                }
                
            }
            return View(invoice);
        }

        
        private bool InvoiceExists(int id)
        {
          return (_context.Invoice?.Any(e => e.MaHoaDon == id)).GetValueOrDefault();
        }
    }
}
