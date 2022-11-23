﻿using System;
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
            LeerObjetosNoDisp();
            
            //GuardarListaObjetosNoDisp();

            string Menu = "Bienvenido al sistema de gestion de trueques \n" +
                        "1- Agregar Objetos \n" +
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
                            AgregarObjeto();
                            //Se agrega el objeto
                            //se buscan preferencias
                            //si hay match se despliega el objeto en pantalla
                            //se consulta si desea realizar el cambio
                            //se realiza el cambio de objetos, guardando el trueque en el hitorico y eliminacion de ambos objetos en la lista objetos
                            break;
                        case "2":
                            Buscar();
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
                        if (opcion == "1" | opcion == "2" | opcion == "3") {
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
            obj.FechaIngreso = DateTime.Today.ToString();
            bool agregar = false;
            bool cancelar = false;
            string opc = "";
            while (!agregar && !cancelar) {
                string agregarobj = "Seleccione los campos del objeto a ingresar \n" +
                    "\n" +
                    "Fecha: " + obj.FechaIngreso + "\n" +
                    "1- Id: " + obj.Id + "\n" +
                    "2- Descripcion: " + obj.Descripcion + "\n" +
                    "3- Nombre del Propietario: " + obj.NombrePropietario + "\n" +
                    "4- Valor aproximado del Objeto: " + obj.Valor + "\n" +
                    "5- Preferencia N°1 para el intercambio: " + obj.Preferencia1 + "\n" +
                    "6- Preferencia N°2 para el intercambio: " + obj.Preferencia2 + "\n" +
                    "7- Preferencia N°3 para el intercambio: " + obj.Preferencia3 + "\n" +
                    "8- Guardar \n" +
                    "9- Cancelar \n";
                Console.WriteLine(agregarobj);
                opc = Console.ReadLine();
                Console.Clear();
                if (opc == "1" | opc == "2" | opc == "3" | opc == "4" | opc == "5" | opc == "6" | opc == "7" | opc == "8" | opc == "9") {
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
                                        Console.Clear();
                                        if (opcAgregar == "1" | opcAgregar == "2") {
                                            switch (opcAgregar) {
                                                case "1":
                                                    //TO-DO: Guardar en la lista
                                                    losObjetos.Add(obj);
                                                    GuardarListaObjetos();

                                                    
                                                    Console.WriteLine("Se agrego el objeto correctamente.\n");
                                                    Console.Clear();
                                                    //realizar busqueda de coincidencias del objeto recien ingresado
                                                    string siNo = "";
                                                    while (siNo == "") { 
                                                        Console.WriteLine(
                                                            "\n"+
                                                            "Desea buscar si existen objetos que coincidan con sus preferencias?" +
                                                            "\n"+
                                                            "1-Si \n"+
                                                            "2-No ");
                                                        siNo = Console.ReadLine();
                                                        Console.Clear();
                                                        if (siNo == "1" | siNo == "2") {
                                                            switch (siNo) {
                                                                case "1":
                                                                    //se deben buscar si existen coincidencias del objeto recien ingresado
                                                                    string descripcion = obj.Descripcion;
                                                                    string preferencia1 = obj.Preferencia1;
                                                                    string preferencia2 = obj.Preferencia2;
                                                                    string preferencia3 = obj.Preferencia3;
                                                                    Console.WriteLine("Resultados de busqueda:\n ");
                                                                    List<Objeto> perfectMatch = PerfectMatch(preferencia1, descripcion);
                                                                    List<Objeto> secondaryMatch = SecondaryMatch(preferencia2, descripcion);
                                                                    List<Objeto> thirdMatch = ThirdMatch(preferencia3, descripcion);
                                                                    List<Objeto> listMatch = ListMatch(preferencia1, preferencia2, preferencia3);
                                                                    Console.WriteLine("Objetos que coinciden al 100% con su objeto y su primera preferencia: \n");
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    foreach (Objeto ob in perfectMatch) {
                                                                        Console.WriteLine("ID: " + ob.Id + "\n" + " Descripcion: " + ob.Descripcion + "\n" + " Nombre Propietario: " + ob.NombrePropietario + "\n" + " Valor aprox. Objeto: " + ob.Valor + "\n" + "Preferencia 1: " + ob.Preferencia1 + "\n" + " Preferencia 2: " + ob.Preferencia2 + "\n" + " Preferencia 3: " + ob.Preferencia3+ "\n");
                                                                    }
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    Console.WriteLine("Objetos que coinciden al 100% con su objeto y su segunda preferencia: \n");
                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                    foreach (Objeto ob in secondaryMatch) {
                                                                        Console.WriteLine("ID: " + ob.Id + "\n" + " Descripcion: " + ob.Descripcion + "\n" + " Nombre Propietario: " + ob.NombrePropietario + "\n" + " Valor aprox. Objeto: " + ob.Valor + "\n" + "Preferencia 1: " + ob.Preferencia1 + "\n" + " Preferencia 2: " + ob.Preferencia2 + "\n" + " Preferencia 3: " + ob.Preferencia3+"\n");
                                                                    }
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    Console.WriteLine("Objetos que coinciden al 100% con su objeto y su tercera preferencia: \n");
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    foreach (Objeto ob in thirdMatch) {
                                                                        Console.WriteLine("ID: " + ob.Id + "\n" + " Descripcion: " + ob.Descripcion + "\n" + " Nombre Propietario: " + ob.NombrePropietario + "\n" + " Valor aprox. Objeto: " + ob.Valor + "\n" + "Preferencia 1: " + ob.Preferencia1 + "\n" + " Preferencia 2: " + ob.Preferencia2 + "\n" + " Preferencia 3: " + ob.Preferencia3+ "\n" );
                                                                    }
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    Console.WriteLine("Todos los objetos que cumplen con sus preferencias: \n");
                                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                                    foreach (Objeto ob in listMatch) {
                                                                        Console.WriteLine("ID: " + ob.Id + "\n"+"Descripcion: " + ob.Descripcion + "\n" + " Nombre Propietario: " + ob.NombrePropietario + "\n" + " Valor aprox. Objeto: " + ob.Valor + "\n" + "Preferencia 1: " + ob.Preferencia1 + "\n" + " Preferencia 2: " + ob.Preferencia2 + "\n" + " Preferencia 3: " + ob.Preferencia3+ "\n" );
                                                                    }
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    break;
                                                                case "2":
                                                                    Console.Clear();
                                                                    break;
                                                            
                                                            }
                                                        }
                                                    }
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
                        case "9":
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
                obj = id;
            }          
            return obj;
        }
        public static Objeto BuscarObjNoDisp(int ids) {
            Objeto obj = new Objeto();
            List<Objeto> objs = (from o in objetosNoDisp
                                   where o.Id == ids
                                   select o).ToList();
            foreach (Objeto o in objs) {
                obj = o;
            }
            return obj;
        }
        public static List<Objeto> BuscarObj(string txt) {
            Objeto obj = new Objeto();
            List<Objeto> objs = (from o in losObjetos
                                where o.Descripcion.Contains(txt)
                                select o).ToList();
                    
            return objs;
        }
        public static List<Objeto> BuscarObjNoDisp(string txt) {
            Objeto obj = new Objeto();
            List<Objeto> objs = (from o in objetosNoDisp
                                   where (o.Descripcion.Contains(txt))
                                   select o).ToList();
            return objs;
        }

        public static List<Objeto> PerfectMatch(string pref1, string desc) {
            Objeto obj = new Objeto();
            List<Objeto> prefObj = (from prefUno in losObjetos
                                    where prefUno.Descripcion == pref1 && prefUno.Preferencia1 == desc 
                                    select prefUno).ToList();
            /*
            foreach (Objeto prefUno in prefObj) {
                Console.WriteLine("ID: " + prefUno.Id + " Descripcion: " + prefUno.Descripcion + " Nombre Propietario: " + prefUno.NombrePropietario + " Valor aprox. Objeto: " + prefUno.Valor + "Preferencia 1: " + prefUno.Preferencia1 + " Preferencia 2: " + prefUno.Preferencia2 + " Preferencia 3: " + prefUno.Preferencia3 );
                obj = prefUno;
            }*/
            return prefObj;
        }
        public static List<Objeto> SecondaryMatch(string pref2, string desc) {
            Objeto obj = new Objeto();
            List<Objeto> prefObj = (from prefDos in losObjetos
                                    where prefDos.Descripcion == pref2 && prefDos.Preferencia2 == desc
                                    select prefDos).ToList();
            /*
            foreach (Objeto prefDos in prefObj) {
                Console.WriteLine("ID: " + prefDos.Id + " Descripcion: " + prefDos.Descripcion + " Nombre Propietario: " + prefDos.NombrePropietario + " Valor aprox. Objeto: " + prefDos.Valor + "Preferencia 1: " + prefDos.Preferencia1 + " Preferencia 2: " + prefDos.Preferencia2 + " Preferencia 3: " + prefDos.Preferencia3);
                obj = prefDos;
            }*/

            return prefObj;
        }
        public static List<Objeto> ThirdMatch(string pref3, string desc) {
            Objeto obj = new Objeto();
            List<Objeto> prefObj = (from prefTres in losObjetos
                                    where prefTres.Descripcion == pref3 && prefTres.Preferencia3 == desc
                                    select prefTres).ToList();
            /*
            foreach (Objeto prefTres in prefObj) {
                Console.WriteLine("ID: " + prefTres.Id + " Descripcion: " + prefTres.Descripcion + " Nombre Propietario: " + prefTres.NombrePropietario + " Valor aprox. Objeto: " + prefTres.Valor + "Preferencia 1: " + prefTres.Preferencia1 + " Preferencia 2: " + prefTres.Preferencia2 + " Preferencia 3: " + prefTres.Preferencia3);
                obj = prefTres;
            }*/

            return prefObj;
        }
        public static List<Objeto> ListMatch(string pref1, string pref2, string pref3) {
            Objeto obj = new Objeto();
            List<Objeto> descObj = (from descripcion in losObjetos
                                    where descripcion.Descripcion == pref1 || descripcion.Descripcion == pref2 || descripcion.Descripcion == pref3
                                    select descripcion).ToList();
            //ToDo eliminar ya que este foreach es solo para mostrar en consola
            /*
            foreach (Objeto descripcion in descObj) {
                Console.WriteLine("ID: " + descripcion.Id + " Descripcion: " + descripcion.Descripcion + " Nombre Propietario: " + descripcion.NombrePropietario + " Valor aprox. Objeto: " + descripcion.Valor + "Preferencia 1: " + descripcion.Preferencia1 + " Preferencia 2: " + descripcion.Preferencia2 + " Preferencia 3: " + descripcion.Preferencia3);
                obj = descripcion;
            }*/
            return descObj;
        }

        public static void Buscar() {
            string MenuBuscar = "Indique la opción de búsqueda: \n" +
                        "1 = Buscar objetos disponibles \n" +
                        "2 = Buscar objetos no disponibles \n" +
                        "3 = Salir \n";
            string opc = "";
            while (opc!="3") {
                Console.Clear();
                Console.WriteLine(MenuBuscar);
                opc= Console.ReadLine();
                if(opc=="1" | opc=="2" | opc=="3"){
                    Console.Clear();
                    Boolean num = false;
                    if (opc == "1") {
                        Console.WriteLine("Ingrese texto o id a buscar:");
                        string txtBsc = Console.ReadLine();
                        int id;
                        num = int.TryParse(txtBsc, out id);
                        if (num) {
                            Console.Clear();
                            id = Int32.Parse(txtBsc);
                            Objeto obj = BuscarObj(id);
                            if (obj.Id != id) {
                                MostrarMensajeError("No existe el id indicado en los registros. Presione cualquier tecla para continuar.");
                            } else {
                                MostrarObjeto(obj);
                                num = false;
                            }
                        } else {
                            Console.Clear();
                            List<Objeto> lstObjEnc = BuscarObj(txtBsc);
                            MostrarObjeto(lstObjEnc);
                        }
                    }
                    if (opc == "2") {
                        Console.WriteLine("Ingrese texto o id a buscar:");
                        string txtBsc = Console.ReadLine();
                        int id;
                        num = int.TryParse(txtBsc, out id);
                        if (num) {
                            Console.Clear();
                            id = Int32.Parse(txtBsc);
                            Objeto obj = BuscarObjNoDisp(id);
                            if (obj.Id != id) {
                                MostrarMensajeError("No existe el id indicado en los registros. Presione cualquier tecla para continuar.");
                            } else {
                                MostrarObjeto(obj);
                                num = false;
                            }
                        } else {
                            Console.Clear();
                            List<Objeto> lstObjEnc = BuscarObjNoDisp(txtBsc);
                            MostrarObjeto(lstObjEnc);
                        }
                    }

                } else {
                    MostrarMensajeError("La opción indicada no existe, favor indicar una opción válida");
                }


            }
        }
        public static void MostrarMensajeError (string txt) {
            Console.Clear() ;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(txt);
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        public static void MostrarObjeto(Objeto obj) {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|","ID","Descripción","Propietario","Valor","Preferencia 1","Preferencia 2","Preferencia3"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(obj.Id.ToString(),6), CortarTexto(obj.Descripcion,20), CortarTexto(obj.NombrePropietario,15), CortarTexto(obj.Valor.ToString(),8), CortarTexto(obj.Preferencia1,20), CortarTexto(obj.Preferencia2,20), CortarTexto(obj.Preferencia3,20)));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.ReadKey();

        }

        public static void MostrarObjeto(List<Objeto> lstObj) {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|","ID","Descripción","Propietario","Valor","Preferencia 1","Preferencia 2","Preferencia3"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            if(lstObj.Count!=0) {
                foreach (Objeto obj in lstObj) {
                    Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(obj.Id.ToString(),6), CortarTexto(obj.Descripcion,20), CortarTexto(obj.NombrePropietario,15), CortarTexto(obj.Valor.ToString(),8), CortarTexto(obj.Preferencia1,20), CortarTexto(obj.Preferencia2,20), CortarTexto(obj.Preferencia3,20)));
                }
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                Console.ReadKey();
            } else {
                MostrarMensajeError("No se encontraron objetos. Presione cualquier tecla para continuar.");
           
            }
        }

        public static string CortarTexto(string txt, int ind) {
            string txtC = "";
            if(txt.Length > ind) { 
                txtC = txt.Substring(0,ind);
            } else {
                txtC = txt;
            }
            return txtC;

        }



    }


    
}
