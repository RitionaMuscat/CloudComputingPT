using CloudComputingPT.Data;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CloudComputingPT.Controllers
{
    public class DriverController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DriverController(ApplicationDbContext applicationDBContext, UserManager<IdentityUser> userManager)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
        }
        // GET: DriverController
        public ActionResult Index()
        {
            CreateDriverService driverService = new CreateDriverService(_applicationDBContext);

            return View(driverService.driver_service());
        }

        // GET: DriverController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DriverController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverService driverService)
        {
            try
            {
                var loggedInUser = _userManager.GetUserId(User);
                
                driverService.driverId = new Guid( loggedInUser );
                
                _applicationDBContext.driverServices.Add(driverService);
                _applicationDBContext.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DriverController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
    }
}
