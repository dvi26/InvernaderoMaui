using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsTemperaturas
    {
        private DateTime fecha;
        private int idInvernadero;
        private double? temp1;
        private double? temp2;
        private double? temp3;
        private int? humedad1;
        private int? humedad2;
        private int? humedad3;
        
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public int IdInvernadero
        {
            get { return idInvernadero; }
        }
        public double? Temp1
        {
            get { return temp1; }
            set { temp1 = value; }
        }
        public double? Temp2
        {
            get { return temp2; }
            set { temp2 = value; }
        }
        public double? Temp3
        {
            get { return temp3; }
            set { temp3 = value; }
        }
        public int? Humedad1
        {
            get { return humedad1; }
            set { humedad1 = value; }
        }
        public int? Humedad2
        {
            get { return humedad2; }
            set { humedad2 = value; }
        }
        public int? Humedad3
        {
            get { return humedad3; }
            set { humedad3 = value; }
        }
        public clsTemperaturas(DateTime fecha, int idInvernadero, double ?temp1, double ?temp2, double ?temp3, int ?humedad1, int ?humedad2, int ?humedad3)
        {
            this.fecha = fecha;
            this.idInvernadero = idInvernadero;
            this.temp1 = temp1;
            this.temp2 = temp2;
            this.temp3 = temp3;
            this.humedad1 = humedad1;
            this.humedad2 = humedad2;
            this.humedad3 = humedad3;
        }
        public clsTemperaturas()
        {

        }
    }
}
