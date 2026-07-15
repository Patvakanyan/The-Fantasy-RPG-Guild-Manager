using System;

public class GuildRoster<T> where T : IHero
{
    private readonly List<T> heroes = new();
    private readonly Dictionary<int, string> heroStatuses = new();

    public void AddHero(T hero)
    {
        heroes.Add(hero);
        heroStatuses.Add(hero.Id, "Idle");
    }
}
