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
    class EstrategiaSeguePontos : IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo>
    {
        private Point destino;
        private InfoEtoCRobo info;
        private Point centroRobo;
        private Point frenteRobo;
        private int direcao;

        const int DIREITA = 0, ESQUERDA = 1, ALINHADO = 2;

        /// <summary>
        /// Com este construtor a estratégia faz o robô ia até determinado ponto
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public EstrategiaSeguePontos(Point[] pontos)
        {
            info = new InfoEtoCRobo();
            destino = pontos[0];
            centroRobo = new Point();
        }

        public InfoEtoCRobo executarEstrategia(InfoVtoERobo infoRobo, InfoVtoEBola infoBola, InfoVtoECampo infoCampo)
        {
            //destino = Point.Empty;
            if (destino != Point.Empty && infoRobo != null && infoRobo.PosicaoIndividual != Point.Empty && infoRobo.PosicaoTime != Point.Empty)
            {
                centroRobo.X = (infoRobo.PosicaoIndividual.X + infoRobo.PosicaoTime.X) / 2;
                centroRobo.Y = (infoRobo.PosicaoIndividual.Y + infoRobo.PosicaoTime.Y) / 2;

                if (distancia(centroRobo, destino) < 30)
                {
                    info.RodaDireita = 0;
                    info.RodaEsquerda = 0;
                    return info;
                }

                //Decide pra qual lado acelerar
                if (distancia(infoRobo.PosicaoIndividual, destino) < distancia(infoRobo.PosicaoTime, destino))
                { direcao = -1; frenteRobo = infoRobo.PosicaoIndividual; }
                else
                { direcao = 1; frenteRobo = infoRobo.PosicaoTime; }

                //Deslocando para a origem - para calcular o ângulo
                Point centro = new Point(0, 0);
                Point a = new Point(frenteRobo.X - centroRobo.X, frenteRobo.Y - frenteRobo.Y);
                Point b = new Point(destino.X - centroRobo.X, destino.Y - centroRobo.Y);

                //Calcula Angulo
                double theta = angulo(a, b);

                //Define o lado do ponto
                int lado = side(a, b);



                int rodaA = 100, rodaB;
                if (theta < Math.PI / 6)
                { rodaB = 20; }
                else if (theta < Math.PI / 3)
                { rodaB = 50; }
                else if (theta < Math.PI / 2.4)
                { rodaB = 80; }
                else
                { rodaB = 100; }

                rodaA = rodaA * direcao;
                rodaB = rodaB * direcao;

                if (lado == ESQUERDA)
                { info.RodaDireita = rodaA; info.RodaEsquerda = rodaB; }
                else if (lado == ESQUERDA)
                { info.RodaDireita = rodaB; info.RodaEsquerda = rodaA; }
                else
                { info.RodaDireita = info.RodaEsquerda = rodaA; }
            }

            return info;
        }

        /// <summary>
        /// Define se o ponto B está a direita ou esquerda da reta formada da origem até A
        /// </summary>
        /// <param name="a">Ponto A</param>
        /// <param name="b">Ponto B</param>
        /// <returns> 
        /// Considera-se a oritem como centro
        /// Use as constantes ESQUERDA, DIREITA e ALINHADO
        /// </returns>
        private int side(Point a, Point b)
        {
            //return (a.X - centro.X) * (b.Y - centro.Y) - (b.X - centro.X) * (a.Y - centro.Y);
            double dir = (a.X) * (b.Y) - (b.X) * (a.Y);
            return dir > 0 ? ESQUERDA : dir < 0 ? DIREITA : ALINHADO;
        }

        /// <summary>
        /// Distância entre dois pontos
        /// </summary>
        /// <param name="a">Ponto a</param>
        /// <param name="b">Ponto B</param>
        /// <returns>Distância entre os dois pontos usando como base as posições X e Y de cada ponto</returns>
        private int distancia(Point a, Point b)
        {
            return (int)Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }

        private double angulo(Point a, Point b)
        {
            return Math.Acos((a.X * b.X + a.Y * b.Y) /
                (Math.Sqrt(Math.Pow(a.X, 2) + Math.Pow(a.Y, 2)) * Math.Sqrt(Math.Pow(b.X, 2) + Math.Pow(b.Y, 2))));
        }
    }
}
