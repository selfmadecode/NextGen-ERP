# NextGen ERP

# HR.API Service Documentation

This documentation provides information on how to interact with the HR service and set up the necessary MongoDB database using Docker.
The HR service relies on a MongoDB database for CRUD operations. Please follow the steps below to get started.

## Prerequisites

Before you can interact with the HR service, you will need to ensure that you have the following prerequisites in place:

1. **Docker Engine**: Docker is required to create and manage containers, including the MongoDB container for the HR service.

    Install Docker Engine by following the official Docker installation instructions: [Docker Installation Guide](https://docs.docker.com/get-docker/)

## Pulling the MongoDB Docker Image

Before setting up the HR service, you need to pull the official MongoDB Docker image from Docker Hub. Use the following command to pull the image:

```bash
docker pull mongo
```

This command will download the MongoDB image from Docker Hub and make it available for container creation.

## Setting up the MongoDB Database

To use the HR service, you need to create a MongoDB database by running a Docker container with MongoDB. Please follow these steps:

1. Open a terminal on your system.

2. Use the following Docker command to pull the official MongoDB image from Docker Hub and run a container with MongoDB. This will also map port 27017 on your local system to port 27017 in the MongoDB container. The `-v` option is used to create a named volume for the MongoDB data storage.

    ```bash
    docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
    ```

    - `-d`: Run the container in the background.
    - `--rm`: Remove the container when it stops.
    - `--name mongo`: Assign a name to the container (in this case, "mongo").
    - `-p 27017:27017`: Map port 27017 in the container to port 27017 on your local system.
    - `-v mongodbdata:/data/db`: Create a named volume "mongodbdata" to persist MongoDB data.

3. Use the following command to confirm that the MongoDB container is running:

    ```bash
    docker ps
    ```

    This command will display a list of running containers, including the "mongo" container.

## Interacting with the HR Service

With the MongoDB database container running, you can now interact with the HR service. The HR service will use the MongoDB container as its database.

That's it! You have successfully set up the HR service to work with a MongoDB database using Docker.

-------------------------------------------------------------------------------------------------------------

# Interacting with the HR Service via Docker

This documentation provides step-by-step instructions for setting up and interacting with the HR.API application and MongoDB using Docker.

## Prerequisites

- Docker installed on your machine: [Install Docker](https://docs.docker.com/get-docker/)
- Basic knowledge of Docker commands and concepts

## Step 1: Create a Docker Network

To enable communication between the HR.API and MongoDB containers, create a Docker network named `hrservicenetwork` using the following command:

```bash
docker network create hrservicenetwork
```

## Step 2: Verify the Created Network

Run the following command to list the Docker networks and ensure that `hrservicenetwork` is listed:

```bash
docker network ls
```

Make sure the MongoDB container is not running before proceeding to the next step. If it is running, kill and remove it. 

## Step 3: Build MongoDB Image and Run Container

Build the MongoDB image and set the network tag to `hrservicenetwork` using the following command:

```bash
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db --network=hrservicenetwork mongo
```

This command creates a MongoDB container named `mongo`, exposes port 27017, and attaches it to the `hrservicenetwork` network.

## Step 4: Build the HR.API Docker Image

Build the HR.API Docker image using the provided Dockerfile. Make sure you are in the directory (`NextGen ERP\src`) and run the following command:

```bash
docker build -f Dockerfile -t hrservice .
```

## Step 5: Run the HR.API Docker Container

Run the HR.API image and set the network tag to `hrservicenetwork` using the following command:

```bash
docker run -it --rm -p 8080:80 -e MongoDBSettings:Host=mongo --network=hrservicenetwork hrservice
```

This command starts the HR.API container, maps port 8080 to the host machine, sets the MongoDB host environment variable, and connects the container to the `hrservicenetwork` network.

You have now successfully set up and run the HR.API application and MongoDB in Docker. The HR.API application should be accessible at `http://localhost:8080`.
Ensure that you have the necessary configurations and dependencies in place for a complete and functional application.

-------------------------------------------------------------------------------------------------------------

