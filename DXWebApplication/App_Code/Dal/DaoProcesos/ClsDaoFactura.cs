using DXWebApplication.App_Code.Models;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Dal.DaoProcesos
{
    public class ClsDaoFactura : ClsDataLayer
    {
        ClsConexion objSql = new ClsConexion();
        ClsErrorHandler log = new ClsErrorHandler();
        string strSql = "";


        public bool InsertarFactura(ClsFactura Factura, List<ClsDetalleFactura> LstDetalleFactura)
        {
            SqlConnection conexion = objSql.OpenConexion();
            SqlTransaction transaccion;
            transaccion = conexion.BeginTransaction();
            int linea = 1;
            try
            {

                strSql = "INSERT INTO POS.FACTURA (ID_FACTURA, SERIE, FECHA, ID_CLIENTE, TOTAL) VALUES ((SELECT ISNULL(MAX(ID_FACTURA),0) + 1 FROM POS.FACTURA), '" + Factura.Serie + "', GETDATE(), " + Factura.IdCliente + ", " + Factura.Total + ")";
                objSql.EjectuaSQLT(conexion, transaccion, strSql);

                foreach (ClsDetalleFactura detalleFactura in LstDetalleFactura)
                {

                    strSql = "INSERT INTO POS.DETALLE_FACTURA (ID_FACTURA, SERIE, ID_DETALLE_FACTURA, ID_PRODUCTO, PRECIO, CANTIDAD, SUBTOTAL) " +
                             " VALUES  ((SELECT MAX(ID_FACTURA) FROM POS.FACTURA), '" + Factura.Serie + "', " + linea + "," + detalleFactura.IdProducto + ", " + detalleFactura.Precio + ", " + detalleFactura.Cantidad + "," + detalleFactura.Subtotal + ")";
                    objSql.EjectuaSQLT(conexion, transaccion, strSql);
                    strSql = "UPDATE POS.PRODUCTO SET EXISTENCIA = EXISTENCIA - " + detalleFactura.Cantidad +
                            " WHERE ID_PRODUCTO = " + detalleFactura.IdProducto;
                    objSql.EjectuaSQLT(conexion, transaccion, strSql);
                    linea++;
                }

                transaccion.Commit();

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                transaccion.Rollback();
                conexion.Close();

                return false;
            }

            conexion.Close();

            return true;
        }
    }
}