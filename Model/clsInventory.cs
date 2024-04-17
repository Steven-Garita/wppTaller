using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpp_Taller.Model
{
    internal class clsInventory
    {
        public clsInventory() { }
        public int idInventario { get; set; }
        public string nombreRpuesto { get; set; }
        public string disponibilidad { get; set; }
        public string estadoRepuesto { get; set; }
        public string marcaAuto { get; set; }
        public string modeloAuto { get; set; }
        public string otros { get; set; }
        public decimal precio { get; set; }
        public int unidades { get; set; }
    }
}
