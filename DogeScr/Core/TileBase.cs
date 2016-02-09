using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DogeScr.Core
{
    [Serializable]
    public class TileBase
    {
        public TileType tileType { get; set; }

        public double opacity { get; set; }

        public TileAnimation animation { get; set; }

        public TileBase()
        {
            //set default values
            opacity = 1;
            animation = new TileAnimation(0, 0, 1000);
        }
    }

    public enum TileType
    {
        Image,
        Text
    }
}
