using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Music.Models;

namespace Music.Controllers
{
    public class MusicController : ApiController
    {
        private ItunesDataEntities db = new ItunesDataEntities();


        public dynamic getPlayCount()
        {
            var list = (from i in db.ItunesTables
                        let count = (from k in db.ItunesTables
                                     where k.Album == i.Album && k.Artist == i.Artist
                                     select new { k.PlayCount }).Sum(s => s.PlayCount).Value
                        select new { i.Artist, i.Album, count }).Distinct();

            List<BarChartData> chart = new List<BarChartData>();
            string curArtist = "";
            string preArtist = "";
            BarChartData item = new BarChartData();
            AlbumCount pair = new AlbumCount();
            List<AlbumCount> pairList = new List<AlbumCount>();

            foreach (var i in list)
            {
                curArtist = i.Artist;
                Console.WriteLine(curArtist + " " + i.Album);

                pair = new AlbumCount { Album = i.Album, PlayCount = i.count };

                if (preArtist == "") //first time entering the loop
                {
                    item = new BarChartData { Artist = i.Artist };
                    pairList.Add(pair);
                }
                else if (curArtist == preArtist) //no need to make a new BarChartData
                {
                    pairList.Add(pair);
                }
                else // new artist, make a new BarChartData
                {
                    item.AlbumList = pairList;
                    chart.Add(item);
                    item = new BarChartData { Artist = i.Artist };
                    pairList = new List<AlbumCount>();
                    pairList.Add(pair);
                }

                preArtist = curArtist;
            }

            var temp = (from i in list
                        orderby i.count descending
                        select i);
            return temp;
        }

        /// <summary>
        /// Returns a pair of Artist and their number of songs
        /// </summary>
        /// <returns></returns>
        public dynamic getArtistCount()
        {
            var list = (from i in db.ItunesTables
                        let Count = (from k in db.ItunesTables
                                     where k.Artist == i.Artist
                                     select k).Count()
                        select new { i.Artist, Count }).Distinct();
            var temp = (from i in list
                        orderby i.Count descending
                        select i);
            return temp;
        }

        /// <summary>
        /// return a list of genres with respective number of songs
        /// </summary>
        /// <returns></returns>
        public dynamic getGenreCount()
        {

            var list = (from i in db.ItunesTables
                        let GenreCount = (from k in db.ItunesTables
                                          where k.Genre == i.Genre
                                          select k).Count()
                        select new { i.Genre, GenreCount }).Distinct();

            var temp = (from i in list
                        orderby i.GenreCount descending
                        select i);
            return temp;
        }

        /// <summary>
        /// returns a list of genres with respective number of playcount
        /// </summary>
        /// <returns></returns>
        public dynamic getGenrePlayCount()
        {

            var list = (from i in db.ItunesTables
                        let GenrePlayCount = (from k in db.ItunesTables
                                          where k.Genre == i.Genre
                                          select new { k.PlayCount }).Sum(s => s.PlayCount).Value
                        let GenreCount = (from k in db.ItunesTables
                                          where k.Genre == i.Genre
                                          select k).Count()
                        let Ratio = ((double)GenrePlayCount) / ((double)GenreCount)
                        select new { i.Genre, GenreCount, GenrePlayCount ,Ratio }).Distinct();

            var temp = (from i in list
                        orderby i.Ratio descending
                        select i);
            return temp;
        }
    }
}