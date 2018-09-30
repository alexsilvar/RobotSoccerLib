using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.interno.ambiente.informacao
{
    public class InfoVtoERobo
    {
        private Point posicaoIndividual;
        private Point posicaoTime;

        public Point PosicaoIndividual
        {
            get { return posicaoIndividual; }
            set { posicaoIndividual = value; }
        }

        public Point PosicaoTime
        {
            get { return posicaoTime; }
            set { posicaoTime = value; }
        }
    }
}
