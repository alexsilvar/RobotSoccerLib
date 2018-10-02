using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.ambiente.informacao
{
    public class InfoVtoECampo
    {
        public Rectangle Gol { get; set; }
        public Rectangle GolAdversario { get; set; }
        public Rectangle GrandeArea { get; set; }
        public Rectangle GrandeAreaAdversario { get; set; }
        public Rectangle MeioCampo { get; set; }
        public Rectangle AreaTotal { get; set; }
    }
}
