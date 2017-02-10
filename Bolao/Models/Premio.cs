using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Bolao.Models
{
    [DataContract]
    public class Premio
    {
        [DataMember]
        public string Faixa { get; set; }

        [DataMember]
        public int NumeroGanhadores { get; set; }

        [DataMember]
        public double Valor { get; set; }
    }
}