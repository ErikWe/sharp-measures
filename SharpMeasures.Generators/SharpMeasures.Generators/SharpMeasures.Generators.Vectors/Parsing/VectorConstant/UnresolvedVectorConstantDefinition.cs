namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Raw.Vectors;

using System.Collections.Generic;

internal record class UnresolvedVectorConstantDefinition : AUnresolvedQuantityConstantDefinition<VectorConstantLocations>, IRawVectorConstant
{
    public IReadOnlyList<double> Value => value;

    private ReadOnlyEquatableList<double> value { get; }

    public UnresolvedVectorConstantDefinition(string name, string unit, IReadOnlyList<double> value, bool generateMultiplesProperty, string? multiples,
        VectorConstantLocations locations)
        : base(name, unit, generateMultiplesProperty, multiples, locations)
    {
        this.value = value.AsReadOnlyEquatable();
    }
}
