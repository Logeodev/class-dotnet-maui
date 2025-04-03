FROM ollama/ollama:0.6.3

# Installation des d�pendances n�cessaires
RUN apt-get update && apt-get install -y curl

# Pr�paration du script de v�rification et de pr�chargement du mod�le
RUN echo '#!/bin/bash \n\
ollama serve & \n\
sleep 5 \n\
# V�rifier si le mod�le est d�j� pr�sent \n\
if ! ollama list | grep -q "llama3.2:1b"; then \n\
  echo "T�l�chargement du mod�le llama3.2:1b..." \n\
  ollama pull llama3.2:1b \n\
else \n\
  echo "Le mod�le llama3.2:1b est d�j� pr�sent, pr�chargement des tensors..." \n\
fi \n\
# Pr�chargement des tensors dans tous les cas \n\
curl -X POST http://localhost:11434/api/generate -d \'{"model": "llama3.2:1b", "prompt": "Hello", "stream": false}\' \n\
# Arr�t du serveur \n\
pkill ollama \n\
exit 0' > /prepare-model.sh && chmod +x /prepare-model.sh

RUN /prepare-model.sh

# Configuration du point d'entr�e
ENTRYPOINT ["ollama", "serve"]
