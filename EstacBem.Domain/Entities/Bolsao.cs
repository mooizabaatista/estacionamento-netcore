using System.Collections.Generic;

namespace EstacBem.Domain.Entities
{
    public class Bolsao
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int QtdVagas { get; set; }

        // Relacionamentos
        public virtual IEnumerable<Estadia> Estadias { get; set; }
    }
}
