using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Trueque {
    internal class Objeto {
        //propiedades del objeto
        //-ID
        //-Descripcion
        //-Nombre Propietario
        //-Fecha de ingreso
        //-Valor
        //-Preferencia 1
        //-Prefenrecia 2
        //-Preferencia 3

        public DateTime Fecha;
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string NombrePropietario { get; set; }
        public int Valor { get; set; }
        public string Preferencia1 { get; set; }
        public string Preferencia2 { get; set; }
        public string Preferencia3 { get; set; }
        public string FechaIngreso { get; }

        //Contructor
        public Objeto(int id, string descripcion,string nombrepropietario, int valor, string preferencia1, string preferencia2, string preferencia3,string fechaingreso) {
            Id = id;
            Descripcion = descripcion;
            NombrePropietario = nombrepropietario;
            Valor = valor;
            Preferencia1 = preferencia1;
            Preferencia2 = preferencia2;
            Preferencia3 = preferencia3;
            FechaIngreso = fechaingreso;
        }
        //Metodos
        public void ObtenerFechaIngreso() {
            try {
                Fecha = DateTime.ParseExact(FechaIngreso, "dd/MM/yyyy", null);
            } catch (Exception e){
                Console.WriteLine("Fecha ingresada invalida! \n" + e);
                return;
            }
        }
        


    }
}
