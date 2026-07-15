using System;

public class BankruptException : Exception
{
    public BankruptException()
        : base("The guild is bankrupt.")
    {
    }

    public BankruptException(decimal required, decimal available)
        : base($"The guild is bankrupt. Required: {required}, Available: {available}.")
    {
    }
}