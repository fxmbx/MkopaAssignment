ImageName = smsservice
ContainerName = smscontainer

test:
	dotnet test 

server:
	cd SmsService \
	&& dotnet watch run 

build:
	dotnet build 

build-docker: 
	docker build -t $(ImageName):latest .

run-docker: 
	docker run -d --name $(smscontainer) -p 5059:5059 $(ImageName):latest
.
up:
	docker-compse up -d 

