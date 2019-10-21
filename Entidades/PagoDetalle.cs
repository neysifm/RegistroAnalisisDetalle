using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class PagoDetalle
    {
        [Key]
        public int PagoDetalleId { get; set; }
        public int PagoId { get; set; }
        public int AnalisisId { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        [ForeignKey("PagoId")]
        public virtual Pago Pago { get; set; }
        [ForeignKey("AnalisisId")]
        public virtual Analisis Analisis { get; set; }

        public PagoDetalle()
        {
            PagoDetalleId = 0;
            PagoId = 0;
            AnalisisId = 0;
            Monto = 0;
            Estado = string.Empty;
        }

        public PagoDetalle(int pagoDetalleId, int pagoId, int analisisId, decimal monto, string estado)
        {
            PagoDetalleId = pagoDetalleId;
            PagoId = pagoId;
            AnalisisId = analisisId;
            Monto = monto;
            Estado = estado;
        }
    }
}
