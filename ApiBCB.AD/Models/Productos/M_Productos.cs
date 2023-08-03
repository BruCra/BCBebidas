using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Models.Productos
{
    public class M_Productos
    {
        public int Id_Producto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Id_Categoria { get; set; }
        public int Id_Bodega { get; set; }
        public int Id_Envase { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra {get; set; }
        public int Descuento { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public bool Activo { get; set; }

        //JOIN Categorias
        public string NombreCategoria { get; set; } = string.Empty;

        //JOIN Bodegas
        public string NombreBodega { get; set; } = string.Empty;

        //JOIN Envases
        public string NombreEnvase { get; set; } = string.Empty;
    }
}
