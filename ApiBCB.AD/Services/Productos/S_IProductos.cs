using ApiBCB.AD.Models.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Services.Productos
{
    public interface S_IProductos
    {
        Task<List<M_Productos>> GetProductosAsync();
        Task<M_Productos> GetProductosByIdAsync(int id_Producto);
        Task<M_Productos> GetProductosByNameAsync(string nombreProducto);
        Task<int> AddProductosAsync(M_Productos productos);
        Task<int> ModifyProductosAsync(M_Productos productos);
    }
}
