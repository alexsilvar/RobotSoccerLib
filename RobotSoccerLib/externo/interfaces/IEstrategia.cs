using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.interfaces
{


    /// <summary>
    /// Atuador Estratégia de um Robô
    /// </summary>
    /// <typeparam name="VtoERobo">Informação da [V]isão Computacional para[to] [E]stratégia - Robo</typeparam>
    /// <typeparam name="EtoCRobo">Informação da [E]stratégia para[to] o [C]omunicação sem Fio</typeparam>
    /// <typeparam name="VtoEBola">Informação da [V]isão Computacional para[to] [E]stratégia - Bola</typeparam>
    /// <typeparam name="VtoECampo">Informação da [V]isão Computacional para[to] [E]stratégia - Campo</typeparam>
    public interface IEstrategia<VtoERobo, EtoCRobo, VtoEBola, VtoECampo>
    {
        /// <summary>
        /// Executa o processamento definido para cada robô usando os pontos obtidos da visão computacional
        /// </summary>
        /// <param name="infoRobo">Dados oriundos da visão computacional (pontos chave)</param>
        /// <returns>Informações das velocidades de um robô</returns>


        /// <summary>
        /// Executa o processamento definido para cada robô usando os pontos obtidos da visão computacional
        /// </summary>
        /// <param name="infoRobo">Dados oriundos da visão computacional (pontos chave)</param>
        /// <param name="infoBola">Dados oriundos da visão computacional (pontos chave)</param>
        /// <param name="infoCampo">Dados oriundos da visão computacional (pontos chave)</param>
        /// <returns>Resultado do processamento da estratégia para o robô</returns>
        EtoCRobo executarEstrategia(VtoERobo infoRobo, VtoEBola infoBola, VtoECampo infoCampo);

    }
}
