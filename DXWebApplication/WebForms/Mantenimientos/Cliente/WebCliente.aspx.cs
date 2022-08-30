using DXWebApplication.App_Code.Controller.ControllerMantenimientos;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DXWebApplication.App_Code.Models;

namespace DXWebApplication.WebForms.Mantenimientos.Cliente
{
    public partial class WebCliente : System.Web.UI.Page
    {
        ClsErrorHandler log = new ClsErrorHandler();
        DataSet dsCliente = new DataSet();
        ClsControllerCliente objCliente = new ClsControllerCliente();
        ClsCliente cliente = new ClsCliente();
        
        string mensaje = System.Configuration.ConfigurationManager.AppSettings["msjErrorTecnico"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)                
                    CargaClientes();
                else           
                    SetGridCliente();                
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        void CargaClientes()
        {
            if (objCliente.GetClienteAll())
            {
                dxGridCliente.DataSource = objCliente.DsReturn.Tables["Cliente"];
                dxGridCliente.DataBind();
                Session["Cliente"] = objCliente.DsReturn;
            }
        }
        void SetGridCliente()
        {
            dxGridCliente.DataSource = ((DataSet)Session["Cliente"]);
            dxGridCliente.DataBind();
        }
        void VaciarGridCliente()
        {
            dxGridCliente.DataSource = null;
            dxGridCliente.DataBind();
        }

        protected void dxGridCliente_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            try
            {
                cliente.Nombre = e.NewValues["NOMBRE"].ToString();
                cliente.Apellido = e.NewValues["APELLIDO"].ToString();
                cliente.Direccion = e.NewValues["DIRECCION"].ToString();
                cliente.Telefono = e.NewValues["TELEFONO"].ToString();
                cliente.Nit =  e.NewValues["NIT"].ToString();
                if (objCliente.InsertCliente(cliente))
                {
                    CargaClientes();
                }
                e.Cancel = true;
                this.dxGridCliente.CancelEdit();

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);                
            }
            

        }

        protected void dxGridCliente_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                cliente.IdCliente = int.Parse(e.NewValues["ID_CLIENTE"].ToString());
                cliente.Nombre = e.NewValues["NOMBRE"].ToString();
                cliente.Apellido = e.NewValues["APELLIDO"].ToString();
                cliente.Direccion = e.NewValues["DIRECCION"].ToString();
                cliente.Telefono = e.NewValues["TELEFONO"].ToString();
                cliente.Nit = e.NewValues["NIT"].ToString();
                
                if (objCliente.ModificaCliente(cliente))
                {
                    CargaClientes();
                }
                e.Cancel = true;
                this.dxGridCliente.CancelEdit();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        protected void dxGridCliente_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                cliente.IdCliente = int.Parse(e.Values["ID_CLIENTE"].ToString());

                if (objCliente.BorraCliente(cliente))
                {
                    CargaClientes();
                }
                e.Cancel = true;
                this.dxGridCliente.CancelEdit();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }
    }
}