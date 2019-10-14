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
        
        public string DataEvento { get; set; }
        public string Local { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<LoteDTO> Lote { get; set; }
        public List<RedeSocialDTO> RedeSocial { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }
        public string ImagemURL { get; set; }
    }
}
