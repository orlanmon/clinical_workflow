using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
    [Serializable]
    public class PatientsDTO
    {
        public List<PatientDTO> PatientDTOs { get; set; }

    }
}
