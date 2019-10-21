using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistroAnalisisDetalle.Registros
{
    public partial class RegistroPagos : System.Web.UI.Page
    {
        readonly string KeyViewState = "Pagos";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                ViewState[KeyViewState] = new Pago();
            }
        }
        public void Limpiar()
        {
            PagosIdTextBox.Text = "0";
            PacienteTextBox.Text = string.Empty;
            BalanceTextBox.Text = string.Empty;
            ViewState[KeyViewState] = new Pago();
            AnalisisDropDownList.Items.Clear();
            this.BindGrid();
        }
        private void LlenarCombo()
        {
            RepositorioAnalisis repositorioAnalisis = new RepositorioAnalisis();
            AnalisisDropDownList.Items.Clear();
            int PacienteId = PacienteTextBox.Text.ToInt();
            List<Analisis> ListaAnalisis = repositorioAnalisis.GetList(x => x.PacienteId == PacienteId && x.Balance > 0);
            AnalisisDropDownList.DataSource = ListaAnalisis;
            AnalisisDropDownList.DataTextField = "AnalisisId";
            AnalisisDropDownList.DataValueField = "AnalisisId";
            AnalisisDropDownList.DataBind();
        }
        private void BindGrid()
        {
            Pago Pagos = ViewState[KeyViewState].ToPago();
            DetalleGridView.DataSource = Pagos.PagoDetalle;
            DetalleGridView.DataBind();
        }
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            if (!Validar())
                return;
            bool paso = false;
            RepositorioPagos repositorio = new RepositorioPagos();
            Pago Pagos = LlenaClase();
            MostrarMensajes.Text = "No Se Pudo Guardar El Registro";
            MostrarMensajes.CssClass = "alert-warning";
            MostrarMensajes.Visible = true;

            if (Pagos.PagoId == 0)
                paso = repositorio.Guardar(Pagos);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    MostrarMensajes.Visible = true;
                    MostrarMensajes.Text = "Registro No encontrado";
                    MostrarMensajes.CssClass = "alert-danger";
                    return;
                }
                paso = repositorio.Modificar(Pagos);
            }
            if (paso)
            {
                Limpiar();
                MostrarMensajes.Text = "Guardado Exitosamente!!";
                MostrarMensajes.CssClass = "alert-success";
                MostrarMensajes.Visible = true;
            }
            else
            {
                MostrarMensajes.Text = "No Se Pudo Guardar El Registro";
                MostrarMensajes.CssClass = "alert-warning";
                MostrarMensajes.Visible = true;
            }
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioPagos repositorio = new RepositorioPagos();
            Pago Pagos = repositorio.Buscar(PagosIdTextBox.Text.ToInt());
            if (!Pagos.EsNulo())
            {
                Limpiar();
                LlenarCampos(Pagos);
            }
            else
            MostrarMensajes.Visible = true;
            MostrarMensajes.Text = "Registro No encontrado";
            MostrarMensajes.CssClass = "alert-danger";
            repositorio.Dispose();
        }
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioPagos repositorio = new RepositorioPagos();
            int id = PagosIdTextBox.Text.ToInt();
            if (ExisteEnLaBaseDeDatos())
            {
                MostrarMensajes.Visible = true;
                MostrarMensajes.Text = "Registro No encontrado";
                MostrarMensajes.CssClass = "alert-danger";
                return;
            }
            else
            {
                if (repositorio.Eliminar(id))
                {
                    MostrarMensajes.Visible = true;
                    MostrarMensajes.Text = "Eliminado Correctamente!!";
                    MostrarMensajes.CssClass = "alert-danger";
                    Limpiar();
                }
            }
            repositorio.Dispose();

        }
        private void LlenarCampos(Pago pagos)
        {
            PagosIdTextBox.Text = pagos.PagoId.ToString();
            PacienteTextBox.Text = pagos.PacienteId.ToString();
            ViewState[KeyViewState] = pagos;
            this.BindGrid();
        }

        private Pago LlenaClase()
        {
            Pago Pagos = ViewState[KeyViewState].ToPago();
            Pagos.PagoId = PagosIdTextBox.Text.ToInt();
            Pagos.PacienteId = PacienteTextBox.Text.ToInt();
            Pagos.Fecha = FechaTextBox.Text.ToDatetime();
            return Pagos;
        }
        protected void AgregarPagoButton_Click(object sender, EventArgs e)
        {
            if (MontoPagarTextBox.Text.ToDecimal() <= 0)
                return;
            if (!SumarTotalPagos())
                return;
            Pago Pago = ViewState[KeyViewState].ToPago();
            Pago.AgregarDetalle(0, Pago.PagoId, AnalisisDropDownList.SelectedValue.ToInt(), MontoPagarTextBox.Text.ToDecimal(),"Reviso");
            ViewState[KeyViewState] = Pago;
            this.BindGrid();
            MontoPagarTextBox.Text = string.Empty;
        }
        protected void RemoverDetalleClick_Click(object sender, EventArgs e)
        {
            Pago Pagos = ViewState[KeyViewState].ToPago();
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            Pagos.RemoverDetalle(row.RowIndex);
            ViewState[KeyViewState] = Pagos;
            this.BindGrid();
        }
        private bool SumarTotalPagos()
        {
            bool paso = false;
            Pago Pagos = ViewState[KeyViewState].ToPago();
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            Analisis analisis = repositorio.Buscar(AnalisisDropDownList.SelectedValue.ToInt());
            decimal Total = 0;
            Pagos.PagoDetalle.ForEach(x => Total += x.Monto);
            Total += MontoPagarTextBox.Text.ToDecimal();
            paso = Total <= analisis.Monto ? true : false;
            repositorio.Dispose();
            return paso;
        }
        private bool Validar()
        {
            bool paso = true;
            if (AnalisisDropDownList.Items.Count == 0)
                paso = false;
            if (ViewState[KeyViewState].ToPago().PagoDetalle.Count() == 0)
                paso = false;
            return paso;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioPagos repositorio = new RepositorioPagos();
            Pago pagos = repositorio.Buscar(PagosIdTextBox.Text.ToInt());
            repositorio.Dispose();
            return pagos.EsNulo();
        }
        protected void DetalleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Pago Pago = ViewState[KeyViewState].ToPago();
            DetalleGridView.DataSource = Pago.PagoDetalle;
            DetalleGridView.PageIndex = e.NewPageIndex;
            DetalleGridView.DataBind();
        }
        protected void BuscarPaciente_Click(object sender, EventArgs e)
        {
            RepositorioBase<Pacientes> repositorioBase = new RepositorioBase<Pacientes>();
            if (!repositorioBase.Buscar(PacienteTextBox.Text.ToInt()).EsNulo())
            {
                LlenarCombo();
                AnalisisDropDownList_SelectedIndexChanged(null, null);
            }
        }
        protected void AnalisisDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            List<Analisis> ListaAnalisis = repositorio.GetList(x => true);
            int AnalisisId = AnalisisDropDownList.SelectedValue.ToInt();
            var Analisis = ListaAnalisis.Find(x => x.AnalisisId == AnalisisId);
            BalanceTextBox.Text = Analisis.Balance.ToString();
        }
    }
}