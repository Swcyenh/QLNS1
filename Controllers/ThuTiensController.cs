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
    public class ThuTiensController : Controller
    {
        private readonly QLNS1Context _context;

        public ThuTiensController(QLNS1Context context)
        {
            _context = context;
        }

        // GET: ThuTiens
        public async Task<IActionResult> Index()
        {
              return _context.ThuTien != null ? 
                          View(await _context.ThuTien.ToListAsync()) :
                          Problem("Entity set 'QLNS1Context.ThuTien'  is null.");
        }



        // GET: ThuTiens/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdThuTien,HoTen,DiaChi,SoDienThoai,Email,NgayThu,SoTienThu")] ThuTien thuTien)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == thuTien.Email || u.PhoneNumber == thuTien.SoDienThoai);
                if (user != null)
                {
                    if (user.TienNo >= thuTien.SoTienThu)
                    {
                        user.TienNo = user.TienNo - thuTien.SoTienThu;
                        thuTien.NgayThu = DateTime.Now; 
                        _context.Add(thuTien);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("SoTienThu", "Số tien thu vuot qua tien no");
                    }
                }
            }
            return View(thuTien);
        }


        private bool ThuTienExists(int id)
        {
          return (_context.ThuTien?.Any(e => e.IdThuTien == id)).GetValueOrDefault();
        }
    }
}
