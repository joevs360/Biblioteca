using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Arduino
    {
        //Atributos
        public SerialPort serialPorta { get; set; }
        public string RFID { get; set; }
      
        //Métodos
        public void enviarDados(string Dados)
        {
            if (serialPorta.IsOpen == true)
                serialPorta.Write(Dados);
        }
        


    }
}
