using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Dto
{
    public class RedeSocialDTO
    {
        public int Id;
       
        public string Nome { get; set; }

        public string URL { get; set; }
    }
}
