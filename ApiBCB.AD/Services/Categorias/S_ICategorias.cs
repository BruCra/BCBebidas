using ApiBCB.AD.Models.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Services.Categorias
{
    public interface S_ICategorias
    {
        Task<List<M_Categorias>> GetCategoriasAsync();
        Task<M_Categorias> GetCategoriasByIdAsync(int id_Categorias);
        Task<M_Categorias> GetCategoriasByNameAsync(string nombreCategoria);
        Task<int> AddCategoriasAsync(M_Categorias categorias);
        Task<int> ModifyCategoriasAsync(M_Categorias categorias);
    }
}
