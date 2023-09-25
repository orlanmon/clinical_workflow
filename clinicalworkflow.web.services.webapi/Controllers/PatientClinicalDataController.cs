using clinicalworkflow.web.services.dto;
using clinicalworkflow.web.services.model.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace clinicalworkflow.web.services.webapi.Controllers
{
    public class PatientClinicalDataController : Controller
    {

        private readonly IConfiguration _configuration = null;

        public PatientClinicalDataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        AutoMapper.IMapper _objAutoMapper = dto.AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

        [HttpGet]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/Get/{id}")]
        public IActionResult Get(int id)
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            //var result = (from pt in objDB_Context_ClinicalWorkflow.PatientClinicalData where pt.PatientClinicalDataId == id select pt).FirstOrDefault<PatientClinicalData>();
            var result = objDB_Context_ClinicalWorkflow.PatientClinicalData.Where(p => p.PatientClinicalDataId == id).Include(p => p.ClinicalDataFieldFourNavigation).Include(p => p.ClinicalDataFieldSixNavigation).Include(p => p.Patient).FirstOrDefault<PatientClinicalData>();

            if (result != null)
            {
                return new OkObjectResult(_objAutoMapper.Map<PatientClinicalData,  PatientClinicalDataDTO>(result));
            }

            return new OkObjectResult(_objAutoMapper.Map<PatientClinicalData, PatientClinicalDataDTO>(null));
        }
        //
        [HttpGet]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/Get/")]
        public IActionResult Get()
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            //var result = (from pt in objDB_Context_ClinicalWorkflow.PatientClinicalData select pt).ToList<PatientClinicalData>();

            var result = objDB_Context_ClinicalWorkflow.PatientClinicalData.Include(p => p.ClinicalDataFieldFourNavigation).Include(p => p.ClinicalDataFieldSixNavigation).Include(p => p.Patient).ToList<PatientClinicalData>();

            PatientsClinicalDataDTO objPatientsClinicalDataDTO = new PatientsClinicalDataDTO();
            objPatientsClinicalDataDTO.PatientClinicalDataDTOs = new List<PatientClinicalDataDTO>();

            if (result != null)
            {
                foreach (PatientClinicalData objPCD in result)
                {
                    objPatientsClinicalDataDTO.PatientClinicalDataDTOs.Add(_objAutoMapper.Map<PatientClinicalData, PatientClinicalDataDTO>(objPCD));
                }
            }

            return new OkObjectResult(objPatientsClinicalDataDTO);

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/Create")]
        public IActionResult Post([FromBody] PatientClinicalDataDTO patientClinicalDataDTO)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = objDB_Context_ClinicalWorkflow.PatientClinicalData.Add(_objAutoMapper.Map<PatientClinicalDataDTO, PatientClinicalData>(patientClinicalDataDTO)  );

            objDB_Context_ClinicalWorkflow.SaveChanges();

            return new OkObjectResult(_objAutoMapper.Map<PatientClinicalData, PatientClinicalDataDTO>(result.Entity));

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/Update")]
        public IActionResult Update([FromBody] PatientClinicalDataDTO patientClinicalDataDTO)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            PatientClinicalData objPatientClinicalData = _objAutoMapper.Map<PatientClinicalDataDTO, PatientClinicalData>(patientClinicalDataDTO);

            PatientClinicalData upatePatientClinicalData = objDB_Context_ClinicalWorkflow.PatientClinicalData.Single(pt => pt.PatientClinicalDataId == objPatientClinicalData.PatientClinicalDataId);

            upatePatientClinicalData.PatientId = objPatientClinicalData.PatientId;
            upatePatientClinicalData.ClinicalDataFieldOne = objPatientClinicalData.ClinicalDataFieldOne;
            upatePatientClinicalData.ClinicalDataFieldTwo = objPatientClinicalData.ClinicalDataFieldTwo;
            upatePatientClinicalData.ClinicalDataFieldThree = objPatientClinicalData.ClinicalDataFieldThree;
            upatePatientClinicalData.ClinicalDataFieldFour = objPatientClinicalData.ClinicalDataFieldFour;
            upatePatientClinicalData.ClinicalDataFieldFive = objPatientClinicalData.ClinicalDataFieldFive;
            upatePatientClinicalData.ClinicalDataFieldSix = objPatientClinicalData.ClinicalDataFieldSix;
            upatePatientClinicalData.ClinicalDataFieldSeven = objPatientClinicalData.ClinicalDataFieldSeven;

            objDB_Context_ClinicalWorkflow.SaveChanges();

            return new OkObjectResult(_objAutoMapper.Map<PatientClinicalData, PatientClinicalDataDTO>(objDB_Context_ClinicalWorkflow.PatientClinicalData.Where( pt => pt.PatientClinicalDataId == objPatientClinicalData.PatientClinicalDataId).Include(p => p.ClinicalDataFieldFourNavigation).Include(p => p.ClinicalDataFieldSixNavigation).Include(p => p.Patient).FirstOrDefault<PatientClinicalData>()));

        }


        [HttpGet]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            PatientClinicalData deletePatientClinicalData = objDB_Context_ClinicalWorkflow.PatientClinicalData.Single(pt => pt.PatientClinicalDataId == id);

            objDB_Context_ClinicalWorkflow.PatientClinicalData.Remove(deletePatientClinicalData);

            objDB_Context_ClinicalWorkflow.SaveChanges();

            return new OkObjectResult(true);

        }


        [HttpGet]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/GetClinicalDataFieldFourLookup/")]
        public IActionResult GetClinicalDataFieldFourLookup()
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = from cdflv in objDB_Context_ClinicalWorkflow.ClinicalDataFieldFourLookups select new LookupValueDTO { LookupValue = cdflv.ClinicalDataFieldFourId, LookupText = cdflv.ClinicalDataFieldFourValue };

            LookupValueDTOs objLookupValueDTOs = new LookupValueDTOs();

            objLookupValueDTOs.LookupValuesDTO = result.ToList<LookupValueDTO>();

            return new OkObjectResult(objLookupValueDTOs);

        }

        [HttpGet]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/GetClinicalDataFieldSixLookup/")]
        public IActionResult GetClinicalDataFieldSixLookup()
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = from cdflv in objDB_Context_ClinicalWorkflow.ClinicalDataFieldSixLookups select new LookupValueDTO  { LookupValue = cdflv.ClinicalDataFieldSixId, LookupText = cdflv.ClinicalDataFieldSixValue };

            LookupValueDTOs objLookupValueDTOs = new LookupValueDTOs();

            objLookupValueDTOs.LookupValuesDTO = result.ToList<LookupValueDTO>();

            return new OkObjectResult(objLookupValueDTOs);

        }

        [HttpGet]
        [Produces("application/json")]
        [Route("api/PatientClinicalData/PatientLookup/")]
        public IActionResult GetPatientLookup()
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = from cdflv in objDB_Context_ClinicalWorkflow.Patients select new LookupValueDTO { LookupValue = cdflv.PatientId, LookupText = cdflv.FirstName + ", " + cdflv.LastName };

            LookupValueDTOs objLookupValueDTOs = new LookupValueDTOs();

            objLookupValueDTOs.LookupValuesDTO = result.ToList<LookupValueDTO>();

            return new OkObjectResult(objLookupValueDTOs);

        }


    }
}
