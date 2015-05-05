FROM microsoft/aspnet:1.0.0-beta3

COPY . /app
WORKDIR /app
RUN apt-get update -qq \
    && apt-get install -qqy git nodejs-legacy
	&& git config --global url."http://".insteadOf git://
RUN apt-get install npm
RUN npm install bower grunt-cli
RUN ["kpm", "restore"]
EXPOSE 5060
ENTRYPOINT ["k", "kestrel"]