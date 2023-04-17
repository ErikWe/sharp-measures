namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
public interface IVectorQuantitySyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the unit that describes the quantity.</summary>
    public abstract Location Unit { get; }

    /// <summary>The <see cref="Location"/> of the argument for the dimension of the quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Dimension { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should support addition of two instances. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementSum { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should support subtraction of two instances. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementDifference { get; }
}
