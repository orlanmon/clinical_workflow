using System;
using System.Collections.Generic;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
    public partial class PatientClinicalDataDTO
    {
        public int PatientClinicalDataId { get; set; }
        public int PatientId { get; set; }
        public string ClinicalDataFieldOne { get; set; }
        public string ClinicalDataFieldTwo { get; set; }
        public string ClinicalDataFieldThree { get; set; }
        public int ClinicalDataFieldFour { get; set; }
        public string ClinicalDataFieldFive { get; set; }
        public int ClinicalDataFieldSix { get; set; }
        public int ClinicalDataFieldSeven { get; set; }

    }
}
