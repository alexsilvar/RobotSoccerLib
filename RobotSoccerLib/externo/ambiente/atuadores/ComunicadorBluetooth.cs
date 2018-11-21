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
            if (!portaCom.IsOpen)
                try { portaCom.Open(); } catch { }
        }

        public void desconectar()
        {
            if (portaCom.IsOpen)
                portaCom.Close();
        }

        private string mensagem;
        private string mensagemAntiga = null;
        public void enviarMensagem(InfoEtoCRobo informacao)
        {
            mensagem = protocolar(informacao.RodaEsquerda, informacao.RodaDireita);
            if (!portaCom.IsOpen)
                conectar();
            if (mensagemAntiga == null || !mensagem.Equals(mensagemAntiga))
            {
                portaCom.Write(mensagem);
                mensagemAntiga = mensagem;
            }

        }

        private string protocolar(int rodaEsquerda, int rodaDireita)
        {
            if (rodaEsquerda == -555 && rodaDireita == -555)
            {
                return "255S255S";
            }
            //Utiliza o sinal para determinar se é para frente ou para trás
            string dirE, dirD;

            //Decide direção
            dirD = rodaDireita > 0 ? "F" : "T";
            rodaDireita = rodaDireita > 255 ? 255 : rodaDireita < -255 ? -255 : rodaDireita;

            dirE = rodaEsquerda > 0 ? "F" : "T";
            rodaEsquerda = rodaEsquerda > 255 ? 255 : rodaEsquerda < -255 ? -255 : rodaEsquerda;

            //Formata para NNNXNNNX - X = F ou T N = numero
            return Math.Abs(rodaEsquerda).ToString().PadLeft(3, '0') + dirE +
                   Math.Abs(rodaDireita).ToString().PadLeft(3, '0') + dirD;
        }

        public void parar()
        {
            if (portaCom.IsOpen)
                portaCom.Write("000F000F");
        }
    }
}
