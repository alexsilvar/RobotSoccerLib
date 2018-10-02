using RobotSoccerLib.externo.ambiente.atuadores;
using RobotSoccerLib.externo.ambiente.etc;
using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.externo.ambiente.informacao;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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
            controle.definirCampo(vCampo, ref placeToDraw);
        }

        public void setupBola(Range paramBola, ref PictureBox placeToDraw)
        {
            var vBola = new VisaoBola(paramBola);

            controle.definirBola(vBola, ref placeToDraw);
        }

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
                vRobo.defineLugarDesenho(ref placeToDraw);
                controle.defineRobo(papel, vRobo, eBasica, cBluetooth);
            }
        }

        public void novoCustomRobo(string id, IVisao<Bitmap, InfoVtoERobo, PictureBox> visao,
            IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo> estrategia,
            IComunicacao<InfoEtoCRobo> comunicacao,
            ref PictureBox placeToDraw)
        {
            visao.defineLugarDesenho(ref placeToDraw);
            controle.defineRobo(id, visao, estrategia, comunicacao);
        }

        public void controleManual(string id, int velRodaD, int velRodaE)
        {
            InfoEtoCRobo info = new InfoEtoCRobo();
            info.RodaDireita = velRodaD;
            info.RodaEsquerda = velRodaE;
            controle.controleManual(id, info);
        }

        public SimpleVSSS(int camId, ref PictureBox placeToDraw)
        {
            controle = new Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox>(new CapturaVideo(camId));
            controle.defineLugarDesenhoOriginal(ref placeToDraw);
            controle.iniciaCaptura();
        }
    }
}
