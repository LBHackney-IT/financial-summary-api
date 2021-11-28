FROM public.ecr.aws/lambda/dotnet:3.1

WORKDIR /var/task

RUN yum update -y
RUN yum install -y pango.x86_64 
RUN yum install -y libXcomposite.x86_64 
RUN yum install -y libXcursor.x86_64
RUN yum install -y libXdamage.x86_64
RUN yum install -y libXext.x86_64 
RUN yum install -y libXi.x86_64 
RUN yum install -y libXtst.x86_64 
RUN yum install -y cups-libs.x86_64 
RUN yum install -y libXScrnSaver.x86_64 
RUN yum install -y libXrandr.x86_64
RUN yum install -y GConf2.x86_64 
RUN yum install -y alsa-lib.x86_64
RUN yum install -y atk.x86_64 
RUN yum install -y gtk3.x86_64 
RUN yum install -y ipa-gothic-fonts 
RUN yum install -y xorg-x11-fonts-100dpi
RUN yum install -y xorg-x11-fonts-75dpi
RUN yum install -y xorg-x11-utils
RUN yum install -y xorg-x11-fonts-cyrillic
RUN yum install -y xorg-x11-fonts-Type1 
RUN yum install -y xorg-x11-fonts-misc
RUN yum install -y glibc-devel.x86_64
RUN yum install -y at-spi2-atk.x86_64
RUN yum install -y mesa-libgbm.x86_64

COPY "bin/Release/lambda-publish"  .

FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /app
# Install dependencies
#RUN apt-get update -qq && apt-get -y install libgdiplus
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet build -c Release -o out
RUN dotnet publish -c Release -o out

EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/FinancialSummaryApi.dll
