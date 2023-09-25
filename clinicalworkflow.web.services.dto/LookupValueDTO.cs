using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
    [Serializable]
    public partial class LookupValueDTO
    {
        public int LookupValue { get; set; }
        public string LookupText { get; set; }
        
    }
}
