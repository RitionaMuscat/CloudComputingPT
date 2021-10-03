using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputingPT.Controllers
{
    public class ConfirmedBookingController : Controller
    {
        private ApplicationDbContext _applicationDBContext;

        public ConfirmedBookingController(ApplicationDbContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;

        }
        // GET: ConfirmedControllers
        public ActionResult Index()
        {
            CreateBookingDetails createBookingDetails = new CreateBookingDetails(_applicationDBContext);

            return View(createBookingDetails.GetBookingDetails());
        }

    }
}
