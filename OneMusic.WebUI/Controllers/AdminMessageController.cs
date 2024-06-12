using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Controllers
{
    public class AdminMessageController(IMessageService _messageService) : Controller
    {
        public IActionResult Index()
        {
            var messages=_messageService.TGetList();
            return View(messages);
        }
        public IActionResult DeleteMessage(int id)
        {
            _messageService.TDelete(id);
            return RedirectToAction("Index");
        }
        public IActionResult ReadMessage(int id)
        {
            var message=_messageService.TGetById(id);
            return View(message);
        }
    }
}
