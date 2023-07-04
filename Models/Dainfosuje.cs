using System.ComponentModel.DataAnnotations;

namespace liquidador_web.Models
{
    public class Dainfosuje
    {
        [Key]
        [MaxLength(15)]
        public string numesuje { get; set; }
        [MaxLength(100)]
        public string nombsuje { get; set; }
        [MaxLength(2)]
        public string docusuje { get; set; }
        [MaxLength(5)]
        public string ciudsuje { get; set; }
        [MaxLength(200)]
        public string dir1suje { get; set; }
        [MaxLength(200)]
        public string dir2suje { get; set; }
        [MaxLength(30)]
        public string tel1suje { get; set; }
        [MaxLength(30)]
        public string fax1suje { get; set; }
        [MaxLength(5)]
        public string ciu1suje { get; set; }
        [MaxLength(5)]
        public string ciu2suje { get; set; }
        [MaxLength(20)]
        public string tarjprof { get; set; }
        [MaxLength(1)]
        public string provdefi { get; set; }
        [MaxLength(2)]
        public string flagsanc { get; set; }
    }
}
