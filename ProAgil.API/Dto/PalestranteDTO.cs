using ProAgil.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Dto
{
    public class PalestranteDTO
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public List<EventoDTO> Eventos { get; set; }
        public List<RedeSocialDTO> RedeSocial { get; set; }
    }
}

