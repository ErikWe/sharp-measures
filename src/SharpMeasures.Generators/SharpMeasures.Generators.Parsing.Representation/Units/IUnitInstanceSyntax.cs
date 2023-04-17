namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed attribute that describes an instance of a unit.</summary>
public interface IUnitInstanceSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the name of the unit instance.</summary>
    public abstract Location Name { get; }

    /// <summary>The <see cref="Location"/> of the argument for the plural form name of the unit instance. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location PluralForm { get; }
}
