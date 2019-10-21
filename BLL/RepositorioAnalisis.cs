using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioAnalisis : RepositorioBase<Analisis>
    {
        public RepositorioAnalisis() : base()
        {
            // CONSTRUCTOR
        }

        // METODO GUARDAR
        public override bool Guardar(Analisis analisis)
        {
            analisis.Balance = analisis.Monto;
            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
            Pacientes paciente = repositorio.Buscar(analisis.PacienteId);
            paciente.Balance += analisis.Balance;
            repositorio.Dispose();
            RepositorioBase<Pacientes> RepositorioModificar = new RepositorioBase<Pacientes>();
            RepositorioModificar.Modificar(paciente);
            RepositorioModificar.Dispose();
            return base.Guardar(analisis);
        }

        // METODO MODIFICAR
        public override bool Modificar(Analisis analisis)
        {
            bool paso = false;
            Analisis AnalisisAnterior = Buscar(analisis.AnalisisId);
            RepositorioBase<Pacientes> repositorioPaciente = new RepositorioBase<Pacientes>();
            Pacientes Paciente = repositorioPaciente.Buscar(analisis.PacienteId);
            Paciente.Balance -= AnalisisAnterior.Balance;
            Contexto contexto1 = new Contexto();
            try
            {
                using (Contexto contexto = new Contexto())
                {
                    foreach (var item in AnalisisAnterior.AnalisisDetalle.ToList())
                    {
                        if (!analisis.AnalisisDetalle.Exists(x => x.AnalisisDetalleId == item.AnalisisDetalleId))
                        {
                            RepositorioBase<TiposAnalisis> repositorioBase = new RepositorioBase<TiposAnalisis>();
                            TiposAnalisis TipoAnalisis = repositorioBase.Buscar(item.TipoId);
                            analisis.Balance -= TipoAnalisis.Monto;
                            contexto.Entry(item).State = EntityState.Deleted;
                            analisis.AnalisisDetalle.Remove(item);
                            repositorioBase.Dispose();
                        }
                    }
                    contexto.SaveChanges();
                }
                foreach (var item in analisis.AnalisisDetalle.ToList())
                {
                    var estado = EntityState.Unchanged;
                    if (item.AnalisisDetalleId == 0)
                    {
                        analisis.Balance += contexto1.TipoAnalisis.Find(item.TipoId).Monto;
                        estado = EntityState.Added;
                    }
                    contexto1.Entry(item).State = estado;
                }
                Paciente.Balance += analisis.Balance;
                repositorioPaciente.Modificar(Paciente);
                contexto1.Entry(analisis).State = EntityState.Modified;
                paso = (contexto1.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto1.Dispose();
            }
            return paso;
        }

        // METODO ELIMINAR
        public override bool Eliminar(int id)
        {
            Analisis analisis = Buscar(id);
            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
            Pacientes paciente = repositorio.Buscar(analisis.PacienteId);
            paciente.Balance -= analisis.Balance;
            repositorio.Modificar(paciente);
            repositorio.Dispose();
            return base.Eliminar(id);
        }

        // METODO BUSCAR
        public override Analisis Buscar(int id)
        {
            Analisis analisis = new Analisis();
            Contexto contexto = new Contexto();
            try
            {
                analisis = contexto.Analisis.AsNoTracking().Include(x => x.AnalisisDetalle).Where(x => x.AnalisisId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return analisis;
        }

        // METODO LISTAR
        public override List<Analisis> GetList(Expression<Func<Analisis, bool>> expression)
        {
            List<Analisis> Lista = new List<Analisis>();
            Contexto contexto = new Contexto();
            try
            {
                Lista = contexto.Set<Analisis>().AsNoTracking().Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
