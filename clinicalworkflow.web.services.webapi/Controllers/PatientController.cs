using clinicalworkflow.web.services.dto;
using clinicalworkflow.web.services.model.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clinicalworkflow.web.services.webapi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IConfiguration _configuration = null;

        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        AutoMapper.IMapper _objAutoMapper = dto.AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

        [HttpGet]
        [Produces("application/json")]
        [Route("api/Patient/Get/{id}")]
        public IActionResult Get(int id)
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = (from pt in objDB_Context_ClinicalWorkflow.Patients where pt.PatientId == id select pt).FirstOrDefault<Patient>();

            if(result != null)
            {
                return new OkObjectResult(_objAutoMapper.Map<Patient, PatientDTO>(result));
            }

            return new OkObjectResult(_objAutoMapper.Map<Patient, PatientDTO>(null));
        }
        //
        [HttpGet]
        [Produces("application/json")]
        [Route("api/Patient/Get/")]
        public IActionResult Get()
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = (from pt in objDB_Context_ClinicalWorkflow.Patients select pt).ToList<Patient>();

            PatientsDTO objPatientsDTO = new PatientsDTO();
            objPatientsDTO.PatientDTOs = new List<PatientDTO>();

            if (result != null)
            {
                foreach (Patient objPT in result)
                {
                    objPatientsDTO.PatientDTOs.Add(_objAutoMapper.Map<Patient, PatientDTO>(objPT));
                }
            }

            return new OkObjectResult(objPatientsDTO);

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/Patient/Create")]
        public IActionResult Post([FromBody] PatientDTO patientDTO)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = objDB_Context_ClinicalWorkflow.Patients.Add(_objAutoMapper.Map<PatientDTO, Patient>(patientDTO));

            objDB_Context_ClinicalWorkflow.SaveChanges();

            return new OkObjectResult(_objAutoMapper.Map<Patient, PatientDTO>(result.Entity));

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/Patient/Update")]
        public IActionResult Update([FromBody] PatientDTO patientDTO)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            Patient objPatient = _objAutoMapper.Map<PatientDTO, Patient>(patientDTO);

            Patient upatePatient = objDB_Context_ClinicalWorkflow.Patients.Single(pt => pt.PatientId == objPatient.PatientId);

            upatePatient.AddressOne = objPatient.AddressOne;
            upatePatient.FirstName = objPatient.FirstName;
            upatePatient.LastName = objPatient.LastName;
            upatePatient.Zip = objPatient.Zip;
            upatePatient.State = objPatient.State;
            upatePatient.City = objPatient.City;

            objDB_Context_ClinicalWorkflow.SaveChanges();

            return new OkObjectResult(_objAutoMapper.Map < Patient, PatientDTO >(objDB_Context_ClinicalWorkflow.Patients.Single(pt => pt.PatientId == objPatient.PatientId)));

        }

        
        [HttpGet]
        [Produces("application/json")]
        [Route("api/Patient/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            Patient deletePatient = objDB_Context_ClinicalWorkflow.Patients.Single(pt => pt.PatientId == id);
            
            objDB_Context_ClinicalWorkflow.Patients.Remove(deletePatient);

            objDB_Context_ClinicalWorkflow.SaveChanges();



            return new OkObjectResult(true);

        }
    }
}
