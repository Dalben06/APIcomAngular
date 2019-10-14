using System.ComponentModel.DataAnnotations;

namespace ProAgil.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Url]
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; }
        public int? PalestranteId { get; set; }
        public Palestrante Palestrante { get;}
    }
}