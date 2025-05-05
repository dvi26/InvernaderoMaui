using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT;

namespace InvernaderoMaui.Models
{
    //Posible modelo/DTO, no me ha hecho falta
    public class clsInvernaderoConFecha
    {
        public clsInvernadero Invernadero { get; set; } 
        public DateTime FechaSeleccionada { get; set; } 

        public clsInvernaderoConFecha() { }

        public clsInvernaderoConFecha(clsInvernadero invernadero, DateTime fecha)
        {
            Invernadero = invernadero;
            FechaSeleccionada = fecha;
        }
    }
}
