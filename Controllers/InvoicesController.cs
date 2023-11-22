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
                    if (tempsach - invoice.SoLuong >= 10)
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
                            var user = _context.Users.FirstOrDefault(u => u.PhoneNumber == invoice.sdt);
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
        public IActionResult TongKetTien()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TongKetTien(IFormCollection Form)
        {
            Console.WriteLine("Start Running");
            var Thang = Form["Thang"];
            Console.WriteLine(Thang);
            var User = _context.Users.ToList();
            var viewModelList = new List<HienTienNo>();
            foreach (var i in User)
            {
                var HoaDon = _context.Invoice.FirstOrDefault(s => s.sdt == i.PhoneNumber);

                // Check if NhapSach is not null before accessing its properties
                if (User != null)
                {
                    var TonCuoiThang = i.TienNo;
                    var PhatSinh = _context.Invoice
                        .Where(o => o.Date.Month == Int32.Parse(Thang) && o.sdt == i.PhoneNumber && o.Debt == 1)
                        .Sum(o => o.Gia);

                    var TonDauThang = TinhTonDauNam(i.PhoneNumber, Thang);

                    // Perform further operations or return values as needed
                    var viewModel = new HienTienNo
                    {
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        PhoneNumber = i.PhoneNumber,
                        NoCuoiThang = TonCuoiThang,
                        PhatSinh = PhatSinh,
                        NoDauThang = TonDauThang
                    };                    // Add the view model to the list
                    viewModelList.Add(viewModel);
                }
            }
            return View("List", viewModelList);

        }
        private int TinhTonDauNam(string sdt, string thang)
        {
            var Invoice = _context.Invoice.Where(s => s.sdt == sdt && s.Date.Month == Int32.Parse(thang)).ToList();
            var ton = Invoice.Sum(o => o.Gia);
            return ton;
        }
    }
}
