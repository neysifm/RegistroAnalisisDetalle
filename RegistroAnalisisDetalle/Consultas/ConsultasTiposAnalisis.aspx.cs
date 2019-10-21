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
    public partial class ConsultasTiposAnalisis : System.Web.UI.Page
    {
        List<TiposAnalisis> lista = new List<TiposAnalisis>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaDesdeTextBox.Text = DateTime.Now.ToFormatDate();
                FechaHastaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<TiposAnalisis, bool>> filtro = x => true;
            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();
            List<TiposAnalisis> TiposAnalisis = new RepositorioBase<TiposAnalisis>().GetList(x => true);
            int id;
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1://ID
                    id = (FiltroTextBox.Text).ToInt();
                    filtro = x => x.TipoId == id;
                    break;
                case 2:
                    filtro = x => x.Descripcion.Contains(FiltroTextBox.Text);
                    break;
                case 3:
                    id = TiposAnalisis.Find(x => x.Descripcion.Contains(FiltroTextBox.Text)).TipoId;

                    break;
            }
            DateTime fechaDesde = FechaDesdeTextBox.Text.ToDatetime();
            DateTime FechaHasta = FechaHastaTextBox.Text.ToDatetime();
            if (FechaCheckBox.Checked)
                lista = repositorio.GetList(filtro).Where(x => x.FechaRegistro.Date >= fechaDesde.Date && x.FechaRegistro.Date <= FechaHasta.Date).ToList();
            else
            lista = repositorio.GetList(filtro);
            this.BindGrid(lista);
        }

        private void BindGrid(List<TiposAnalisis> lista)
        {
            DatosGridView.DataSource = lista;
            CAntidadTextBox.Text = lista.Count.ToString();
            DatosGridView.DataBind();
        }

        protected void FechaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FechaCheckBox.Checked)
            {
                FechaDesdeTextBox.Visible = true;
                FechaHastaTextBox.Visible = true;
            }
            else
            {
                FechaDesdeTextBox.Visible = false;
                FechaHastaTextBox.Visible = false;
            }
        }

        protected void DatosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DatosGridView.DataSource = lista;
            DatosGridView.PageIndex = e.NewPageIndex;
            DatosGridView.DataBind();
        }
    }
}