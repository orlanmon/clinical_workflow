using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace clinicalworkflow.web.services.dto
{
    public partial class UserLoginDTO
    {
        
        public int UserLoginId { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User Password is required.")]
        public string UserPassword { get; set; }

    }
}
