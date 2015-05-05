FROM microsoft/aspnet:1.0.0-beta3

COPY . /app
WORKDIR /app
RUN apt-get update -qq \
    && apt-get install -qqy git
	&& git config --global url."http://".insteadOf git://
	&& apt-get install nodejs-legacy
	&& apt-get install npm
	&& npm install bower grunt-cli
RUN ["kpm", "restore"]
EXPOSE 5060
ENTRYPOINT ["k", "kestrel"]