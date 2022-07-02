using System.Collections.Generic;

namespace EstacBem.Domain.Entities
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }

        //Relacionamentos
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public virtual IEnumerable<Estadia> Estadias { get; set; }
    }
}
