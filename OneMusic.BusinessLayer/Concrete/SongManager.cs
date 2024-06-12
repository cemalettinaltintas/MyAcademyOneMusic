using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Concrete
{
    public class SongManager : ISongService
    {
        private readonly ISongDal _songDal;
        public SongManager(ISongDal songDal)
        {
            _songDal = songDal;
        }

        public List<Song> TGetSongsWithAlbumByUserId(int userId)
        {
            return _songDal.GetSongsWithAlbumByUserId(userId);
        }

        public void TCreate(Song entity)
        {
            _songDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _songDal.Delete(id);
        }

        public Song TGetById(int id)
        {
            return _songDal.GetById(id);
        }

        public List<Song> TGetList()
        {
            return new List<Song>();
        }

        public List<Song> TGetSongsWithAlbumsAndArtist()
        {
            return _songDal.GetSongsWithAlbumsAndArtist();
        }

        public void TUpdate(Song entity)
        {
            _songDal.Update(entity);
        }

        public List<Song> TGetSongWithAlbum()
        {
            return _songDal.GetSongWithAlbum();
        }

        public List<Song> TGetSongsWithAlbumByAlbumId(int albumId)
        {
            return _songDal.GetSongsWithAlbumByAlbumId(albumId);
        }

    }
}
