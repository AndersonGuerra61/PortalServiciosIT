<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="WebTipoProducto.aspx.cs" Inherits="DXWebApplication.WebForms.Mantenimientos.TipoProducto.WebTipoProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
    <%--<link rel="stylesheet" runat="server" media="screen" href="~/Content/StyleWebForm.css" />--%>
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebForm.css") %>" type="text/css" />
    <div class="contenedor" id="contenedor">
        <div class="cabecera" id="cabecera">
            <th>Mantenimiento de Tipos de Producto</th>
        </div>
        
        <dx:ASPxGridView ID="dxGridTipoProducto" runat="server" AutoGenerateColumns="false"  ClientInstanceName="dxGridTipoProducto" KeyFieldName="ID_TIPO_PRODUCTO" SettingsBehavior-ConfirmDelete ="true"
            OnRowInserting="dxGridTipoProducto_RowInserting" OnRowUpdating="dxGridTipoProducto_RowUpdating" OnRowDeleting="dxGridTipoProducto_RowDeleting" Width="100%">
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
                               
                <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="ID_TIPO_PRODUCTO" VisibleIndex="1" ReadOnly="true">                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="ESTADO" FieldName="ESTADO" VisibleIndex="6">
                    <PropertiesComboBox TextField="ESTADO" ValueField="ESTADO" ValueType="System.Int32" 
                        IncrementalFilteringMode="StartsWith">
                        <Items>
                            <dx:ListEditItem Text="Inactivo" Value="0" />
                            <dx:ListEditItem Text="Activo" Value="1" />
                        </Items>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <SettingsPager>
                <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
            </SettingsPager>
        </dx:ASPxGridView>
    </div>
</asp:Content>
