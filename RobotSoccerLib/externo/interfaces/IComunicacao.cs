using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.interfaces
{
    /// <summary>
    /// Executado ao enviar mensagens para cada robô
    /// </summary>
    /// <typeparam name="EtoC">Informação vinda da [E]stratégia para[to] [C]omunicação sem fio</typeparam>
    public interface IComunicacao<EtoC>
    {
        /// <summary>
        /// Protocola e Envia informação destinada ao robô
        /// </summary>
        /// <param name="informacao">Informação oriunda da estratégia</param>
        void enviarMensagem(EtoC informacao);

        /// <summary>
        /// Para Comunicações Orientados a conexão
        /// </summary>
        void conectar();

        /// <summary>
        /// Para Comunicações Orientados a conexão
        /// </summary>
        void desconectar();

        void parar();
    }
}
