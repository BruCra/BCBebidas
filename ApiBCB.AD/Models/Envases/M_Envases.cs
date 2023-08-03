using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Models.Envases
{
    public class M_Envases
    {
        public int Id_Envase { get; set; }
        public string Material { get; set; } = string.Empty;
        public int Milimetros { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
