using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudComputingPT.Controllers
{
    public class DriverController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IPubSubAccess _pubSubAccess;
        private ILogAccess _logAccess;
        string bucketName = "bucket-cloud";
        public DriverController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager, IPubSubAccess pubSubAccess, ILogAccess logAccess)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
            _pubSubAccess = pubSubAccess;
            _logAccess = logAccess;
        }
        // GET: DriverController
        public ActionResult Index()
        {
            CreateDriverService driverService = new CreateDriverService(_applicationDBContext);
            var gStorage = StorageClient.Create();
            var getStorageObj = gStorage.ListObjects(bucketName);
            _logAccess.Log("Showing Driver Services");
            return View(driverService.driver_service());
        }

        // GET: DriverController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(DriverService driverService, IFormFile file)
        {
            try
            {
                string filePath = Directory.GetCurrentDirectory();

                var copyfile = System.IO.File.Create(filePath + @"\Files\" + file.FileName);
                file.CopyTo(copyfile);

                var gcsStorage = StorageClient.Create();

                copyfile.Close();
                var f = System.IO.File.OpenRead(filePath + @"\Files\" + file.FileName);

                string objectName = Path.GetFileName(filePath + @"\Files\" + file.FileName);
                driverService.Picture = @"https://storage.googleapis.com/" + bucketName + "/" + objectName;
                gcsStorage.UploadObject(bucketName, objectName, null, f);

                var loggedInUser = _userManager.GetUserId(User);

                driverService.driverId = new Guid(loggedInUser);

                _applicationDBContext.DriverServices.Add(driverService);
                _applicationDBContext.SaveChanges();
                _logAccess.Log("Added new Service");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                _logAccess.Log("Exception: "+ ex.Message);
                return View();
            }
        }


    }
}
