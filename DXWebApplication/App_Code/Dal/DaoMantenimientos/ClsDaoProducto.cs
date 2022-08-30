using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DXWebApplication.App_Code.Utilidades;
using DXWebApplication.App_Code.Models;

namespace DXWebApplication.App_Code.Dal.DaoMantenimientos
{
    public class ClsDaoProducto : ClsDataLayer
    {
        ClsConexion objSql = new ClsConexion();
        ClsErrorHandler log = new ClsErrorHandler();
        string strSql = "";
        //Estructura de un metodo para obtener informacion de la BD
        public bool GetProductoAll()
        {
            try
            {
                strSql = "SELECT ID_PRODUCTO, ID_TIPO_PRODUCTO, DESCRIPCION, PRECIO, EXISTENCIA, ESTADO FROM POS.PRODUCTO  ";
                DsReturn = objSql.EjectuaSQL(strSql, "Producto");
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetProductoCategoria()
        {
            try
            {
                strSql = "SELECT	P.ID_PRODUCTO, P.DESCRIPCION, TP.DESCRIPCION CATEGORIA, P.PRECIO, P.EXISTENCIA, " +
                                   "CASE WHEN P.ESTADO = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END ESTADO  " +
                                   "FROM	POS.PRODUCTO P " +
                                   "JOIN	POS.TIPO_PRODUCTO TP " +
                                   "ON		P.ID_TIPO_PRODUCTO = TP.ID_TIPO_PRODUCTO " +
                                   "ORDER BY P.ID_PRODUCTO, TP.DESCRIPCION ";


                DsReturn = objSql.EjectuaSQL(strSql, "Producto");
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        //Metodo para insertar producto
        public bool InsertProducto(ClsProducto producto)
        {
            strSql = "INSERT INTO POS.PRODUCTO " +
                     "(ID_PRODUCTO, ID_TIPO_PRODUCTO, DESCRIPCION, PRECIO, EXISTENCIA, ESTADO) " +
                     "VALUES " +
                     "((SELECT ISNULL(MAX(ID_PRODUCTO),0) + 1 FROM POS.PRODUCTO), '" +
                     producto.IdTipoProducto + "', '" +
                     producto.Descripcion + "', '" +
                     producto.Precio + "', '" +
                     producto.Existencia + "', '" +
                     producto.Estado + "')";
            return ExecuteSql(strSql);
        }

        //Metodo para modificar producto
        public bool ModificaProducto(ClsProducto producto)
        {
            strSql = "UPDATE POS.PRODUCTO " +
                      "SET ID_TIPO_PRODUCTO = '" + producto.IdTipoProducto + "', " +
                      "DESCRIPCION = '" + producto.Descripcion + "', " +
                      "PRECIO = " + producto.Precio + ", " +
                      "EXISTENCIA = " + producto.Existencia + ", " +
                      "ESTADO = " + producto.Estado + " " +
                      "WHERE ID_PRODUCTO = " + producto.IdProducto;

            return ExecuteSql(strSql);
        }

        //Metodo para modificar producto
        public bool BorraProducto(ClsProducto producto)
        {
            strSql = "DELETE FROM POS.PRODUCTO " +
                      "WHERE ID_PRODUCTO = " + producto.IdProducto;

            return ExecuteSql(strSql);
        }

        //Estructura de un metodo para ejecutar una accion INSERT, UPDATE, DELETE en nuestra BD
        public bool ExecuteSql(string strSql)
        {
            try
            {
                return objSql.ejecutarNonQuery(strSql);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
        }
    }
}
