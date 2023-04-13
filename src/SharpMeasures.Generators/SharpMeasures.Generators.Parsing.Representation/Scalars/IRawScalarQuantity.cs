namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
public interface IRawScalarQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }

    /// <summary>Dictates whether the quantity allows negative magnitudes.</summary>
    public abstract bool? AllowNegative { get; }

    /// <summary>Dictates whether quantity should consider the bias term of the unit</summary>
    public abstract bool? UseUnitBias { get; }

    /// <summary>Dictates whether the quantity should support addition of two instances.</summary>
    public abstract bool? ImplementSum { get; }

    /// <summary>Dictates whether the quantity should support subtraction of two instances.</summary>
    public abstract bool? ImplementDifference { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    public abstract IScalarQuantitySyntax? Syntax { get; }
}
