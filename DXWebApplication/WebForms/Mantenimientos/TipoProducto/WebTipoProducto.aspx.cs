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

namespace DXWebApplication.WebForms.Mantenimientos.TipoProducto
{
    public partial class WebTipoProducto : System.Web.UI.Page
    {
        ClsErrorHandler log = new ClsErrorHandler();
        DataSet dsTipoProducto = new DataSet();
        ClsControllerTipoProducto objTipoProducto = new ClsControllerTipoProducto();
        ClsTipoProducto tipoProducto = new ClsTipoProducto();

        string mensaje = System.Configuration.ConfigurationManager.AppSettings["msjErrorTecnico"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    CargaTipoProductos();
                else
                    SetGridTipoProducto();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        void CargaTipoProductos()
        {
            if (objTipoProducto.GetTipoProductoAll())
            {
                dxGridTipoProducto.DataSource = objTipoProducto.DsReturn.Tables["TipoProducto"];
                dxGridTipoProducto.DataBind();
                Session["TipoProducto"] = objTipoProducto.DsReturn;
            }
        }
        void SetGridTipoProducto()
        {
            dxGridTipoProducto.DataSource = ((DataSet)Session["TipoProducto"]);
            dxGridTipoProducto.DataBind();
        }
        void VaciarGridTipoProducto()
        {
            dxGridTipoProducto.DataSource = null;
            dxGridTipoProducto.DataBind();
        }

        protected void dxGridTipoProducto_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            try
            {
                tipoProducto.Descripcion = e.NewValues["DESCRIPCION"].ToString();
                tipoProducto.Estado = int.Parse(e.NewValues["ESTADO"].ToString());
                if (objTipoProducto.InsertTipoProducto(tipoProducto))
                {
                    CargaTipoProductos();
                }
                e.Cancel = true;
                this.dxGridTipoProducto.CancelEdit();

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }


        }

        protected void dxGridTipoProducto_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                tipoProducto.IdTipoProducto = int.Parse(e.NewValues["ID_TIPO_PRODUCTO"].ToString());
                tipoProducto.Descripcion = e.NewValues["DESCRIPCION"].ToString();
                tipoProducto.Estado = int.Parse(e.NewValues["ESTADO"].ToString());

                if (objTipoProducto.ModificaTipoProducto(tipoProducto))
                {
                    CargaTipoProductos();
                }
                e.Cancel = true;
                this.dxGridTipoProducto.CancelEdit();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }

        protected void dxGridTipoProducto_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                tipoProducto.IdTipoProducto = int.Parse(e.Values["ID_TIPO_PRODUCTO"].ToString());

                if (objTipoProducto.BorraTipoProducto(tipoProducto))
                {
                    CargaTipoProductos();
                }
                e.Cancel = true;
                this.dxGridTipoProducto.CancelEdit();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
        }
    }
}