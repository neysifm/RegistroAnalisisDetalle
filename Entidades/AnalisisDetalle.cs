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
    public class AnalisisDetalle
    {

        [Key]
        public int AnalisisDetalleId { get; set; }
        public int AnalisisId { get; set; }
        [ForeignKey("AnalisisId")]
        public virtual Analisis Analisis { get; set; }
        public int TipoId { get; set; }
        [ForeignKey("TipoId")]
        public virtual TiposAnalisis TipoAnalisis { get; set; }
        public string Resultado { get; set; }

        public AnalisisDetalle()
        {
            AnalisisDetalleId = 0;
            AnalisisId = 0;
            TipoId = 0;
            Resultado = string.Empty;
        }

        public AnalisisDetalle(int analisisDetalleId, int analisisId, int tipoId, string resultado)
        {
            AnalisisDetalleId = analisisDetalleId;
            AnalisisId = analisisId;
            TipoId = tipoId;
            Resultado = resultado;
        }
    }
}
