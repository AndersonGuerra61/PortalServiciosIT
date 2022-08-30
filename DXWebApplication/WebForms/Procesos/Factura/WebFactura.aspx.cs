using DevExpress.Web;
using DXWebApplication.App_Code.Controller.ControllerMantenimientos;
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

namespace DXWebApplication.WebForms.Procesos.Factura
{
    public partial class WebFactura : System.Web.UI.Page
    {

        ClsControllerProducto objProducto = new ClsControllerProducto();
        ClsControllerFactura objFactura = new ClsControllerFactura();
        ClsControllerCliente objCliente = new ClsControllerCliente();
        ClsCliente cliente = new ClsCliente();
        ClsErrorHandler log = new ClsErrorHandler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["PedidoDetalle"] = null;
                Session["CatProducto"] = null;
                Session["PedidoDetalle"] = null;
                Session["Correlativo"] = null;
            }
        }

        protected void dxTxtNit_TextChanged(object sender, EventArgs e)
        {
            cliente.Nit = dxTxtNit.Text;
            if (objCliente.GetClienteByNit(cliente))
            {
                if (objCliente.DsReturn.Tables["Cliente"].Rows.Count > 0)
                {
                    string nombre = objCliente.DsReturn.Tables["Cliente"].Rows[0]["NOMBRE"].ToString();
                    string apellido = objCliente.DsReturn.Tables["Cliente"].Rows[0]["APELLIDO"].ToString();
                    string direccion = objCliente.DsReturn.Tables["Cliente"].Rows[0]["DIRECCION"].ToString();

                    dxTxtCliente.Text = nombre + " " + apellido;
                    dxTxtDireccion.Text = direccion;
                    //Session["Cliente"] = objCliente.DsReturn;
                    Session["IdCliente"] = objCliente.DsReturn.Tables["Cliente"].Rows[0]["ID_CLIENTE"].ToString();
                }
                else
                {
                    Response.Redirect("~/WebForms/Mantenimientos/Cliente/WebCliente.aspx");
                }
            }

        }

        void CargaProducto()
        {
            if (objProducto.GetProductoCategoria())
            {
                dxGridProducto.DataSource = objProducto.DsReturn.Tables["Producto"];
                dxGridProducto.DataBind();
                Session["CatProducto"] = objProducto.DsReturn;
            }
        }

        void SetGridProducto()
        {
            dxGridProducto.DataSource = ((DataSet)Session["CatProducto"]);
            dxGridProducto.DataBind();


        }

        void SetGridDetalle()
        {
            dxGridDetalle.DataSource = ((DataTable)Session["PedidoDetalle"]);
            dxGridDetalle.DataBind();
        }

        void ClearDatos()
        {
            dxTxtNit.Text = "";
            dxTxtCliente.Text = "";
            dxTxtDireccion.Text = "";

            Session["PedidoDetalle"] = null;
            Session["CatProducto"] = null;
            Session["PedidoDetalle"] = null;
            Session["Correlativo"] = null;
            Session["IdCliente"] = null;

            dxGridDetalle.DataSource = null;
            dxGridDetalle.DataBind();
        }

        protected void dxGridProducto_Init(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargaProducto();

                }
                if (IsPostBack)
                {
                    SetGridProducto();
                }

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);

            }
        }

        protected void dxBtnAgregar_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            ClsDetalleFactura detalleFactura = new ClsDetalleFactura();
            List<object> fieldValues = dxGridProducto.GetSelectedFieldValues(new string[] { "ID_PRODUCTO", "DESCRIPCION", "PRECIO" });

            if (fieldValues.Count > 0)
            {
                foreach (object[] item in fieldValues)
                {
                    detalleFactura.IdProducto = int.Parse(item[0].ToString());
                    detalleFactura.Descripcion = item[1].ToString();
                    detalleFactura.Precio = decimal.Parse(item[2].ToString());
                }
                int correlativo;
                if (Session["PedidoDetalle"] != null)
                {
                    dt = Session["PedidoDetalle"] as DataTable;
                    correlativo = int.Parse(Session["Correlativo"].ToString()) + 1;
                }
                else
                {
                    dt = filldata();
                    correlativo = 1;

                }
                Session["Correlativo"] = correlativo;
                DataRow fila = dt.NewRow();

                fila[0] = correlativo;
                fila[1] = int.Parse(dxSpnCantidad.Text);
                fila[2] = detalleFactura.IdProducto;
                fila[3] = detalleFactura.Descripcion;
                fila[4] = detalleFactura.Precio;
                fila[5] = detalleFactura.Precio * int.Parse(dxSpnCantidad.Text);

                dt.Rows.Add(fila);

                dxGridDetalle.DataSource = dt;
                dxGridDetalle.DataBind();

                Session["PedidoDetalle"] = dt;
            }
        }

        //Data table Virtual
        public DataTable filldata()
        {
            DataTable dt = new DataTable();
            DataColumn correlativo = dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("CANTIDAD", typeof(int));
            dt.Columns.Add("ID_PRODUCTO", typeof(string));
            dt.Columns.Add("PRODUCTO", typeof(string));
            dt.Columns.Add("PRECIO", typeof(decimal));
            dt.Columns.Add("SUBTOTAL", typeof(decimal));

            //dt.Columns.Add("ELIMINAR", typeof(Button));
            dt.PrimaryKey = new DataColumn[] { correlativo };
            correlativo.ReadOnly = true;

            return dt;
        }

        protected void dxGridDetalle_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
                SetGridDetalle();
        }

        protected void dxGridDetalle_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                ClsProducto ProductoItem = new ClsProducto();
                List<object> fieldValues = dxGridProducto.GetSelectedFieldValues(new
                string[] { "ID_PRODUCTO", "DESCRIPCION" });
                foreach (object[] item in fieldValues)
                {
                    ProductoItem.IdProducto = int.Parse(item[0].ToString());
                }
                if (fieldValues.Count > 0)
                {
                    int idDetalle = int.Parse(e.Keys["ID"].ToString());
                    DataTable table = Session["PedidoDetalle"] as DataTable;
                    //DataRow found = table.Rows.Find(e.Keys["ID"]);
                    //table.Rows.Remove(found);
                    for (int i = table.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = table.Rows[i];
                        if (int.Parse(dr["ID"].ToString()) == idDetalle)
                        {
                            dr.Delete();
                            table.AcceptChanges();
                            dxGridDetalle.DataSource = table;
                            dxGridDetalle.DataBind();
                            Session["PedidoDetalle"] = table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
            }
            //updControles.Update();
            e.Cancel = true;
            dxGridDetalle.CancelEdit();
        }

        protected void dxBtnAceptar_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(HiddenFieldAceptar.Value))
            {
                return;
            } 

            List<ClsDetalleFactura> lsdDetalleFactura = new List<ClsDetalleFactura>();

            //Modelo para encabezado de factura
            ClsFactura Factura = new ClsFactura();

            Factura.IdCliente = int.Parse(Session["IdCliente"].ToString());
            Factura.Serie = "A-1";

            //seguir llenando el objeto

            decimal total = 0;

            //recorrer el grid del detalle de factura y llenar la lista para el detalle de la factura
            if (dxGridDetalle.VisibleRowCount > 0)
            {
                for (int i = 0; i < dxGridDetalle.VisibleRowCount; i++)
                {
                    ClsDetalleFactura DetallePedido = new ClsDetalleFactura();
                    DetallePedido.IdDetalle = int.Parse(dxGridDetalle.GetRowValues(i, "ID").ToString());
                    DetallePedido.IdProducto = int.Parse(dxGridDetalle.GetRowValues(i, "ID_PRODUCTO").ToString());
                    DetallePedido.Precio = decimal.Parse(dxGridDetalle.GetRowValues(i, "PRECIO").ToString());
                    DetallePedido.Cantidad = int.Parse(dxGridDetalle.GetRowValues(i, "CANTIDAD").ToString());
                    DetallePedido.Subtotal = (DetallePedido.Precio) * DetallePedido.Cantidad;

                    total = DetallePedido.Subtotal + total;
                    lsdDetalleFactura.Add(DetallePedido);
                }

                Factura.Total = total;
                objFactura.InsertarFactura(Factura, lsdDetalleFactura);
                string StrQry = "<script language='javascript'>";
                StrQry += "alert('factura generada con exito!'); ";
                StrQry += "</script>";
                ClientScript.RegisterStartupScript(GetType(), "mensaje", StrQry, false);
                //ScriptManager.RegisterStartupScript(updControles, updControles.GetType(), "GeneraPedido", "GeneraPedido(0,'Pedio generado correctamente');", true);
            }

            ClearDatos();
        }

        protected void dxBtnNuevaFactura_Click(object sender, EventArgs e)
        {
            ClearDatos();
        }
    }
}