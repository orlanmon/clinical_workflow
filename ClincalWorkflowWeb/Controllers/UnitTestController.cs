using clincalworkflow.web.app.Services;
using clinicalworkflow.web.services.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clincalworkflow.web.app.Controllers
{
    public class UnitTestController : Controller
    {
        
        private readonly IUnitTestService _unitTestService = null;

        public UnitTestController(IUnitTestService unitTestService)
        {
        
            _unitTestService = unitTestService;

        }

        public async Task<PatientsClinicalDataDTO> GetPatientByID(int PatientID)
        {
            return await _unitTestService.GetPatient(PatientID);

        }



    }
}
