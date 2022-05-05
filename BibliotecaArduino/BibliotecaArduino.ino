int pinoLed = 13; 

void setup() {
  // Iniciar o serial 
  Serial.begin(9600);

  //Iniciar pinos 
   pinMode(pinoLed, OUTPUT);
}

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
void lerRFID(char op){
 
  if(op == 'I'){
    digitalWrite (pinoLed, HIGH);
  }  
  else{
     digitalWrite (pinoLed, LOW);
   }
}
void loop() {
  String opcao = lerSerial();
  // Se a opcao não for vazia
  if(opcao!=""){
    switch(opcao[1]){
      case 'T': testar(opcao); break;
      case 'L': lerRFID(opcao[2]); break;
    }
  }
}
