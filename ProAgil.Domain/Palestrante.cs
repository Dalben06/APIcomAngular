using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.Domain
{
    public class Palestrante
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<RedeSocial> RedeSocial{ get; set; }
        public List<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}