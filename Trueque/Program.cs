using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Trueque
{
    internal class Program
    {
        
        static void Main(string[] args) {

            LeerObjetos();
            GuardarListaObjetos();

            string Menu = "Bienvenido al sistema de gestion de trueques \n" +
                        "1- Agregar Objetos \n" +
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
        }
        //metodos
        private static List<Objeto> losObjetos = new List<Objeto>();
        public static void LeerObjetos() {
            string ruta = "Objetos.txt";
            using (StreamReader sr = new StreamReader(ruta)) {
                try {
                    string linea;
                    linea = sr.ReadLine();
                    do {
                        string[] campos = linea.Split('|');
                        losObjetos.Add(new Objeto(Int32.Parse(campos[0]), campos[1], campos[2], Int32.Parse(campos[3]), campos[4], campos[5], campos[6], campos[7]));
                        linea = sr.ReadLine();
                    } while (linea != null);
                } catch (Exception e) {
                    Console.WriteLine("!!ERROR¡¡--> " + e.ToString());
                } finally {
                    sr.Dispose();
                    sr.Close();
                }
            }
        }
        private static void GuardarListaObjetos() {
            string ruta = "Objetos.txt";

            StreamWriter sw = new StreamWriter(ruta, false);

            try {
                //Elementos de prueba
                //sw.WriteLine("05|Computador hp|Erik1|250000|Monitor|PS4|Nintendo Switch");
                //sw.WriteLine("02|Computador hp|Erik2|150000|Monitor|PS4|Nintendo Switch");
                //sw.WriteLine("03|Computador hp|Erik3|450000|Monitor|PS4|Nintendo Switch");
                //sw.WriteLine("04|Computador hp|Erik4|550000|Monitor|PS4|Nintendo Switch");

                //Guardar lista losObjetos
                foreach (Objeto obj in losObjetos) { 
                    sw.WriteLine(obj.Id.ToString()+"|"+obj.Descripcion.ToString()+"|"+obj.NombrePropietario.ToString()+"|"+obj.Valor.ToString()+"|"+obj.Preferencia1.ToString()+"|"+ obj.Preferencia2.ToString()+"|"+ obj.Preferencia3.ToString()+"|"+obj.FechaIngreso.ToString());
                }
            } catch (Exception ex) {
                Console.WriteLine("Error ->" + ex.ToString());
            } finally {
                sw.Dispose();
                sw.Close();
            }
        }
    }
}
