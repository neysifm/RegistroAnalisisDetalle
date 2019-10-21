﻿<%@ Page Title="Consultas de Tipos de Analisis" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="ConsultasTiposAnalisis.aspx.cs" 
    Inherits="RegistroAnalisisDetalle.Consultas.ConsultasTiposAnalisis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="card text-center bg-light mb-3">
            <div class="card-header"><%:Page.Title %></div>
            <div class="card-body">
                <div class="form-horizontal col-md-12" role="form">
                    <div>
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="FiltroDropDownList">Filtro </span>
                            </div>
                            <div class="input-group-prepend col-md-4" aria-describedby="FiltroDropDownList">
                                <asp:DropDownList ID="BuscarPorDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem>Todos</asp:ListItem>
                                    <asp:ListItem>TipoAnalisisID</asp:ListItem>
                                    <asp:ListItem>Descripcion</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="CriterioLB">Criterio </span>
                            </div>
                            <div class="input-group-append" aria-describedby="CriterioLB">
                                <asp:TextBox ID="FiltroTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="input-group-append" aria-describedby="FiltroTextBox">
                                <asp:Button ID="BuscarButton" runat="server" Class="btn btn-success input-sm" Text="Buscar" OnClick="BuscarButton_Click" />
                            </div>
                        </div>
                    </div>
                    <%--CheckBox--%>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <asp:CheckBox AutoPostBack="true" Checked="true" OnCheckedChanged="FechaCheckBox_CheckedChanged" ID="FechaCheckBox" runat="server" Text="Filtrar por fecha" />
                        </div>
                    </div>
                    <%--FechaDesde--%>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="FechaDesde">Desde </span>
                        </div>
                        <div class="input-group-append" aria-describedby="FechaDesdeTextBox">
                            <asp:TextBox ID="FechaDesdeTextBox" TextMode="Date" runat="server" class="form-control input-sm" Visible="true"></asp:TextBox>
                        </div>
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="FechaHasta">Hasta </span>
                        </div>
                        <div class="input-group-append" aria-describedby="FechaHastaTextBox">
                            <asp:TextBox ID="FechaHastaTextBox" TextMode="Date" runat="server" class="form-control input-sm" Visible="true"></asp:TextBox>
                        </div>
                    </div>
                    <%--GRID--%>
                    <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <div class="table table-condensed table-bordered table-responsive">
                                <asp:GridView ID="DatosGridView"
                                    runat="server"
                                    CssClass="table table-condensed table-bordered table-responsive"
                                    CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AllowPaging="true" PageSize="6"
                                    OnPageIndexChanging="DatosGridView_PageIndexChanging">

                                    <AlternatingRowStyle BackColor="LightBlue" />
                                    <Columns>
                                        <asp:HyperLinkField ControlStyle-CssClass="btn btn-info"
                                            DataNavigateUrlFields="TipoAnalisisID"
                                            DataNavigateUrlFormatString="~/Registros/RegistroTipoAnalisis.aspx?TipoAnalisisID={0}"
                                            Text="Editar"></asp:HyperLinkField>
                                    </Columns>
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DatosGridView" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="Cantidad">Cantidad </span>
                    </div>
                    <div aria-describedby="Cantidad">
                        <asp:TextBox ID="CAntidadTextBox" TextMode="Number" ReadOnly="true" MaxLength="9" runat="server" Text="0" class="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                </div>
            </div>
        </div>
</asp:Content>
