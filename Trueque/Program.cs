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
                        "1- Agregar Objeto \n" +
                        "2- Consultar Objetos \n" +
                        "3- Gestionar Trueque \n" +
                        "4- Guardar cambios \n" +
                        "5- Salir";
            string opcion = "";
            while (opcion == "") {
                Console.WriteLine(Menu);
                opcion=Console.ReadLine();
                if (opcion == "1" | opcion == "2" | opcion == "3" | opcion == "4" | opcion == "5") {
                    switch (opcion) {
                        case "1":
                            //Se agrega el objeto
                            //se buscan preferencias
                            //si hay match se despliega el objeto en pantalla
                            //se consulta si desea realizar el cambio
                            //se realiza el cambio de objetos, guardando el trueque en el hitorico y eliminacion de ambos objetos en la lista objetos
                            break;
                        case "2":
                            //Consultar objetos
                            // -1
                            //  -1.1 buscar objetos por nombre y/o id
                            //  -1.2 desplegar lista de objetos desde el mas antiguo
                            // -2 articulos no disponibles
                            //  -2.1 desplegar lista de objetos no disponibles con informcion del cliente
                            //  -2.2 buscar objetos no disp. por nombre cliente o id
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
                        if (opcion == "1" | opcion == "2" | opcion == "3" | opcion =="4") {
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
