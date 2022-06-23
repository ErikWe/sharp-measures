﻿namespace SharpMeasures.Generators.Vectors.Refinement.SharpMeasuresVector;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal readonly record struct RefinedSharpMeasuresVectorDefinition
{
    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }
    public ResizedVectorGroup VectorGroup { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public RefinedSharpMeasuresVectorDefinition(UnitInterface unit, ScalarInterface? scalar, ResizedVectorGroup vectorGroup, int dimension, bool implementSum,
        bool implementDifference, NamedType difference, string? defaultUnitName, string? defaultUnitSymbol, bool generateDocumentation)
    {
        Unit = unit;
        Scalar = scalar;
        VectorGroup = vectorGroup;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
