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
        const int DIREITA = 0, ESQUERDA = 1, ALINHADO = 2;

        private Point centroRobo;
        private Point frenteRobo;
        private Point destino;
        public EstrategiaBasica()
        {
            info = new InfoEtoCRobo();
        }


        public InfoEtoCRobo executarEstrategia(InfoVtoERobo infoRobo, InfoVtoEBola infoBola, InfoVtoECampo infoCampo)
        {

            try
            {
                destino = infoBola.Posicao;
                if (destino != Point.Empty && infoRobo != null && infoRobo.PosicaoIndividual != Point.Empty && infoRobo.PosicaoTime != Point.Empty)
                {
                    centroRobo.X = (infoRobo.PosicaoIndividual.X + infoRobo.PosicaoTime.X) / 2;
                    centroRobo.Y = ((480 - infoRobo.PosicaoIndividual.Y) + (480 - infoRobo.PosicaoTime.Y)) / 2;
                    if (distancia(centroRobo, destino) < 10)
                    {

                        info.RodaDireita = -555;
                        info.RodaEsquerda = -555;
                        return info;
                    }

                    //Registro de tempo
                    //Console.WriteLine(centroRobo + "\tTime:" + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

                    //Decide pra qual lado acelerar
                    /*if (distancia(new Point(infoRobo.PosicaoIndividual.X, 480 - infoRobo.PosicaoIndividual.Y), destino) <
                        distancia(new Point(infoRobo.PosicaoTime.X, 480 - infoRobo.PosicaoTime.Y), destino))
                    { direcao = 1; frenteRobo = infoRobo.PosicaoIndividual; }
                    else
                    { direcao = -1; frenteRobo = infoRobo.PosicaoTime; }*/

                    frenteRobo = infoRobo.PosicaoIndividual;
                    frenteRobo.Y = 480 - frenteRobo.Y;

                    //Deslocando para a origem - para calcular o ângulo
                    Point centro = new Point(0, 0);
                    Point a = new Point(frenteRobo.X - centroRobo.X, frenteRobo.Y - centroRobo.Y);
                    Point b = new Point(destino.X - centroRobo.X, destino.Y - centroRobo.Y);

                    //Calcula Angulo
                    double theta = angulo(a, b);
                    theta = theta * 180 / Math.PI;

                    //Velocidade do robô
                    //int dist;
                    //if (posicaoAnterior != Point.Empty)
                    //{
                    //  dist = distancia(centroRobo, posicaoAnterior);
                    //}
                    //else
                    //{
                    //  posicaoAnterior = centroRobo;
                    //}


                    if (theta > 20)
                    {
                        int distObjetivo = distancia(centroRobo, destino);
                        if (distObjetivo < 100)
                        {
                            if (side(a, b) == DIREITA)
                            {
                                info.RodaDireita = -50;
                                info.RodaEsquerda = 50;
                            }
                            else
                            {
                                info.RodaDireita = 50;
                                info.RodaEsquerda = -50;
                            }
                        }
                        else
                        {
                            if (theta > 60)
                            {
                                if (side(a, b) == DIREITA)
                                {
                                    info.RodaDireita = -50;
                                    info.RodaEsquerda = 50;
                                }
                                else
                                {
                                    info.RodaDireita = 50;
                                    info.RodaEsquerda = -50;
                                }
                            }
                        }


                    }
                    else
                    {

                        int distObjetivo = distancia(centroRobo, destino);
                        if (distObjetivo > 200)
                        {
                            info.RodaDireita = 150;
                            info.RodaEsquerda = 150;
                        }
                        else
                        {
                            info.RodaDireita = 70;
                            info.RodaEsquerda = 70;
                        }

                    }

                    //int[] vels = defineAceleracoes(centroRobo, (int)theta, destino);
                    //info.RodaDireita = (int)(vels[0] * 255.0 / 100.0) * -1;
                    //info.RodaEsquerda = (int)(vels[1] * 255.0 / 100.0) * -1;
                }
            }
            catch
            {
                info.RodaDireita = 0;
                info.RodaEsquerda = 0;
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
