using System;
using BLL;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegistroAnalisisDetalleTest
{
    [TestClass]
    public class PagoDetalleTest
    {
        [TestMethod]
        public void Guardar()
        {
            RepositorioBase<PagoDetalle> repositorio = new RepositorioBase<PagoDetalle>();
            Assert.IsTrue(repositorio.Guardar(GetPagoDetalle()));
        }

        private PagoDetalle GetPagoDetalle()
        {
            PagoDetalle pago = new PagoDetalle();

            pago.PagoDetalleId = 1;
            pago.PagoId = 1;
            pago.AnalisisId = 2;
            pago.AnalisisDetalleId = 2;
            pago.Fecha = DateTime.Now;
            pago.Monto = 3000;

            return pago;
        }
    }
}
