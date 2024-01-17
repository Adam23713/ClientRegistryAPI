# Users Manager API

This project is a web API designed to handle user management, allowing you to efficiently retrieve and manage a list of users. The supported actions include:

- **Create**: Add a new user to the system.
- **Update**: Modify existing user information.
- **Delete**: Remove a user from the system.
- **List**: Retrieve a list of all users.
- **Get by ID**: Fetch a specific user based on their unique identifier.

## Technology Stack

- **Database**: MongoDB
- **Web API Framework**: C#, .Net Core 6, Swagger, OpenAPI
- **Test Framework**: xUnit with .Net Core 6

## Usage

To interact with the Users Manager API, you can utilize the following endpoints or use Swagger (the API self-documented):

- **Create User**: `POST /users`
- **Update User**: `PUT /users/{id}`
- **Delete User**: `DELETE /users/{id}`
- **List Users**: `GET /users`
- **Get User by ID**: `GET /users/{id}`

## Getting Started

1. Clone this repository.
2. Set up a MongoDB database to store user information.
3. Configure the project to connect to your MongoDB instance.
4. Run the application.

## Test Data

During development and testing, the [JSONPlaceholder users dataset](https://jsonplaceholder.typicode.com/users) was utilized to simulate real-world user scenarios.

Feel free to explore the API and provide feedback or contributions. Thank you for using the Users Manager API!
