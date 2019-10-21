using Entidades;
using DAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistroAnalisisDetalle.Reportes
{
    public partial class RptViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var lista = Pago.GetList(x => true);
                MyReportViewer.ProcessingMode = ProcessingMode.Local;
               
                MyReportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reportes\ReportePagos.rdlc");
                
                MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Pagos", lista));
                MyReportViewer.LocalReport.Refresh();
            }
        }
    }
}