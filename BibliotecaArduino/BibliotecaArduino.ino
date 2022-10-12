#include <MFRC522.h>
#include <SPI.h>
#include <SoftwareSerial.h> 

//Definindo pinos
const int pinoLed = 8; 

//Pinos RFID
const int SDA_PIN = 10;
const int RST_PIN = 9;
/*
 * MOSI: 11
 * MISO: 12
 * SCK: 13
 * SDA: 10
 * RST: 09
 */

 //Pino WIFI
 const int pinWifiRX = 3;
 const int pinWifiTX = 2;
 SoftwareSerial wifi(pinWifiRX, pinWifiTX); 
 
//Instanciando leitor
MFRC522 mfrc522(SDA_PIN, RST_PIN); 

//Variaveis globais
bool lerRFID = false;
bool lerWIFI = true;


String lerSerial(){
  String resp="";
  while(Serial.available()) {
    //Recebe o texto pelo serial
    resp= Serial.readString();
  }
  return resp;

}
String separarOpcao(String texto){
  String resp = "";
  //Passa pelo texto todo, iniciando no quarto caracter até o ultimo
  for(int i = 4; i < texto.length(); i++){
    //Atribui cada caracter a resposta
    resp += texto[i];
  }
  return resp;
}
void testar(String op){
  if(op[2] == 'I'){
    String teste = separarOpcao(op);
    Serial.println(teste+"-OK");
    digitalWrite (pinoLed, HIGH);
  }
}
void ligarRFID(char op){
 
  if(op == 'I'){
    digitalWrite (pinoLed, HIGH);
    lerRFID = true;
  }  
  else{
     digitalWrite (pinoLed, LOW);
     lerRFID = false;
   }
}

void setup() {
  // Iniciar o serial 
  Serial.begin(9600);
  wifi.begin(115200);
  SPI.begin();      // Inicia  SPI bus
  mfrc522.PCD_Init();   // Inicia MFRC522
  
  //Iniciar pinos 
   pinMode(pinoLed, OUTPUT);

   delay(100);
}

void loop() {
  if(Serial.available() > 0){
     String opcao = lerSerial();
      // Se a opcao não for vazia
      if(opcao!=""){
        switch(opcao[1]){
          case 'T': testar(opcao); break;
          case 'L': ligarRFID(opcao[2]); break;
          default:

            //Enviar do serial pro wifi
            if(opcao.indexOf("[WIFI]")!=-1){
              lerWIFI = true;
              opcao.replace("[WIFI]","");
              wifi.flush();
              wifi.println(opcao);
            }
          break;
        }
      }
  }
  
  if(wifi.available() && lerWIFI){
    Serial.flush();
    //Receber do wifi e enviar pro serial
    String result = wifi.readString();
    if(result.indexOf("Resposta: ")!=-1){
       result.replace("Resposta: ","");
       Serial.println("\n[WIFI] "+result);
     } 
     lerWIFI=false;
  }
 
  if(lerRFID){
     String conteudo= "";
     byte letra;
      if ( ! mfrc522.PICC_IsNewCardPresent()) 
      {
         return;
      }
      if ( ! mfrc522.PICC_ReadCardSerial()) 
      {
         return;
      }
      for (byte i = 0; i < mfrc522.uid.size; i++) 
      {
         conteudo.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
         conteudo.concat(String(mfrc522.uid.uidByte[i], HEX));
      }
      Serial.println(conteudo.substring(1));
   }
}
