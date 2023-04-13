namespace SharpMeasures.Generators.Parsing.Scalars;

/// <summary>Represents a parsed <see cref="UnitlessQuantityAttribute"/>.</summary>
public interface IRawUnitlessQuantity
{
    /// <summary>Dictates whether the quantity allows negative magnitudes.</summary>
    public abstract bool? AllowNegative { get; }

    /// <summary>Dictates whether the quantity should support addition of two instances.</summary>
    public abstract bool? ImplementSum { get; }

    /// <summary>Dictates whether the quantity should support subtraction of two instances.</summary>
    public abstract bool? ImplementDifference { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="UnitlessQuantityAttribute"/>.</summary>
    public abstract IUnitlessQuantitySyntax? Syntax { get; }
}
