# Imagen base de .NET para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Exponer el puerto 8080 para la API
EXPOSE 8080

# Imagen para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar archivos del proyecto y restaurar dependencias
COPY ["MercurialBackendDotnet.csproj", "./"]
RUN dotnet restore

# Copiar el resto del código fuente del proyecto
COPY . .

# Compilar en modo Release
RUN dotnet publish -c Release -o /app/publish

# Imagen final con la app lista para ejecutar
FROM base AS final
WORKDIR /app

# Copiar la publicación desde la etapa de build
COPY --from=build /app/publish .

# Definir la variable de entorno PORT antes de usarla
ENV PORT=8080

# Configurar ASP.NET Core para que escuche en el puerto 8080
ENV ASPNETCORE_URLS=http://+:${PORT}

ENV ASPNETCORE_ENVIRONMENT=Production

ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

# Copiar la carpeta templates manualmente
COPY --from=build /src/Templates /app/Templates

# Comando de inicio de la aplicación
ENTRYPOINT ["dotnet", "MercurialBackendDotnet.dll"]