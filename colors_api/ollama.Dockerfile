FROM ollama/ollama:0.6.3

# Installation des dépendances nécessaires
RUN apt-get update && apt-get install -y curl

# Préparation du script de vérification et de préchargement du modèle
RUN echo '#!/bin/bash \n\
ollama serve & \n\
sleep 5 \n\
# Vérifier si le modèle est déjà présent \n\
if ! ollama list | grep -q "llama3.2:1b"; then \n\
  echo "Téléchargement du modèle llama3.2:1b..." \n\
  ollama pull llama3.2:1b \n\
else \n\
  echo "Le modèle llama3.2:1b est déjà présent, préchargement des tensors..." \n\
fi \n\
# Préchargement des tensors dans tous les cas \n\
curl -X POST http://localhost:11434/api/generate -d \'{"model": "llama3.2:1b", "prompt": "Hello", "stream": false}\' \n\
# Arrêt du serveur \n\
pkill ollama \n\
exit 0' > /prepare-model.sh && chmod +x /prepare-model.sh

RUN /prepare-model.sh

# Configuration du point d'entrée
ENTRYPOINT ["ollama", "serve"]
