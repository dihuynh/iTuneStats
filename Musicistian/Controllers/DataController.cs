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
using Musicistian.Models;

namespace Musicistian.Controllers
{
    public class DataController : ApiController
    {
        private ItunesDataEntities db = new ItunesDataEntities();


        public dynamic getAllArtists()
        {
            var list = (from i in db.MusicDatas
                        select i.Artist).Distinct();
            return list;
        }


        // GET api/Data
        public IEnumerable<MusicData> GetMusicDatas()
        {
            return db.MusicDatas.AsEnumerable();
        }

        // GET api/Data/5
        public MusicData GetMusicData(int id)
        {
            MusicData musicdata = db.MusicDatas.Find(id);
            if (musicdata == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return musicdata;
        }

        // PUT api/Data/5
        public HttpResponseMessage PutMusicData(int id, MusicData musicdata)
        {
            if (ModelState.IsValid && id == musicdata.ID)
            {
                db.Entry(musicdata).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Data
        public HttpResponseMessage PostMusicData(MusicData musicdata)
        {
            if (ModelState.IsValid)
            {
                db.MusicDatas.Add(musicdata);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, musicdata);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = musicdata.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Data/5
        public HttpResponseMessage DeleteMusicData(int id)
        {
            MusicData musicdata = db.MusicDatas.Find(id);
            if (musicdata == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.MusicDatas.Remove(musicdata);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, musicdata);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}