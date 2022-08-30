<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="WebCliente.aspx.cs" Inherits="DXWebApplication.WebForms.Mantenimientos.Cliente.WebCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
    <%--<link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleWebForm.css" />--%>
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebForm.css") %>" type="text/css" />
    <div class="contenedor" id="contenedor">
        <div class="cabecera" id="cabecera">
            <th>Mantenimiento de Clientes</th>
        </div>
        
        <dx:ASPxGridView ID="dxGridCliente" runat="server" AutoGenerateColumns="false"  ClientInstanceName="dxGridCliente" KeyFieldName="ID_CLIENTE" SettingsBehavior-ConfirmDelete ="true"
            OnRowInserting="dxGridCliente_RowInserting" OnRowUpdating="dxGridCliente_RowUpdating" OnRowDeleting="dxGridCliente_RowDeleting" Width="100%">
            <SettingsAdaptivity AdaptivityMode="HideDataCells" />
            <SettingsPager PageSize="10" />            
            <EditFormLayoutProperties>
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
            </EditFormLayoutProperties>            
            <SettingsSearchPanel Visible="true"  />
            <Settings  ShowFooter="True"/>
            <SettingsCommandButton>
                    <EditButton  Text="Editar" Image-Url="~/Content/Images/edit.png" />
                    <NewButton   Text="Nuevo"  Image-Url="~/Content/Images/new.png"/>
                    <DeleteButton   Text="Eliminar"  Image-Url="~/Content/Images/borrar.png"/>
            </SettingsCommandButton>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="true" ShowNewButton="true" ShowDeleteButton="true" />  
                               
                <dx:GridViewDataTextColumn Caption="CODIGO CLIENTE" FieldName="ID_CLIENTE" VisibleIndex="1" ReadOnly="true">                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="NOMBRE" FieldName="NOMBRE" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="APELLIDO" FieldName="APELLIDO" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="DIRECCION" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="TELEFONO" FieldName="TELEFONO" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager>
                <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
            </SettingsPager>
        </dx:ASPxGridView>
    </div>
</asp:Content>
