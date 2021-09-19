using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CloudComputingPT.Controllers
{
    public class PassengerController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;

        private IPubSubAccess _pubSubAccess;
        public PassengerController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager, IPubSubAccess pubSubAccess)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
            _pubSubAccess = pubSubAccess;
        }
        // GET: PassengerController
        public ActionResult Index()
        {
            BookingDetails Det = new BookingDetails();
            CreateBookingDetails bookingdetails = new CreateBookingDetails(_applicationDBContext);
            return View(bookingdetails.book_details());

        }

        // GET: PassengerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PassengerController/Create
        public ActionResult Create()
        {
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
                    details.flatPrice = 1.25;
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
        public ActionResult Edit(Guid id)
        {
            BookingDetails bookingDetailsToEdit = new BookingDetails();
            var getPassengerBooking = (from b in _applicationDBContext.bookingDetails
                                       where b.Id.Equals(id)
                                       select b).ToList();
            foreach (var item in getPassengerBooking)
            {
                bookingDetailsToEdit.business = item.business;
                bookingDetailsToEdit.destinationAddress = item.destinationAddress;
                bookingDetailsToEdit.economy = item.economy;
                bookingDetailsToEdit.flatPrice = item.flatPrice;
                bookingDetailsToEdit.isBookingConfirmed = item.isBookingConfirmed;
                bookingDetailsToEdit.luxury = item.luxury;
                bookingDetailsToEdit.residingAdress = item.residingAdress;
                bookingDetailsToEdit.Id = item.Id;
                bookingDetailsToEdit.passengerId = item.passengerId;
            }
            return View(bookingDetailsToEdit);
        }

        // POST: PassengerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookingDetails bookingDetails)
        {
            try
            {
                var getPassengerBooking = (from b in _applicationDBContext.bookingDetails
                                           where b.Id.Equals(id)
                                           select b).ToList();

                if (User.Identity.IsAuthenticated)
                {
                    var current_User = _userManager.GetUserId(User);
                    bookingDetails.passengerId = new Guid(current_User);
                    _applicationDBContext.bookingDetails.Update(bookingDetails);
                    _applicationDBContext.SaveChanges();
                }

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

        public async Task<IActionResult> SendEmail()
        {
            MailMessage mm = new MailMessage();
            mm.To.Add("ritiona.muscat13@gmail.com");
            mm.From = new MailAddress("ritionamuscatdemo@gmail.com");

            mm.Subject = "Demo Test Pub Sub";
            mm.Body = "This is a test Body for pub sub";

            await _pubSubAccess.PublishEmailAsync(mm);
            await ReadEmail();
            return Content("done");
        }
        public async Task<ActionResult> ReadEmail()
        {
            var result = await _pubSubAccess.ReadEmail();

            if (result != null)
            {
                string returnedResult = $"To: {result.MM.To},Body: {result.MM.Body}, AckId: {result.AckId}";
                //the above line can be replaced with sending out the actual email using some smtp server or mail gun api
                return Content(returnedResult);
            }
            else
            {
                return Content("no emails read");
            }
        }
        public ActionResult AcknowledgeMessage(string ackId)
        {
            _pubSubAccess.AcknowledgeMessage(ackId);

            return Content("done");
        }
    }
}
