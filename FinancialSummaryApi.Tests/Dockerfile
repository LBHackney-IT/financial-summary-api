FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN

ARG SONAR_TOKEN
ENV SONAR_TOKEN=$SONAR_TOKEN

ENV DynamoDb_LocalMode='true'

WORKDIR /app

# Enable SonarCloud
RUN apt-get update && apt-get install -y openjdk-11-jdk
RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet sonarscanner begin /k:"LBHackney-IT_financial-summary-api" /o:"lbhackney-it" /d:sonar.host.url=https://sonarcloud.io /d:sonar.login="${SONAR_TOKEN}"

# Copy csproj and restore as distinct layers
COPY ./FinancialSummaryApi.sln ./
COPY ./FinancialSummaryApi/FinancialSummaryApi.csproj ./FinancialSummaryApi/
COPY ./FinancialSummaryApi.Tests/FinancialSummaryApi.Tests.csproj ./FinancialSummaryApi.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config
RUN dotnet restore ./FinancialSummaryApi/FinancialSummaryApi.csproj
RUN dotnet restore ./FinancialSummaryApi.Tests/FinancialSummaryApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out FinancialSummaryApi/FinancialSummaryApi.csproj
RUN dotnet build -c debug -o out FinancialSummaryApi.Tests/FinancialSummaryApi.Tests.csproj

CMD dotnet test

RUN dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"