# The Fantasy RPG Guild Manager

A console-based C# application for managing a fantasy adventurer guild.

The application allows users to recruit heroes, manage their availability, dispatch them on quests, calculate bonuses, and generate guild reports.

This project demonstrates core C# concepts including value types, reference types, inheritance, interfaces, generics, custom exceptions, delegates, events, LINQ, collections, and safe user input handling.

## Technologies

* C#
* .NET 6 or higher
* Console Application

## Project Goals

The main purpose of this project is to practice and demonstrate:

* Value types and reference types
* Structures
* Interfaces
* Abstract classes
* Inheritance
* Operator overloading
* Generic classes
* Lists and dictionaries
* Custom exceptions
* Delegates and lambda expressions
* Events
* LINQ
* StringBuilder
* Exception handling
* Safe console input parsing

## Features

### Resource Management

Guild resources are represented by the `Resource` structure.

Each resource contains:

* An amount
* A resource type, such as `Gold` or `Mana`

Resources support:

* Addition
* Subtraction
* Greater-than comparison
* Less-than comparison

Resources of different types cannot be combined directly.

For example, Gold cannot be added to Mana without a conversion system.

The application also prevents unsafe resource operations, such as subtracting more resources than are available.

### Hero System

Every hero implements the `IHero` interface.

All heroes contain:

* A unique ID
* A name
* A daily cost

The shared hero properties are implemented in the abstract `BaseHero` class.

Two hero types are available:

#### Mage

A Mage has all common hero properties and an additional `MagicLevel` property.

#### Warrior

A Warrior has all common hero properties and an additional `StrengthLevel` property.

### Guild Roster

The generic `GuildRoster<T>` class manages heroes that implement the `IHero` interface.

It uses:

* A `List<T>` to store hero objects
* A `Dictionary<int, string>` to store each hero's current status

Possible statuses include:

* `Idle`
* `On Quest`

The roster supports:

* Adding new heroes
* Finding heroes by ID
* Checking hero availability
* Dispatching heroes on quests

A hero who is already on a quest cannot be dispatched again.

### Custom Exceptions

The project contains custom exception types for domain-specific errors.

#### HeroUnavailableException

Thrown when a hero cannot be dispatched, for example when the hero is already on a quest.

#### BankruptException

Thrown when a resource operation would leave the guild with an invalid negative resource amount.

All exceptions are handled safely so that the application continues running instead of crashing.

### Guild Alarm Event

The guild roster contains an `OnHeroesDepleted` event.

This event is triggered when dispatching a hero leaves the guild with exactly zero idle heroes.

The console application subscribes to this event and displays the following warning in red:

> WARNING: No heroes left to defend the guild!

### Bonus Calculator

The application uses a `Func<IHero, Resource>` delegate to calculate hero bonuses.

The bonus calculation logic can be passed as a lambda expression.

For example, a bonus rule can give Mage characters a Gold bonus equal to 20% of their daily cost.

Because the calculation is passed as a delegate, different bonus rules can be used without changing the main bonus-processing method.

### Guild Reports

The `GuildReport` class generates reports using LINQ.

The reporting methods do not use manual loops such as `for`, `foreach`, or `while`.

Available reports include:

#### Most Expensive Heroes

Orders heroes by daily cost from highest to lowest and returns the requested number of heroes.

#### Available Warriors

Returns only heroes who:

* Are of type `Warrior`
* Currently have the `Idle` status

### Console Commands

The application runs inside an interactive command loop.

Available commands include:

#### RECRUIT

Creates a new Mage or Warrior.

The user enters:

* Hero type
* Hero ID
* Hero name
* Daily cost
* Magic level or strength level

Numeric input is validated using safe parsing methods.

#### QUEST

Dispatches a hero on a quest using their ID.

If the hero is already on a quest or is otherwise unavailable, the application displays a polite error message and continues running.

#### REPORT

Displays formatted guild information, including:

* The most expensive heroes
* Available Warriors
* Hero names
* Hero types
* Daily costs
* Current statuses

The report output is constructed using `StringBuilder`.

#### EXIT

Closes the application safely.

## Project Structure

Each class, structure, interface, and exception is stored in its own file.

```text
The-Fantasy-RPG-Guild-Manager/
│
├── Program.cs
├── Resource.cs
├── IHero.cs
├── BaseHero.cs
├── Mage.cs
├── Warrior.cs
├── GuildRoster.cs
├── GuildReport.cs
├── HeroUnavailableException.cs
├── BankruptException.cs
├── The-Fantasy-RPG-Guild-Manager.csproj
└── README.md
```

## Application Architecture

```text
Program
   |
   | recruits heroes and processes commands
   v
GuildRoster<IHero>
   |
   | stores hero objects
   v
List<IHero>

GuildRoster<IHero>
   |
   | stores hero statuses
   v
Dictionary<int, string>

GuildReport
   |
   | reads roster data using LINQ
   v
Formatted reports
```

## How to Run

Make sure that .NET 6 or a newer version is installed.

Clone the repository:

```bash
git clone https://github.com/Patvakanyan/The-Fantasy-RPG-Guild-Manager.git
```

Open the project directory:

```bash
cd The-Fantasy-RPG-Guild-Manager
```

Restore the project dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

## Example Workflow

```text
Enter command: RECRUIT
Hero type: Mage
ID: 1
Name: Merlin
Daily cost: 100
Resource type: Gold
Magic level: 90

Hero recruited successfully.

Enter command: QUEST
Hero ID: 1

Merlin has been dispatched on a quest.

WARNING: No heroes left to defend the guild!

Enter command: REPORT
```

## Error Handling

The application is designed to remain stable when invalid input or invalid operations occur.

Handled situations include:

* Invalid numeric input
* Duplicate hero IDs
* Unknown hero IDs
* Dispatching an unavailable hero
* Combining different resource types
* Subtracting more resources than are available
* Invalid commands
* Empty user input

Instead of terminating unexpectedly, the application displays an understandable error message and returns to the command loop.

## Coding Style

The project follows these naming rules:

* Classes, structures, interfaces, properties, and methods use `PascalCase`
* Local variables and method parameters use `camelCase`
* Each type is placed in its own `.cs` file
* Methods have clear responsibilities
* Repeated logic is avoided
* User input is validated before use

## Possible Future Improvements

The project could later be expanded with:

* Quest difficulty levels
* Hero experience and levels
* Quest completion
* Returning heroes to the Idle status
* More hero classes, such as Archer or Healer
* Resource conversion
* Guild treasury management
* Persistent data storage
* JSON save and load support
* Unit tests
* More advanced bonus rules

## Author

Created as a C# learning project focused on object-oriented programming and modern .NET features.
