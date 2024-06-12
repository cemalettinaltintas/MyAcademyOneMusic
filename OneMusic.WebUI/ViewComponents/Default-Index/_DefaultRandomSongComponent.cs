using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultRandomSongComponent : ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultRandomSongComponent(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
            var songs = _songService.TGetSongWithAlbum().ToList();
            var randomSong=songs.OrderBy(x=>Guid.NewGuid()).First();
            return View(randomSong);
        }
    }
}
