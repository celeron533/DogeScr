using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DogeScr.Core
{
    public class TextTile : TileBase
    {
        public TextTile() : base()
        {
            tileType = TileType.Text;
            font = SystemFonts.DefaultFont;
        }

        public string text { get; set; }
        public Font font { get; set; }
        public Color backgroundColor { get; set; }
        public Color forgroundColor { get; set; }
    }
}
