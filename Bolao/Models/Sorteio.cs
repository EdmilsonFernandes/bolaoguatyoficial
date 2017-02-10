using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Bolao.Models
{
    [DataContract]
    public class Sorteio
    {
        [DataMember]
        public int NumSorteio { get; set; }

        [DataMember]
        public List<int> Numeros { get; set; }

        [DataMember]
        public List<Premio> Premios { get; set; }

        [DataMember]
        public List<Ganhadores> Ganhadores { get; set; }

    }
}