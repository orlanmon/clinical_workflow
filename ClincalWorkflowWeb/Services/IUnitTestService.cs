using clinicalworkflow.web.services.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clincalworkflow.web.app.Services
{
    public interface IUnitTestService
    {
        public Task<PatientsClinicalDataDTO> GetPatient(int PatientID);
    }
}
