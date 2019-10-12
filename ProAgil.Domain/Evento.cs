using System;
using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public DateTime  DataEvento { get; set; }
        public string  Local { get; set; } 
        public string  Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string Telefone { get; set; }
        public string  Email { get; set; }
        public List<Lote> Lote { get; set; }
        public List<RedeSocial> RedeSocial { get; set; }
        public List<PalestranteEvento> PalestrantesEvento { get;}
        public string ImagemURL { get; set; }
    }
}