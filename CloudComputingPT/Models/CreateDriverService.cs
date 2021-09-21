using CloudComputingPT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var driverService = from a in _applicationDBContext.driverServices
                                 select a;

            _driverService = driverService;
            return _driverService;

        }
    }
}
