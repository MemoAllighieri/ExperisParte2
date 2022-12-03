using System;

namespace Parte2.Models
{
    public class Producto
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Stock { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}