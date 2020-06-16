FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env

WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY Anjoz.Identity.sln ./
COPY ./src/**/*.csproj ./src/
COPY ./tests/**/*.csproj ./tests/
RUN for file in src/*.csproj; do filename=$(basename -s .csproj $file); filepath="src/$filename"; mkdir $filepath; mv $file $filepath; done
RUN for file in tests/*.csproj; do filename=$(basename -s .csproj $file); filepath="tests/$filename"; mkdir $filepath; mv $file $filepath; done
RUN dotnet restore ./Anjoz.Identity.sln

# Build da aplicação
COPY ./src ./src 
COPY ./tests ./tests
RUN dotnet publish Anjoz.Identity.sln -c Release -o out --no-restore

# Build da imagem
FROM  mcr.microsoft.com/dotnet/core/aspnet:2.1 as runtime
WORKDIR /app
COPY --from=build-env /app/src/*.WebApi/out .

# EXPOSE 80

# EXPOSE $PORTA

# OLD way: ENTRYPOINT [ "dotnet", "Anjoz.Identity.WebApi.dll" ]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Anjoz.Identity.WebApi.dll
