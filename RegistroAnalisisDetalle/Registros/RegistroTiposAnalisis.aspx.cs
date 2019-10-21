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
    public partial class RegistroTiposAnalisis : System.Web.UI.Page
    {
        readonly string KeyViewState = "TipoAnalisis";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                ViewState[KeyViewState] = new TiposAnalisis();
                int id = Request.QueryString["TipoId"].ToInt();
                if (id > 0)
                {
                    var TipoAnalisis = new RepositorioBase<TiposAnalisis>().Buscar(id);
                    if (TipoAnalisis.EsNulo())
                    {
                        MostrarMensajes.Visible = true;
                        MostrarMensajes.Text = "Registro No encontrado";
                        MostrarMensajes.CssClass = "alert-danger";
                    }
                    else
                    {
                        LlenarCampos(TipoAnalisis);
                    }
                }
            }
        }

        private TiposAnalisis LlenaClase()
        {
            TiposAnalisis tipo = ViewState[KeyViewState].ToTipoAnalisis();
            tipo.TipoId = TipoIdTextBox.Text.ToInt();
            tipo.Descripcion = DescripcionTextBox.Text;
            tipo.Monto = MontoTextBox.Text.ToDecimal();
            tipo.FechaRegistro = FechaTextBox.Text.ToDatetime();
            return tipo;
        }
        private void LlenarCampos(TiposAnalisis tipo)
        {
            TipoIdTextBox.Text = tipo.TipoId.ToString();
            DescripcionTextBox.Text = tipo.Descripcion;
            MontoTextBox.Text = tipo.Monto.ToString();
            FechaTextBox.Text = tipo.FechaRegistro.ToFormatDate();
            ViewState[KeyViewState] = tipo;
        }
        private void Limpiar()
        {
            TipoIdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            MontoTextBox.Text = "0";
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            ViewState[KeyViewState] = new TiposAnalisis();
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
            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();
            MostrarMensajes.Text = "No Se Pudo Guardar El Registro";
            MostrarMensajes.CssClass = "alert-warning";
            MostrarMensajes.Visible = true;
            TiposAnalisis tipoAnalisis = LlenaClase();
            if (tipoAnalisis.TipoId == 0)
                paso = repositorio.Guardar(tipoAnalisis);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MostrarMensajes.Visible = true;
                    MostrarMensajes.Text = "Registro No encontrado";
                    MostrarMensajes.CssClass = "alert-danger";
                    return;
                }
                paso = repositorio.Modificar(tipoAnalisis);
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
            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();
            TiposAnalisis tiposAnalisis = repositorio.Buscar(TipoIdTextBox.Text.ToInt());
            if (!tiposAnalisis.EsNulo())
            {
                Limpiar();
                LlenarCampos(tiposAnalisis);
            }
            else
            {
                MostrarMensajes.Visible = true;
                MostrarMensajes.Text = "Registro No encontrado";
                MostrarMensajes.CssClass = "alert-danger";
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();
            int id = TipoIdTextBox.Text.ToInt();
            if (!ExisteEnLaBaseDeDatos())
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
        }
        private bool Validar()
        {
            bool paso = true;
            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                paso = false;
            if (string.IsNullOrEmpty(MontoTextBox.Text) || MontoTextBox.Text.ToDecimal() <= 0)
                paso = false;
            return paso;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            TiposAnalisis tipoAnalisis = new TiposAnalisis();
            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();
            tipoAnalisis = repositorio.Buscar(TipoIdTextBox.Text.ToInt());
            return tipoAnalisis != null;
        }
    }
}