﻿using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistroAnalisisDetalle.Registros
{
    public partial class RegistroAnalisis : System.Web.UI.Page
    {
        readonly string KeyViewState = "Analisis";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                ViewState[KeyViewState] = new Analisis();
                LlenarCombo();
                int id = Request.QueryString["AnalisisId"].ToInt();
                if (id > 0)
                {
                    var Analisis = new RepositorioAnalisis().Buscar(id);
                    if (Analisis.EsNulo())
                    {
                        MostrarMensajes.Visible = true;
                        MostrarMensajes.Text = "Registro No encontrado";
                        MostrarMensajes.CssClass = "alert-danger";
                    }
                    else
                        LlenarCampos(Analisis);
                }
            }
        }
        private void LlenarCombo()
        {
            TipoAnalisisDropdonwList.Items.Clear();
            PacientesDropdownList.Items.Clear();
            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();
            TipoAnalisisDropdonwList.DataSource = repositorio.GetList(x => true);
            TipoAnalisisDropdonwList.DataValueField = "TipoId";
            TipoAnalisisDropdonwList.DataTextField = "Descripcion";
            TipoAnalisisDropdonwList.DataBind();

            RepositorioBase<Pacientes> repositorioPacientes = new RepositorioBase<Pacientes>();
            PacientesDropdownList.DataSource = repositorioPacientes.GetList(x => true);
            PacientesDropdownList.DataValueField = "PacienteId";
            PacientesDropdownList.DataTextField = "Nombre";
            PacientesDropdownList.DataBind();
        }
        protected void BindGrid()
        {
            Analisis analisis = ViewState[KeyViewState].ToAnalisis();
            DetalleGridView.DataSource = analisis.AnalisisDetalle;
            DetalleGridView.DataBind();
        }
        public Analisis LLenaClase()
        {
            Analisis Analisis = new Analisis();
            Analisis = ViewState[KeyViewState].ToAnalisis();
            Analisis.AnalisisId = AnalisisIdTextBox.Text.ToInt();
            Analisis.PacienteId = PacientesDropdownList.SelectedValue.ToInt();
            Analisis.Fecha = FechaTextBox.Text.ToDatetime();
            Analisis.Monto = 0;
            Analisis.AnalisisDetalle.ForEach(x => Analisis.Monto += new RepositorioBase<TiposAnalisis>().Buscar(x.TipoId).Monto);
            return Analisis;
        }
        public void LlenarCampos(Analisis analisis)
        {
            Limpiar();
            AnalisisIdTextBox.Text = analisis.AnalisisId.ToString();
            PacientesDropdownList.SelectedValue = analisis.PacienteId.ToString();
            FechaTextBox.Text = analisis.Fecha.ToFormatDate();
            MontoTextBox.Text = analisis.Monto.ToString();
            BalanceTextBox.Text = analisis.Balance.ToString();
            ViewState[KeyViewState] = analisis;
            Calcular();
            this.BindGrid();
        }
        private void Limpiar()
        {
            AnalisisIdTextBox.Text = "0";
            PacientesDropdownList.ClearSelection();
            TipoAnalisisDropdonwList.ClearSelection();
            ResultadoAnalisisTextBox.Text = string.Empty;
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            MontoTextBox.Text = 0.ToString();
            BalanceTextBox.Text = 0.ToString();
            ViewState[KeyViewState] = new Analisis();
            this.BindGrid();
        }
        protected void AgregarAnaliss_Click(object sender, EventArgs e)
        {
            RepositorioBase<TiposAnalisis> repositorio = new RepositorioBase<TiposAnalisis>();

            if (!string.IsNullOrEmpty(DescripcionAnalisisTextBox.Text) && PrecioAnalisisTexBox.Text.ToDecimal() > 0)
                repositorio.Guardar(new TiposAnalisis(0, DescripcionAnalisisTextBox.Text, PrecioAnalisisTexBox.Text.ToDecimal(), DateTime.Now.ToDatetime()));

            LlenarCombo();
        }

        protected void AgregarPacientesButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Pacientes> repositorio = new RepositorioBase<Pacientes>();
            if (!string.IsNullOrEmpty(NombrePacienteTextBox.Text))
            {
                repositorio.Guardar(new Pacientes(0, NombrePacienteTextBox.Text, 0, DateTime.Now));
            }
            else
            {
                MostrarMensajes.Text = "No Se Pudo Guardar El Registro";
                MostrarMensajes.CssClass = "alert-warning";
                MostrarMensajes.Visible = true;
            }
            LlenarCombo();
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
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            Analisis analisis = LLenaClase();
            MostrarMensajes.Visible = true;
            MostrarMensajes.Text = "Registro No encontrado";
            MostrarMensajes.CssClass = "alert-danger";

            if (analisis.AnalisisId == 0)
                paso = repositorio.Guardar(analisis);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    MostrarMensajes.Visible = true;
                    MostrarMensajes.Text = "Registro No encontrado";
                    MostrarMensajes.CssClass = "alert-danger";
                    return;
                }
                paso = repositorio.Modificar(analisis);
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
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            Analisis Analisis = repositorio.Buscar(AnalisisIdTextBox.Text.ToInt());
            if (!Analisis.EsNulo())
            {
                Limpiar();
                LlenarCampos(Analisis);
            }
            else
                MostrarMensajes.Visible = true;
                MostrarMensajes.Text = "Registro No encontrado";
                MostrarMensajes.CssClass = "alert-danger";
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            int id = AnalisisIdTextBox.Text.ToInt();
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
                    Limpiar();
                    MostrarMensajes.Visible = true;
                    MostrarMensajes.Text = "Eliminado Correctamente!!";
                    MostrarMensajes.CssClass = "alert-danger";
                }
            }
        }

        protected void AgregarDetalleButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ResultadoAnalisisTextBox.Text))
                return;
            Analisis analisis = ViewState[KeyViewState].ToAnalisis();
            analisis.AgregarDetalle(0, analisis.AnalisisId, TipoAnalisisDropdonwList.SelectedValue.ToInt(), ResultadoAnalisisTextBox.Text);
            ViewState[KeyViewState] = analisis;
            this.BindGrid();
            Calcular();
            ResultadoAnalisisTextBox.Text = string.Empty;
        }

        protected void RemoverDetalleClick_Click(object sender, EventArgs e)
        {
            Analisis analisis = ViewState[KeyViewState].ToAnalisis();
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            analisis.RemoverDetalle(row.RowIndex);
            ViewState[KeyViewState] = analisis;
            this.BindGrid();
            Calcular();
        }

        public void Calcular()
        {
            decimal Monto = 0;
            Analisis analisis = ViewState[KeyViewState].ToAnalisis();
            foreach (var item in analisis.AnalisisDetalle.ToList())
            {
                TiposAnalisis tipo = new RepositorioBase<TiposAnalisis>().Buscar(item.TipoId);
                Monto += tipo.EsNulo() ? 0 : tipo.Monto;
            }
            analisis.Monto = Monto;
            ViewState[KeyViewState] = analisis;
            this.BindGrid();
        }

        protected void DetalleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Analisis analisis = ViewState[KeyViewState].ToAnalisis();
            DetalleGridView.DataSource = analisis.AnalisisDetalle;
            DetalleGridView.PageIndex = e.NewPageIndex;
            DetalleGridView.DataBind();
        }

        private bool Validar()
        {
            bool paso = true;
            if (TipoAnalisisDropdonwList.Items.Count == 0)
                paso = false;
            if (PacientesDropdownList.Items.Count == 0)
                paso = false;
            if (DetalleGridView.Rows.Count == 0)
                paso = false;
            return paso;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();
            return repositorio.Buscar(AnalisisIdTextBox.Text.ToInt()) == null;
        }
    }
}