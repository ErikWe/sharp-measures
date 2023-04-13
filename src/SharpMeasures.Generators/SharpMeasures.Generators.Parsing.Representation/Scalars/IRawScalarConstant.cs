namespace SharpMeasures.Generators.Parsing.Scalars;

using OneOf;

/// <summary>Represents a parsed <see cref="ScalarConstantAttribute"/>.</summary>
public interface IRawScalarConstant
{
    /// <summary>The name of the constant.</summary>
    public abstract string? Name { get; }

    /// <summary>The name of the unit instance in which the provided value is expressed.</summary>
    public abstract string? UnitInstanceName { get; }

    /// <summary>The value of the constant, when expressed in the provided unit instance.</summary>
    public abstract OneOf<double, string?> Value { get; }

    /// <summary>Determines whether to implement a property describing the magnitude of the scalar in terms of multiples of this constant.</summary>
    public abstract bool? GenerateMultiplesProperty { get; }

    /// <summary>The name describing multiples of this constant, if provided.</summary>
    public abstract string? MultiplesName { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="ScalarConstantAttribute"/>.</summary>
    public abstract IScalarConstantSyntax? Syntax { get; }
}
