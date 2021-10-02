using CloudComputingPT.Data;
using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingPT.Controllers
{
    public class ConfirmedBookingController : Controller
    {
        private ApplicationDbContext _applicationDBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IPubSubAccess _pubSubAccess;

        string bucketName = "cloudcomputing_bucket2";
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

    }
    }
