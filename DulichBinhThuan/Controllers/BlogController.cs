using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DulichBinhThuan.Models;
using PagedList;
using PagedList.Mvc;

namespace DulichBinhThuan.Controllers
{
    public class BlogController : Controller
    {
        TravelDBDataContext db = new TravelDBDataContext();
        // GET: Tours
        public ActionResult Index(int? page)
        {
            int iSize = 5;
            int iPageNum = (page ?? 1);

            var listBlogs = db.Blogs.ToList();
            return View(listBlogs.ToPagedList(iPageNum, iSize));
        }
        public ActionResult BlogDetail(int id)
        {
            var blog = db.Blogs.SingleOrDefault(n => n.MaBlog == id);
            return View(blog);
        }
        public ActionResult OrderBlogs(int id) // write void random id
        {
            var listOrderBlogs = db.Blogs.Where(n => n.MaBlog != id).ToList();
            ViewBag.ErrorCode = "0";
            if (listOrderBlogs.Count == 0 || listOrderBlogs == null)
            {
                ViewBag.ErrorCode = "1";
            }
            return View(listOrderBlogs);
        }
    }
}