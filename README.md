# Message-Driven System

## Introduction

This repository contains two key services: a **Producer** and a **Consumer**, developed using .NET and C# with RabbitMQ as the message broker. The Producer is responsible for sending messages to a RabbitMQ queue, while the Consumer consumes those messages and processes them. It also exposes some endpoints to retrieve metrics such as the total number of processed messages and the total number of successfully processed messages.

### 🛠️ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- JetBrains Rider, Visual Studio, or Visual Studio Code (optional but recommended)

---

### 🐳 Running the Full System (Producer + Consumer + RabbitMQ)

1. **Clone the repository:**

    ```bash
    git clone https://github.com/darshakardeshana/MessageDrivenSystem
    cd MessageDrivenSystem
    ```

2. **Build and run all services using Docker Compose:**

    ```bash
    docker-compose up --build
    ```

   This will:
   - Build Docker images for the Producer and Consumer APIs.
   - Start the RabbitMQ server.
   - Run all three containers together.

3. **Access the following:**
   - Producer Swagger UI: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)
   - Consumer Swagger UI: [http://localhost:8082/swagger/index.html](http://localhost:8082/swagger/index.html)
   - RabbitMQ Management UI: [http://localhost:15672](http://localhost:15672)
      - Username: `guest`
      - Password: `guest`

---

### 📬 Usage Instructions

- **Publish a Message**
   - Open the **Producer API** Swagger UI.
   - Use the `/api/message` endpoint to POST a new message.
   - Example body:

     ```json
     "This is my message"
     ```

- **Consume Messages**
   - The **Consumer API** automatically listens to the `message-queue` queue.
   - You can view processed metrics via the Consumer API Swagger.

---

### 🧪 Running Unit Tests

1. Navigate to the test project directory:

    ```bash
    cd test/MessageConsumer.Tests
    cd test/MessageProducer.Tests
    ```

2. Run all tests:

    ```bash
    dotnet test
    ```

- Tests validate the correct behavior of the message publishing service (e.g., ensuring no exceptions are thrown).

---

### Future Enhancements
- **Retry Logic:** Implement retry logic in the Consumer service to handle transient failures during message processing. This can help ensure messages are reprocessed a specified number of times before being marked as failed.
- **Unit Testing:** Increase test coverage by adding unit tests for critical components of both services.
- **Multiple Consumers:** Implement the ability to have multiple consumers to process messages in parallel for increased throughput.