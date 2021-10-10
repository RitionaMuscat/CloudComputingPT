using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CloudComputingPT.Controllers
{
    public class ConfirmedBookingController : Controller
    {
        private ApplicationDbContext _applicationDBContext;

        private readonly UserManager<IdentityUser> _userManager;
        private IPubSubAccess _pubSubAccess;
        private ILogAccess _logAccess;

        string bucketName = "bucket-cloud";
        public ConfirmedBookingController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager, IPubSubAccess pubSubAccess, ILogAccess logAccess)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
            _pubSubAccess = pubSubAccess;
            _logAccess = logAccess;
        }
        // GET: ConfirmedControllers
        public ActionResult Index()
        {
            CreateBookingDetails createBookingDetails = new CreateBookingDetails(_applicationDBContext);

            return View(createBookingDetails.GetBookingDetails());
        }



    }
}
