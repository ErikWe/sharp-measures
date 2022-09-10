﻿namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Maths;

using SharpMeasures.Generators.Vectors.Documentation;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType? Difference { get; }

    public NamedType? Scalar { get; }
    public NamedType? SquaredScalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, bool implementSum, bool implementDifference, NamedType? difference, NamedType? scalar, NamedType? squaredScalar,
        NamedType unit, NamedType unitQuantity, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        Scalar = scalar;
        SquaredScalar = squaredScalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        Documentation = documentation;
    }
}