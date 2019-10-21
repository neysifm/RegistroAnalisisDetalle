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
    public class RepositorioPagos : RepositorioBase<Pago>
    {
        // METODO GUARDAR
        public override bool Guardar(Pago pago)
        {
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            bool paso = false;
            foreach (var item in pago.PagoDetalle.ToList())
            {
                var Analisis = repositorio.Buscar(item.AnalisisId);
                Analisis.Balance -= item.Monto;
                paso = repositorio.Modificar(Analisis);
            }
            repositorio.Dispose();
            if (paso)
            return base.Guardar(pago);
            return paso;
        }

        // METODO MODIFICAR
        public override bool Modificar(Pago pago)
        {
            bool paso = false;
            var Anterior = Buscar(pago.PagoId);
            Contexto contexto1 = new Contexto();
            try
            {
                using (Contexto contexto = new Contexto())
                {
                    bool pass = false;
                    foreach (var item in Anterior.PagoDetalle.ToList())
                    {
                        if (!pago.PagoDetalle.Exists(x => x.PagoDetalleId == item.PagoDetalleId))
                        {
                            RepositorioAnalisis repositorio = new RepositorioAnalisis();
                            Analisis Analisis = repositorio.Buscar(item.AnalisisId);
                            Analisis.Balance += item.Monto;

                            contexto.Entry(item).State = EntityState.Deleted;
                            repositorio.Modificar(Analisis);
                            repositorio.Dispose();
                            pass = true;
                        }
                    }

                    if (pass)
                    contexto.SaveChanges();
                    contexto.Dispose();
                }

                foreach (var item in pago.PagoDetalle)
                {
                    var estado = EntityState.Unchanged;
                    if (item.PagoDetalleId == 0)
                    {
                        RepositorioAnalisis repositorio = new RepositorioAnalisis();
                        Analisis Analisis = repositorio.Buscar(item.AnalisisId);
                        Analisis.Balance -= item.Monto;
                        estado = EntityState.Added;
                        repositorio.Modificar(Analisis);
                        repositorio.Dispose();
                    }
                    contexto1.Entry(item).State = estado;
                }
                contexto1.Entry(pago).State = EntityState.Modified;
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

        // METODO BUSCAR
        public override Pago Buscar(int id)
        {
            Pago Pagos = new Pago();
            Contexto contexto = new Contexto();
            try
            {
                Pagos = contexto.Pago.Include(x => x.PagoDetalle).Where(x => x.PagoId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Pagos;
        }

        // METODO ELIMINAR
        public override bool Eliminar(int id)
        {
            Pago pago = Buscar(id);
            bool paso = false;
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            foreach (var item in pago.PagoDetalle)
            {
                var Analisis = repositorio.Buscar(item.AnalisisId);
                Analisis.Balance += item.Monto;
                paso = repositorio.Modificar(Analisis);
            }
            if (paso)
            return base.Eliminar(pago.PagoId);
            return paso;
        }

        // METODO LISTAR
        public override List<Pago> GetList(Expression<Func<Pago, bool>> expression)
        {
            List<Pago> Lista = new List<Pago>();
            Contexto contexto = new Contexto();
            try
            {
                Lista = contexto.Set<Pago>().AsNoTracking().Where(expression).ToList();
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
