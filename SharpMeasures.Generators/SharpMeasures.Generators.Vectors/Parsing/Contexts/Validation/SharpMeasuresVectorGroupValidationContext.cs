﻿namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal sealed record class SharpMeasuresVectorGroupValidationContext : ISharpMeasuresVectorGroupValidationContext
{
    public DefinedType Type { get; }

    public VectorProcessingData ProcessingData { get; }

    public IUnitPopulation UnitPopulation { get; }
    public IScalarPopulation ScalarPopulation { get; }
    public IVectorPopulation VectorPopulation { get; }

    public SharpMeasuresVectorGroupValidationContext(DefinedType type, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        Type = type;

        ProcessingData = processingData;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}
