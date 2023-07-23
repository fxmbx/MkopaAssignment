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

###

If i had more time i would have implemented exponential backoff for retry. it is an algorithm that allows you retry exponentially.
basically giving the queue more time to come back up for each retry.
