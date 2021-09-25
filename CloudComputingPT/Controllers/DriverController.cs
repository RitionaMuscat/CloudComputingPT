using CloudComputingPT.Data;
using CloudComputingPT.Models;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CloudComputingPT.Controllers
{
    public class DriverController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;
        string bucketName = "cloudcomputing_bucket2";
        public DriverController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
        }
        // GET: DriverController
        public ActionResult Index()
        {
            CreateDriverService driverService = new CreateDriverService(_applicationDBContext);
            var gStorage = StorageClient.Create();
            var getStorageObj = gStorage.ListObjects(bucketName);
            
            return View(driverService.driver_service());
        }

        // GET: DriverController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverService driverService, IFormFile file)
        {
            try
            {
            
                string filePath = Directory.GetCurrentDirectory();
                var copyfile = System.IO.File.Create(filePath + @"\Files\"+ file.FileName);
                file.CopyTo(copyfile);
               
                 var gcsStorage = StorageClient.Create();
                
                copyfile.Close();
                var f = System.IO.File.OpenRead(filePath + @"\Files\" + file.FileName);

                string objectName = Path.GetFileName(filePath + @"\Files\" + file.FileName);
                driverService.Picture = @"https://storage.googleapis.com/"+bucketName+"/"+objectName;
                gcsStorage.UploadObject(bucketName, objectName, null, f);

                var loggedInUser = _userManager.GetUserId(User);

                driverService.driverId = new Guid(loggedInUser);

              
                _applicationDBContext.driverServices.Add(driverService);
                _applicationDBContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return View();
            }
        }

        // POST: DriverController/Edit/5
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

        // GET: DriverController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DriverController/Delete/5
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

        public ActionResult AvailableServices()
        {

            CreateBookingDetails createBookingDetails = new CreateBookingDetails(_applicationDBContext);

            return View(createBookingDetails.GetBookingDetails());
        }
    }
}
