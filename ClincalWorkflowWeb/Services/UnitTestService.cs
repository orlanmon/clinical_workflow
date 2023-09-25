using clinicalworkflow.web.services.dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace clincalworkflow.web.app.Services
{
    public class UnitTestService : IUnitTestService
    {

        private readonly IConfiguration _configuration = null;
        private readonly string _webAPIBaseURI;

        public UnitTestService(IConfiguration configuration)
        {
            this._configuration = configuration;

            this._webAPIBaseURI = this._configuration.GetConnectionString("WebAPIBaseURI");

        }

        public async Task<PatientsClinicalDataDTO> GetPatient(int PatientID)
        {

            HttpClient client = new HttpClient();
            PatientsClinicalDataDTO objPatient = null;

            client.BaseAddress = new Uri(this._webAPIBaseURI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(string.Format("/api/Patient/Get/{0}", PatientID));

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    objPatient = JsonConvert.DeserializeObject<PatientsClinicalDataDTO>(content);
                }
            }

            return objPatient;
        }
    }
}
