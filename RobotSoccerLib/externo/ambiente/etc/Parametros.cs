using RobotSoccerLib.externo.ambiente.informacao;
using RobotSoccerLib.externo.interfaces;
using System.Drawing;
using System.Windows.Forms;

namespace RobotSoccerLib.externo.ambiente.etc
{

    public struct ParamControle
    {
        public int CamId { get; set; }
        public ParamControle(int camId)
        {
            CamId = camId;
        }
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

        public IVisao<Bitmap, InfoVtoERobo, PictureBox> VisaoRobo { get; set; }
        public IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo> EstrategiaRobo { get; set; }
        public IComunicacao<InfoEtoCRobo> ComunicacaoRobo { get; set; }

        public ParamRobo(string id, Range rangeTime, Range rangeIndividual, string portaCom)
        {
            Id = id;
            RangeIndividual = rangeIndividual;
            RangeTime = rangeTime;
            PortaCom = portaCom;
            VisaoRobo = null;
            EstrategiaRobo = null;
            ComunicacaoRobo = null;
        }
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
        public ParamBola(Range rangeBola)
        {
            RangeBola = rangeBola;
        }
    }
}
