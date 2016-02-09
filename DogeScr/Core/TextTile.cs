using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DogeScr.Core
{
    public class TextTile : TileBase
    {
        public TextTile() : base()
        {
            tileType = TileType.Text;
            fontSize = 36;
        }

        public string text { get; set; }
        public double fontSize { get; set; }
        public Color background { get; set; }
        public bool randomBackground { get; set; }
        public Color foreground { get; set; }
        public bool randomForeground { get; set; }

    }
}
