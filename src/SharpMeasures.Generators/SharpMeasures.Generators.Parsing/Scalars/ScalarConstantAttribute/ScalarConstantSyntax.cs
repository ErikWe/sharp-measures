namespace SharpMeasures.Generators.Parsing.Scalars.ScalarConstantAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IScalarConstantSyntax"/>
internal sealed record class ScalarConstantSyntax : IScalarConstantSyntax
{
    private Location Name { get; }
    private Location UnitInstance { get; }
    private Location Value { get; }
    private Location GenerateMultiplesProperty { get; }
    private Location MultiplesName { get; }

    /// <summary>Instantiates a <see cref="ScalarConstantSyntax"/>, representing syntactical information about a parsed <see cref="ScalarConstantAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IScalarConstantSyntax.Name" path="/summary"/></param>
    /// <param name="unitInstance"><inheritdoc cref="IScalarConstantSyntax.UnitInstanceName" path="/summary"/></param>
    /// <param name="value"><inheritdoc cref="IScalarConstantSyntax.Value" path="/summary"/></param>
    /// <param name="generateMultiplesProperty"><inheritdoc cref="IScalarConstantSyntax.GenerateMultiplesProperty" path="/summary"/></param>
    /// <param name="multiplesName"><inheritdoc cref="IScalarConstantSyntax.MultiplesName" path="/summary"/></param>
    public ScalarConstantSyntax(Location name, Location unitInstance, Location value, Location generateMultiplesProperty, Location multiplesName)
    {
        Name = name;
        UnitInstance = unitInstance;
        Value = value;
        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;
    }

    Location IScalarConstantSyntax.Name => Name;
    Location IScalarConstantSyntax.UnitInstanceName => UnitInstance;
    Location IScalarConstantSyntax.Value => Value;
    Location IScalarConstantSyntax.GenerateMultiplesProperty => GenerateMultiplesProperty;
    Location IScalarConstantSyntax.MultiplesName => MultiplesName;
}
