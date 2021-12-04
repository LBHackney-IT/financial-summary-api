FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app
# For Rotativa SDK
RUN yum update -y
RUN  yum install -y yum-utils rpmdevtools
RUN yum install libxrender 

RUN chmod 755 /app/wkhtmltopdf/wkhtmltopdf
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet build -c Release -o out
RUN dotnet publish -c Release -o out
EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
