# SaladProject

This simple RESTful service connects to the RAWG API according to the spec provided by Salad.

To use this service, an api key must be provided.
This key is configured by adding a file titled `api_key.txt` to the project's root directory.
The contents of this file should be nothing more or less than the user's API key.
No formatting, headers, or whitespace should be included.

This service has the following endpoints:

* GET `/games` allows the user to retrieve a list of games.
Two parameters are available: `q` provides search functionality on the game's title, and `sort` allows the results to be ordered.
The available options for `sort` are: `name`, `released`, `added`, `created`, `updated`, `rating`, `metacritic`.
Additionally, sorts can be inverted by prefixing a hyphen, such as `-added`.

* POST `/users` allows the addition of a user object.
Users retain an id and a list of their favorite games.
When a user is instantiated, it has an empty list of games.
The body for this post request is unused and should be left empty.

* GET `/users/{userId}` retrieves the profile of a user.
This profile will contain the user's ID and games list.

* POST `/users/{userId}/games` adds a game to a user's games list.
The game is passed through the body of this request, which should take the form `{"gameId": int id}`
The game's details will be retrieved from the RAWG API and used to populate a Game object in the user's list.

* DELETE `/users/{userId}/games/{gameId}` deletes a previously added game from a user's games list.

* POST `/users/{userId}/comparison` compares the games lists of two users.
Three comparators are available: `union`, `intersection`, and `difference`.
The body of this request contains the second user and the comparator in the following form:
`{"otherUserId": int otherUserId, "comparison": string comparator}`
