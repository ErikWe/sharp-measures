namespace SharpMeasures.Generators.Parsing.Scalars.ScalarConstantAttribute;

using OneOf;

using SharpMeasures;

/// <inheritdoc cref="IRawScalarConstant"/>
internal sealed record class RawScalarConstant : IRawScalarConstant
{
    private string? Name { get; }
    private string? UnitInstance { get; }
    private OneOf<double, string?> Value { get; }
    private bool? GenerateMultiplesProperty { get; }
    private string? MultiplesName { get; }

    private IScalarConstantSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawScalarConstant"/>, representing a parsed <see cref="ScalarConstantAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IRawScalarConstant.Name" path="/summary"/></param>
    /// <param name="unitInstance"><inheritdoc cref="IRawScalarConstant.UnitInstanceName" path="/summary"/></param>
    /// <param name="value"><inheritdoc cref="IRawScalarConstant.Value" path="/summary"/></param>
    /// <param name="generateMultiplesProperty"><inheritdoc cref="IRawScalarConstant.GenerateMultiplesProperty" path="/summary"/></param>
    /// <param name="multiplesName"><inheritdoc cref="IRawScalarConstant.MultiplesName" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawScalarConstant.Syntax" path="/summary"/></param>
    public RawScalarConstant(string? name, string? unitInstance, OneOf<double, string?> value, bool? generateMultiplesProperty, string? multiplesName, IScalarConstantSyntax? syntax)
    {
        Name = name;
        UnitInstance = unitInstance;
        Value = value;
        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;

        Syntax = syntax;
    }

    string? IRawScalarConstant.Name => Name;
    string? IRawScalarConstant.UnitInstanceName => UnitInstance;
    OneOf<double, string?> IRawScalarConstant.Value => Value;
    bool? IRawScalarConstant.GenerateMultiplesProperty => GenerateMultiplesProperty;
    string? IRawScalarConstant.MultiplesName => MultiplesName;

    IScalarConstantSyntax? IRawScalarConstant.Syntax => Syntax;
}
