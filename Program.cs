using System;
using System.Collections.Generic;
using System.Text;
public static class Program
{
    private static void Present()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("EXIT");
        Console.WriteLine("RECRUIT");
        Console.WriteLine("QUEST");
        Console.WriteLine("REPORT");
    }

    private static void Recruit(GuildRoster<IHero> roster)
    {
        Console.Write("Hero type (Mage/Warrior): ");
        string? heroType = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(heroType))
        {
            Console.WriteLine("Hero type cannot be empty.");
            return;
        }

        Console.Write("Hero ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Please enter a valid ID.");
            return;
        }

        Console.Write("Hero name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Hero name cannot be empty.");
            return;
        }

        Console.Write("Daily cost amount: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            Console.WriteLine("Please enter a valid daily cost.");
            return;
        }

        Console.Write("Resource type (Gold/Mana): ");
        string? resourceType = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(resourceType))
        {
            Console.WriteLine("Resource type cannot be empty.");
            return;
        }

        try
        {
            Resource dailyCost = new Resource(resourceType, amount);

            if (heroType.Equals(
                "Mage",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Magic level: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int magicLevel))
                {
                    Console.WriteLine(
                        "Please enter a valid magic level.");
                    return;
                }

                Mage mage = new Mage(
                    id,
                    name,
                    dailyCost,
                    magicLevel);

                roster.AddHero(mage);

                Console.WriteLine(
                    $"Mage {name} was recruited successfully.");
            }
            else if (heroType.Equals(
                "Warrior",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Strength level: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int strengthLevel))
                {
                    Console.WriteLine(
                        "Please enter a valid strength level.");
                    return;
                }

                Warrior warrior = new Warrior(
                    id,
                    name,
                    dailyCost,
                    strengthLevel);

                roster.AddHero(warrior);

                Console.WriteLine(
                    $"Warrior {name} was recruited successfully.");
            }
            else
            {
                Console.WriteLine(
                    "Unknown hero type. Enter Mage or Warrior.");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"Hero could not be recruited: {exception.Message}");
        }
    }
    private static void Quest(GuildRoster<IHero> roster)
    {
        Console.Write("Input ID: ");
        string? inputId = Console.ReadLine();

        if (!int.TryParse(inputId, out int id))
        {
            Console.WriteLine("Please enter a valid hero ID.");
            return;
        }

        try
        {
            roster.DispatchOnQuest(id);
            Console.WriteLine(
                $"Hero with ID {id} was dispatched successfully.");
        }
        catch (HeroUnavailableException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    private static void Report(GuildRoster<IHero> roster)
    {
        GuildReport<IHero> guildReport = new GuildReport<IHero>(roster);
        StringBuilder reportBuilder = new StringBuilder();

        reportBuilder.AppendLine("Top 3 Most Expensive Heroes");

        IEnumerable<IHero> expensiveHeroes =
            guildReport.GetMostExpensiveHeroes(3);

        int heroNumber = 1;
        foreach(IHero hero in expensiveHeroes)
        {
            reportBuilder.AppendLine(
                $"{heroNumber}. {hero.Name} — " +
                $"{hero.GetType().Name} — " +
                $"{hero.DailyCost}");

            heroNumber++;
        }
        if (heroNumber == 1)
        {
            reportBuilder.AppendLine("No heroes found.");
        }

        reportBuilder.AppendLine();
        reportBuilder.AppendLine("Available Warriors");

        IEnumerable<Warrior> availableWarriors =
            guildReport.GetAvailableWarriors();

        int warriorNumber = 1;

        foreach (Warrior warrior in availableWarriors)
        {
            reportBuilder.AppendLine(
                $"{warriorNumber}. {warrior.Name} — " +
                $"Strength: {warrior.StrengthLevel}");

            warriorNumber++;
        }

        if (warriorNumber == 1)
        {
            reportBuilder.AppendLine("No available warriors.");
        }
        Console.WriteLine(reportBuilder.ToString());

    }
    public static void Main()
    {
        GuildRoster<IHero> roster = new();
        roster.OnHeroesDepleted += () =>
        {
            ConsoleColor previousColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                "WARNING: No heroes left to defend the guild!");

            Console.ForegroundColor = previousColor;
        };
        while (true)
        {
            Present();

            Console.Write("Input command: ");
            string? line = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string command = line.Trim().ToUpperInvariant();

            if (command == "EXIT")
            {
                break;
            }

            if (command == "RECRUIT")
            {
                Recruit(roster);
            }
            else if (command == "QUEST")
            {
                Quest(roster);
            }
            else if (command == "REPORT")
            {
                Report(roster);
            }
            else
            {
                Console.WriteLine("Unknown command.");
            }
        }
    }
}