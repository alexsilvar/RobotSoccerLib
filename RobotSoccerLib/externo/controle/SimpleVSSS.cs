

using RobotSoccerLib.externo.controle;
using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.interno.ambiente.atuadores;
using RobotSoccerLib.interno.ambiente.etc;
using RobotSoccerLib.interno.ambiente.informacao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        //Image<Bgr,byte>
        private Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox> controle;

        #region Atuadores
        //private VisaoCampo vCampo;
        //private VisaoBola vBola;

        #endregion


        public SimpleVSSS(int camId, ref PictureBox pBox)
        {
            controle = new Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox>(new CapturaVideo(camId));
            controle.defineLugarDesenhoOriginal(ref pBox);
            controle.iniciaCaptura();
        }



        public void setupCampo(Dictionary<int, Rectangle> areas, ref PictureBox pBoxRef)
        {
            var vCampo = new VisaoCampo(
                areas[GOL],
                areas[GOL_ADVERSARIO],
                areas[GRANDE_AREA],
                areas[GRANDE_AREA_ADVERSARIO],
                areas[MEIO_CAMPO],
                areas[AREA_TOTAL]
                );
            controle.definirCampo(vCampo, ref pBoxRef);
        }

        public void setupBola(Range rangeBola, ref PictureBox placeToDraw)
        {
            var vBola = new VisaoBola(rangeBola);

            controle.definirBola(vBola, ref placeToDraw);
        }

        public void novoRoboBasico(string id, Range rangeRobo, Range rangeTime, string portaCom, ref PictureBox placeToDraw)
        {
            VisaoRobo vRobo = new VisaoRobo(rangeRobo, rangeTime);
            EstrategiaBasica eBasica = new EstrategiaBasica();
            ComunicadorBuetooth cBluetooth = new ComunicadorBuetooth(portaCom);
            vRobo.defineLugarDesenho(ref placeToDraw);
            controle.novoRobo(id, vRobo, eBasica, cBluetooth);
        }

        public void controleManual(string id, int rodaEsquerda, int rodaDireita)
        {
            InfoEtoCRobo info = new InfoEtoCRobo();
            info.RodaDireita = rodaDireita;
            info.RodaEsquerda = rodaEsquerda;
            controle.controleManual(id, info);
        }

        public void ondeDesenharImgOriginal(ref PictureBox pbox)
        {
            //pboxRef = pbox;

        }

        public void ondeDesenharImgProcBola(ref PictureBox pboxBola)
        {
            //pboxRefBolaProc = pboxBola;
        }

        /*public static Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox> getInstanceControle()
        {
            Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox> controle;
            controle = new Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox>(new CapturaVideo(1));



            //controle.definirBola(new VisaoBola(new Range(1, 1, 1)));

            VisaoCampo campo = new VisaoCampo(
              new Rectangle(1, 1, 5, 5),
              new Rectangle(1, 1, 5, 5),
              new Rectangle(1, 1, 5, 5),
              new Rectangle(1, 1, 5, 5),
              new Rectangle(1, 1, 5, 5),
              new Rectangle(1, 1, 5, 5)
            );

            //controle.definirCampo(campo);

            Range rangeTime = new Range(22, 22, 22);

            Range rangeJohn = new Range(33, 33, 33);
            controle.novoRobo("JOHN", new VisaoRobo(rangeJohn, rangeTime), new EstrategiaBasica(), new ComunicadorBuetooth("COM5"));

            Range rangeMary = new Range(44, 44, 44);
            controle.novoRobo("MARY", new VisaoRobo(rangeMary, rangeTime), new EstrategiaBasica(), new ComunicadorBuetooth("COM4"));



            controle.executarProcedural();

            controle.pararExecucao();

            return controle;
        }*/


    }
}
