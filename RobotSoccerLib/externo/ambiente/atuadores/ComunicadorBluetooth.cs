using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.externo.ambiente.informacao;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.ambiente.atuadores
{
    public class ComunicadorBuetooth : IComunicacao<InfoEtoCRobo>
    {
        private SerialPort portaCom;

        public ComunicadorBuetooth(string portaCom)
        {
            this.portaCom = new SerialPort(portaCom);
        }

        public void conectar()
        {
            //if (!portaCom.IsOpen)
            //  portaCom.Open();
        }

        public void desconectar()
        {
            if (portaCom.IsOpen)
                portaCom.Close();
        }

        private string mensagem;
        public void enviarMensagem(InfoEtoCRobo informacao)
        {
            mensagem = protocolar(informacao.RodaEsquerda, informacao.RodaDireita);
            portaCom.Write(mensagem);
        }

        private string protocolar(int rodaEsquerda, int rodaDireita)
        {
            //Utiliza o sinal para determinar se é para frente ou para trás
            string dirE, dirD;

            dirD = rodaDireita > 0 ? "F" : "T";
            rodaDireita = rodaDireita > 255 ? 255 : rodaDireita < -255 ? -255 : rodaDireita;

            dirE = rodaEsquerda > 0 ? "F" : "T";
            rodaEsquerda = rodaEsquerda > 255 ? 255 : rodaEsquerda < -255 ? -255 : rodaEsquerda;

            return Math.Abs(rodaEsquerda).ToString().PadLeft(3, '0') + dirE +
                   Math.Abs(rodaDireita).ToString().PadLeft(3, '0') + dirD;
        }

        public void parar()
        {
            portaCom.Write("000F000F");
        }
    }
}
