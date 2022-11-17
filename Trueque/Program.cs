using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trueque
{
    internal class Program
    {
        static void Main(string[] args) {
            string Menu = "Bienvenido al sistema de gestion de trueques \n" +
                        "1- Clientes \n" +
                        "2- Consultar Objetos \n" +
                        "3- Gestionar Trueque \n" +
                        "4- Salir";
            string opcion = "";
            while (opcion == "") {
                Console.WriteLine(Menu);
                opcion=Console.ReadLine();
                if (opcion == "1" | opcion == "2" | opcion == "3" | opcion == "4") {
                    switch (opcion) {
                        case "1":
                            //Clientes (sub menu con 2 metodos)
                            //1- Agregar cliente (permite agregar un cliente)
                            //2- Agregar producto (permite agregar un producto asignandoselo a un cliente)
                            break;
                        case "2":
                            //Consultar objetos(sub menu con 2 metodos)
                            //este metodo debe contener un sub-menu en donde permita consultar todos los objetos,
                            //y otro en donde permita consultar los objetos por antiguedad desde el mas antiguo al mas nuevo
                            break;
                        case "3":
                            //Gestionar trueque(metodo)
                            //Esta opcion permite permutar 2 productos, en un inicio se debe buscar en la lista de objetos el 1er objeto a permutar
                            //luego de seleccionar el primer producto, se muestra en pantalla y permite buscar el 2do objeto a permutar,
                            //cuando esten los 2 productos seleccionados se muestra en pantalla si desea realizar el cambio
                            //al realizar el cambio hay que guardar los objetos como strings en una lista que se debe llamar trueques
                            //despues de guardar en la lista se deben eliminar los objetos seleccionados de la lista objetos
                            break;
                    }
                        if (opcion == "1" | opcion == "2" | opcion == "3") {
                        opcion = "";
                        } 
                    } else {
                    opcion = "";
                    Console.Clear();
                    Console.WriteLine("Opcion ingresada no valida, Reingrese una opcion valida");
                }
            }
            //metodos

        }
    }
}
