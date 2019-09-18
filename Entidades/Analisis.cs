using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Analisis
    {
        [Key]

        public int AnalisisId { get; set; }
        public int PacienteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }

        public virtual List<AnalisisDetalle> AnalisisDetalle { get; set; }

        public Analisis()
        {
            AnalisisId = 0;
            Fecha = DateTime.Now;
            Monto = 0;
            Balance = 0;
            AnalisisDetalle = new List<AnalisisDetalle>();
        }

        public void AgregarDetalle(int AnalisisDetalleId, int tipoId, DateTime fecha, decimal balance)
        {
            this.AnalisisDetalle.Add(new Entidades.AnalisisDetalle(AnalisisDetalleId, this.AnalisisId, this.PacienteId, tipoId, fecha, balance));
        }
    }
}
