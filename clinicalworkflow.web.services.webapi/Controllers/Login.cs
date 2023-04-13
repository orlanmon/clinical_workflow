using clinicalworkflow.web.services.dto;
using clinicalworkflow.web.services.model.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinicalworkflow.web.services.webapi.Controllers
{


    [ApiController]
    //[Route("[controller]")]
    public class Login : ControllerBase
    {

        AutoMapper.IMapper _objAutoMapper = dto.AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

        private readonly ILogger<Login> _logger;
        private readonly IConfiguration _configuration;

        public Login(ILogger<Login> logger, IConfiguration configuration)
        {
            _logger = logger;

            _configuration = configuration;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("api/Login/Valid/{UserName}/{Password}")]
        public IActionResult Valid(string UserName, string Password)
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");


            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = objDB_Context_ClinicalWorkflow.UserLogins.Select(ul => ul.UserName == UserName && ul.UserPassword == Password);

            return new OkObjectResult(result);

        }

        [HttpGet]
        [Produces("application/json")]
        [Route("api/Login/Get/{UserName}/{Password}")]
        public IActionResult Get(string UserName, string Password)
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = (from login in objDB_Context_ClinicalWorkflow.UserLogins where login.UserName == UserName && login.UserPassword == Password select login).FirstOrDefault<UserLogin>();

            return new OkObjectResult(_objAutoMapper.Map<UserLogin, UserLoginDTO>(result));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("api/Login/GetAll")]
        public IActionResult GetAll()
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            DB_Context_ClinicalWorkflow objDB_Context_ClinicalWorkflow = new DB_Context_ClinicalWorkflow(connectionString);

            var result = (from login in objDB_Context_ClinicalWorkflow.UserLogins select login).ToList<UserLogin>();
            
            List<UserLoginDTO> objListUserLoginDTO = new List<UserLoginDTO>();

            foreach( UserLogin ul in result)
            {
                objListUserLoginDTO.Add(_objAutoMapper.Map<UserLogin, UserLoginDTO>(ul));
            }

            return new OkObjectResult(result);
        }

    }
}
