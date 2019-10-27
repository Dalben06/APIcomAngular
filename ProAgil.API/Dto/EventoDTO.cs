using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Dto
{
    public class EventoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required!")]
        public string DataEvento { get; set; }
        [Required]
        public string Local { get; set; }
        [Required , MinLength(5)]
        public string Tema { get; set; }
        [Range(2, 120000)]
        public int QtdPessoas { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public List<LoteDTO> Lote { get; set; }

        public List<RedeSocialDTO> RedeSocial { get; set; }

        public List<PalestranteDTO> Palestrantes { get; set; }

        public string ImagemURL { get; set; }
    }
}
