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
        private Point destino;
        private Point centroRobo;
        public EstrategiaBasica()
        {
            info = new InfoEtoCRobo();
            destino = Point.Empty;
        }

        /// <summary>
        /// Com este construtor a estratégia faz o robô ia até determinado ponto
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public EstrategiaBasica(int posX, int posY)
        {
            info = new InfoEtoCRobo();
            destino = new Point(posX, posY);
            centroRobo = new Point();
            centroObjetivo = new LineSegment2D();
            centroFrente = new LineSegment2D();
        }

        private LineSegment2D centroObjetivo;
        private LineSegment2D centroFrente;
        private bool frente;

        public InfoEtoCRobo executarEstrategia(InfoVtoERobo infoRobo, InfoVtoEBola infoBola, InfoVtoECampo infoCampo)
        {
            if (destino != Point.Empty)
            {
                centroRobo.X = (infoRobo.PosicaoIndividual.X + infoRobo.PosicaoTime.X) / 2;
                centroRobo.Y = (infoRobo.PosicaoIndividual.Y + infoRobo.PosicaoTime.Y) / 2;

                //O centro sempre é o mesmo
                centroFrente.P1 = centroRobo;

                //Decide pra qual lado acelerar
                if (distancia(infoRobo.PosicaoIndividual, destino) < distancia(infoRobo.PosicaoTime, destino))
                { centroFrente.P2 = infoRobo.PosicaoIndividual; frente = true; }
                else
                { centroFrente.P2 = infoRobo.PosicaoTime; frente = false; }

                centroObjetivo.P1 = centroRobo;
                centroObjetivo.P2 = destino;
                //double x = centroObjetivo.GetExteriorAngleDegree(centroFrente);                

                //var x = (int)(180 / Math.PI * Math.Atan2((dy), (dx)));


            }
            info.RodaDireita = 255;
            info.RodaEsquerda = 255;

            return info;
        }

        private int distancia(Point a, Point b)
        {
            return (int)Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
    }
}
