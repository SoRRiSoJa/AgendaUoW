namespace AgendaUoW.Domain.Models
{
    public class Contato
    {
        public Contato()
        {

        }
        public decimal Id { get; set; }

        public string Nome { get; set; }

        public string Numero { get; set; }

        public decimal Ref { get; set; }

        public bool IsAtivo { get; set; } = true;
    }
}
