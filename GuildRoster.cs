using System;
using System.Collections.Generic;
using System.Linq;

public class GuildRoster<T> where T : IHero
{
    private readonly List<T> heroes = new();
    private readonly Dictionary<int, string> heroStatuses = new();

    public IReadOnlyList<T> Heroes => heroes;

    public event Action? OnHeroesDepleted;

    public string GetHeroStatus(int heroId)
    {
        if (!heroStatuses.TryGetValue(
            heroId,
            out string? status))
        {
            throw new HeroUnavailableException(
                $"Hero with ID {heroId} was not found.");
        }

        return status;
    }

    public void AddHero(T hero)
    {
        if (hero is null)
        {
            throw new ArgumentNullException(nameof(hero));
        }

        if (heroStatuses.ContainsKey(hero.Id))
        {
            throw new ArgumentException(
                $"A hero with ID {hero.Id} already exists.");
        }

        heroes.Add(hero);
        heroStatuses.Add(hero.Id, "Idle");
    }

    public void DispatchOnQuest(int heroId)
    {
        if (!heroStatuses.ContainsKey(heroId))
        {
            throw new HeroUnavailableException(
                $"A hero with ID {heroId} was not found.");
        }

        if (heroStatuses[heroId] == "On Quest")
        {
            throw new HeroUnavailableException(
                $"The hero with ID {heroId} is already on a quest.");
        }

        heroStatuses[heroId] = "On Quest";

        bool hasIdleHeroes = heroStatuses.Values.Any(
            status => status == "Idle");

        if (!hasIdleHeroes)
        {
            OnHeroesDepleted?.Invoke();
        }
    }
}