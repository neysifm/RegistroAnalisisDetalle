using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioBase<T> : IDisposable, IRepository<T> where T : class
    {
        internal Contexto contexto;

        // CONSTRUCTOR
        public RepositorioBase()
        {
            contexto = new Contexto();
        }

        // METODO GUARDAR
        public virtual bool Guardar(T entity)
        {
            bool Paso = false;
            try
            {
                if (contexto.Set<T>().Add(entity) != null)
                    Paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Paso;
        }

        // METODO MODIFICAR
        public virtual bool Modificar(T entity)
        {
            bool Paso = false;
            try
            {
                contexto.Entry(entity).State = EntityState.Modified;
                Paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Paso;
        }

        // METODO BUSCAR
        public virtual T Buscar(int id)
        {
            T entity;
            try
            {
                entity = contexto.Set<T>().Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return entity;
        }

        // LISTAR
        public virtual List<T> GetList(Expression<Func<T, bool>> expression)
        {
            List<T> lista = new List<T>();
            try
            {
                lista = contexto.Set<T>().AsNoTracking().Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        // METODO ELIMINAR
        public virtual bool Eliminar(int id)
        {
            bool Paso = false;
            T entity;
            try
            {
                entity = contexto.Set<T>().Find(id);
                contexto.Set<T>().Remove(entity);
                Paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Paso;
        }
        public void Dispose()
        {
            // contexto.Dispose();
        }
    }
}
