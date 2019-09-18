using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TiposAnalisis
    {
        [Key]

        public int TipoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaIngreso { get; set; }

        public TiposAnalisis(int tipoId, string nombre, string descripcion, decimal precio, DateTime fechaIngreso)
        {
            TipoId = tipoId;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            FechaIngreso = fechaIngreso;
        }

        public TiposAnalisis()
        {
            TipoId = 0;
            Nombre = String.Empty;
            Descripcion = String.Empty;
            Precio = 0;
            FechaIngreso = DateTime.Now;
        }
    }
}
