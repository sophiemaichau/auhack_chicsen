#include <Servo.h>

Servo leftServo;
Servo rightServo;

double timePerRound = 1050;
String incomingByte;
bool colonBool = false;

void setup() {
  leftServo.attach(13);  // attaches the servo on pin 9 to the servo object
  rightServo.attach(11);
  pinMode(7,INPUT_PULLUP);
}

void loop() {
  colonBool = false;
  while (Serial.available()) {
    byte imed = Serial.read();
    
       if(imed != '0'){
        
          if(imed == 'l'){
              rotateAmountLeft(0,imed);
              imed = '0';
              leftServo.write(90);
              break;
          }
          
           if(imed == 'r'){
              rotateAmountRight(0,imed);
              imed = '0';
              rightServo.write(90);
              break;
          }
          
        }
    }
}

void rotateAmountLeft(int way, double degree){
    if(way == 1){
        leftServo.write(180);
      }else{
        leftServo.write(0);
      }

    delay(calcDelay(degree));
    leftServo.write(90);
  } 

void rotateAmountRight(int way, double degree){
    if(way == 1){
        rightServo.write(180);
      }else{
        rightServo.write(0);
      }

    delay(calcDelay(degree));
    rightServo.write(90);
  } 

double calcDelay(double degree){
    Serial.println((degree/360)*timePerRound); 
    return ((degree/360)*timePerRound); 
  }

