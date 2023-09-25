using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicalworkflow.web.services.model.Models.DB;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using clinicalworkflow.web.services.dto;
using System.Text;

namespace clincalworkflow.web.app.Controllers
{
    public class PatientClinicalDatasController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _webAPIBaseURI;

        public PatientClinicalDatasController(IConfiguration configuration)
        {
            this._configuration = configuration;

            this._webAPIBaseURI = this._configuration.GetConnectionString("WebAPIBaseURI");

        }


        // GET: PatientClinicalDatas
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

            HttpResponseMessage response = await client.GetAsync("/api/PatientClinicalData/Get");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();

                if (!string.IsNullOrEmpty(content))
                {
                    var objPatientsClinicalDataDTO = JsonConvert.DeserializeObject<PatientsClinicalDataDTO>(content);

                    View("Index", objPatientsClinicalDataDTO.PatientClinicalDataDTOs);

                }
                else
                {
                    View("Index");

                }
            }

            return View("Index");

        }

        // GET: PatientClinicalDatas/Details/5
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

            HttpResponseMessage response = await client.GetAsync(string.Format("/api/PatientClinicalData/Get/{0}", id));

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    PatientClinicalDataDTO PatientClinicalDataDTO = JsonConvert.DeserializeObject<PatientClinicalDataDTO>(content);

                    ViewData["Patient_Name"] = string.Format("{0}, {1}", PatientClinicalDataDTO.Patient.LastName, PatientClinicalDataDTO.Patient.FirstName);

                    return View("Details", PatientClinicalDataDTO);
                }
            }

            return View("Index");

        }

        // GET: PatientClinicalDatas/Create
        
        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/PatientClinicalData/PatientLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);
                 
                    ViewData["PatientId"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText");

                }

            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldFourLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                    ViewData["ClinicalDataFieldFour"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText");

                }

            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldSixLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                    ViewData["ClinicalDataFieldSix"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText");

                }

            }

            return View();
        }
        

        // POST: PatientClinicalDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientClinicalDataId,PatientId,ClinicalDataFieldOne,ClinicalDataFieldTwo,ClinicalDataFieldThree,ClinicalDataFieldFour,ClinicalDataFieldFive,ClinicalDataFieldSix,ClinicalDataFieldSeven")] PatientClinicalDataDTO patientClinicalData)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {

                client.BaseAddress = new Uri(this._webAPIBaseURI);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent body = new StringContent(JsonConvert.SerializeObject(patientClinicalData), Encoding.UTF8, "application/json");

                response = client.PostAsync("api/PatientClinicalData/Create", body).Result;

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
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync("api/PatientClinicalData/PatientLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                    ViewData["PatientId"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText", patientClinicalData.PatientId);

                }

            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldFourLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                    ViewData["ClinicalDataFieldFour"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText", patientClinicalData.ClinicalDataFieldFour);

                }

            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldSixLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                    ViewData["ClinicalDataFieldSix"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText", patientClinicalData.ClinicalDataFieldSix);

                }

            }

            return View(patientClinicalData);
        }


        // GET: PatientClinicalDatas/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            PatientClinicalDataDTO objPatientClinicalDataDTO = null;


            if (id == null)
            {
                return NotFound();

            }


            

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync(string.Format("api/PatientClinicalData/Get/{0}", id));

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    objPatientClinicalDataDTO = JsonConvert.DeserializeObject<PatientClinicalDataDTO>(content);
                }
            }


            ViewData["Patient_Name"] = string.Format("{0}, {1}", objPatientClinicalDataDTO.Patient.LastName, objPatientClinicalDataDTO.Patient.FirstName);

            // Enumerator, Value Field, Text Field, Selected Value 

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldFourLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                    ViewData["ClinicalDataFieldFour"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText", objPatientClinicalDataDTO.ClinicalDataFieldFour);

                }

            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldSixLookup/");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                if (!string.IsNullOrEmpty(content))
                {
                    LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                    ViewData["ClinicalDataFieldSix"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText", objPatientClinicalDataDTO.ClinicalDataFieldSix);

                }

            }

            // ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "LastName", patientClinicalData.PatientId);

            return View("Edit", objPatientClinicalDataDTO);

           
        }
        


        // POST: PatientClinicalDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientClinicalDataId,PatientId,ClinicalDataFieldOne,ClinicalDataFieldTwo,ClinicalDataFieldThree,ClinicalDataFieldFour,ClinicalDataFieldFive,ClinicalDataFieldSix,ClinicalDataFieldSeven")] PatientClinicalDataDTO objPatientClinicalDataDTO)
        {
            HttpClient client = new HttpClient();
            PatientClinicalDataDTO objDeserializeObject = null;


            if (id != objPatientClinicalDataDTO.PatientClinicalDataId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

               
                client.BaseAddress = new Uri(this._webAPIBaseURI);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent body = new StringContent(JsonConvert.SerializeObject(objPatientClinicalDataDTO), Encoding.UTF8, "application/json");

                var response = client.PostAsync("api/PatientClinicalData/Update", body).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {

                        objDeserializeObject = JsonConvert.DeserializeObject<PatientClinicalDataDTO>(content);

                        Console.WriteLine("Data Saved Successfully.");

                        ViewData["Patient_Name"] = string.Format("{0}, {1}", objDeserializeObject.Patient.LastName, objDeserializeObject.Patient.FirstName);

                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldFourLookup/");

                        if (response.IsSuccessStatusCode)
                        {

                            content = await response.Content.ReadAsStringAsync();

                            JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                            if (!string.IsNullOrEmpty(content))
                            {
                                LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                                ViewData["ClinicalDataFieldFour"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText", objPatientClinicalDataDTO.ClinicalDataFieldFour);

                            }

                        }

                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        response = await client.GetAsync("api/PatientClinicalData/GetClinicalDataFieldSixLookup/");

                        if (response.IsSuccessStatusCode)
                        {

                            content = await response.Content.ReadAsStringAsync();

                            JsonSerializerSettings objJSONSettings = new JsonSerializerSettings();


                            if (!string.IsNullOrEmpty(content))
                            {
                                LookupValueDTOs objPatientLookup = JsonConvert.DeserializeObject<LookupValueDTOs>(content, objJSONSettings);

                                ViewData["ClinicalDataFieldSix"] = new SelectList(objPatientLookup.LookupValuesDTO, "LookupValue", "LookupText", objPatientClinicalDataDTO.ClinicalDataFieldSix);

                            }

                        }

                        return View("Edit", objDeserializeObject);

                    }
                }


            }

            return View("Edit", objDeserializeObject);

        }
        

        // GET: PatientClinicalDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            PatientClinicalDataDTO objPatientClinicalDataDTO = null;

            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(string.Format("api/PatientClinicalData/Get/{0}", id));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                   objPatientClinicalDataDTO = JsonConvert.DeserializeObject<PatientClinicalDataDTO>(content);       
                }
            }

            return View("Delete", objPatientClinicalDataDTO);

        }

        // POST: PatientClinicalDatas/Delete/5
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

            HttpResponseMessage response = await client.GetAsync(string.Format("api/PatientClinicalData/Delete/{0}", id));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            }

            return View("Delete");

            
        }

        private bool PatientClinicalDataExists(int id)
        {
            /*
            return _context.PatientClinicalData.Any(e => e.PatientClinicalDataId == id);
            */

            return false;
        }
    }
}
