using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ApiMaui.Resources;
using DAL;
using ENT;
using Microsoft.Data.SqlClient;

namespace InvernaderoMaui.Views
{
    public class clsViewModelVistaDetalles : IQueryAttributable, INotifyPropertyChanged
    {
        #region Atributos 
        private int invernaderoId;
        private DateTime fechaSeleccionada;
        private clsTemperaturas datosTemperatura { get; set; }
        private string nombreInvernadero { get; set; }

        private double progressTemp1;
        private double progressTemp2;
        private double progressTemp3;
        private double progressHumedad1;
        private double progressHumedad2;
        private double progressHumedad3;

        private const double MaxTemp = 250.0;
        private const int MaxHumedad = 100;

        private string temp1Label;
        private string temp2Label;
        private string temp3Label;
        private string humedad1Label;
        private string humedad2Label;
        private string humedad3Label;

        private DelegateCommand volverCommand;

        #endregion

        #region Propiedades Displays
        public string Temp1Label
        {
            get { return temp1Label; }
            private set
            {
                temp1Label = value;
                NotifyPropertyChanged("Temp1Label");
            }
        }

        public string Temp2Label
        {
            get { return temp2Label; }
            private set
            {
                temp2Label = value;
                NotifyPropertyChanged("Temp2Label");
            }
        }

        public string Temp3Label
        {
            get { return temp3Label; }
            private set
            {
                temp3Label = value;
                NotifyPropertyChanged("Temp3Label");
            }
        }

        public string Humedad1Label
        {
            get { return humedad1Label; }
            private set
            {
                humedad1Label = value;
                NotifyPropertyChanged("Humedad1Label");
            }
        }

        public string Humedad2Label
        {
            get { return humedad2Label; }
            private set
            {
                humedad2Label = value;
                NotifyPropertyChanged("Humedad2Label");
            }
        }

        public string Humedad3Label
        {
            get { return humedad3Label; }
            private set
            {
                humedad3Label = value;
                NotifyPropertyChanged("Humedad3Label");
            }
        }

        #endregion

        #region Propiedades 

        public DelegateCommand VolverCommand
        {
            get { return volverCommand; }
            private set
            {
                volverCommand = value;
            }
        }

        public double ProgressTemp1
        {
            get { return progressTemp1; }
            private set
            {
                progressTemp1 = value;
                NotifyPropertyChanged("ProgressTemp1");
            }
        }

        public double ProgressTemp2
        {
            get { return progressTemp2; }
            private set
            {
                progressTemp2 = value;
                NotifyPropertyChanged("ProgressTemp2");
            }
        }

        public double ProgressTemp3
        {
            get { return progressTemp3; }
            private set
            {
                progressTemp3 = value;
                NotifyPropertyChanged("ProgressTemp3");
            }
        }

        public double ProgressHumedad1
        {
            get { return progressHumedad1; }
            private set
            {
                progressHumedad1 = value;
                NotifyPropertyChanged("ProgressHumedad1");
            }
        }

        public double ProgressHumedad2
        {
            get { return progressHumedad2; }
            private set
            {
                progressHumedad2 = value;
                NotifyPropertyChanged("ProgressHumedad2");
            }
        }

        public double ProgressHumedad3
        {
            get { return progressHumedad3; }
            private set
            {
                progressHumedad3 = value;
                NotifyPropertyChanged("ProgressHumedad3");
            }
        }

        public int InvernaderoId
        {
            get { return invernaderoId; }
            private set
            {
                invernaderoId = value;
                NotifyPropertyChanged("InvernaderoId");
            }
        }

        public DateTime FechaSeleccionada
        {
            get { return fechaSeleccionada; }
            private set
            {
                fechaSeleccionada = value;
                NotifyPropertyChanged("FechaSeleccionada");
            }
        }

        public string NombreInvernadero
        {
            get { return nombreInvernadero; }
            private set
            {
                nombreInvernadero = value;
                NotifyPropertyChanged("NombreInvernadero");
            }
        }

        public clsTemperaturas DatosTemperatura
        {
            get { return datosTemperatura; }
            private set
            {
                datosTemperatura = value;
                NotifyPropertyChanged("DatosTemperatura");
            }
        }
        #endregion

