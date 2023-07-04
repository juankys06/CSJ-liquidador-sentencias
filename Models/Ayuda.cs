using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liquidador_web.Models
{
    public class Ayuda
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string urlDocumento { get; set; }
        public DateTime fecha { get; set; }
        public string roles { get; set; }
    }
}
