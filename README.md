# Users Manager API

This project is a web API designed to handle user management, allowing you to efficiently retrieve and manage a list of users. The supported actions include:

- **Create**: Add a new user to the system.
- **Add User Later**: Add a new user to the cache and later save it to the Microsoft SQL database and then send email to user
- **Update**: Modify existing user information.
- **Delete**: Remove a user from the system.
- **List**: Retrieve a list of all users.
- **Get by ID**: Fetch a specific user based on their unique identifier.

## Technology Stack

- **Database**: MongoDB and Microsoft SQL
- **Web API Framework**: C#, .Net Core 6, Swagger, OpenAPI

## Usage

To interact with the Users Manager API, you can utilize the following endpoints or use Swagger (the API self-documented):

- **Create User**: `POST /user`
- **Add User Later**  `POST /user/adduserlater`
- **Update User**: `PUT /user/{id}`
- **Delete User**: `DELETE /user/{id}`
- **List Users**: `GET /user`
- **Get User by ID**: `GET /user/{id}`

## Getting Started

1. Clone this repository.
2. Set up a MongoDB (or use cloud mongodb) and Microsoft SQL database to store user information.
3. Configure the project to connect to your MongoDB and Microsoft SQL instance.
4. Run the application.
