﻿namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class SharpMeasuresScalarResolutionContext : SimpleProcessingContext, ISharpMeasuresScalarResolutionContext
{
    public IUnresolvedUnitPopulation UnitPopulation { get; }
    public IUnresolvedScalarPopulation ScalarPopulation { get; }
    public IUnresolvedVectorPopulation VectorPopulation { get; }

    public SharpMeasuresScalarResolutionContext(DefinedType type, IUnresolvedUnitPopulation unitPopulation, IUnresolvedScalarPopulation scalarPopulation,
        IUnresolvedVectorPopulation vectorPopulation) : base(type)
    {
        ScalarPopulation = scalarPopulation;
        UnitPopulation = unitPopulation;
        VectorPopulation = vectorPopulation;
    }
}