using ASM.Constant;
using ASM.Filters;
using ASM.Models;
using ASM.Models.ViewModel;
using ASM.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWebHostEnvironment _webHostEnvironment;
        IMonAnSvc _monAnSvc;
        IKhachhangSvc _khachhangSvc;
        IDonhangSvc _donhangSvc;
        IDonhangChitietSvc _donhangChitietSvc;

        public HomeController(IWebHostEnvironment webHostEnvironment,
            IMonAnSvc monAnSvc,
            IKhachhangSvc khachhangSvc,
            IDonhangSvc donhangSvc,
            IDonhangChitietSvc donhangChitietSvc)
        {
            _webHostEnvironment = webHostEnvironment;
            _monAnSvc = monAnSvc;
            _khachhangSvc = khachhangSvc;
            _donhangSvc = donhangSvc;
            _donhangChitietSvc = donhangChitietSvc;

        }

        #region Login Logout Regiser
        public IActionResult Login(string returnUrl)
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_FullName);
            if (kH_Email != null && kH_Email != "")
            {
                return RedirectToAction("Index", "Home");
            }
            #region Hiển thị login
            ViewWebLogin login = new ViewWebLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewWebLogin viewWebLogin)
        {
            if (ModelState.IsValid)
            {
                KhachHang khachHang = _khachhangSvc.Login(viewWebLogin);
                if (khachHang != null)
                {
                    HttpContext.Session.SetString(SessionKey.KhachHang.KH_Email, khachHang.EmailAddress);
                    HttpContext.Session.SetString(SessionKey.KhachHang.KH_FullName, khachHang.FullName);
                    HttpContext.Session.SetString(SessionKey.KhachHang.KhachhangContext,
                        JsonConvert.SerializeObject(khachHang));
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

            }
            return View(viewWebLogin);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.KhachHang.KH_Email);
            HttpContext.Session.Remove(SessionKey.KhachHang.KH_FullName);
            HttpContext.Session.Remove(SessionKey.KhachHang.KhachhangContext);
            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang khachHang)
        {
            try
            {
                _khachhangSvc.AddKhachHang(khachHang);
                return RedirectToAction(nameof(Login), new { id = khachHang.KhachhangID });
            }
            catch
            {
                return View();
            }
        }
        #endregion
        #region Info + History DatHang
        [AuthenticationFilterAttribute_KH]
        public ActionResult Info()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_FullName);
            if (kH_Email == null || kH_Email == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var khachhangContext = HttpContext.Session.GetString(SessionKey.KhachHang.KhachhangContext);
            var khachhangId = JsonConvert.DeserializeObject<KhachHang>(khachhangContext).KhachhangID;
            var khachhang = _khachhangSvc.GetKhachHang(khachhangId);
            return View(khachhang);
        }

        [AuthenticationFilterAttribute_KH]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(int KhachhangID, KhachHang khachhang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _khachhangSvc.EditKhachHang(KhachhangID, khachhang);
                    return RedirectToAction(nameof(Index), new { id = khachhang.KhachhangID });
                }

            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }
        #region cart
        public ActionResult Index()
        {
            return View(_monAnSvc.GetMonAnAll());
        }
        public IActionResult AddCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                var monAn = _monAnSvc.GetMonAn(id);
                List<ViewCart> listCart = new List<ViewCart>()
                {
                    new ViewCart
                    {
                        MonAn =monAn,
                        Quantity = 1
                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    var monAn = _monAnSvc.GetMonAn(id);
                    dataCart.Add(new ViewCart
                    {
                        MonAn = monAn,
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }
            return Ok();
        }
        public IActionResult Cart()
        {
            List<ViewCart> dataCart = new List<ViewCart>();
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
            }
            return View(dataCart);
        }
        public IActionResult UpdateCart(int id, int soluong)
        {
            var cart = HttpContext.Session.GetString("cart");
            double total = 0;
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart[i].Quantity = soluong;
                        break;
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }
        public IActionResult DeleteCart(int id)
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }

        public IActionResult OrderCart()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_FullName);
            if (kH_Email == null || kH_Email == "")  // đã có session
            {
                return BadRequest();
            }
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null && cart.Count() > 0)
            {
                #region DonHang
                var khachhangContext = HttpContext.Session.GetString(SessionKey.KhachHang.KhachhangContext);
                var khachhangId = JsonConvert.DeserializeObject<KhachHang>(khachhangContext).KhachhangID;

                double total = Tongtien();

                var donhang = new DonHang()
                {
                    Trangthai = TrangthaiDonhang.Moidat,
                    KhachhangID = khachhangId,
                    Tongtien = total,
                    Ngaydat = DateTime.Now,
                    Ghichu = "",
                };

                _donhangSvc.AddDonhang(donhang);
                int donhangId = donhang.DonhangID;

                #region Chitiet
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    DonhangChitiet chitiet = new DonhangChitiet()
                    {
                        DonhangID = donhangId,
                        MonAnID = dataCart[i].MonAn.MonAnID,
                        SoLuong = dataCart[i].Quantity,
                        Thanhtien = dataCart[i].MonAn.Gia * dataCart[i].Quantity,
                        Ghichu = "",
                    };
                    //donhang.DonhangChitiets.Add(chitiet);
                    _donhangChitietSvc.AddDonhangChitietSvc(chitiet);
                }

                #endregion
                #endregion
                #endregion
                

                HttpContext.Session.Remove("cart");

                return Ok();
            }
            return BadRequest();
        }



        [NonAction]
        private double Tongtien()
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    total += (dataCart[i].MonAn.Gia * dataCart[i].Quantity);
                }
            }
            return total;
        }

        [AuthenticationFilterAttribute_KH]
        public ActionResult Details(int id)
        {
            return View(_donhangSvc.GetDonHang(id));
        }

        [AuthenticationFilterAttribute_KH]
        public IActionResult History()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_FullName);
            if (kH_Email == null || kH_Email == "")  // đã có session
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_donhangSvc.GetDonhangAll());
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}