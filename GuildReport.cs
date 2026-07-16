using System;

public class GuildReport<T> where T : IHero
{
    private readonly GuildRoster<T> roster;

    public GuildReport(GuildRoster<T> roster)
    {
        this.roster = roster;
    }

    public IEnumerable<T> GetMostExpensiveHeroes(int count)
    {
        return roster.Heroes
            .OrderByDescending(hero => hero.DailyCost.Amount)
            .Take(count);
    }
    public IEnumerable<Warrior> GetAvailableWarriors()
    {
        return roster.Heroes
            .OfType<Warrior>()
            .Where(warrior =>
                roster.GetHeroStatus(warrior.Id) == "Idle");
    }
}
