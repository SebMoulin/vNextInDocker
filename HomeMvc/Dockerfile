FROM microsoft/aspnet:1.0.0-beta3

COPY . /app
WORKDIR /app
RUN apt-get update -qq \
    && apt-get install -qqy git
	&& git config --global url."http://".insteadOf git://
RUN ["kpm", "restore"]

EXPOSE 5050
ENTRYPOINT ["k", "kestrel"]