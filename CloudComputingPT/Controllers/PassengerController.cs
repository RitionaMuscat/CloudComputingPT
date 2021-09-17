using CloudComputingPT.Data;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingPT.Controllers
{
    public class PassengerController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;

        public class InputModel
        {
             public string Name { get; set; }
        }
        public PassengerController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
        }
        // GET: PassengerController
        public ActionResult Index()
        {
            BookingDetails Det = new BookingDetails();

            var PassengerBookings = (from a in _applicationDBContext.bookingDetails
                                    select new
                                    {
                                    a.destinationAddress,
                                    a.isBookingConfirmed,
                                    a.residingAdress,
                                    a.passengerId,
                                    a.luxury,
                                    a.economy,
                                    a.business
                                    
                    
                    } ).ToList();



            foreach (var item in PassengerBookings)
            {


                Det.passengerId = item.passengerId;
                Det.residingAdress = item.residingAdress;
                Det.isBookingConfirmed = item.isBookingConfirmed;
                Det.isBookingConfirmed.ToString();
                Det.destinationAddress = item.destinationAddress;
             if (item.luxury)
                Det.luxury = item.luxury;
             else if (item.business)
                Det.business = item.business;
             else
                Det.economy = item.economy;
              




            }
           
            if (Det.Id != null)
                return View(Det);
            else
                return View();
        }

        // GET: PassengerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PassengerController/Create
        public ActionResult Create()
        {
            //ViewData["roles"] = _applicationDBContext.categories.ToList();


            return View();
        }

        // POST: PassengerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingDetails details)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var current_User = _userManager.GetUserId(User);
                    details.passengerId = new Guid(current_User);
                   
                  _applicationDBContext.bookingDetails.Add(details);
                    _applicationDBContext.SaveChanges();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PassengerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PassengerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PassengerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PassengerController/Delete/5
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
