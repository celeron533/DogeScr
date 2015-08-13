using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DogeScr
{
    [Serializable]
    [XmlRoot]
    public class Configuration : PersistableObject
    {
        [XmlElement]
        public int fadeInDuration { set; get; }
        [XmlElement]
        public int fadeOutDuration { set; get; }
        [XmlElement]
        public int stayDuration { set; get; }

        [XmlArrayItem]
        public List<string> phraseList { set; get; }

        [XmlElement]
        public Font font { set; get; }


        public void Create()
        {
            throw new NotImplementedException();
        }
    }
}
