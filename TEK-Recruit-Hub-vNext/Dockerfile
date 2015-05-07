FROM microsoft/aspnet:1.0.0-beta4

COPY . /app
WORKDIR /app
RUN curl -sL https://deb.nodesource.com/setup | bash - \
    && apt-get install -qqy nodejs \
    && npm install -g grunt-cli
RUN ["dnu", "restore"]
EXPOSE 5080
ENTRYPOINT ["dnx", "./src/TEK.Recruit.WebApp", "kestrel"]