using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.externo.interfaces
{
    /// <summary>
    /// Captura imagens e deixa disponível 'para processamento
    /// </summary>
    /// <typeparam name="Img">Tipo de imagem a ser processado</typeparam>
    public interface IVideoRetriever<Img, PlaceToDraw>
    {
        /// <summary>
        /// Disponibiliza a ultima imagem a ser capturada para ser processada
        /// </summary>
        /// <returns></returns>
        //Img pegarImagem();

        void iniciaCaptura();

        void paraCaptura();

        void definePlaceToDraw(ref PlaceToDraw placeToDraw);

        event EventHandler<Img> imagemPega;
    }
}
