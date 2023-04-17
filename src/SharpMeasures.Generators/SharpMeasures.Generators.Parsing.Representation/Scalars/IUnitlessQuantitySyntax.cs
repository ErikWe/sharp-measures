namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="UnitlessQuantityAttribute"/>.</summary>
public interface IUnitlessQuantitySyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for whether the quantity allows negative magnitudes. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location AllowNegative { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should support addition of two instances. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementSum { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should support subtraction of two instances. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementDifference { get; }
}
