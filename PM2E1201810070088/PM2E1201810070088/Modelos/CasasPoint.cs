using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E1201810070088.Modelos
{
    public class CasasPoint
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set; }

         public string descripcion { get; set; }

         public string direccion { get; set; }

        public string base64 { get; set; }
    }
}
