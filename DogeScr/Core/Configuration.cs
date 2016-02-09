using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DogeScr.Core
{
    [Serializable]
    [XmlInclude(typeof(TileBase))]
    [XmlInclude(typeof(ImageTile))]
    [XmlInclude(typeof(TextTile))]
    [XmlRoot]
    public class Configuration : PersistableObject
    {
        #region Universal Animation
        [XmlElement]
        public bool useUniversalAnimation { get; set; }

        [XmlElement]
        public TileAnimation universalAnimation { get; set; }
        #endregion

        [XmlArrayItem]
        public List<TileBase> tileList { get; set; }


        public void Reset()
        {
            this.tileList = new List<TileBase>();
            this.useUniversalAnimation = false;
            this.universalAnimation = new TileAnimation(100, 100, 2000);
        }

        public void CreateDefault()
        {
            Reset();

            ImageTile it = new ImageTile();
            it.imagePath = @"Resources/doge.png";
            it.imageSize = new System.Drawing.Size(120, 100);
            this.tileList.Add(it);

            TextTile tt = new TextTile();
            tt.text = "woo";
            tt.foreground = Colors.White;
            tt.randomBackground = true;
            tt.background = Colors.DarkGreen;
            tt.randomForeground = true;
            this.tileList.Add(tt);
        }
    }
}
