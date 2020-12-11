
using Microsoft.AspNetCore.Mvc;

namespace YiiBulbColor.Controllers
{
    public class YiiColorBulbController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("[controller]/api/[action]")]
        public string TestAjax()
        {
            return "{ Name: YiiColorBulb, Status: OK }";
        }
    }
}
