using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class ArtistDetailController(UserManager<AppUser> _userManager,IAlbumService _albumService) : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            var artist =await _userManager.FindByIdAsync(id.ToString());
            ViewBag.artist=artist.Name+" "+artist.Surname;
            var values = _albumService.TGetAlbumsByArtist(id);
            return View(values);
        }
    }
}
