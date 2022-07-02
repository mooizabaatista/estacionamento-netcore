using System.Collections.Generic;

namespace EstacBem.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string CPF { get; set; }

        //Relacionamentos
        public virtual IEnumerable<Veiculo> Veiculos { get; set; }
    }
}
