# AI Rules for GymTracker

Gymtracker is designed to streamline the process of manually tracking gym workouts by offering functionalities to add, edit, and block exercises while recording 
detailed training sessions. It ensures consistency and accuracy by preventing duplicate exercise entries and propagating edits to historical records. 
Built with .NET MVC and secured with JWT authentication, Gymtracker provides a scalable and user-friendly platform for managing workout history.

## Tech Stack
- .net 8
- asp.net core
- razor pages
- c#
- jwt

## Project Structure
- src - main source code
  - GymTracker
	- Controllers - API controllers for handling requests
	- Models - Data models representing the application's entities
	- Views - Razor views for rendering HTML
	- wwwroot - Static files (CSS, JS, images)

## BACKEND

### Guidelines for DOTNET

#### ENTITY_FRAMEWORK

- Use the repository and unit of work patterns to abstract data access logic and simplify testing
- Implement eager loading with Include() to avoid N+1 query problems for {{entity_relationships}}
- Use migrations for database schema changes and version control with proper naming conventions
- Apply appropriate tracking behavior (AsNoTracking() for read-only queries) to optimize performance
- Implement query optimization techniques like compiled queries for frequently executed database operations
- Use value conversions for complex property transformations and proper handling of {{custom_data_types}}

#### ASP_NET

- Use minimal APIs for simple endpoints in .NET 6+ applications to reduce boilerplate code
- Implement the mediator pattern with MediatR for decoupling request handling and simplifying cross-cutting concerns
- Use API controllers with model binding and validation attributes for {{complex_data_models}}
- Apply proper response caching with cache profiles and ETags for improved performance on {{high_traffic_endpoints}}
- Implement proper exception handling with ExceptionFilter or middleware to provide consistent error responses
- Use dependency injection with scoped lifetime for request-specific services and singleton for stateless services


## DATABASE

### Guidelines for SQL

#### MYSQL

- Use InnoDB storage engine for transactions and foreign key constraints
- Implement proper indexing strategies based on {{query_patterns}}
- Use connection pooling for better performance


## DEVOPS

### Guidelines for CI_CD

#### GITHUB_ACTIONS

- Check if `package.json` exists in project root and summarize key scripts
- Check if `.nvmrc` exists in project root
- Check if `.env.example` exists in project root to identify key `env:` variables
- Always use terminal command: `git branch -a | cat` to verify whether we use `main` or `master` branch
- Always use `env:` variables and secrets attached to jobs instead of global workflows
- Always use `npm ci` for Node-based dependency setup
- Extract common steps into composite actions in separate files
- Once you're done, as a final step conduct the following: for each public action always use <tool>"Run Terminal"</tool> to see what is the most up-to-date version (use only major version) - extract tag_name from the response:
- ```bash curl -s https://api.github.com/repos/{owner}/{repo}/releases/latest ```


