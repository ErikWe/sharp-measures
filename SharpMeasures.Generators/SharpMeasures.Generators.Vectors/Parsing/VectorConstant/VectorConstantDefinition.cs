namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

internal record class VectorConstantDefinition : AQuantityConstantDefinition<VectorConstantLocations>, IVectorConstant
{
    public IReadOnlyList<double> Value => value;

    private ReadOnlyEquatableList<double> value { get; }

    public VectorConstantDefinition(string name, string unit, IReadOnlyList<double> value, bool generateMultiplesProperty, string? multiples, VectorConstantLocations locations)
        : base(name, unit, generateMultiplesProperty, multiples, locations)
    {
        this.value = value.AsReadOnlyEquatable();
    }
}
