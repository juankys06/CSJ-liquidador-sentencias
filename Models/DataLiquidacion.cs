using System;
using System.ComponentModel.DataAnnotations;

namespace liquidador_web.Models
{
    public class DataLiquidacion
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public TipoLiquidacion tipo { get; set; }
        [Required]
        [MaxLength(25)]
        public string llaveproc { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }
        [Required]
        public string data { get; set; }
        public bool autoGuardar { get; set; }
        [Required]
        public LiquidadorUser creador { get; set; }
    }
}
