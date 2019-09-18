using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        public int AnalisisId { get; set; }

        public virtual List<PagoDetalle> PagoDetalle { get; set; }

        public Pago()
        {
            PagoDetalle = new List<PagoDetalle>();
        }

        public void AgregarDetalle(int AnalisisDetalleId, decimal Monto)
        {
            this.PagoDetalle.Add(new Entidades.PagoDetalle(this.PagoId, this.AnalisisId, AnalisisDetalleId, Monto, DateTime.Now));
        }
    }
}
