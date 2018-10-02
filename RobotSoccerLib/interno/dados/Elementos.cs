using RobotSoccerLib.externo.interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.interno.dados
{
    /// <summary>
    /// Dados do Robô
    /// </summary>
    /// <typeparam name="Img">Tipo de Imagem</typeparam>
    /// <typeparam name="VtoERobo">Informação da Visão para Estratégia</typeparam>
    /// <typeparam name="EtoCRobo">Informação da Estratégia para Comunicação sem fio</typeparam>
    internal class Robo<Img, VtoERobo, EtoCRobo, VtoEBola, VtoECampo, PlaceToDraw>
    {
        private string id;
        private VtoERobo infoEstrat;
        private EtoCRobo infoExped;
        private IVisao<Img, VtoERobo, PlaceToDraw> visao;
        private IEstrategia<VtoERobo, EtoCRobo, VtoEBola, VtoECampo> estrategia;
        private IComunicacao<EtoCRobo> comunicacao;

        public Robo(string id, IVisao<Img, VtoERobo, PlaceToDraw> visao, IEstrategia<VtoERobo, EtoCRobo, VtoEBola, VtoECampo> estrategia, IComunicacao<EtoCRobo> comunicacao)
        {
            this.id = id;
            infoEstrat = default(VtoERobo);
            infoExped = default(EtoCRobo);
            this.visao = visao;
            this.estrategia = estrategia;
            this.comunicacao = comunicacao;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public IVisao<Img, VtoERobo, PlaceToDraw> Visao
        {
            get { return visao; }
            set { visao = value; }
        }

        public IEstrategia<VtoERobo, EtoCRobo, VtoEBola, VtoECampo> Estrategia
        {
            get { return estrategia; }
            set { estrategia = value; }
        }

        public IComunicacao<EtoCRobo> Comunicacao
        {
            get { return comunicacao; }
            set { comunicacao = value; }
        }

        internal void visaoParaEstrategia(Img frame)
        {
            infoEstrat = visao.processarImagem(frame);
        }

        internal void estrategiaParaExpedidor(VtoEBola infoBola, VtoECampo infoCampo)
        {
            infoExped = estrategia.executarEstrategia(infoEstrat, infoBola, infoCampo);
        }
        internal void expedidorParaRobo()
        {
            comunicacao.enviarMensagem(infoExped);
        }
        internal void controleManual(EtoCRobo infoExped)
        {
            comunicacao.enviarMensagem(infoExped);
        }

    }

    internal class Bola<Img, VtoEBola, PlaceToDraw>
    {
        private VtoEBola informacao;
        private IVisao<Img, VtoEBola, PlaceToDraw> visao;

        /*public Bola(IVisao<Img, VtoEBola, PlaceToDraw> visao)
        {
            informacao = default(VtoEBola);
            this.visao = visao;
        }*/

        public VtoEBola Informacao
        {
            get { return informacao; }
            set { informacao = value; }
        }

        public IVisao<Img, VtoEBola, PlaceToDraw> Visao
        {
            get { return visao; }
            set { visao = value; }
        }

        void frameParaInfo(Img frame)
        {
            Informacao = visao.processarImagem(frame);
        }
    }

    internal class Campo<Img, VtoECampo, PlaceToDraw>
    {
        private object informacao;
        private IVisao<Img, VtoECampo, PlaceToDraw> visao;

        /*public Campo(object informacao, IVisao<Img, VtoECampo, PlaceToDraw> visao)
        {
            this.informacao = informacao;
            this.visao = visao;
        }*/

        public IVisao<Img, VtoECampo, PlaceToDraw> Visao
        {
            get { return visao; }
            set { visao = value; }
        }

        public void frameParaInfo(Img frame)
        {
            informacao = visao.processarImagem(frame);
        }
    }


}
