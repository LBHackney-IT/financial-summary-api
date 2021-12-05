FROM mcr.microsoft.com/dotnet/core/sdk:3.1
RUN apt-get update && apt-get -y install xvfb && apt-get -y install fontconfig && apt-get -y install libssl1.0-dev && apt-get -y install libx11-dev libx11-xcb-dev libxcb-icccm4-dev libxcb-image0-dev libxcb-keysyms1-dev libxcb-randr0-dev libxcb-render-util0-dev libxcb-render0-dev libxcb-shm0-dev libxcb-util0-dev libxcb-xfixes0-dev libxcb-xkb-dev libxcb1-dev libxfixes-dev libxrandr-dev libxrender-dev

WORKDIR /var/task
RUN chmod 777 /QtBinariesLinux
COPY /QtBinariesLinux
WORKDIR /app
# For Rotativa SDK

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet build -c Release -o out
RUN dotnet publish -c Release -o out
EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
