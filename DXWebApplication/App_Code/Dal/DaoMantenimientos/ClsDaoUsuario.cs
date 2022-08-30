using DXWebApplication.App_Code.Models;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Dal.DaoMantenimientos
{
    public class ClsDaoUsuario : ClsDataLayer
    {
        ClsConexion objSql = new ClsConexion();
        ClsErrorHandler log = new ClsErrorHandler();
        string strSql = "";
        //Estructura de un metodo para obtener informacion de la BD
        public bool GetUsuarioAll()
        {
            try
            {
                strSql = "SELECT ID_USUARIO, NOMBRE, APELLIDO, CORREO, DIRECCION, TELEFONO FROM USUARIO ";
                DsReturn = objSql.EjectuaSQL(strSql, "Usuario");
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return true;
        }

        public bool GetUsuarioByCorreo(ClsUsuario usuario)
        {
            try
            {
                strSql = "SELECT U.ID_USUARIO, U.NOMBRE, U.APELLIDO, U.CORREO, U.DIRECCION, U.TELEFONO, A.ID_AREA, A.DESCRIPCION AREA " +
                         "FROM USUARIO U " +
                         "JOIN   AREA A " +
                         "ON U.ID_AREA = A.ID_AREA " +
                         "WHERE U.CORREO = '" + usuario.Correo + "'";
                DsReturn = objSql.EjectuaSQL(strSql, "Usuario");
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