﻿namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public NamedType? Scalar { get; }
    
    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IReadOnlyList<IUnitInstance> IncluedUnits => includedUnits;
    public IReadOnlyList<IVectorConstant> Constants => constants;

    public IVectorDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IUnitInstance> includedUnits { get; }
    private ReadOnlyEquatableList<IVectorConstant> constants { get; }

    public DataModel(DefinedType vector, int dimension, NamedType? scalar, NamedType unit, NamedType unitQuantity, IReadOnlyList<IUnitInstance> includedUnits,
        IReadOnlyList<IVectorConstant> constants, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        this.includedUnits = includedUnits.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
