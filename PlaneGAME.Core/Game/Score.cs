using System;
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

    }
}
