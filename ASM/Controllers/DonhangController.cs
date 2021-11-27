using ASM.Models;
using ASM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM.Controllers
{
    public class DonhangController : BaseController
    {
        private IDonhangSvc _donhangSvc;
        public DonhangController(IDonhangSvc donhangSvc)
        {
            _donhangSvc = donhangSvc;
        }
        // GET: DonhangController
        public ActionResult Index()
        {
            return View(_donhangSvc.GetDonhangAll());
        }

        // GET: DonhangController/Details/5
        public ActionResult Details(int id)
        {
            return View(_donhangSvc.GetDonHang(id));
        }

        // GET: DonhangController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DonhangController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: DonhangController/Edit/5
        public ActionResult Edit(int id)
        {
            var donhang = _donhangSvc.GetDonHang(id);
            return View(donhang);
        }

        // POST: DonhangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DonHang donhang)
        {
            try
            {
                donhang.Khachhang = null;
                _donhangSvc.EditDonhang(id, donhang);
                return RedirectToAction(nameof(Details),new {id = donhang.DonhangID});
            }
            catch
            {
                return View();
            }
        }

        // GET: DonhangController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DonhangController/Delete/5
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
