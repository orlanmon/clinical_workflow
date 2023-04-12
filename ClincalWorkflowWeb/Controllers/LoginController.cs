
using clinicalworkflow.web.services.dto;



using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClincalWorkflowWeb.Controllers
{
    public class LoginController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly string _webAPIBaseURI;


        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;

            this._webAPIBaseURI = this._configuration.GetConnectionString("WebAPIBaseURI");

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login([Bind("ID, UserName, UserPassword")] UserLoginDTO userLoginDTO)
        {
            UserLoginDTO objUserLoginDTO = null;
            HttpClient client = new HttpClient();

            if (!ModelState.IsValid)
            {
                return View("Index");
            }

          
            client.BaseAddress = new Uri(this._webAPIBaseURI);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = await client.GetAsync(string.Format("http://localhost:34269/api/Login/Get/{0}/{1}", userLoginDTO.UserName, userLoginDTO.UserPassword));
            HttpResponseMessage response = await client.GetAsync(string.Format("api/Login/Get/{0}/{1}", userLoginDTO.UserName, userLoginDTO.UserPassword));

            if (response.IsSuccessStatusCode)
            {
               
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                if ( !string.IsNullOrEmpty(content) )
                {
                    objUserLoginDTO = JsonSerializer.Deserialize<UserLoginDTO>(content, options);

                    System.Diagnostics.Debug.WriteLine(string.Format("User Name: {0} Password {1}", objUserLoginDTO.UserName, objUserLoginDTO.UserPassword));

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewData["LoginStatus"] = "Login was not successfull";

                }     
            }

            return View("Index");
        }
    }
}
