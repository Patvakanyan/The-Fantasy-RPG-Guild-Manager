using System;

public abstract class ABaseHero : IHero
{
    private int _id;
    private Resource _dailyCost;
    private string _name;

    public int Id
    {
        get => _id;
        private set
        {
            if (value <= 0 || value > 1000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value,
                    "Hero ID must be between 1 and 1000.");
            }

            _id = value;
        }
    }

    public Resource DailyCost
    {
        get => _dailyCost;
        private set
        {
            _dailyCost = value;
        }
    }

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Hero name cannot be null or empty.",
                    nameof(value));
            }

            _name = value;
        }
    }

    protected ABaseHero(int id, string name, decimal dailyCost)
    {
        _id = 0;
        _name = string.Empty;
        _dailyCost = new Resource("Gold", 0m);

        Id = id;
        Name = name;
        DailyCost = new Resource("Gold", dailyCost);
    }
}