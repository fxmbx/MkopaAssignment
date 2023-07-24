# M-Kopa Backend Engineering API

### Note: API has no concrete implementation

### Api Documentation

The api documentation can be on swagger

```
{{Host}}/swagger/index.html

```

Based on [this repo](https://github.com/fxmbx/MkopaAssignment).

## Usage

### 1. Build projects

```bash
dotnet build
```

### 2. Run test

```bash
dotnet test
```

### 3. Run Server

navigate into the SmsService directory then run :

```bash
dotnet watch run
```

# OR using make command

from the base directory /MkopaAssignment

### 1. Build project

```bash
make build
```

To run test

```bash
make test
```

to run server

```bash
make server
```

### 2. Prepare docker

To build a dev container run:

```bash
make build-docker
```

To run the docker image

```bash
make run-docker
```

To run docker-compose

```bash
make up
```

### Asumptions

- The message broker being used is unknown but it is either RabbitMq or Kafka.
- The validation of recipients number would be handled in the concret implementation of the send sms by a call to a private function

### Explanation of problem being solved

```
The problem we are trying to solve is to build a simple microservice that acts as a wrapper around a 3rd-party SMS service API. The 3rd-party SMS service requires an HTTP request to send an SMS message to a customer's phone number, and we want to abstract this HTTP interaction behind an asynchronous flow in our microservice.

By creating this microservice, we aim to achieve the following:

Simplified Integration: The microservice will provide a simple and standardized interface for other services within our system to send SMS messages. This abstracts the complexity of interacting with the 3rd-party SMS service API directly, making it easier for developers to use the SMS functionality.

Asynchronous Handling: As SMS delivery might not be instantaneous, the microservice can use asynchronous communication to handle SMS sending. This ensures that the main application can continue executing without waiting for the SMS to be sent successfully.

Error Handling and Resilience: By implementing the Circuit Breaker pattern (as mentioned in a previous response), we can handle failures gracefully. If the 3rd-party SMS service experiences issues or becomes unresponsive, our microservice can protect the main application from potential cascading failures.

Scalability: The microservice can be designed to handle a high volume of SMS requests efficiently, allowing for horizontal scaling as the system's load increases.

Monitoring and Logging: Implementing proper monitoring and logging in the microservice allows us to track the SMS sending process, detect any issues, and troubleshoot problems effectively.
```

### If I had more time

- i would publish the library to a private packagae repository to make it resusable acrrose microservices
- implement a circuit breaker pattern to avoid unnecesaary retries on a queue that has been down past a defined failure threshold (fail fast and graceful degredation)
- write more thorough test ,integration test with the concrete implementation
