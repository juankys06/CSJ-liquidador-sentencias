using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace liquidador_web.Models
{
    public class LiquidadorUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Creator { get; set; }
        public bool   Active { get; set; }
        public string Token { get; set; }
        public string CodLocalidad { get; set; }
        public string CodEntidad { get; set; }
        public string CodEspecialidad { get; set; }
        public string CodDespacho { get; set; }
        [JsonIgnore]
        public List<Auditoria> Auditoria { get; set; }
        [JsonIgnore]
        public List<Liquidaciones> Liquidaciones { get; set; }
    }
}
