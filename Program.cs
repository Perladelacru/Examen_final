// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;


//Para la prueba se le ha asignado el codigo del empleado como ABCD y la contraseña como  "SDC123".

namespace ExamenFinal
{

    internal class Program
    {

        static void Main(string[] args)
        {

            
            AgenteTransito agenteTransito = new AgenteTransito
            {
                CodigoEmpleado = "ABCD",
                NombreCompleto = "Julio Alcantara",
                Direccion = "Calle #2, Los Guayabos",
                FechaIngreso = new DateTime(2015, 7, 1)
            };
            Console.WriteLine("el codigo del empleado es ABCD y la contraseña es SDC123");
            Console.WriteLine("Saludos, favor ingresar su codigo de empleado:");
            string opcionA = Console.ReadLine();

            Console.WriteLine("Favor ingresar su contraseña:");
            string contraseña = "SDC123";
            string opcionB = Console.ReadLine();


            if (opcionA == agenteTransito.CodigoEmpleado && opcionB == contraseña)
            {
                Console.WriteLine("Ingresar el nombre completo del conductor:");
                string nombreCompleto = Console.ReadLine();

                Console.WriteLine("Ingresar la cédula del conductor:");
                string cedula = Console.ReadLine();

                Console.WriteLine("Ingresar la dirección del conductor:");
                string direccion = Console.ReadLine();

                Console.WriteLine("Ingresar el tipo de licencia del conductor (TipoA, TipoB o TipoC):");
                string tipoLicenciaString = Console.ReadLine();
                TipoLicencia tipoLicencia;
                Enum.TryParse<TipoLicencia>(tipoLicenciaString, out tipoLicencia);


                Console.WriteLine("Ingresar la placa del vehículo:");
                string placa = Console.ReadLine();

                Console.WriteLine("Ingresar la marca del vehículo:");
                string marca = Console.ReadLine();

                Console.WriteLine("Ingresar el modelo del vehículo:");
                string modelo = Console.ReadLine();

                Console.WriteLine("Ingresar el color del vehículo:");
                string color = Console.ReadLine();

                Console.WriteLine("Ingresar el año del vehículo:");
                int anio = int.Parse(Console.ReadLine());

                Console.WriteLine("Ingresar el número de chasis del vehículo:");
                string numeroChasis = Console.ReadLine();

                Console.WriteLine("Ingresar la fecha de nacimiento del conductor:");
                DateTime fechaNacimiento;
                while (!DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
                {
                    Console.WriteLine("Fecha inválida. Favor ingresar la fecha en formato dd/mm/yyyy:");
                }

                
                Conductor conductor = new Conductor
                {
                    NombreCompleto = nombreCompleto,
                    Cedula = cedula,
                    Direccion = direccion,
                    TipoLicencia = tipoLicencia,
                    FechaNacimiento = fechaNacimiento,
                    Vehiculo = new Vehiculo
                    {
                        Placa = placa,
                        Marca = marca,
                        Modelo = modelo,
                        Color = color,
                        Anio = anio,
                        NumeroChasis = numeroChasis
                    }
                };



                
                GestionInfracciones gestionInfracciones = new GestionInfracciones();

               
                gestionInfracciones.RegistrarInfraccion(TipoInfraccion.PaseSemaforoRojo, conductor, agenteTransito);

               
                foreach (Infraccion infraccion in gestionInfracciones.ObtenerInfracciones())
                {
                    Console.WriteLine($"Tipo: {infraccion.Tipo}, Penalidad: RD${infraccion.Penalidad}, Fecha: {infraccion.Fecha}, Conductor: {infraccion.Conductor.NombreCompleto}, Agente de Tránsito: {infraccion.AgenteTransito.NombreCompleto}");
                }


            }
            else
            {
                Console.WriteLine("Lo sentimos, su acceso ha sido denegado");
                return;
            }

        }

    }

public class Conductor
    {
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public TipoLicencia TipoLicencia { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }

    public enum TipoLicencia
    {
        TipoA,
        TipoB,
        TipoC
    }

    public class Vehiculo
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public int Anio { get; set; }
        public string NumeroChasis { get; set; }
    }

    public class AgenteTransito
    {
        public string CodigoEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaIngreso { get; set; }
    }

    public class Infraccion
    {
        public TipoInfraccion Tipo { get; set; }
        public decimal Penalidad { get; set; }
        public DateTime Fecha { get; set; }
        public Conductor Conductor { get; set; }
        public AgenteTransito AgenteTransito { get; set; }
    }

    public enum TipoInfraccion
    {
        ObstruccionTransito,
        PaseSemaforoRojo,
        HablarCelular,
        ConducirSinCinturon,
        LicenciaVencida
    }


    public class GestionInfracciones
    {
        private List<Infraccion> infracciones = new List<Infraccion>();

        public void RegistrarInfraccion(TipoInfraccion tipo, Conductor conductor, AgenteTransito agenteTransito)
        {
            decimal penalidad = 0;

            switch (tipo)
            {
                case TipoInfraccion.ObstruccionTransito:
                    penalidad = 1800;
                    break;
                case TipoInfraccion.PaseSemaforoRojo:
                    penalidad = 5950;
                    break;
                case TipoInfraccion.HablarCelular:
                    penalidad = 3750;
                    break;
                case TipoInfraccion.ConducirSinCinturon:
                    penalidad = 2560;
                    break;
                case TipoInfraccion.LicenciaVencida:
                    penalidad = 3890;
                    break;
                default:
                    throw new ArgumentException("Tipo de infracción inválido");
            }

            Infraccion infraccion = new Infraccion
            {
                Tipo = tipo,
                Penalidad = penalidad,
                Fecha = DateTime.Now,
                Conductor = conductor,
                AgenteTransito = agenteTransito
            };

            infracciones.Add(infraccion);
        }

        public IEnumerable<Infraccion> ObtenerInfracciones()
        {
            return infracciones;
        }
    }

}