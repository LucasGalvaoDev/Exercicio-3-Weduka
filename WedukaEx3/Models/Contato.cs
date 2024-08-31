using WedukaEx3.Enum;

namespace WedukaEx3.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
    }
}
