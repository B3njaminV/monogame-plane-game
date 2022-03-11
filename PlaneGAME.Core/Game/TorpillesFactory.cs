//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlaneGAME.Core.Game
//{
//    class TorpillesFactory
//    {
//        public List<Torpille> listeTorpilles = new List<Torpille>();
//        public int nombreTorpille;

//        public TorpillesFactory(Difficulte difficulte) {
//            listeTorpilles = null;
//            nombreTorpille = ChercheTorpille(difficulte);
//            fabrique(nombreTorpille);

//        }

//        public int ChercheTorpille(Difficulte difficulte)
//        {
//            switch (difficulte)
//            {
//                case Difficulte.Facile:
//                    return 2;
//                case Difficulte.Moyen:
//                    return 3;
//                case Difficulte.Difficile:
//                    return 4;
//                default:
//                    return 2;
//            }
       
//        }

//        public List<Torpille> fabrique(int nombre)
//        {
            
//        }
//    }
//}
