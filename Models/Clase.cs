using System.Collections.Generic;

namespace liquidador_web.Models
{
    public partial class Clase
    {
        public Clase()
        {
            T103dainfoproc = new HashSet<T103dainfoproc>();
        }

        public string codiclas { get; set; }
        public string descclas { get; set; }

        public ICollection<T103dainfoproc> T103dainfoproc { get; set; }
    }
}
