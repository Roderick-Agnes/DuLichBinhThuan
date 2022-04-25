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
    public class HotelController : Controller
    {
        TravelDBDataContext db = new TravelDBDataContext();
        // GET: Tours
        public ActionResult Index(int? page)
        {
            int iSize = 9;
            int iPageNum = (page ?? 1);

            var listHotels = db.Hotels.ToList();
            return View(listHotels.ToPagedList(iPageNum, iSize));
        }
        public ActionResult HotelDetail(int id)
        {
            var hotel = db.Hotels.SingleOrDefault(n => n.MaHotel == id);
            return View(hotel);
        }
        public ActionResult OrderHotels(int id) // write void random id
        {
            int maxId = db.Hotels.Max(n => n.MaHotel);
            if (maxId == id)
            {
                maxId -= 1;
            }
            var hotel = db.Hotels.SingleOrDefault(n => n.MaHotel == maxId);
            return View(hotel);
        }
    }
}