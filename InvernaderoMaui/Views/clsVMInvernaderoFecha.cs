using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiMaui.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls;

namespace InvernaderoMaui.Views
{
    public class clsVMInvernaderoFecha
    {
        private DelegateCommand cambiarVistaCommand;

        public DelegateCommand CambiarVistaCommand
        {
            get { return cambiarVistaCommand; }
        }
        clsVMInvernaderoFecha()
        {
            cambiarVistaCommand = new DelegateCommand(cambiarVistaCommandExecuted, cambiarVistaCommandCanExecute);
        } 
        private async void cambiarVistaCommandExecuted()
        {
            await Navigation(new vistaDetalles());
        }
        private bool cambiarVistaCommandCanExecute()
        {
            return true;
        }
    }
}
