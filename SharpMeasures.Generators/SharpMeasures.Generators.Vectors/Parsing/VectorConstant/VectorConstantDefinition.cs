﻿namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

internal sealed record class VectorConstantDefinition : AQuantityConstantDefinition<VectorConstantLocations>, IVectorConstant
{
    public IReadOnlyList<double> Value { get; }

    IVectorConstantLocations IVectorConstant.Locations => Locations;

    public VectorConstantDefinition(string name, string unit, IReadOnlyList<double> value, bool generateMultiplesProperty, string? multiples, VectorConstantLocations locations) : base(name, unit, generateMultiplesProperty, multiples, locations)
    {
        Value = value.AsReadOnlyEquatable();
    }
}
