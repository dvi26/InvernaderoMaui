using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsInvernadero
    {
        private int idInvernadero;
        private string nombre;

        public int IdInvernadero
        {
            get { return idInvernadero; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public clsInvernadero(int idInvernadero, string nombre)
        {
            this.idInvernadero = idInvernadero;
            this.nombre = nombre;
        }

        public clsInvernadero()
        {

        }
    }
}
