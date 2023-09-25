using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicalworkflow.web.services.model.Models.DB;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using clinicalworkflow.web.services.dto;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace clincalworkflow.web.app.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _webAPIBaseURI;


        public PatientsController(IConfiguration configuration)
        {
            this._configuration = configuration;

            this._webAPIBaseURI = this._configuration.GetConnectionString("WebAPIBaseURI");

        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage response = await client.GetAsync("/api/Patient/Get");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();

              

                if (!string.IsNullOrEmpty(content))
                {
                    var objPatientsDTO = JsonConvert.DeserializeObject<PatientsDTO>(content);

                    View("Index", objPatientsDTO.PatientDTOs);


                }
                else
                {
                    View("Index");

                }
            }

            return View("Index");
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }


            if (id == null)
            {
                return NotFound();
            }


            HttpClient client = new HttpClient();

            
            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(string.Format("/api/Patient/Get/{0}", id));

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                
                if (!string.IsNullOrEmpty(content))
                {
                    PatientsClinicalDataDTO objPatient = JsonConvert.DeserializeObject<PatientsClinicalDataDTO>(content);

                    return View("Details", objPatient);
                }
            }

            return View("Index");
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,LastName,FirstName,AddressOne,City,State,Zip")] PatientsClinicalDataDTO patient)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");

            }

           
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent body = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

            var response = client.PostAsync("/api/Patient/Create", body).Result;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var objDeserializeObject = JsonConvert.DeserializeObject<PatientsClinicalDataDTO>(content);

                    Console.WriteLine("Data Saved Successfully.");

                    return RedirectToAction(nameof(Index));

                }
            }

            return View("Create");

        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
         
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(string.Format("/api/Patient/Get/{0}", id));

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    PatientDTO objPatient = JsonConvert.DeserializeObject<PatientDTO>(content);

                    return View("Edit", objPatient);
                }
            }

            return NotFound();
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,LastName,FirstName,AddressOne,City,State,Zip")] PatientDTO patient)
        {
            PatientDTO objDeserializeObject = null;


            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("Edit");

            }

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent body = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

            var response = client.PostAsync("/api/Patient/Update", body).Result;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    objDeserializeObject = JsonConvert.DeserializeObject<PatientDTO>(content);

                    Console.WriteLine("Data Saved Successfully.");

                    return View("Edit", objDeserializeObject);

                }
            }

            return View("Edit");

        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("Delete");
            }

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(string.Format("/api/Patient/Get{0}", id));

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                
                if (!string.IsNullOrEmpty(content))
                {
                    PatientDTO objPatient = JsonConvert.DeserializeObject<PatientDTO>(content);

                    return View("Delete", objPatient);
                }
            }
            return View("Delete");

        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (!ModelState.IsValid)
            {
                return View("Delete");
            }

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(string.Format("/api/Patient/Delete/{0}", id));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            }

            return View("Delete");
        }

        
    }
}
