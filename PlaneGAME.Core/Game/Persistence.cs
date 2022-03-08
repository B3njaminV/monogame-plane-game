using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlaneGAME.Core.Game
{
    public class Persistence
    {
        public Score Load(string path)
        {
            Score saved = null;
            XmlSerializer x = new XmlSerializer(typeof(Score));
            using (FileStream st = File.Open(path, FileMode.Open))
            {
                saved = (Score)x.Deserialize(st);
            }
            return saved;
        }

        public void Save(string path, Score sc)
        {
            XmlSerializer x = new XmlSerializer(typeof(Score));
            using (FileStream st = File.Open(path, FileMode.Create))
            {
                x.Serialize(st, sc);
            }
        }
    }
}
