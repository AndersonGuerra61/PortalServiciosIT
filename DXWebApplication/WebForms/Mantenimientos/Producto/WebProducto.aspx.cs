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

namespace DXWebApplication.WebForms.Mantenimientos.Producto
{
    public partial class WebProducto : System.Web.UI.Page
    {
        ClsErrorHandler log = new ClsErrorHandler();
        DataSet dsProducto = new DataSet();
        ClsControllerProducto objProducto = new ClsControllerProducto();
        ClsProducto producto = new ClsProducto();

        string mensaje = System.Configuration.ConfigurationManager.AppSettings["msjErrorTecnico"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    CargaProductos();
                else
                    SetGridProducto();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        void CargaProductos()
        {
            if (objProducto.GetProductoAll())
            {
                dxGridProducto.DataSource = objProducto.DsReturn.Tables["Producto"];
                dxGridProducto.DataBind();
                Session["Producto"] = objProducto.DsReturn;
            }
        }
        void SetGridProducto()
        {
            dxGridProducto.DataSource = ((DataSet)Session["Producto"]);
            dxGridProducto.DataBind();
        }
        void VaciarGridProducto()
        {
            dxGridProducto.DataSource = null;
            dxGridProducto.DataBind();
        }

        protected void dxGridProducto_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            try
            {
                producto.IdTipoProducto = int.Parse(e.NewValues["ID_TIPO_PRODUCTO"].ToString());
                producto.Descripcion = e.NewValues["DESCRIPCION"].ToString();
                producto.Precio = decimal.Parse(e.NewValues["PRECIO"].ToString());
                producto.Existencia = int.Parse(e.NewValues["EXISTENCIA"].ToString());
                producto.Estado = int.Parse(e.NewValues["ESTADO"].ToString());
                if (objProducto.InsertProducto(producto))
                {
                    CargaProductos();
                }
                e.Cancel = true;
                this.dxGridProducto.CancelEdit();

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        protected void dxGridProducto_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                producto.IdProducto = int.Parse(e.NewValues["ID_PRODUCTO"].ToString());
                producto.IdTipoProducto = int.Parse(e.NewValues["ID_TIPO_PRODUCTO"].ToString());
                producto.Descripcion = e.NewValues["DESCRIPCION"].ToString();
                producto.Precio = decimal.Parse(e.NewValues["PRECIO"].ToString());
                producto.Existencia = int.Parse(e.NewValues["EXISTENCIA"].ToString());
                producto.Estado = int.Parse(e.NewValues["ESTADO"].ToString());

                if (objProducto.ModificaProducto(producto))
                {
                    CargaProductos();
                }
                e.Cancel = true;
                this.dxGridProducto.CancelEdit();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        protected void dxGridProducto_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                producto.IdProducto = int.Parse(e.Values["ID_PRODUCTO"].ToString());

                if (objProducto.BorraProducto(producto))
                {
                    CargaProductos();
                }
                e.Cancel = true;
                this.dxGridProducto.CancelEdit();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }
    }
}