using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PagoDetalle
    {
        [Key]
        public int PagoDetalleId { get; set; }
        public int PagoId { get; set; }
        public int AnalisisId { get; set; }
        public int AnalisisDetalleId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        [ForeignKey("PagoId")]
        public virtual Pago Pago { get; set; }

        public PagoDetalle()
        {
            PagoDetalleId = 0;
            PagoId = 0;
            AnalisisId = 0;
            AnalisisDetalleId = 0;
            Fecha = DateTime.Now;
            Monto = 0;
        }

        public PagoDetalle(int pagoId, int analisisId, int analisisDetalleId, decimal monto, DateTime fecha)
        {
            PagoId = pagoId;
            AnalisisId = analisisId;
            AnalisisDetalleId = analisisDetalleId;
            Monto = monto;
            Fecha = fecha;
        }
    }
}
