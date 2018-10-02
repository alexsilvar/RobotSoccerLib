using RobotSoccerLib.interno.ambiente.etc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.ambiente.etc
{
    public struct ParamControle
    {
        public int CamId { get; set; }
    }
    public struct ParamCtrlMan
    {
        public string Id { get; set; }
        public int VelRodaD { get; set; }
        public int VelRodaE { get; set; }
    }
    public struct ParamRobo
    {
        public Range RangeIndividual { get; set; }
        public Range RangeTime { get; set; }
        public string PortaCom { get; set; }
        public string Id { get; set; }
    }
    public struct ParamCampo
    {

        public Rectangle Gol { get; set; }
        public Rectangle GolAdversario { get; set; }
        public Rectangle GrandeArea { get; set; }
        public Rectangle GrandeAreaAdversario { get; set; }
        public Rectangle MeioCampo { get; set; }
        public Rectangle AreaTotal { get; set; }
    }
    public struct ParamBola
    {
        public Range RangeBola { get; set; }
    }
}
