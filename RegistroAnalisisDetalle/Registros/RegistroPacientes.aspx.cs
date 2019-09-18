using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistroAnalisisDetalle.Registros
{
	public partial class RegistroPacientes1 : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LlenarCombos();

                int id = Utils.ToInt(Request.QueryString["id"]);
                if (id > 0)
                {
                    BLL.RepositorioBase<Pacientes> repositorio = new BLL.RepositorioBase<Pacientes>();
                    var paciente = repositorio.Buscar(id);

                    if (paciente == null)
                    {
                        MostrarMensaje(TiposMensaje.Error, "Registro no encontrado");
                    }
                    else
                    {
                        LlenaCampos(paciente);
                    }
                }
            }
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Pacientes> repositorio = new BLL.RepositorioBase<Pacientes>();
            Pacientes paciente = new Pacientes();
            bool paso = false;

            LlenaClase(paciente);

            if (paciente.PacienteId == 0)
                paso = repositorio.Guardar(paciente);
            else
                paso = repositorio.Modificar(paciente);

            if (paso)
            {
                MostrarMensaje(TiposMensaje.Success, "Transaccion Exitosa!");
                Limpiar();
            }
            else
                MostrarMensaje(TiposMensaje.Error, "No fue posible terminar la transacción");

        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Pacientes> repositorio = new BLL.RepositorioBase<Pacientes>();
            int id = Utils.ToInt(IdTextBox.Text);

            var categoria = repositorio.Buscar(id);

            if (categoria == null)
                MostrarMensaje(TiposMensaje.Error, "Registro no encontrado");
            else
                repositorio.Eliminar(id);
        }

        private void LlenaClase(Pacientes paciente)
        {
            paciente.PacienteId = Utils.ToInt(IdTextBox.Text);
            paciente.Descripcion = DescripcionTextBox.Text;
            paciente.Presupuesto = Utils.ToDecimal(PresupuestoTextBox.Text);
            paciente.Tipo = (TiposTransacciones)Enum.Parse(typeof(TiposTransacciones), TipoDropDownList.SelectedValue);

        }
        private void LlenaCampos(Pacientes paciente)
        {
            IdTextBox.Text = paciente.CategoriaId.ToString();
            DescripcionTextBox.Text = paciente.Descripcion;
            PresupuestoTextBox.Text = paciente.Presupuesto.ToString("N2");
            TipoDropDownList.SelectedValue = Convert.ToInt16(paciente.Tipo).ToString();
        }
        private void Limpiar()
        {
            IdTextBox.Text = "";
            DescripcionTextBox.Text = "";
            PresupuestoTextBox.Text = "";
        }
        void LlenarCombos()
        {
            TipoDropDownList.DataSource = Enum.GetValues(typeof(TiposTransacciones));
            TipoDropDownList.DataBind();
        }

        void MostrarMensaje(TiposMensaje tipo, string mensaje)
        {
            ErrorLabel.Text = mensaje;

            if (tipo == TiposMensaje.Success)
                ErrorLabel.CssClass = "alert-success";
            else
                ErrorLabel.CssClass = "alert-danger";
        }
    }
}