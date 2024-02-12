// Pin du bouton ON/OFF
const int boutonPin = 6;
const int potentiometrePin = A1; // Broche analogique à laquelle le potentiomètre est connecté
// Variable pour stocker l'état du bouton
int etatBouton;
int dernierEtatBouton = HIGH;  // HIGH signifie relâché
long dernierDebounceTime = 0;
long debounceDelay = 50;

void setup() {
  // Initialisation du port série
  Serial.begin(9600);

  // Configuration du bouton en tant que broche d'entrée
  pinMode(boutonPin, INPUT_PULLUP);
}

void loop() {
  // Lecture de l'état du bouton
  int lectureBouton = digitalRead(boutonPin);

  // Lire la valeur du potentiomètre
  int valeurPotentiometre = analogRead(potentiometrePin);

  // Débouncing pour éviter les rebonds mécaniques
  if (lectureBouton != dernierEtatBouton) {
    dernierDebounceTime = millis();
  }

  if ((millis() - dernierDebounceTime) > debounceDelay) {
    // Si l'état du bouton a changé, mettre à jour l'état
    if (lectureBouton != etatBouton) {
      etatBouton = lectureBouton;

      // Si le bouton est enfoncé
      if (etatBouton == LOW) {
        Serial.println("on");
      } else {
        Serial.println("off");
      }
    }
  }
  Serial.print(",");
  Serial.println(valeurPotentiometre);

  delay(250); // Délai optionnel pour stabiliser la transmission
  

  // Mettre à jour le dernier état du bouton
  dernierEtatBouton = lectureBouton;
}


  

  
  

