using System;

public class HeroUnavailableException : Exception
{
    public HeroUnavailableException()
        : base("The hero is unavailable.")
    {
    }

    public HeroUnavailableException(string message)
        : base(message)
    {
    }

    public HeroUnavailableException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}