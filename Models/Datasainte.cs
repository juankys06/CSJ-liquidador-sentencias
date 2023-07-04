using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace liquidador_web.Models
{
    public class Datasainte
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDTasa { get; set; }
        public string TipoTasa { get; set; }
        //public TiposTasas tipoTasa { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime VigenteDesde { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime VigenteHasta { get; set; }
        public double? ValorTasa { get; set; }
        public string Periodo { get; set; }
        public string ResVigencia { get; set; }
        public int? importedID { get; set; }
    }
}
