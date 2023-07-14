using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sysne.Banco.Class
{
    public class Cliente
    {
     /// <summary>
     /// modelo
     /// </summary>
     
        
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public Cuenta Cuenta { get; set; }
    }


}
