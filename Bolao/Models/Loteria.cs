using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bolao.Models
{
    [DataContract]
    public class Loteria
    {
        [DataMember]
        public int NumeroConcurso { get; set; }
        [DataMember]
        public bool Acumulou { get; set; }
        [DataMember]
        public double EstimativaPremio { get; set; }
        [DataMember]
        public double ValorAcumulado { get; set; }
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public string RealizadoEm { get; set; }
        [DataMember]
        public object DescricaoAcumuladoOutro { get; set; }
        [DataMember]
        public object ValorAcumuladoOutro { get; set; }
        [DataMember]
        public string DataProximo { get; set; }
        [DataMember]
        public double ValorAcumuladoEspecial { get; set; }
        [DataMember]
        public double Arrecadacao { get; set; }
        [DataMember]
        public List<Sorteio> Sorteios { get; set; }
    }
}