#if defined(ESP32)
#include <WiFi.h>
#elif defined(ESP8266)
#include <ESP8266WiFi.h>
#endif
#include <NTPClient.h> 
#include <WiFiUdp.h>

#include <Firebase_ESP_Client.h>
#include <addons/TokenHelper.h>
#include <addons/RTDBHelper.h>

#include "DHT.h"
 
#define DHTPIN 2
#define DHTTYPE DHT11 

DHT dht(DHTPIN, DHTTYPE);

//Wifi
#define WIFI_SSID "Joe"
#define WIFI_PASSWORD "01041998"

//Firebase
#define API_KEY  "AIzaSyA6OMVXRaDe8Jo7AD4xh5MQw5sHAdTCu8A"
#define DATABASE_URL "https://bibliotecaarduino-4d629-default-rtdb.firebaseio.com" 
#define USER_EMAIL "joe.victor14@gmail.com"
#define USER_PASSWORD "123456"

//NTP
WiFiUDP ntpUDP;
NTPClient ntp(ntpUDP);


FirebaseData fbdo;

FirebaseAuth auth;
FirebaseConfig config;


unsigned long sendDataPrevMillis = 0;

unsigned long count = 0;


//Variaveis global
void conectarFireBase(){
  config.api_key = API_KEY;
  auth.user.email = USER_EMAIL;
  auth.user.password = USER_PASSWORD;
  config.database_url = DATABASE_URL;
  config.token_status_callback = tokenStatusCallback;

  #if defined(ESP8266)
  fbdo.setBSSLBufferSize(2048, 2048 );
  #endif
  fbdo.setResponseSize(2048);

  Firebase.begin(&config, &auth);
  Firebase.reconnectWiFi(true);
  Firebase.setDoubleDigits(5);
  config.timeout.serverResponse = 10 * 1000;  
}

void conectarWifi(){
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);

  delay(300);
  Serial.print("Conectando");
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(300);
    Serial.print(".");
  }
  
  Serial.println();
  Serial.print("Conectado IP: ");
  Serial.println(WiFi.localIP());
  Serial.println();  
}

void setup() {
  delay(2000);
  dht.begin();
  Serial.begin(115200);
  conectarWifi();
  delay(500);
  //Configurando conexão firebase
  conectarFireBase();

  ntp.begin();
  ntp.setTimeOffset(-10800);
}

void enviar(String tipo,String nome,String caminho, String id,String valor){
    testarFirebase();
    if(tipo == ""){
          Serial.println("SEM TIPO!");
          return;
     }
     if(nome == ""){
      Serial.println("SEM NOME!");
      return;
     }
     if(id== ""){
      Serial.println("SEM ID!");
      return;
     }
     if(caminho == ""){
      Serial.println("SEM VAMINHO!");
      return;
     }
     if(valor == ""){
      Serial.println("SEM VALOR!");
      return;
     }

     //Pegar ID
     if(isDigit(id[0]) && id.toInt() <= 0){
      caminho +=gerarID(nome);
     }
     else{
       caminho += "/"+id+"/";
     }

     Serial.println(caminho);
     //Selecionar Tipo
      if(tipo == "bool"){; Serial.printf("Resposta: %s\n", Firebase.RTDB.setBool(&fbdo, caminho, (valor == "true")? true : false) ? "ok" : fbdo.errorReason().c_str());}
      else if(tipo == "int"){  Serial.printf("Resposta: %s\n", Firebase.RTDB.setInt(&fbdo, caminho, valor.toInt()) ? "ok" : fbdo.errorReason().c_str());}
      else if(tipo == "float"){Serial.printf("Resposta: %s\n", Firebase.RTDB.setFloat(&fbdo, caminho, valor.toFloat()) ? "ok" : fbdo.errorReason().c_str());}
      else if(tipo == "double"){  Serial.printf("Resposta: %s\n", Firebase.RTDB.setDouble(&fbdo, caminho, valor.toDouble()) ? "ok" : fbdo.errorReason().c_str());}
      else if(tipo == "string"){Serial.printf("Resposta: %s\n", Firebase.RTDB.setString(&fbdo, caminho, (valor)) ? "ok" : fbdo.errorReason().c_str());}
       else{ 
          Serial.print("TIPO [");
          Serial.print(tipo);
          Serial.println("] INVALIDO");
     }
}
void testarFirebase(){
  String result = ("Resposta: %s\n", Firebase.RTDB.getInt(&fbdo, "/TesteConexao/") ? String(fbdo.to<int>()).c_str() : fbdo.errorReason().c_str());
  if(result.indexOf("token is not ready")!=-1){
    Serial.println("Não foi possivel conectar com o firebase, reiniciando...");
    ESP.restart();
  }  
}


void ler(String tipo,String caminho){
  testarFirebase();
   //Selecionar Tipo
      if(tipo == "bool"){Serial.printf("Resposta: %s\n", Firebase.RTDB.getBool(&fbdo, caminho) ? fbdo.to<bool>() ? "true" : "false" : fbdo.errorReason().c_str());}
      else if(tipo == "int"){ Serial.printf("Resposta: %s\n", Firebase.RTDB.getInt(&fbdo, caminho) ? String(fbdo.to<int>()).c_str() : fbdo.errorReason().c_str());}
      else if(tipo == "float"){Serial.printf("Resposta: %s\n", Firebase.RTDB.getFloat(&fbdo, caminho) ? String(fbdo.to<float>()).c_str() : fbdo.errorReason().c_str());}
      else if(tipo == "double"){ Serial.printf("Resposta: %s\n", Firebase.RTDB.getDouble(&fbdo, caminho) ? String(fbdo.to<double>()).c_str() : fbdo.errorReason().c_str());}
      else if(tipo == "string"){ Serial.printf("Resposta: %s\n", Firebase.RTDB.getString(&fbdo, caminho) ? fbdo.to<const char *>() : fbdo.errorReason().c_str());}
      else{ 
          Serial.print("\nTIPO [");
          Serial.print(tipo);
          Serial.println("] INVALIDO");
      }
}
void remover(String caminho){
  testarFirebase();
  Serial.printf("Resposta: %s\n", Firebase.RTDB.deleteNode(&fbdo, caminho) ? "REMOVIDO"  : fbdo.errorReason().c_str() );
}

