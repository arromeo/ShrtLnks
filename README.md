# ShrtLnks

ShrtLnks is a link shortening service built using ASP.NET Core MVC. Users can create an account, shorten their links and manage all of their previous links which includes editing where they redirect to and deleting them.

## Goal

The goal of this project was to begin learning ASP.NET as well as other .NET concepts such as Entity Framework, Linq and dependency injection. The MVP of this project was about as simple as it gets, with a single CRUD controller for handling links, the built in Identity authentication setup and a simple controller to handle redirects.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

To install and run this project you will need a machine running Windows with the .NET SDK. The project currently uses LocalDb which it's my understanding is only available on Windows. I plan on switching the database provider and will update this README when that is done.

### Installing

1. Clone this project onto your local machine.
2. In a terminal, navigate to the project folder `/ShrtLnks/ShrtLnks` and run `dotnet restore` to download the project dependencies.
3. Run `dotnet ef database update` to create the necessary database and tables.
4. Start the project by running `dotnet run` and in your browser of choice navigate to `localhost:5000`.

## Contributing

Being that this was my first attempt at a ASP.NET project, I'm certain there are mistakes that were made and patterns that were missed. I would be very appreciative to hear some critical analysis of my code to help me be a better programmer and to improve faster.

## Authors

* **Adam Romeo** - *Everything* - [Portfolio](https://adamromeo.dev/)

## Resources

* The book Programming ASP.NET Core by Dino Esposito
* YouTuber [kudvenkat's](https://www.youtube.com/user/kudvenkat) fantastic C# and ASP.NET videos
