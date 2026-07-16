using System;

public class Mage : ABaseHero
{
    private int _magicLevel;

    public int MagicLevel
    {
        get => _magicLevel;
        private set
        {
            if (value < 0 || value > 1000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value,
                    "Magic level must be between 0 and 1000.");
            }

            _magicLevel = value;
        }
    }

    public Mage(
        int id,
        string name,
        Resource dailyCost,
        int magicLevel)
        : base(id, name, dailyCost)
    {
        MagicLevel = magicLevel;
    }
}