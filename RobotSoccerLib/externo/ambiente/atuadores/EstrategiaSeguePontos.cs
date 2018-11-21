using Emgu.CV.Structure;
using RobotSoccerLib.externo.ambiente.informacao;
using RobotSoccerLib.externo.interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSoccerLib.externo.ambiente.atuadores
{
    public class EstrategiaSeguePontos : IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo>
    {
        private Point destino;
        private InfoEtoCRobo info;

        private Point centroRobo;
        private Point frenteRobo;
        private int direcao;

        private Point posicaoAnterior;
        const int DIREITA = 0, ESQUERDA = 1, ALINHADO = 2;
        private Queue<Point> destinos;

        /// <summary>
        /// Com este construtor a estratégia faz o robô ia até determinado ponto
        /// </summary>
        /// <param name="pontos">Pontos para seguir</param>
        public EstrategiaSeguePontos(Point[] pontos)
        {
            info = new InfoEtoCRobo();

            centroRobo = new Point();
            posicaoAnterior = Point.Empty;
            direcao = 2;

            //destino = new Point(pontos[0].X, 480 - pontos[0].Y);
            destinos = new Queue<Point>();
            foreach (var ponto in pontos)
            {
                destinos.Enqueue(new Point(ponto.X, 480 - ponto.Y));
            }
            destino = destinos.Dequeue();
        }

        public InfoEtoCRobo executarEstrategia(InfoVtoERobo infoRobo, InfoVtoEBola infoBola, InfoVtoECampo infoCampo)
        {
            //destino = Point.Empty;
            try
            {
                if (destino != Point.Empty && infoRobo != null && infoRobo.PosicaoIndividual != Point.Empty && infoRobo.PosicaoTime != Point.Empty)
                {
                    centroRobo.X = (infoRobo.PosicaoIndividual.X + infoRobo.PosicaoTime.X) / 2;
                    centroRobo.Y = ((480 - infoRobo.PosicaoIndividual.Y) + (480 - infoRobo.PosicaoTime.Y)) / 2;
                    if (distancia(centroRobo, destino) < 15)
                    {
                        if (destinos.Count > 0)
                        {
                            destino = destinos.Dequeue();
                        }

                        info.RodaDireita = -555;
                        info.RodaEsquerda = -555;
                        return info;
                    }

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
                        if (side(a, b) == DIREITA)
                        {
                            info.RodaDireita = -40;
                            info.RodaEsquerda = 40;
                        }
                        else
                        {
                            info.RodaDireita = 40;
                            info.RodaEsquerda = -40;
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
            //Console.WriteLine("Velocidades: " + info.RodaEsquerda + " , " + info.RodaDireita);
            Console.WriteLine("|");

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

        private int[] defineAceleracoes(Point posicao, int rotacao, Point destino)
        {
            double x = destino.X, y = destino.Y;
            int desired_angle = 0, theta_e = 0, vl, vr, vc = 70;

            double dx, dy, d_e, Ka = 10.0 / 90.0;
            dx = x - posicao.X;//robot->pos.x;
            dy = y - posicao.Y;//robot->pos.y;

            d_e = Math.Sqrt(dx * dx + dy * dy);
            if (dx == 0 && dy == 0)
                desired_angle = 90;
            else
                desired_angle = (int)(180.0 / Math.PI * Math.Atan2((double)(dy), (double)(dx)));
            theta_e = desired_angle - rotacao;//(int)robot->rotation;

            while (theta_e > 180) theta_e -= 360;
            while (theta_e < -180) theta_e += 360;

            if (d_e > 100.0)
                Ka = 17.0 / 90.0;
            else if (d_e > 50)
                Ka = 19.0 / 90.0;
            else if (d_e > 30)
                Ka = 21.0 / 90.0;
            else if (d_e > 20)
                Ka = 23.0 / 90.0;
            else
                Ka = 25.0 / 90.0;

            if (theta_e > 95 || theta_e < -95)
            {
                theta_e += 180;

                if (theta_e > 180)
                    theta_e -= 360;
                if (theta_e > 80)
                    theta_e = 80;
                if (theta_e < -80)
                    theta_e = -80;
                if (d_e < 5.0 && Math.Abs(theta_e) < 40)
                    Ka = 0.1;
                vr = (int)(-vc * (1.0 / (1.0 + Math.Exp(-3.0 * d_e)) - 0.3) + Ka * theta_e);
                vl = (int)(-vc * (1.0 / (1.0 + Math.Exp(-3.0 * d_e)) - 0.3) - Ka * theta_e);
            }

            else if (theta_e < 85 && theta_e > -85)
            {
                if (d_e < 5.0 && Math.Abs(theta_e) < 40)
                    Ka = 0.1;
                vr = (int)(vc * (1.0 / (1.0 + Math.Exp(-3.0 * d_e)) - 0.3) + Ka * theta_e);
                vl = (int)(vc * (1.0 / (1.0 + Math.Exp(-3.0 * d_e)) - 0.3) - Ka * theta_e);
            }

            else
            {
                vr = (int)(+.17 * theta_e);
                vl = (int)(-.17 * theta_e);
            }

            //Velocity(robot, vl, vr);
            return new int[] { vl, vr };
        }
    }
}
