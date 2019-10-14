using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Dto
{
    public class LoteDTO
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        [Required]
        public decimal Preco { get; set; }
        
        public string DataInicio { get; set; }
        
        public string DataFim { get; set; }
        
        public int Quantidade { get; set; }
    }
}
