using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wedding_planner.Models;

namespace wedding_planner.Controllers {
    public class HomeController : Controller {
        private WeddingContext _context;

        public HomeController ([FromServices] WeddingContext context) {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            return View ();
        }

        [HttpGet]
        [Route ("dashboard")]
        public IActionResult Dashboard () {
            List<Wedding> Weddings = _context.weddings
                .Include (p => p.Users)
                .ThenInclude (s => s.User)
                .ToList ();
            ViewBag.session=HttpContext.Session.GetInt32("session_user");
            ViewBag.weddings=Weddings;
            ViewBag.flag=false;
            Console.WriteLine("*******************"+Weddings[1].Users[0].UserId);            
            return View ("Dashboard");
        }

        [HttpGet]
        [Route ("wedding")]
        public IActionResult Wedding () {
            return View ("Wedding");
        }

        [HttpGet]
        [Route ("logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }

        [HttpPost]
        [Route ("register")]
        public IActionResult Register (RegisterViews ruser) {
            if (ModelState.IsValid) {
                List<User> isuser = _context.users.Where (useri => useri.Email == ruser.Email).ToList ();
                if (isuser.Count () > 0) {
                    ViewBag.message = "There is already another user with this email!Please use other email!";

                    return View ("Index");
                }
                User NewUser = new User {
                    FirstName = ruser.FirstName,
                    LastName = ruser.LastName,
                    Email = ruser.Email,
                    Password = ruser.Password
                };
                _context.users.Add (NewUser);
                _context.SaveChanges ();
                List<User> user = _context.users.ToList ();
                int id = user[user.Count () - 1].UserId;
                HttpContext.Session.SetInt32 ("session_user", id);
                return RedirectToAction ("Dashboard");
            }
            return View ("Index");
        }

        [HttpPost]
        [Route ("login")]
        public IActionResult LoginProcess (string email, string password) {
            List<User> user = _context.users.Where (u => u.Email == email).ToList ();
            if (user.Count > 0) {
                if (user[0].Password == password) {
                    HttpContext.Session.SetInt32 ("session_user", user[0].UserId);
                    return RedirectToAction ("Dashboard");
                } else {
                    string error = "Password you entered does not match what we have in our records!";
                    ViewBag.err = error;
                    return View ("Index");
                }
            } else {
                ViewBag.err = "We could not find your email in our database!";
            }
            return View ("Index");
        }

        [HttpPost]
        [Route ("addwedding")]
        public IActionResult AddWedding (WeddingViews wedd) {
            if (ModelState.IsValid) {
                int? uid = HttpContext.Session.GetInt32 ("session_user");
                int id = Convert.ToInt32 (uid);
                Wedding wedding = new Wedding () {
                    WedderOne = wedd.WedderOne,
                    WedderTwo = wedd.WedderTwo,
                    Date = wedd.Date,
                    Address = wedd.Address,
                    UserId=id
                };
                _context.weddings.Add (wedding);
                _context.SaveChanges ();
                Wedding last = _context.weddings.ToList ().Last ();
                int wid = last.WeddingId;
                Join join = new Join () {
                    UserId = id,
                    WeddingId = wid
                };
                _context.joins.Add (join);
                _context.SaveChanges ();
                return RedirectToAction ("Dashboard");
            } else {
                return View ("Wedding");
            }
        }

        [HttpGet]
        [Route("thiswedding/{wedId}")]
        public IActionResult ThisWedding(int wedId){
            List<Wedding> Weddings = _context.weddings
                .Include (p => p.Users)
                .ThenInclude (s => s.User)
                .ToList ();
            Wedding wedding=Weddings.SingleOrDefault(w=>w.WeddingId==wedId);
            Console.WriteLine("***********"+wedding.Users.Count());
            ViewBag.wedding=wedding;
            return View("ThisWedding");
        }

        [HttpGet]
        [Route("delete/{wid}")]
        public IActionResult DeleteWedding(int wid){
            Wedding wedding=_context.weddings.SingleOrDefault(w=>w.WeddingId==wid);
            _context.weddings.Remove(wedding);
            List<Join> joins=_context.joins.Where(w=>w.WeddingId==wid).ToList();
            foreach(var join in joins){
                _context.joins.Remove(join);
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
            
        }

        [HttpGet]
        [Route("RSVP/{wid}")]
        public IActionResult JoinWedding(int wid){
            int? id=HttpContext.Session.GetInt32("session_user");
            int uid=Convert.ToInt32(id);
            Join join=new Join(){
                UserId=uid,
                WeddingId=wid
            };
            _context.joins.Add(join);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [HttpGet]
        [Route("unRSVP/{wid}")]
        public IActionResult LeaveWedding(int wid){
            int? id=HttpContext.Session.GetInt32("session_user");
            int uid=Convert.ToInt32(id);
            Join mjoins=_context.joins.SingleOrDefault(w=>w.WeddingId==wid && w.UserId==uid);
            _context.joins.Remove(mjoins);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}