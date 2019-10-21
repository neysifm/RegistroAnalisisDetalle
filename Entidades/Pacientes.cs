using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Pacientes
    {
        [Key]
        public int PacienteId { get; set; }
        public string Nombre { get; set; }
        public decimal Balance { get; set; }
        public DateTime FechaIngreso { get; set; }

        public Pacientes()
        {
            PacienteId = 0;
            Nombre = String.Empty;
            Balance = 0;
            FechaIngreso = DateTime.Now;
        }

        public Pacientes(int pacienteId, string nombre, decimal balance, DateTime fechaIngreso)
        {
            PacienteId = pacienteId;
            Nombre = nombre;
            Balance = balance;
            FechaIngreso = fechaIngreso;
        }
    }
}
