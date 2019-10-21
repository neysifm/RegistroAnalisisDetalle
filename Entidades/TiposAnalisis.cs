using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class TiposAnalisis
    {
        [Key]

        public int TipoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaRegistro { get; set; }

        public TiposAnalisis(int tipoId, string descripcion, decimal monto, DateTime fechaRegistro)
        {
            TipoId = tipoId;
            Descripcion = descripcion;
            Monto = monto;
            FechaRegistro = fechaRegistro;
        }

        public TiposAnalisis()
        {
            TipoId = 0;
            Descripcion = string.Empty;
            Monto = 0;
            FechaRegistro = DateTime.Now;
        }
    }
}
