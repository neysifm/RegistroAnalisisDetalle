using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistroAnalisisDetalle.Consultas
{
    public partial class ConsultaPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuscarButton_Click((object)this.BuscarButton, new EventArgs());
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<Pacientes, bool>> filtro = x => true;
            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
            int id;

            if (!string.IsNullOrEmpty(FiltroTextBox.Text))
            {
                switch (BuscarPorDropDownList.SelectedIndex)
                {
                    case 0://ID
                        id = Utilitarios.Utils.ToInt(FiltroTextBox.Text);
                        filtro = c => c.PacienteId == id;
                        break;
                    case 1://Nombre
                        filtro = c => c.Nombre.Contains(FiltroTextBox.Text);
                        break;
                }
            }
            DatosGridView.DataSource = repositorio.GetList(filtro);
            DatosGridView.DataBind();
        }
    }
}