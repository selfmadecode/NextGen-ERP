# NextGen ERP
# HR Service Documentation

This documentation provides information on how to interact with the HR service and set up the necessary MongoDB database using Docker.
The HR service relies on a MongoDB database for CRUD operations. Please follow the steps below to get started.

## Prerequisites

Before you can interact with the HR service, you will need to ensure that you have the following prerequisites in place:

1. **Docker Engine**: Docker is required to create and manage containers, including the MongoDB container for the HR service.

    Install Docker Engine by following the official Docker installation instructions: [Docker Installation Guide](https://docs.docker.com/get-docker/)

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