using Emgu.CV;
using Emgu.CV.Structure;
using RobotSoccerLib.externo.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSoccerLib.externo.ambiente.atuadores
{
    public class CapturaVideo : IVideoRetriever<Bitmap, PictureBox>
    {
        private VideoCapture camera;
        private Mat imagemCapturada;
        private bool captureInProgress;
        private PictureBox placeToDraw;
        private Bitmap capturaBitmap;

        public bool Desenha { get; set; } = true;

        public event EventHandler<Bitmap> imagemPega;

        //public event EventHandler imagemPega;

        public CapturaVideo(int cam)
        {
            CvInvoke.UseOpenCL = false;
            camera = new VideoCapture(cam);
            imagemCapturada = new Mat();
            camera.ImageGrabbed += ProcessFrame;
            captureInProgress = false;
        }



        private void ProcessFrame(object sender, EventArgs e)
        {
            if (camera != null && camera.Ptr != IntPtr.Zero)
            {
                camera.Retrieve(imagemCapturada);
                imagemPega?.Invoke(this, imagemCapturada.Bitmap);
                if (Desenha)
                {
                    capturaBitmap = imagemCapturada.ToImage<Bgr, byte>().ToBitmap(placeToDraw.Width, placeToDraw.Height);
                    placeToDraw.Image = capturaBitmap;
                }
            }
        }

        public void iniciaCaptura()
        {
            if (!captureInProgress)
            {
                camera.Start();
                captureInProgress = !captureInProgress;
            }
        }

        public void paraCaptura()
        {
            if (captureInProgress)
            {
                camera.Stop();
                captureInProgress = !captureInProgress;
            }
        }



        public void definePlaceToDraw(ref PictureBox placeToDraw)
        {
            this.placeToDraw = placeToDraw;
        }
    }
}
