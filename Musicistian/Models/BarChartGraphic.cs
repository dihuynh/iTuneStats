using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musicistian.Models
{
    public class BarChartGraphic
    {
        public string Artist { get; set; }
        public List<AlbumCountPair> AlbumPlayCount { get; set; }

        public BarChartGraphic()
        {
            AlbumPlayCount = new List<AlbumCountPair>();
        }
     }

    public class AlbumCountPair
    {
        public string Album { get; set; }
        public int PlayCount { get; set; }
    }
}