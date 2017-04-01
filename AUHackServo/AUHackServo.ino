/*
 Controlling a servo position using a potentiometer (variable resistor)
 by Michal Rinott <http://people.interaction-ivrea.it/m.rinott>

 modified on 8 Nov 2013
 by Scott Fitzgerald
 http://www.arduino.cc/en/Tutorial/Knob
*/

#include <Servo.h>

Servo myservo;  // create servo object to control a servo

double timePerRound = 1050;

void setup() {
  myservo.attach(13);  // attaches the servo on pin 9 to the servo object
  pinMode(7,INPUT_PULLUP);
}

void loop() {
   // scale it to use it with the servo (value between 0 and 180)
  
  if (Serial.available() > 0) 
    {
          byte incomingByte = Serial.read();

          Serial.print("I received: ");
          Serial.println(incomingByte, DEC);
          
          rotateAmount(1,360);
    }
  
  if(digitalRead(7) == 0){
      rotateAmount(1,360);
      delay(300);
    }
  myservo.write(90);

  // waits for the servo to get there
}

void rotateAmount(int way, double degree){
    if(way == 1){
        myservo.write(180);
      }else{
        myservo.write(0);
      }

    delay(calcDelay(degree));
  } 

double calcDelay(double degree){
    Serial.println((degree/360)*timePerRound); 
    return ((degree/360)*timePerRound); 
  }

