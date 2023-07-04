using System;
using System.ComponentModel.DataAnnotations;

namespace liquidador_web.Models
{
    public class Auditoria
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime fecha { get; set; }
        public LiquidadorUser usuario { get; set; }
        public string modulo { get; set; }
        public string evento { get; set; }
        public string logAnterior { get; set; }
        public string logActual { get; set; }
    }
}
