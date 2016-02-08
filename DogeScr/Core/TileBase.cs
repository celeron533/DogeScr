using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DogeScr.Core
{
    public class TileBase
    {
        public TileType tileType { get; set; }

        public double opacity { get; set; }

        public TileAnimation animation { get; set; }

        public TileBase()
        {
            opacity = 1;
            animation = new TileAnimation();
        }
    }

    public enum TileType
    {
        Image,
        Text
    }
}
