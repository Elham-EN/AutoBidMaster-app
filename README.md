# AutoBidMaster - Microservice Auction App for cars (Fullstack)

## Project Overview:

The app allows users to advertise their cars on an auction platform and enables other users to bid on these cars. It's designed to offer a real-time auction experience with a bidding system and live auction features.

## Tech Stack

### Client-Side Technologies:

- Next.js & React.js with TypeScript: I chose these for the frontend to leverage React's component-based architecture, enhanced with TypeScript for type safety and better developer experience.
- Tailwind CSS: For styling, Tailwind CSS offers a utility-first approach, making it easy to design responsive and aesthetically pleasing UIs.

### Backend Microservices:

- C# & .NET Web API
- Entity Framework
- RabbitMQ and gRPC: These are employed as service buses for efficient inter-service communication.
- Microsoft YARP: Acts as a gateway, routing requests to appropriate services.
- SignalR: For real-time push notifications to the client app.
- Docker: Utilized for containerizing the services
- PostgreSQL
- MongoDB

### DevOps and Testing:

- GitHub Actions: Implements CI/CD workflows, automating the testing and deployment process.
- Docker Compose: For local development and testing.
- Unit and Integration Testing: To ensure backend quality.
- Kubernetes Deployment: For orchestrating and scaling services in a cloud environment.

### Security:

- Identity Server: Ensures that the application is secure, managing authentication and authorization effectively.

## Setup

You can run this app locally on your computer by following these instructions:

1. Using your terminal or command prompt clone the repo onto your machine in a user folder
   ```
    git clone https://github.com/Elham-EN/AutoBidMaster-app.git
   ```
2. Change into the Carsties directory
   ```
   cd AutoBidMaster
   ```
3. Ensure you have Docker Desktop installed on your machine. If
4. Build the services locally on your computer by running (NOTE: this may take several minutes to complete):
   ```
   docker compose build
   ```
5. Once this completes you can use the following to run the services:
   ```
   docker compose up -d
   ```
6. To see the app working you will need to provide it with an SSL certificate. To do this please install 'mkcert' onto your computer which you can get from [here](https://github.com/FiloSottile/mkcert). Once you have this you will need to install the local Certificate Authority by using:
   ```
   mkcert -install
   ```
7. You will then need to create the certificate and key file on your computer to replace the certificates that I used. You will need to change into the 'devcerts' directory and then run the following command:
   ```
   cd devcerts mkcert -key-file autobidmaster.com.key -cert-file autobidmaster.com.crt app.autobidmaster.com api autobidmaster.com id.autobidmaster.com
   ```
8. You will also need to create an entry in your host file so you can reach the app by its domain name. Please use this [guide](https://phoenixnap.com/kb/mac-hosts-file) if you do not know how to do this. Create the following entry:
   ```
   127.0.0.1 id.autobidmaster.com app.autobidmaster.com api.autobidmaster.com
   ```
9. You should now be able to browse to the app on https://app.autobidmaster.com
