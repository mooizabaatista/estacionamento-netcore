using System.Collections.Generic;

namespace EstacBem.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }


        public Status()
        {

        }

        public Status(int id, string tipo, string nome)
        {
            Id = id;
            Tipo = tipo;
            Nome = nome;
        }

        //Relacionamentos
        public virtual List<Estadia> Estadias { get; set; }
    }
}
