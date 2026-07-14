using System;

public struct Resource
{
    private string _type;
    private decimal _amount;

    public string Type
    {
        get => _type; private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                   "Resource type cannot be null or empty.",
                   nameof(value));
            if (value != "Gold" && value != "Mana")
            {
                throw new ArgumentException(
                    $"Unsupported resource type: {value}.",
                    nameof(value));
            }
            _type = value;
        }
    }
    public decimal Amount
    {
        get => _amount;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value,
                    "Resource amount cannot be negative.");
            }

            _amount = value;
        }
    }

    public Resource(string type, decimal amount)
    {
        _type = string.Empty;
        _amount = 0;

        Type = type;
        Amount = amount;
    }
    public override string ToString()
    {
        return $"{Amount} {Type}";
    }
    private static void ValidateSameType(Resource left, Resource right)
    {
        if (left.Type != right.Type)
        {
            throw new InvalidOperationException(
                $"Cannot operate on different resource types: " +
                $"{left.Type} and {right.Type}.");
        }
    }

    public static Resource operator +(Resource left, Resource right)
    {
        ValidateSameType(left, right);
        return new Resource(left.Type, left.Amount + right.Amount);
    }
    public static Resource operator -(Resource left, Resource right)
    {
        ValidateSameType(left, right);
        if (left < right)
            throw new InvalidOperationException(
                $"Cannot subtract {right.Amount} {right.Type} " +
                $"from {left.Amount} {left.Type}.");
        return new Resource(left.Type, left.Amount - right.Amount);
    }

    public static bool operator <(Resource left, Resource right)
    {
        ValidateSameType(left, right);
        return left.Amount < right.Amount;
    }
    public static bool operator >(Resource left, Resource right)
    {
        ValidateSameType(left, right);
        return left.Amount > right.Amount;
    }

}
