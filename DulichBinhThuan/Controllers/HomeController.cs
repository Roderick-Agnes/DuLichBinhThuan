using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DulichBinhThuan.Models;
using NuGet.Protocol.Core.Types;

namespace DulichBinhThuan.Controllers
{
    public class DonDatTourCustom
    {
        public int maDon { get; set; }
        public int maTour { get; set; }
        public int amount { get; set; }
        public DonDatTourCustom(int maDon, int maTour, int amount)
        {
            this.maDon = maDon;
            this.maTour = maTour;
            this.amount = amount;
        }
    }
    public class DonDatHotelCustom
    {
        public int maDon { get; set; }
        public int maHotel { get; set; }
        public int amount { get; set; }
        public DonDatHotelCustom(int maDon, int maHotel, int amount)
        {
            this.maDon = maDon;
            this.maHotel = maHotel;
            this.amount = amount;
        }
    }
    
    public class HomeController : Controller
    {
        TravelDBDataContext db = new TravelDBDataContext();
        List<DonDatTourCustom> listToursHot = new List<DonDatTourCustom>();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //Start section "ToursHot" in here
        //Show options ToursTypes
        public ActionResult ToursTypes()
        {
            var tourTypes = db.LoaiTours.ToList();
            return View(tourTypes);
        }
        //Count amount hot tours by maTour
        public int GetCountTours(int maDon, int maTour)
        {
            int count = db.DonDatTours.Count(n => n.MaTour == maTour);
            return count;
        }
        //Check Id of tour exitted ?
        public bool TourIdExitted(int id)
        {
            for (int i = 0; i < listToursHot.Count(); i++)
            {
                if (listToursHot[i].maTour == id)
                {
                    return true;
                }
            }
            return false;
        }
        //Main void show result to UI - Section "ToursHot"
        public ActionResult ToursHot()
        {
            var listDon = db.DonDatTours.ToList();
            for(int i = 0; i < listDon.Count(); i++)
            {
                if (i == 0)
                {
                    DonDatTourCustom obj = new DonDatTourCustom(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaTour), GetCountTours(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaTour)));
                    listToursHot.Add(obj);
                } else
                {
                    if (!TourIdExitted(Convert.ToInt32(listDon[i].MaTour)))
                    {
                        DonDatTourCustom obj = new DonDatTourCustom(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaTour), GetCountTours(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaTour)));
                        listToursHot.Add(obj);
                    }
                }
            }
            int amount = 3; // số lượng tour hot muốn show lên mục Tours Hot
            List<Tour> listTourHotShow = new List<Tour>();
            if (listToursHot.Count() == 1)
            {
                Tour t = db.Tours.SingleOrDefault(m => m.MaTour == listToursHot[0].maTour);
                listTourHotShow.Add(t);
            } else
            {
                //Repository.InsertAllOnSubmit(list.Take(100).Cast<Activity>());
                for (int i = 0; i < listToursHot.OrderByDescending(n => n.amount).Count(); i++)
                {
                    Tour t = db.Tours.SingleOrDefault(m => m.MaTour == listToursHot[i].maTour);
                    listTourHotShow.Add(t);
                }
            }
            
            return View(listTourHotShow.Take(amount));
        }
        //Done section "ToursHot" in here


        //Start section "Hotel & Resort" in here
        List<DonDatHotelCustom> listHotelsHot = new List<DonDatHotelCustom>();
        //Count amount hot hotels by maHotel
        public int GetCountHotels(int maDon, int maHotel)
        {
            int count = db.DonDatHotels.Count(n => n.MaHotel == maHotel);
            return count;
        }
        //Check Id of hotel exitted ?
        public bool HotelIdExitted(int id)
        {
            for (int i = 0; i < listHotelsHot.Count(); i++)
            {
                if (listHotelsHot[i].maHotel == id)
                {
                    return true;
                }
            }
            return false;
        }
        //Main void show result to UI - Section "Hotel & Resort"
        public ActionResult HotelAndResort()
        {
            var listDon = db.DonDatHotels.ToList();
            for (int i = 0; i < listDon.Count(); i++)
            {
                if (i == 0)
                {
                    DonDatHotelCustom obj = new DonDatHotelCustom(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaHotel), GetCountTours(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaHotel)));
                    listHotelsHot.Add(obj);
                }
                else
                {
                    if (!HotelIdExitted(Convert.ToInt32(listDon[i].MaHotel)))
                    {
                        DonDatHotelCustom obj = new DonDatHotelCustom(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaHotel), GetCountTours(listDon[i].MaDon, Convert.ToInt32(listDon[i].MaHotel)));
                        listHotelsHot.Add(obj);
                    }
                }
            }
            int amount = 2; // số lượng Hotels hot muốn show lên mục "Hotel & Resort"
            List<Hotel> listHotelHotShow = new List<Hotel>();
            if (listHotelsHot.Count() == 1)
            {
                Hotel h = db.Hotels.SingleOrDefault(m => m.MaHotel == listHotelsHot[0].maHotel);
                listHotelHotShow.Add(h);
            }
            else
            {
                //Repository.InsertAllOnSubmit(list.Take(100).Cast<Activity>());
                for (int i = 0; i < listHotelsHot.OrderByDescending(n => n.amount).Count(); i++)
                {
                    Hotel h = db.Hotels.SingleOrDefault(m => m.MaHotel == listHotelsHot[i].maHotel);
                    listHotelHotShow.Add(h);
                }
            }

            return View(listHotelHotShow.Take(amount));
        }
        //Done section "Hotel & Resort" in here


        //Start section "Bài viết" in here
        public ActionResult NewBlogs()
        {
            int amount = 3; // số lượng bài viết mới nhất muốn show lên mục "Bài viết"
            var listBlogs = db.Blogs.OrderByDescending(n => n.MaBlog).ToList();
            return View(listBlogs.Take(amount));
        }
        //Done section "Bài viết" in here
    }
}