using RobotSoccerLib.externo.ambiente.atuadores;
using RobotSoccerLib.externo.ambiente.etc;
using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.externo.ambiente.informacao;
using System.Drawing;
using System.Windows.Forms;

namespace RobotSoccerLib.externo.controle
{
    public class SimpleVSSS : IPiloto<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox, ParamCampo, ParamBola, ParamRobo, ParamCtrlMan, ParamControle>
    {
        private Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox> controle;

        public void setupCampo(ParamCampo paramCampo, ref PictureBox placeToDraw)
        {
            var vCampo = new VisaoCampo(
                paramCampo.Gol,
                paramCampo.GolAdversario,
                paramCampo.GrandeArea,
                paramCampo.GrandeAreaAdversario,
                paramCampo.MeioCampo,
                paramCampo.AreaTotal
            );
            controle.definirCampo(vCampo, ref placeToDraw);
        }

        public void setupBola(ParamBola paramBola, ref PictureBox placeToDraw)
        {
            var vBola = new VisaoBola(paramBola.RangeBola);

            controle.definirBola(vBola, ref placeToDraw);
        }

        public void novoRobo(ParamRobo paramRobo, ref PictureBox placeToDraw)
        {
            VisaoRobo vRobo = new VisaoRobo(paramRobo.RangeIndividual, paramRobo.RangeTime);
            EstrategiaBasica eBasica = new EstrategiaBasica();
            ComunicadorBuetooth cBluetooth = new ComunicadorBuetooth(paramRobo.PortaCom);
            vRobo.defineLugarDesenho(ref placeToDraw);
            controle.novoRobo(paramRobo.Id, vRobo, eBasica, cBluetooth);
        }

        public void controleManual(ParamCtrlMan paramCtrlMan)
        {
            InfoEtoCRobo info = new InfoEtoCRobo();
            info.RodaDireita = paramCtrlMan.VelRodaD;
            info.RodaEsquerda = paramCtrlMan.VelRodaE;
            controle.controleManual(paramCtrlMan.Id, info);
        }

        public void setupControle(ParamControle paramControle, ref PictureBox placeToDraw)
        {
            controle = new Controle<Bitmap, InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo, PictureBox>(new CapturaVideo(paramControle.CamId));
            controle.defineLugarDesenhoOriginal(ref placeToDraw);
            controle.iniciaCaptura();
        }
    }
}
