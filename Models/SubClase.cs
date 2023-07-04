using System.Collections.Generic;

namespace liquidador_web.Models
{
    public partial class SubClase
    {
        public SubClase()
        {
            T103dainfoproc = new HashSet<T103dainfoproc>();
        }

        public string codisubc { get; set; }
        public string descsubc { get; set; }

        public ICollection<T103dainfoproc> T103dainfoproc { get; set; }
    }
}
