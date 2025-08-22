using System.Collections.Generic;

namespace ML
{
    public class ImagenUsuario
    {
        public string IdImagenUsuarioT { get; set; }
        public int IdImagenUsuario { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> ImagenesUsuarios { get; set; }
    }
}
