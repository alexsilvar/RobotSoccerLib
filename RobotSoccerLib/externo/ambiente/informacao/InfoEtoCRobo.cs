using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.interno.ambiente.informacao
{
    public class InfoEtoCRobo
    {
        private int rodaDireita;
        private int rodaEsquerda;

        public int RodaDireita
        {
            get { return rodaDireita; }
            set { rodaDireita = value; }
        }

        public int RodaEsquerda
        {
            get { return rodaEsquerda; }
            set { rodaEsquerda = value; }
        }
    }
}
