using EstacBem.Domain.Entities;
using System;

namespace EstacBem.WebUI.Models
{
    public class EstadiaViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; } // ok
        public DateTime Entrada { get; set; } //
        public DateTime? Saida { get; set; }
        public decimal ValorPrimeira { get; set; }
        public decimal ValorDemais { get; set; }
        public decimal? ValorTotal { get; set; }

        //Relacionamentos
        public int BolsaoId { get; set; }
        public virtual Bolsao Bolsao { get; set; }
        public int VeiculoId { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public string TempoTotal { get; set; }
    }
}
