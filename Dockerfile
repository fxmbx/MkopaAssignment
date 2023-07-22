FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5059

ENV ASPNETCORE_URLS=http://+:5059


RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["SmsService/SmsService.csproj", "SmsService/"]
COPY ["SmsService.Test/SmsService.Test.csproj", "SmsService.Test/"]
COPY ["MessageQueueLibrary/MessageQueueLibrary.csproj", "MessageQueueLibrary/"]

RUN dotnet restore "SmsService/SmsService.csproj"

COPY . .
WORKDIR "/src/SmsService"

RUN dotnet build "./SmsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./SmsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmsService.dll"]


