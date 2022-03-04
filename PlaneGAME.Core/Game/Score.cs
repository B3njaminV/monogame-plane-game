using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlaneGAME.Core.Game
{
    [Serializable]
    public class Score
    {
        public int score;

        public Score()
        {
            score = 0;
        }

        public int addPoint()=>score++;

        public int getPoint()
        {
            return score;
        }

        public void reinitScore() => score = 0;

        /*public Score Load(string path)
        {
            Score saved = null;
            XmlSerializer x = new XmlSerializer(typeof(Score));
            using (FileStream st = File.Open(path, FileMode.Open))
            {
                saved = (Score)x.Deserialize(st);
            }
            return saved;
        }*/

        public void Save(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(Score));
            using (FileStream st = File.Open(path, FileMode.Create))
            {
                x.Serialize(st, this);
            }
        }

    }
}
