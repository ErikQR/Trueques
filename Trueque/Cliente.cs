using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trueque {
    internal class Cliente {
        //Propiedades Cliente
        //-Nombre
        //-Numero telefono
        //-Objetos (lista)
        public string Nombre { get; set; }
        public int NumTelefono { get; set; }
        public List<Objeto> objetos;
        //Constructor
        public Cliente(string nombre, int numtelefono) { 
            Nombre = nombre;
            NumTelefono = numtelefono;
            objetos = new List<Objeto>();
        }
    }
}
