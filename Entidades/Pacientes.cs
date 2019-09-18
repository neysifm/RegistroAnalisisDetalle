using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pacientes
    {
        [Key]
        public int PacienteId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }

        public Pacientes()
        {
            PacienteId = 0;
            Nombre = String.Empty;
            FechaIngreso = DateTime.Now;
        }

        public Pacientes(int pacienteId, string nombre, DateTime fechaIngreso)
        {
            PacienteId = pacienteId;
            Nombre = nombre;
            FechaIngreso = fechaIngreso;
        }
    }
}
