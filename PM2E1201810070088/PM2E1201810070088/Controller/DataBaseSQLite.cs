using PM2E1201810070088.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM2E1201810070088.Controller
{
   public class DataBaseSQLite
    {
        readonly SQLiteAsyncConnection db;

        //constructor de la clase DataBaseSQLite
        public DataBaseSQLite(string pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<CasasPoint>().Wait();
        }

        //Operaciones crud de sqlite
        //Read List way
        public Task<List<CasasPoint>> ObtenerListaCasasPoint()
        {
            return db.Table<CasasPoint>().ToListAsync();
        }

        //read one by one 
        public Task<CasasPoint> ObtenerCasasPoint(int pcodigo)
        {
            return db.Table<CasasPoint>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        //Create o update personas
        public Task<int> GrabarCasasPoint(CasasPoint casa)
        {
            if (casa.codigo != 0)
            {
                return db.UpdateAsync(casa);
            }
            else
            {
                return db.InsertAsync(casa);
            }

        }



        //delete
        public Task<int> EliminarCasasPoint(CasasPoint localizacion)
        {
            return db.DeleteAsync(localizacion);
        }


    }
}
