using ASM.Models;
using ASM.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Controllers
{
    public class NguoidungController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private INguoidungSvc _nguoidungSvc;
        public NguoidungController(IWebHostEnvironment webHostEnviroment, INguoidungSvc nguoidungSvc)
        {
            _webHostEnvironment = webHostEnviroment;
            _nguoidungSvc = nguoidungSvc;
        }
        // GET: NguoidungController
        public ActionResult Index()
        {
            return View(_nguoidungSvc.GetNguoidungAll());
        }

        // GET: NguoidungController/Details/5
        public ActionResult Details(int id)
        {
            var nguoidung = _nguoidungSvc.GetNguoidung(id);
            return View(nguoidung);
        }

        // GET: NguoidungController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoidungController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nguoidung nguoidung)
        {
            try
            {
                _nguoidungSvc.AddNguoidung(nguoidung);
                return RedirectToAction(nameof(Details), new { id = nguoidung.NguoidungID });
            }
            catch
            {
                return View();
            }
        }

        // GET: NguoidungController/Edit/5
        public ActionResult Edit(int id)
        {
            var nguoidung = _nguoidungSvc.GetNguoidung(id);
            return View(nguoidung);
        }

        // POST: NguoidungController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Nguoidung nguoidung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _nguoidungSvc.EditNguoidung(id, nguoidung);
                }
                return RedirectToAction(nameof(Details), new { id = nguoidung.NguoidungID });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: NguoidungController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NguoidungController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
