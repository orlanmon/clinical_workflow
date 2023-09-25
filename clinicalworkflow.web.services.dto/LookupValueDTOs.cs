using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
  
    [Serializable]
    public class LookupValueDTOs
    {
        public List<LookupValueDTO> LookupValuesDTO { get; set; }

    }

}
