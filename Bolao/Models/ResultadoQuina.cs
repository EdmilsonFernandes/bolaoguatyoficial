using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bolao.Models
{
    public class ResultadoQuina
    {
        public int acertosQuina { get; set; }
        public string numeroSorteio { get; set; }

        public string jogada { get; set; }

        public string numerosAcertados { get; set; } 

        public string valorGanho { get; set;}

        public string acertoTipoJogo { get; set; }

        public string quantidadeAcerto { get; set; }
    }

    public class ListaResultadoQuina : List<ResultadoQuina> { };
}