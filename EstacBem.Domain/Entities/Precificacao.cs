using System;

namespace EstacBem.Domain.Entities
{
    public class Precificacao
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        public decimal ValorPrimeira { get; set; }
        public decimal ValorDemais { get; set; }
    }
}
