namespace WedukaEx3.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Contato> Contatos { get; set; }
    }
}
