using DXWebApplication.App_Code.Dal;
using DXWebApplication.App_Code.Dal.DaoMantenimientos;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Controller.ControllerMantenimientos
{
    public class ClsControllerServicio : ClsController
    {
        ClsErrorHandler log = new ClsErrorHandler();
        ClsDaoServicio objServicio = new ClsDaoServicio();

        public bool GetServicioAll()
        {
            try
            {
                if (objServicio.GetServicioAll())
                {
                    DsReturn = objServicio.DsReturn;
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