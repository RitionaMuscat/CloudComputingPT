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

        string bucketName = "bucket-cloud";
        public ConfirmedBookingController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager, IPubSubAccess pubSubAccess)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
            _pubSubAccess = pubSubAccess;
        }

      
        
        // GET: ConfirmedControllers
        public ActionResult Index()
        {
            CreateBookingDetails createBookingDetails = new CreateBookingDetails(_applicationDBContext);

            return View(createBookingDetails.GetBookingDetails());
        }
        public async Task<ActionResult> ReadEmail()
        {

            var result = await _pubSubAccess.ReadEmail();

            if (result != null)
            {
                string returnedResult = $"To: {result.MM.To},Body: {result.MM.Body}, AckId: {result.AckId}";
                //the above line can be replaced with sending out the actual email using some smtp server or mail gun api
                AcknowledgeEmails(result.AckId);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Content("no emails read");
            }
        }

        public ActionResult AcknowledgeMessage(string ackId)
        {
            _pubSubAccess.AcknowledgeMessage(ackId);

            return RedirectToAction("Index");
        }

        public void AcknowledgeEmails(string ackId)
        {
            AcknowledgeMessage(ackId);
        }


    }
}
