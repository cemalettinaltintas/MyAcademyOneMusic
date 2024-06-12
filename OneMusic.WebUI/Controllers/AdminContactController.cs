using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminContactController(IContactService _contactService) : Controller
    {
        public IActionResult Index()
        {
            var values=_contactService.TGetList();
            return View(values);
        }
        public IActionResult DeleteContact(int id)
        {
            _contactService.TDelete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateContact(Contact contact)
        {
            _contactService.TCreate(contact);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult UpdateContact(int id)
        {
            var value=_contactService.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateContact(Contact contact)
        {
            _contactService.TUpdate(contact);
            return RedirectToAction("Index");
        }
    }
}
