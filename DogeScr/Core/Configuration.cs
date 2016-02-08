﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
        

        public void CreateDefault()
        {
            throw new NotImplementedException();
        }
    }
}