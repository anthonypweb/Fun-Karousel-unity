const int boutonPin = 11; // Broche numérique à laquelle le bouton est connecté
const int potentiometrePin = A1; // Broche analogique à laquelle le potentiomètre est connecté

void setup() {
  Serial.begin(9600); // Initialise la communication série à 9600 bauds
  pinMode(boutonPin, INPUT); // Configure la broche du bouton en entrée
}

void loop() {
  // Lire l'état du bouton
  int etatBouton = digitalRead(boutonPin);

  // Lire la valeur du potentiomètre
  int valeurPotentiometre = analogRead(potentiometrePin);

  // Envoyer l'état du bouton et la valeur du potentiomètre via le port série
  Serial.print(etatBouton);
  Serial.print(",");
  Serial.println(valeurPotentiometre);

  delay(250); // Délai optionnel pour stabiliser la transmission
}

