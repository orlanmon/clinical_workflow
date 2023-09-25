using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
    public partial class PatientClinicalDataDTO
    {
        public int PatientClinicalDataId { get; set; }
        
        public int PatientId { get; set; }
        public virtual PatientDTO Patient { get; set; }

        [Display(Name = "ClinicalDataFieldOne")]
        public string ClinicalDataFieldOne { get; set; }
        public string ClinicalDataFieldTwo { get; set; }
        public string ClinicalDataFieldThree { get; set; }
        public int ClinicalDataFieldFour { get; set; }
        public string ClinicalDataFieldFive { get; set; }
        public int ClinicalDataFieldSix { get; set; }
        public int ClinicalDataFieldSeven { get; set; }

        public virtual ClinicalDataFieldFourLookupDTO ClinicalDataFieldFourNavigation { get; set; }
        public virtual ClinicalDataFieldSixLookupDTO ClinicalDataFieldSixNavigation { get; set; }

    }
}
