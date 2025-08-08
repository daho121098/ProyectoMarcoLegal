namespace Servicio.Proyecto.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Correo { get; set; }
        public string? Contrasennia { get; set; }
        public string? Nombre { get; set; }
        public int IdRol { get; set; }
        public int IdEstado { get; set; }
        public bool Verificado { get; set; }
    }
}
