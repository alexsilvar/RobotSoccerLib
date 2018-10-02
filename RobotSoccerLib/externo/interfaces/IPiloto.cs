using RobotSoccerLib.externo.controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.interfaces
{
    /// <summary>
    /// Interface que sugere o comportamento necessário para um Piloto da classe Controle
    /// Usado para definir as 
    /// </summary>
    /// <typeparam name="Img">Tipo de imagem usada no processamento</typeparam>
    /// <typeparam name="VtoERobo">Informação que vai da Visão para a Estrtégia</typeparam>
    /// <typeparam name="EtoCRobo">Estratégia para Controle</typeparam>
    /// <typeparam name="VtoEBola">Visão para Estratégia - Bola</typeparam>
    /// <typeparam name="VtoECampo">Visão para Estratégia - Campo</typeparam>
    /// <typeparam name="PlaceToDraw">Onde é desenhado a imagem - Ex.: PictureBox</typeparam>
    /// <typeparam name="TCampo">Parametro para definir campo - Ex.: ParamCampo</typeparam>
    /// <typeparam name="TBola">Parametro para definir bola - Ex.: ParamBola</typeparam>
    /// <typeparam name="TRobo">Parametro para Criar robô - Ex. ParamRobo</typeparam>
    /// <typeparam name="TCtrlMan">Parametro para controle manual de um Robô - Ex ParamCtrlMan</typeparam>
    /// <typeparam name="TControle">Parametro para criar o Controle - Backend do processo</typeparam>
    public interface IPiloto<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw, TCampo, TBola, TRobo, TCtrlMan, TControle>
    {

        /// <summary>
        /// Inicializa instancia do controle da maneira desejada
        /// </summary>
        /// <param name="paramControle">Parametros para Setup do Controle</param>
        /// <param name="placeToDraw">Local a desenhar Imagem Original Capturada</param>
        //void setupControle(TControle paramControle, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Define o campo no Controle
        /// </summary>
        /// <param name="paramCampo">Parametros para detectar o campo</param>
        /// <param name="placeToDraw">Local a desenhar imagens processadas</param>
        //void setupCampo(TCampo paramCampo, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Define a bola no Controle
        /// </summary>
        /// <param name="paramBola">Parametros de configuração da bola</param>
        /// <param name="placeToDraw">local a desenhar as imagens processadas</param>
        //void setupBola(TBola paramBola, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Cria um Robô no Controle
        /// </summary>
        /// <param name="paramRobo">Parametros para inicializar um robô</param>
        /// <param name="placeToDraw">Local a desenhar imagens processadas</param>
        //void setupRobo(TRobo paramRobo, ref PlaceToDraw placeToDraw);

        /// <summary>
        /// Executa o controle Manual
        /// </summary>
        /// <param name="paramCtrlMan">parametros de controle manual</param>
        //void controleManual(TCtrlMan paramCtrlMan);

        void setupRobo(string id,
            IVisao<Img, VtoERobo, PlaceToDraw> visao,
            IEstrategia<VtoERobo, EtoCRobo, VtoEBola, VtoECampo> estrategia,
            IComunicacao<EtoCRobo> comunicacao);
    }
}
