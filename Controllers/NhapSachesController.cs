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



        public IActionResult Create()
        {
            return View();
        }
        //TDOO: When value come from form, check amount import > 100 and check sachId exist and Sach.Amount < 200 then Sacj+= AmountImport
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,SachId,AmountImport,DateImport")] NhapSach nhapSach)
        {
            if (ModelState.IsValid)
            {
                if (nhapSach.AmountImport <100)
                {
                    ModelState.AddModelError("AmountImport", "Số lượng nhập không được bé hơn 100");
                    return View(nhapSach);
                }
                else
                { 
                    var sach = await _context.Sach.FindAsync(nhapSach.Id);
                    if ((sach == null) || (sach.SachId != nhapSach.Id))
                    {
                        ModelState.AddModelError("SachId", "Mã sách không tồn tại");
                        return View(nhapSach);
                    }
                    else
                    {
                        sach.Amount += nhapSach.AmountImport;
                    }
                }
                _context.Add(nhapSach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhapSach);
        }


        private bool NhapSachExists(int id)
        {
          return (_context.Nhap?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
