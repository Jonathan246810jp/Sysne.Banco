using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sysne.Banco.Class
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int NumeroCuenta { get; set; }
        public int Nip { get; set; }
        public double Saldo { get; set; }
        public DateTime FechaModificacion { get; set; }       
    }

    
}
