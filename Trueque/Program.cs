using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Trueque
{
    internal class Program
    {
        
        static void Main(string[] args) {

            LeerObjetos();
            //LeerObjetosNoDisp();
            
            //GuardarListaObjetosNoDisp();

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
                            AgregarObjeto();
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
                        case "4":
                            //guardar cambios
                            GuardarListaObjetos();
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
        private static List<Objeto> objetosNoDisp = new List<Objeto>();
        private static List<string> historialTrueques = new List<string>();
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
        public static void LeerObjetosNoDisp() {
            string ruta = "ObjetosNoDisponibles.txt";
            using (StreamReader sr = new StreamReader(ruta)) {
                try {
                    string linea;
                    linea = sr.ReadLine();
                    do {
                        string[] campos = linea.Split('|');
                        objetosNoDisp.Add(new Objeto(Int32.Parse(campos[0]), campos[1], campos[2], Int32.Parse(campos[3]), campos[4], campos[5], campos[6], campos[7]));
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
        private static void GuardarListaObjetosNoDisp() {
            string ruta = "ObjetosNoDisponibles.txt";

            StreamWriter sw = new StreamWriter(ruta, false);

            try {
                //Guardar lista losObjetos
                foreach (Objeto obj in objetosNoDisp) {
                    sw.WriteLine(obj.Id.ToString() + "|" + obj.Descripcion.ToString() + "|" + obj.NombrePropietario.ToString() + "|" + obj.Valor.ToString() + "|" + obj.Preferencia1.ToString() + "|" + obj.Preferencia2.ToString() + "|" + obj.Preferencia3.ToString() + "|" + obj.FechaIngreso.ToString());
                }
            } catch (Exception ex) {
                Console.WriteLine("Error ->" + ex.ToString());
            } finally {
                sw.Dispose();
                sw.Close();
            }
        }
        public static void AgregarObjeto() {
            Console.Clear();
            Objeto obj=new Objeto();
            bool agregar = false;
            bool cancelar = false;
            string opc = "";
            while (!agregar && !cancelar) {
                string agregarobj = "Seleccione los campos del objeto a ingresar \n" +
                    "\n" +
                    "1- Id: " + obj.Id + "\n" +
                    "2- Descripcion: " + obj.Descripcion + "\n" +
                    "3- Nombre del Propietario: " + obj.NombrePropietario + "\n" +
                    "4- Valor aproximado del Objeto: " + obj.Valor + "\n" +
                    "5- Preferencia N°1 para el intercambio: " + obj.Preferencia1 + "\n" +
                    "6- Preferencia N°2 para el intercambio: " + obj.Preferencia2 + "\n" +
                    "7- Preferencia N°3 para el intercambio: " + obj.Preferencia3 + "\n" +
                    "8- Fecha de Ingreso del objeto: " + obj.FechaIngreso + "\n" +
                    "9- Guardar \n" +
                    "10- Cancelar \n";
                Console.WriteLine(agregarobj);
                opc = Console.ReadLine();
                Console.Clear();
                if (opc == "1" | opc == "2" | opc == "3" | opc == "4" | opc == "5" | opc == "6" | opc == "7" | opc == "8" | opc == "9" | opc == "10") {
                    switch (opc) {
                        case "1":
                            bool num = false;
                            while (!num) {
                                int id;
                                Console.WriteLine("Ingrese numero de ID: (distinto a 0)");
                                string r = Console.ReadLine();
                                num = int.TryParse(r, out id);
                                if (num) {
                                    Console.Clear();
                                    id = Int32.Parse(r);
                                    //verificar que el id no se repita en la lista
                                    Objeto compararObj = BuscarObj(id);
                                    if (compararObj.Id != id) {
                                        obj.Id = id;
                                        num = true;
                                    } else {
                                        Console.WriteLine("Ya existe un objeto con el registro ingresado, ingrese un Id diferente");
                                        num = true;
                                    }
                                } else {
                                    Console.Clear();
                                    Console.WriteLine("Ingrese solamente numeros");
                                }
                            }
                            break;
                        case "2":
                            string desc = "";
                            while (desc == "") {
                                Console.WriteLine("Ingrese descripcion del objeto (Ejemplo : Teclado Mecanico Gamer)");
                                desc = Console.ReadLine();
                                Console.Clear();
                            }
                            if (desc != "") obj.Descripcion = desc;
                            break;
                        case "3":
                            string NomProp = "";
                            while (NomProp == "") {
                                Console.WriteLine("Ingrese nombre del propietario del objeto (Formato: Nombre Apellido)");
                                NomProp = Console.ReadLine();
                                Console.Clear();
                            }
                            if (NomProp != "") obj.NombrePropietario = NomProp;
                            break;
                        case "4":
                            bool numValor = false;
                            while (!numValor) {
                                int valor;
                                Console.WriteLine("Ingrese el valor aproximado del objeto (Ingresar solo numeros)");
                                string res = Console.ReadLine();
                                numValor = int.TryParse(res, out valor);
                                if (numValor) {
                                    valor = Int32.Parse(res);
                                    Console.Clear();
                                    obj.Valor = valor;
                                } else {
                                    Console.Clear();
                                    Console.WriteLine("Ingrese solamente valores numericos");
                                }
                            }
                            break;
                        case "5":
                            string pref1 = "";
                            while (pref1 == "") {
                                Console.WriteLine("Ingrese primer objeto de preferencia para el intercambio");
                                pref1 = Console.ReadLine();
                                Console.Clear();
                            }
                            if (pref1 != "") obj.Preferencia1 = pref1;
                            break;
                        case "6":
                            string pref2 = "";
                            while (pref2 == "") {
                                Console.WriteLine("Ingrese segundo objeto de preferencia para el intercambio");
                                pref2 = Console.ReadLine();
                                Console.Clear();
                            }
                            if (pref2 != "") obj.Preferencia2 = pref2;
                            break;
                        case "7":
                            string pref3 = "";
                            while (pref3 == "") {
                                Console.WriteLine("Ingrese tercer objeto de preferencia para el intercambio");
                                pref3 = Console.ReadLine();
                                Console.Clear();
                            }
                            if (pref3 != "") obj.Preferencia3 = pref3;
                            break;
                        case "8":
                            string fechaIng = "";
                            while (fechaIng == "") {
                                Console.WriteLine("Ingrese 1 para guardar fecha Actual: " + DateTime.Today);
                                fechaIng = Console.ReadLine();
                                Console.Clear();
                            }
                            if (fechaIng == "1") obj.FechaIngreso = DateTime.Today.ToString();
                            else {
                                Console.WriteLine("Es obligatorio guardar la fecha del registro del objeto!");
                            }
                            break;
                        case "9":
                            Console.Clear();
                            if (!agregar) {
                                if (obj.Id == 0) Console.WriteLine("Debe asignar un id al objeto");
                                if (obj.Descripcion == "" | obj.Descripcion == null) Console.WriteLine("Debe asignar una descripcion al objeto");
                                if (obj.NombrePropietario == "" | obj.NombrePropietario == null) Console.WriteLine("Debe asignar nombre del propietario del objeto");
                                if (obj.Valor == 0) Console.WriteLine("Debe ingresar valor aproximado del objeto");
                                if (obj.Id != 0 && obj.Valor != 0 && (obj.Descripcion != null && obj.Descripcion != "") && (obj.NombrePropietario != null && obj.NombrePropietario != "")
                                    && (obj.Preferencia1 != null && obj.Preferencia1 != "") && (obj.Preferencia2 != null && obj.Preferencia2 != "")
                                    && (obj.Preferencia3 != null && obj.Preferencia3 != "") && (obj.FechaIngreso != null && obj.FechaIngreso != "")) {
                                    string opcAgregar = "";
                                    while (opcAgregar == "") {
                                        Console.WriteLine("¿Desea agregar al jugador el objeto con los siguientes datos?: \n" +
                                            "\n" +
                                            "-ID: " + obj.Id + "\n" +
                                            "-Descripcion: " + obj.Descripcion + "\n" +
                                            "-Nombre Propietario: " + obj.NombrePropietario + "\n" +
                                            "-Valor aproximado: " + obj.Valor + "\n" +
                                            "-Preferencia 1: " + obj.Preferencia1 + "\n" +
                                            "-Preferencia 2: " + obj.Preferencia2 + "\n" +
                                            "-Preferencia 3: " + obj.Preferencia3 + "\n" +
                                            "-Fecha de ingreso: " + obj.FechaIngreso + "\n" +
                                            "\n" +
                                            "-1 Guardar \n" +
                                            "-2 Cancelar \n");
                                        opcAgregar = Console.ReadLine();
                                        if (opcAgregar == "1" | opcAgregar == "2") {
                                            switch (opcAgregar) {
                                                case "1":
                                                    losObjetos.Add(obj);
                                                    Console.WriteLine("Se agrego el objeto correctamente. Presione una tecla para continuar");
                                                    agregar = true;
                                                    break;
                                                case "2":
                                                    Console.Clear();
                                                    break;
                                            }
                                        } else {
                                            Console.Clear();
                                            Console.WriteLine("Seleccione una opcion valida");
                                            opcAgregar = "";
                                        }
                                    }
                                }
                            }
                            break;
                        case "10":
                            cancelar = true;
                            Console.WriteLine("Se cancela el registro del objeto. Presione cualquier tecla para continuar");
                            break;

                    }
                } else {
                    opc = "";
                    Console.Clear();
                    Console.WriteLine("Opcion ingresada no valida, ingrese una opcion valida");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
        
        public static Objeto BuscarObj(int ids) {
            Objeto obj = new Objeto();
            List<Objeto> idObjs = (from id in losObjetos
                                where id.Id == ids
                                select id).ToList();
            foreach (Objeto id in idObjs) {
                Console.WriteLine("ID: "+id.Id+" Descripcion: "+id.Descripcion+" Nombre Propietario: "+id.NombrePropietario+" Valor aprox. Objeto: "+id.Valor);
                obj = id;
            }          
            return obj;
        }
    }
}
