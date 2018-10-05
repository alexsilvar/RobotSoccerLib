using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.externo.ambiente.informacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV.Structure;

namespace RobotSoccerLib.externo.ambiente.atuadores
{
    public class EstrategiaBasica : IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo>
    {
        private InfoEtoCRobo info;

        private Point centroRobo;
        public EstrategiaBasica()
        {
            info = new InfoEtoCRobo();
        }


        public InfoEtoCRobo executarEstrategia(InfoVtoERobo infoRobo, InfoVtoEBola infoBola, InfoVtoECampo infoCampo)
        {
            info.RodaDireita = 100;
            info.RodaEsquerda = -100;
            return info;
        }

        private int distancia(Point a, Point b)
        {
            return (int)Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
    }
}
