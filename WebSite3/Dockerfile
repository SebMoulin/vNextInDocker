FROM microsoft/aspnet:1.0.0-beta3

COPY . /app
WORKDIR /app
RUN apt-get update -qq \
    && apt-get install -qqy git
	&& git config --global url."http://".insteadOf git://
RUN ["kpm", "restore", "-s", "https://www.myget.org/F/aspnetvnext/api/v2", "-f", "https://nuget.org/api/v2"]

EXPOSE 5080
ENTRYPOINT ["k", "kestrel"]