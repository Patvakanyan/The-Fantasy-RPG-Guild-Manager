using System;

public class Warrior: ABaseHero
{
	public int _strengthLevel;

	public int StrengthLevel
    {
		get;
		set
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
    public Mage(int id, string name, decimal dailyCost, int sLevel)
        : base(id, name, dailyCost)
    {
        StrengthLevel = sLevel;
    }
}
