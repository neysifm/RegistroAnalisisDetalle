<%@ Page Title="Pacientes" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="RegistroPacientes.aspx.cs" 
    Inherits="RegistroAnalisisDetalle.Registros.RegistroPacientes1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">Registro de Pacientes</div>

        <div class="panel-body">
            <div class="form-horizontal col-md-12" role="form">
                <%--PacienteId--%>
                <div class="form-group">
                    <label for="IdTextBox" class="col-md-3 control-label input-sm">Id: </label>
                    <div class="col-md-1 col-sm-2 col-xs-4">
                        <asp:TextBox ID="IdTextBox" runat="server" ReadOnly="True" placeholder="0" class="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-2 col-xs-4">
                        <asp:LinkButton ID="BusquedaButton" CssClass="btn btn-info btn-block btn-md" data-toggle="modal" data-target="#myModal" CausesValidation="False" runat="server" Text="<span class='glyphicon glyphicon-search'></span>" PostBackUrl="~/Consultas/cCategorias.aspx" />
                    </div>
                </div>

                <%--Descripcion--%>
                <div class="form-group">
                    <label for="DescripcionTextBox" class="col-md-3 control-label input-sm">Descripcion</label>
                    <div class="col-md-8">
                        <asp:TextBox ID="DescripcionTextBox" runat="server" 
                            Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescripcion" runat="server" MaxLength="200" 
                            ControlToValidate="DescripcionTextBox" 
                            ErrorMessage="Campo Descripcion obligatorio" ForeColor="Red" 
                            Display="Dynamic" SetFocusOnError="True" 
                            ToolTip="Campo Descripcion obligatorio">Por favor llenar el campo Descripcion
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <%--Tipo--%>
                <div class="form-group">
                    <label for="TipoDropDownList" class="col-md-3 control-label input-sm">Tipo</label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="TipoDropDownList" runat="server" Class="form-control input-sm">
                            <asp:ListItem Selected="True">[Seleccione Uno]</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <%--Presupuesto--%>
                <div class="form-group">
                    <label for="PresupuestoTextBox" class="col-md-3 control-label input-sm">Presupuesto</label>
                    <div class="col-md-8">
                        <asp:TextBox ID="PresupuestoTextBox" runat="server" Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFV_MontoTextBox" runat="server" ControlToValidate="PresupuestoTextBox" ErrorMessage="Campo presupuesto obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Monto obligatorio">Por favor llenar el campo Monto</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REV_MontoTextBox" runat="server" ControlToValidate="PresupuestoTextBox" ErrorMessage="No se permiten caracteres en el presupuesto" ForeColor="Red" Dynamic="true" SetFocusOnError="true" ValidationExpression="^(\d|-)?(\d|,)*\.?\d*$">Solo se permiten Números. </asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <asp:ValidationSummary runat="server" ID="SumaryValidation"
                    ForeColor="red"
                    DisplayMode="BulletList"
                    ShowSummary="true"
                    EnableClientScript="True"
                    Font-Bold="False"
                    CssClass=" alert alert-danger" />
            </div>

            <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="panel-footer">
            <div class="text-center">
                <div class="form-group" style="display: inline-block">

                    <asp:Button Text="Nuevo" class="btn btn-warning btn-sm" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                    <asp:Button Text="Guardar" class="btn btn-success btn-sm" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
                    <asp:Button Text="Eliminar" class="btn btn-danger btn-sm" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />

                </div>
            </div>

        </div>
    </div>

</asp:Content>
