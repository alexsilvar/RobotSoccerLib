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

        #region Configuracao de Ambiente
        private bool proceduraIsRunning;
        private Dictionary<string, Robo<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw>> robos;
        private Bola<Img, VtoEBola, PlaceToDraw> bola;
        private Campo<Img, VtoECampo, PlaceToDraw> campo;
        private IVideoRetriever<Img, PlaceToDraw> capturaVideo;



        /// <summary>
        /// Cria um novo robô/Atualiza suas propriedades, também chama o método de conexão para estabelece-la
        /// </summary>
        /// <param name="id">Identificador único do robô</param>
        /// <param name="visao">Classe que processa Imagem</param>
        /// <param name="estrategia">Processa Estratégia</param>
        /// <param name="comunicacao">Comunicação sem fio</param>
        public void defineRobo(
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
            lock (robos)
                if (robos.ContainsKey(id))
                {
                    if (visao != null) robos[id].Visao = visao;
                    if (estrategia != null) robos[id].Estrategia = estrategia;
                    if (comunicacao != null)
                    {
                        robos[id].Comunicacao.desconectar();
                        robos[id].Comunicacao = comunicacao;
                        robos[id].Comunicacao.conectar();
                    }
                }
                else
                {
                    robos.Add(id, robo);
                    robos[id].Comunicacao.conectar();
                }
        }

        public void defineLugarDesenhoRobo(string id, ref PlaceToDraw pToDraw)
        {
            if (robos[id].Visao != null)
                robos[id].Visao.defineLugarDesenho(ref pToDraw);
        }

        public void defineLugarDesenhoBola(ref PlaceToDraw pToDraw)
        {
            if (bola.Visao != null)
                bola.Visao.defineLugarDesenho(ref pToDraw);
        }

        public void defineLugarDesenhoCampo(ref PlaceToDraw pToDraw)
        {
            if (campo.Visao != null)
                campo.Visao.defineLugarDesenho(ref pToDraw);
        }

        public void iniciarPartida()
        {
            proceduraIsRunning = true;
        }

        /// <summary>
        /// Para a execução de jogo se estiver ocorrendo
        /// </summary>
        internal void pararPartida()
        {
            proceduraIsRunning = false;
            lock (robos)
                foreach (var robo in robos)
                {
                    robo.Value.Comunicacao.parar();
                }
        }

        /// <summary>
        /// Para o robô e desconecta, então deleta
        /// </summary>
        /// <param name="id">Identificador do Robô</param>
        public void removeRobo(string id)
        {
            lock (robos)
                try
                {
                    robos[id].Comunicacao.parar();
                    robos[id].Comunicacao.desconectar();
                    var robo = robos.Remove(id);
                }
                catch { }
        }

        public void definirCampo(IVisao<Img, VtoECampo, PlaceToDraw> visaoCampo)
        {
            campo.Visao = visaoCampo;
        }

        public void definirBola(IVisao<Img, VtoEBola, PlaceToDraw> visaoBola)
        {
            bola.Visao = visaoBola;
        }
        #endregion

        #region Processo
        private Img imagem;
        private VtoEBola infoBola;
        private VtoECampo infoCampo;


        public void desenha(bool desenhar)
        {
            capturaVideo.Desenha = desenhar;
            lock (bola)
                if (bola.Visao != null)
                    bola.Visao.Desenhar = desenhar;
            lock (campo)
                if (campo.Visao != null)
                    campo.Visao.Desenhar = desenhar;
            lock (robos)
                foreach (var robo in robos)
                {
                    if (robo.Value.Visao != null)
                        robo.Value.Visao.Desenhar = desenhar;
                }
        }

        private void novaImagem(object sender, Img e)
        {
            imagem = e;
            if (proceduraIsRunning)
            {
                if (bola != null && bola.Visao != null)
                    infoBola = bola.Visao.processarImagem(imagem);
                if (campo != null && campo.Visao != null)
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
                if (bola.Visao != null)
                    lock (bola)
                        bola.Visao.processarImagem(imagem);
                if (campo.Visao != null)
                    lock (campo)
                        campo.Visao.processarImagem(imagem);
                lock (robos)
                    foreach (var robo in robos)
                    {
                        try
                        {
                            if (robo.Value != null)
                                robo.Value.visaoParaEstrategia(imagem);
                        }
                        catch { }
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