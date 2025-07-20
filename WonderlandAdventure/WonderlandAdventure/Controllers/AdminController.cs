using System.Web.Mvc;

namespace WonderlandAdventure.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Reports
        public ActionResult Reports()
        {
            return View();
        }

        // GET: Admin/Staff
        public ActionResult Staff()
        {
            return View();
        }

        // GET: Admin/Settings
        public ActionResult Settings()
        {
            return View();
        }

        // GET: Admin/Messages
        public ActionResult Messages()
        {
            return View();
        }
    }
}