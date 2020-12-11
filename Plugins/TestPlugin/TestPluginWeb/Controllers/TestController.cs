using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("[controller]/api/[action]")]
        public string TestAjax()
        {
            return "{ Name: Test, Status: OK }";
        }
    }
}
