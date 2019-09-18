using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AnalisisDetalle
    {

        [Key]
        public int AnalisisDetalleId { get; set; }
        public int AnalisisId { get; set; }
        public int PacienteId { get; set; }
        public int TipoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Balance { get; set; }
        [ForeignKey("AnalisisId")]
        public virtual Analisis Analisis { get; set; }

        public AnalisisDetalle(int analisisDetalleId, int analisisId, int pacienteId, int tipoId, DateTime fecha, decimal balance)
        {
            AnalisisDetalleId = analisisDetalleId;
            AnalisisId = analisisId;
            PacienteId = pacienteId;
            TipoId = tipoId;
            Fecha = fecha;
            Balance = balance;
        }

        public AnalisisDetalle()
        {
            Fecha = DateTime.Now;
            Balance = 0;
        }
    }
}
