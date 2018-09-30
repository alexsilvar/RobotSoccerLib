using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RobotSoccerLib.externo.interfaces
{
    /// <summary>
    /// Executa para cada elemento necessário a 
    /// </summary>
    /// <typeparam name="Img">Tipo de Imagem utilizada para processar</typeparam>
    /// <typeparam name="VtoE">Informacao da [V]isão para[t] [E]stratégia</typeparam>
    public interface IVisao<Img, VtoE, placeToDraw>
    {

        /// <summary>
        /// Cada elemento a ser processado retorna um objeto contendo as informações a ser interpretada pela estratégia
        /// </summary>
        /// <param name="imagem">Imagem Capturada pela Visão Computacional</param>
        /// <returns>Elemento processado a ser compreendido pela Estratégia</returns>
        VtoE processarImagem(Img imagem);

        /// <summary>
        /// Exporta as imagens processadas que o controle disponibilizará
        /// </summary>
        /// <returns>Imagens Processadas</returns>
        //Img[] imagensProcessadas();

        void defineLugarDesenho(ref placeToDraw place);

        //event EventHandler<Img[]> imagensProcessadas;

    }
}
