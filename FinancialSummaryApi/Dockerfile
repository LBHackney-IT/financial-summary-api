FROM amazonlinux:2

RUN yum install -y \
    rpmdevtools \
    wget \
    yum-utils

WORKDIR /tmp

# Download wkhtmltopdf and its dependencies. Then extract all rpm files.
ENV WKHTMLTOPDF_BIN="wkhtmltopdf.rpm"
RUN wget -O $WKHTMLTOPDF_BIN https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox-0.12.6-1.centos7.$(arch).rpm \
    && yum install --downloadonly --downloaddir=/tmp $WKHTMLTOPDF_BIN \
    && yumdownloader --archlist=$(arch) \
    bzip2-libs \
    expat \
    libuuid \
    && rpmdev-extract *rpm

WORKDIR /layer

# Copy wkhtmltopdf binary and dependency libraries for packaging
RUN mkdir -p {bin,lib} \
    && cp /tmp/wkhtml*/usr/local/bin/* bin \
    && cp /tmp/*/usr/lib64/* lib || :

# Zip files
ENV LAYER_ZIP="layer.zip"
RUN zip -r $LAYER_ZIP bin lib \
    && mv $LAYER_ZIP /

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
