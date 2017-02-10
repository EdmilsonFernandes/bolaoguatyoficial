using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bolao.Models
{
    public class EstaticaBolao
    {

        public int numero { get; set; }
        public int qtd { get; set; }
    }

    public class ListaEstaticaBolao:List<EstaticaBolao>
    {

    }
}