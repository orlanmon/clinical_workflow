using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
    [Serializable]
    public class PatientsClinicalDataDTO
    {
        public List<PatientClinicalDataDTO> PatientClinicalDataDTOs { get; set; }

    }
}
