using System.Collections.Generic;

namespace liquidador_web.Models
{
    public partial class TipoProceso
    {
        public TipoProceso()
        {
            T103dainfoproc = new HashSet<T103dainfoproc>();
        }

        public string codiproc { get; set; }
        public string descproc { get; set; }

        public ICollection<T103dainfoproc> T103dainfoproc { get; set; }
    }
}
