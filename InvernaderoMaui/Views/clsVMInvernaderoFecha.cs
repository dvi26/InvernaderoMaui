using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ApiMaui.Resources;
using DAL;
using ENT;
using InvernaderoMaui.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;

namespace InvernaderoMaui.Views
{
    public class clsVMInvernaderoFecha : INotifyPropertyChanged
    {
        private List<clsInvernadero> listadoInvernaderos;
        private clsVMInvernaderoFecha invernaderoFecha;
        private clsInvernadero invernaderoSeleccionado;
        private DelegateCommand cambiarVistaCommand;
        private DateTime fechaSeleccionada;

        public clsInvernadero InvernaderoSeleccionado
        {
            get { return invernaderoSeleccionado; }
            set {
                if (invernaderoSeleccionado != value)
                {
                    invernaderoSeleccionado = value;
                    NotifyPropertyChanged("InvernaderoSeleccionado");
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
        public clsVMInvernaderoFecha InvernaderoFecha
        {
            get { return invernaderoFecha; }
            set { invernaderoFecha = value; }
        }
        public DelegateCommand CambiarVistaCommand
        {
            get { return cambiarVistaCommand; }
        }
        public clsVMInvernaderoFecha()
        {
            ListadoInvernaderos = clsDalBDD.getInvernaderos();
            FechaSeleccionada = DateTime.Now;
            cambiarVistaCommand = new DelegateCommand(cambiarVistaCommandExecuted, cambiarVistaCommandCanExecute);
        } 
        private async void cambiarVistaCommandExecuted()
        {
            //creo que no hace falta comprobar, ya que lo comprueba en el CanExecute
            if (InvernaderoSeleccionado != null)
            {
                var invernaderoConFecha = new clsInvernaderoConFecha{
                    Invernadero = InvernaderoSeleccionado,
                    FechaSeleccionada = FechaSeleccionada
                };

                var navigationParameter = new ShellNavigationQueryParameters{
            { "InvernaderoId", InvernaderoSeleccionado.IdInvernadero.ToString() },
            { "FechaSeleccionada", FechaSeleccionada.ToString("yyyy-MM-dd") }};

                await Shell.Current.GoToAsync($"///vistaDetalles", navigationParameter);
            }
        }
        private bool cambiarVistaCommandCanExecute()
        {
            bool res=false;
            if (InvernaderoSeleccionado != null)
            {
                return true;
            }
            return res;
        }
        #region notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
