namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="ScalarConstantAttribute"/>.</summary>
public interface IScalarConstantSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the name of the constant.</summary>
    public abstract Location Name { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the unit instance in which the provided value is expressed.</summary>
    public abstract Location UnitInstanceName { get; }

    /// <summary>The <see cref="Location"/> of the argument for the value of the constant.</summary>
    public abstract Location Value { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether to implement a property describing the magnitude of the scalar in terms of multiples of this constant. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location GenerateMultiplesProperty { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name describing multiples of this constant, if provided. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location MultiplesName { get; }
}
