using RobotSoccerLib.externo.ambiente.atuadores;
using RobotSoccerLib.externo.ambiente.etc;
using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.externo.ambiente.informacao;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

namespace RobotSoccerLib.externo.controle
{
    public class SimpleVSSS
    {
        public const int GOL = 2;
        public const int GOL_ADVERSARIO = 3;
        public const int GRANDE_AREA = 4;
        public const int GRANDE_AREA_ADVERSARIO = 5;
        public const int MEIO_CAMPO = 6;
        public const int AREA_TOTAL = 1;

        public const string ATACANTE = "ATACANTE";
        public const string GOLEIRO = "GOLEIRO";
        public const string ZAGUEIRO = "ZAGUEIRO";

        private Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox> controle;

        public void setupCampo(Dictionary<int, Rectangle> paramCampo, ref PictureBox placeToDraw)
        {
            var vCampo = new VisaoCampo(
                paramCampo[GOL],
                paramCampo[GOL_ADVERSARIO],
                paramCampo[GRANDE_AREA],
                paramCampo[GRANDE_AREA_ADVERSARIO],
                paramCampo[MEIO_CAMPO],
                paramCampo[AREA_TOTAL]
            );
            controle.definirCampo(vCampo);
            controle.defineLugarDesenhoCampo(ref placeToDraw);
        }

        public void setupBola(Range paramBola, ref PictureBox placeToDraw)
        {
            var vBola = new VisaoBola(paramBola);
            controle.definirBola(vBola);
            controle.defineLugarDesenhoBola(ref placeToDraw);
        }

        /// <summary>
        /// Cria um robo pre definido segundo as constantes
        /// </summary>
        /// <param name="papel">Constantes: ATACANTE,GOLEIRO,ZAGUEIRO</param>
        /// <param name="rangeRobo">Range de Cor individual</param>
        /// <param name="rangeTime">Range de Cor do Time</param>
        /// <param name="portaCom">Porta serial de comunicação com Robô</param>
        /// <param name="placeToDraw">Local para desenhar imagem processada do robô</param>
        public void novoRoboBasico(string papel, Range rangeRobo, Range rangeTime, string portaCom, ref PictureBox placeToDraw)
        {
            VisaoRobo vRobo = null;
            EstrategiaBasica eBasica = null;
            ComunicadorBuetooth cBluetooth = null;
            switch (papel)
            {
                case ATACANTE:
                    vRobo = new VisaoRobo(rangeRobo, rangeTime);
                    eBasica = new EstrategiaBasica();
                    cBluetooth = new ComunicadorBuetooth(portaCom);
                    break;
                case ZAGUEIRO:
                    vRobo = new VisaoRobo(rangeRobo, rangeTime);
                    eBasica = new EstrategiaBasica();
                    cBluetooth = new ComunicadorBuetooth(portaCom);
                    break;
                case GOLEIRO:
                    vRobo = new VisaoRobo(rangeRobo, rangeTime);
                    eBasica = new EstrategiaBasica();
                    cBluetooth = new ComunicadorBuetooth(portaCom);
                    break;
            }
            if (vRobo != null)
            {
                controle.defineRobo(papel, vRobo, eBasica, cBluetooth);
                controle.defineLugarDesenhoRobo(papel, ref placeToDraw);
            }
        }


        /// <summary>
        /// Robô segue os pontos passados
        /// </summary>
        /// <param name="papel">Use constantes Atacante Goleiro e Zagueiro</param>
        /// <param name="rangeRobo">Cor individual</param>
        /// <param name="rangeTime">Cor Time</param>
        /// <param name="portaCom">Porta de comunicação</param>
        /// <param name="places">Conjunto de pontos a visitar</param>
        /// <param name="placeToDraw">Local para desenhar</param>
        public void novoRoboSeguePontos(string papel, Range rangeRobo, Range rangeTime, string portaCom, Point[] places, ref PictureBox placeToDraw)
        {
            VisaoRobo vRobo = null;
            EstrategiaSeguePontos eSeguePont = null;
            ComunicadorBuetooth cBluetooth = null;
            switch (papel)
            {
                case ATACANTE:
                    vRobo = new VisaoRobo(rangeRobo, rangeTime);
                    eSeguePont = new EstrategiaSeguePontos(places);
                    cBluetooth = new ComunicadorBuetooth(portaCom);
                    break;
                case ZAGUEIRO:
                    vRobo = new VisaoRobo(rangeRobo, rangeTime);
                    eSeguePont = new EstrategiaSeguePontos(places);
                    cBluetooth = new ComunicadorBuetooth(portaCom);
                    break;
                case GOLEIRO:
                    vRobo = new VisaoRobo(rangeRobo, rangeTime);
                    eSeguePont = new EstrategiaSeguePontos(places);
                    cBluetooth = new ComunicadorBuetooth(portaCom);
                    break;
            }
            if (vRobo != null)
            {
                controle.defineRobo(papel, vRobo, eSeguePont, cBluetooth);
                controle.defineLugarDesenhoRobo(papel, ref placeToDraw);
            }
        }

        public void defineCustomRobo(
            string id,
            IVisao<Bitmap, InfoVtoERobo, PictureBox> visao,
            IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo> estrategia,
            IComunicacao<InfoEtoCRobo> comunicacao,
            ref PictureBox placeToDraw)
        {
            //visao.defineLugarDesenho(ref placeToDraw);
            controle.defineRobo(id, visao, estrategia, comunicacao);
            if (placeToDraw != null)
                controle.defineLugarDesenhoRobo(id, ref placeToDraw);
        }

        public void removeRobo(string id)
        { controle.removeRobo(id); }

        public void vaiPraPonto(string id, int posX, int posY)
        { controle.defineRobo(id, null, new EstrategiaSeguePontos(new Point[] { new Point(posX, posY) }), null); }

        public void controleManual(string id, int velRodaD, int velRodaE)
        {
            InfoEtoCRobo info = new InfoEtoCRobo();
            info.RodaDireita = velRodaD;
            info.RodaEsquerda = velRodaE;
            try { controle.controleManual(id, info); } catch (Exception ex) { throw ex; }
        }

        public void iniciarPartida()
        { controle.iniciarPartida(); }

        public void pararPartida()
        { controle.pararPartida(); }

        public void exibirImagens(bool exibir)
        { controle.desenha(exibir); }

        public SimpleVSSS(int camId, ref PictureBox placeToDraw)
        {
            controle = new Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox>(new CapturaVideo(camId));
            controle.defineLugarDesenhoOriginal(ref placeToDraw);
            controle.iniciaCaptura();
        }
    }
}
