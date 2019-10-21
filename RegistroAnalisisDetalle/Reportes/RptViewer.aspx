<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="RptViewer.aspx.cs" 
    Inherits="RegistroAnalisisDetalle.Reportes.RptViewer" %>

    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
        html,body,form,#div1 {
            height: 100%; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1" >
            <%--AGREGAMOS EL SCRIPTMANAGER--%>
            <asp:ScriptManager runat="server"></asp:ScriptManager>   

            <%--AGREGAMOS EL VIEWER--%>
            <rsweb:ReportViewer ID="MyReportViewer" runat="server" ProcessingMode="Remote" Height="100%" Width="100%">
                <ServerReport ReportPath="" ReportServerUrl="" />
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
