using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MySongController : Controller
    {
        private readonly ISongService _songService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumService _albumService;
        public MySongController(ISongService songService, UserManager<AppUser> userManager, IAlbumService albumService)
        {
            _songService = songService;
            _userManager = userManager;
            _albumService = albumService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _songService.TGetSongsWithAlbumByUserId(user.Id);
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateSong()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var albumList = _albumService.TGetAlbumsByArtist(user.Id);
            List<SelectListItem> albums = (from x in albumList
                                           select new SelectListItem
                                           {
                                               Text = x.AlbumName,
                                               Value = x.AlbumId.ToString()
                                           }).ToList();
            ViewBag.albums = albums;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(SongViewModel model)
        {
            var song = new Song()
            {
                SongName = model.SongName,
                AlbumId = model.AlbumId
            };
            if (model.SongFile != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(model.SongFile.FileName).ToLower();
                if (extension != ".mp3")
                {
                    // Desteklenmeyen dosya uzantısı hatası
                    ModelState.AddModelError("SongFile", "Sadece mp3 dosyaları kabul edilir.");
                    // Gerekirse, işlemi sonlandırabilirsiniz.
                    return View(model);
                }
                var songName = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/songs/" + songName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await model.SongFile.CopyToAsync(stream);
                song.SongUrl = "/songs/" + songName;
            }

            _songService.TCreate(song);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteSong(int id)
        {
            _songService.TDelete(id);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateSong(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var albumList = _albumService.TGetAlbumsByArtist(user.Id);
            List<SelectListItem> albums = (from x in albumList
                                           select new SelectListItem
                                           {
                                               Text = x.AlbumName,
                                               Value = x.AlbumId.ToString()
                                           }).ToList();
            ViewBag.albums = albums;

            var song = _songService.TGetById(id);
            var model = new SongViewModel()
            {
                SongId = song.SongId,
                SongName = song.SongName,
                SongUrl = song.SongUrl,
                AlbumId = song.AlbumId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSong(SongViewModel model)
        {

            var value = _songService.TGetById(model.SongId);
            value.SongName = model.SongName;
            value.AlbumId = model.AlbumId;
            if (model.SongFile != null)
            {
                var ex = Path.GetExtension(model.SongFile.FileName);
                var songname = Guid.NewGuid() + ex;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/songs/", songname);
                var stream = new FileStream(location, FileMode.Create);
                var songFileName= "/songs/" + songname;
                model.SongFile.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                value.SongUrl=songFileName;
            }


            _songService.TUpdate(value);

            return RedirectToAction("Index");
        }
    }
}


/*
 
         public static string CreateImage(IFormFile formFile, string path)
        {
            var ex = Path.GetExtension(formFile.FileName);
            var imageName = Guid.NewGuid() + ex;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/" + path + "/", imageName);
            var stream = new FileStream(location, FileMode.Create);
            formFile.CopyTo(stream);
            stream.Close();
            stream.Dispose();
            return "/Images/" + path + "/" + imageName;
        }

        public static void DeleteImage(string imagePath)
        {
            var location = "wwwroot" + imagePath;
            if (System.IO.File.Exists(location))
            {
                System.IO.File.Delete(location);
            }
        }

        public static string CreateSong(IFormFile formFile)
        {
            var ex = Path.GetExtension(formFile.FileName);
            var songname = Guid.NewGuid() + ex;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Songs/", songname);
            var stream = new FileStream(location, FileMode.Create);
            formFile.CopyTo(stream);
            stream.Close();
            stream.Dispose();
            return "/Songs/" + songname;
        }
 
 */