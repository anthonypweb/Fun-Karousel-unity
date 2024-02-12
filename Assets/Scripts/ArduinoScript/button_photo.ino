

void setup() {

  //start serial connection

  Serial.begin(9600);

  //configure pin 2 as an input and enable the internal pull-up resistor

  pinMode(4, INPUT_PULLUP);

}

void loop() {

  //read the pushbutton value into a variable

  int sensorVal = digitalRead(4);

  //print out the value of the pushbutton


  // Keep in mind the pull-up means the pushbutton's logic is inverted. It goes

  // HIGH when it's open, and LOW when it's pressed. Turn on pin 13 when the

  // button's pressed, and off when it's not:

  if (sensorVal == LOW) {

    Serial.print('gvuftufucy');
  delay(2000);                // waits for a second
  } 
  
}