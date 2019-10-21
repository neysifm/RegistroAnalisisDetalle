using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
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

        public void AgregarDetalle(int analisisDetalleId, int analisisId, int tipoId, string resultado)
        {
            this.AnalisisDetalle.Add(new AnalisisDetalle(analisisDetalleId, analisisId, tipoId, resultado));
        }

        public void RemoverDetalle(int Index)
        {
            this.AnalisisDetalle.RemoveAt(Index);
        }
    }
}
