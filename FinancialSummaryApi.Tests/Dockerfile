FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN

ENV DynamoDb_LocalMode='true'

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./FinancialSummaryApi.sln ./
COPY ./FinancialSummaryApi/FinancialSummaryApi.csproj ./FinancialSummaryApi/
COPY ./FinancialSummaryApi.Tests/FinancialSummaryApi.Tests.csproj ./FinancialSummaryApi.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./FinancialSummaryApi/FinancialSummaryApi.csproj
RUN dotnet restore ./FinancialSummaryApi.Tests/FinancialSummaryApi.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out FinancialSummaryApi.Tests/FinancialSummaryApi.Tests.csproj

CMD dotnet test
