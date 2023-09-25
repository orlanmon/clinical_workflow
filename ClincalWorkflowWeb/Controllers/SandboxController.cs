using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clincalworkflow.web.app.Controllers
{
    public class SandboxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
