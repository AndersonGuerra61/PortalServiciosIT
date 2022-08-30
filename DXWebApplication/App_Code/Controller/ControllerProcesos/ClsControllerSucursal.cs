using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DXWebApplication.App_Code.Dal;
using DXWebApplication.App_Code.Utilidades;
using DXWebApplication.App_Code.Dal.DaoProcesos;
using DXWebApplication.App_Code.Models;

namespace DXWebApplication.App_Code.Controller.ControllerProcesos
{
    public class ClsControllerSucursal : ClsController
    {
        ClsErrorHandler log = new ClsErrorHandler();
        ClsDaoSucursal objSucursal = new ClsDaoSucursal();

        public bool GetSucursalAll()
        {
            try
            {
                if (objSucursal.GetSucursalAll())
                {
                    DsReturn = objSucursal.DsReturn;
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