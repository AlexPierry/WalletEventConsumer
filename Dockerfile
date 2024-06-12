# Usar a imagem oficial do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante do código e compilar a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Usar a imagem oficial do .NET runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Definir a entrada do container
ENTRYPOINT ["dotnet", "WalletEventConsumer.dll"]