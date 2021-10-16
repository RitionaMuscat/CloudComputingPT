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
            var current_User = _userManager.GetUserId(User);
            _logAccess.Log("Getting all Bookings");
            return View(bookingdetails.book_details(new Guid(current_User)));

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
                    if (details.isBookingConfirmed)
                    {
                        if (details.luxury && details.economy && !details.business)
                        {
                            ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                            _logAccess.Log("Cannot Choose Multiple Categories");
                        }
                        else if (details.luxury && !details.economy && details.business)
                        {
                            ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                            _logAccess.Log("Cannot Choose Multiple Categories");
                        }
                        else if (!details.luxury && details.economy && details.business)
                        {
                            ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                            _logAccess.Log("Cannot Choose Multiple Categories");
                        }
                        else if (details.luxury && details.economy && !details.business)
                        {
                            ModelState.AddModelError(string.Empty, "Only One Category Can Be Chosen");
                            _logAccess.Log("Cannot Choose Multiple Categories");
                        }
                        else if (!details.luxury && !details.economy && !details.business)
                        {
                            ModelState.AddModelError(string.Empty, "Choose 1 Category");
                            _logAccess.Log("Choose 1 Category");
                        }
                        
                        else
                        {
                            if(!details.isBookingConfirmed)
                            {
                                details.business = false;
                                details.luxury = false;
                                details.economy = false;
                            }
                            _applicationDBContext.BookingDetails.Add(details);
                            _applicationDBContext.SaveChanges();
                            _logAccess.Log("Saved Bookings");
                        }
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
            catch (Exception ex)
            {
                _logAccess.Log("Exception: " + ex.Message.ToString());
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
            if (User.IsInRole("Passenger") || User.IsInRole("Driver"))
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
                _logAccess.Log("Successfully Loaded Data To Edit");

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
                    var email = _userManager.FindByIdAsync(current_User).Result.Email;
                    bookingDetails.passengerId = new Guid(current_User);
                    if (User.IsInRole("Driver"))
                    {
                        bookingDetails.AcknowledgedService = true;
                        bookingDetails.DriverDetails = email;
                        ReadEmail();
                    }
                    _applicationDBContext.BookingDetails.Update(bookingDetails);
                    _applicationDBContext.SaveChanges();
                    _logAccess.Log("Updated Records");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logAccess.Log("Error while editing: " + ex.Message);
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
                                          a.business,
                                          a.passengerId

                                      }).ToList();

                MyMailMessage mm = new MyMailMessage();

                foreach (var item in bookingDetails)
                {
                    mm.Body = $"Residing Status {item.residingAdress} \n Destination Address: {item.destinationAddress}";
                    if (item.luxury && item.isBookingConfirmed)
                        mm.Body = mm.Body + $"\n Service Type: Luxury \n Booking Confirmed: Yes";
                    else if (item.economy && item.isBookingConfirmed)
                        mm.Body = mm.Body + $"\n Service Type: Economy \n Booking Confirmed: Yes";
                    else if (item.business && item.isBookingConfirmed)
                        mm.Body = mm.Body + $"\n Service Type: Business \n Booking Confirmed: Yes";
                    var _email = _userManager.FindByIdAsync(item.passengerId.ToString());
                    mm.To = _email.Result.Email;
                }
                await _pubSubAccess.PublishEmailAsync(mm);
                _logAccess.Log("Email Sent");

            }
            catch (Exception ex)
            {
                _logAccess.Log("Exception While Sending Email: " + ex.Message);
                Console.Write("EXCEPTION: ", ex.Message);
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> ReadEmail()
        {

            var result = await _pubSubAccess.ReadEmail();

            if (result != null)
            {
                string returnedResult = $"To: {result.MM.To},Body: {result.MM.Body}, AckId: {result.AckId}";
                //the above line can be replaced with sending out the actual email using some smtp server or mail gun api
               
                AcknowledgeEmails(result.AckId);
                _logAccess.Log("Reading Email");
                return Content(returnedResult);
  
            }
            else
            {
                _logAccess.Log("No emails read");
                return Content("no emails read");
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AcknowledgeMessage(string ackId)
        {
            _pubSubAccess.AcknowledgeMessage(ackId);
            _logAccess.Log("Acknowledging email");
            return RedirectToAction("Index");
        }

        public void AcknowledgeEmails(string ackId)
        {
            AcknowledgeMessage(ackId);
        }
    }
}
