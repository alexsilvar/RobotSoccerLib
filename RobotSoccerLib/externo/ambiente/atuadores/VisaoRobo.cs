using Emgu.CV;
using Emgu.CV.Cvb;
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
    public class VisaoRobo : IVisao<Bitmap, InfoVtoERobo, PictureBox>
    {
        private etc.Range rangeIndividual;
        private etc.Range rangeTime;
        private InfoVtoERobo infoVtoERobo;
        private Image<Hsv, byte> imagemHsv;
        private Image<Gray, Byte> imagemGrayIndividual;
        private Image<Gray, Byte> imagemGrayTime;
        private PictureBox pBoxProcessado;

        public VisaoRobo(etc.Range rangeIndividual, etc.Range rangeTime)
        {
            this.rangeIndividual = rangeIndividual;
            this.rangeTime = rangeTime;
            infoVtoERobo = new InfoVtoERobo();
        }

        public InfoVtoERobo processarImagem(Bitmap imagem)
        {
            imagemHsv = new Image<Hsv, byte>(imagem);
            imagemHsv._SmoothGaussian(3, 3, 1, 1);
            CvInvoke.MedianBlur(imagemHsv, imagemHsv, 3);
            imagemGrayIndividual = imagemHsv.InRange(rangeIndividual.Lowerrange, rangeIndividual.Upperrange);
            //infoVtoERobo.PosicaoIndividual = centroideDeCorIndividual(imagemGrayIndividual);
            imagemGrayTime = imagemHsv.InRange(rangeIndividual.Lowerrange, rangeIndividual.Upperrange);
            //infoVtoERobo.PosicaoTime = centroideDeCorTime(imagemGrayTime, infoVtoERobo.PosicaoIndividual);

            pBoxProcessado.Image = imagemGrayIndividual.ToBitmap(pBoxProcessado.Width, pBoxProcessado.Height);

            return infoVtoERobo;
        }


        private CvBlobDetector blobDetector = new CvBlobDetector();
        private CvBlobs detectedBlobs = new CvBlobs();
        private List<CvBlob> blobList;
        private Point centro = new Point();
        private Point centroideDeCorIndividual(Image<Gray, byte> imgGray)
        {
            //USANDO BLOBS
            blobDetector.Detect(imgGray, detectedBlobs);
            blobList = detectedBlobs.Values.ToList();
            int posicao = filterSize(blobList);
            centro.X = (int)blobList[posicao].Centroid.X;
            centro.Y = (int)blobList[posicao].Centroid.Y;
            return centro;
        }

        private List<Point> time = new List<Point>();


        private Point centroideDeCorTime(Image<Gray, Byte> imgGray, Point individual)
        {
            blobDetector.Detect(imgGray, detectedBlobs);
            detectedBlobs.FilterByArea(300, 99999);
            blobList = detectedBlobs.Values.ToList();
            time.Clear();
            foreach (var blob in blobList)
            {
                time.Add(new Point((int)blob.Centroid.X, (int)blob.Centroid.Y));
            }
            return nearest(time, infoVtoERobo.PosicaoIndividual);
        }

        private Point nearest(List<Point> pontosTime, Point jogador)
        {
            int selected = 0;
            double maior = double.MaxValue;
            double atual;
            for (int i = 0; i < pontosTime.Count; i++)
            {
                atual = Math.Sqrt(Math.Pow((pontosTime[i].X - jogador.X), 2) + Math.Pow((pontosTime[i].Y - jogador.Y), 2));
                if (atual < maior)
                {
                    maior = atual;
                    selected = i;
                }
            }
            return pontosTime[selected];
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
            pBoxProcessado = place;
        }
    }
}
