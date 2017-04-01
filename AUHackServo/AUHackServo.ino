#include <Servo.h>

Servo myservo;  // create servo object to control a servo

double timePerRound = 1050;
String incomingByte;
bool colonBool = false;

void setup() {
  myservo.attach(13);  // attaches the servo on pin 9 to the servo object
  pinMode(7,INPUT_PULLUP);
}

void loop() {
  colonBool = false;
  while (Serial.available()) {
    byte imed = Serial.read();
       if(imed == '1'){
          imed = '0';
          rotateAmount(0,360);
          myservo.write(90);
          delay(2000);
        }
        if(digitalRead(7) == 0){
            rotateAmount(1,360);
            delay(300);
          }
  }
}

void rotateAmount(int way, double degree){
    if(way == 1){
        myservo.write(180);
      }else{
        myservo.write(0);
      }

    delay(calcDelay(degree));
    myservo.write(90);
  } 

double calcDelay(double degree){
    Serial.println((degree/360)*timePerRound); 
    return ((degree/360)*timePerRound); 
  }

