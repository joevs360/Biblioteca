using Biblioteca.Data;
using Biblioteca.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAO
{
    internal class RegistroDAO
    {
        
        FirebaseClient fc = FirebaseConfiguracao.fc;

        public async Task<List<Registro>> GetRegistro(DateTime data)
        {
            try
            {
                string dia = data.Date.ToString("yyyy-MM-dd");
                var GetItems = (await fc.Child("Registro").Child(dia).OnceAsync<Registro>()).ToList();
                List<Registro> registros = new List<Registro>();
                
                foreach (var item in GetItems)
                {
                    Registro registro = new Registro();
                    registro.Temperatura = item.Object.Temperatura;
                    registro.Umidade = item.Object.Umidade;
                    string hora = item.Key.Substring(0,2);
                    string minuto = item.Key.Substring(3, 2);
                    string segundo = item.Key.Substring(6, 2);
                    registro.DataHora = new DateTime(data.Year, data.Month, data.Day,Convert.ToInt16(hora) , Convert.ToInt16(minuto), Convert.ToInt16(segundo));
                    registro.Key = item.Key;
                    registros.Add(registro);
                    
                }
                return registros;
            }
            catch (Exception)
            {
                return new List<Registro>();
            }
          
        }
    }
}
