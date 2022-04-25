using DulichBinhThuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace DulichBinhThuan.Controllers
{
    public class ToursController : Controller
    {
        TravelDBDataContext db = new TravelDBDataContext();
        // GET: Tours
        public ActionResult Index(int? page)
        {
            int iSize = 9;
            int iPageNum = (page ?? 1);

            var listTours = db.Tours.ToList();
            return View(listTours.ToPagedList(iPageNum, iSize));
        }
        public ActionResult ToursDetail(int id)
        {
            var tour = db.Tours.SingleOrDefault(n => n.MaTour == id);
            return View(tour);
        }
        public ActionResult LichTrinhTab(int id)
        {
            var lichTrinh = db.LichTrinhs.Where(n => n.MaTour == id).ToList();
            if (lichTrinh.Count() == 0 || lichTrinh == null)
            {
                ViewBag.ErrorCode = "1";
            }
            else
            {
                ViewBag.ErrorCode = "0";
            }
            return View(lichTrinh);
        }
        public ActionResult OrderTours(int id) // write void random id
        {
            int maxId = db.Tours.Max(n => n.MaTour);
            if(maxId == id)
            {
                maxId -= 1;
            }
            var tour = db.Tours.SingleOrDefault(n => n.MaTour == maxId);
            return View(tour);
        }
    }
}