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

        public int AddPoint()=>score++;

        public int GetPoint()
        {
            return score;
        }

        public void ReinitScore() => score = 0;        

    }
}
