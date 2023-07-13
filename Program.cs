using Sysne.Banco.Class;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sysne.Banco
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
			Operaciones operaciones = new Operaciones();
            int idCliente = 0;
            double saldo = 0;
            while (true)
            {
                WriteLine("Banco del Bienestar\n------------------------------");
                WriteLine("1: Registrar Cliente\n2: Acceder a cuenta\n0: Salir");
                var accion = ReadLine();
                if (accion == null || !IsNumero(accion))
                    WriteLine("Accion no valida");
                else
                {
                    if (accion == "0")
                        break;

                    switch (accion)
                    {
                        case "1":
                            Cliente cliente = new Cliente()
                            {
                                Cuenta = new Cuenta()
                              
                            };
                            WriteLine("Datos Personales\n--------------------------------");
                            WriteLine("Nombre completo");
                            cliente.NombreCompleto = ReadLine();
                            WriteLine("Edad");
                            cliente.Edad = int.Parse(ReadLine());
                            WriteLine("Telefono");
                            cliente.Telefono = ReadLine();
                            WriteLine("Dirección");
                            cliente.Direccion = ReadLine();
                            WriteLine("Sexo F o M");
                            cliente.Sexo = ReadLine();

                            while(true)
                            {
                                WriteLine("Datos Bancarios\n--------------------------------");
                                WriteLine("Numero de cuenta");
                                cliente.Cuenta.NumeroCuenta = int.Parse(ReadLine());
                                WriteLine("NIP");
                                cliente.Cuenta.Nip = int.Parse(ReadLine());
                                var (operacion, mensaje) = operaciones.AddClinete(cliente);
                                if (operacion)
                                {
                                    WriteLine(mensaje);
                                    break;
                                }
                                WriteLine(mensaje);
                            }                         
                            break;
                        case "2":
                            var numeroClientes = operaciones.ObtenerNumeroDeClientesRegistrados();
                            if(numeroClientes == 0)
                            {
                                WriteLine("No hay clientes registrados \n--------------------------------");
                                break;
                            }
                            WriteLine("Acceder a mi cuenta Bienestar \n--------------------------------");
                            WriteLine("Numero de cuenta");
                            int numeroCuenta = int.Parse(ReadLine());
                            WriteLine("NIP");
                            int nip = int.Parse(ReadLine());


                            ((bool operacion, string menssage), (int id, double saldo)) acceso = operaciones.AccederCuenta(nip, numeroCuenta);

                            if (acceso.Item1.operacion)
                            {
                                idCliente = acceso.Item2.id;
                                saldo = acceso.Item2.saldo;
                                WriteLine($"{acceso.Item1.menssage}\n--------------------------------");
                                while (true)
                                {
                                    WriteLine("Banco del bienestar (Mi cuenta) \n--------------------------------");
                                    WriteLine("1: Depocitar\n2: Retirar\n3: Consular\n0: Salir");
                                    var accionMiCuenta = ReadLine();
                                    if (accionMiCuenta == null || !IsNumero(accionMiCuenta))
                                        WriteLine("Accion no valida");
                                    else
                                    {
                                        if (accionMiCuenta == "0")
                                        {
                                            idCliente = 0;
                                            saldo = 0;
                                            break;
                                        }
                                        switch (accionMiCuenta)
                                        {
                                            case "1":
                                                WriteLine("Cantidada a depocitar");
                                                double saldoDeposito = double.Parse(ReadLine());
                                                var resultDeposito =  operaciones.DepocitarSaldo(idCliente, saldo + saldoDeposito);
                                                if (resultDeposito.operacion)
                                                    saldo += saldoDeposito;
                                                WriteLine(resultDeposito.mensaje);
                                                break;
                                            case "2":
                                                WriteLine("Cantidada a retirar");
                                                double saldoRetiro = double.Parse(ReadLine());
                                                var resultRetiro = operaciones.RetirarSaldo(idCliente, saldoRetiro);
                                                if (resultRetiro.operacion)
                                                    saldo -= saldoRetiro;
                                                WriteLine(resultRetiro.mensaje);
                                                break;
                                            case "3":
                                                WriteLine($"Tu saldo actual es: {operaciones.ConsultarSaldo(idCliente)}\n--------------------");
                                                break;
                                            case "4":
                                                WriteLine($"Tu saldo actual es: {operaciones.ConsultarSaldo(idCliente)}\n--------------------");
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                                WriteLine($"{acceso.Item1.menssage}\n--------------------------------");
                            break;
                    }
                }               
            }
        }

        public static bool IsNumero(string numero) =>
             int.TryParse(numero, out _);
    }
}
