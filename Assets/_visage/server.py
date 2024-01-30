from flask import Flask, render_template, request, redirect, url_for, send_from_directory, jsonify
import cv2
import os
from datetime import datetime

app = Flask(__name__)

# Chemin du dossier où les photos seront enregistrées
UPLOAD_FOLDER = '../project_unity/Fun-Karousel/Assets/photos'
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER

# Définir la taille maximale du fichier (10 Mo)
app.config['MAX_CONTENT_LENGTH'] = 10 * 1024 * 1024

# Définir le chemin du modèle de détection de visage
face_cascade_path = "haarcascade_frontalface_default.xml"
face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + face_cascade_path)

# Définir le chemin du modèle de détection de fond

# Fonction pour détourer le visage dans l'image
def crop_face(image):
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    faces = face_cascade.detectMultiScale(gray, scaleFactor=1.1, minNeighbors=5)
    if len(faces) > 0:
        (x, y, w, h) = faces[0]
        # Agrandir le rectangle de détection pour capturer une zone plus grande autour du visage
        margin = 70  # Vous pouvez ajuster cette valeur selon vos besoins
        x = max(0, x - margin)
        y = max(0, y - margin)
        w = min(image.shape[1], w + 2 * margin)
        h = min(image.shape[0], h + 2 * margin)
        cropped_face = image[y:y+h, x:x+w]
        return cropped_face
    else:
        return None
# Fonction pour retirer le fond de l'image
    
bg_subtractor = cv2.createBackgroundSubtractorMOG2()
def remove_background(image):
    fg_mask = bg_subtractor.apply(image)
    fg = cv2.bitwise_and(image, image, mask=fg_mask)
    return fg


# Page d'accueil
@app.route('/')
def index():
    return render_template('index.html')

# Route pour capturer une photo
@app.route('/process_image', methods=['POST'])
def capture():
    # Accéder à la webcam
    cap = cv2.VideoCapture(0)

    # Lire une image depuis la webcam
    ret, frame = cap.read()

    # Retirer le fond de l'image

    # Détourer le visage dans l'image
    cropped_face = crop_face(frame)
    fg_image = remove_background(cropped_face)

    # Générer un nom de fichier unique basé sur la date et l'heure actuelles
    now = datetime.now()
    timestamp = now.strftime("%Y%m%d%H%M%S")
    filename = f"photo_{timestamp}.png"  # Enregistrer au format PNG pour la transparence

    # Enregistrer l'image dans le dossier de téléchargement
    filepath = os.path.join(app.config['UPLOAD_FOLDER'], filename)
    cv2.imwrite(filepath, fg_image)

    # Libérer la webcam
    cap.release()

    return jsonify({'success': True})

# Route pour accéder aux photos téléchargées
@app.route('/photos/<filename>')
def uploaded_file(filename):
    return send_from_directory(app.config['UPLOAD_FOLDER'], filename)

if __name__ == '__main__':
    # Créer le dossier de téléchargement s'il n'existe pas
    if not os.path.exists(UPLOAD_FOLDER):
        os.makedirs(UPLOAD_FOLDER)

    # Lancer le serveur Flask
    app.run(debug=True)
