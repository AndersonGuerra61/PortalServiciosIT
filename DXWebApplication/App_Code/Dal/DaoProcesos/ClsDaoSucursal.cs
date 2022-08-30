using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DXWebApplication.App_Code.Utilidades;
using DXWebApplication.App_Code.Models;

namespace DXWebApplication.App_Code.Dal.DaoProcesos
{
    public class ClsDaoSucursal : ClsDataLayer
    {
        ClsConexion objSql = new ClsConexion();
        ClsErrorHandler log = new ClsErrorHandler();
        string strSql = "";
        //Estructura de un metodo para obtener informacion de la BD
        public bool GetSucursalAll()
        {
            try
            {
                strSql = "SELECT ID_SUCURSAL, DESCRIPCION, DIRECCION, ESTADO FROM SUCURSAL  ";
                DsReturn = objSql.EjectuaSQL(strSql, "Sucursal");
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }
    }
}