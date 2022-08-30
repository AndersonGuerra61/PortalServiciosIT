<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="WebFactura.aspx.cs" Inherits="DXWebApplication.WebForms.Procesos.Factura.WebFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebForm.css") %>" type="text/css" />
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebFactura.css") %>" type="text/css" />
    <script ="text/javascript" >
        function ShowProductWindow() {
            dxPopUpProducto.Show();
            dxSpnCantidad.SetText('');
        }
        function ConfirmarFactura() {
            var resultado = confirm('¿Generar factura?');
            
            if (resultado) {
                document.getElementById('<%= HiddenFieldAceptar.ClientID %>').value = true;
            } else {
                document.getElementById('<%= HiddenFieldAceptar.ClientID %>').value = false;
            }
        }
    </script> 
    
    <div class="container" id="contenedor">       
        <div class="cabecera" id="cabecera">
            <div>Facturación</div>
        </div>
        <div style="overflow-x:auto" class="encabezado" >
            <dx:ASPxTextBox ID="dxTxtNit" runat="server" Width="200px" Caption="Nit" CssClass="form-control" AutoPostBack="true"  OnTextChanged="dxTxtNit_TextChanged">
                <CaptionSettings Position="Top" />
                <ValidationSettings ValidationGroup="BuscarValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom" ErrorDisplayMode="Text" >
                <RequiredField ErrorText="El Nit es requerido." IsRequired="true" />
                </ValidationSettings>
            </dx:ASPxTextBox>
           <%-- <dx:ASPxButton ID="dxBtnBuscar" runat="server" Text="" ValidationGroup="BuscarValidationGroup" RenderMode="Link" ImagePosition="Right" >    
                <Image IconID="actions_refresh_16x16gray"></Image>            
            </dx:ASPxButton>--%>
            <dx:ASPxTextBox ID="dxTxtCliente" runat="server" Width="200px" Caption="Cliente" ValidationSettings-Display="Dynamic" CssClass="form-control">
              <CaptionSettings Position="Top" />              
            </dx:ASPxTextBox>
            <dx:ASPxTextBox ID="dxTxtDireccion" runat="server" Width="200px" Caption="Dirección" ValidationSettings-Display="Dynamic" CssClass="form-control">
              <CaptionSettings Position="Top" />              
            </dx:ASPxTextBox>            
            <br />         
            <dx:ASPxButton ID="dxBtnShowProduct" runat="server" AutoPostBack="False" Font-Bold="true" Font-Size="Medium" Text="Agregar Productos" UseSubmitBehavior="false" Width="200px"  >
                <ClientSideEvents Click="function(s, e) {  ShowProductWindow(); }" />                                        
            </dx:ASPxButton>   
            <br />
            <dx:ASPxGridView ID="dxGridDetalle" runat="server" AutoGenerateColumns="False" EnableCallBacks="false" KeyFieldName="ID" OnInit="dxGridDetalle_Init"
                            ClientInstanceName="dxGridDetalle" SettingsBehavior-ConfirmDelete="true" SettingsPager-PageSize="10" Width="100%" OnRowDeleting="dxGridDetalle_RowDeleting">
                 <SettingsAdaptivity AdaptivityMode="HideDataCells" />
                <EditFormLayoutProperties>
                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                </EditFormLayoutProperties>
                <SettingsCommandButton>
                    <DeleteButton   Text="Eliminar"  Image-Url="~/Content/Images/borrar.png"/>
                </SettingsCommandButton>
                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="true" />

                    <dx:GridViewDataTextColumn Caption="№" FieldName="ID" VisibleIndex="1" Width="5%" ReadOnly="true">
                        <Settings AllowAutoFilter="false" />
                        <EditFormSettings Visible ="false" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn Caption="Cantidad" FieldName="CANTIDAD" VisibleIndex="2" Width="10%" PropertiesSpinEdit-MinValue="1" PropertiesSpinEdit-MaxValue="100000">
                        <PropertiesSpinEdit DisplayFormatString="g" MaxValue="100000" MinValue="1">
                        </PropertiesSpinEdit>
                        <Settings AllowAutoFilter="false" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataTextColumn Caption="Código Producto" FieldName="ID_PRODUCTO" VisibleIndex="3" Width="10%" ReadOnly="true">
                                    
                        <EditFormSettings Visible ="false" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Producto" FieldName="PRODUCTO" VisibleIndex="4" Width="30%" ReadOnly="true">
                        <EditFormSettings Visible ="false" />
                    </dx:GridViewDataTextColumn>                   
                    <dx:GridViewDataTextColumn Caption="Precio" FieldName="PRECIO" VisibleIndex="6" Width="10%" ReadOnly="true" PropertiesTextEdit-DisplayFormatString="{0:c2}" >
                        <Settings AllowAutoFilter="false" />
                        <EditFormSettings Visible ="false" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Sub Total" FieldName="SUBTOTAL" VisibleIndex="7" Width="10%" ReadOnly="true" PropertiesTextEdit-DisplayFormatString="{0:c2}"  ToolTip="Cantidad del producto por el precio" >
                        <Settings AllowAutoFilter="false" />
                        <EditFormSettings Visible ="false" />
                    </dx:GridViewDataTextColumn>
                </Columns>
                            
                <TotalSummary>
                <dx:ASPxSummaryItem FieldName="SUBTOTAL"  SummaryType="Sum"  DisplayFormat="Total: {0:c2}" ShowInColumn="Sub Total"   >
                                
                </dx:ASPxSummaryItem>                            
                </TotalSummary>
                <SettingsDataSecurity AllowEdit="true" AllowInsert="False" AllowDelete="true" />
                    <SettingsEditing Mode="Inline" />
                <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowFooter="True"   />
                <SettingsBehavior AllowSelectByRowClick="True"  ProcessSelectionChangedOnServer="True"  />
                <%--<SettingsBehavior AllowSelectByRowClick="True"  />--%>
                <SettingsPager>
                    <PageSizeItemSettings Items="10, 15, 20" Visible="true" />
                </SettingsPager>
            </dx:ASPxGridView>
            
            <div class="pie">
                <dx:ASPxButton ID="dxBtnNuevaFactura" runat="server" OnClick ="dxBtnNuevaFactura_Click" AutoPostBack="False" Font-Bold="true" Font-Size="Medium" Text="Nueva Factura" UseSubmitBehavior="false" Width="200px"  >                                                       
                </dx:ASPxButton>
                <dx:ASPxButton ID="dxBtnAceptar" runat="server" OnClick ="dxBtnAceptar_Click" AutoPostBack="False" Font-Bold="true" Font-Size="Medium" Text="Facturar" UseSubmitBehavior="false" Width="200px"  >                                                       
                    <ClientSideEvents Click="function(s, e) {  ConfirmarFactura(); }" />  
                </dx:ASPxButton>
                <asp:HiddenField id="HiddenFieldAceptar" runat="server" />
            </div>

            <dx:ASPxPopupControl ID="dxPopUpProducto" runat="server" AllowDragging="True" ClientInstanceName="dxPopUpProducto" CloseAction="CloseButton" EnableViewState="False" Modal="true"
                                HeaderText="Productos" Height="0px"  PopupAnimationType="Slide" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="0px">
                    <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MaxWidth="700px" />
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                            <dx:ASPxGridView ID="dxGridProducto" runat="server" AutoGenerateColumns="false"  ClientInstanceName="dxGridProducto" KeyFieldName="ID_PRODUCTO"  
                                OnInit="dxGridProducto_Init"                            
                                   Width="100%">
                                <SettingsAdaptivity AdaptivityMode="HideDataCells" />
                                <SettingsPager PageSize="10" />            
                                <EditFormLayoutProperties>
                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                </EditFormLayoutProperties>            
                                <SettingsSearchPanel Visible="true" />
                                <Settings  ShowFooter="True"/>                      
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="ID_PRODUCTO"  VisibleIndex="1" ReadOnly="true">                    
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="DESCRIPCION" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="CATEGORIA" FieldName="CATEGORIA" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="EXISTENCIA" FieldName="EXISTENCIA" VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>                                   
                                </Columns>
                                <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" />
                                <SettingsPager>
                                    <PageSizeItemSettings Visible="true" Items="10, 15, 20" />
                                </SettingsPager>
                            </dx:ASPxGridView>
                            <br />
                            <dx:ASPxSpinEdit ID="dxSpnCantidad" ClientInstanceName="dxSpnCantidad" runat="server" Font-Bold="true" 
                                            Font-Size="Small" Number="0" Width="20%" MinValue="1" MaxValue="10000" Enabled="true" Height="40px" >                        
                            </dx:ASPxSpinEdit>
                            <br />
                            <dx:ASPxButton ID="dxBtnAgregar" runat="server" AutoPostBack="False" Font-Bold="true" OnClick="dxBtnAgregar_Click"
                                            Font-Size="Medium" Text="Agregar" UseSubmitBehavior="false" Width="20%"  > 
                                <ClientSideEvents Click="function(s, e) { dxPopUpProducto.Hide(); }" />                                                      
                            </dx:ASPxButton>
                        </dx:PopupControlContentControl>
                    </ContentCollection>                    
            </dx:ASPxPopupControl>
        </div>
    </div>
</asp:Content>
