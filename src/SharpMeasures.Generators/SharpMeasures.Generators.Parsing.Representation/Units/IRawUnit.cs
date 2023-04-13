namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface IRawUnit
{
    /// <summary>The <see cref="ITypeSymbol"/> of the scalar quantity that the unit describes.</summary>
    public abstract ITypeSymbol Scalar { get; }

    /// <summary>Indicates whether the unit includes a bias term.</summary>
    public abstract bool? BiasTerm { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="UnitAttribute{TScalar}"/>.</summary>
    public abstract IUnitSyntax? Syntax { get; }
}
