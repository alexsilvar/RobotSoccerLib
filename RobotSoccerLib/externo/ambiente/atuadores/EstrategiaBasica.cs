using RobotSoccerLib.externo.interfaces;
using RobotSoccerLib.interno.ambiente.informacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSoccerLib.interno.ambiente.atuadores
{
    public class EstrategiaBasica : IEstrategia<InfoVtoERobo, InfoEtoCRobo, InfoVtoEBola, InfoVtoECampo>
    {
        private InfoEtoCRobo info;

        public EstrategiaBasica()
        {
            info = new InfoEtoCRobo();
        }

        public InfoEtoCRobo executarEstrategia(InfoVtoERobo infoRobo, InfoVtoEBola infoBola, InfoVtoECampo infoCampo)
        {
            info.RodaDireita = 255;
            info.RodaEsquerda = 255;

            return info;
        }
    }
}
