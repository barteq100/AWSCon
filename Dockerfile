FROM public.ecr.aws/lambda/dotnet:7 AS base

FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /src
COPY . .
RUN dotnet restore "src/SharedLib/SharedLib.csproj"
RUN dotnet restore "src/AWSAnno/AWSAnno.csproj"

WORKDIR "/src"
COPY . .
RUN dotnet build "src/AWSAnno/AWSAnno.csproj" --configuration Release --output /app/build

FROM build AS publish
RUN dotnet publish "src/AWSAnno/AWSAnno.csproj" \
            --configuration Release \ 
            --runtime linux-x64 \
            --self-contained false \ 
            --output /app/publish \
            -p:PublishReadyToRun=true  

FROM base AS final
WORKDIR /var/task
COPY --from=publish /app/publish .
