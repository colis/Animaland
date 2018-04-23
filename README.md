# Animaland

ASP.NET Core 2 Web Api sample project

## Details

The application uses the Entity Framework Core as an in-memory database.

The source code can be built and the exposed endpoints can be tested using Postman.
Controllers have also been tested using XUnit.

## Endpoints

#### Users

- **<code>GET</code> api/user** - Returns a list of users.
- **<code>GET</code> api/user/:userId** - Returns a single user.

#### Animals

- **<code>GET</code> api/animal** - Returns a list of animals.
- **<code>GET</code> api/animal/:animalId** - Returns a single animal.

#### Adoption

- **<code>PUT</code> api/user/:userId/adopt/:animalId** - Assign an animal to a specific user.

#### Feeding

- **<code>PUT</code> api/animal/:animalId/feed** - Feed an animal.

#### Petting

- **<code>PUT</code> api/animal/:animalId/pet** - Pet an animal.
