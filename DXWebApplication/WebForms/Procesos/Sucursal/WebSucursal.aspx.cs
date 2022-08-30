using DevExpress.Web;
using DXWebApplication.App_Code.Controller.ControllerProcesos;
using DXWebApplication.App_Code.Models;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DXWebApplication.WebForms.Procesos.Sucursal
{
    public partial class WebSucursal : System.Web.UI.Page
    {
        ClsControllerSucursal objSucursal = new ClsControllerSucursal();
        ClsSucursal sucursal = new ClsSucursal();
        ClsErrorHandler log = new ClsErrorHandler();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void dxGridSucursal_Init(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaSucursal();

                }
                if (IsPostBack)
                {
                    SetGridSucursal();
                }

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);

            }
        }

        void CargaSucursal()
        {
            if (objSucursal.GetSucursalAll())
            {
                dxGridSucursal.DataSource = objSucursal.DsReturn.Tables["Sucursal"];
                dxGridSucursal.DataBind();
                Session["Sucursal"] = objSucursal.DsReturn;

            }
        }

        void SetGridSucursal()
        {
            dxGridSucursal.DataSource = ((DataSet)Session["Sucursal"]);
            dxGridSucursal.DataBind();
        }

        protected void dxBtnAceptar_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            ClsSucursal sucursal = new ClsSucursal();
            List<object> fieldValues = dxGridSucursal.GetSelectedFieldValues(new string[] { "ID_SUCURSAL", "DESCRIPCION", "DIRECCION" });

            if (fieldValues.Count > 0)
            {
                foreach (object[] item in fieldValues)
                {
                    sucursal.IdSucursal = int.Parse(item[0].ToString());
                    sucursal.Descripcion = item[1].ToString();
                    sucursal.Direccion = item[2].ToString();
                }

                dxTxtCodigo.Text = sucursal.IdSucursal.ToString();
                dxTxtSucursal.Text = sucursal.Descripcion;
                dxTxtDireccion.Text = sucursal.Direccion;
            }
        }
    }
}