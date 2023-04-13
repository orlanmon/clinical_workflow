using System;
using System.Collections.Generic;

#nullable disable

namespace clinicalworkflow.web.services.model.Models.DB
{
    public partial class Patient
    {
        public int PatientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string AddressOne { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
