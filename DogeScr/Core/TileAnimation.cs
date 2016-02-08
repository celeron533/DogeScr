using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DogeScr.Core
{
    [Serializable]
    public class TileAnimation
    {
        [XmlElement]
        public int fadeInDuration { set; get; }
        [XmlElement]
        public int fadeOutDuration { set; get; }
        [XmlElement]
        public int stayDuration { set; get; }

        public TileAnimation() { }

        public TileAnimation(int fadeInDuration, int fadeOutDuration, int stayDuration)
        {
            this.fadeInDuration = fadeInDuration;
            this.fadeOutDuration = fadeOutDuration;
            this.stayDuration = stayDuration;
        }
    }
}
