using System;

public class Warrior : ABaseHero
{
    private int _strengthLevel;

	public int StrengthLevel
    {
        get => _strengthLevel;
        private set
		{
            if (value < 0 || value > 1000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value,
                    "Warrior level must be between 0 and 1000.");
            }
            _strengthLevel = value;
        }
	}
    public Warrior(int id, string name, Resource dailyCost, int sLevel)
        : base(id, name, dailyCost)
    {
        StrengthLevel = sLevel;
    }
}
