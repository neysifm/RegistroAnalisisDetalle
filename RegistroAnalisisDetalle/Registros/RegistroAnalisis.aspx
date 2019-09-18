<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroAnalisis.aspx.cs" Inherits="RegistroAnalisisDetalle.Registros.RegistroAnalisis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">Registro de Pacientes</div>

        <div class="panel-body">
            <div class="form-horizontal col-md-12" role="form">

                <%--PacienteId--%>
                <div class="form-group">
                    <label for="IdTextBox" class="col-md-3 control-label input-sm">ID: </label>
                    <div class="col-md-1 col-sm-2 col-xs-4">
                        <asp:TextBox ID="IdTextBox" runat="server" ReadOnly="True" placeholder="0" class="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-2 col-xs-4">
                        <asp:LinkButton ID="BusquedaButton" CssClass="btn btn-info btn-block btn-md" data-toggle="modal" data-target="#myModal" CausesValidation="False" runat="server" Text="<span class='glyphicon glyphicon-search'></span>" PostBackUrl="~/Consultas/ConsultaPacientes.aspx" />
                    </div>
                  </div>
                 </div>
						<div class="form-group">
							<label for="PacienteDropDownList" class="control-label input-sm">Paciente</label>
							<div class="">
								<asp:DropDownList ID="PacienteDropDownList" CssClass=" form-control dropdown" AppendDataBoundItems="true" runat="server" Height="2.5em">
								</asp:DropDownList>
								<asp:RequiredFieldValidator ID="PacienteRequiredFieldValidator" CssClass="col-md-4 col-sm-4" runat="server" ControlToValidate="PacienteDropDownList" Display="Dynamic" ErrorMessage="Porfavor elige un resultado valido..." ValidationGroup="AgregarDetalle">Porfavor elige un paciente valido...</asp:RequiredFieldValidator>
							</div>
						</div>
						<asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
					</div>
					<br />

					<h2 class="text-center">Detalle de Analisis</h2>
					<div class="form-group">
						<label for="TipoAnalisisDropDownList" class="control-label input-sm">Resultado</label>
						<div class="row">
		    				<asp:DropDownList class="col-md-6" ID="TipoAnalisisDropDownList" CssClass=" form-control dropdown" AppendDataBoundItems="true" runat="server" Height="2.5em">
							</asp:DropDownList>
							<asp:RequiredFieldValidator ID="RequiredFieldValidatorTipoAnalisis" CssClass="col-md-4 col-sm-4" runat="server" ControlToValidate="TipoAnalisisDropDownList" Display="Dynamic" ErrorMessage="Porfavor elige un resultado valido..." ValidationGroup="AgregarDetalle">Porfavor elige un paciente valido...</asp:RequiredFieldValidator>
						</div>
						<asp:LinkButton ID="BotonAgregarDetalle" OnClick="BotonAgregarDetalle_Click" CssClass="btn btn-info btn-block btn-md" CausesValidation="False" runat="server" Text="<i class='fa fa-plus'></i>" />
					</div>
					<div class="row">

						<asp:GridView ID="DetalleGridView" AllowPaging="true" PageSize="5" OnPageIndexChanging="DetalleGridView_PageIndexChanging" CssClass="table table-bordered col-md-offset-4 col-sm-offset-4" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="244px" AutoGenerateColumns="true">
							<Columns>
								<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
								<asp:TemplateField HeaderText="Código">
									<ItemTemplate>
										<asp:Label ID="LabelIdTipoAnalisis" Text='<%# Bind("Id_Tipo_Analisis") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateField>

                       <%--Nombre--%>
                <div class="form-group">
                    <label for="NombreTextBox" class="col-md-3 control-label input-sm">Nombre</label>
                    <div class="col-md-8">
                       <asp:TextBox ID="NombreTextBox" runat="server" 
                            Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVDescripcion" runat="server" MaxLength="200" 
                            ControlToValidate="NombreTextBox" 
                            ErrorMessage="Campo Nombre obligatorio" ForeColor="Red" 
                            Display="Dynamic" SetFocusOnError="True" 
                            ToolTip="Campo Nombre obligatorio">Por favor llenar el campo Nombre
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                   <%--Precio--%>
                <div class="form-group">
                    <label for="PrecioTextBox" class="col-md-3 control-label input-sm">Precio</label>
                    <div class="col-md-8">
                       <asp:TextBox ID="TextBox2" runat="server" 
                            Class="form-control input-sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" MaxLength="200" 
                            ControlToValidate="PrecioTextBox" 
                            ErrorMessage="Campo Precio obligatorio" ForeColor="Red" 
                            Display="Dynamic" SetFocusOnError="True" 
                            ToolTip="Campo Precio obligatorio">Por favor llenar el campo Precio
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
							</Columns>
							<AlternatingRowStyle BackColor="White" />
							<EditRowStyle BackColor="#2461BF" />
							<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
							<RowStyle BackColor="#EFF3FB" />
							<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
							<SortedAscendingCellStyle BackColor="#F5F7FB" />
							<SortedAscendingHeaderStyle BackColor="#6D95E1" />
							<SortedDescendingCellStyle BackColor="#E9EBEF" />
							<SortedDescendingHeaderStyle BackColor="#4870BE" />
						</asp:GridView>
					</div>

					<div class="panel-footer">
						<div class="text-center">
							<div class="form-group" style="display: inline-block">
								<asp:Button Text="Nuevo" class="btn btn-warning btn-sm" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
								<asp:Button Text="Guardar" ValidationGroup="grupoValidar" class="btn btn-success btn-sm" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
								<asp:Button Text="Eliminar" class="btn btn-danger btn-sm" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />
							</div>

						</div>
					</div>
        </div>
</asp:Content>
