using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Music.Models
{
    public class BarChartData
    {
        public string Artist { get; set; }
        public List<AlbumCount> AlbumList { get; set; }

        public BarChartData()
        {
            AlbumList = new List<AlbumCount>();
        }
    }

    public class AlbumCount
    {
        public string Album { get; set; }
        public double PlayCount { get; set; }

        public AlbumCount()
        {

        }
    }

}