using ApiBCB.AD.Models.Envases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Services.Envases
{
    public interface S_IEnvases
    {
        Task<List<M_Envases>> GetEnvasesAsync();
        Task<M_Envases> GetEnvasesByIdAsync(int id_Envase);
        Task<M_Envases> GetEnvasesByNameAsync(int milimetros);
        Task<int> AddEnvasesAsync(M_Envases envases);
        Task<int> ModifyEnvasesAsync(M_Envases envases);
    }
}
