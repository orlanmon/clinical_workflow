using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
    [Serializable]
    public partial class PatientDTO
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]



        public string LastName { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Address One is required.")]
        public string AddressOne { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zip is required.")]
        public string Zip { get; set; }
    }
}
