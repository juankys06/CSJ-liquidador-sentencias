using System;
using System.ComponentModel.DataAnnotations;

namespace liquidador_web.Models
{
    public class Liquidaciones
    {
        [Key]
        public string ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }
        public TipoLiquidacion tipo { get; set; }
        public LiquidadorUser autor { get; set; }
        public string urlFile { get; set; }
        public string nameFile { get; set; }
        public T103dainfoproc llaveproc { get; set; }
    }
}