        #region Constructor
        public clsViewModelVistaDetalles()
        {
            volverCommand = new DelegateCommand(volverCommandExecuted);

            ProgressTemp1 = 0;
            ProgressTemp2 = 0;
            ProgressTemp3 = 0;
            ProgressHumedad1 = 0;
            ProgressHumedad2 = 0;
            ProgressHumedad3 = 0;

            temp1Label = "?";
            temp2Label = "?";
            temp3Label = "?";
            humedad1Label = "?";
            humedad2Label = "?";
            humedad3Label = "?";
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Trae los parametros necesarios para la vista de la anterior, ademas actua como un "constructor" llamando a la carga de temperaturas
        /// </summary>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("IdInvernadero"))
            {
                if (int.TryParse(query["IdInvernadero"]?.ToString(), out int id))
                {
                    InvernaderoId = id;
                }
            }
            if (query.ContainsKey("Nombre"))
            {
                NombreInvernadero = query["Nombre"]?.ToString();
            }

            if (query.ContainsKey("FechaSeleccionada"))
            {
                if (DateTime.TryParse(query["FechaSeleccionada"]?.ToString(), out DateTime fecha))
                {
                    FechaSeleccionada = fecha;
                }
            }
            cargarTemperaturas();
        }

        /// <summary>
        /// Carga las temperaturas de la base de datos. Tambien cargo los progresos de las barras de temperatura y humedad, y les doy sus valores a los labels.
        /// </summary>
        private async void cargarTemperaturas()
        {
            //Comprobaccion innecesaria?
            if (InvernaderoId != 0 && FechaSeleccionada != DateTime.Now)
            {
                try
                {
                    DatosTemperatura = clsDalBDD.GetTemperaturasPorFecha(InvernaderoId, FechaSeleccionada);
                    ProgressTemp1 = DatosTemperatura.Temp1.HasValue ? Math.Min(DatosTemperatura.Temp1.Value / MaxTemp, 1.0) : 0;
                    ProgressTemp2 = DatosTemperatura.Temp2.HasValue ? Math.Min(DatosTemperatura.Temp2.Value / MaxTemp, 1.0) : 0;
                    ProgressTemp3 = DatosTemperatura.Temp3.HasValue ? Math.Min(DatosTemperatura.Temp3.Value / MaxTemp, 1.0) : 0;

                    ProgressHumedad1 = DatosTemperatura.Humedad1.HasValue ? Math.Min(DatosTemperatura.Humedad1.Value / (double)MaxHumedad, 1.0) : 0;
                    ProgressHumedad2 = DatosTemperatura.Humedad2.HasValue ? Math.Min(DatosTemperatura.Humedad2.Value / (double)MaxHumedad, 1.0) : 0;
                    ProgressHumedad3 = DatosTemperatura.Humedad3.HasValue ? Math.Min(DatosTemperatura.Humedad3.Value / (double)MaxHumedad, 1.0) : 0;

                    Temp1Label = DatosTemperatura.Temp1.HasValue ? DatosTemperatura.Temp1.Value.ToString("F1") : "?";
                    Temp2Label = DatosTemperatura.Temp2.HasValue ? DatosTemperatura.Temp2.Value.ToString("F1") : "?";
                    Temp3Label = DatosTemperatura.Temp3.HasValue ? DatosTemperatura.Temp3.Value.ToString("F1") : "?";

                    Humedad1Label = DatosTemperatura.Humedad1.HasValue ? DatosTemperatura.Humedad1.Value.ToString() : "?";
                    Humedad2Label = DatosTemperatura.Humedad2.HasValue ? DatosTemperatura.Humedad2.Value.ToString() : "?";
                    Humedad3Label = DatosTemperatura.Humedad3.HasValue ? DatosTemperatura.Humedad3.Value.ToString() : "?";
                }
                catch (SqlException ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Error en la base de datos al traer las temperaturas", "OK");
                }
            }
        }
        private async void volverCommandExecuted()
        {
            await Shell.Current.GoToAsync("//vistaPicker");
        }
        #endregion
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
