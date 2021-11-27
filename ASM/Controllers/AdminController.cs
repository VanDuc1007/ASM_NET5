using ASM.Constant;
using ASM.Models.ViewModel;
using ASM.Models;
using ASM.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ASM.Constant.SessionKey;

namespace ASM.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private INguoidungSvc _nguoidungSvc;
        public AdminController(IWebHostEnvironment webHostEnviroment, INguoidungSvc nguoidungSvc)
        {
            _webHostEnvironment = webHostEnviroment;
            _nguoidungSvc = nguoidungSvc;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            string userName = HttpContext.Session.GetString(SessionKey.NguoiDung.UserName);
            if (userName != null && userName != "")
            {
                return RedirectToAction("Index", "Home");
            }
            #region Hiện thị Login
            ViewLogin login = new ViewLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }
        //GET: AdminController

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                Nguoidung nguoidung = _nguoidungSvc.Login(viewLogin);
                if (nguoidung != null)
                {
                    HttpContext.Session.SetString(SessionKey.NguoiDung.UserName, nguoidung.UserName);
                    HttpContext.Session.SetString(SessionKey.NguoiDung.FullName, nguoidung.FullName);
                    HttpContext.Session.SetString(SessionKey.NguoiDung.NguoidungContext,
                        JsonConvert.SerializeObject(nguoidung));

                    return RedirectToAction(nameof(Index), "Admin");
                }
            }
            return View(viewLogin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.NguoiDung.UserName);
            HttpContext.Session.Remove(SessionKey.NguoiDung.FullName);
            HttpContext.Session.Remove(SessionKey.NguoiDung.NguoidungContext);

            return RedirectToAction(nameof(Index), "Admin");
        }


    }
}
