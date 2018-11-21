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
using Emgu.CV.Cvb;

namespace RobotSoccerLib.externo.ambiente.atuadores
{
    public class VisaoBola : IVisao<Bitmap, InfoVtoEBola, PictureBox>
    {
        private InfoVtoEBola info;
        //private Point modifica;
        private etc.Range range;
        private Image<Hsv, byte> imagemHsv;
        private Image<Gray, Byte> imagemGrayBola;
        private PictureBox pBox;
        public bool Desenhar { get; set; } = true;

        public VisaoBola(etc.Range range)
        {
            this.range = range;
            info = new InfoVtoEBola();
            //modifica = new Point();
        }


        public InfoVtoEBola processarImagem(Bitmap imagem)
        {

            imagemHsv = new Image<Hsv, byte>(imagem);

            imagemGrayBola = imagemHsv.InRange(range.Lowerrange, range.Upperrange);
            imagemGrayBola._SmoothGaussian(3, 3, 1, 1);
            imagemGrayBola._Erode(1);
            imagemGrayBola._Dilate(2);

            info.Posicao = detectaCentroide(imagemGrayBola);

            CvInvoke.PutText(imagemHsv, "(" + info.Posicao.X + "," + info.Posicao.Y + ")", info.Posicao, Emgu.CV.CvEnum.FontFace.HersheyPlain, 2, new MCvScalar());

            

            if (Desenhar)
                pBox.Image = /*imagemGrayBola*/imagemHsv.Resize(pBox.Width, pBox.Height, Emgu.CV.CvEnum.Inter.Linear).Bitmap;
            //info.Posicao = modifica;

            return info;
        }

        private CvBlobDetector blobDetector = new CvBlobDetector();
        private CvBlobs detectedBlobs = new CvBlobs();
        private List<CvBlob> blobList;
        private Point centro = new Point();
        private Point detectaCentroide(Image<Gray, byte> imgGray)
        {
            //USANDO BLOBS
            blobDetector.Detect(imgGray, detectedBlobs);
            blobList = detectedBlobs.Values.ToList();
            int posicao = filterSize(blobList);
            centro.X = (int)blobList[posicao].Centroid.X;
            centro.Y = (int)blobList[posicao].Centroid.Y;
            return centro;
        }

        private void definePonto(Image<Gray, byte> imagem)
        {

        }

        /// <summary>
        /// Seleciona o maior blob, ignorar ruidos
        /// </summary>
        /// <param name="blobs">Lista de blobs a detectar</param>
        /// <returns></returns>
        private int filterSize(List<CvBlob> blobs)
        {
            int area = blobs[0].Area;
            int loc = 0;
            for (int i = 1; i < blobs.Count; i++)
            {
                if (blobs[i].Area > area)
                {
                    area = blobs[i].Area;
                    loc = i;
                }
            }
            return loc;
        }

        public void defineLugarDesenho(ref PictureBox place)
        {
            pBox = place;
            //imagemGrayBola.Bitmap = (Bitmap)place.Image;
        }
    }
}
