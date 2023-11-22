using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            return _context.Sach != null ?
                        View(await _context.Sach.ToListAsync()) :
                        Problem("Entity set 'QLNS1Context.Sach'  is null.");
        }

        public IActionResult Create()
        {
            return View();
        }
        //TDOO: When value come from form, check amount import > 100 and check sachId exist and Sach.Amount < 200 then Sach += AmountImport
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,TenSach,TacGia,TheLoai,AmountImport,DateImport,SachSauNhap")] NhapSach nhapSach)
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

                    var sach = _context.Sach.FirstOrDefault(s => s.Name == nhapSach.TenSach  && s.Author == nhapSach.TacGia && s.Type == nhapSach.TheLoai);
                    if (sach == null)
                    {
                        ModelState.AddModelError("Id", "Sách không tồn tại");
                        return View(nhapSach);
                    }
                    else
                    {
                        if (sach.Amount >= 200)
                        {
                            ModelState.AddModelError("AmountImport", "Sách du");
                            return View(nhapSach);
                        }
                        nhapSach.DateImport = DateTime.Now;
                        sach.Amount += nhapSach.AmountImport;
                        nhapSach.SachSauNhap = sach.Amount;
                        _context.Add(nhapSach);
                        await _context.SaveChangesAsync();
                        
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(nhapSach);
        }


        private bool NhapSachExists(int id)
        {
          return (_context.Nhap?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
