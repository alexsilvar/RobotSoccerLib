using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;


using RobotSoccerLib.interno.dados;
using RobotSoccerLib.externo.interfaces;
using System.Windows.Forms;

namespace RobotSoccerLib.externo.controle
{

    /// <summary>
    /// Controle de todo o processo e Enviorment Setup do Futebol de Robos
    /// </summary>
    /// <typeparam name="Img">Tipo de Imagem</typeparam>
    /// <typeparam name="VtoERobo">Informação da Visão para Estrategia</typeparam>
    /// <typeparam name="EtoCRobo">Informacao da Estratégia para Expedidor</typeparam>
    /// <typeparam name="VtoEBola">Informação da Visão para Estrategia</typeparam>
    /// <typeparam name="VtoECampo">Informação da Visão para Estrategia</typeparam>
    /// <typeparam name="PlaceToDraw">Informação da Visão para Estrategia</typeparam>
    public class Controle<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw>
    {

        public Controle(IVideoRetriever<Img, PlaceToDraw> capturaVideo) : this()
        {
            this.capturaVideo = capturaVideo;
        }

        private Controle()
        {
            robos = new Dictionary<string, Robo<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw>>();
            bola = new Bola<Img, VtoEBola, PlaceToDraw>();
            campo = new Campo<Img, VtoECampo, PlaceToDraw>();
        }

        #region Captura de Vídeo
        public void iniciaCaptura()
        {
            capturaVideo.iniciaCaptura();
            capturaVideo.imagemPega += novaImagem;
        }

        internal void defineLugarDesenhoOriginal(ref PlaceToDraw pBox)
        {
            capturaVideo.definePlaceToDraw(ref pBox);
        }

        public void paraCaptura()
        {
            capturaVideo.paraCaptura();
        }
        #endregion

        #region Ambiente
        private bool proceduraIsRunning;
        private Dictionary<string, Robo<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw>> robos;
        private Bola<Img, VtoEBola, PlaceToDraw> bola;
        private Campo<Img, VtoECampo, PlaceToDraw> campo;
        private IVideoRetriever<Img, PlaceToDraw> capturaVideo;


        public Img pegarImagemOriginal()
        {
            return imagem;
        }

        /// <summary>
        /// Cria um novo robô, também chama o método de conexão para estabelece-la
        /// </summary>
        /// <param name="id">Identificador único do robô</param>
        /// <param name="visao">Classe que processa Imagem</param>
        /// <param name="estrategia">Processa Estratégia</param>
        /// <param name="comunicacao">Comunicação sem fio</param>
        public void novoRobo(
            string id,
            IVisao<Img, VtoERobo, PlaceToDraw> visao,
            IEstrategia<VtoERobo, EtoCRobo, VtoEBola, VtoECampo> estrategia,
            IComunicacao<EtoCRobo> comunicacao)
        {
            foreach (var x in robos)
            {
                if (x.Value.Visao == visao || x.Value.Comunicacao == comunicacao || x.Value.Estrategia == estrategia)
                {
                    throw new Exception("Componentes de Visão, Estratégia e Expedidor devem ser Instâncias Distintas");
                }
            }
            var robo = new Robo<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw>(id, visao, estrategia, comunicacao);
            try
            {
                robos.Add(id, robo);
                robos[id].Comunicacao.conectar();
            }
            catch
            { throw new Exception("Robo de ID:" + id + " já existente, Robos já cadastrados são: \n" + robos.Keys); }
        }

        public void executarProcedural()
        {
            proceduraIsRunning = true;
        }

        /// <summary>
        /// Para a execução de jogo se estiver ocorrendo
        /// </summary>
        internal void pararExecucao()
        {
            proceduraIsRunning = false;
        }

        /// <summary>
        /// Para o robô e desconecta, então deleta
        /// </summary>
        /// <param name="id">Identificador do Robô</param>
        public void deletaRobo(string id)
        {
            robos[id].Comunicacao.parar();
            robos[id].Comunicacao.desconectar();
            var robo = robos.Remove(id);
        }

        public void definirCampo(IVisao<Img, VtoECampo, PlaceToDraw> visaoCampo, ref PlaceToDraw ptD)
        {
            campo.Visao = visaoCampo;
            campo.Visao.defineLugarDesenho(ref ptD);
        }

        public void definirBola(IVisao<Img, VtoEBola, PlaceToDraw> visaoBola, ref PlaceToDraw ptD)
        {
            bola.Visao = visaoBola;
            bola.Visao.defineLugarDesenho(ref ptD);
        }



        #endregion

        #region Processo
        private Img imagem;
        private VtoEBola infoBola;
        private VtoECampo infoCampo;



        private void novaImagem(object sender, Img e)
        {
            imagem = e;
            if (proceduraIsRunning)
            {
                infoBola = bola.Visao.processarImagem(imagem);
                infoCampo = campo.Visao.processarImagem(imagem);
                foreach (var robo in robos)
                {
                    robo.Value.visaoParaEstrategia(imagem);
                    robo.Value.estrategiaParaExpedidor(infoBola, infoCampo);
                    robo.Value.expedidorParaRobo();
                }
            }
            else
            {
                if (!EqualityComparer<Bola<Img, VtoEBola, PlaceToDraw>>.Default.Equals(bola, default(Bola<Img, VtoEBola, PlaceToDraw>)))
                    if (!EqualityComparer<Bola<Img, VtoEBola, PlaceToDraw>>.Default.Equals(bola, default(Bola<Img, VtoEBola, PlaceToDraw>)))
                        bola.Visao.processarImagem(imagem);

                if (!EqualityComparer<Campo<Img, VtoECampo, PlaceToDraw>>.Default.Equals(campo, default(Campo<Img, VtoECampo, PlaceToDraw>)))
                    if (!EqualityComparer<Campo<Img, VtoECampo, PlaceToDraw>>.Default.Equals(campo, default(Campo<Img, VtoECampo, PlaceToDraw>)))
                        bola.Visao.processarImagem(imagem);
                foreach (var robo in robos)
                {
                    robo.Value.visaoParaEstrategia(imagem);
                }
            }
        }


        public void controleManual(string id, EtoCRobo infoEtoCRobo)
        {
            if (!proceduraIsRunning)
                robos[id].controleManual(infoEtoCRobo);
            else
                throw new Exception("Jogo em execução, Não possível Executar controle Manual");
        }


        public void executarMultithread()
        { throw new NotImplementedException("Implementação Futura"); }
        #endregion
    }
}