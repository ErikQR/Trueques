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
using static System.Net.Mime.MediaTypeNames;

namespace Trueque {
    internal class Program {

        static void Main(string[] args) {

            LeerObjetos();
            LeerObjetosNoDisp();
            LeerHistorico();
            GuardarListaObjetos();
            GuardarListaObjetosNoDisp();
            GuardarHistorico();

            //GuardarListaObjetosNoDisp();

            string Menu = "Bienvenido al sistema de gestion de trueques \n" +
                        "1- Agregar Objetos \n" +
                        "2- Consultar Objetos \n" +
                        "3- Gestionar Trueque \n" +
                        "4- Salir";

            string opcion = "";
            while (opcion == "") {
                Console.WriteLine(Menu);
                opcion = Console.ReadLine();
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
                            break;
                        case "3":
                            GestionarTrueque();
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
                        losObjetos.Add(new Objeto(Int32.Parse(campos[0]), campos[1], campos[2], Int32.Parse(campos[3]), campos[4], campos[5], campos[6], DateTime.Parse(campos[7])));
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
                        objetosNoDisp.Add(new Objeto(Int32.Parse(campos[0]), campos[1], campos[2], Int32.Parse(campos[3]), campos[4], campos[5], campos[6], DateTime.Parse(campos[7])));
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
        public static void LeerHistorico() {
            string ruta = "Historico.txt";
            using (StreamReader sr = new StreamReader(ruta)) {
                try
                {
                    string linea;
                    linea = sr.ReadLine();
                    do
                    {
                        string campos = linea;
                        historialTrueques.Add(campos);
                        linea = sr.ReadLine();
                    } while (linea != null);
                }
                catch (Exception e){
                    Console.WriteLine("!!ERROR¡¡--> " + e.ToString());
                }
            }

        }
        private static void GuardarListaObjetos()
        {
            string ruta = "Objetos.txt";

            StreamWriter sw = new StreamWriter(ruta, false);

            try
            {
                //Guardar lista losObjetos
                foreach (Objeto obj in losObjetos) {
                    sw.WriteLine(obj.Id.ToString() + "|" + obj.Descripcion.ToString() + "|" + obj.NombrePropietario.ToString() + "|" + obj.Valor.ToString() + "|" + obj.Preferencia1.ToString() + "|" + obj.Preferencia2.ToString() + "|" + obj.Preferencia3.ToString() + "|" + obj.Fecha.ToString("d"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->" + ex.ToString());
            }
            finally
            {
                sw.Dispose();
                sw.Close();
            }
        }
        private static void GuardarListaObjetosNoDisp()
        {
            string ruta = "ObjetosNoDisponibles.txt";

            StreamWriter sw = new StreamWriter(ruta, false);

            try
            {
                //Guardar lista losObjetos
                foreach (Objeto obj in objetosNoDisp) {
                    sw.WriteLine(obj.Id.ToString() + "|" + obj.Descripcion.ToString() + "|" + obj.NombrePropietario.ToString() + "|" + obj.Valor.ToString() + "|" + obj.Preferencia1.ToString() + "|" + obj.Preferencia2.ToString() + "|" + obj.Preferencia3.ToString() + "|" + obj.Fecha.ToString("d"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->" + ex.ToString());
            }
            finally
            {
                sw.Dispose();
                sw.Close();
            }
        }
        private static void GuardarHistorico()
        {
            string ruta = "Historico.txt";
            StreamWriter sw = new StreamWriter(ruta, false);
            try
            {
                foreach (string objs in historialTrueques)
                {
                    sw.WriteLine(objs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ->" + e.ToString());
            }
            finally
            {
                sw.Dispose();
                sw.Close();
            }
        }
        public static void AgregarObjeto()
        {
            Console.Clear();
            Objeto obj = new Objeto();
            obj.Fecha = DateTime.Today;

            bool agregar = false;
            bool cancelar = false;
            string opc = "";
            while (!agregar && !cancelar) {
                string agregarobj = "Seleccione los campos del objeto a ingresar \n" +
                    "\n" +
                    "Fecha: " + obj.Fecha.ToString("d") + "\n" +
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
                                    && (obj.Preferencia3 != null && obj.Preferencia3 != "")) {
                                    string opcAgregar = "";
                                    while (opcAgregar == "")
                                    {
                                        Console.WriteLine("¿Desea agregar el objeto con los siguientes datos?: \n" +
                                            "\n" +
                                            "-ID: " + obj.Id + "\n" +
                                            "-Descripcion: " + obj.Descripcion + "\n" +
                                            "-Nombre Propietario: " + obj.NombrePropietario + "\n" +
                                            "-Valor aproximado: " + obj.Valor + "\n" +
                                            "-Preferencia 1: " + obj.Preferencia1 + "\n" +
                                            "-Preferencia 2: " + obj.Preferencia2 + "\n" +
                                            "-Preferencia 3: " + obj.Preferencia3 + "\n" +
                                            "-Fecha de ingreso: " + obj.Fecha.ToString("d") + "\n" +
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
                                                    string siNo2 = "";
                                                    while (siNo == "") {
                                                        Console.WriteLine(
                                                            "\n" +
                                                            "Desea buscar si existen objetos que coincidan con sus preferencias?" +
                                                            "\n" +
                                                            "1-Si \n" +
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
                                                                    MostrarObjeto(perfectMatch);
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    Console.WriteLine("Objetos que coinciden al 100% con su objeto y su segunda preferencia: \n");
                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                    MostrarObjeto(secondaryMatch);
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    Console.WriteLine("Objetos que coinciden al 100% con su objeto y su tercera preferencia: \n");
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    MostrarObjeto(thirdMatch);
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    Console.WriteLine("Todos los objetos que cumplen con sus preferencias: \n");
                                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                                    MostrarObjeto(listMatch);
                                                                    Console.ForegroundColor = ConsoleColor.White;
                                                                    break;
                                                                case "2":
                                                                    Console.Clear();
                                                                    break;

                                                            }
                                                        }
                                                    }
                                                    while (siNo2 == "") {
                                                        Console.WriteLine(
                                                            "\n" +
                                                            "Desea realizar un trueue de inmediato?" +
                                                            "\n" +
                                                            "1-Si \n" +
                                                            "2-No ");
                                                        siNo2 = Console.ReadLine();
                                                        Console.Clear();
                                                        if (siNo2=="1"|siNo2=="2") {
                                                            switch (siNo2) { 
                                                                case "1":
                                                                    GestionarTrueque();
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
                                    where prefUno.Descripcion == pref1 && (prefUno.Preferencia1 == desc || prefUno.Preferencia2 == desc || prefUno.Preferencia3 == desc)
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
                                    where prefDos.Descripcion == pref2 && (prefDos.Preferencia2 == desc || prefDos.Preferencia1 == desc || prefDos.Preferencia3 == desc)
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
                                    where prefTres.Descripcion == pref3 && (prefTres.Preferencia3 == desc || prefTres.Preferencia1 == desc || prefTres.Preferencia2 == desc)
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
                        "3 = Listar objetos disponibles por tiempo de antiguedad \n" +
                        "4 = Valorizar objetos \n" +
                        "5 = Salir \n";
            string opc = "";
            while (opc != "5") {
                Console.Clear();
                Console.WriteLine(MenuBuscar);
                opc = Console.ReadLine();
                if (opc == "1" | opc == "2" | opc == "3" | opc == "4"| opc=="5") {
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
                            if (lstObjEnc.Count != 0) {
                                MostrarObjeto(lstObjEnc);
                                Console.ReadKey();
                            } else {
                                MostrarMensajeError("No se encontraron objetos. Presione cualquier tecla para continuar.");
                            }
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
                            if (lstObjEnc.Count != 0) {
                                MostrarObjeto(lstObjEnc);
                                Console.ReadKey();
                            } else {
                                MostrarMensajeError("No se encontraron objetos. Presione cualquier tecla para continuar.");
                            }

                        }
                    }
                    if (opc == "3") {
                        string rango1 = "";
                        string rango2 = "";
                        string opcListar = "";
                        
                        while (opcListar != "4") {
                            string menuListar = "Ingrese el rango de fechas: \n" +
                                          "1 = Desde: " + rango1 + "\n" +
                                          "2 = Hasta: " + rango2 + "\n" +
                                          "3 = Aplicar filtro \n" +
                                          "4 = Salir";
                            Console.Clear();
                            Console.WriteLine(menuListar);
                            opcListar = Console.ReadLine();
                            if (opcListar == "1" | opcListar == "2" | opcListar == "3" | opcListar == "4") {
                                if (opcListar == "1") {
                                    rango1 = ObtenerFecha();
                                }
                                if (opcListar == "2") {
                                    rango2 = ObtenerFecha();
                                }
                                if (opcListar == "3") {
                                    if((rango1!="" & rango2 != "")&(DateTime.Parse(rango1)<DateTime.Parse(rango2))) {
                                        Console.Clear();
                                        Console.WriteLine("Filtros aplicados: \n" +
                                                          "Desde: " + rango1 + "\n" +
                                                          "Hasta: " + rango2);
                                        List<Objeto> objs = (from o in losObjetos
                                                             where o.Fecha >= DateTime.Parse(rango1) && o.Fecha <= DateTime.Parse(rango2)
                                                             orderby o.Fecha ascending
                                                             select o).ToList();
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine(("").PadLeft(108,'-'));
                                        Console.WriteLine(String.Format("|{0,6}|{1,15}|{2,15}|{3,8}|{4,15}|{5,15}|{6,15}|{7,10}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3","Fecha"));
                                        Console.WriteLine(("").PadLeft(108,'-'));
                                        foreach (Objeto obj in objs) {
                                            Console.WriteLine(String.Format("|{0,6}|{1,15}|{2,15}|{3,8}|{4,15}|{5,15}|{6,15}|{7,10}|", CortarTexto(obj.Id.ToString(), 6), CortarTexto(obj.Descripcion, 15), CortarTexto(obj.NombrePropietario, 15), CortarTexto(obj.Valor.ToString(), 8), CortarTexto(obj.Preferencia1, 15), CortarTexto(obj.Preferencia2, 15), CortarTexto(obj.Preferencia3, 15), CortarTexto(obj.Fecha.ToString(),10)));
                                        }
                                        Console.WriteLine(("").PadLeft(108,'-'));

                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.ReadKey();
                                    } else {
                                        MostrarMensajeError("Aplique un filtro válido");
                                    }
                                }
                            } else {
                                MostrarMensajeError("La opción indicada no existe, favor indicar una opción válida");
                            }
                        }
                    }

                    if (opc == "4") {
                        Console.Clear();
                        List<Objeto> objs = (from o in losObjetos
                                             orderby o.Id ascending
                                             select o).ToList();
                        int v = (from o in losObjetos 
                                 select o.Valor).Sum();
                        Console.WriteLine("Valorización: \n");
                        foreach(Objeto obj in objs) {
                            Console.WriteLine(obj.Descripcion.PadRight(25)+": $"+obj.Valor.ToString().PadLeft(8));
                        }
                            
                        Console.WriteLine(("").PadLeft(36,'-'));
                        Console.WriteLine(("Total").PadRight(25)+": $"+v.ToString().PadLeft(8));
                        Console.ReadKey();
                    }

                } else {
                    MostrarMensajeError("La opción indicada no existe, favor indicar una opción válida");
                }


            }
        }
        public static void MostrarMensajeError(string txt) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(txt);
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        public static void MostrarObjeto(Objeto obj) {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(obj.Id.ToString(), 6), CortarTexto(obj.Descripcion, 20), CortarTexto(obj.NombrePropietario, 15), CortarTexto(obj.Valor.ToString(), 8), CortarTexto(obj.Preferencia1, 20), CortarTexto(obj.Preferencia2, 20), CortarTexto(obj.Preferencia3, 20)));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.ReadKey();

        }

        public static void MostrarObjeto(List<Objeto> lstObj) {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            foreach (Objeto obj in lstObj) {
                Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(obj.Id.ToString(), 6), CortarTexto(obj.Descripcion, 20), CortarTexto(obj.NombrePropietario, 15), CortarTexto(obj.Valor.ToString(), 8), CortarTexto(obj.Preferencia1, 20), CortarTexto(obj.Preferencia2, 20), CortarTexto(obj.Preferencia3, 20)));
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

        }

        public static string CortarTexto(string txt, int ind) {
            string txtC = "";
            if (txt.Length > ind) {
                txtC = txt.Substring(0, ind);
            } else {
                txtC = txt;
            }
            return txtC;

        }
        public static void GestionarTrueque()
        {
            Console.Clear();
            Objeto objeto1 = new Objeto();
            Objeto objeto2 = new Objeto();
            bool permutar = false;
            bool cancelar = false;
            string opc = "";
            while (!permutar && !cancelar)
            {
                string menu = "Seleccione los objetos que desea buscar para realizar el trueque: \n" +
                        "\n" +
                        "-1 Primer objeto \n" +
                        "-2 Segundo objeto \n" +
                        "-3 Realizar trueque \n" +
                        "-4 Cancelar trueque ";
                Console.WriteLine(menu);
                opc = Console.ReadLine();
                Console.Clear();
                if (opc == "1" | opc == "2" | opc == "3" | opc == "4")
                {
                    switch (opc)
                    {
                        case "1":
                            bool num = false;
                            while (!num)
                            {
                                int id;
                                Console.WriteLine("Ingrese id del objeto que desea permutar");
                                string ids = Console.ReadLine();
                                num = int.TryParse(ids, out id);
                                if (num)
                                {
                                    Console.Clear();
                                    id = Int32.Parse(ids);
                                    Objeto ob = BuscarObj(id);
                                    if (ob.Id == id)
                                    {
                                        Console.WriteLine("Resultado de la busqueda: \n ");
                                        objeto1 = ob;
                                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                                        Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3"));
                                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                                        Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(objeto1.Id.ToString(), 6), CortarTexto(objeto1.Descripcion, 20), CortarTexto(objeto1.NombrePropietario, 15), CortarTexto(objeto1.Valor.ToString(), 8), CortarTexto(objeto1.Preferencia1, 20), CortarTexto(objeto1.Preferencia2, 20), CortarTexto(objeto1.Preferencia3, 20)));
                                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------- \n");
                                        Console.WriteLine("Presione cualquier tecla para continuar");
                                        Console.ReadKey();
                                        Console.Clear();
                                        num = true;
                                    }
                                    else
                                    {
                                        MostrarMensajeError("No existe el id indicado en los registros. Presione cualquier tecla para continuar.");
                                        num = true;
                                    }
                                }
                                else
                                {
                                    MostrarMensajeError("Ingrese solo numeros");
                                }
                            }
                            break;
                        case "2":
                            //TO-DO  : buscar 2do objeto y almacenarlo en objeto 2
                            bool num1 = false;
                            while (!num1)
                            {
                                int id;
                                Console.WriteLine("Ingrese id del objeto que desea permutar");
                                string ids = Console.ReadLine();
                                num1 = int.TryParse(ids, out id);
                                if (num1)
                                {
                                    Console.Clear();
                                    id = Int32.Parse(ids);
                                    Objeto ob = BuscarObj(id);
                                    if (ob.Id == id)
                                    {
                                        Console.WriteLine("Resultado de la busqueda: \n ");
                                        objeto2 = ob;
                                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                                        Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3"));
                                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
                                        Console.WriteLine(String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(objeto2.Id.ToString(), 6), CortarTexto(objeto2.Descripcion, 20), CortarTexto(objeto2.NombrePropietario, 15), CortarTexto(objeto2.Valor.ToString(), 8), CortarTexto(objeto2.Preferencia1, 20), CortarTexto(objeto2.Preferencia2, 20), CortarTexto(objeto2.Preferencia3, 20)));
                                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------- \n");
                                        Console.WriteLine("Presione cualquier tecla para continuar");
                                        Console.ReadKey();
                                        Console.Clear();
                                        num1 = true;

                                    }
                                    else
                                    {
                                        MostrarMensajeError("No existe el id indicado en los registros. Presione cualquier tecla para continuar.");
                                        num1 = true;
                                    }
                                }
                                else
                                {
                                    MostrarMensajeError("Ingrese solo numeros");
                                }
                            }
                            break;
                        case "3":
                            if (!permutar)
                            {

                                if (objeto1 == null) MostrarMensajeError("Debe seleccionar el primer objeto");
                                if (objeto2 == null) MostrarMensajeError("Debe seleccionar el segundo objeto");
                                if (objeto1 != null && objeto2 != null)
                                {
                                    string opcPermutar = "";
                                    while (opcPermutar == "")
                                    {
                                        Console.WriteLine("¿Desea permutar los siguientes objetos? \n" +
                                            "Objeto 1: \n" +
                                            "---------------------------------------------------------------------------------------------------------------------\n" +
                                            String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3") + "\n" +
                                            "---------------------------------------------------------------------------------------------------------------------\n" +
                                            String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(objeto1.Id.ToString(), 6), CortarTexto(objeto1.Descripcion, 20), CortarTexto(objeto1.NombrePropietario, 15), CortarTexto(objeto1.Valor.ToString(), 8), CortarTexto(objeto1.Preferencia1, 20), CortarTexto(objeto1.Preferencia2, 20), CortarTexto(objeto1.Preferencia3, 20)) + "\n" +
                                            "--------------------------------------------------------------------------------------------------------------------- \n" +
                                            "Objeto 2: \n" +
                                            "---------------------------------------------------------------------------------------------------------------------\n" +
                                            String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3") + "\n" +
                                            "---------------------------------------------------------------------------------------------------------------------\n" +
                                            String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(objeto2.Id.ToString(), 6), CortarTexto(objeto2.Descripcion, 20), CortarTexto(objeto2.NombrePropietario, 15), CortarTexto(objeto2.Valor.ToString(), 8), CortarTexto(objeto2.Preferencia1, 20), CortarTexto(objeto2.Preferencia2, 20), CortarTexto(objeto2.Preferencia3, 20)) + "\n" +
                                            "--------------------------------------------------------------------------------------------------------------------- \n" +
                                            "\n" +
                                            "1- Si \n" +
                                            "2- No ");
                                        opcPermutar = Console.ReadLine();
                                        Console.Clear();
                                        if (opcPermutar == "1" | opcPermutar == "2")
                                        {
                                            switch (opcPermutar)
                                            {
                                                case "1":
                                                    //opcion si, guarda los 2 objetos en una cadena de string dentro de la lista historica
                                                    historialTrueques.Add("Objeto 1:\n" +
                                                        "---------------------------------------------------------------------------------------------------------------------\n" +
                                                        String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3") + "\n" +
                                                        "---------------------------------------------------------------------------------------------------------------------\n" +
                                                        String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(objeto1.Id.ToString(), 6), CortarTexto(objeto1.Descripcion, 20), CortarTexto(objeto1.NombrePropietario, 15), CortarTexto(objeto1.Valor.ToString(), 8), CortarTexto(objeto1.Preferencia1, 20), CortarTexto(objeto1.Preferencia2, 20), CortarTexto(objeto1.Preferencia3, 20)) + "\n" +
                                                        "--------------------------------------------------------------------------------------------------------------------- \n" +
                                                        "Objeto 2\n" +
                                                        "---------------------------------------------------------------------------------------------------------------------\n" +
                                                        String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", "ID", "Descripción", "Propietario", "Valor", "Preferencia 1", "Preferencia 2", "Preferencia3") + "\n" +
                                                        "---------------------------------------------------------------------------------------------------------------------\n" +
                                                        String.Format("|{0,6}|{1,20}|{2,15}|{3,8}|{4,20}|{5,20}|{6,20}|", CortarTexto(objeto2.Id.ToString(), 6), CortarTexto(objeto2.Descripcion, 20), CortarTexto(objeto2.NombrePropietario, 15), CortarTexto(objeto2.Valor.ToString(), 8), CortarTexto(objeto2.Preferencia1, 20), CortarTexto(objeto2.Preferencia2, 20), CortarTexto(objeto2.Preferencia3, 20)) + "\n" +
                                                        "--------------------------------------------------------------------------------------------------------------------- ");
                                                    GuardarHistorico();
                                                    objetosNoDisp.Add(objeto1);
                                                    objetosNoDisp.Add(objeto2);
                                                    losObjetos.Remove(objeto1);
                                                    losObjetos.Remove(objeto2);
                                                    GuardarListaObjetosNoDisp();
                                                    GuardarListaObjetos();
                                                    


                                                    break;
                                                case "2":
                                                    //opcion no, se cancela la permutacion
                                                    Console.Clear();
                                                    break;

                                            }
                                        }
                                    }
                                }

                            }
                            //TO-DO : guardar los 2 objetos concatenados en un string, el cual debe ser guardado en la lista historica
                            // luego de guardar los objetos en la lista, eliminar ambos objetos en la lista de objetosdisponibles
                            break;
                        case "4":
                            //Cancelar la operacion
                            cancelar = true;
                            Console.WriteLine("Se cancela el trueque, presione cualquier tecla para salir");
                            break;
                    }


                }
                else
                {
                    opc = "";
                    Console.Clear();
                    Console.WriteLine("Opcion ingresada no valida, ingrese una opcion valida");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
        public static string ObtenerFecha() {
            string fechaObtenida = "";
            string dia = "";
            string mes = "";
            string anio = "";
            string opc = "";

            while (opc != "5") {
                string menuRango1 = "Ingrese rango de fecha: \n" +
                    "1 = Día: " + dia + "\n" +
                    "2 = Mes: " + mes + "\n" +
                    "3 = Año: " + anio + "\n" +
                    "4 = Ingresar fecha \n" +
                    "5 = Cancelar";
                Console.Clear();
                Console.WriteLine(menuRango1);
                opc = Console.ReadLine();
                if (opc == "1" | opc == "2" | opc == "3" | opc == "4" | opc == "5") {
                    if (opc == "1") {
                        Console.Clear();
                        Console.WriteLine("Ingrese día con formato DD: ");
                        dia = Console.ReadLine();
                        if (ValidarNumero(dia)) {
                            if((!(Int32.Parse(dia)>0&& Int32.Parse(dia) < 32))|dia.Length!=2) {
                                MostrarMensajeError("Ingrese un día válido");
                                dia = "";
                            }
                        } else {
                            MostrarMensajeError("Ingrese un día válido");
                            dia = "";
                        }
                    }
                    if (opc == "2") {
                        Console.Clear();
                        Console.WriteLine("Ingrese mes con formato MM: ");
                        mes = Console.ReadLine();
                        if (ValidarNumero(mes)) {
                            if ((!(Int32.Parse(mes) > 0 && Int32.Parse(mes) < 13))|mes.Length!=2) {
                                MostrarMensajeError("Ingrese un mes válido");
                                mes = "";
                            }
                        } else {
                            MostrarMensajeError("Ingrese un mes válido");
                            mes = "";
                        }
                    }
                    if (opc == "3") {
                        Console.Clear();
                        Console.WriteLine("Ingrese año con formato AAAA: ");
                        anio = Console.ReadLine();
                        if (ValidarNumero(anio)) {
                            if (anio.Length!=4) {
                                MostrarMensajeError("Ingrese un año válido");
                                anio = "";
                            }
                        } else {
                            MostrarMensajeError("Ingrese un año válido");
                            anio = "";
                        }
                    }
                    if (opc == "4") {
                        if(dia!="" & mes!="" & anio != "") {
                            fechaObtenida = dia + "-" + mes + "-" + anio;
                            opc = "5";
                        } else {
                            MostrarMensajeError("Fecha inválida, favor ingresar una fecha correcta");
                        }
                        
                    }

                } else {
                    MostrarMensajeError("La opción indicada no existe, favor indicar una opción válida");
                }

            }

            return fechaObtenida;

        }
        public static Boolean ValidarNumero(string num) {
            Boolean n = false;
            int opc = 0;
            n = int.TryParse(num, out opc);
            return n;
        }

    }

}
