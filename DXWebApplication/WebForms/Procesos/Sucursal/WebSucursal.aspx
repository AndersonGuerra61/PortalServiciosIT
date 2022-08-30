<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="WebSucursal.aspx.cs" Inherits="DXWebApplication.WebForms.Procesos.Sucursal.WebSucursal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebForm.css") %>" type="text/css" />
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebFactura.css") %>" type="text/css" />
    <script ="text/javascript" >
        function ShowSucursalWindow() {
            dxPopUpSucursal.Show();
            dxSpnCantidad.SetText('');
        }
    </script> 
    
    <div class="container" id="contenedor">       
        <div class="cabecera" id="cabecera">
            <th>Examen Parcial 1</th>
        </div>
        <div style="overflow-x:auto" class="encabezado" >
            <dx:ASPxButton ID="dxBtnShowSucursal" runat="server" AutoPostBack="False" Font-Bold="true" Font-Size="Medium" Text="Ver Sucursales" UseSubmitBehavior="false" Width="200px"  >
                <ClientSideEvents Click="function(s, e) {  ShowSucursalWindow(); }" />                                        
            </dx:ASPxButton>   
            <br />

            <dx:ASPxPopupControl ID="dxPopUpSucursal" runat="server" AllowDragging="True" ClientInstanceName="dxPopUpSucursal" CloseAction="CloseButton" EnableViewState="False" Modal="true"
                                HeaderText="Sucursales" Height="0px"  PopupAnimationType="Slide" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="0px">
                    <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MaxWidth="700px" />
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                            <dx:ASPxGridView ID="dxGridSucursal" runat="server" AutoGenerateColumns="false"  ClientInstanceName="dxGridSucursal" KeyFieldName="ID_SUCURSAL"  
                                OnInit="dxGridSucursal_Init"                            
                                   Width="100%">
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" />
                                <SettingsPager PageSize="10" />            
                                <EditFormLayoutProperties>
                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                </EditFormLayoutProperties>            
                                <SettingsSearchPanel Visible="true" />
                                <Settings  ShowFooter="True"/>                      
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="ID_SUCURSAL" FieldName="ID_SUCURSAL"  VisibleIndex="1" ReadOnly="true">                    
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="DIRECCION" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>                                   
                                </Columns>
                                <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                                <SettingsPager>
                                    <PageSizeItemSettings Visible="true" Items="10, 15, 20" />
                                </SettingsPager>
                            </dx:ASPxGridView>
                            <br />
                            <dx:ASPxButton ID="dxBtnAceptar" runat="server" AutoPostBack="False" Font-Bold="true" OnClick="dxBtnAceptar_Click"
                                            Font-Size="Medium" Text="Aceptar" UseSubmitBehavior="false" Width="20%"  >                                                      
                            </dx:ASPxButton>
                            <br />
                            <dx:ASPxTextBox ID="dxTxtCodigo" runat="server" Width="200px" Caption="Codigo" ValidationSettings-Display="Dynamic" CssClass="form-control">
                                <CaptionSettings Position="Top" />              
                            </dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="dxTxtSucursal" runat="server" Width="200px" Caption="Sucursal" ValidationSettings-Display="Dynamic" CssClass="form-control">
                                <CaptionSettings Position="Top" />              
                            </dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="dxTxtDireccion" runat="server" Width="200px" Caption="Dirección" ValidationSettings-Display="Dynamic" CssClass="form-control">
                                <CaptionSettings Position="Top" />              
                            </dx:ASPxTextBox>
                        </dx:PopupControlContentControl>
                    </ContentCollection>                    
            </dx:ASPxPopupControl>
        </div>
    </div>
</asp:Content>
