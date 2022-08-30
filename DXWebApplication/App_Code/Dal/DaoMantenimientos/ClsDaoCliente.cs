using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DXWebApplication.App_Code.Utilidades;
using DXWebApplication.App_Code.Models;

namespace DXWebApplication.App_Code.Dal.DaoMantenimientos
{
    public class ClsDaoCliente : ClsDataLayer
    {
        ClsConexion objSql = new ClsConexion();
        ClsErrorHandler log = new ClsErrorHandler();
        string strSql = "";     
        //Estructura de un metodo para obtener informacion de la BD
        public bool GetClienteAll()
        {
            try
            {               
                strSql = "SELECT ID_CLIENTE, NOMBRE, APELLIDO, NIT, DIRECCION, TELEFONO FROM POS.CLIENTE  ";
                DsReturn = objSql.EjectuaSQL(strSql, "Cliente");                
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetClienteByNit(ClsCliente cliente)
        {
            try
            {
                strSql = "SELECT ID_CLIENTE, NOMBRE, APELLIDO, NIT, DIRECCION, TELEFONO FROM POS.CLIENTE WHERE NIT = '" + cliente.Nit + "'";
                DsReturn = objSql.EjectuaSQL(strSql, "Cliente");
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        //Estructura de un metodo para ABC
        public bool InsertCliente(ClsCliente cliente)
        {
            strSql = "INSERT INTO POS.CLIENTE " +
                     "(ID_CLIENTE, NOMBRE, APELLIDO, NIT, DIRECCION, TELEFONO) " +
                     "VALUES " +
                     "((SELECT ISNULL(MAX(ID_CLIENTE),0) + 1 FROM POS.CLIENTE), '" + cliente.Nombre + "', '" + cliente.Apellido + "', " +
                     "'" + cliente.Nit + "', '" + cliente.Direccion + "', '" + cliente.Telefono + "')";
            return ExecuteSql(strSql);
        }

        //Metodo para modificar cliente
        public bool ModificaCliente(ClsCliente cliente)
        {
            strSql = "UPDATE POS.CLIENTE " +
                      "SET NOMBRE = '" + cliente.Nombre + "', " +
                      "APELLIDO = '" + cliente.Apellido + "', " +
                      "NIT = '" + cliente.Nit + "', " +
                      "DIRECCION = '" + cliente.Direccion + "', " +
                      "TELEFONO = " + cliente.Telefono + " " +
                      "WHERE ID_CLIENTE = " + cliente.IdCliente;

            return ExecuteSql(strSql);
        }

        //Metodo para modificar cliente
        public bool BorraCliente(ClsCliente cliente)
        {
            strSql = "DELETE FROM POS.CLIENTE " +
                      "WHERE ID_CLIENTE = " + cliente.IdCliente;

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
