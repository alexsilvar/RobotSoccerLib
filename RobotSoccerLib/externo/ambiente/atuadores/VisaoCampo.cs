using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.externo.ambiente.informacao;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSoccerLib.externo.ambiente.atuadores
{
    public class VisaoCampo : IVisao<Bitmap, InfoVtoECampo, PictureBox>
    {
        private InfoVtoECampo informacao;
        private PictureBox pbox;

        public VisaoCampo(Rectangle gol, Rectangle golAdversario, Rectangle grandeArea, Rectangle grandeAreaAdversario, Rectangle meioCampo, Rectangle areaTotal)
        {
            informacao = new InfoVtoECampo();

            informacao.Gol = gol;
            informacao.GolAdversario = golAdversario;
            informacao.GrandeArea = grandeArea;
            informacao.GrandeAreaAdversario = grandeAreaAdversario;
            informacao.MeioCampo = meioCampo;
            informacao.AreaTotal = areaTotal;
        }

        public void defineLugarDesenho(ref PictureBox place)
        {
            pbox = place;
        }

        public InfoVtoECampo processarImagem(Bitmap imagem)
        {
            return informacao;
        }

    }
}
