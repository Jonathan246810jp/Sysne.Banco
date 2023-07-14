using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sysne.Banco.Class
{
    public class Operaciones
    {
        private List<Cliente> Clientes = new List<Cliente>();
        private int Identificador = 0;
        #region Metodos del banco
        /// <summary>
        /// Método que se encarga de ragregar un nuevo cliente.
        /// </summary>
        /// <param name="cliente">Objeto con la inforamcion completa del cliente</param>
        /// <returns>Resultado de la operacion con un mensaje</returns>

        public (bool operacion, string mensaje) AddClinete(Cliente cliente)
        {
            var existeCliente = Clientes.FirstOrDefault(f => f.Cuenta.NumeroCuenta.Equals(cliente.Cuenta.NumeroCuenta));
            if (existeCliente != null)
                return (false, "El numero de cliente ya existe");
            //se valida el nip con el operadoor or
            else if (cliente.Cuenta.Nip.ToString().Length < 4 || cliente.Cuenta.Nip.ToString().Length > 4)
                return (false, "El NIP no es valido");
            Identificador++;
            //precaucion 
            cliente.Id = Identificador;
            cliente.Cuenta.Id = Identificador;
            cliente.Cuenta.IdCliente = Identificador;
            Clientes.Add(cliente);
            return (true, "Cliente registrado");
        }
       

      
        /// <summary>
        /// Metodo que permite acceder a la cuenta del cliente mediante el nip y l numero de cuenta
        /// </summary>
        /// <param name="nip">Parametro que tendra el nip del cliente</param>
        /// <param name="numeroCuenta">Parametro que tendra el numero de cuenta del cliente</param>
        /// <returns>Retorna un valor bolenao, un mensaje y el id del cliente y su saldo </returns>
        public ((bool operacion, string mensaje), (int id, double saldo)) AccederCuenta(int nip, int numeroCuenta)
        {
            if (nip.ToString().Length < 4 || nip.ToString().Length > 4)
                //con el (0,0) significa los valores que vamos a retornar del Id y el saldo que estaran en  0
                return ((false, "Nip invalido"), (0, 0));
            var cuentaExiste = Clientes.FirstOrDefault(f => (f.Cuenta.NumeroCuenta == numeroCuenta && f.Cuenta.Nip == nip));
            if (cuentaExiste == null)
                return ((false, "Acceso denegado, verifique su inforamcion"), (0, 0));
            return ((true, "Acceso correcto"), (cuentaExiste.Id, cuentaExiste.Cuenta.Saldo));
        }

      
        
       
        /// <summary>
        /// Metodo para depositar a la cuenta del cliente 
        /// </summary>
        /// <param name="idCliente">Parametro que tendra el id cliente de la clase Cuenta</param>
        /// <param name="cantidad">Parametro que se ocupara incrementar el saldo </param>
        /// <returns>El resultado de la operacion y un mensaje </returns>
        public (bool operacion, string mensaje) DepocitarSaldo(int idCliente, double cantidad)
        {
            var clienteTmp = Clientes.FirstOrDefault(c => c.Id == idCliente);
            if (clienteTmp != null)
            {
                clienteTmp.Cuenta.Saldo = cantidad;
                clienteTmp.Cuenta.FechaModificacion = DateTime.Now;
                return (true, "Deposito exitoso");
            }
            return (false, "La cuenta no existe");
        }
        

       
        /// <summary>
        /// Metodo  que dismunuyee al saldo
        /// </summary>
        /// <param name="idCliente">Parametro que tendra el id cliente de la clase Cuenta</param>
        /// <param name="cantidad">Parametro que tendra se obtendra desde el program</param>
        /// <returns>Resultado de la operacion y un mensaje</returns>
        public (bool operacion, string mensaje) RetirarSaldo(int idCliente, double cantidad)
        {
            var clienteTmp = Clientes.FirstOrDefault(c => c.Id == idCliente);
            if (clienteTmp != null)
            {
                if(!ValidarSaldo(cantidad, clienteTmp.Cuenta.Saldo))
                    return (false, "Saldo no disponible");

                clienteTmp.Cuenta.Saldo = cantidad;
                clienteTmp.Cuenta.FechaModificacion = DateTime.Now;
                return (true, "Retiro exitoso");
            }
            return (false, "La cuenta no existe");
        }

        

        
        public double ConsultarSaldo(int idCliente)
        {
            var clienteTmp = Clientes.FirstOrDefault(c => c.Id == idCliente);
            if (clienteTmp != null)
            {
                return clienteTmp.Cuenta.Saldo;
            }
            return 0;
        }

         public int ObtenerNumeroDeClientesRegistrados() =>
             Clientes.Count();
       
        private bool ValidarSaldo(double saldoRetiro, double saldoActual)
        {
            if (saldoRetiro <= saldoActual)
                return true;
            return false;
        }
        #endregion

    }
}
