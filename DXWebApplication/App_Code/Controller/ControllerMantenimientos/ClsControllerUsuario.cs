using DXWebApplication.App_Code.Dal;
using DXWebApplication.App_Code.Dal.DaoMantenimientos;
using DXWebApplication.App_Code.Models;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Controller.ControllerMantenimientos
{
    public class ClsControllerUsuario : ClsController
    {
        ClsErrorHandler log = new ClsErrorHandler();
        ClsDaoUsuario objUsuario = new ClsDaoUsuario();

        public bool GetUsuario()
        {
            try
            {
                if (objUsuario.GetUsuarioAll())
                {
                    DsReturn = objUsuario.DsReturn;
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return false;
        }

        public bool GetUsuarioByCorreo(ClsUsuario usuario)
        {
            try
            {
                if (objUsuario.GetUsuarioByCorreo(usuario))
                {
                    DsReturn = objUsuario.DsReturn;
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return false;
        }
    }
}