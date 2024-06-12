using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AlbumDetailController : Controller
    {
        OneMusicContext _oneMusicContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISongService _songService;
        public AlbumDetailController(OneMusicContext oneMusicContext, UserManager<AppUser> userManager, ISongService songService)
        {
            _oneMusicContext = oneMusicContext;
            _userManager = userManager;
            _songService = songService;
        }

        public async Task<IActionResult> Index(int id)
        {

            var values =_oneMusicContext.Songs.Include(x=>x.Album).Where(x=>x.AlbumId==id).ToList();

            ViewBag.albumName = _oneMusicContext.Albums.Where(x => x.AlbumId == id).Select(x => x.AlbumName).First() ;

            return View(values);
        }
        public IActionResult Detail(int id)
        {
            var value = _oneMusicContext.Albums.Find(id);
            ViewBag.albumName = value.AlbumName;
            var values=_songService.TGetSongsWithAlbumByAlbumId(id);
            return View(values);
        }
    }
}
