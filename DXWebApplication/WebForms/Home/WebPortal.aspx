<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="WebPortal.aspx.cs" Inherits="DXWebApplication.WebForms.Home.WebPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebForm.css") %>" type="text/css" />
    <link rel="Stylesheet" href="<%= ResolveUrl("~/Content/StyleWebPortal.css") %>" type="text/css" />
    <script ="text/javascript" >
        function ConfirmarSolicitud() {
            var resultado = confirm('¿Generar solicitud?');
            
            if (resultado) {
                document.getElementById('<%= HiddenFieldAceptar.ClientID %>').value = true;
            } else {
                document.getElementById('<%= HiddenFieldAceptar.ClientID %>').value = false;
            }
        }
    </script> 
    <div class="container" id="contenedor">
        <div class="cabecera" id="cabecera">
            <div>Portal de Servicios de IT</div>
        </div>
        <div style="overflow-x: auto" class="encabezado">
            <dx:ASPxTextBox ID="dxTxtCorreo" runat="server" Native="True" Caption="Correo" CssClass="form-control" AutoPostBack="true"
                OnTextChanged="dxTxtCorreo_TextChanged" NullText ="Ingresa tu correo electrónico..." Width="300px">
                <NullTextStyle ForeColor="Gray"></NullTextStyle>
                <CaptionSettings Position="Top" />
                <ValidationSettings ErrorDisplayMode="ImageWithText" SetFocusOnError="True" Display="Dynamic" ErrorTextPosition="Bottom" ValidateOnLeave="true">
                    <RequiredField ErrorText="Campo requerido." IsRequired="true" />
                    <RegularExpression ErrorText="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </ValidationSettings>
                <InvalidStyle BackColor="LightPink" />
            </dx:ASPxTextBox>
            <br />
            <dx:ASPxTextBox ID="dxTxtNombre" runat="server" Native="True" Caption="Nombre" ValidationSettings-Display="Dynamic" CssClass="form-control" 
                Width="300px" ReadOnly="true" NullText="Nombre Completo">
                <NullTextStyle ForeColor="Gray"></NullTextStyle>
                <CaptionSettings Position="Top" />
            </dx:ASPxTextBox>
            <br />
            <dx:ASPxTextBox ID="dxTxtArea" runat="server" Native="True" Caption="Area" ValidationSettings-Display="Dynamic" CssClass="form-control" 
                Width="300px" ReadOnly="true" NullText="Area de Trabajo">
                <NullTextStyle ForeColor="Gray"></NullTextStyle>
                <CaptionSettings Position="Top" />
            </dx:ASPxTextBox>
            <br />
            <dx:ASPxRadioButtonList ID="dxRbServicios" ClientInstanceName="dxRbServicios" runat="server" Caption="Servicio" Native="True"
                OnSelectedIndexChanged="dxRbServicios_SelectedIndexChanged" Width="300px" ValueField="ID_SERVICIO" TextField="DESCRIPCION">
                
                <CaptionSettings Position="Top" />
                <ValidationSettings ErrorDisplayMode="ImageWithText" SetFocusOnError="True" Display="Dynamic" ErrorTextPosition="Bottom" ValidateOnLeave="true">
                    <RequiredField ErrorText="Seleccione un servicio." IsRequired="true" />
                </ValidationSettings>
            </dx:ASPxRadioButtonList>
            <br />
            <dx:ASPxTextBox ID="dxTxtOtros" runat="server" Native="True" Caption="Especifique" ValidationSettings-Display="Dynamic" CssClass="form-control" Width="300px" NullText="Escriba otro servicio...">
                <NullTextStyle ForeColor="Gray"></NullTextStyle>
                <CaptionSettings Position="Top" />
                <ValidationSettings ErrorDisplayMode="ImageWithText" SetFocusOnError="True" Display="Dynamic" ErrorTextPosition="Bottom" ValidateOnLeave="true">
                    <RequiredField ErrorText="Campo obligatorio." IsRequired="true" />
                </ValidationSettings>
            </dx:ASPxTextBox>
            <br />
            <dx:ASPxMemo ID="ASPxMemoComentario" runat="server" Native="True" Caption="Comentarios" CssClass="form-control"
                Width="300px" Height="150px" NullText="Escriba un comentario...">
                <NullTextStyle ForeColor="Gray"></NullTextStyle>
                <CaptionSettings Position="Top" />
                <ValidationSettings ErrorDisplayMode="ImageWithText" SetFocusOnError="True" Display="Dynamic" ErrorTextPosition="Bottom" ValidateOnLeave="true">
                    <RequiredField ErrorText="Campo requerido." IsRequired="true" />
                </ValidationSettings>
            </dx:ASPxMemo>
            <br />
            <div class="pie">
                <dx:ASPxButton ID="dxBtnAceptar" runat="server" OnClick="dxBtnAceptar_Click" AutoPostBack="False" Font-Bold="true" Font-Size="Medium" Text="Generar" UseSubmitBehavior="false" Width="200px">
                    <ClientSideEvents Click="function(s, e) {  ConfirmarSolicitud(); }" />
                </dx:ASPxButton>
                <asp:HiddenField ID="HiddenFieldAceptar" runat="server" />
            </div>

            <dx:ASPxPopupControl ID="dxPopUpConfirmacion" runat="server" AllowDragging="False" ClientInstanceName="dxPopUpConfirmacion" CloseAction="CloseButton" EnableViewState="False" Modal="true"
                                HeaderText="Solicitud Aceptada" Height="0px"  PopupAnimationType="Slide" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="0px">
                    <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MaxWidth="700px" />
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                            <div class="cabecera">
                                <div>Gracias! Tu solicitud ha sido enviada satisfactoriamente</div>
                            </div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>                    
            </dx:ASPxPopupControl>
        </div>
    </div>
</asp:Content>
