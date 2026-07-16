using System;

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

    public static void Main()
    {
        GuildRoster<IHero> roster = new();

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
                Console.WriteLine(
                    "QUEST is not implemented yet.");
            }
            else if (command == "REPORT")
            {
                Console.WriteLine(
                    "REPORT is not implemented yet.");
            }
            else
            {
                Console.WriteLine("Unknown command.");
            }
        }
    }
}