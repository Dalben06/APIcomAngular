using System;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.Domain
{
    public class Lote
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "This Date is invalid")]
        public DateTime? DataInicio { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "This Date is invalid")]
        public DateTime? DataFim { get; set; }
        [Required]
        [Range(1, 99999999)]
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get;}
    }
}