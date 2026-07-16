using System;

public class BonusService
{
    private readonly Func<IHero, Resource> bonusCalculator;

    public BonusService(Func<IHero, Resource> bonusCalculator)
    {
        this.bonusCalculator = bonusCalculator;
    }

    public Resource CalculateBonus(IHero hero)
    {
        return bonusCalculator(hero);
    }
}
