using System;
using System.Collections.Generic;
using System.Text;

namespace AppAerop.Models
{
    public class Vuelo
    {
        public int IdVuelo { get; set; }
        public DateTime Hora { get; set; }
        public string Destino { get; set; }
        public string Status { get; set; } 
        public string ClaveVuelo { get; set; } 
    }
}
