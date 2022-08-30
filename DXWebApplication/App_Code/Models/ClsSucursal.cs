using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Models
{
    public class ClsSucursal
    {
        private int idSucursal;
        private string descripcion;
        private string direccion;
        private int estado;

        public int IdSucursal
        {
            get
            {
                return idSucursal;
            }

            set
            {
                idSucursal = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }

        public int Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }
    }
}