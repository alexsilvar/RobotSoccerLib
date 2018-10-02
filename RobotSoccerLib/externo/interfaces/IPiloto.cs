using RobotSoccerLib.externo.controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.interfaces
{
    public interface IPiloto<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw, TCampo, TBola, TRobo, TCtrlMan, TControle>
    {

        /// <summary>
        /// Inicializa instancia do controle da maneira desejada
        /// </summary>
        /// <param name="paramControle">Parametros para Setup do Controle</param>
        /// <param name="placeToDraw">Local a desenhar Imagem Original Capturada</param>
        void setupControle(TControle paramControle, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Define o campo no Controle
        /// </summary>
        /// <param name="paramCampo">Parametros para detectar o campo</param>
        /// <param name="placeToDraw">Local a desenhar imagens processadas</param>
        void setupCampo(TCampo paramCampo, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Define a bola no Controle
        /// </summary>
        /// <param name="paramBola">Parametros de configuração da bola</param>
        /// <param name="placeToDraw">local a desenhar as imagens processadas</param>
        void setupBola(TBola paramBola, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Cria um Robô no Controle
        /// </summary>
        /// <param name="paramRobo">Parametros para inicializar um robô</param>
        /// <param name="placeToDraw">Local a desenhar imagens processadas</param>
        void novoRobo(TRobo paramRobo, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Executa o controle Manual
        /// </summary>
        /// <param name="paramCtrlMan">parametros de controle manual</param>
        void controleManual(TCtrlMan paramCtrlMan);


    }
}
