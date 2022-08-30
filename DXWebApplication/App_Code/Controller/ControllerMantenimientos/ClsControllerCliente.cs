using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DXWebApplication.App_Code.Dal;
using DXWebApplication.App_Code.Utilidades;
using DXWebApplication.App_Code.Dal.DaoMantenimientos;
using DXWebApplication.App_Code.Models;

namespace DXWebApplication.App_Code.Controller.ControllerMantenimientos
{
    public class ClsControllerCliente : ClsController
    {
        ClsErrorHandler log = new ClsErrorHandler();
        ClsDaoCliente objCliente = new ClsDaoCliente();

        public bool GetClienteAll()
        {
            try
            {
                if (objCliente.GetClienteAll())
                {
                    DsReturn = objCliente.DsReturn;
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

        public bool GetClienteByNit(ClsCliente cliente)
        {
            try
            {
                if (objCliente.GetClienteByNit(cliente))
                {
                    DsReturn = objCliente.DsReturn;
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

        public bool InsertCliente(ClsCliente cliente)
        {
            try
            {
                if (objCliente.InsertCliente(cliente))
                    return true;                
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return false;
        }

        public bool ModificaCliente(ClsCliente cliente)
        {
            try
            {
                if (objCliente.ModificaCliente(cliente))
                    return true;
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString(), ex.StackTrace);
                return false;
            }
            return false;
        }

        public bool BorraCliente(ClsCliente cliente)
        {
            try
            {
                if (objCliente.BorraCliente(cliente))
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