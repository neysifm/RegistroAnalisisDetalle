using BLL;
using Entidades;
using RegistroAnalisisDetalle.Utilitarios;
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
            ErrorLabel.Text = string.Empty;
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Pacientes> repositorio = new BLL.RepositorioBase<Pacientes>();
            Pacientes paciente = new Pacientes();
            paciente = LlenaClase();
            bool paso = false;

            if (paciente.PacienteId <= 0)
            {
                paciente.FechaIngreso = DateTime.Now;
                paso = repositorio.Guardar(paciente);
                if (paso)
                {
                    Limpiar();
                }
            }
            else
                paso = repositorio.Modificar(paciente);

            if (paso)
            {
                MostrarMensaje(TiposMensaje.Success, "Registro Guardado Correctamente!");
                Limpiar();
            }
            else
                MostrarMensaje(TiposMensaje.Error, "No Fue Posible Guardar El Registro");

        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = 0;

            if (string.IsNullOrEmpty(this.IdTextBox.Text) || string.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                MostrarMensaje(TiposMensaje.Error, "El Registro no Puede Ser Eliminado!!");
                return;
            }
            id = Utils.ToInt(IdTextBox.Text);
            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();

            if (repositorio.Buscar(id) == null)
            {
                MostrarMensaje(TiposMensaje.Error, "Registro no encontrado");
                return;
            }
            bool eliminado = repositorio.Eliminar(id);
            if (eliminado)
            {
                MostrarMensaje(TiposMensaje.Success, "Registro Eliminado!!");
                Limpiar();
                return;
            }
        }

        private Pacientes LlenaClase()
        {
            Pacientes paciente = new Pacientes();
            paciente.PacienteId = Utils.ToInt(IdTextBox.Text);
            paciente.Nombre = NombreTextBox.Text;
            return paciente;
        }
        private void LlenaCampos(Pacientes paciente)
        {
            IdTextBox.Text = paciente.PacienteId.ToString();
            NombreTextBox.Text = paciente.Nombre;
        }
        private void Limpiar()
        {
            IdTextBox.Text = "";
            NombreTextBox.Text = "";
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