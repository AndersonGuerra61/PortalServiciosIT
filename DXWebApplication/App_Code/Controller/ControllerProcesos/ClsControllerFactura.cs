using DXWebApplication.App_Code.Dal.DaoMantenimientos;
using DXWebApplication.App_Code.Dal.DaoProcesos;
using DXWebApplication.App_Code.Models;
using DXWebApplication.App_Code.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Controller.ControllerProcesos
{
    public class ClsControllerFactura
    {
        ClsErrorHandler log = new ClsErrorHandler();
        ClsDaoFactura objFactura = new ClsDaoFactura();

        public bool InsertarFactura(ClsFactura Factura, List<ClsDetalleFactura> LstDetalleFactura)
        {
            try
            {
                if (objFactura.InsertarFactura(Factura, LstDetalleFactura))
                    return true;
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