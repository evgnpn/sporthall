using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sporthall.WebUI.Areas.Mgmt.Controllers
{
    [Area("Mgmt")]
    public class ManagersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}