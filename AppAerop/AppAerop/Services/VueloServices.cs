using AppAerop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppAerop.Services
{
    public class VueloServices
    {
        HttpClient client;
        public VueloServices()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://skyisllandaerolinea.sistemas19.com/")
            };
        }

        public event Action<List<string>> Error;

        public async Task<bool> Ordenar(Vuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            var response = await client.PostAsync("api/Aerolinea", new StringContent(json, Encoding.UTF8, "application/json"));
            return true;
        }

        public async Task<List<Vuelo>> GetVuelo()
        {
            List<Vuelo> list = null;
            var response = await client.GetAsync("api/Aerolinea");
            if (response.IsSuccessStatusCode)
            {
                var Json = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Vuelo>>(Json);
            }
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Vuelo>();
            }
        }

        public async Task<bool> Insert(Vuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            var response = await client.PostAsync("api/Aerolinea", new StringContent(json, Encoding.UTF8, "aplication/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            return true;
           
        }

        public async Task<bool> Delete(Vuelo v)
        {
            var response = await client.DeleteAsync("api/Aerolinea/" + v.IdVuelo);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el Id del vuelo");
            }
            return true;
        }


        void LanzarError(string mensaje)
        {
            Error?.Invoke(new List<string> { mensaje });
        }
        void LanzarErrorJson(string json)
        {
            List<string> obj = JsonConvert.DeserializeObject<List<string>>(json);
            if (obj != null)
            {
                Error?.Invoke(obj);
            }
        }

    }
}
