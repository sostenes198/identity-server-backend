dotnet ef --project ../Anjoz.Identity.Repository migrations add CreateDatabase

Comando para builder imagem:  docker build --no-cache -t anjoz_identity:1.0 .

Comando para subir container: docker run -d -p 8099:80 --name container_anjoz_identity anjoz_identity:1.0