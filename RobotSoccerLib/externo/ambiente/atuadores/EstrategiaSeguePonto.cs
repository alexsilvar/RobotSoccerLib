using Emgu.CV.Structure;
using RobotSoccerLib.externo.ambiente.informacao;
using RobotSoccerLib.externo.interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.ambiente.atuadores
{
    class EstrategiaSeguePonto : IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo>
    {
        private Point destino;
        private InfoEtoCRobo info;
        private Point centroRobo;
        private LineSegment2D centroFrente;
        private int direcao;


        /// <summary>
        /// Com este construtor a estratégia faz o robô ia até determinado ponto
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public EstrategiaSeguePonto(int posX, int posY)
        {
            info = new InfoEtoCRobo();
            destino = new Point(posX, posY);
            centroRobo = new Point();
            //centroObjetivo = new LineSegment2D();
            centroFrente = new LineSegment2D();
        }

        public InfoEtoCRobo executarEstrategia(InfoVtoERobo infoRobo, InfoVtoEBola infoBola, InfoVtoECampo infoCampo)
        {
            //destino = Point.Empty;
            if (destino != Point.Empty)
            {
                centroRobo.X = (infoRobo.PosicaoIndividual.X + infoRobo.PosicaoTime.X) / 2;
                centroRobo.Y = (infoRobo.PosicaoIndividual.Y + infoRobo.PosicaoTime.Y) / 2;

                //O centro sempre é o mesmo
                centroFrente.P1 = centroRobo;

                //Decide pra qual lado acelerar
                if (distancia(infoRobo.PosicaoIndividual, destino) < distancia(infoRobo.PosicaoTime, destino))
                { direcao = 1; }
                else
                { direcao = -1; }

                //centroObjetivo.P1 = centroRobo;
                //centroObjetivo.P2 = destino;

                if (centroFrente.Side(infoBola.Posicao) == -1)
                { info.RodaDireita = 100 * direcao; info.RodaEsquerda = 50 * direcao; }
                else
                { info.RodaDireita = 50 * direcao; info.RodaEsquerda = 100 * direcao; }

                //var x = (int)(180 / Math.PI * Math.Atan2((dy), (dx)));
            }

            return info;
        }

        private int distancia(Point a, Point b)
        {
            return (int)Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
    }
}
