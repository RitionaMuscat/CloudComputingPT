using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CloudComputingPT.Controllers
{
    public class PassengerController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private ICacheAccess _cacheAccess;
        private IPubSubAccess _pubSubAccess;
        private ILogAccess _logAccess;
        public PassengerController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager, IPubSubAccess pubSubAccess, ICacheAccess cacheAccess, ILogAccess logAccess)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
            _pubSubAccess = pubSubAccess;
            _cacheAccess = cacheAccess;
            _logAccess = logAccess;
        }
        // GET: PassengerController
        public ActionResult Index()
        {
            CreateBookingDetails bookingdetails = new CreateBookingDetails(_applicationDBContext);
            _logAccess.Log("Getting all Bookings");
            return View(bookingdetails.book_details());

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
                    if (details.luxury && details.economy && !details.business)
                    {
                        ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                    }
                    else if (details.luxury && !details.economy && details.business)
                    {
                        ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                    }
                    else if (!details.luxury && details.economy && details.business)
                    {
                        ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                    }
                    else if (details.luxury && details.economy && !details.business)
                    {
                        ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                    }
                    else
                    {
                        _applicationDBContext.BookingDetails.Add(details);
                        _applicationDBContext.SaveChanges();
                        _logAccess.Log("Saved Bookings");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logAccess.Log("Exception: "+ ex.Message.ToString());
                return View();
            }
        }

        // GET: PassengerController/Edit/5
        public ActionResult Edit(Guid id)
        {
            BookingDetails bookingDetailsToEdit = new BookingDetails();
            var getPassengerBooking = (from b in _applicationDBContext.BookingDetails
                                       where b.Id.Equals(id)
                                       select b).ToList();
            if (User.IsInRole("Passenger"))
            {
                foreach (var item in getPassengerBooking)
                {
                    bookingDetailsToEdit.business = item.business;
                    bookingDetailsToEdit.destinationAddress = item.destinationAddress;
                    bookingDetailsToEdit.economy = item.economy;
                    bookingDetailsToEdit.isBookingConfirmed = item.isBookingConfirmed;
                    bookingDetailsToEdit.luxury = item.luxury;
                    bookingDetailsToEdit.residingAdress = item.residingAdress;
                    bookingDetailsToEdit.Id = item.Id;
                    bookingDetailsToEdit.passengerId = item.passengerId;
                }
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
                var getPassengerBooking = (from b in _applicationDBContext.BookingDetails
                                           where b.Id.Equals(id)
                                           select b).ToList();

                if (User.Identity.IsAuthenticated)
                {
                    var current_User = _userManager.GetUserId(User);
                    bookingDetails.passengerId = new Guid(current_User);
                    _applicationDBContext.BookingDetails.Update(bookingDetails);
                    _applicationDBContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> SendEmail(Guid id)
        {
            try
            {
                var email = _userManager.GetUserName(User);
                var bookingDetails = (from a in _applicationDBContext.BookingDetails
                                      where a.Id.Equals(id)
                                      select new
                                      {
                                          a.Id,
                                          a.residingAdress,
                                          a.destinationAddress,
                                          a.luxury,
                                          a.isBookingConfirmed,
                                          a.economy,
                                          a.business

                                      }).ToList();
               MyMailMessage mm = new MyMailMessage();
                MyMailMessage _mm = new MyMailMessage();
                mm.To = email;
                //mm.From = new MailAddress("schooldemo21@gmail.com");
                foreach (var item in bookingDetails)
                {
                    //mm.Subject = $"Your Booking Details: {item.Id}";
                    mm.Body = $"Residing Status {item.residingAdress} \n Destination Address: {item.destinationAddress}";
                    if (item.luxury && item.isBookingConfirmed)
                        mm.Body = mm.Body + $"\n Service Type: Luxury \n Booking Confirmed: Yes";
                    else if (item.economy && item.isBookingConfirmed)
                        mm.Body = mm.Body + $"\n Service Type: Economy \n Booking Confirmed: Yes";
                    else if (item.business && item.isBookingConfirmed)
                        mm.Body = mm.Body + $"\n Service Type: Business \n Booking Confirmed: Yes";
                }
                await _pubSubAccess.PublishEmailAsync(mm);

                //_log.Log("Pushing a mail into a queue");

                //var d = DateTime.Now.ToShortTimeString();
                //MyMailMessage mm = new MyMailMessage
                //{
                //    Body = "This is a test body for pub sub " + d,
                //    To = "ritiona.muscat13@gmail.com"
                //};

                //await _pubSubAccess.PublishEmailAsync(mm);
            }
            catch (Exception ex )
            {
                Console.Write("EXCEPTION: ", ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
