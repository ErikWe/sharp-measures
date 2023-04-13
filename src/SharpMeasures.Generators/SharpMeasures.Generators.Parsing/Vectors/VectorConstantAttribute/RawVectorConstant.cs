namespace SharpMeasures.Generators.Parsing.Vectors.VectorConstantAttribute;

using OneOf;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawVectorConstant"/>
internal sealed record class RawVectorConstant : IRawVectorConstant
{
    private string? Name { get; }
    private string? UnitInstanceName { get; }
    private OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; }

    private IVectorConstantSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawVectorConstant"/>, representing a parsed <see cref="VectorConstantAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawVectorConstant.Name" path="/summary"/></param>
    /// <param name="unitInstanceName"><inheritdoc cref="IRawVectorConstant.UnitInstanceName" path="/summary"/></param>
    /// <param name="value"><inheritdoc cref="IRawVectorConstant.Value" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawVectorConstant.Syntax" path="/summary"/></param>
    public RawVectorConstant(string? name, string? unitInstanceName, OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> value, IVectorConstantSyntax? syntax)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Value = value;

        Syntax = syntax;
    }

    string? IRawVectorConstant.Name => Name;
    string? IRawVectorConstant.UnitInstanceName => UnitInstanceName;
    OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> IRawVectorConstant.Value => Value;

    IVectorConstantSyntax? IRawVectorConstant.Syntax => Syntax;
}
