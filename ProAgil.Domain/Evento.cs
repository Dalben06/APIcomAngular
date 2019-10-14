using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.Domain
{
    public class Evento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is Required!")]
        [DataType(DataType.DateTime, ErrorMessage = "This Date is invalid")]
        public DateTime  DataEvento { get; set; }
        [Required]
        public string  Local { get; set; } 
        [Required]
        [MinLength(5)]
        public string  Tema { get; set; }
        [Range(2,120000)]
        public int QtdPessoas { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [EmailAddress]
        public string  Email { get; set; }
        public List<Lote> Lote { get; set; }
        public List<RedeSocial> RedeSocial { get; set; }
        public List<PalestranteEvento> PalestrantesEvento { get; set; }
        public string ImagemURL { get; set; }
    }
}