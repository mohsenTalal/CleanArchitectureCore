#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EAI.Template/EAI.Template.API/EAI.Template.API.csproj", "EAI.Template/EAI.Template.API/"]
COPY ["EAI.Template/EAI.Template.Infrastructure/EAI.Template.Infrastructure.csproj", "EAI.Template/EAI.Template.Infrastructure/"]
COPY ["EAI.Template/EAI.Template.Application/EAI.Template.Application.csproj", "EAI.Template/EAI.Template.Application/"]
COPY ["EAI.Template/EAI.Template.Data/EAI.Template.Data.csproj", "EAI.Template2/EAI.Template.Data/"]
COPY ["EAI.Template/EAI.Template.Common/EAI.Template.Common.csproj", "EAI.Template2/EAI.Template.Common/"]
RUN dotnet restore "EAI.Template/EAI.Template.API/EAI.Template.API.csproj"
COPY . .
WORKDIR "/src/EAI.Template/EAI.Template.API"
RUN dotnet build "EAI.Template.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EAI.Template.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EAI.Template.API.dll"]
