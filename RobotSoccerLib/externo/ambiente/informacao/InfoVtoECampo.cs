using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.interno.ambiente.informacao
{
    public class InfoVtoECampo
    {
        private Rectangle gol;
        private Rectangle golAdversario;
        private Rectangle grandeArea;
        private Rectangle grandeAreaAdversario;
        private Rectangle meioCampo;
        private Rectangle areaTotal;


        public Rectangle Gol
        {
            get { return gol; }
            set { gol = value; }
        }

        public Rectangle GolAdversario
        {
            get { return golAdversario; }
            set { golAdversario = value; }
        }

        public Rectangle GrandeArea
        {
            get { return grandeArea; }
            set { grandeArea = value; }
        }

        public Rectangle GrandeAreaAdversario
        {
            get { return grandeAreaAdversario; }
            set { grandeAreaAdversario = value; }
        }

        public Rectangle MeioCampo
        {
            get { return meioCampo; }
            set { meioCampo = value; }
        }

        public Rectangle AreaTotal
        {
            get { return areaTotal; }
            set { areaTotal = value; }
        }
    }
}
