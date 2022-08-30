using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Dal.DaoMantenimientos
{
    public class ClsDaoServicio : ClsDataLayer
    {
        ClsConexion objSql = new ClsConexion();
        ClsErrorHandler log = new ClsErrorHandler();
        string strSql = "";
        //Estructura de un metodo para obtener informacion de la BD
        public bool GetServicioAll()
        {
            try
            {
                strSql = "SELECT ID_SERVICIO, DESCRIPCION FROM SERVICIO ";
                DsReturn = objSql.EjectuaSQL(strSql, "Servicio");
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