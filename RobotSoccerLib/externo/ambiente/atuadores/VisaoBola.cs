using Emgu.CV;
using Emgu.CV.Structure;
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
    public class VisaoBola : IVisao<Bitmap, InfoVtoEBola, PictureBox>
    {
        private InfoVtoEBola info;
        private Point modifica;
        private etc.Range range;
        private Image<Hsv, byte> imagemHsv;
        private Image<Gray, Byte> imagemGrayBola;
        private PictureBox pBox;


        public VisaoBola(etc.Range range)
        {
            this.range = range;
            info = new InfoVtoEBola();
            modifica = new Point();
        }


        public InfoVtoEBola processarImagem(Bitmap imagem)
        {

            imagemHsv = new Image<Hsv, byte>(imagem);
            imagemGrayBola = imagemHsv.InRange(range.Lowerrange, range.Upperrange);

            pBox.Image = imagemGrayBola.Resize(pBox.Width, pBox.Height, Emgu.CV.CvEnum.Inter.Linear).Bitmap;

            modifica.X = 3;
            modifica.Y = 3;
            info.Posicao = modifica;



            return info;
        }

        public void defineLugarDesenho(ref PictureBox place)
        {
            pBox = place;
            //imagemGrayBola.Bitmap = (Bitmap)place.Image;
        }
    }
}
