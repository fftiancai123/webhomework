using System.Web.Mvc;

namespace WonderlandAdventure.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitForm(string name, string contact, string feedback)
        {
            TempData["SuccessMessage"] = "Thank you for your message! We'll get back to you soon.";
            return RedirectToAction("Index");
        }
    }
}