using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DogeScr.Core
{
    public class ImageTile : TileBase
    {
        public ImageTile() : base()
        {
            tileType = TileType.Image;
        }

        public string imagePath { get; set; }

        public Size imageSize { get; set; }

    }
}
