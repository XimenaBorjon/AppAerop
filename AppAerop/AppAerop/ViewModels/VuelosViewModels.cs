using AppAerop.Models;
using AppAerop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppAerop.ViewModels
{
    public class VuelosViewModels : INotifyPropertyChanged
    {
        public ObservableCollection<Vuelo> ListVuelos { get; set; } = new ObservableCollection<Vuelo>();   
        
        VueloServices servic = new VueloServices();
        public ICommand GuardarCommand { get; set; }
        public ICommand EnviarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public Vuelo vuelos { get; set; } = new Vuelo();
        public string Error { get; set; } = "";
        public VuelosViewModels()
        {
            GuardarCommand = new Command(Guardar);
            EliminarCommand = new Command(Eliminar);
        }

        public async void Enviar()
        {
            try
            {
                //await servic.GetVuelo(vuelos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        async void Eliminar()
        {
            ListVuelos.Remove(vuelos);
            Actualizar(nameof(ListVuelos));
            await servic.Delete(vuelos);

        }



        readonly VueloServices servicevuelos = new VueloServices();
        async void Guardar()
        {
            Error = "";
            Actualizar(nameof(Error));

            if (string.IsNullOrEmpty(vuelos.ClaveVuelo))
            {
                Error = "Escriba la clave del vuelo";
                Actualizar(nameof(Error));
            }
            if (string.IsNullOrEmpty(vuelos.Destino))
            {
                Error = "Escriba el destino que dese agregar";
                Actualizar(nameof(Error));
            }
            if (string.IsNullOrEmpty(vuelos.Status))
            {
               Error ="Escriba el Status del vuelo.";
                Actualizar(nameof(Error));
            }
            if (Error == "")
            {
                ListVuelos.Add(vuelos);
                Actualizar(nameof(ListVuelos));
                await servic.Ordenar(vuelos);
            }

        }

        
        public void Actualizar(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}

