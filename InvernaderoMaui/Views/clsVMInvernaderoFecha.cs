using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ApiMaui.Resources;
using DAL;
using ENT;
using InvernaderoMaui.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;

namespace InvernaderoMaui.Views
{

    public class clsVMInvernaderoFecha : INotifyPropertyChanged
    {
        #region Atributos 
        private List<clsInvernadero> listadoInvernaderos;
        //private clsVMInvernaderoFecha invernaderoFecha;
        private clsInvernadero invernaderoSeleccionado;
        private DelegateCommand cambiarVistaCommand;
        private DateTime fechaSeleccionada;
        #endregion

        #region Propiedades
        public clsInvernadero InvernaderoSeleccionado
        {
            get { return invernaderoSeleccionado; }
            set
            {
                if (invernaderoSeleccionado != value)
                {
                    invernaderoSeleccionado = value;
                    //NotifyPropertyChanged("InvernaderoSeleccionado");
                    cambiarVistaCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime FechaSeleccionada
        {
            get { return fechaSeleccionada; }
            set
            {
                if (fechaSeleccionada != value)
                {
                    fechaSeleccionada = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<clsInvernadero> ListadoInvernaderos
        {
            get { return listadoInvernaderos; }
            private set { listadoInvernaderos = value; }
        }
        /*
        public clsVMInvernaderoFecha InvernaderoFecha
        {
            get { return invernaderoFecha; }
            set { invernaderoFecha = value; }
        }*/

        public DelegateCommand CambiarVistaCommand
        {
            get { return cambiarVistaCommand; }
        }
        #endregion

        #region Constructor

        public clsVMInvernaderoFecha()
        {
            try
            {
                ListadoInvernaderos = clsDalBDD.getInvernaderos();
            }
            catch (SqlException ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "Error al cargar los invernaderos", "OK");
            }
            
            FechaSeleccionada = DateTime.Now;
            cambiarVistaCommand = new DelegateCommand(cambiarVistaCommandExecuted, cambiarVistaCommandCanExecute);
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Ejecuta el comando para cambiar la vista, pasandole los parametros necesarios (Nombre, IdInvernadero y FechaSeleccionada). Si no existe un invernadero con esa fecha, muestra error.
        /// </summary>
        private async void cambiarVistaCommandExecuted()
        {
            /// Verifica si existe un invernadero con la fecha seleccionada, quizas podria haberlo hecho con la funcion getTemperaturas.
            bool existeFecha = false;
            try
            {
                existeFecha = clsDalBDD.existeFechaEnInvernadero(InvernaderoSeleccionado.IdInvernadero, FechaSeleccionada);
            }
            catch (SqlException ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error al verificar la fecha", "OK");
            }
                
            if (existeFecha)
            {
                
                /*
                var invernaderoConFecha = new clsInvernaderoConFecha
                {
                    Invernadero = InvernaderoSeleccionado,
                    FechaSeleccionada = FechaSeleccionada
                };*/

                var navigationParameter = new ShellNavigationQueryParameters
                {
                    { "Nombre", InvernaderoSeleccionado.Nombre },
                    { "IdInvernadero", InvernaderoSeleccionado.IdInvernadero.ToString() },
                    { "FechaSeleccionada", FechaSeleccionada.ToString("yyyy-MM-dd") }
                };

                await Shell.Current.GoToAsync("//vistaDetalles", navigationParameter);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No existe un invernadero con esa fecha", "OK");
            }
        }

        /// <summary>
        /// Verifica si el comando de cambiar vista puede ejecutarse. 
        /// El comando solo se puede ejecutar si hay un invernadero seleccionado.
        /// </summary>
        private bool cambiarVistaCommandCanExecute()
        {
            return InvernaderoSeleccionado != null;
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
