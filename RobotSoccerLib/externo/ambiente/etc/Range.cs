using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.ambiente.etc
{
    public class Range
    {
        private Hsv lowerrange;
        public Hsv Lowerrange
        {
            get { return lowerrange; }
            set { lowerrange = value; }
        }
        private Hsv upperrange;
        public Hsv Upperrange
        {
            get { return upperrange; }
            set { upperrange = value; }
        }

        private int deltalow;
        public int Deltalow
        {
            get { return deltalow; }
            set { deltalow = value; }
        }
        private int deltahigh;
        public int Deltahigh
        {
            get { return deltahigh; }
            set { deltahigh = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="h">HUE</param>
        /// <param name="s">Saturação</param>
        /// <param name="v">Valor</param>
        /// <param name="deltalow">Variação inferior</param>
        /// <param name="deltahigh">Variação superior</param>
        public Range(double h, double s, double v, int deltalow = 30, int deltahigh = 30)
        {
            lowerrange = new Hsv(h - deltalow, s - deltalow, v - deltalow);
            upperrange = new Hsv(h + deltahigh, s + deltahigh, v + deltahigh);
        }
    }
}
