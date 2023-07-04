namespace liquidador_web.Models
{
    public class Procesos
    {
        public string Ciudad { get; set; }
        public string Entidad { get; set; } //-- 2
        public string Especialidad { get; set; } //-- 2
        public string Despacho { get; set; } //-- 3
        public string Año { get; set; } //-- 4
        public string Codigo { get; set; } //-- 5
        public string Numero { get; set; } //-- 2
        public string Completo { get; set; } //-- 23
        public string DEMANDANTE { get; set; }
        public string DEMANDADO { get; set; }
        public string Tipo { get; set; } //-- 50
        public string Clase { get; set; } //-- 50
        public string Descripcion { get; set; } //-- 50
    }
}
