using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        public int PacienteId { get; set; }
        public virtual List<PagoDetalle> PagoDetalle { get; set; }
        public DateTime Fecha { get; set; }

        public Pago()
        {
            PagoId = 0;
            PagoDetalle = new List<PagoDetalle>();
            Fecha = DateTime.Now;
        }

        public Pago(int pagoId, int pacienteId, List<PagoDetalle> pagoDetalle, DateTime fecha)
        {
            PagoId = pagoId;
            PacienteId = pacienteId;
            PagoDetalle = pagoDetalle;
            Fecha = fecha;
        }

        public void AgregarDetalle(int PagoDetalleId, int PagoId, int PacienteId, decimal Monto, string estado)
        {
            PagoDetalle.Add(new PagoDetalle(PagoDetalleId, PagoId, PacienteId, Monto, estado));
        }

        public void RemoverDetalle(int Index)
        {
            this.PagoDetalle.RemoveAt(Index);
        }
    }
}