String gerarID(String nome){
      int id = 0;
      String caminho = "/IDS/" + nome;
      String getTemp = ("ULTIMO ID: %s\n", Firebase.RTDB.getInt(&fbdo, caminho, &id) ? String(id).c_str() : fbdo.errorReason().c_str() );
      Serial.println(getTemp);
      if(getTemp.equals("path not exist")){
        id = 0;
      }
              
      id++;
      Serial.printf("ATUALIZANDO ID: %s\n", Firebase.RTDB.setInt(&fbdo,  caminho, id) ? "ok" : fbdo.errorReason().c_str());
      String result = "/";
      result += id;
      result += "/";
      return result;
}
int tempMaxLeitura =0;
int tempMaxFirebase = 1800000;
void loop() {
       if(tempMaxLeitura >= 2000){
           // A leitura da temperatura e umidade pode levar 250ms!
           // O atraso do sensor pode chegar a 2 segundos.
            float h = dht.readHumidity();
            float t = dht.readTemperature();
            // testa se retorno é valido, caso contrário algo está errado.
            if (isnan(t) || isnan(h)) 
            {
              Serial.println("Failed to read from DHT");
            } 
            else
            {
              Serial.flush();
              Serial.println("Umidade: " + String(h)+ +" %t"+ "Temperatura: "+String(t)+ " *C");
              Serial.println("Firebase Ultima Atualizacao: "+String(tempMaxFirebase)+"ms" );
              if(tempMaxFirebase >= 1800000 ){
                     Serial.println("Transfirindo...");
                     String Dia, Hora;
                      if (ntp.update()) {
                        String result =   ntp.getFormattedDate();
                        int indexT = result.indexOf('T');
                        int indexZ = result.indexOf('Z');
                        Dia = result.substring(0,indexT);
                        Hora = result.substring(indexT+1,indexZ);
                        
                      } else {
                          Serial.println("!Erro ao atualizar NTP!\n");
                          return;
                      }
                     
                     String nome = "Temperatura";
                     String caminho = "/Registro/";
                     caminho += Dia;
                     caminho += "/";
                     caminho += Hora;
                     caminho += "/";
                     enviar("float", nome, caminho, nome, String(t));
                     nome = "Umidade";
                     enviar("float", nome, caminho, nome, String(h));
                     tempMaxFirebase = 0;
                }
            }
          tempMaxLeitura = 0;  
      }

      delay(100);
      tempMaxLeitura +=100;
      tempMaxFirebase +=100;
    
      
      if (Serial.available()){
            String dados = Serial.readString();
            Serial.println("Recebido: "+dados);
            
             String funcao,tipo, nome,caminho, valor,id;
        
            //Separar valores do serial
            String dadosTemp = dados;
            for(int i = 0; i<= 5; i++){
              int index = dadosTemp.indexOf(' ');
              String sub;

              if(i<5){
                 
                 
                 if(funcao == "enviar"){
                      if(index == -1){
                         sub = "";
                      }
                      else{
                         sub = dadosTemp.substring(0,index);
                      }
                  }
                  else if (funcao == "remover"){
                      if(index == -1 && i != 3){
                         sub = "";
                      }
                      else{
                         sub = dadosTemp.substring(0,index);
                      }
                  }
                  else if (funcao == "ler"){
                      if(index == -1 && (i != 1 && i != 3)){
                         sub = "";
                      }
                      else{
                         sub = dadosTemp.substring(0,index);
                      }
                  }
                  else if(i==0){sub = dadosTemp.substring(0,index);}
                
                 dadosTemp = dadosTemp.substring(index+1,dadosTemp.length());
              }
              else{
                 sub =dadosTemp;
               }

              sub.replace("\n",""); 
              sub.replace("\r","");

              
              switch(i){
                case 0: funcao = sub; break;
                case 1: tipo = sub; break;  
                case 2: nome = sub; break;  
                case 3: caminho = sub; break; 
                case 4: id = sub; break; 
                case 5: valor = sub; break;
              }
            }
              //Teste Corte
              Serial.println("Teste\nFuncao:["+funcao+"]\nTipo:["+tipo+"]\nNome:["+nome+"]\nCaminho["+caminho+"]\nValor:["+valor+"]");
              
              if(caminho[0] != '/'){
                  Serial.println("CAMINHO INVALIDO");
                  return;
                }
        
            if(funcao == ""){
              Serial.println("SEM FUNCAO");
              return;
             }
        
           //Selecionar Funcao
           if(funcao == "enviar"){
               caminho += nome;
               enviar(tipo, nome, caminho, id, valor);
           }
           else if(funcao == "remover"){
              remover(caminho);
           }
           else if(funcao == "ler"){
              ler(tipo, caminho);
           }
       }
}
