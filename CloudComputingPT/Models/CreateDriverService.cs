﻿using CloudComputingPT.Data;
using System.Linq;

namespace CloudComputingPT.Models
{
    public class CreateDriverService
    {
        ApplicationDbContext _applicationDBContext;
        public CreateDriverService(ApplicationDbContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public IQueryable<DriverService> _driverService { get; set; }
        public IQueryable<DriverService> driver_service()
        {
            var driverService = from a in _applicationDBContext.DriverServices
                                select a;

            _driverService = driverService;
            return _driverService;

        }
    }
}
