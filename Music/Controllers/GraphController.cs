using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Music.Controllers
{
    public class GraphController : ApiController
    {
        private ItunesDataEntities db = new ItunesDataEntities();
        private int num = 15; //number of items to graph

        public dynamic getSongSongCount()
        {
            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Song == i.Song
                                     select k).Count()
                        let Item = i.Song
                        select new { Item, Count }).Distinct();
            var temp = (from i in list
                        orderby i.Count descending
                        select i).Take(num);
            return temp;
        }

        public dynamic getSongPlayCount()
        {

            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Song == i.Song
                                     select new { k.PlayCount }).Sum(s => s.PlayCount).Value
                        let Item = i.Song
                        select new { Item, Count }).Distinct();

            var ans = (from i in list
                       orderby i.Count descending
                       select i).Take(num);
            return ans;
        }

        public dynamic getAlbumSongCount()
        {
            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Album == i.Album
                                     select k).Count()
                        let Item = i.Album
                        select new { Item, Count }).Distinct();
            var temp = (from i in list
                        orderby i.Count descending
                        select i).Take(num);
            return temp;
        }

        public dynamic getAlbumPlayCount()
        {

            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Album == i.Album
                                     select new { k.PlayCount }).Sum(s => s.PlayCount).Value
                        let Item = i.Album
                        select new { Item, Count }).Distinct();

            var ans = (from i in list
                       orderby i.Count descending
                       select i).Take(num);
            return ans;
        }

        public dynamic getArtistSongCount()
        {
            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Artist == i.Artist
                                     select k).Count()
                        let Item = i.Artist
                        select new { Item, Count }).Distinct();
            var temp = (from i in list
                        orderby i.Count descending
                        select i).Take(num);
            return temp;
        }

        public dynamic getArtistPlayCount()
        {

            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                               where k.Artist == i.Artist
                                               select new { k.PlayCount }).Sum(s => s.PlayCount).Value
                        let Item = i.Artist
                        select new { Item, Count }).Distinct();

            var ans = (from i in list
                       orderby i.Count descending
                       select i).Take(num);
            return ans;
        }



        public dynamic getGenreSongCount()
        {
            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Genre == i.Genre
                                     select k).Count()
                        let Item = i.Genre
                        select new { Item, Count }).Distinct();

            var temp = (from i in list
                        orderby i.Count descending
                        select i).Take(num);
            return temp;
        }

        public dynamic getGenrePlayCount()
        {
            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Genre == i.Genre
                                     select new { k.PlayCount }).Sum(s => s.PlayCount).Value
                        let Item = i.Genre
                        select new { Item, Count }).Distinct();

            var ans = (from i in list
                       orderby i.Count descending
                       select i).Take(num);
            return ans;
        }

    }
}
