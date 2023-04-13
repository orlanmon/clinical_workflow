using System;
using System.Collections.Generic;

#nullable disable

namespace clinicalworkflow.web.services.model.Models.DB
{
    public partial class UserLogin
    {
        public int UserLoginId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
