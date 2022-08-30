using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXWebApplication.App_Code.Models
{
    public class ClsProducto
    {
        public int IdProducto { get; set; }

        public int IdTipoProducto { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Existencia { get; set; }

        public int Estado { get; set; }
    }
}