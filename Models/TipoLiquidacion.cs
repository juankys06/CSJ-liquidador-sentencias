using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace liquidador_web.Models
{
    public class TipoLiquidacion
    {
        [Key]
        public int codigo { get; set; }
        public string nombre { get; set; }
        [JsonIgnore]
        public List<Liquidaciones> Liquidaciones { get; set; }
        public List<DataLiquidacion> Guardadas { get; set; }
    }
}
