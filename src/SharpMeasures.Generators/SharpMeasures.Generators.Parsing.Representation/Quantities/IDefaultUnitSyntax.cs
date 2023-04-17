namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="DefaultUnitAttribute"/>.</summary>
public interface IDefaultUnitSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the name of the default unit instance.</summary>
    public abstract Location Unit { get; }

    /// <summary>The <see cref="Location"/> of the argument for the symbol representing the default unit instance.</summary>
    public abstract Location Symbol { get; }
}
