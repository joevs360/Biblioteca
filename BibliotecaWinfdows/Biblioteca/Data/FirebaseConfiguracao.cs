using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data
{
    public static class FirebaseConfiguracao
    {
        public static string FirebaseClient = "https://bibliotecaarduino-4d629-default-rtdb.firebaseio.com/";
        public static string FrebaseSecret = "tobIcwMNwjCqDmya6lZgOWPOaYvguE6WsgmVMiIZ";

        public static FirebaseClient fc = new FirebaseClient(FirebaseClient,new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FrebaseSecret) });

    }
}
