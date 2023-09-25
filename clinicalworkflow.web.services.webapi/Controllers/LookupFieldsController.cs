using clinicalworkflow.web.services.model.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinicalworkflow.web.services.webapi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class LookupFieldsController : ControllerBase
    {

        private readonly IConfiguration _configuration = null;

        public LookupFieldsController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        [Produces("application/json")]
        [Route("api/Lookups/Get/DataFieldFour/")]
        public IActionResult GetDataFieldFourLookup(int id)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = (from pt in objDB_Context_ClinicalWorkflow.ClinicalDataFieldFourLookups select new { Text = pt.ClinicalDataFieldFourValue, Value = pt.ClinicalDataFieldFourId });

            return new OkObjectResult(result);
        }



    }
}
