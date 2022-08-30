using DevExpress.Web;
using DXWebApplication.App_Code.Controller.ControllerMantenimientos;
using DXWebApplication.App_Code.Models;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXWebApplication.WebForms.Home
{
    public partial class WebPortal : System.Web.UI.Page
    {
        ClsControllerUsuario objUsuario = new ClsControllerUsuario();
        ClsControllerServicio objServicio = new ClsControllerServicio();
        ClsUsuario usuario = new ClsUsuario();
        ClsServicio servicio = new ClsServicio();
        ClsErrorHandler log = new ClsErrorHandler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["CatProducto"] = null;
            }
            else
            {
                
            }
            CargaServicio();
            dxRbServicios.AutoPostBack = true;
            dxTxtOtros.Visible = false;
        }

        void ClearDatos()
        {
            dxTxtCorreo.Text = "";
            dxTxtNombre.Text = "";
            dxTxtArea.Text = "";

            Session["IdUsuario"] = null;
        }

        void CargaServicio()
        {
            if (objServicio.GetServicioAll())
            {
                dxRbServicios.DataSource = objServicio.DsReturn.Tables["Servicio"];
                dxRbServicios.DataBind();
                Session["Servicio"] = objServicio.DsReturn;
            }
        }

        void EnviarEmail()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("frontdevtester@outlook.com");
                mail.To.Add(dxTxtCorreo.Text);
                mail.Subject = "Gracias por tu email";
                mail.Body = "<h2>Hemos recibido tu solicitud de apoyo, y alguien de nuestro equipo se pondrá en contacto contigo pronto.</h2>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("frontdevtester@outlook.com", "qwerty@1234");
                    smtp.EnableSsl = true;

                    try
                    {
                        smtp.Send(mail);
                        dxPopUpConfirmacion.ShowOnPageLoad = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void dxTxtCorreo_TextChanged(object sender, EventArgs e)
        {
            usuario.Correo = dxTxtCorreo.Text;
            if (objUsuario.GetUsuarioByCorreo(usuario))
            {
                if (objUsuario.DsReturn.Tables["Usuario"].Rows.Count > 0)
                {
                    string nombre = objUsuario.DsReturn.Tables["Usuario"].Rows[0]["NOMBRE"].ToString();
                    string apellido = objUsuario.DsReturn.Tables["Usuario"].Rows[0]["APELLIDO"].ToString();
                    string area = objUsuario.DsReturn.Tables["Usuario"].Rows[0]["AREA"].ToString();

                    dxTxtNombre.Text = nombre + " " + apellido;
                    dxTxtArea.Text = area;
                    Session["IdUsuario"] = objUsuario.DsReturn.Tables["Usuario"].Rows[0]["ID_USUARIO"].ToString();
                }
                else
                {
                    //Response.Redirect("~/WebForms/Mantenimientos/Cliente/WebCliente.aspx");
                }
            }
        }

        protected void dxRbServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dxRbServicios.SelectedItem.Value.ToString() == "5")
            {
                dxTxtOtros.Visible = true;
            }
            else
            {
                dxTxtOtros.Visible = false;
            }
        }

        protected void dxBtnAceptar_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(HiddenFieldAceptar.Value))
            {
                return;
            }

            EnviarEmail();

            ClearDatos();
        }

        protected void dxBtnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            ClearDatos();
        }
    }
}