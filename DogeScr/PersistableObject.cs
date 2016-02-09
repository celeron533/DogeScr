using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DogeScr
{
    public class PersistableObject
    {
        public static T Load<T>(string fileName) where T : PersistableObject, new()
        {
            T result = default(T);

            using (FileStream stream = File.OpenRead(fileName))
            {
                result = new XmlSerializer(typeof(T)).Deserialize(stream) as T;
            }
            return result;
        }

        public void Save<T>(string fileName) where T : PersistableObject
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                new XmlSerializer(typeof(T)).Serialize(stream, this);
            }
        }
    }
}
